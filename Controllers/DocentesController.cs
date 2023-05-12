using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeguridadDocentesApi.Data;
using SeguridadDocentesApi.DTO;

namespace SeguridadDocentesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocentesController : ControllerBase
    {
        private readonly ItesrcneDocentesContext context;
        public DocentesController(ItesrcneDocentesContext context)
        {
            this.context= context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
         var docentes=context.Usuarios.OrderBy(x => x.Nombre).Where(x=>x.Eliminado==0).Select(x=> new DocenteDTO
            {
                Id=x.Id,
                Correo=x.Correo??"",//null-coalescene,
                Nombre=x.Nombre,
                NumEmpleado=x.NumEmpleado,
            });;

            return Ok(docentes);
        }
    }
}
