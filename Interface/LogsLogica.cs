using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Clases.Utils;
using FerreteriaJuanito.Controllers;
using FerreteriaJuanito.Controllers.Base;
using FerreteriaJuanito.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Interface
{
    public class LogsLogica
    {
        private readonly DataContext _context;
        private readonly ILogger<CarroCompraController> _logger;
        private static readonly string v = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        //private DateTime fechaLogin = v;

        public LogsLogica(DataContext context, ILogger<CarroCompraController> logger)
        {
            _context = context;
            this._logger = logger;
        }
        public async Task<ActionResult<List<Logs>>> AddLogs(Logs logs)
        {
            logs.LogName = logs.LogName;
            logs.LogDescription = logs.LogDescription;
            logs.FechaLog = logs.FechaLog;
            logs.Clase = logs.Clase;
            await _context.Logss.AddAsync(logs);
            await _context.SaveChangesAsync();
            if (logs != null) {
                return Ok(await _context.Logss.ToListAsync());
            }
            return Ok(await _context.Logss.ToListAsync());

        }
        public static void LogWrite(string texto)
        {
            
                StreamWriter sw;
                string logPath = $"Logs/{DateTime.Now.ToString("yyyy-MM-dd")}-log.txt";
                sw = (File.Exists(logPath)) ? File.AppendText(logPath) : File.CreateText(logPath);
                sw.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss")}] {texto}");
                sw.Close();
            
        }

        private ActionResult<List<Logs>> Ok(List<Logs> logs)
        {
            throw new NotImplementedException();
        }
    }
}
