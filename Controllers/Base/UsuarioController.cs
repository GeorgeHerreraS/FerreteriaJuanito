using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using SQLitePCL;
using System.Data.SqlTypes;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        public UsuarioController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<Usuario>>> AddUsuario(Usuario usuario)
        {
            //validar tipo usuario

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(await _context.Usuarios.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetAllUsuario()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }
        [HttpGet("{email}/{password}")]
        //public ActionResult<Usuario> GetUsuario(string email, string password)
        public async Task<ActionResult<Usuario>> GetUsuario(string email, string password)
        {
            //  List<Usuario> result = _context.Usuarios.Where(x => x.Email == email && x.Password == password)
            //                      .ToList();

            if (_context.Usuarios == null)
            {
                return Problem("Error en consulta");
            }
            else {
                var consulta = from m in _context.Usuarios select m;
                if(!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password) ){
                    consulta = consulta.Where(s => s.Email == email && s.Password == password);
                }
                return Ok(await consulta.ToListAsync());
            }


            
            
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario(int id)
        {
            var idBusc = await _context.Usuarios.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("Usuario no existe en BD");
            }
            else
            {
                _context.Usuarios.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.Usuarios.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(Usuario usuario)
        {
            var updateId = await _context.Usuarios.FindAsync(usuario.Id);
            if (updateId == null)
            {
                return NotFound("No existe Usuario");
            }
            else
            {
                updateId.IdTipoUsuario = usuario.IdTipoUsuario;
                updateId.NombreUsuario = usuario.NombreUsuario;
                updateId.DireccionUsuario = usuario.DireccionUsuario;
                updateId.TelefonoUsuario = usuario.TelefonoUsuario;
                await _context.SaveChangesAsync();
                return Ok(await _context.Usuarios.ToListAsync());
            }
        }
    }
}
