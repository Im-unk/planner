namespace planner.Models
{
    public class Month
    {
        public Month()
        {
            MonthId = 0;
            MonthName = "";
            MonthNumber = "";
        }

        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public string MonthNumber { get; set; }
    }
}
