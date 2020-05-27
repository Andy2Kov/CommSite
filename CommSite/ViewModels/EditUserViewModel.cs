using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoBank.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        [Display(Name = "Фото профиля")]
        public IFormFile ProfileImage { get; set; }

        [Display(Name = "Фото профиля")]
        public byte[] ProfilePicture { get; set; }

        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Display(Name = "Пол")]
        public string Sex { get; set; }

        [Display(Name = "День рождения")]
        public DateTime BirthDay { get; set; }
        public bool Check { get; set; }
    }
}