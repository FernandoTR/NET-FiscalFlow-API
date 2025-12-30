
namespace FiscalFlow.Domain;

public partial class Cfdi
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid Uuid { get; set; }

    public string RfcEmisor { get; set; } = null!;

    public string RfcReceptor { get; set; } = null!;

    public string Version { get; set; } = null!;

    public decimal Total { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    public string CurrentStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BatchItem> BatchItems { get; set; } = new List<BatchItem>();

    public virtual ICollection<CfdiPdf> CfdiPdfs { get; set; } = new List<CfdiPdf>();

    public virtual ICollection<CfdiStatusHistory> CfdiStatusHistories { get; set; } = new List<CfdiStatusHistory>();

    public virtual ICollection<CfdiXml> CfdiXmls { get; set; } = new List<CfdiXml>();

    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    public virtual ICollection<StampMovement> StampMovements { get; set; } = new List<StampMovement>();

    public virtual User User { get; set; } = null!;
}
