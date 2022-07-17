using Freelance_WbSellerFeedbackPareser.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbFeedbackReader
    {
        private readonly IRequestSender _requestSender;
        private const int _take = 100;

        public WbFeedbackReader(SellerSettingsModel sellerSettings)
        {
            _requestSender = new WbSellerHttpSender(sellerSettings);
        }

        public List<FeedbackModel> ReadAllFeedbacks()
        {
            List<FeedbackModel> allFeedbacks = new List<FeedbackModel>();
            string firstAppealsContent = GetAppealsContent(0);
            int totalFeedback = GetTotalFeedback(firstAppealsContent);
            List<FeedbackModel> firstFeedbacks = ConvertToFeedbackList(firstAppealsContent);
            allFeedbacks.AddRange(firstFeedbacks);

            int skip = _take;
            while(totalFeedback > skip)
            {
                string appealsContent = GetAppealsContent(skip);
                List<FeedbackModel> feedbacks = ConvertToFeedbackList(appealsContent);
                allFeedbacks.AddRange(feedbacks);
                skip += _take;
            }
            return allFeedbacks;
        }

        private string GetAppealsContent(int skip)
        {
            string url = $"https://seller.wildberries.ru/ns/suppliers-proxy/callcenter/suppliers-appeals-api/v1/suppliers/appeals?destination=desc&limit=10&offset={skip}&sort=createDate";
            string content = _requestSender.SendRequestAsync(HttpMethod.Get, url).Result;
            return content;
        }

        private List<FeedbackModel> ConvertToFeedbackList(string content)
        {
            List<FeedbackModel> feedbacks = JToken.Parse(content)["data"]?["rows"]?
                .ToObject<List<FeedbackModel>>() ?? new List<FeedbackModel>();
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
