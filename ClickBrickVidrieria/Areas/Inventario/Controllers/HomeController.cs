using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using ClickBrickVidrieria.Modelos.Especificaciones;
using ClickBrickVidrieria.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClickBrickVidrieria.Areas.Inventario.Controllers
{

    [Area("Inventario")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly iUnidadTrabajo _UnidadTrabajo;

        public HomeController(ILogger<HomeController> logger, iUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _UnidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int pageNumber =1, string busqueda="", string busquedaActual="")
        {
            if(!string.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else 
            {
                busqueda = busquedaActual;
            }
            ViewData["BusquedaActual"] = busqueda;


            if(pageNumber < 1) { pageNumber = 1; }

            Parametros parametros = new Parametros()
            {

                PageNumber = pageNumber,
                PageSize = 4

            };

            var resultado = _UnidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            if(!string.IsNullOrEmpty(busqueda))
            {
                resultado = _UnidadTrabajo.Producto.ObtenerTodosPaginado(parametros, p => p.Descripcion.Contains(busqueda)); 
            }

            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSize"] = resultado.MetaData.PagesSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disabled";
            ViewData["Siguiente"] = "";

            if(pageNumber > 1) { ViewData["Previo"] = ""; }
            if(resultado.MetaData.TotalPages <= pageNumber) { ViewData["Siguiente"] = "disabled"; }

            return View(resultado);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}