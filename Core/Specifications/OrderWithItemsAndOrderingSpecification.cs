using Core.Model.OrderAggregate;

namespace Core.Specifications
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<Orders>
    {
        public OrderWithItemsAndOrderingSpecification(string email) : base(o => o.BuyerEmail == 
        email)
        {
            AddInclude(o => o.OrderItem);
            AddInclude(o => o.DeliveryMethodNavigation);
            AddOrderByDescending(o => o.OrderDate);
        }

        public OrderWithItemsAndOrderingSpecification(int id, string email)
            : base(o => o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o => o.OrderItem);
            AddInclude(o => o.DeliveryMethodNavigation);
        }
    }
}