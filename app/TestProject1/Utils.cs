using dataaccess.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace TestProject1;

public static class Utils
{
  public static string ConnectionString = "data source=consul-tek.com; uid=dev; pwd=iconodev; initial catalog=andipandi; trustservercertificate=true;";

  public static SqlDbContext GetSqlDbContext()
  {
    var optionsBuilder = new DbContextOptionsBuilder<SqlDbContext>().UseSqlServer(ConnectionString).EnableDetailedErrors();
    return new SqlDbContext(optionsBuilder.Options);
  }
}