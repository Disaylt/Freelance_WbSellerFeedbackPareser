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
                DateTime createDate = DateTime.Parse(feedback.TextCreateDate).Date;
                object[] cells =
                {
                    feedback.Id,
                    feedback.Status,
                    createDate,
                    feedback.Title,
                    feedback.ParentId ?? 0,
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
            Columns.Add("Связанное обращение Id", typeof(int));
            Columns.Add("Описание", typeof(string));
            Columns.Add("Ответ поддержки", typeof(string));
        }
    }
}
