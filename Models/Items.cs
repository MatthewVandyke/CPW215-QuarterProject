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

	public enum BookType
	{
		Hardcover,
		Paperback
	}

	public class Book : Item
	{
		// Regex for ISBN-10 and ISBN-13
		[RegularExpression(@"^(97(8|9))?\d{9}(\d|X)$")]
		[Required]
		public string ISBN { get; set; }

		[Required]
		[Display(Name = "Author's Full Name")]
		public string AuthorFullName { get; set; }

		[Required]
		public BookType BookType { get; set; }
	}
}
