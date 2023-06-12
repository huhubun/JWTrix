using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JWTrix.App.Helpers
{
    public static class NavigationHelper
    {
        public static ValueTask<object> BrowserGoBack(this IJSRuntime js)
        {
            return js.InvokeAsync<object>("history.back");
        }
    }
}
