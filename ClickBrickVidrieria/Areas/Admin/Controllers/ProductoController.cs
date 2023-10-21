﻿using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using ClickBrickVidrieria.Modelos;
using ClickBrickVidrieria.Modelos.ViewModels;
using ClickBrickVidrieria.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace ClickBrickVidrieria.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ProductoController : Controller
    {

        private readonly iUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public ProductoController(iUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

     
        public async Task<IActionResult> Upsert(int? id)

        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Categoria"),
                MarcaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Marca"),
                PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Producto")
            };

            if(id == null)
            {
                // crear nuevo producto
                productoVM.Producto.Estado = true;
                return View(productoVM);
            }
            else 
            {
                productoVM.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if(productoVM.Producto == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Upsert(ProductoVM productoVM)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productoVM.Producto.Id == 0)
                {
                    //crear
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using(var fileStream = new FileStream(Path.Combine(upload, fileName+extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productoVM.Producto.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Producto.Agregar(productoVM.Producto);
                }
                else
                {
                    //actualizar
                    var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == productoVM.Producto.Id, isTracking: false);
                    if(files.Count>0) // SI SE CARGA UNA NUEVA IMAGEN PARA UN PRODUCTO YA EXISTENTE
                    {
                        String upload = webRootPath + DS.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension (files[0].FileName);

                        //borrar la imagen anterior

                        var anteriorfile = Path.Combine(upload, objProducto.ImagenUrl);
                        if(System.IO.File.Exists(anteriorfile))
                            {
                            System.IO.File.Delete(anteriorfile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productoVM.Producto.ImagenUrl = fileName + extension;
                    } // en caso no se cargue una nueva imagen
                    else
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _unidadTrabajo.Producto.Actualizar(productoVM.Producto); 
                }
                TempData[DS.Exitosa] = "Se guardo con exito";
                await _unidadTrabajo.Guardar();
                return View("Index");

            } // si algo llegara a fallar
            productoVM.CategoriaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Categoria");
            productoVM.MarcaLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Marca");
            productoVM.PadreLista = _unidadTrabajo.Producto.ObtenerTodosDropdownLista("Producto");
            return View(productoVM);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {

            var todos = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });

        }

        [HttpPost]
        public async Task <IActionResult> Delete(int id)
        {

            var productoBd = await _unidadTrabajo.Producto.Obtener(id);
        if(productoBd == null)

            {
                return Json(new { success = false, message = "Error al borrar Producto" });
            }

            //remover imagen
            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorFile = Path.Combine(upload, productoBd.ImagenUrl);

            if(System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }

            _unidadTrabajo.Producto.Remover(productoBd);
            await _unidadTrabajo.Guardar();
            return Json(new { success = true, message = "Producto borrado con Exito" });
        }

        [ActionName("ValidarSerie")]
        public async Task<IActionResult> ValidarSerie(String serie, int id =0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();
            if(id== 0)
            {
                valor = lista.Any(b=>b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == serie.ToLower().Trim() && b.Id != id);

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
