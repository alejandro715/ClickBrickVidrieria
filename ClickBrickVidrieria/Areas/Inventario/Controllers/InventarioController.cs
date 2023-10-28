using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using ClickBrickVidrieria.Modelos.ViewModels;
using ClickBrickVidrieria.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClickBrickVidrieria.Areas.Inventario.Controllers
{
    [Area("Inventario")]
    [Authorize(Roles = DS.Role_Admin + "," + DS.Role_Inventario)]
    public class InventarioController : Controller
    {
        private readonly iUnidadTrabajo _unidadTrabajo;

        [BindProperty]
        public InventarioVM inventarioVM { get; set; }

        public InventarioController(iUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }



        public IActionResult Index()
        {
            return View();
        }


        public IActionResult NuevoInventario()
        {
            inventarioVM = new InventarioVM()
            {
                Inventario = new Modelos.Inventario(),
                BodegaLista = _unidadTrabajo.Inventario.ObtenerTodosDropDownLista("Bodega")
            };
            inventarioVM.Inventario.Estado = false;
            //obtener el id del usuario de la sesion

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            inventarioVM.Inventario.UsuarioAplicacionId = claim.Value;
            inventarioVM.Inventario.FechaInicial = DateTime.Now;
            inventarioVM.Inventario.FechaFinal = DateTime.Now;

            return View(inventarioVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NuevoInventario(InventarioVM inventario)
        {
            if (ModelState.IsValid)
            {
                inventarioVM.Inventario.FechaInicial = DateTime.Now;
                inventarioVM.Inventario.FechaFinal = DateTime.Now;
                await _unidadTrabajo.Inventario.Agregar(inventarioVM.Inventario);
                await _unidadTrabajo.Guardar();
                return RedirectToAction("DetalleInventario", new { id = inventarioVM.Inventario.Id });

            }
            inventarioVM.BodegaLista = _unidadTrabajo.Inventario.ObtenerTodosDropDownLista("Bodega");
            return View(inventarioVM);
        }

        public async Task <IActionResult> DetalleInventario(int id)
        {
            inventarioVM = new InventarioVM();
            inventarioVM.Inventario = await _unidadTrabajo.Inventario.ObtenerPrimero(i => i.Id == id, incluirPropiedades: "Bodega");
            inventarioVM.InventarioDetalles = await _unidadTrabajo.InventarioDetalle.ObtenerTodos(d => d.InventarioId == id,
                                                                                                  incluirPropiedades: "Producto,Producto.Marca");

            return View(inventarioVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DetalleInventario(int inventarioId, int productoId, int cantidadId)
        {
            inventarioVM = new InventarioVM();
            inventarioVM.Inventario = await _unidadTrabajo.Inventario.ObtenerPrimero(i => i.Id == inventarioId);
            var bodegaProducto = await _unidadTrabajo.ProductoBodega.ObtenerPrimero(b => b.ProductoId == productoId && b.BodegaId == inventarioVM.Inventario.BodegaId);


            var detalle = await _unidadTrabajo.InventarioDetalle.ObtenerPrimero(d => d.InventarioId == inventarioId && d.ProductoId == productoId);
            if (detalle == null)
            {
                inventarioVM.InventarioDetalle = new InventarioDetalle();
                inventarioVM.InventarioDetalle.ProductoId = productoId;
                inventarioVM.InventarioDetalle.InventarioId = inventarioId;

                if (bodegaProducto !=null)
                {
                    inventarioVM.InventarioDetalle.StockAnterior = bodegaProducto.Cantidad;
                }
                else
                {
                    inventarioVM.InventarioDetalle.StockAnterior = 0;
                }
                inventarioVM.InventarioDetalle.Cantidad = cantidadId;
                await _unidadTrabajo.InventarioDetalle.Agregar(inventarioVM.InventarioDetalle);
                await _unidadTrabajo.Guardar();
            }
            else
            {
                detalle.Cantidad += cantidadId;
                await _unidadTrabajo.Guardar();
            }

            return RedirectToAction("DetalleInventario", new { id = inventarioId });
        }


        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.ProductoBodega.ObtenerTodos(incluirPropiedades: "Bodega,Producto");
            return Json(new { data = todos });

        }

        [HttpGet]
        public async Task<IActionResult> BuscarProducto(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var listaProductos = await _unidadTrabajo.Producto.ObtenerTodos(p => p.Estado == true);
                var data = listaProductos.Where(x => x.NumeroSerie.Contains(term,StringComparison.OrdinalIgnoreCase) ||
                                                x.Descripcion.Contains(term,StringComparison.OrdinalIgnoreCase)).ToList();

                return Ok(data);
            }
            return Ok();
        }
        






        #endregion


    }
}
