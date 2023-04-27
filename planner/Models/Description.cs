using Microsoft.VisualBasic;

namespace planner.Models
{
    public class Description
    {
        public Description()
        {
            DescriptionId = 0;
            DescriptionName = "یادداشت";
            MonthId = 0;
            YearId = 0;
            DayId = 0;
            Id = 0;
        }

        public int DescriptionId { get;  set; }
        public string DescriptionName { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int DayId { get; set; }
        public int Id { get; set; }
    }
}
