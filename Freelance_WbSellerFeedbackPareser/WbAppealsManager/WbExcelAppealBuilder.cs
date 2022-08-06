namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbExcelAppealBuilder
    {
        private readonly List<TotalAppealModel> _totalAppeals;
        public WbExcelAppealBuilder(List<TotalAppealModel> totalAppeals)
        {
            _totalAppeals = totalAppeals;
        }

        public List<ExcelAppealModel> CreateExcelAppeals(IRequestSender requestSender)
        {
            WbAppealReader wbAppealReader = new(requestSender);
            List<ExcelAppealModel> excelAppeals = new();
            foreach(var totalApeal in _totalAppeals)
            {
                string productId = wbAppealReader.GetProductId(totalApeal.Id);
                ExcelAppealModel excelAppeal = CreateExcelAppeal(totalApeal, productId);
                excelAppeals.Add(excelAppeal);
            }
            return excelAppeals;
        }

        private static ExcelAppealModel CreateExcelAppeal(TotalAppealModel totalAppeal, string productId)
        {
            ExcelAppealModel excelAppeal = new()
            {
                Answer = totalAppeal.Answer,
                AppealText = totalAppeal.AppealText,
                Id = totalAppeal.Id,
                ParentId = totalAppeal.ParentId,
                ProductId = productId,
                Status = totalAppeal.Status,
                TextCreateDate = totalAppeal.TextCreateDate,
                Title = totalAppeal.Title
            };
            return excelAppeal;
        }
    }
}
