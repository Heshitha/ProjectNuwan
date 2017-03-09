using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetworkMarketing.Models
{
    public class UserVM
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TransctionKey { get; set; }
        public string Country { get; set; }
        public string District { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string SponserID { get; set; }
        public string ImageExt { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}