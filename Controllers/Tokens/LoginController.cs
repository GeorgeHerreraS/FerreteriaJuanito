using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Clases.Flujo;
using FerreteriaJuanito.Clases.Utils;
using FerreteriaJuanito.Data;
using FerreteriaJuanito.Exception;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FerreteriaJuanito.Controllers.Tokens
{
    [Route("api/[Controller]")]
    [ApiController]
    [TypeFilter(typeof(MyException))]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;
        public LoginController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost]
        public IActionResult Login(Login login) {
            
                var user = AuthenticateAsync(login);
                if (user.Result != null)
                {
                    var token = Generate(login.Emaillogin, login.PasswordLogin);
                    // token.EncodedPayload;
                    return Ok(token);
                }
                return NotFound("Error en datos entrada");
            
        }



        private async Task<Usuario> AuthenticateAsync(Login login)
        {
            if (_context.Usuarios == null) {
                return null;
            }
           var currentUser = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(login.Emaillogin.ToLower())
                  && u.Password.Equals(login.PasswordLogin));
           if (currentUser == null) { 
                return null; //CustomAttributes = Count = 2
            }            
            else { 
            return currentUser;
            }

            
        }
        private string Generate(string email, string password)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
             var rolTipoUsuario = from usu in _context.Usuarios
                                 join tip in _context.TipoUsuarios on usu.IdTipoUsuario equals tip.Id
                                 select new { usu.IdTipoUsuario, tip.DescTipoUsuario, usu.Email, usu.Password , usu.Id };
            rolTipoUsuario = rolTipoUsuario.Where(s => s.Email == email && s.Password == password);
            var rolUsu = rolTipoUsuario.ToListAsync();
            string rolUsuSt = rolUsu.Result[0].DescTipoUsuario.ToString();
            string rolUsuStId = rolUsu.Result[0].Id.ToString();
         
            string concat = (email+password).ToString();
            // Crear los claims
            var claims = new[]
            {

                new Claim(ClaimTypes.Email, email),
                new Claim("Password", password),
                new Claim("concat", concat),
                new Claim("usu", rolUsuStId),
                new Claim(ClaimTypes.Role, rolUsuSt)

            };


            // Crear el token

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
