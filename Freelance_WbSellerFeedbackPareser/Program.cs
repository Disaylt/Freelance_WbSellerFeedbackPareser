using Freelance_WbSellerFeedbackPareser;
using Freelance_WbSellerFeedbackPareser.Models;

try
{
    Configuration configuration = Configuration.GetInstance();
    WbFeedbacksExcelBuilder excelBuilder = new WbFeedbacksExcelBuilder();
    foreach (var seller in configuration.SellerSettings)
    {
        try
        {
            WbFeedbackReader feedbackReader = new WbFeedbackReader(seller);
            var feedbacks = feedbackReader.ReadAllFeedbacks();
            excelBuilder.WriteFeedbacksToNewList(seller.SellerName, feedbacks);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка при работае с селлером - {seller.SellerName}");
            Console.WriteLine(ex.Message);
        }
    }
    excelBuilder.Save();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    Console.ReadLine();
}