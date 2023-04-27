using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace planner.Models
{
    public class Tasks
    {
        public Tasks()
        {
            TaskId = 0;
            MonthId = 0;
            YearId = 0;
            DayId = 0;
            TaskName = "";
            IsDone = 0;
            Id = 0;
        }

        public int TaskId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get;  set; }
        public int DayId { get;  set; }

        [Required]
        public string TaskName { get; set; }
        public int IsDone { get; set; }
        public int Id { get; set; }
    }
}
