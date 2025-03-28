using Microsoft.EntityFrameworkCore;
using WashGo.Model;

namespace WashGo.Repository
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDBContext _context;

        public DeliveryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int deliveryId)
        {
            return await _context.Deliveries.FindAsync(deliveryId);
        }

        public async Task<IEnumerable<Delivery>> GetDeliveriesByDriverIdAsync(int deliveryPersonId)
        {
            return await _context.Deliveries.Where(d => d.DeliveryPersonID == deliveryPersonId)
                                            .ToListAsync();
        }

        public async Task<IEnumerable<Delivery>> GetAllDeliveriesAsync()
        {
            return await _context.Deliveries.ToListAsync();
        }

        public async Task AddDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeliveryAsync(int deliveryId)
        {
            var delivery = await _context.Deliveries.FindAsync(deliveryId);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
    }
}
