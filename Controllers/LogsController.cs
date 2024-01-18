using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Clases.Utils;
using FerreteriaJuanito.Controllers.Base;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly DataContext _context;
        public LogsController(DataContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<ActionResult<List<Logs>>> AddLogs(Logs logs)
        {
            _context.Logss.Add(logs);
            await _context.SaveChangesAsync();
            return Ok(await _context.Logss.ToListAsync());
        }
    }
}
