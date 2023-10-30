using Microsoft.JSInterop;

namespace BlazorEcommerce_Server.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "Success", message);
        }
        public static async ValueTask ToastrError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ShowToastr", "Error", message);
        }
    }
}
