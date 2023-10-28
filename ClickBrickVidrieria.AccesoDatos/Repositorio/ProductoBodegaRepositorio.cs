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
    public class ProductoBodegaRepositorio : Repositorio<BodegaProducto>, iProductoBodegaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public ProductoBodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(BodegaProducto BodegaProducto)
        {
            var ProductoBodegaBD = _db.BodegasProductos.FirstOrDefault(b => b.Id == BodegaProducto.Id);

            if(ProductoBodegaBD != null)
            {
                ProductoBodegaBD.Cantidad = BodegaProducto.Cantidad;

                _db.SaveChanges();

            }
        }

        
    }
}
