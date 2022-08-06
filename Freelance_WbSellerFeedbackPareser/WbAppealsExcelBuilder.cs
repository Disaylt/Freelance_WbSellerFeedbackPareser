using ClosedXML.Excel;
using Freelance_WbSellerFeedbackPareser.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class WbAppealsExcelBuilder
    {
        private const string _name = "feedbacks.xlsx";
        private readonly XLWorkbook _workbook;

        public WbAppealsExcelBuilder()
        {
            _workbook = new XLWorkbook();
            SetWorkbookSettings();
            Console.WriteLine("Создал пустой Excel");
        }

        public void WriteFeedbacksToNewList(string listName, List<AppealModel> feedbacks)
        {
            FeedbackDataTable feedbackDataTable = new FeedbackDataTable(feedbacks);
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
            string newListName = listName;
            if (_workbook.TryGetWorksheet(listName, out var xLWorksheet))
            {
                int numberName = CreateUniqueListName(listName, 1);
                newListName += numberName;
            }
            return newListName;
        }

        private int CreateUniqueListName(string listName, int number)
        {
            string currentListName = $"{listName}{number}";
            int newNumber = number;
            if (_workbook.TryGetWorksheet(currentListName, out var xLWorksheet))
            {
                newNumber += 1;
                newNumber = CreateUniqueListName(listName, newNumber);
            }
            return newNumber;
        }

        private void SetDefaultWorksheetWidht(IXLWorksheet worksheet)
        {
            worksheet.Column(1).Width = 15f;
            worksheet.Column(2).Width = 10f;
            worksheet.Column(3).Width = 15f;
            worksheet.Column(4).Width = 30f;
            worksheet.Column(5).Width = 15f;
            worksheet.Column(6).Width = 50f;
            worksheet.Column(7).Width = 50f;


        }

        private void SetWorkbookSettings()
        {
            _workbook.Style.Alignment.WrapText = true;
        }
    }
}
