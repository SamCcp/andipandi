using dataaccess.ApplicationDbContext;
using dataaccess.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace core.Features.Usuarios;


public class ListaUsuariosRequest : IRequest<ListaUsuariosResponse> 
{}

public class ListaUsuariosResponse
{
  public List<UserEntity> Usuarios { get; set; }
}

public class ListaUsuariosHandler : IRequestHandler<ListaUsuariosRequest, ListaUsuariosResponse>
{
  private readonly SqlDbContext _dbContext;

  public ListaUsuariosHandler(SqlDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public async Task<ListaUsuariosResponse> Handle(ListaUsuariosRequest request, CancellationToken cancellationToken)
  {
    var usuarios = await _dbContext.Usuarios.ToListAsync(cancellationToken);
    return new ListaUsuariosResponse
    {
      Usuarios = usuarios
    };
  }
}