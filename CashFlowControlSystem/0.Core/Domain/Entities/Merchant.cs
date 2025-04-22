using Domain.Enums;
using SharedKernel.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Merchant : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "varchar(4)")]
        public TaxType TaxId { get; set; }
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public ICollection<DailyBalance> DailyBalances { get; set;} = new List<DailyBalance>();
    }
}
