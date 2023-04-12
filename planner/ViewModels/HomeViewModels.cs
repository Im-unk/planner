using planner.Models;

namespace planner.ViewModels
{
    public class HomeViewModels
    {
        public Tasks Tasks { get; set; }
        public List<Tasks> TasksList { get; set; }
        public Expense Expenses { get; set; }
        public List<Expense> ExpenseList { get; set; }
        public Goals Goals { get; set; }
        public List<Goals> GoalsList { get; set; }
        public Description Description { get; set; }
        public List<Description> DescriptionList { get; set; }
        public List<Month> Month { get; set; }
        public List<Year> Year { get; set; }
        public List<Day> Day { get; set; }
        public Users Users { get; set; }
    }
}
