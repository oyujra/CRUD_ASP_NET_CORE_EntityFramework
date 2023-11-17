using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUD_ASP_NET_CORE_EntityFramework.Models.ViewModels
{
    public class EmpleadoVM
    {
        public Empleado oEmpleado { get; set; }

        public List<SelectListItem> oListaCargo { get; set; }

    }
}
