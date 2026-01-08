namespace FiscalFlow.Domain.Entities;

public partial class StampBalance
{
    public Guid UserId { get; set; }

    public int AvailableStamps { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
