using Newtonsoft.Json.Linq;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealReader
    {
        private readonly IRequestSender _requestSender;
        public WbAppealReader(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }

        public string GetProductId(int appealId)
        {
            string content = ReadAppeal(appealId);
            List<AppealAttributeModel> appealAttributes = ConvertToAttributes(content);
            AppealAttributeModel? productData = appealAttributes
                .FirstOrDefault(x => x.Name == "Номенклатура" || x.Name == "Артикул  товара");
            if(productData == null)
            {
                return string.Empty;
            }
            else
            {
                return productData.Value;
            }
        }

        private string ReadAppeal(int appealId)
        {
            string url = $"https://seller.wildberries.ru/ns/suppliers-proxy/callcenter/suppliers-appeals-api/v1/suppliers/users/appeals/{appealId}";
            var content = _requestSender.SendRequestAsync(HttpMethod.Get, url).Result;
            Thread.Sleep(400);
            return content;
        }

        private List<AppealAttributeModel> ConvertToAttributes(string content)
        {
            List<AppealAttributeModel> feedbacks = JToken.Parse(content)["data"]?[0]?["attr"]?
                .ToObject<List<AppealAttributeModel>>() ?? new List<AppealAttributeModel>();
            return feedbacks;
        }
    }
}
