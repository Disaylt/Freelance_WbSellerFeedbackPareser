namespace Freelance_WbSellerFeedbackPareser.Http
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
                SetHeaders(request);
                var response = await httpClient.SendAsync(request);
                Console.WriteLine($"Отправлен запрос:\r\n {url}");
                string responseContent = await response.Content.ReadAsStringAsync();
                return responseContent;
            }
        }
    }
}
