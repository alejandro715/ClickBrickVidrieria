using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
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

        public async Task <IActionResult> Index()
        {
            IEnumerable<Producto> productoLista = await _UnidadTrabajo.Producto.ObtenerTodos();
            return View(productoLista);
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