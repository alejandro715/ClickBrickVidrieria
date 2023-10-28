using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using ClickBrickVidrieria.Utilidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickBrickVidrieria.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =DS.Role_Admin)]

    public class BodegaController : Controller
    {

        private readonly iUnidadTrabajo _unidadTrabajo;

        public BodegaController(iUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Route("/Admin/Bodega/Upsert/{idBodega?}")]
        public async Task<IActionResult> Upsert(int? id)

        {

            Bodega bodega = new Bodega();

            if(id == null)
            {
                //crea una nueva bodega
                bodega.Estado = true;
                return View(bodega);
            }
            //actualización de la bodega
            bodega = await _unidadTrabajo.Bodega.Obtener(id.GetValueOrDefault());
            if(bodega == null)
            {
                return NotFound();
            }
            return View(bodega);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upsert(Bodega bodega)

        {
            if(ModelState.IsValid)

            {
                if(bodega.IdBodega == 0)
                {
                    await _unidadTrabajo.Bodega.Agregar(bodega);
                    TempData[DS.Exitosa] = "Bodega creada con exito";
                }
                else
                {
                    _unidadTrabajo.Bodega.Actualizar(bodega);
                    TempData[DS.Exitosa] = "Bodega actualizada con exito";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar la bodega";
            return View(bodega);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {

            var todos = await _unidadTrabajo.Bodega.ObtenerTodos();
            return Json(new { data = todos });

        }

        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {

            var bodegaDb = await _unidadTrabajo.Bodega.Obtener(id);
        if(bodegaDb == null)

            {
                return Json(new { success = false, message = "Error al borrar Bodega" });
            }
            _unidadTrabajo.Bodega.Remover(bodegaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Bodega borrada con Exito" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(String nombre, int id =0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Bodega.ObtenerTodos();
            if(id== 0)
            {
                valor = lista.Any(b=>b.Nombre.ToLower().Trim() == nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == nombre.ToLower().Trim() && b.IdBodega != id);

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
