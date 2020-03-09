using Example3Api.Extensions;
using Example3Api.Settings;
using Example3Api.Settings.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Example3Api
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
                .AddValidatedSettings<ConnectionStringsSettings, ConnectionStringsSettingsValidator>(_configuration)
                .AddValidatedSettings<DatabaseSettings, DatabaseSettingsValidator>(_configuration)
                .AddValidatedSettings<MonitoredSettings, MonitoredSettingsValidator>(_configuration)
                .AddValidatedSettings<UnmonitoredButValidatedSettings, UnmonitoredButValidatedSettingsValidator>(_configuration)
                .AddSettings<UnvalidatedSettings>(_configuration)
                ;
          
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
