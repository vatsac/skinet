using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core.Model.OrderAggregate
{
    public partial class Orders
    {
        
        public Orders()
        {
            OrderItem = new HashSet<OrderItem>();
        }

/*         public Orders(IReadOnlyList<OrderItem> orderItems, string buyerEmail, Address shipToAddress, DeliveryMethod DM, decimal? subtotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            ShipToAddressFirstName = shipToAddress.FirstName;
            ShipToAddressLastName = shipToAddress.LastName;
            ShipToAddressStreet = shipToAddress.Street;
            ShipToAddressCity = shipToAddress.City;
            ShipToAddressState = shipToAddress.State;
            ShipToAddressZipcode = shipToAddress.Zipcode;
            DeliveryMethod = DM.Id;
            Subtotal = subtotal;
            OrderItems = orderItems;
             DetailOfDeliveryMethod =DM;
            ShipToAddress =shipToAddress;
        }  */


        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string ShipToAddressFirstName { get; set; }
        public string ShipToAddressLastName { get; set; }
        public string ShipToAddressStreet { get; set; }
        public string ShipToAddressCity { get; set; }
        public string ShipToAddressState { get; set; }
        public string ShipToAddressZipcode { get; set; }

        public int DeliveryMethod { get; set; }
        public decimal? Subtotal { get; set; }
        public string Status { get; set; }
        public string PaymentIntenId { get; set; }

        // public  Address ShipToAddress { get; set; }
        //public  DeliveryMethod DetailOfDeliveryMethod { get; set; }
        //public  IReadOnlyList<OrderItem> OrderItems { get; set; }



        public virtual DeliveryMethod DeliveryMethodNavigation { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
