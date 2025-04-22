using SharedKernel.Common;

namespace Domain.Entities
{
    public class DailyBalance : BaseEntity
    {
        public Guid MerchantId { get; set; }
        public Merchant Merchant { get; set; } = new Merchant();
        public decimal OpeningBalance { get; set; }
        public decimal TotalCredits { get; set; }
        public decimal TotalDebits { get; set; }
        public decimal ClosingBalance { get; set; }
        public uint TransactionCount { get; set; }
    }
}
