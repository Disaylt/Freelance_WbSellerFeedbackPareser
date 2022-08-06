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
            List<TotalAppealModel> allFeedbacks = new();
            string firstAppealsContent = _wbAppealsManager.GetAppealsContent(0, _take);
            int totalFeedback = WbAppealsJsonConverter.ReadTotalFeedback(firstAppealsContent);
            List<TotalAppealModel> firstFeedbacks = WbAppealsJsonConverter.ConvertToFeedbackList(firstAppealsContent);
            allFeedbacks.AddRange(firstFeedbacks);

            int skip = _take;
            while(totalFeedback > skip)
            {
                string appealsContent = _wbAppealsManager.GetAppealsContent(skip, _take);
                List<TotalAppealModel> feedbacks = WbAppealsJsonConverter.ConvertToFeedbackList(appealsContent);
                allFeedbacks.AddRange(feedbacks);
                skip += _take;
            }
            Console.WriteLine($"Получил {totalFeedback} сообщений");
            return allFeedbacks;
        }
    }
}
