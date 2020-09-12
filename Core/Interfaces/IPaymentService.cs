using System.Threading.Tasks;
using Core.Model;
using Core.Model.OrderAggregate;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
         Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
         Task<Orders> UpdateOrderPaymentSucceeded(string paymentIntentId);
         Task<Orders> UpdateOrderPaymentFailed(string paymentIntentId);
         
    }
}    