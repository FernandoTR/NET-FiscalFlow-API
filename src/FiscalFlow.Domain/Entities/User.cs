
namespace FiscalFlow.Domain;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<CfdiBatch> CfdiBatches { get; set; } = new List<CfdiBatch>();

    public virtual ICollection<Cfdi> Cfdis { get; set; } = new List<Cfdi>();

    public virtual ICollection<MassDownloadRequest> MassDownloadRequests { get; set; } = new List<MassDownloadRequest>();

    public virtual StampBalance? StampBalance { get; set; }

    public virtual ICollection<StampMovement> StampMovements { get; set; } = new List<StampMovement>();
}
