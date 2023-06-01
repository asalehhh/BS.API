using Newtonsoft.Json;

namespace BS.DataLayer.Models.User
{
    /// <summary>
    /// Represents user login request model
    /// </summary>
    public class Login
    {
        /// <summary>
        ///Login username
        /// </summary>
        [JsonProperty("userName")]
        public string UserName { get; set; }

        /// <summary>
        /// Login password
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
