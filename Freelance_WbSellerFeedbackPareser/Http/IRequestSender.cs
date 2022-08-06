namespace Freelance_WbSellerFeedbackPareser.Http
{
    internal interface IRequestSender
    {
        public Task<string> SendRequestAsync(HttpMethod method, string url);
    }
}
