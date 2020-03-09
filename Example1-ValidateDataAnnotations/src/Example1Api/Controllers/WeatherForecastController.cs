using System;
using System.Collections.Generic;
using System.Linq;
using Example1Api.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Example1Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly DatabaseSettings _databaseSettings;
        private readonly UnvalidatedSettings _unvalidatedSettings;
        private readonly MonitoredSettingsSettings _monitoredSettingsSettings;
        private readonly UnmonitoredButValidatedSettings _unmonitoredButValidatedSettings;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptionsSnapshot<DatabaseSettings> databaseSettingsAccessor,
            IOptions<UnvalidatedSettings> unvalidatedSettingsAccessor,
            IOptionsMonitor<MonitoredSettingsSettings> monitoredSettingsSettingsAccessor,
            IOptions<UnmonitoredButValidatedSettings> unmonitoredButValidatedSettingsAccessor
            )
        {
            _logger = logger;
            
            /* if IOptions validation fails, code will error out here (first access)
             * example:
             *   DataAnnotation validation failed for members: 'DatabaseType' with the
             *   error: 'The DatabaseType field is required.'.
             */

            
            // IOptionsSnapshot<T> will revalidate on every request
            _databaseSettings = databaseSettingsAccessor.Value;
            
            // No validation for this
            _unvalidatedSettings = unvalidatedSettingsAccessor.Value;

            // Validation will occur once on first access, then again every on each change of the configuration
            _monitoredSettingsSettings = monitoredSettingsSettingsAccessor.CurrentValue;
            
            // Validation will occur once on first access
            _unmonitoredButValidatedSettings = unmonitoredButValidatedSettingsAccessor.Value;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
