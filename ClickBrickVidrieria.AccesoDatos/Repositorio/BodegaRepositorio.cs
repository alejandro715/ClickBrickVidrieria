using ClickBrickVidrieria.AccesoDatos.Data;
using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, iBodegaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Bodega bodega)
        {
            var bodegaBD = _db.Bodegas.FirstOrDefault(b => b.IdBodega == bodega.IdBodega);

            if(bodegaBD != null)
            {

                bodegaBD.Nombre = bodega.Nombre;
                bodegaBD.Descripcion = bodega.Descripcion;
                bodegaBD.Estado = bodega.Estado;
                _db.SaveChanges();

            }
        }
    }
}
