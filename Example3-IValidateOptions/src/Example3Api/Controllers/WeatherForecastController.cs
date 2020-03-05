using System;
using System.Collections.Generic;
using System.Linq;
using Example3Api.Options;
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

        private readonly DatabaseOptions _databaseOptions;
        private readonly UnvalidatedOptions _unvalidatedOptions;
        private readonly MonitoredSettingsOptions _monitoredSettingsOptions;
        private readonly UnmonitoredButValidatedOptions _unmonitoredButValidatedOptionsAccessor;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptionsSnapshot<DatabaseOptions> databaseOptionsAccessor,
            IOptions<UnvalidatedOptions> unvalidatedOptionsAccessor,
            IOptionsMonitor<MonitoredSettingsOptions> monitoredSettingsOptionsAccessor,
            IOptions<UnmonitoredButValidatedOptions> unmonitoredButValidatedOptionsAccessor
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
            _unvalidatedOptions = unvalidatedOptionsAccessor.Value;
            
            // IOptions<T> is a singleton that only validates on the first-use (anywhere).
            _unmonitoredButValidatedOptionsAccessor = unmonitoredButValidatedOptionsAccessor.Value;
            
            // IOptionsMonitor<T> only runs when the underlying file has actually changed (still a singleton).
            _monitoredSettingsOptions = monitoredSettingsOptionsAccessor.CurrentValue;

            // IOptionsSnapshot<T> runs validation on every new request.
            _databaseOptions = databaseOptionsAccessor.Value;
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
