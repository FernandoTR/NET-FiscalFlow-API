using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FiscalFlow.Infrastructure;

[Index("Token", Name = "UQ__AuthToke__1EB4F8179ABCEE5B", IsUnique = true)]
public partial class AuthToken
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [StringLength(500)]
    public string Token { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AuthTokens")]
    public virtual User User { get; set; } = null!;
}
