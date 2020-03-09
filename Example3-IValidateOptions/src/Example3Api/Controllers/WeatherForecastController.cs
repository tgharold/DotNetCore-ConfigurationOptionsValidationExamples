using System;
using System.Collections.Generic;
using System.Linq;
using Example3Api.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Example3Api.Controllers
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
        private readonly MonitoredSettings _monitoredSettings;
        private readonly UnmonitoredButValidatedSettings _unmonitoredButValidatedSettings;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptionsSnapshot<DatabaseSettings> databaseSettingsAccessor,
            IOptions<UnvalidatedSettings> unvalidatedSettingsAccessor,
            IOptionsMonitor<MonitoredSettings> monitoredSettingsAccessor,
            IOptions<UnmonitoredButValidatedSettings> unmonitoredButValidatedAccessor
            )
        {
            _logger = logger;

            /* If options-pattern validation fails, code will error out here.  However, it will error out on the
             * first object that fails to validate.  You could wrap a try-catch around all of the calls.
             * 
             * example:
             *   - OptionsValidationException: Comment1 is required.; DatabaseType is required.; Comment2 is required.;
             *     SchemaNames.Schema1 is required.; SchemaNames.Schema2 is required.
             */
            
            // There's no validator for this one.
            _unvalidatedSettings = unvalidatedSettingsAccessor.Value;
            
            // IOptions<T> is a singleton that only validates on the first-use (anywhere).
            _unmonitoredButValidatedSettings = unmonitoredButValidatedAccessor.Value;
            
            // IOptionsMonitor<T> only runs when the underlying file has actually changed (still a singleton).
            _monitoredSettings = monitoredSettingsAccessor.CurrentValue;

            // IOptionsSnapshot<T> runs validation on every new request.
            _databaseSettings = databaseSettingsAccessor.Value;
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
