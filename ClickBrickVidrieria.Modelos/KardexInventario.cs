﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.Modelos
{
    public class KardexInventario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BodegaProductoId { get; set; }

        [ForeignKey("BodegaProductoId")]
        public BodegaProducto BodegaProducto { get; set; }

        [Required]
        [MaxLength(100)]
        public string Tipo { get; set; }

        public string Detalle { get; set; }

        [Required]
        public int StockAnterior { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public double Costo { get; set; }

        [Required]
        public int Stock { get; set; }

        public double Total { get; set; }

        [Required]
        public String UsuarioAplicacionId { get; set; }
        
        [ForeignKey("UsuarioAplicacionId")]
        public UsuarioAplicacion UsuarioAplicacion { get; set; }
        public DateTime FechaRegistro { get; set; }

    }
}
