using Newtonsoft.Json;
using System;

namespace CallWebSericeDemo
{
    public class WeatherObservation
    {
        [JsonProperty(PropertyName = "observation")]
        public String observation { get; set; }
        [JsonProperty(PropertyName = "stationName")]
        public String stationName { get; set; }
        [JsonProperty(PropertyName = "temperature")]
        public float temperature { get; set; }
        [JsonProperty(PropertyName = "humidity")]
        public float humidity { get; set; }
		[JsonProperty(PropertyName = "clouds")]
        public String clouds { get; set; }
		[JsonProperty(PropertyName = "weatherCondition")]
        public String weatherCondition { get; set; }
    }
    public class Weather
	{
		public WeatherObservation weatherObservation;

	}
}

