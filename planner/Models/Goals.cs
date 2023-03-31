namespace planner.Models
{
    public class Goals
    {
        public Goals()
        {
            GoalId = 0;
            GoalName = "";
            YearId = 0;
            MonthId = 0;
        }

        public int GoalId { get; set; }
        public string GoalName { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
    }
}
