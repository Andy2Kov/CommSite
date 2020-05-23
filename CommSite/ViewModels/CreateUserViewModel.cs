
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoBank.ViewModels
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Display(Name = "День рождения")]
        public DateTime BirthDay { get; set; }
    }
}
