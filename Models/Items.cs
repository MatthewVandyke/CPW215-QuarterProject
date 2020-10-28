using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CPW215_QuarterProject.Models
{
	public class Item
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string ItemId { get; set; }
		public string ItemType { get; set; }

		public Item()
		{
			ItemType = this.GetType().Name;
		}

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

	public enum BookFormat
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
		[StringLength(50)]
		[Display(Name = "Author's Full Name")]
		public string AuthorFullName { get; set; }

		[StringLength(50)]
		public string Publisher { get; set; }

		[Required]
		public BookFormat BookFormat { get; set; }
	}

	public enum GameRating
	{
		Mature,
		Teen,
		Everyone
	}

	public class VideoGame : Item
	{
		public string Publisher { get; set; }

		[Required]
		public string Developer { get; set; }

		[Required]
		public GameRating GameRating { get; set; }

		[DataType(DataType.Date)]
		[Required]
		public DateTime ReleaseDate { get; set; }
	}
}
