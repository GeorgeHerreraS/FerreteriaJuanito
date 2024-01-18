using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespachoController : ControllerBase
    {
        private readonly DataContext _context;
        public DespachoController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<Despacho>>> AddDespacho(Despacho despacho)
        {
            _context.Despachos.Add(despacho);
            await _context.SaveChangesAsync();
            return Ok(await _context.Despachos.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<Despacho>>> GetAllDespacho()
        {
            return Ok(await _context.Despachos.ToListAsync());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Despacho>>> DeleteDespacho(int id)
        {
            var idBusc = await _context.Despachos.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("Despacho no existe en BD");
            }
            else
            {
                _context.Despachos.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.Despachos.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<Despacho>>> UpdateDespacho(Despacho despacho)
        {
            var updateId = await _context.Despachos.FindAsync(despacho.Id);
            if (updateId == null)
            {
                return NotFound("No existe Despacho");
            }
            else
            {
                
                updateId.IdUsuario = despacho.IdUsuario;
                updateId.DescDespacho = despacho.DescDespacho;
                await _context.SaveChangesAsync();
                return Ok(await _context.Despachos.ToListAsync());
            }
        }

    }
}
