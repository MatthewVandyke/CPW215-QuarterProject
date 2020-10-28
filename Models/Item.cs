using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CPW215_QuarterProject.Models
{
	public class Item
	{
		[Key]
		public string ItemId { get; set; }

		public IdentityUser Seller { get; set; }

		[StringLength(100)]
		[Required]
		public string Name { get; set; }

		[DataType(DataType.Currency)]
		[Required]
		public double Price { get; set; }

		[StringLength(500)]
		public string Description { get; set; }
	}
}
