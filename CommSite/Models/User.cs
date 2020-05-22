using Microsoft.AspNetCore.Identity;
using System;

namespace PhotoBank.Models
{
    public class User : IdentityUser
    {
        //public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
