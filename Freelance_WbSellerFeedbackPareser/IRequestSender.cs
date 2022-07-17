﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal interface IRequestSender
    {
        public Task<string> SendRequestAsync(HttpMethod method, string url);
    }
}