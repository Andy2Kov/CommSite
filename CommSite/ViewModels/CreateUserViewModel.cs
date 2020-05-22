
using System;

namespace PhotoBank.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
