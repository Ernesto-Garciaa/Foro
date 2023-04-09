using System.ComponentModel.DataAnnotations;

namespace Foro.Models
{
    public class UsuarioModel
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El campo no debe quedar vacio")]
        public int idTipoUsuario { get; set; }

        [Required(ErrorMessage = "El campo no debe quedar vacio")]
        public string? nombre { get; set; }

        [Required(ErrorMessage = "El campo no debe quedar vacio")]
        public string? email { get; set; }


        [Required(ErrorMessage = "El campo no debe quedar vacio")]
        public string? contrasenia { get; set; }
    }
}
