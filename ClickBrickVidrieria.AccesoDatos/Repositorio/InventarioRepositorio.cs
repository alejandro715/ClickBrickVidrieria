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
    public class InventarioRepositorio : Repositorio<Inventario>, iInventarioRepositorio
    {

        private readonly ApplicationDbContext _db;

        public InventarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Inventario inventario)
        {
            var InvetarioBD = _db.Inventarios.FirstOrDefault(b => b.Id == inventario.Id);

            if(InvetarioBD != null)
            {
                InvetarioBD.BodegaId = inventario.BodegaId;
                InvetarioBD.FechaFinal = inventario.FechaFinal;
                InvetarioBD.Estado = inventario.Estado;


                _db.SaveChanges();

            }
        }

        public IEnumerable<SelectListItem> ObtenerTodosDropDownLista(string obj)
        {
        
            if (obj == "Bodega")
            {
                return _db.Bodegas.Where(b => b.Estado == true).Select(b => new SelectListItem
                {
                    Text = b.Nombre,
                    Value = b.IdBodega.ToString()
                });
            }
            return null;
        }
    }
}
