using Microsoft.VisualBasic;

namespace planner.Models
{
    public class Tasks
    {
        public Tasks()
        {
            TaskId = 0;
            MonthId = 0;
            YearId = 0;
            TaskName = "";
            IsDone = 0;
        }

        public int TaskId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; private set; }
        public string TaskName { get; set; }
        public int IsDone { get; set; }
    }
}
