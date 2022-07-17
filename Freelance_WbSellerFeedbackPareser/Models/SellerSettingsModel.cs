using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser.Models
{
    internal class SellerSettingsModel
    {
        public string SellerName { get; set; } = "Unknown";
        public string? WbToken { get; set; }
        public string? SupplierId { get; set; }
    }
}
