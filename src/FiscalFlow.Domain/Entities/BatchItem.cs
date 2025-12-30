using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Table("BatchItem")]
public partial class BatchItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid BatchId { get; set; }

    public Guid CfdiId { get; set; }

    [StringLength(30)]
    public string Status { get; set; } = null!;

    [ForeignKey("BatchId")]
    [InverseProperty("BatchItems")]
    public virtual CfdiBatch Batch { get; set; } = null!;

    [ForeignKey("CfdiId")]
    [InverseProperty("BatchItems")]
    public virtual Cfdi Cfdi { get; set; } = null!;
}
