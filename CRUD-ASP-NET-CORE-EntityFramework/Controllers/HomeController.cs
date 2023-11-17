using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using CRUD_ASP_NET_CORE_EntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CRUD_ASP_NET_CORE_EntityFramework.Models.ViewModels;

namespace CRUD_ASP_NET_CORE_EntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbcrudcoreContext _dbcrudcoreContext;

        public HomeController(DbcrudcoreContext dbcrudcoreContext)
        {
            _dbcrudcoreContext = dbcrudcoreContext;
        }

        public IActionResult Index()
        {
            List<Empleado> lista = _dbcrudcoreContext.Empleados.Include(c => c.oCargo).ToList();
            return View(lista);
        }
        [HttpGet]
        public IActionResult Empleado_Detalle(int idEmpleado)
        {
            EmpleadoVM oEmpleadoVM = new EmpleadoVM()
            {
                oEmpleado = new Empleado(),
                oListaCargo = _dbcrudcoreContext.Cargos.Select(cargo => new SelectListItem()
                {
                    Text = cargo.Descripcion,
                    Value = cargo.IdCargo.ToString()

                }).ToList()
            };

            if (idEmpleado != 0)
            {

                oEmpleadoVM.oEmpleado = _dbcrudcoreContext.Empleados.Find(idEmpleado);
            }


            return View(oEmpleadoVM);
        }

        [HttpPost]
        public IActionResult Empleado_Detalle(EmpleadoVM oEmpleadoVM)
        {
            if (oEmpleadoVM.oEmpleado.IdEmpleado == 0)
            {
                _dbcrudcoreContext.Empleados.Add(oEmpleadoVM.oEmpleado);

            }
            else
            {
                _dbcrudcoreContext.Empleados.Update(oEmpleadoVM.oEmpleado);
            }

            _dbcrudcoreContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Eliminar(int idEmpleado)
        {
            Empleado oEmpleado = _dbcrudcoreContext.Empleados.Include(c => c.oCargo).Where(e => e.IdEmpleado == idEmpleado).FirstOrDefault();

            return View(oEmpleado);
        }

        [HttpPost]
        public IActionResult Eliminar(Empleado oEmpleado)
        {
            _dbcrudcoreContext.Empleados.Remove(oEmpleado);
            _dbcrudcoreContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

    }
}