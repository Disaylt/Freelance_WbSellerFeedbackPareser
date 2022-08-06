using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbAppealReader
    {
        private readonly IRequestSender _requestSender;
        public WbAppealReader(IRequestSender requestSender)
        {
            _requestSender = requestSender;
        }
    }
}
