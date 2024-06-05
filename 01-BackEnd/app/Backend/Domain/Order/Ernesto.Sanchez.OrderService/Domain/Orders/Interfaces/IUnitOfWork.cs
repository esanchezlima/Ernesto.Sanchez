namespace Ernesto.Sanchez.OrderService.Domain.Orders.Interfaces
{
    public interface IUnitOfWork
    {
        void Dispose();
        Task<bool> SaveAsync();
    }
}