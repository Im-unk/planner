using Microsoft.VisualBasic;

namespace planner.Models
{
    public class Expense
    {
        public Expense()
        {
            ExpenseId = 0;
            ExpenseTitle = "";
            ExpensePrice = "";
            MonthId = 0;
            YearId = 0;
            DayId = 0;
            Id = 0;
        }

        public int ExpenseId { get; set; }
        public string ExpenseTitle { get; set; }
        public string ExpensePrice { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int DayId { get; set; }
        public int Id { get; set; }
    }
}
