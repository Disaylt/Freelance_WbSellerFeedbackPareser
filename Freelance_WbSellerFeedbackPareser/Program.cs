using Freelance_WbSellerFeedbackPareser;
using Freelance_WbSellerFeedbackPareser.Models;

Configuration configuration = Configuration.GetInstance();
foreach(var seller in configuration.SellerSettings)
{
    WbFeedbackReader feedbackReader = new WbFeedbackReader(seller);
    var feedbacks = feedbackReader.ReadAllFeedbacks();
    FeedbackDataTable feedbackDataTable = new FeedbackDataTable(feedbacks);
    Console.ReadLine();
}