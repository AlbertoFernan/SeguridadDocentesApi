using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SeguridadDocentesApi.Data;
using SeguridadDocentesApi.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SeguridadDocentesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        public LoginController(ItesrcneDocentesContext context)
        {
            Context = context;
        }
        public ItesrcneDocentesContext Context { get; set; }
        [HttpPost]
        public IActionResult Login(LoginDTO datos)
        {
            //no olvidar encriptar los datos
            var depto = Context.Departamentos.SingleOrDefault(x=>x.Clave==datos.Contraseña&&x.Contraseña==datos.Contraseña);
            if(depto==null||depto.Eliminado==1)//no existe
            {

                return Unauthorized("Contraseña o clave de departamento incorrecto");
            }
            else
            {
                //1. Crear los claims
                //2. Crear el token
                //3. Regresar el token

                List<Claim> claims = new()
                {
                    new Claim("id", depto.Id.ToString()),
                    new Claim("Clave", depto.Clave ?? ""),
                    new Claim(ClaimTypes.Name, depto.Nombre),
                    new Claim(ClaimTypes.Email, depto.Correo ?? ""),
                    new Claim(ClaimTypes.Role, "Departamento")
                };

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer = "docentes.itesrc.net",
                    Audience = "mauidocentes",
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddHours(.5),
                    SigningCredentials= new SigningCredentials( new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DocentesKey")),SecurityAlgorithms.HmacSha256),
                    Subject= new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme)
                };

                JwtSecurityTokenHandler handler= new JwtSecurityTokenHandler();

                var token = handler.CreateToken(tokenDescriptor);
                return Ok(handler.WriteToken(token));

            }
        }
    }
}
