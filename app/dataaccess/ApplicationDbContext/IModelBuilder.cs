using Microsoft.EntityFrameworkCore;

namespace dataaccess.ApplicationDbContext;

public interface IModelBuilder
{
  void ConfigureModel(ModelBuilder modelBuilder);
}