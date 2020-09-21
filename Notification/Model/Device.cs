using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notification.Model
{
    public class Device
    {
        [JsonProperty(PropertyName ="id")]
        public string DeviceId { get; set; }
        public double SystolicBloodPressure { get; set; }
        public double DiastolicBloodPressure { get; set; }
        public double BodyTemperatureInFahrenheit { get; set; }
        public double DeviceTemperatureInFahrenheit { get; set; }
        public int HeartRate { get; set; }
        public int HeartRateVariability { get; set; }
        public int RespiratoryRate { get; set; }
        public DateTime GeneratedTime { get; set; }

        public static explicit operator Task<object>(Device v)
        {
            throw new NotImplementedException();
        }
    }
}
