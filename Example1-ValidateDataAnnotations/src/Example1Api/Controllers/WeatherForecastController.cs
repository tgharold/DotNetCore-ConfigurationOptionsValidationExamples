using System.Collections.Generic;
using System.Diagnostics;
using Example1Api.Models;
using Example1Api.Services;
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


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastService _weatherForecastService;

        private readonly DatabaseSettings _databaseSettings;
        private readonly UnvalidatedSettings _unvalidatedSettings;
        private readonly MonitoredSettings _monitoredSettings;
        private readonly UnmonitoredButValidatedSettings _unmonitoredButValidatedSettings;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptionsSnapshot<DatabaseSettings> databaseSettingsAccessor,
            IOptions<UnvalidatedSettings> unvalidatedSettingsAccessor,
            IOptionsMonitor<MonitoredSettings> monitoredSettingsAccessor,
            IOptions<UnmonitoredButValidatedSettings> unmonitoredButValidatedSettingsAccessor,
            WeatherForecastService weatherForecastService
            )
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;

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
            _unmonitoredButValidatedSettings = unmonitoredButValidatedSettingsAccessor.Value;
            
            // IOptionsMonitor<T> only runs when the underlying file has actually changed (still a singleton).
            _monitoredSettings = monitoredSettingsAccessor.CurrentValue;

            // IOptionsSnapshot<T> runs validation on every new request.
            _databaseSettings = databaseSettingsAccessor.Value;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            Debug.WriteLine($"Unvalidated Parameter A: '{_unvalidatedSettings.ParameterA}'.");
            Debug.WriteLine($"Unmonitored but Validated Option A: '{_unmonitoredButValidatedSettings.OptionA}'.");
            Debug.WriteLine($"Monitored A: '{_monitoredSettings.MonitorA}'.");
            Debug.WriteLine($"Database Type: '{_databaseSettings.DatabaseType}'.");
            Debug.WriteLine($"Database Schema 1: '{_databaseSettings.SchemaNames.Schema1}'.");
            
            return _weatherForecastService.GetForecast();
        }
    }
}
