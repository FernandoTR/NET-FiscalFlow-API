using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Table("CfdiPdf")]
public partial class CfdiPdf
{
    [Key]
    public Guid Id { get; set; }

    public Guid CfdiId { get; set; }

    [StringLength(500)]
    public string FilePath { get; set; } = null!;

    public int Version { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("CfdiId")]
    [InverseProperty("CfdiPdfs")]
    public virtual Cfdi Cfdi { get; set; } = null!;
}
