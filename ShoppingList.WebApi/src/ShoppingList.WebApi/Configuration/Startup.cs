using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingList.WebApi.Repositories;
using ShoppingList.WebApi.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace ShoppingList.WebApi.Configuration
{
    public sealed class Startup
    {
        private const string CorsPolicyAllowAll = "AllowAll";
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            var ShoppingListDbConnectionString = _configuration.GetConnectionString("ShoppingListDb");
            services.AddDbContext<ShoppingListDbContext>(o => o.UseSqlServer(ShoppingListDbConnectionString));

            services.AddTransient<IItemService, ItemService>();

            if (_environment.IsDevelopment())
            { 
                services.AddMvc();
            }
            else
            {
                services.AddMvc(options =>
                {
                    options.Filters.Add(new RequireHttpsAttribute());
                });
            }

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "ShoppingList Api", Version = "v1" });
                options.DescribeAllEnumsAsStrings();
            });

            services.AddCors(options => options.AddPolicy(CorsPolicyAllowAll, p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseCors(CorsPolicyAllowAll);
            app.UseHttpStatusCodeExceptionMiddleware();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "ShoppingList Api v1");
            });

            app.InitializeDatabase().Wait();
        }
    }
}
