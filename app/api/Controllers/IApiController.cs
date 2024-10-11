namespace api.Controllers;

public interface IApiController
{
  void MapController(WebApplication app);
}

public static class MapApiControllers
{
  /// <summary>
  /// Method that uses reflection to register all the controllers
  /// </summary>
  public static WebApplication ApiControllerMapper(this WebApplication app)
  { 
    foreach (var controller in typeof(Program).Assembly.GetTypes().Where(x => typeof(IApiController).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false }).Select(Activator.CreateInstance).Cast<IApiController>())
    {
      controller.MapController(app);
    }
    return app;
  }
}