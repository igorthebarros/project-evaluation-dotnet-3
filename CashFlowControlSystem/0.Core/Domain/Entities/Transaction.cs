using Domain.Enums;
using SharedKernel.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; } = new Merchant();
        public decimal Amount { get; set; }

        [Column(TypeName = "varchar(3)")]
        public CurrencyType Currency { get; set; }

        [Column(TypeName = "varchar(6)")]
        public PaymentMethod PaymentMethod { get; set; }

        [Column(TypeName = "varchar(9)")]
        public TransactionStatus Status { get; set; }
    }
}
