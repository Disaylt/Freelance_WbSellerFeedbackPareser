using ClosedXML.Excel;

namespace Freelance_WbSellerFeedbackPareser.WbAppealsManager
{
    internal class WbAppealsExcelCreator
    {
        private const string _name = "feedbacks.xlsx";
        private readonly XLWorkbook _workbook;

        public WbAppealsExcelCreator()
        {
            _workbook = new XLWorkbook();
            SetWorkbookSettings();
            Console.WriteLine("Создал пустой Excel");
        }

        public void WriteFeedbacksToNewList(string listName, List<ExcelAppealModel> feedbacks)
        {
            AppealsDataTable feedbackDataTable = new(feedbacks);
            string verifiedListName = CreateUniqueListName(listName);
            IXLWorksheet worksheet =  _workbook.AddWorksheet(feedbackDataTable, verifiedListName);
            SetDefaultWorksheetWidht(worksheet);
            Console.WriteLine($"Создан лист {verifiedListName} с сообщениями");
        }

        public void Save()
        {
            _workbook.SaveAs($@"files\{_name}");
            Console.WriteLine("Excel сохранен");
        }

        private string CreateUniqueListName(string listName)
        {
            if(!CheckExistsWorksheetName(listName))
            {
                return listName;
            }

            string uniqueName = listName;
            for (int numName = 1; numName < int.MaxValue; numName++)
            {
                if (!CheckExistsWorksheetName($"{uniqueName}{numName}"))
                {
                    return listName;
                }
            }
            throw new Exception("Error create worksheet name.");
        }

        private bool CheckExistsWorksheetName(string name)
        {
            if (_workbook.Worksheets.Any(x => x.Name == name))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void SetDefaultWorksheetWidht(IXLWorksheet worksheet)
        {
            worksheet.Column(1).Width = 15f;
            worksheet.Column(2).Width = 15f;
            worksheet.Column(3).Width = 10f;
            worksheet.Column(4).Width = 15f;
            worksheet.Column(5).Width = 30f;
            worksheet.Column(6).Width = 15f;
            worksheet.Column(7).Width = 50f;
            worksheet.Column(8).Width = 50f;
        }

        private void SetWorkbookSettings()
        {
            _workbook.Style.Alignment.WrapText = true;
        }
    }
}
