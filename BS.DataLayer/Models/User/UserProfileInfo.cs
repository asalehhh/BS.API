using Newtonsoft.Json;

namespace BS.DataLayer.Models.User
{
    /// <summary>
    /// Represents user profile with account info
    /// </summary>
    public class UserProfileInfo
    {
        /// <summary>
        /// Foreign key to identity user table
        /// </summary>
        [JsonProperty("identityUserId")]
        public string IdentityUserId { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// User last name
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("hasAccount")]
        public int HasAccount { get; set; }


        [JsonProperty("totalBalance")]
        public decimal TotalBalance { get; set; }


      

    }
}
