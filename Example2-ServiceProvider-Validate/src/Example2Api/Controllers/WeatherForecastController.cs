using System;
using System.Collections.Generic;
using System.Linq;
using Example2Api.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Example2Api.Controllers
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
        
        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptions<DatabaseOptions> databaseOptionsAccessor
            )
        {
            _logger = logger;

            /* if IOptions validation fails, code will error out here (first access)
             * example:
             *   - OptionsValidationException: A validation error has occured.
             *   - OptionsValidationException: custom error
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
