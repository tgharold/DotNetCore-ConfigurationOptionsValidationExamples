using Example1Api.Extensions;
using Example1Api.Services;
using Example1Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Example1Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddValidatedSettings<ConnectionStringsSettings>(_configuration)
                .AddValidatedSettings<DatabaseSettings>(_configuration)
                .AddValidatedSettings<MonitoredSettings>(_configuration)
                .AddValidatedSettings<UnmonitoredButValidatedSettings>(_configuration)
                .AddSettings<UnvalidatedSettings>(_configuration)
                ;

            services.AddSingleton<WeatherForecastService>(ctx =>
            {
                var identitySettings = ctx.GetRequiredService<IOptions<ConnectionStringsSettings>>();
                return new WeatherForecastService(identitySettings);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
