using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealsManager
    {
        private readonly IRequestSender _requestSender;

        public WbAppealsManager(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }

        public string GetAppealsContent(int skip, int take)
        {
            string url = $"https://seller.wildberries.ru/ns/suppliers-proxy/callcenter/suppliers-appeals-api/v1/suppliers/appeals?destination=desc&limit={take}&offset={skip}&sort=createDate";
            string content = _requestSender.SendRequestAsync(HttpMethod.Get, url).Result;
            return content;
        }
    }
}
