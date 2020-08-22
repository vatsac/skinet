using System;
using System.Collections.Generic;

namespace Core.Model
{
    public partial class ProductBrand
    {
        public ProductBrand()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
