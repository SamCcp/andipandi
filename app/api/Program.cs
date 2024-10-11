using api.Controllers;
using core;
using core.Features.Usuarios;
using dataaccess.ApplicationDbContext;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cadenaConexion = builder.Configuration.GetConnectionString("sql");
var optionsBuilder = new DbContextOptionsBuilder<SqlDbContext>().UseSqlServer(cadenaConexion).EnableDetailedErrors();
var dbContext = new SqlDbContext(optionsBuilder.Options);

builder.Services.AddMediatR(config =>
{
  config.RegisterServicesFromAssembly(typeof(CorePointer).Assembly);
});


builder.Services.AddSingleton<SqlDbContext>(new SqlDbContext(optionsBuilder.Options));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.ApiControllerMapper();

app.MapGet("/listausuarios", async () =>
{
  var request = new ListaUsuariosRequest();
  var handler = new ListaUsuariosHandler(dbContext);
  var response = await handler.Handle(request, CancellationToken.None);
  return response;
});

app.MapGet("/listausuarios2", async ([FromServices]ISender sender, CancellationToken cancellationToken) =>
{
  var request = new ListaUsuariosRequest();
  var data = await sender.Send(request, cancellationToken);
  return data.Usuarios;
});

app.MapPost("/crearusuario", async (
  [FromServices] ISender sender,
  CrearUsuarioRequest request,
  CancellationToken cancellationToken
) =>
{
  var resultado = await sender.Send(request, cancellationToken);
  return resultado;
});

app.Run();
