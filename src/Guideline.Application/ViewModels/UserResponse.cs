﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Guideline.Application.ViewModels
{
    public class UserResponse : IResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome obrigatório")]
        [MinLength(2)]
        [MaxLength(100)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login obrigatório")]
        [DisplayName("Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "E-mail obrigatório")]
        [EmailAddress]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Documento")]
        public string Document { get; set; }
    }
}
