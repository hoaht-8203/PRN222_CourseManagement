using Microsoft.AspNetCore.Components.WebAssembly.Http;
using System.Net;
using Microsoft.AspNetCore.Components;

namespace BlazorAppSecure.Sevices
{
    public class CutomHttpHandler : DelegatingHandler
    {
        private readonly NavigationManager _navigationManager;

        public CutomHttpHandler(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
            request.Headers.Add("X-Requested-With", ["XMLHttpRequest"]);
            
            var response = await base.SendAsync(request, cancellationToken);
            
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var publicPages = new List<string> { "/forgot-password", "/reset-password", "/confirm-email" };

                // Kiểm tra xem trang hiện tại có phải trang công khai không
                var currentUri = _navigationManager.Uri;
                bool isOnPublicPage = publicPages.Any(page => currentUri.Contains(page, StringComparison.OrdinalIgnoreCase));
                // Chỉ redirect nếu không phải đang ở trang login và register
                if (!isOnPublicPage && !currentUri.Contains("/login") && !currentUri.Contains("/register"))
                {
                    _navigationManager.NavigateTo("/login", true);
                }
            }

            return response;
        }
    }
}
