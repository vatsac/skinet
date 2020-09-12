using System;
using System.Linq.Expressions;
using Core.Model.OrderAggregate;

namespace Core.Specifications
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Orders>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId) : base(o => o.PaymentIntenId == paymentIntentId)
        {
        }
    }
}