using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace AtelierBlazorApp.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _jsRuntime;

        public AuthService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> IsUserLoggedIn()
        {
            var token = await GetTokenAsync();
            return !string.IsNullOrEmpty(token);
        }
        

        public async Task<string> GetTokenAsync()
        {
            return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        }
    }
}
