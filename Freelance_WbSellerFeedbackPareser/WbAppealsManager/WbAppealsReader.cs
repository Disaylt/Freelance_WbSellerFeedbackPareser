using Newtonsoft.Json.Linq;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealsReader
    {
        private readonly WbAppealsManager _wbAppealsManager;
        private const int _take = 100;

        public WbAppealsReader(IRequestSender requestSender)
        {
            _wbAppealsManager = new WbAppealsManager(requestSender);
        }

        public List<TotalAppealModel> ReadAllFeedbacks()
        {
            List<TotalAppealModel> allFeedbacks = new List<TotalAppealModel>();
            string firstAppealsContent = _wbAppealsManager.GetAppealsContent(0, _take);
            int totalFeedback = ReadTotalFeedback(firstAppealsContent);
            List<TotalAppealModel> firstFeedbacks = ConvertToFeedbackList(firstAppealsContent);
            allFeedbacks.AddRange(firstFeedbacks);

            int skip = _take;
            while(totalFeedback > skip)
            {
                string appealsContent = _wbAppealsManager.GetAppealsContent(skip, _take);
                List<TotalAppealModel> feedbacks = ConvertToFeedbackList(appealsContent);
                allFeedbacks.AddRange(feedbacks);
                skip += _take;
            }
            Console.WriteLine($"Получил {totalFeedback} сообщений");
            return allFeedbacks;
        }

        private List<TotalAppealModel> ConvertToFeedbackList(string content)
        {
            List<TotalAppealModel> feedbacks = JToken.Parse(content)["data"]?["rows"]?
                .ToObject<List<TotalAppealModel>>() ?? new List<TotalAppealModel>();
            return feedbacks;
        }

        private int ReadTotalFeedback(string content)
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
