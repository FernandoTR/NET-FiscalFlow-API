using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

[Table("MassDownloadFile")]
public partial class MassDownloadFile
{
    [Key]
    public Guid Id { get; set; }

    public Guid RequestId { get; set; }

    public Guid Uuid { get; set; }

    [StringLength(500)]
    public string FilePath { get; set; } = null!;

    [ForeignKey("RequestId")]
    [InverseProperty("MassDownloadFiles")]
    public virtual MassDownloadRequest Request { get; set; } = null!;
}
