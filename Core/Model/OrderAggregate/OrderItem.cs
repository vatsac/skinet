using System;
using System.Collections.Generic;

namespace Core.Model.OrderAggregate
{
    public partial class OrderItem
    {
/*         public OrderItem()
        {
        }

        public OrderItem(ProductItemOrdered itemOrdered, decimal price, int quantity)
        {
            ItemOrdered = itemOrdered;
            Price = price;
            Quantity = quantity;
        } */

        public int Id { get; set; }
        public int? ItemOrderedProductItemId { get; set; }
        public string ItemOrderedProductName { get; set; }
        public string ItemOrderedPictureUrl { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int OrderId { get; set; }

        //public ProductItemOrdered ItemOrdered { get; set; }


        public virtual Orders Order { get; set; }
    }
}
