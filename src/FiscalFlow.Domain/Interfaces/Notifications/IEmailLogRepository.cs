using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Notifications;

public interface IEmailLogRepository
{
    Task AddAsync(EmailLog log);
}
