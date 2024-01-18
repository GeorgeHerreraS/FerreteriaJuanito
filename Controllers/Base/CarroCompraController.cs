using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Clases.Utils;
using FerreteriaJuanito.Data;
using FerreteriaJuanito.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace FerreteriaJuanito.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroCompraController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<CarroCompraController> _logger;
        private static readonly string v = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        Logs logs = new Logs();
        //private DateTime fechaLogin = v;

        public CarroCompraController(DataContext context, ILogger<CarroCompraController> logger)
        {
            _context = context;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<ActionResult<List<CarroCompra>>> AddCarroCompra(CarroCompra carroCompra)
        {
            logs.LogDescription = " --> IdUsuario =" + carroCompra.IdUsuario+ " IdProducto=" + carroCompra.IdProducto+ " IdMedioPago=" + carroCompra.IdMedioPago+ " IdDespacho=" + carroCompra.IdDespacho+" :)";
            logs.FechaLog = Convert.ToDateTime(v);
            logs.LogName = "AddCarroCompra";
            logs.Clase = "" + carroCompra.GetType();
            _logger.LogWarning("logs -->"+logs.FechaLog.ToString()+" - "+ logs.Clase.ToString() + " - " + logs.LogDescription.ToString() + " - " + logs.LogName.ToString());
            _context.Logss.Add(logs);
            _context.CarroCompras.Add(carroCompra);
            await _context.SaveChangesAsync();
            return Ok(await _context.CarroCompras.ToListAsync());
        }
        [HttpGet]
        public async Task<ActionResult<List<CarroCompra>>> GetAllCarroCompra()
        {
            return Ok(await _context.CarroCompras.ToListAsync());
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<CarroCompra>>> DeleteCarroCompra(int id)
        {
            var idBusc = await _context.CarroCompras.FindAsync(id);
            if (idBusc == null)
            {
                return BadRequest("Carro Compra vacio o no existe en BD");
            }
            else
            {
                _context.CarroCompras.Remove(idBusc);
                await _context.SaveChangesAsync();

                return Ok(await _context.CarroCompras.ToListAsync());
            }
        }
        [HttpPut]
        public async Task<ActionResult<List<CarroCompra>>> UpdateCarroCompra(CarroCompra carroCompra)
        {
            var updateId = await _context.CarroCompras.FindAsync(carroCompra.Id);
            if (updateId == null)
            {
                return NotFound("No existe carro compra");
            }
            else
            {
                updateId.IdProducto = carroCompra.IdProducto;
                updateId.IdUsuario = carroCompra.IdUsuario;
                updateId.IdMedioPago = carroCompra.IdMedioPago;
                updateId.IdDespacho = carroCompra.IdDespacho;
                await _context.SaveChangesAsync();
                return Ok(await _context.CarroCompras.ToListAsync());
            }


        }

    }
}
