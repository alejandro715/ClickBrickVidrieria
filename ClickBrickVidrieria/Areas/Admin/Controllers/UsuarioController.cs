using ClickBrickVidrieria.AccesoDatos.Data;
using ClickBrickVidrieria.AccesoDatos.Repositorio.iRepositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClickBrickVidrieria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsuarioController : Controller
    {

        private readonly iUnidadTrabajo _unidadTabajo;
        private readonly ApplicationDbContext _db;

        public UsuarioController(iUnidadTrabajo unidadTabajo, ApplicationDbContext db)
        {
            _unidadTabajo = unidadTabajo;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region API
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var usuarioLista = await _unidadTabajo.UsuarioAplicacion.ObtenerTodos();
            var userRole = await _db.UserRoles.ToListAsync();
            var roles = await _db.Roles.ToListAsync();

            foreach (var usuario in usuarioLista)
            {
                var roleId = userRole.FirstOrDefault(u=> u.UserId == usuario.Id).RoleId;
                usuario.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
            }
            return Json(new { data = usuarioLista });
        }

        [HttpPost]
        public async Task<IActionResult> BloquearDesbloquear([FromBody] string id)
        {
            var usuario = await _unidadTabajo.UsuarioAplicacion.ObtenerPrimero(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Error de usuario" });
            }
            if(usuario.LockoutEnd != null && usuario.LockoutEnd > DateTime.Now)
            {
                usuario.LockoutEnd = DateTime.Now;
            }
            else
            {
                usuario.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            await _unidadTabajo.Guardar();
            return Json(new { success = true, message = "Operacion Exitosa" });
        }

        #endregion

    }
}
