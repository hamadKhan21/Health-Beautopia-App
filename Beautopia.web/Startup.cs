using Beautopia.Services.DataAppService;
using Beautopia.Services.IDataService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

namespace Beautopia.web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
			//var contentRoot = env.ContentRootPath;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.Configure(options => options.ValueCountLimit = 4096);
			//services.Configure(options => options.ValueCountLimit = 4096);
			services.AddControllersWithViews();
			services.AddRazorPages();
			//services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddMvc(options => options.EnableEndpointRouting = false);

			//services.Configure<FormOptions>(x =>
			//{
			//	x.ValueLengthLimit =Int32.MaxValue;
			//	//x.MultipartBodyLengthLimit = Int64.MaxValue; // In case of multipart
			//});
			//services.Configure(options => options.ValueCountLimit = 4096);

			services.AddSession(options =>
			{

				options.IdleTimeout = TimeSpan.FromDays(90);

			});
		
			//Add Servies here
			services.AddSingleton<IFileProvider>(
	new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin-custom/Images/SubServices"))
);

			
			services.AddScoped<IServiceRequest, ServiceRequestAppService>();
			services.AddScoped<IUsers, UsersAppService>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSession();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");

				endpoints.MapAreaControllerRoute(
					  name: "Admin",
				  areaName: "Admin",
				  pattern: "Admin/{controller=Accounts}/{action=Login}");
				endpoints.MapRazorPages();
			});

			//app.UseMvc(routes =>
			//{
			//	routes.MapRoute(
			//	  name: "areas",
			//	  template: "{area:exists}/{controller=Accounts}/{action=Login}/{id?}"
			//	);
			//});




		}
	}
}
