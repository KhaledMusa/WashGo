using WashGo.Model;

namespace WashGo.Repository
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int orderId);
    }
}
