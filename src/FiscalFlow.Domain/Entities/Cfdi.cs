using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

[Table("Cfdi")]
[Index("Uuid", Name = "UQ__Cfdi__BDA103F437D13CE2", IsUnique = true)]
public partial class Cfdi
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid Uuid { get; set; }

    [StringLength(13)]
    public string RfcEmisor { get; set; } = null!;

    [StringLength(13)]
    public string RfcReceptor { get; set; } = null!;

    [StringLength(5)]
    public string Version { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Total { get; set; }

    [StringLength(5)]
    public string Currency { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    [StringLength(30)]
    public string CurrentStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [InverseProperty("Cfdi")]
    public virtual ICollection<BatchItem> BatchItems { get; set; } = new List<BatchItem>();

    [InverseProperty("Cfdi")]
    public virtual ICollection<CfdiPdf> CfdiPdfs { get; set; } = new List<CfdiPdf>();

    [InverseProperty("Cfdi")]
    public virtual ICollection<CfdiStatusHistory> CfdiStatusHistories { get; set; } = new List<CfdiStatusHistory>();

    [InverseProperty("Cfdi")]
    public virtual ICollection<CfdiXml> CfdiXmls { get; set; } = new List<CfdiXml>();

    [InverseProperty("Cfdi")]
    public virtual ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();

    [InverseProperty("Cfdi")]
    public virtual ICollection<StampMovement> StampMovements { get; set; } = new List<StampMovement>();

    [ForeignKey("UserId")]
    [InverseProperty("Cfdis")]
    public virtual User User { get; set; } = null!;
}
