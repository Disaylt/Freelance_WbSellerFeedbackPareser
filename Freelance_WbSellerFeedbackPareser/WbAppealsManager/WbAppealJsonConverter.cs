using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealJsonConverter
    {
        public static List<AppealAttributeModel> ConvertToAttributes(string content)
        {
            List<AppealAttributeModel> feedbacks = JToken.Parse(content)["data"]?[0]?["attr"]?
                .ToObject<List<AppealAttributeModel>>() ?? new List<AppealAttributeModel>();
            return feedbacks;
        }
    }
}
