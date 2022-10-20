using EcommerceAPI.Shared;
using Serilog;

namespace EcommerceAPI
{
    public class Startup
    {
        public IConfiguration _config { get; }
        public IConfigurationRoot clientConfig { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;

            clientConfig = new ConfigurationBuilder()
                                .SetBasePath(env.ContentRootPath)
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                                .AddJsonFile("clients.json", optional: true, reloadOnChange: true)
                                .AddEnvironmentVariables()
                                .Build();

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors((options =>
            {
                options.AddPolicy(AppConstants.CorsPolicyName,
                    builder => builder.WithOrigins(_config.GetSection(AppConstants.AllowedClientDomain).Value)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> _logger)
        {
            AppDomain.CurrentDomain.SetData(AppConstants.DataFder, System.IO.Path.Combine(env.ContentRootPath, AppConstants.AppData));
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AppConstants.CorsPolicyName);
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
