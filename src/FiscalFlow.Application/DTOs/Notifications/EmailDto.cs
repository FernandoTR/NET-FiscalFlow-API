namespace FiscalFlow.Application.DTOs.Notifications;

public record EmailDto(
    string To,
    string Subject,
    string Body
);

