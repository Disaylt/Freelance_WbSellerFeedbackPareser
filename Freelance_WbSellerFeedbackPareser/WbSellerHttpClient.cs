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
        private const string _domain = "seller.wildberries.ru";
        
        public WbSellerHttpClient(SellerSettingsModel sellerSettings)
        {
            HttpClientHandler = new HttpClientHandler();
            HttpClientHandler.CookieContainer = CreateWbCookieContainer(sellerSettings);
        }

        public HttpClientHandler HttpClientHandler { get; }

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
