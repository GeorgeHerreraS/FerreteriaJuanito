using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductoController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<Producto>>> AddProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return Ok(await _context.Productos.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAllProducto()
        {
            return Ok(await _context.Productos.ToListAsync());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<Producto>>> DeleteProducto(int id)
        {
            var idBusc = await _context.Productos.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("Producto no existe en BD");
            }
            else
            {
                _context.Productos.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.Productos.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<Producto>>> UpdateProducto(Producto producto)
        {
            var updateId = await _context.Productos.FindAsync(producto.Id);
            if (updateId == null)
            {
                return NotFound("No existe Producto");
            }
            else
            {
                if (producto.IdProveedor != 0) { updateId.IdProveedor = producto.IdProveedor; }
                if (producto.DescProducto != "") { updateId.DescProducto = producto.DescProducto; }
                if (producto.ValorProducto != 0) { updateId.ValorProducto = producto.ValorProducto; }
                if (producto.CantidadProducto != 0) { updateId.CantidadProducto = producto.CantidadProducto; }
                await _context.SaveChangesAsync();
                return Ok(await _context.Productos.ToListAsync());
            }
        }

    }
}
