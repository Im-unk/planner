namespace planner.Models
{
    public class Month
    {
        public Month()
        {
            MonthId = 0;
            MonthName = "";
            MonthNumber = 0;
        }

        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public int MonthNumber { get; set; }
    }
}
