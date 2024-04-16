using FinalProject.Domain.Abstractions;
using FinalProject.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Domain.Entities
{
    [Table("Products")]
	public class Product : BaseEntity<Guid>
	{
		[Column(TypeName = "nvarchar(1000)")]
		public string Name { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public int Quantity { get; set; } = 0;

		[Column(TypeName = "ntext")]
		public string Detail { get; set; }

		[Column(TypeName = "ntext")]
		public string Description { get; set; }

		public Guid CategoryId { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? DiscountPrice { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
		public ICollection<Review> Reviews { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; }
	}
}
