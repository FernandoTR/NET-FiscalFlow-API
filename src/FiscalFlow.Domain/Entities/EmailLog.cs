using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Table("EmailLog")]
public partial class EmailLog
{
    [Key]
    public Guid Id { get; set; }

    public Guid CfdiId { get; set; }

    [StringLength(255)]
    public string Recipient { get; set; } = null!;

    public DateTime SentAt { get; set; }

    [ForeignKey("CfdiId")]
    [InverseProperty("EmailLogs")]
    public virtual Cfdi Cfdi { get; set; } = null!;
}
