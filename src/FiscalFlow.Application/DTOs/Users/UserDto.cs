
namespace FiscalFlow.Application.DTOs.Users;

public record UserDto(
    Guid Id,
    string Email,
    bool IsActive,
    DateTime CreatedAt
);

