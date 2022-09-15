
using System.ComponentModel.DataAnnotations;
namespace ALPHA.Models
{
    public class PropietarioModel
    {
        public int Idpropietario { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public int Idpersona { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? Identificacion { get; set; }
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? Apellido { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? anacimiento { get; set; }
        public string? Ciudad { get; set; }
        [Required(ErrorMessage = "EL campo es obligatorio")]//obligatoria
        public string? Email { get; set; }


    }

}
