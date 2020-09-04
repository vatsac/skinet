using System;
using System.Collections.Generic;

namespace Core.Model.OrderAggregate
{
    public partial class DeliveryMethod
    {
        public DeliveryMethod()
        {
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
    }
}
