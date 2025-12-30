using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Domain;

[Table("StampMovement")]
public partial class StampMovement
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid? CfdiId { get; set; }

    [StringLength(20)]
    public string MovementType { get; set; } = null!;

    public int Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("CfdiId")]
    [InverseProperty("StampMovements")]
    public virtual Cfdi? Cfdi { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("StampMovements")]
    public virtual User User { get; set; } = null!;
}
