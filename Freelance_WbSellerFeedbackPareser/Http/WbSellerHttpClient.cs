using System.Net;

namespace Freelance_WbSellerFeedbackPareser.Http
{
    internal class WbSellerHttpClient
    {
        private const string _domain = "seller.wildberries.ru";
        
        public WbSellerHttpClient(SellerSettingsModel sellerSettings)
        {
            HttpClientHandler = new HttpClientHandler();
            HttpClientHandler.CookieContainer = CreateWbCookieContainer(sellerSettings);
        }

        public HttpClientHandler HttpClientHandler { get; }

        protected virtual void SetHeaders(HttpRequestMessage request)
        {
            request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:102.0) Gecko/20100101 Firefox/102.0");
            request.Headers.TryAddWithoutValidation("Accept", "*/*");
            request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate, br");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Dest", "empty");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Mode", "cors");
            request.Headers.TryAddWithoutValidation("Sec-Fetch-Site", "same-origin");
            request.Headers.TryAddWithoutValidation("Referer", "https://seller.wildberries.ru/service-desk-v2/requests/history");
            request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
        }

        private CookieContainer CreateWbCookieContainer(SellerSettingsModel sellerSettingsModel)
        {
            CookieCollection cookieCollection = new CookieCollection
            {
                new Cookie("WBToken", sellerSettingsModel.WbToken, "/", _domain),
                new Cookie("x-supplier-id", sellerSettingsModel.SupplierId, "/", _domain)
            };
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(cookieCollection);
            return cookieContainer;
        }
    }
}
