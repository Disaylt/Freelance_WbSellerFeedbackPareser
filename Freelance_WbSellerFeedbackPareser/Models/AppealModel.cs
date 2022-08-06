using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Freelance_WbSellerFeedbackPareser.Models
{
    internal class AppealModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("parentComId")]
        public int? ParentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; } = "Неизвестно";

        [JsonProperty("createDate")]
        public string TextCreateDate { get; set; } = "-";

        [JsonProperty("themeName")]
        public string Title { get; set; } = "-";

        [JsonProperty("appealText")]
        public string AppealText { get; set; } = string.Empty;

        [JsonProperty("answer")]
        public string Answer { get; set; } = string.Empty;
    }
}
