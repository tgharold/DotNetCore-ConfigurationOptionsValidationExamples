using System;
using System.Collections.Generic;
using System.Linq;
using Example3ApiAutofac.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Example3ApiAutofac.Controllers
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
        private UnvalidatedOptions _unvalidatedOptions;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptionsSnapshot<DatabaseOptions> databaseOptionsAccessor,
            IOptions<UnvalidatedOptions> unvalidatedOptionsAccessor
            )
        {
            _logger = logger;

            _unvalidatedOptions = unvalidatedOptionsAccessor.Value;
            
            /* if IOptions validation fails, code will error out here (first access)
             * example:
             *   - OptionsValidationException: Comment1 is required.; DatabaseType is required.; Comment2 is required.;
             *     SchemaNames.Schema1 is required.; SchemaNames.Schema2 is required.
             */
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
