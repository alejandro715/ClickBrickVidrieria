using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using ClickBrickVidrieria.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickBrickVidrieria.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = DS.Role_Admin)]
    public class MarcaController : Controller
    {

        private readonly iUnidadTrabajo _unidadTrabajo;

        public MarcaController(iUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

     
        public async Task<IActionResult> Upsert(int? id)

        {

            Marca marca = new Marca();

            if(id == null)
            {
                //crea una nueva Marca
                marca.Estado = true;
                return View(marca);
            }
            //actualización de la marca
            marca = await _unidadTrabajo.Marca.Obtener(id.GetValueOrDefault());
            if(marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upsert(Marca marca)

        {
            if(ModelState.IsValid)

            {
                if(marca.Id == 0)
                {
                    await _unidadTrabajo.Marca.Agregar(marca);
                    TempData[DS.Exitosa] = "Marca creada con exito";
                }
                else
                {
                    _unidadTrabajo.Marca.Actualizar(marca);
                    TempData[DS.Exitosa] = "Marca actualizada con exito";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar la Marca";
            return View(marca);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {

            var todos = await _unidadTrabajo.Marca.ObtenerTodos();
            return Json(new { data = todos });

        }

        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {

            var marcaBd = await _unidadTrabajo.Marca.Obtener(id);
        if(marcaBd == null)

            {
                return Json(new { success = false, message = "Error al borrar Marca" });
            }
            _unidadTrabajo.Marca.Remover(marcaBd);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Marca borrada con Exito" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(String nombre, int id =0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Marca.ObtenerTodos();
            if(id== 0)
            {
                valor = lista.Any(b=>b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.Id != id);

            }
            if(valor)

            {
                return Json(new { data = true });
            }
            return Json(new { data = false });  
        }

        #endregion
    }
}
