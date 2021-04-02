using System.Text.Json;

namespace Hein.Framework.Serialization.Policies
{
    public class UpperCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToUpper();
    }
}
