using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio
{
    public interface iUnidadTrabajo :IDisposable
    {

        iBodegaRepositorio Bodega { get; }
        iCategoriaRepositorio Categoria { get; }
        iMarcaRepositorio Marca { get; }
        iProductoRepositorio Producto { get; }
        iUsuarioAplicacionRepositorio UsuarioAplicacion { get; }

        iProductoBodegaRepositorio ProductoBodega { get; }

        iInventarioRepositorio Inventario { get; }

        iInventarioDetalleRepositorio InventarioDetalle { get; }

        iKardexInventarioRepositorio KardexInventario { get; }
        Task Guardar();

    }
}
