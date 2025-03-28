using Microsoft.EntityFrameworkCore;
using WashGo.Model;

namespace WashGo.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDBContext _context;

        public PaymentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            return await _context.Payments.FindAsync(paymentId);
        }

        public async Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(int orderId)
        {
            return await _context.Payments.Where(p => p.OrderID == orderId)
                                          .ToListAsync();
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePaymentAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
