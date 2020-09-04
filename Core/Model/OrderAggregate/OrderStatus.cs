using System.Runtime.Serialization;

namespace Core.Model.OrderAggregate
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Payment Received")]

        PaymentRecevied,

    
        [EnumMember(Value = "Payment Failed")]
        PaymentFailed
    }
}