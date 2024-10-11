using core.Features.Usuarios;

namespace TestProject1.Usuarios;

public class UserTests
{
  [Fact]
  public async Task CrearUsuarioTest()
  {
    var request = new CrearUsuarioRequest
    {
      Nombre = "Sam el tucan",
      Email = "zH7kF@example.com"
    };

    var handler = new CrearUsuarioHandler(Utils.GetSqlDbContext());
    var response = await handler.Handle(request, CancellationToken.None);
    Assert.True(response.Ok);
  }

  [Fact]
  public async Task GetAllUsersTest()
  {
    var request = new ListaUsuariosRequest();
    var handler = new ListaUsuariosHandler(Utils.GetSqlDbContext());
    var response = await handler.Handle(request, CancellationToken.None);
    Assert.NotNull(response.Usuarios);
    Assert.True(response.Usuarios.Count > 0);
  }
}