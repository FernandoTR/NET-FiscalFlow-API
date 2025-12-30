using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

[Table("MassDownloadRequest")]
public partial class MassDownloadRequest
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateOnly PeriodStart { get; set; }

    public DateOnly PeriodEnd { get; set; }

    [StringLength(30)]
    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [InverseProperty("Request")]
    public virtual ICollection<MassDownloadFile> MassDownloadFiles { get; set; } = new List<MassDownloadFile>();

    [ForeignKey("UserId")]
    [InverseProperty("MassDownloadRequests")]
    public virtual User User { get; set; } = null!;
}
