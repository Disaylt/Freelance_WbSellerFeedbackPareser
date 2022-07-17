using Freelance_WbSellerFeedbackPareser.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_WbSellerFeedbackPareser
{
    internal class FeedbackDataTable : DataTable
    {
        public FeedbackDataTable(List<FeedbackModel> feedbacks)
        {
            AddColumns();
            FillFeedbacks(feedbacks);
        }

        private void FillFeedbacks(List<FeedbackModel> feedbacks)
        {
            foreach (FeedbackModel feedback in feedbacks)
            {
                object[] cells =
                {
                    feedback.Id,
                    feedback.Status,
                    DateTime.Parse(feedback.TextCreateDate).Date,
                    feedback.Title,
                    feedback.ParentId?.ToString() ?? string.Empty,
                    feedback.AppealText,
                    feedback.Answer
                };
                Rows.Add(cells);
            }
        }

        private void AddColumns()
        {
            Columns.Add("Id обращения", typeof(int));
            Columns.Add("Статус", typeof(string));
            Columns.Add("Дата создания", typeof(DateTime));
            Columns.Add("Тема", typeof(string));
            Columns.Add("Связанное обращение Id", typeof(string));
            Columns.Add("Описание", typeof(string));
            Columns.Add("Ответ поддержки", typeof(string));
        }
    }
}
