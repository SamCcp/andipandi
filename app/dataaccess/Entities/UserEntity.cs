using dataaccess.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace dataaccess.Entities;

public class UserEntity : BaseEntity
{
  public int Id { get; set; }
  public string Nombre { get; set; } = "";
  public string? Email { get; set; }
}

public class UserEntityModelBuilder : IModelBuilder
{
  public void ConfigureModel(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<UserEntity>(e =>
    {
      e.ToTable("usuarios");
      e.HasKey(p => p.Id);
    }); 
  }
}