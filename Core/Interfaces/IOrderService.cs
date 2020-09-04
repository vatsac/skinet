using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Model.OrderAggregate;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<Orders> CreateOrderAsync(string buyerEmail, int deliveryMethod, string basketId, Address shippingAddress); 

        Task<IReadOnlyList<Orders>> GetOrdersForUserAsync(string buyerEmail);

        Task<Orders> GetOrderByIdAsync(int id, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
    }
}