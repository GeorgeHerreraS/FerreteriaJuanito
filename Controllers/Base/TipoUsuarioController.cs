using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoUsuarioController : ControllerBase
    {
        private readonly DataContext _context;
        public TipoUsuarioController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<List<TipoUsuario>>> AddTipoUsuario(TipoUsuario tipousuario)
        {
            _context.TipoUsuarios.Add(tipousuario);
            await _context.SaveChangesAsync();
            return Ok(await _context.TipoUsuarios.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> GetAllTipoUsuario()
        {
            return Ok(await _context.TipoUsuarios.ToListAsync());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<TipoUsuario>>> DeleteTipoUsuario(int id)
        {
            var idBusc = await _context.TipoUsuarios.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("TipoUsuario no existe en BD");
            }
            else
            {
                _context.TipoUsuarios.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.TipoUsuarios.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<TipoUsuario>>> UpdateTipoUsuario(TipoUsuario tipoUsuario)
        {
            var updateId = await _context.TipoUsuarios.FindAsync(tipoUsuario.Id);
            if (updateId == null)
            {
                return NotFound("No existe Tipo de Usuario");
            }
            else
            {
                updateId.DescTipoUsuario = tipoUsuario.DescTipoUsuario;
                await _context.SaveChangesAsync();
                return Ok(await _context.TipoUsuarios.ToListAsync());
            }
        }
    }
}
