using API.Dtos;
using AutoMapper;
using Core.Model;
using Core.Model.Identity;
using Core.Model.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                 .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                 .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                 .ForMember( d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Core.Model.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Model.OrderAggregate.Address>();
            CreateMap<Orders, OrderToReturnDto>()
                   .ForMember(d => d.DeliveryMethod,o => o.MapFrom(s =>s.DeliveryMethodNavigation.ShortName))
                   .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethodNavigation.Price))
                   .ForMember(d => d.Total, o=> o.MapFrom(s => s.DeliveryMethodNavigation.Price + s.Subtotal))
                   .ForMember(d => d.OrderItems, o=> o.MapFrom(s => s.OrderItem) );
                  //.ForMember(d => d.ShipToAddress, o=>o.MapFrom(s => s.ShipToAddressFirstName))
                   //.ForMember(d => d.ShipToAddress, o=>o.MapFrom(s => s.ShipToAddressLastName))
                   //.ForMember(d => d.ShipToAddress, o=>o.MapFrom(s => s.ShipToAddressLastName))
                   //.ForMember(d => d.ShipToAddress, o=>o.MapFrom(s => s.ShipToAddressCity))
                   //.ForMember(d => d.ShipToAddress, o=>o.MapFrom(s => s.ShipToAddressState))
                   //.ForMember(d => d.ShipToAddress, o=>o.MapFrom(s => s.ShipToAddressZipcode));

            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrderedProductItemId))
                    .ForMember(d => d.ProductName, o=> o.MapFrom(s =>s.ItemOrderedProductName))
                    .ForMember(d => d.PictureUrl, o=> o.MapFrom(s => s.ItemOrderedPictureUrl))
                    .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResolver>()); 
        }
    }
}