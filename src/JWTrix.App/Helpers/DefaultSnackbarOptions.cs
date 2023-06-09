using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTrix.App.Helpers
{
    public static class DefaultSnackbarOptions
    {
        public static Action<SnackbarOptions> HideIconAndCloseAfterNavigation { get; } = option =>
        {
            option.CloseAfterNavigation = true;
            option.HideIcon = true;
        };
    }
}
