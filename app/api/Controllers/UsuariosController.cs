using Azure;
using core.Features.Usuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class UsuariosController : IApiController
{
  public void MapController(WebApplication app)
  {
    var endpoints = app.MapGroup("/usuarios");
    endpoints.MapGet("/", GetUsuarios);
    endpoints.MapPost("/crear", CrearUsuario);
  }

  private static async Task<IResult> GetUsuarios([FromServices]ISender sender, CancellationToken cancellationToken)
  {
    var request = new ListaUsuariosRequest();
    var data = await sender.Send(request, cancellationToken);
    return Results.Ok(data);
  }

  private static async Task<IResult> CrearUsuario([FromServices] ISender sender,CrearUsuarioRequest request, CancellationToken cancellationToken)
  {
    var resultado = await sender.Send(request, cancellationToken);
    return Results.Ok(resultado);
  }
}