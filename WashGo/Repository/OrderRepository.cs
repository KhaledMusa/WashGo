using Microsoft.EntityFrameworkCore;
using WashGo.Model;

namespace WashGo.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;

        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                        .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders.Where(o => o.UserID == userId)
                                        .Include(o => o.OrderItems)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                        .ToListAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
