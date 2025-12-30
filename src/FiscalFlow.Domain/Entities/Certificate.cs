using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

public partial class Certificate
{
    [Key]
    public Guid CertificateId { get; set; }

    public Guid UserId { get; set; }

    [StringLength(13)]
    public string Rfc { get; set; } = null!;

    [StringLength(50)]
    public string SerialNumber { get; set; } = null!;

    [StringLength(20)]
    public string CertificateType { get; set; } = null!;

    public DateOnly ValidFrom { get; set; }

    public DateOnly ValidTo { get; set; }

    public byte[] CerFile { get; set; } = null!;

    public byte[] KeyFile { get; set; } = null!;

    public byte[] EncryptedKeyPassword { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Certificates")]
    public virtual User User { get; set; } = null!;
}
