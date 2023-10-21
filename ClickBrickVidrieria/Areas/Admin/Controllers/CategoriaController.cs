using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using ClickBrickVidrieria.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ClickBrickVidrieria.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CategoriaController : Controller
    {

        private readonly iUnidadTrabajo _unidadTrabajo;

        public CategoriaController(iUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index()
        {
            return View();
        }

     
        public async Task<IActionResult> Upsert(int? id)

        {

            Categoria categoria = new Categoria();

            if(id == null)
            {
                //crea una nueva categoria
                categoria.Estado = true;
                return View(categoria);
            }
            //actualización de la categoria
            categoria = await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if(categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upsert(Categoria categoria)

        {
            if(ModelState.IsValid)

            {
                if(categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "Categoria creada con exito";
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria actualizada con exito";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error al grabar la Categoria";
            return View(categoria);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {

            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });

        }

        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {

            var categoriaBd = await _unidadTrabajo.Categoria.Obtener(id);
        if(categoriaBd == null)

            {
                return Json(new { success = false, message = "Error al borrar Categoria" });
            }
            _unidadTrabajo.Categoria.Remover(categoriaBd);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Categoria borrada con Exito" });
        }

        [ActionName("ValidarNombre")]
        public async Task<IActionResult> ValidarNombre(String nombre, int id =0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();
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
