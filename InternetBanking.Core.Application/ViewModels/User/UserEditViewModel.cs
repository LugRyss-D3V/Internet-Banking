
using System.ComponentModel.DataAnnotations;


namespace InternetBanking.Core.Application.ViewModels.User
{
    public class UserEditViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar un apellido")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un numero de cedula")]
        [DataType(DataType.Text)]
        public string? PersonalId { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        public List<string>? Roles { get; set; }
        public bool Status { get; set; } = false;
        public double? Amount { get; set; } = 0;
        public bool HasError { get; set; } = false;
        public string? Error { get; set; }
    }
}
