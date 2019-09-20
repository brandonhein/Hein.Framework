using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Hein.Framework.Dynamo.Criterion
{
    public class ExpressionHelper
    {
        public static Dictionary<string, AttributeValue> ConvertExpressionValues<T>(Expression<Func<T, bool>> expression, ref StringBuilder expressionString)
        {
            var valueIndex = 0;

            if (string.IsNullOrWhiteSpace(expressionString.ToString()))
            {
                expressionString = new StringBuilder(expression.Body.ToString());
            }

            var result = ConvertExpressionValues(expression.Body as BinaryExpression, ref valueIndex, ref expressionString);

            SanitizeExpression(expression, ref expressionString);

            return result.ToDictionary(s => s.Key, s => s.Value);
        }

        private static IDictionary<string, AttributeValue> ConvertExpressionValues(BinaryExpression binaryExpression, ref int valueIndex, ref StringBuilder expressionString)
        {
            var expressionValues = GetExpressionValues(binaryExpression, ref valueIndex, ref expressionString);

            SanitizeComparitives(ref expressionString);

            return ConvertExpressionValues(expressionValues);
        }

        private static IDictionary<string, AttributeValue> ConvertExpressionValues(IEnumerable<KeyValuePair<string, object>> expressionValues)
        {
            var convertedExpressionValues = new Dictionary<string, AttributeValue>();

            foreach (var item in expressionValues)
            {
                if (item.Value == null)
                    convertedExpressionValues.Add(item.Key, new AttributeValue { NULL = true });
                else
                    convertedExpressionValues.Add(item.Key, DynamoValueConverter.ConvertToAttributeValue[item.Value.GetType()](item.Value));
            }

            return convertedExpressionValues;
        }

        private static IEnumerable<KeyValuePair<string, object>> GetExpressionValues(BinaryExpression binaryExpression, ref int valueIndex, ref StringBuilder expressionString)
        {
            var variableName = $":val{valueIndex}";
            IEnumerable<KeyValuePair<string, object>> expressionValues = new Dictionary<string, object>();

            expressionValues = expressionValues.Union(GetExpressionValues(binaryExpression, binaryExpression.Left, binaryExpression.Right, ref valueIndex, ref expressionString));
            expressionValues = expressionValues.Union(GetExpressionValues(binaryExpression, binaryExpression.Right, binaryExpression.Left, ref valueIndex, ref expressionString));

            return expressionValues;
        }

        private static IEnumerable<KeyValuePair<string, object>> GetExpressionValues(BinaryExpression binaryExpression, Expression primaryExpression, Expression secondaryExpression, ref int valueIndex, ref StringBuilder expressionString)
        {
            var variableName = $":val{valueIndex}";
            IEnumerable<KeyValuePair<string, object>> expressionValues = new Dictionary<string, object>();

            if (primaryExpression is UnaryExpression)
            {
                expressionValues = expressionValues.Union(GetExpressionValues(primaryExpression as UnaryExpression, ref valueIndex, ref expressionString));
            }
            else if (IsExpressionNodeType(primaryExpression.NodeType))
                expressionValues = expressionValues.Union(GetExpressionValues(primaryExpression as BinaryExpression, ref valueIndex, ref expressionString));
            else if (IsMemberNode(primaryExpression))
            {
                AppedExpressionValue(binaryExpression, primaryExpression, secondaryExpression, variableName, ref expressionValues, ref expressionString);
                valueIndex++;
            }

            return expressionValues;
        }

        private static IEnumerable<KeyValuePair<string, object>> GetExpressionValues(UnaryExpression unaryExpression, ref int valueIndex, ref StringBuilder expressionString)
        {
            var variableName = $":val{valueIndex}";
            IEnumerable<KeyValuePair<string, object>> expressionValues = new Dictionary<string, object>();

            if (!IsMemberNode(unaryExpression.Operand))
            {
                expressionValues = expressionValues.Append(new KeyValuePair<string, object>(variableName, GetValue(unaryExpression)));

                expressionString.Replace(unaryExpression.ToString(), variableName);
                valueIndex++;
            }
            else
            {
                var propertyName = GetPropertyReference(unaryExpression.Operand);

                expressionString.Replace(unaryExpression.ToString(), propertyName);
            }

            return expressionValues;
        }

        private static void AppedExpressionValue(BinaryExpression binaryExpression, Expression propertyExpression, Expression valueExpression, string variableName, ref IEnumerable<KeyValuePair<string, object>> expressionValues, ref StringBuilder expressionString)
        {
            var propertyName = GetPropertyReference(propertyExpression);

            expressionValues = expressionValues.Append(new KeyValuePair<string, object>(variableName, GetValue(valueExpression)));

            expressionString.Replace(binaryExpression.ToString(), $"{propertyName} {binaryExpression.NodeType} {variableName}");
        }

        private static string GetPropertyReference(Expression expression)
        {
            return DynamoAttributeParser.GetPropertyAttribute(expression.ToString());
        }

        // Needs revising for a more generic approach
        private static object GetValue(Expression expression)
        {
            if (ExpValueMapping.ContainsKey(expression.Type))
            {
                return ExpValueMapping[expression.Type](expression);
            }
            else
            {
                return ExpValueMapping[typeof(object)](expression);
            }
        }

        private static bool IsMemberNode(Expression expression)
        {
            if (expression.NodeType != ExpressionType.MemberAccess)
            {
                return false;
            }
            if (expression is MemberExpression)
            {
                if (!(((MemberExpression)expression).Expression is ParameterExpression))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsExpressionNodeType(ExpressionType nodeType)
        {
            if (nodeType != ExpressionType.MemberAccess &&
                nodeType != ExpressionType.Constant &&
                nodeType != ExpressionType.New &&
                nodeType != ExpressionType.Call)
            {
                return true;
            }

            return false;
        }

        private static void SanitizeExpression<T>(Expression<Func<T, bool>> expression, ref StringBuilder expressionString)
        {
            foreach (var parameter in expression.Parameters)
            {
                expressionString.Replace($"{parameter.Name}.", "");
            }

            foreach (Match match in Regex.Matches(expressionString.ToString(), @"(#\w+)+"))
            {
                expressionString.Replace(match.Value, DynamoAttributeParser.GetPropertyAttribute(match.Value.Substring(1)));
            }
        }

        private static void SanitizeComparitives(ref StringBuilder expressionString)
        {
            foreach (var comparitive in Comparitives)
            {
                expressionString.Replace(comparitive.Key, comparitive.Value);
            }
        }

        internal static readonly IDictionary<string, string> Comparitives = new Dictionary<string, string>
        {
            { "LessThanOrEqual", "<=" },
            { "GreaterThanOrEqual", ">=" },
            { "AndAlso", "AND" },
            { "OrElse", "OR" },
            { "NotEqual", "<>" },
            { "GreaterThan", ">" },
            { "LessThan", "<" },
            { "Equal", "=" },
            { "==", "=" },
            { "!=", "<>" },
            { "&&", "AND" },
            { "||", "OR" },
            { "|", "OR" },
            { "&", "AND" },
        };

        internal static readonly IDictionary<Type, Func<Expression, object>> ExpValueMapping = new Dictionary<Type, Func<Expression, object>>
        {
            { typeof(bool), (expression) => Expression.Lambda<Func<bool>>(expression).Compile()() },
            { typeof(bool?), (expression) => Expression.Lambda<Func<bool?>>(expression).Compile()() },
            { typeof(byte), (expression) => Expression.Lambda<Func<byte>>(expression).Compile()() },
            { typeof(byte?), (expression) => Expression.Lambda<Func<byte?>>(expression).Compile()() },
            { typeof(byte[]), (expression) => Expression.Lambda<Func<byte[]>>(expression).Compile()() },
            { typeof(char), (expression) => Expression.Lambda<Func<char>>(expression).Compile()() },
            { typeof(char?), (expression) => Expression.Lambda<Func<char?>>(expression).Compile()() },
            { typeof(DateTime), (expression) => Expression.Lambda<Func<DateTime>>(expression).Compile()() },
            { typeof(DateTime?), (expression) => Expression.Lambda<Func<DateTime?>>(expression).Compile()() },
            { typeof(decimal), (expression) => Expression.Lambda<Func<decimal>>(expression).Compile()() },
            { typeof(decimal?), (expression) => Expression.Lambda<Func<decimal?>>(expression).Compile()() },
            { typeof(double), (expression) => Expression.Lambda<Func<double>>(expression).Compile()() },
            { typeof(double?), (expression) => Expression.Lambda<Func<double?>>(expression).Compile()() },
            { typeof(float), (expression) => Expression.Lambda<Func<float>>(expression).Compile()() },
            { typeof(float?), (expression) => Expression.Lambda<Func<float?>>(expression).Compile()() },
            { typeof(short), (expression) => Expression.Lambda<Func<short>>(expression).Compile()() },
            { typeof(short?), (expression) => Expression.Lambda<Func<short?>>(expression).Compile()() },
            { typeof(int), (expression) => Expression.Lambda<Func<int>>(expression).Compile()() },
            { typeof(int?), (expression) => Expression.Lambda<Func<int?>>(expression).Compile()() },
            { typeof(long), (expression) => Expression.Lambda<Func<long>>(expression).Compile()() },
            { typeof(long?), (expression) => Expression.Lambda<Func<long?>>(expression).Compile()() },
            { typeof(ushort), (expression) => Expression.Lambda<Func<ushort>>(expression).Compile()() },
            { typeof(ushort?), (expression) => Expression.Lambda<Func<ushort?>>(expression).Compile()() },
            { typeof(uint), (expression) => Expression.Lambda<Func<uint>>(expression).Compile()() },
            { typeof(uint?), (expression) => Expression.Lambda<Func<uint?>>(expression).Compile()() },
            { typeof(ulong), (expression) => Expression.Lambda<Func<ulong>>(expression).Compile()() },
            { typeof(ulong?), (expression) => Expression.Lambda<Func<ulong?>>(expression).Compile()() },
            { typeof(object), (expression) => Expression.Lambda<Func<object>>(expression).Compile()() },
            { typeof(Guid), (expression) => Expression.Lambda<Func<Guid>>(expression).Compile()() },
            { typeof(Guid?), (expression) => Expression.Lambda<Func<Guid?>>(expression).Compile()() },
        };
    }
}
