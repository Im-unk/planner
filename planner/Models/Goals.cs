using Microsoft.Build.Framework;

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
            DayId = 0;
            Id = 0;
        }

        public int GoalId { get; set; }
        [Required]
        public string GoalName { get; set; }
        public int YearId { get; set; }
        public int MonthId { get; set; }
        public int DayId { get; set; }
        public int Id { get; set; }
    }
}
