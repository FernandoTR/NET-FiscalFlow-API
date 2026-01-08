
namespace FiscalFlow.Domain.Entities;

public partial class CfdiBatch
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BatchItem> BatchItems { get; set; } = new List<BatchItem>();

    public virtual User User { get; set; } = null!;
}
