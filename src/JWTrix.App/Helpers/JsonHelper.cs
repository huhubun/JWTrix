using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWTrix.App.Helpers
{
    public static class JsonHelper
    {
        public static string Beautify(string json)
        {
            var formatJson = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(formatJson.RootElement, DefaultJsonSerializerOptions.WriteIndented);
        }
    }
}
