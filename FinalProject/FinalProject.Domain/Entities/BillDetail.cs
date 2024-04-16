using FinalProject.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Domain.Entities
{
    [Table("BillDetails")]
    public class BillDetail : BaseEntity<Guid>
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public Guid BillId { get; set; }

        [ForeignKey(nameof(BillId))]
        public Bill Bill { get; set; }
    }
}
