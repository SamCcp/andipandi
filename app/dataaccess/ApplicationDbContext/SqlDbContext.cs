using dataaccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace dataaccess.ApplicationDbContext;

public class SqlDbContext : DbContext
{
  public DbSet<UserEntity> Usuarios { get; set; }
  public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options) {}
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    foreach (var model in typeof(SqlDbContext).Assembly.GetTypes()
               .Where(x => typeof(IModelBuilder).IsAssignableFrom(x)
                           && x is { IsInterface: false, IsAbstract: false })
               .Select(Activator.CreateInstance).Cast<IModelBuilder>())
    {
      model.ConfigureModel(modelBuilder);
    }
  }
}