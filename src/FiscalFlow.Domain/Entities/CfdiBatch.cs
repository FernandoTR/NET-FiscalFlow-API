using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Table("CfdiBatch")]
public partial class CfdiBatch
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [StringLength(30)]
    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [InverseProperty("Batch")]
    public virtual ICollection<BatchItem> BatchItems { get; set; } = new List<BatchItem>();

    [ForeignKey("UserId")]
    [InverseProperty("CfdiBatches")]
    public virtual User User { get; set; } = null!;
}
