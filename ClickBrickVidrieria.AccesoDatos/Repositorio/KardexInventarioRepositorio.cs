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
    public class KardexInventarioRepositorio : Repositorio<KardexInventario>, iKardexInventarioRepositorio
    {

        private readonly ApplicationDbContext _db;

        public KardexInventarioRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

      

        
    }
}
