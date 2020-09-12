using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;
using Core.Model.OrderAggregate;
using Core.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using Product = Core.Model.Product;
using Orders = Core.Model.OrderAggregate.Orders;

namespace Infrastructure.services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        public PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _config = config;
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var basket = await _basketRepository.GetBasketAsync(basketId);

            if (basket == null) return null;

            var shippingPrice = 0m;

            if(basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>()
                     .GetByIdAsync((int)basket.DeliveryMethodId);
                shippingPrice = (decimal)deliveryMethod.Price;  
            }

            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                   Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                   Currency = "inr",
                   PaymentMethodTypes = new List<string> {"card"},
                   Description = "Software development services",
                  Shipping = new ChargeShippingOptions
                  {
                  Name = "Jenny Rosen",
                  Address = new AddressOptions
                  {
                  Line1 = "510 Townsend St",
                  Line2 = "unr",
                  PostalCode = "98140",
                  City = "San Francisco",
                  State = "CA",
                  Country = "IN",
                  },
                 },
                };
                intent = await service.CreateAsync(options);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long) basket.Items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice *100
                };
                await service.UpdateAsync(basket.PaymentIntentId, options);
            }

            await _basketRepository.UpdateBasketAsync(basket);

            return basket;
        }

        public async Task<Orders> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Orders>().GetEntityWithSpec(spec);

            if(order == null) return null;

            order.Status =OrderStatus.PaymentFailed.ToString();
            await _unitOfWork.Complete();

            return order;
        }

        public async Task<Orders> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var spec = new OrderByPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Orders>().GetEntityWithSpec(spec);

            if(order == null) return null;

            order.Status = OrderStatus.PaymentRecevied.ToString();
            _unitOfWork.Repository<Orders>().Update(order);

            await _unitOfWork.Complete();

            return order;
        }
    }
}