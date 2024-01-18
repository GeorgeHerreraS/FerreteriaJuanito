using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly DataContext _context;
        public ProveedorController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<Proveedor>>> AddProveedor(Proveedor proveedor)
        {
            _context.Proveedors.Add(proveedor);
            await _context.SaveChangesAsync();
            return Ok(await _context.Proveedors.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> GetAllProveedor()
        {
            return Ok(await _context.Proveedors.ToListAsync());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Proveedor>>> DeleteProveedor(int id)
        {
            var idBusc = await _context.Proveedors.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("Proveedor no existe en BD");
            }
            else
            {
                _context.Proveedors.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.Proveedors.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<Proveedor>>> UpdateProveedor(Proveedor proveedor)
        {
            var updateId = await _context.Proveedors.FindAsync(proveedor.Id);
            if (updateId == null)
            {
                return NotFound("No existe Proveedor");
            }
            else
            {
                updateId.NombreProveedor = proveedor.NombreProveedor;
                updateId.DireccionProveedor = proveedor.DireccionProveedor;
                await _context.SaveChangesAsync();
                return Ok(await _context.Proveedors.ToListAsync());
            }
        }
    }
}

