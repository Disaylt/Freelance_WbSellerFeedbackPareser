using Freelance_WbSellerFeedbackPareser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbSellerHttpClient
    {
        public HttpClientHandler HttpClientHandler { get; }
        public WbSellerHttpClient(SellerSettingsModel sellerSettingsModel)
        {
            HttpClientHandler = new HttpClientHandler();
            HttpClientHandler.CookieContainer = CreateWbCookieContainer(sellerSettingsModel);
        }

        private CookieContainer CreateWbCookieContainer(SellerSettingsModel sellerSettingsModel)
        {
            CookieCollection cookieCollection = new CookieCollection
            {
                new Cookie("WBToken", sellerSettingsModel.WbToken, "/", "seller.wildberries.ru"),
                new Cookie("x-supplier-id", sellerSettingsModel.SupplierId, "/", "seller.wildberries.ru")
            };
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(cookieCollection);
            return cookieContainer;
        }
    }
}
