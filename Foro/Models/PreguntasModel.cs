using System.ComponentModel.DataAnnotations;

namespace Foro.Models
{
    public class PreguntasModel
    {
        public int IdUsuario { get; set; }
        public string Titulo { get; set; }
        public string Pregunta { get; set; }
        public DateTime Fecha { get; set; }
    }
}
