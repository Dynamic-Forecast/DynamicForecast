using Newtonsoft.Json;

namespace DynamicForecast.Models
{
    public class CaptchaVerification
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
