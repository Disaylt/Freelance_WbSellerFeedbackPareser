using Freelance_WbSellerFeedbackPareser.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbAppealsReader
    {
        private readonly IRequestSender _requestSender;
        private const int _take = 100;

        public WbAppealsReader(SellerSettingsModel sellerSettings)
        {
            _requestSender = new WbSellerHttpSender(sellerSettings);
        }

        public List<AppealModel> ReadAllFeedbacks()
        {
            List<AppealModel> allFeedbacks = new List<AppealModel>();
            string firstAppealsContent = GetAppealsContent(0);
            int totalFeedback = GetTotalFeedback(firstAppealsContent);
            List<AppealModel> firstFeedbacks = ConvertToFeedbackList(firstAppealsContent);
            allFeedbacks.AddRange(firstFeedbacks);

            int skip = _take;
            while(totalFeedback > skip)
            {
                string appealsContent = GetAppealsContent(skip);
                List<AppealModel> feedbacks = ConvertToFeedbackList(appealsContent);
                allFeedbacks.AddRange(feedbacks);
                skip += _take;
            }
            Console.WriteLine($"Получил {totalFeedback} сообщений");
            return allFeedbacks;
        }

        private string GetAppealsContent(int skip)
        {
            string url = $"https://seller.wildberries.ru/ns/suppliers-proxy/callcenter/suppliers-appeals-api/v1/suppliers/appeals?destination=desc&limit={_take}&offset={skip}&sort=createDate";
            string content = _requestSender.SendRequestAsync(HttpMethod.Get, url).Result;
            return content;
        }

        private List<AppealModel> ConvertToFeedbackList(string content)
        {
            List<AppealModel> feedbacks = JToken.Parse(content)["data"]?["rows"]?
                .ToObject<List<AppealModel>>() ?? new List<AppealModel>();
            return feedbacks;
        }

        private int GetTotalFeedback(string content)
        {
            int? total = JToken.Parse(content)?["data"]?
                .Value<int?>("totalCount");
            if(total.HasValue)
            {
                return total.Value;
            }
            else
            {
                return 0;
            }
        }
    }
}
