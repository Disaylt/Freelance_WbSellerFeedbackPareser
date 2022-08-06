using Newtonsoft.Json.Linq;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbAppealsReader
    {
        private readonly IRequestSender _requestSender;
        private const int _take = 100;

        public WbAppealsReader(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }

        public List<TotalAppealModel> ReadAllFeedbacks()
        {
            List<TotalAppealModel> allFeedbacks = new List<TotalAppealModel>();
            string firstAppealsContent = GetAppealsContent(0);
            int totalFeedback = GetTotalFeedback(firstAppealsContent);
            List<TotalAppealModel> firstFeedbacks = ConvertToFeedbackList(firstAppealsContent);
            allFeedbacks.AddRange(firstFeedbacks);

            int skip = _take;
            while(totalFeedback > skip)
            {
                string appealsContent = GetAppealsContent(skip);
                List<TotalAppealModel> feedbacks = ConvertToFeedbackList(appealsContent);
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

        private List<TotalAppealModel> ConvertToFeedbackList(string content)
        {
            List<TotalAppealModel> feedbacks = JToken.Parse(content)["data"]?["rows"]?
                .ToObject<List<TotalAppealModel>>() ?? new List<TotalAppealModel>();
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
