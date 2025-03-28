using WashGo.Model;

namespace WashGo.Repository
{
    public interface IDeliveryRepository
    {
        Task<Delivery> GetDeliveryByIdAsync(int deliveryId);
        Task<IEnumerable<Delivery>> GetDeliveriesByDriverIdAsync(int deliveryPersonId);
        Task<IEnumerable<Delivery>> GetAllDeliveriesAsync();
        Task AddDeliveryAsync(Delivery delivery);
        Task UpdateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(int deliveryId);
    }
}
