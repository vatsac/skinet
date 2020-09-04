using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;
using Core.Model.OrderAggregate;
using Core.Specifications;
using Infrastructure.Data;

namespace Infrastructure.services
{
    public class OrderService : IOrderService
    {

        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;

        }

        public async Task<Orders> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // get basket from basket repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // get items from the product repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                //var OrderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
                var OrderItem = new OrderItem {
                ItemOrderedProductItemId = productItem.Id,
                ItemOrderedProductName = productItem.Name,
                ItemOrderedPictureUrl = productItem.PictureUrl,
                Price = productItem.Price,
                Quantity = item.Quantity
                };
                items.Add(OrderItem);
            }

            // get delivery method from repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // calc subtotal
            var subtotal = items.Sum(item => item.Price * item.Quantity);

            // create order
            //var order = new Orders(items, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            var order = new Orders
            {
                BuyerEmail = buyerEmail,
                ShipToAddressFirstName = shippingAddress.FirstName,
                ShipToAddressLastName = shippingAddress.LastName,
                ShipToAddressStreet = shippingAddress.Street,
                ShipToAddressCity = shippingAddress.City,
                ShipToAddressState = shippingAddress.State,
                ShipToAddressZipcode = shippingAddress.Zipcode,
                DeliveryMethod = deliveryMethod.Id,
                Subtotal = subtotal,
            };
            _unitOfWork.Repository<Orders>().Add(order);


            // save to db
            var result = await _unitOfWork.Complete();
             int orderId = await _unitOfWork.Repository<Orders>().LastUpdatedIdAsync();

            if (result <= 0) {
                return null;
            }
            else {

                foreach(var item in items){
                var OrderItem = new OrderItem {
                ItemOrderedProductItemId = item.ItemOrderedProductItemId,
                ItemOrderedProductName = item.ItemOrderedProductName,
                ItemOrderedPictureUrl = item.ItemOrderedPictureUrl,
                Price = item.Price,
                Quantity = item.Quantity,
                OrderId = orderId
                };
                _unitOfWork.Repository<OrderItem>().Add(OrderItem);
                }


            }

            var ResultFromOrderItem = await _unitOfWork.Complete();

            if(ResultFromOrderItem <= 0) return null;



            // delete basket
            await _basketRepo.DeleteBasketAsync(basketId);
            // return order
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Orders> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(id,buyerEmail);

            return await _unitOfWork.Repository<Orders>().GetEntityWithSpec(spec);
 
        }

        public async Task<IReadOnlyList<Orders>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Orders>().ListAsync(spec);
        }
    }
}