using Freelance_WbSellerFeedbackPareser;
using Freelance_WbSellerFeedbackPareser.Models;

try
{
    Configuration configuration = Configuration.GetInstance();
    WbAppealsExcelBuilder excelBuilder = new WbAppealsExcelBuilder();
    foreach (var seller in configuration.SellerSettings)
    {
        try
        {
            IRequestSender requestSender = new WbSellerHttpSender(seller);
            WbAppealsReader feedbackReader = new WbAppealsReader(requestSender);
            var feedbacks = feedbackReader.ReadAllFeedbacks();
            excelBuilder.WriteFeedbacksToNewList(seller.SellerName, feedbacks);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Произошла ошибка при работае с селлером - {seller.SellerName}");
            Console.ResetColor();
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