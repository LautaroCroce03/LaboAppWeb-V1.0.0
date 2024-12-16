using LaboAppWebV1._0._0.DataAccess;
using LaboAppWebV1._0._0.IServices;
using LaboAppWebV1._0._0.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configurar Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();




builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LaboAppWeb", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorizacion JWT esquema. \r\n\r\n Escribe 'Bearer' [espacio] y escribe el token proporcionado.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                        new List<string>()
                    }
                });

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true, //Tiempo de vida de un token
            ValidateIssuerSigningKey = true, //Validar llave de firma del emisor
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),//Llave secreta de token
            ClockSkew = TimeSpan.Zero,// Es para no tener errores
        };
    });

// **Configuración de Autorización con Roles**
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireSocioRole", policy => policy.RequireRole("Socio"));
    options.AddPolicy("RequireMozoRole", policy => policy.RequireRole("Mozo"));
    options.AddPolicy("RequireAdministradorRole", policy => policy.RequireRole("Administrador"));
    options.AddPolicy("RequireBartenderOrCerveceroOrCocineroRole", policy => policy.RequireRole("Bartender", "Cervecero", "Cocinero"));

});

ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging(); // Middleware para registrar solicitudes básicas

app.UseMiddleware<RequestResponseLoggingMiddleware>(); // Middleware personalizado
// Usar el middleware para manejo de excepciones
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Usar el middleware de TraceId
app.UseMiddleware<TraceIdMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    //Comentar la siguiente linea para prod
    var connectionString = configuration.GetConnectionString("DefaultConnection");


    services.AddDbContext<LaboAppWebV1Context>(options =>
                  options.UseSqlServer(connectionString));

    services.AddScoped<ISectorBusiness, LaboAppWebV1._0._0.Business.Sector>();
    services.AddScoped<IRolBusiness, LaboAppWebV1._0._0.Business.Rol>();
    services.AddScoped<IEmpleadoBusiness, LaboAppWebV1._0._0.Business.Empleado>();
    services.AddScoped<IEstadoMesaBusiness, LaboAppWebV1._0._0.Business.EstadoMesa>();
    services.AddScoped<IMesaBusiness, LaboAppWebV1._0._0.Business.Mesa>();
    services.AddScoped<IPedidoBusiness, LaboAppWebV1._0._0.Business.Pedido>();
    services.AddScoped<IComandaBusiness, LaboAppWebV1._0._0.Business.Comanda>();
    services.AddScoped<IProductoBusiness, LaboAppWebV1._0._0.Business.Producto>();
    services.AddScoped<IResponseApi, LaboAppWebV1._0._0.Business.ResponseApi>();
    services.AddScoped<IEstadoPedidoBusiness, LaboAppWebV1._0._0.Business.EstadoPedido>();

    services.AddScoped<IRolDataAccess, LaboAppWebV1._0._0.DataAccess.Rol>();
    services.AddScoped<ISectorDataAccess, LaboAppWebV1._0._0.DataAccess.Sector>();
    services.AddScoped<IEmpleadoDataAccess, LaboAppWebV1._0._0.DataAccess.Empleado>();
    services.AddScoped<IEstadoMesaDataAccess, LaboAppWebV1._0._0.DataAccess.EstadoMesa>();
    services.AddScoped<IMesaDataAccess, LaboAppWebV1._0._0.DataAccess.Mesa>();
    services.AddScoped<IPedidoDataAccess, LaboAppWebV1._0._0.DataAccess.Pedido>();
    services.AddScoped<IComandaDataAccess, LaboAppWebV1._0._0.DataAccess.Comanda>();
    services.AddScoped<IProductoDataAccess, LaboAppWebV1._0._0.DataAccess.Producto>();
    services.AddScoped<IEstadoPedidoDataAccess, LaboAppWebV1._0._0.DataAccess.EstadoPedido>();

    services.AddScoped<IEncriptar, LaboAppWebV1._0._0.Business.Encriptar>();
    services.AddScoped<ITokenJWT, LaboAppWebV1._0._0.Business.TokenJWT>();
    services.AddScoped<ILogin, LaboAppWebV1._0._0.Business.Login>();

    services.AddAutoMapper(typeof(LaboAppWebV1._0._0.Mappers.MappersP));
}