using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "Nombre de Categoria Requerido")]
        [MaxLength(60, ErrorMessage = "Nombre debe ser maximo 60 Caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Descripción de Categoria Requerido")]
        [MaxLength(100, ErrorMessage = "Descripción debe ser maximo 100 Caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Estado es Requerido")]
        public bool Estado { get; set; }
    }
}
