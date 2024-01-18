using FerreteriaJuanito.Clases;
using FerreteriaJuanito.Clases.Utils;
using FerreteriaJuanito.Controllers.Base;
using FerreteriaJuanito.Data;
using FerreteriaJuanito.Exception;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Expressions;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace FerreteriaJuanito.Controllers.Flujo
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(MyException))]
    public class FerreteriaController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<CarroCompraController> _logger;
        private readonly DataContext _context;
        private static readonly string v = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        Response resp = new Response();
        public FerreteriaController(DataContext context, ILogger<CarroCompraController> logger, IConfiguration config)
        {
            _context = context;
            this._logger = logger;
            _config = config;
        }

        //__ Inicio usuario
        [HttpGet("ListUsers")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Usuario>>> GetAllUsuario([FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var list = (await _context.Usuarios.ToListAsync());
            int count = list.Count;
            if (count > 0) { 
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "GetAllUsuario", _context.Usuarios.GetType().Name, idUsuario);
                return Ok(list);
            }
               resp.Code = 404;
               resp.Type = "No existen Registros => ";
               resp.Description = "ERRFER009";
               var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllUsuario", _context.Usuarios.GetType().Name, idUsuario);
               return NotFound(resp);
        }

        [HttpPost("addUsers")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Usuario>>> AddUsersAdm(Usuario usuario, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            if (usuario.NombreUsuario != "" && usuario.IdTipoUsuario > 0 && usuario.DireccionUsuario != "" && usuario.Email != "" && usuario.Password != ""
                && usuario.TelefonoUsuario != "")
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                var list = (await _context.Usuarios.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "AddUsers", _context.Usuarios.GetType().Name, idUsuario);
                return Ok(list);
            }

               resp.Code = 404;
               resp.Type = "Error al guardar usuario => " + usuario.NombreUsuario;
               resp.Description = "ERRFER007";
               var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "AddUsers", _context.Usuarios.GetType().Name, 1);//idUsuario);
               return NotFound(resp);
        }

        [HttpDelete("DeleteUsers")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Usuario>>> DeleteUsuario(int id, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var idBusc = await _context.Usuarios.FindAsync(id);
            if (idBusc == null)
            {
                resp.Code = 404;
                resp.Type = "Registro no encontrado => " + id;
                resp.Description = "ERRFER008";
                var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "DeleteUsuario", _context.Usuarios.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            else
            {
                _context.Usuarios.Remove(idBusc);
                await _context.SaveChangesAsync();
                var list = (await _context.Usuarios.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "DeleteUsuario", _context.Usuarios.GetType().Name, idUsuario);
                return Ok(list);
            }
        }
        [HttpPut("UpdateUsers")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Usuario>>> UpdateUsuario(Usuario usuario, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var updateId = await _context.Usuarios.FindAsync(usuario.Id);
            if (updateId == null)
            {
                resp.Code = 404;
                resp.Type = "No existe Registro=> " + usuario.Id;
                resp.Description = "ERRFER008";
                var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "UpdateUsuario", _context.Usuarios.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            else
            {
                updateId.IdTipoUsuario = usuario.IdTipoUsuario;
                updateId.NombreUsuario = usuario.NombreUsuario;
                updateId.DireccionUsuario = usuario.DireccionUsuario;
                updateId.TelefonoUsuario = usuario.TelefonoUsuario;
                await _context.SaveChangesAsync();
                var list = (await _context.Usuarios.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "UpdateUsuario", _context.Usuarios.GetType().Name, idUsuario);
                return Ok(list);
            }
        }


        //__fin usuario
        //__Inicio Productos
        [HttpGet("GetAllProductoAdmin")]
        [Authorize(Roles = ("Cliente, Administrador"))]
        public async Task<ActionResult<List<Producto>>> GetAllProducto([FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var ListPro = await _context.Productos.ToListAsync();
            if (ListPro.Count <= 0) {
                resp.Code = 404;
                resp.Type = "No existen Registros => ";
                resp.Description = "ERRFER010";
                var savelNotFo = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllProducto", _context.Productos.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            var consulta = from prod in _context.Productos
                           join prov in _context.Proveedors on prod.IdProveedor equals prov.Id                           
                           select new { prov.NombreProveedor, prod.DescProducto, prod.ValorProducto, prod.CantidadProducto };
            consulta = consulta.Where(s => s.CantidadProducto >0);
            if (consulta.Count() > 0)
            {
                var list = (await consulta.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "GetAllProducto", _context.Productos.GetType().Name, idUsuario);
                return Ok(list);
            }
            resp.Code = 404;
            resp.Type = "Error de datos ";
            resp.Description = "ERRFER011";
            var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllProducto", _context.Productos.GetType().Name, idUsuario);
            return NotFound(resp);
        }
        [HttpGet("GetAllProductoCli")]
        [Authorize(Roles = ("Cliente, Administrador"))]
        public async Task<ActionResult<List<Producto>>> GetAllProductoCli([FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var ListPro = await _context.Productos.ToListAsync();
            if (ListPro.Count <= 0)
            {
                resp.Code = 404;
                resp.Type = "No existen Registros => ";
                resp.Description = "ERRFER012";
                var savelNotFo = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllProductoCli", _context.Productos.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            var consulta = from prod in _context.Productos
                           join prov in _context.Proveedors on prod.IdProveedor equals prov.Id
                           select new {  prod.DescProducto, prod.ValorProducto, prod.CantidadProducto };
            consulta = consulta.Where(s => s.CantidadProducto > 0);
            if (consulta.Count() > 0)
            {
                var list = (await consulta.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "GetAllProductoCli", _context.Productos.GetType().Name, idUsuario);
                return Ok(list);
            }
            resp.Code = 404;
            resp.Type = "Error de datos ";
            resp.Description = "ERRFER013";
            var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllProductoCli", _context.Productos.GetType().Name, idUsuario);
            return NotFound(resp);
        }
        [HttpPost("addProducto")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Producto>>> AddProducto(Producto producto, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            if (producto.CantidadProducto >0 && producto.DescProducto !="" && producto.ValorProducto >0 && producto.IdProveedor >0)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                var list = (await _context.Productos.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "AddProducto", _context.Usuarios.GetType().Name, idUsuario);
                return Ok(list);
            }

            resp.Code = 404;
            resp.Type = "Error al guardar Producto => " + producto.DescProducto;
            resp.Description = "ERRFER014";
            var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "AddProducto", _context.Usuarios.GetType().Name, idUsuario);
            return NotFound(resp);
        }

        [HttpDelete("DeleteProducto")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Producto>>> DeleteProducto(int id, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var idBusc = await _context.Productos.FindAsync(id);
            if (idBusc == null)
            {
                resp.Code = 404;
                resp.Type = "Registro no encontrado => " + id;
                resp.Description = "ERRFER015";
                var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "DeleteProducto", _context.Productos.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            else
            {
                _context.Productos.Remove(idBusc);
                await _context.SaveChangesAsync();
                var list = (await _context.Productos.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "DeleteProducto", _context.Productos.GetType().Name, idUsuario);
                return Ok(list);
            }
        }
        [HttpPut("UpdateProducto")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<Producto>>> UpdateProducto(Producto producto, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var updateId = await _context.Productos.FindAsync(producto.Id);
            if (updateId == null)
            {
                resp.Code = 404;
                resp.Type = "No existe Registro=> " + producto.Id;
                resp.Description = "ERRFER016";
                var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "UpdateProducto", _context.Productos.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            else
            {
                if (producto.IdProveedor > 0) { updateId.IdProveedor = producto.IdProveedor; }
                if (producto.DescProducto != "") { updateId.DescProducto = producto.DescProducto; }
                if (producto.ValorProducto > 0) { updateId.ValorProducto = producto.ValorProducto; }
                if (producto.CantidadProducto > 0) { updateId.CantidadProducto = producto.CantidadProducto; }
                await _context.SaveChangesAsync();
                var list = (await _context.Productos.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "UpdateProducto", _context.Productos.GetType().Name, idUsuario);
                return Ok(list);
            }           
        }

        //__Fin Productos

        //__Inicio Carro
        [HttpGet("GetAllCarroHistCli")]
        [Authorize(Roles = ("Cliente, Administrador"))]
        public async Task<ActionResult<List<CarroCompra>>> GetAllCarroHistCli([FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var consulta = from c in _context.CarroCompras
                           join usu in _context.Usuarios on c.IdUsuario equals usu.Id
                           join prod in _context.Productos on c.IdProducto equals prod.Id
                           join des in _context.Despachos on c.IdDespacho equals des.Id
                           join med in _context.MedioPagos on c.IdMedioPago equals med.Id
                           select new { c.IdUsuario, c.Id, prod.DescProducto, prod.ValorProducto, c.Cantidad };
            consulta = consulta.Where(s => s.IdUsuario == idUsuario);
            int contador = consulta.Count();
            if (contador > 0)
            {
                var list = (await consulta.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "GetAllCarroHistCli", _context.CarroCompras.GetType().Name, idUsuario);
                return Ok(list);
            }

            resp.Code = 404;
            resp.Type = "No Existe registro solicitado " + idUsuario;
            resp.Description = "ERRFER001";
            var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllCarroHistCli", _context.CarroCompras.GetType().Name, idUsuario);
            return NotFound(resp);
        }
        [HttpGet("GetAllCarroHistAdm")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<CarroCompra>>> GetAllCarroHistAdm([FromRoute] int id, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var consulta = from c in _context.CarroCompras
                           join usu in _context.Usuarios on c.IdUsuario equals usu.Id
                           join prod in _context.Productos on c.IdProducto equals prod.Id
                           join des in _context.Despachos on c.IdDespacho equals des.Id
                           join med in _context.MedioPagos on c.IdMedioPago equals med.Id
                           select new { c.IdUsuario, c.Id, prod.DescProducto, prod.ValorProducto, c.Cantidad };
            consulta = consulta.Where(s => s.IdUsuario == id);
            int contador = consulta.Count();
            if (contador > 0)
            {
                var list = (await consulta.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "GetAllCarroHistAdm", _context.CarroCompras.GetType().Name, idUsuario);
                return Ok(list);
            }

            resp.Code = 404;
            resp.Type = "No Existe registro solicitado " + id;
            resp.Description = "ERRFER017";
            var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "GetAllCarroHistAdm", _context.CarroCompras.GetType().Name, idUsuario);
            return NotFound(resp);
        }
        [HttpPost("AddCarro")]
        [Authorize(Roles = ("Cliente, Administrador"))]
        public async Task<ActionResult<List<CarroCompra>>> AddCarro(CarroCompra carroCompra, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);

            if (_context.Usuarios == null)
            {
                resp.Code = 404;
                resp.Type = "No Existe registro solicitado ";
                resp.Description = "ERRFER002";
                var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "AddCarro", _context.CarroCompras.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            else
            {
                var consulta = from m in _context.Productos
                               join prov in _context.Proveedors on m.IdProveedor equals prov.Id
                               select new { m.Id, m.DescProducto, m.ValorProducto, m.CantidadProducto };
                if (carroCompra.IdUsuario > 0 && carroCompra.IdMedioPago > 0 &&
                    carroCompra.IdDespacho > 0 && carroCompra.IdProducto > 0)
                {
                    consulta = consulta.Where(s => s.Id == carroCompra.IdProducto && s.CantidadProducto >= 0 && carroCompra.Cantidad <= s.CantidadProducto);
                    int contador = consulta.Count();
                    if (contador > 0)
                    {
                        int cantProd = consulta.ToListAsync().Result[0].CantidadProducto;
                        int valor = consulta.ToListAsync().Result[0].ValorProducto;
                        int resta = cantProd - carroCompra.Cantidad;

                        var update = UpdateProductoAsync(resta, carroCompra.IdProducto, key);
                        int cantidaxvalor = Convert.ToInt32(carroCompra.Cantidad) * Convert.ToInt32(valor);
                        carroCompra.Total= cantidaxvalor;
                        _context.CarroCompras.Add(carroCompra);
                        await _context.SaveChangesAsync();
                        //var s = consulta.ToListAsync().Result[0].Id;
                        var respu = from car in _context.CarroCompras
                                    join pro in _context.Productos on car.IdProducto equals pro.Id
                                    join usu in _context.Usuarios on car.IdUsuario equals usu.Id
                                    join med in _context.MedioPagos on car.IdMedioPago equals med.Id
                                    join des in _context.Despachos on car.IdDespacho equals des.Id
                                    //orderby car.Id descending
                                    select new { car.IdUsuario, car.Cantidad, pro.DescProducto, med.DescMedioPago, des.DescDespacho };
                        respu = respu.Where(s => s.Cantidad > 0 && idUsuario == s.IdUsuario);
                        var savelok = Savelogs("ok" + respu.ToString(), Convert.ToDateTime(v), "AddCarro", _context.CarroCompras.GetType().Name, idUsuario);
                        return Ok(await respu.ToListAsync());
                    }
                    else
                    {
                        resp.Code = 404;
                        resp.Type = "No Existe registro solicitado ";
                        resp.Description = "ERRFER003";
                        var savelNot3 = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "AddCarro", _context.CarroCompras.GetType().Name, idUsuario);
                        return NotFound("Error en datos entrada, validar si producto existe!!!");
                    }
                }

            }
            resp.Code = 404;
            resp.Type = "No Existe registro solicitado ";
            resp.Description = "ERRFER004";
            var savelNot4 = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "AddCarro", _context.CarroCompras.GetType().Name, idUsuario);
            return NotFound("Error en datos entrada");

        }
        [HttpDelete("DeleteCarro")]
        [Authorize(Roles = ("Administrador"))]
        public async Task<ActionResult<List<CarroCompra>>> DeleteCarro(int id, [FromHeader] string Authorization)
        {
            var aut = Authorization;
            string key = aut.Remove(0, 7);// ("Bearer ");
            int idUsuario = GetName(key);
            var idBusc = await _context.CarroCompras.FindAsync(id);
            if (idBusc == null)
            {
                resp.Code = 404;
                resp.Type = "Registro no encontrado => " + id;
                resp.Description = "ERRFER018";
                var savelNot = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "DeleteCarro", _context.Productos.GetType().Name, idUsuario);
                return NotFound(resp);
            }
            else
            {
                _context.CarroCompras.Remove(idBusc);
                await _context.SaveChangesAsync();
                var list = (await _context.CarroCompras.ToListAsync());
                var savelok = Savelogs("ok" + list.ToString(), Convert.ToDateTime(v), "DeleteCarro", _context.CarroCompras.GetType().Name, idUsuario);
                return Ok(list);
            }
        }


        //__Fin Carro







        protected async Task<ActionResult<List<Producto>>> UpdateProductoAsync(int cantidad, int id, string key) {
           
            int idUsuario = GetName(key);
            if (cantidad >= 0) {
                var updateId = await _context.Productos.FindAsync(id);
                if (updateId == null)
                {
                    resp.Code = 404;
                    resp.Type = "Error registro no encontrado";
                    resp.Description = "ERRFER006";
                    var savelNot6 = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "UpdateProductoAsync", _context.Productos.GetType().Name, idUsuario);
                    return NotFound(resp);
                }
                else
                {
                    updateId.CantidadProducto = cantidad;
                    await _context.SaveChangesAsync();
                    var savelok = Savelogs("ok" + cantidad + " " + id, Convert.ToDateTime(v), "UpdateProductoAsync", _context.Productos.GetType().Name, idUsuario);
                    return Ok(await _context.Productos.ToListAsync());
                }                

            }
            resp.Code = 404;
            resp.Type = "Error cantidad, para update";
            resp.Description = "ERRFER005";
            var savelNot5 = await Savelogs(resp.Type.ToString(), Convert.ToDateTime(v), "UpdateProductoAsync", _context.Productos.GetType().Name, idUsuario);
            return NotFound(resp);
        }
        private async Task<ActionResult<Logs>> Savelogs(String LogDescription, DateTime FechaLog, String LogName, String Clase, int id) {
            var logs = new Logs();
            logs.LogDescription = LogDescription;
            logs.FechaLog = FechaLog;
            logs.LogName = LogName;
            logs.Clase = Clase;
            logs.IdUsuario = id;

            _context.Logss.Add(logs);
             var save= await _context.SaveChangesAsync();
            return Ok();
        
        }
        protected int GetName(string token)
        {
            string secret = _config["Jwt:Key"];
            var tok = new JwtSecurityTokenHandler().ReadToken(token);
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            //var claims = tokenS.Payload.Claims.GetType(AttributeTargets.ReturnValue);
            int id = Convert.ToInt32(tokenS.Claims.First(claim => claim.Type == "usu").Value);
            var savelok = Savelogs("ok" + id, Convert.ToDateTime(v), "GetName", "Data", id);
            return id;
        }

    }
}
