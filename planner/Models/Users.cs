using Microsoft.AspNetCore.Identity;

namespace planner.Models
{
    public class Users
    {
        public Users()
        {
            Id = 0;
            UserName = "";
            Password = "";
            Mobile = "";
            RegisterDate = DateTime.Now;
            LoginDate = DateTime.Now;
            Address = "";
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public DateTime RegisterDate { get; set; }
         public DateTime LoginDate { get; set; }
        public string Address { get; set; }
    }
}
