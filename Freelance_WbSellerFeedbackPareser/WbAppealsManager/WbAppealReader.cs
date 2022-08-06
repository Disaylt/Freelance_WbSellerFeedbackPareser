using Newtonsoft.Json.Linq;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealReader
    {
        private readonly WbAppealManager _wbAppealManager;
        public WbAppealReader(IRequestSender requestSender)
        {
            _wbAppealManager =new WbAppealManager(requestSender);
        }

        public string GetProductId(int appealId)
        {
            string content = _wbAppealManager.ReadAppeal(appealId);
            List<AppealAttributeModel> appealAttributes = WbAppealJsonConverter.ConvertToAttributes(content);
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
    }
}
