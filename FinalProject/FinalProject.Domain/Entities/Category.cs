using FinalProject.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Domain.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity<Guid>
    {
        [Column(TypeName = "nvarchar(1000)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        public string Image { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
