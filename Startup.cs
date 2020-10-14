using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using CPW215_QuarterProject.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CPW215_QuarterProject
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));
			services.AddDefaultIdentity<IdentityUser>(SetIdentityOptions)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			services.AddControllersWithViews();
			services.AddRazorPages();
		}


		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
				endpoints.MapRazorPages();
			});

			// Create roles here
			IServiceScope serviceProvider = app.ApplicationServices
									.GetRequiredService<IServiceProvider>().CreateScope();
			CreateRoles(serviceProvider.ServiceProvider, "Admin", "Seller", "RegUser").Wait();
			CreateDefaultAdmin(serviceProvider.ServiceProvider).Wait();
		}

		private static void SetIdentityOptions(IdentityOptions options)
		{
			// Sign in options
			options.SignIn.RequireConfirmedEmail = false;

			// Password strength
			options.Password.RequiredLength = 8;

			// Lockout options
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
			options.Lockout.MaxFailedAccessAttempts = 5;
		}

		private static async Task CreateRoles(IServiceProvider provider, params string[] roles)
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

		private static async Task CreateDefaultAdmin(IServiceProvider provider)
		{
			const string email = "g@email.com";
			const string username = "admin";
			const string password = "Programming02*";

			var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();

			// Check if there are any users in database
			if(userManager.Users.Count() == 0)
			{
				IdentityUser admin = new IdentityUser()
				{
					Email = email,
					UserName = username
				};

				//Create admin
				await userManager.CreateAsync(admin, password);

				// Add to admin
				await userManager.AddToRoleAsync(admin, "Admin");
			}
		}
	}
}
