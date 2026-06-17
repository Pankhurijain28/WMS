namespace WMS.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedOn { get; set; }
}