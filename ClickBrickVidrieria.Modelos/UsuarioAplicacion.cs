using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.Modelos
{
    public class UsuarioAplicacion : IdentityUser
    {
        [Required(ErrorMessage ="Nombres es Requerido")]
        [MaxLength(80)]
        public String Nombres { get; set; }


        [Required(ErrorMessage = "Apellidos es Requerido")]
        [MaxLength(80)]
        public String Apellidos { get; set; }


        [Required(ErrorMessage = "Dirrecion es Requerido")]
        [MaxLength(200)]
        public String Dirrecion { get; set; }


        [Required(ErrorMessage = "Ciudad es Requerido")]
        [MaxLength(60)]
        public String Ciudad { get; set; }

        [Required(ErrorMessage = "País es Requerido")]
        [MaxLength(60)]
        public String pais { get; set; }

        [NotMapped]
        public string Role { get; set; }

    }
}
