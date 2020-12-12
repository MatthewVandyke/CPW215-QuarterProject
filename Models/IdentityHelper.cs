using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPW215_QuarterProject.Models
{
	public static class IdentityHelper
	{
		internal static void SetIdentityOptions(IdentityOptions options)
		{
			// Sign in options
			options.SignIn.RequireConfirmedEmail = false;
			options.SignIn.RequireConfirmedAccount = true;

			// Password strength
			options.Password.RequiredLength = 8;

			// Lockout options
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
			options.Lockout.MaxFailedAccessAttempts = 5;
		}

		internal static async Task CreateRoles(IServiceProvider provider, params string[] roles)
		{
			RoleManager<IdentityRole> roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();

			// Create role if it does not exist
			foreach (string role in roles)
			{
				bool doesRoleExist = await roleManager.RoleExistsAsync(role);
				if (!doesRoleExist)
				{
					await roleManager.CreateAsync(new IdentityRole(role));
				}
			}
		}

		internal static async Task CreateDefaultAdmin(IServiceProvider provider)
		{
			const string email = "g@email.com";
			const string username = "admin";
			const string password = "Programming02*";

			var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();

			// Check if there are any users in database
			if (userManager.Users.Count() == 0)
			{
				IdentityUser admin = new IdentityUser()
				{
					Email = email,
					UserName = username,
					EmailConfirmed = true
				};

				//Create admin
				await userManager.CreateAsync(admin, password);

				// Add to admin
				await userManager.AddToRoleAsync(admin, "Admin");
			}
		}
	}
}
