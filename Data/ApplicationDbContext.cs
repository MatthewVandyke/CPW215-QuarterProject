using System;
using System.Collections.Generic;
using System.Text;
using CPW215_QuarterProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CPW215_QuarterProject.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public virtual DbSet<Item> Items { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Item>()
				.HasDiscriminator(item => item.ItemType)
				.HasValue<Item>(nameof(Item))
				.HasValue<Book>(nameof(Book))
				.HasValue<VideoGame>(nameof(VideoGame));

			builder.Entity<Item>()
				.Property(item => item.ItemType)
				.HasMaxLength(200)
				.HasColumnName("item_type");
		}
	}
}
