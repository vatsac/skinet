using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Interfaces;
using Core.Model.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderDto orderDto)
        {
          var email = HttpContext.User.RetrieveEmailFromPrincipal();

          var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);


          var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

          if(order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));

          return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
          
            var orders = await _orderService.GetOrdersForUserAsync(email);

             var address = new Address();

           foreach(var item in orders) {
               address.FirstName = item.ShipToAddressFirstName;
               address.LastName = item.ShipToAddressLastName;
               address.Street = item.ShipToAddressStreet;
               address.State = item.ShipToAddressState;
               address.City = item.ShipToAddressCity;
               address.Zipcode = item.ShipToAddressZipcode;
           }

           var OrderToReturn = _mapper.Map<IReadOnlyList<Orders>, IReadOnlyList<OrderToReturnDto>>(orders);
        
           foreach(var item in OrderToReturn){
               item.ShipToAddress = address;
            }

            return Ok(OrderToReturn);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var order = await _orderService.GetOrderByIdAsync(id, email);

            if (order == null) return NotFound(new ApiResponse(404));

            var orderToReturnDto = _mapper.Map<Orders, OrderToReturnDto>(order);

            var address = new Address{
                FirstName = order.ShipToAddressFirstName,
               LastName = order.ShipToAddressLastName,
               Street = order.ShipToAddressStreet,
               State = order.ShipToAddressState,
               City = order.ShipToAddressCity,
               Zipcode = order.ShipToAddressZipcode
            };

            orderToReturnDto.ShipToAddress = address;

            return orderToReturnDto;
        }

        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodAsync());
        }
    }
}