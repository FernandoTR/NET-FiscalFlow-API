
namespace FiscalFlow.Domain;

public partial class BatchItem
{
    public Guid Id { get; set; }

    public Guid BatchId { get; set; }

    public Guid CfdiId { get; set; }

    public string Status { get; set; } = null!;

    public virtual CfdiBatch Batch { get; set; } = null!;

    public virtual Cfdi Cfdi { get; set; } = null!;
}
