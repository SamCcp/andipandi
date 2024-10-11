using dataaccess.ApplicationDbContext;
using dataaccess.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace core.Features.Usuarios;


public class CrearUsuarioRequest : IRequest<CrearUsuarioResponse>
{
  public string Nombre { get; set; } = "";
  public string Email { get; set; } = "";
}


public class CrearUsuarioResponse
{
  public bool Ok { get; set; }
  public UserEntity? Usuario { get; set; }
}

// handler es el que se encarga de hacer la accion
public class CrearUsuarioHandler(SqlDbContext dbContext) 
  : IRequestHandler<CrearUsuarioRequest, CrearUsuarioResponse> 
{
  public async Task<CrearUsuarioResponse> Handle(CrearUsuarioRequest request, CancellationToken cancellationToken)
  {
    if (string.IsNullOrEmpty(request.Nombre))
    {
      return new CrearUsuarioResponse
      {
        Ok = false,
        Usuario = null
      };
    }
    
    //validar que el email no existe en la tabla
    var emailExiste = await dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken: cancellationToken);
    if (emailExiste != null)
    {
      return new CrearUsuarioResponse
      {
        Ok = false,
        Usuario = null
      };
    }

    var nuevoUsuario = new UserEntity
    {
      Nombre = request.Nombre,
      Email = request.Email
    };
    dbContext.Usuarios.Add(nuevoUsuario);
    await dbContext.SaveChangesAsync(cancellationToken);

    return new CrearUsuarioResponse
    {
      Ok = true,
      Usuario = nuevoUsuario
    };

  }
}