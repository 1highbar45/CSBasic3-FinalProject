﻿using FinalProject.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Domain.Entities
{
    [Table("Reviews")]
	public class Review : BaseEntity<Guid>
	{

		[Column(TypeName = "nvarchar(1000)")]
		public string ReviewerName { get; set; }

		[Column(TypeName = "nvarchar(1000)")]
		public string? Email { get; set; }

		[Column(TypeName = "ntext")]
		public string Content { get; set; }
		public int Rating { get; set; }


		public Guid ProductId { get; set; }

		[ForeignKey(nameof(ProductId))]
		public Product Product { get; set; }

	}

}
