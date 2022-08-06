try
{
    Configuration configuration = Configuration.GetInstance();
    WbAppealsExcelCreator excelBuilder = new WbAppealsExcelCreator();
    foreach (var seller in configuration.SellerSettings)
    {
        try
        {
            IRequestSender requestSender = new WbSellerHttpSender(seller);
            WbAppealsReader appealsReader = new WbAppealsReader(requestSender);
            var appeals = appealsReader.ReadAllFeedbacks();
            WbExcelAppealBuilder wbExcelAppealBuilder = new WbExcelAppealBuilder(appeals);
            var excelAppeals = wbExcelAppealBuilder.CreateExcelAppeals(requestSender);
            excelBuilder.WriteFeedbacksToNewList(seller.SellerName, excelAppeals);
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