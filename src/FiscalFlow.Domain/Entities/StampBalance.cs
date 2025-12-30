using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

[Table("StampBalance")]
public partial class StampBalance
{
    [Key]
    public Guid UserId { get; set; }

    public int AvailableStamps { get; set; }

    public DateTime UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("StampBalance")]
    public virtual User User { get; set; } = null!;
}
