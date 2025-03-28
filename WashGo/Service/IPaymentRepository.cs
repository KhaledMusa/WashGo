using WashGo.Model;

namespace WashGo.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByIdAsync(int paymentId);
        Task<IEnumerable<Payment>> GetPaymentsByOrderIdAsync(int orderId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(int paymentId);
    }
}
