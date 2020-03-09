using System;
using System.Diagnostics;
using System.Linq;
using Example1Api.Models;
using Example1Api.Settings;
using Microsoft.Extensions.Options;

namespace Example1Api.Services
{
    public class WeatherForecastService
    {
        private ConnectionStringsSettings _connectionStringsSettings;
        private static readonly Random Random = new Random();
        
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastService(
            IOptions<ConnectionStringsSettings> connectionStringsAccessor
            )
        {
            _connectionStringsSettings = connectionStringsAccessor.Value;
            
            Debug.WriteLine($"Conn1: {_connectionStringsSettings.Connection1}");
        }

        public WeatherForecast[] GetForecast()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Next(-20, 55),
                Summary = Summaries[Random.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}