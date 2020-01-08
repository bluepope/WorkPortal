using System;
using System.IO;

namespace BluePope.WorkPortal.Data
{
    public class WeatherForecast
    {
        public bool isEdit { get; set; } = false;
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }

        public FileStream File1 { get; set; }
    }
}
