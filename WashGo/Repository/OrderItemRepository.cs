using Microsoft.EntityFrameworkCore;
using WashGo.Model;

namespace WashGo.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderItemRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int orderItemId)
        {
            return await _context.OrderItems.FindAsync(orderItemId);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems.Where(oi => oi.OrderID == orderId)
                                            .ToListAsync();
        }

        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int orderItemId)
        {
            var orderItem = await _context.OrderItems.FindAsync(orderItemId);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
