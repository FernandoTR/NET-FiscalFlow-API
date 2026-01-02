namespace FiscalFlow.Application.DTOs.Notifications;

public record EmailLogDto(
    Guid CfdiId,
    string Email,
    DateTime SentAt
);

