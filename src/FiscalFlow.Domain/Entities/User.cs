using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

[Index("Username", Name = "UQ__Users__536C85E45AF31EEF", IsUnique = true)]
public partial class User
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(100)]
    public string Username { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AuthToken> AuthTokens { get; set; } = new List<AuthToken>();

    [InverseProperty("User")]
    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    [InverseProperty("User")]
    public virtual ICollection<CfdiBatch> CfdiBatches { get; set; } = new List<CfdiBatch>();

    [InverseProperty("User")]
    public virtual ICollection<Cfdi> Cfdis { get; set; } = new List<Cfdi>();

    [InverseProperty("User")]
    public virtual ICollection<MassDownloadRequest> MassDownloadRequests { get; set; } = new List<MassDownloadRequest>();

    [InverseProperty("User")]
    public virtual StampBalance? StampBalance { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<StampMovement> StampMovements { get; set; } = new List<StampMovement>();
}
