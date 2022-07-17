using Freelance_WbSellerFeedbackPareser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbSellerHttpSender : WbSellerHttpClient, IRequestSender
    {
        public WbSellerHttpSender(SellerSettingsModel sellerSettings) : base(sellerSettings)
        {

        }

        public async Task<string> SendRequestAsync(HttpMethod method, string url)
        {
            var httpClient = new HttpClient(HttpClientHandler);
            using(var request = new HttpRequestMessage(method, url))
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

                var response = await httpClient.SendAsync(request);
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }
}
