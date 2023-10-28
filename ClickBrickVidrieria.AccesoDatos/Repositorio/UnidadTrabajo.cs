using ClickBrickVidrieria.AccesoDatos.Data;
using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio
{
    public class UnidadTrabajo : iUnidadTrabajo
    {

        private readonly ApplicationDbContext _db;
        public iBodegaRepositorio Bodega { get; private set; }
        public iCategoriaRepositorio Categoria { get; private set; }
        public iMarcaRepositorio Marca { get; private set; }
        public iProductoRepositorio Producto { get; private set; }
        public iUsuarioAplicacionRepositorio UsuarioAplicacion { get; private set; }

        public iProductoBodegaRepositorio ProductoBodega { get; private set; }

        public iInventarioRepositorio Inventario { get; private set; }

        public iInventarioDetalleRepositorio InventarioDetalle { get; private set; }

        public iKardexInventarioRepositorio KardexInventario { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
            Categoria = new CategoriaRepositorio(_db);
            Marca = new MarcaRepositorio(_db);
            Producto = new ProductoRepositorio(_db);
            UsuarioAplicacion = new UsuarioAplicacionRepositorio(_db);
            ProductoBodega = new ProductoBodegaRepositorio(_db);
            Inventario = new InventarioRepositorio(_db);
            InventarioDetalle = new InventarioDetalleRepositorio(_db);
            KardexInventario = new KardexInventarioRepositorio(_db);

        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
