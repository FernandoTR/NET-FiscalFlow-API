using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Table("CfdiXml")]
public partial class CfdiXml
{
    [Key]
    public Guid Id { get; set; }

    public Guid CfdiId { get; set; }

    [Column(TypeName = "xml")]
    public string XmlContent { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    [ForeignKey("CfdiId")]
    [InverseProperty("CfdiXmls")]
    public virtual Cfdi Cfdi { get; set; } = null!;
}
