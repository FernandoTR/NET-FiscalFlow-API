
namespace FiscalFlow.Domain.Entities;

public partial class StampMovement
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? CfdiId { get; set; }

    public string MovementType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Cfdi? Cfdi { get; set; }

    public virtual User User { get; set; } = null!;
}
