using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Guideline.Application.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Login obrigatório")]
        [DisplayName("Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha obrigatória")]
        [DisplayName("Senha")]
        public string Pass { get; set; }
    }
}
