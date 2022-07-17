using Freelance_WbSellerFeedbackPareser;
using Freelance_WbSellerFeedbackPareser.Models;

Configuration configuration = Configuration.GetInstance();
WbFeedbacksExcelBuilder excelBuilder = new WbFeedbacksExcelBuilder();
foreach (var seller in configuration.SellerSettings)
{
    WbFeedbackReader feedbackReader = new WbFeedbackReader(seller);
    var feedbacks = feedbackReader.ReadAllFeedbacks();
    excelBuilder.WriteFeedbacksToNewList(seller.SellerName, feedbacks);
}
excelBuilder.Save();