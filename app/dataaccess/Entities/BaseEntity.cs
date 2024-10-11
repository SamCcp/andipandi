namespace dataaccess.Entities;

public class BaseEntity
{
  public bool IsActive { get; set; } = true;
  public DateTime ModifiedDate { get; set; } = DateTime.Now;
}