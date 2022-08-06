using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealsJsonConverter
    {
        public static List<TotalAppealModel> ConvertToFeedbackList(string content)
        {
            List<TotalAppealModel> feedbacks = JToken.Parse(content)["data"]?["rows"]?
                .ToObject<List<TotalAppealModel>>() ?? new List<TotalAppealModel>();
            return feedbacks;
        }

        public static int ReadTotalFeedback(string content)
        {
            int? total = JToken.Parse(content)?["data"]?
                .Value<int?>("totalCount");
            if (total.HasValue)
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
