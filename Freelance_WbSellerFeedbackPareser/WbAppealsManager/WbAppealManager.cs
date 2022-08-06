using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealManager
    {
        private readonly IRequestSender _requestSender;

        public WbAppealManager(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }

        public string ReadAppeal(int appealId)
        {
            string url = $"https://seller.wildberries.ru/ns/suppliers-proxy/callcenter/suppliers-appeals-api/v1/suppliers/users/appeals/{appealId}";
            var content = _requestSender.SendRequestAsync(HttpMethod.Get, url).Result;
            return content;
        }
    }
}
