using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public DateTime RegisterDate { get; set; }
        [Required]
        public DateTime LoginDate { get; set; }
        public string Address { get; set; }
    }
}
