using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Hein.Framework.Serialization.Policies
{
    public class SnakeCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => ToSnakeCase(name);

        private string ToSnakeCase(string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            var pattern =
                new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");

            var result = string.Join("_", pattern.Matches(str).Cast<Match>().Select(m => m.Value)).ToLower();
            return str.StartsWith("_") 
                ? string.Concat("_", result) 
                : result;
        }
    }
}
