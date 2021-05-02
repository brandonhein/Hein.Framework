using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Hein.Framework.Dynamo.Helpers
{
    internal static class ObjectExtensions
    {
        public static ExpandoObject ToObject(this IDictionary<string, object> source)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<string, object>>)expandoObj;

            foreach (var keyValuePair in source)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }

        public static IDictionary<string, object> AsDictionary(this object source, BindingFlags bindingAttr = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance)
        {
            return source.GetType().GetProperties(bindingAttr).ToDictionary
            (
                propInfo => propInfo.Name,
                propInfo => propInfo.GetValue(source, null)
            );
        }

        public static IList CastToList(this IEnumerable source, Type itemType)
        {
            var listType = typeof(List<>).MakeGenericType(itemType);
            var list = (IList)Activator.CreateInstance(listType);
            foreach (var item in source) list.Add(item);
            return list;
        }

        public static Array CastToArray(this IEnumerable source, Type elementType)
        {
            var list = CastToList(source, elementType);
            var array = Array.CreateInstance(elementType, list.Count);
            list.CopyTo(array, 0);

            return array;
        }

        public static bool IsOneOf(this Type type, params Type[] types)
            => type != null && types != null && types.Any() && types.Contains(type);
    }
}
