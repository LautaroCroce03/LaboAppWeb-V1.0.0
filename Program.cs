using LaboAppWebV1._0._0.DataAccess;
using LaboAppWebV1._0._0.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

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

    services.AddScoped<IRolDataAccess, LaboAppWebV1._0._0.DataAccess.Rol>();
    services.AddScoped<ISectorDataAccess, LaboAppWebV1._0._0.DataAccess.Sector>();
    services.AddScoped<IEmpleadoDataAccess, LaboAppWebV1._0._0.DataAccess.Empleado>();
    services.AddScoped<IEstadoMesaDataAccess, LaboAppWebV1._0._0.DataAccess.EstadoMesa>();
    services.AddScoped<IMesaDataAccess, LaboAppWebV1._0._0.DataAccess.Mesa>();

}