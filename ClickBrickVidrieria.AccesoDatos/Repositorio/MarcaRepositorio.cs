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
    public class MarcaRepositorio : Repositorio<Marca>, iMarcaRepositorio
    {

        private readonly ApplicationDbContext _db;

        public MarcaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Actualizar(Marca marca)
        {
            var MarcaBD = _db.Marcas.FirstOrDefault(b => b.Id == marca.Id);

            if(MarcaBD != null)
            {

                MarcaBD.Nombre = marca.Nombre;
                MarcaBD.Descripcion = marca.Descripcion;
                MarcaBD.Estado = marca.Estado;
                _db.SaveChanges();

            }
        }
    }
}
