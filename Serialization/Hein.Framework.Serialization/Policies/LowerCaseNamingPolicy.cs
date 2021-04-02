using System.Text.Json;

namespace Hein.Framework.Serialization.Policies
{
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name) => name.ToLower();
    }
}
