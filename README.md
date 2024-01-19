# FerreteriaJuanito
se agregan de paquetes:
  - Microsoft.AspNetCore.Authentication.JwtBearer
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.Design
  - Microsoft.EntityFrameworkCore.Sqlite

    se crea una BD local Sqlite.db donde se registran las tablas y se trabaja con sentencias LINQ a su vez se crea una tabla para registros de logs.

    se trabaja con jwtBearer para login donde se encripta datos de sesion y valores identifican la cuenta asi a su vez se pasa un tiempo de caducidad del token.-

    se adjunta proyecto postman para su ejecucion por medio de flujo el cual es obligatorio el jwt. definiendo carpeta para labores administrativa y de cliente con estos mismos roles de filtra el inicio.

    se crea clase MyException para controlar errores como 500

    se crea response para errores controlados y enumerados identificando explicicamente el lugar donde este ocurrio.

    comandos utilizados:
      - dotnet ef migrations add InitialMigra    => migra las clases para poder crear las tablas
      - dotnet ef database update                => crea la BD Sqlite.db en raiz del proyecto.-

  En Program.cs se gregan:
      //version y titulo
    -  builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "==> Ferretería Juanito", Version = "v1" }));
        //Sqlite 
    -  builder.Services.AddDbContext<DataContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

    -  builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });


-  En appsettings.json se agregan parametros para Jwt
                      url emula servicio 
        "Jwt": {
                "Key": "u,F@%0£6C:K01h9.LD_`xZDgZrEeJ+3Dlsh",
                "Issuer": "https://localhost:4200",
                "Audience": "https://localhost:4200"
              }
-  se agrega el nivel logger para el controlador    -->   "FerreteriaJuanito.Controllers": "Warning"



-  En clases/ se crea la carpeta /utils   donde se comparten las clases para poder tener funciones "Utiles"
-  En /Controllers se crea la carpeta /Base donde estan alojados los controladores y poder montar o ejecutar directamente sin jwt. esta carpeta es solo para develop y poder completar las tabblas con valores necesarios.
-   El controlador principal "FerreteriaController.cs" se agrega [Authorize(Roles = ("Cliente, Administrador"))] para poder filtrar los usuarios.- (tabla Tipo_Usuario)


    
