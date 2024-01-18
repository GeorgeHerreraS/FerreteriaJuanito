using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedioPagoController : ControllerBase
    {
        private readonly DataContext _context;
        public MedioPagoController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<MedioPago>>> AddMedioPago(MedioPago medioPago)
        {
            _context.MedioPagos.Add(medioPago);
            await _context.SaveChangesAsync();
            return Ok(await _context.MedioPagos.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<MedioPago>>> GetAllMedioPago()
        {
            return Ok(await _context.MedioPagos.ToListAsync());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<MedioPago>>> DeleteMedioPago(int id)
        {
            var idBusc = await _context.MedioPagos.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("Medio de Pago no existe en BD");
            }
            else
            {
                _context.MedioPagos.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.MedioPagos.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<MedioPago>>> UpdateMedioPago(MedioPago medioPago)
        {
            var updateId = await _context.MedioPagos.FindAsync(medioPago.Id);
            if (updateId == null)
            {
                return NotFound("No existe Medio de Pago");
            }
            else
            {
                updateId.DescMedioPago = medioPago.DescMedioPago;
                await _context.SaveChangesAsync();
                return Ok(await _context.MedioPagos.ToListAsync());
            }
        }

    }
}
