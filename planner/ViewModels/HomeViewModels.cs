using planner.Models;

namespace planner.ViewModels
{
    public class HomeViewModels
    {
        public Tasks Tasks { get; set; }
        public Expense Expense { get; set; }
        public Goals Goals { get; set; }
        public Description Description { get; set; }
        public Month Month { get; set; }
        public Year Year { get; set; }
        public Users Users { get; set; }
    }
}
