using FiscalFlow.Domain.Entities;

namespace FiscalFlow.Domain.Interfaces.Stamping;

public interface IStampMovementRepository
{
    Task AddAsync(StampMovement movement);
}
