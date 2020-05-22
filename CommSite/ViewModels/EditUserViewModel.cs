﻿using System;

namespace PhotoBank.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDay { get; set; }
        public bool Check { get; set; }
    }
}