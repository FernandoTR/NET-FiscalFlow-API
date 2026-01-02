
namespace FiscalFlow.Application.DTOs.Users;

public record UpdateUserDto(
    string? Email,
    bool? IsActive
);

