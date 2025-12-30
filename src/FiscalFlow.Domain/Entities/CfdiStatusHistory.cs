using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Table("CfdiStatusHistory")]
public partial class CfdiStatusHistory
{
    [Key]
    public Guid Id { get; set; }

    public Guid CfdiId { get; set; }

    [StringLength(30)]
    public string Status { get; set; } = null!;

    public DateTime ChangedAt { get; set; }

    [ForeignKey("CfdiId")]
    [InverseProperty("CfdiStatusHistories")]
    public virtual Cfdi Cfdi { get; set; } = null!;
}
