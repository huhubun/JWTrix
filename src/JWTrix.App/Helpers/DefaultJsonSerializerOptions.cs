using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JWTrix.App.Helpers
{
    internal static class DefaultJsonSerializerOptions
    {
        public static JsonSerializerOptions WriteIndented { get; } = new()
        {
            WriteIndented = true
        };
    }
}
