using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TiendaWebApi.Extensions;
using TiendaWebApi.Interfaces;
using TiendaWebApi.Models;
using TiendaWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors();
//builder.Services.AddAplicacionServices();
//builder.Services.ConfigureApiVersioning();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//consultar
builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });



// se construye el servidor de base de datos, eh indica la direccion donde esta ubicada la la cadena de conexion
// para mas informacion vicite: https://learn.microsoft.com/es-es/ef/core/miscellaneous/connection-strings
builder.Services.AddDbContext<TiendaWebApiContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
