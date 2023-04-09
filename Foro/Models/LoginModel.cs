using System.ComponentModel.DataAnnotations;

namespace Foro.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El campo no debe quedar vacio")]
        public string? nombre { get; set; }


        [Required(ErrorMessage = "El campo no debe quedar vacio")]
        public string? contrasenia { get; set; }
    }
}
