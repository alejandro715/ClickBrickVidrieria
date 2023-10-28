using ClickBrickVidrieria.AccesoDatos.Data;
using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio
{
    public class InventarioDetalleRepositorio : Repositorio<InventarioDetalle>, iInventarioDetalleRepositorio
    {

        private readonly ApplicationDbContext _db;

        public InventarioDetalleRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(InventarioDetalle inventarioDetalle)
        {
            var InvetarioDetalleBD = _db.InventarioDetalles.FirstOrDefault(b => b.Id == inventarioDetalle.Id);

            if(InvetarioDetalleBD != null)
            {
                InvetarioDetalleBD.StockAnterior = inventarioDetalle.StockAnterior;
                InvetarioDetalleBD.Cantidad = inventarioDetalle.Cantidad;


                _db.SaveChanges();

            }
        }

        
    }
}
