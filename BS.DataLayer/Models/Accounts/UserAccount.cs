using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS.DataLayer.Models.Accounts
{
    /// <summary>
    /// Represents User account class
    /// </summary>
    public class UserAccount : BaseModel
    {
        /// <summary>
        /// Foreign key to user profile table
        /// </summary>
        [ForeignKey("UserProfile")]
        [JsonProperty("userProfileId")]
        public int UserProfileId { get; set; }

        /// <summary>
        /// Total balance
        /// </summary>
        [JsonProperty("initialBalance")]
        public decimal InitialBalance { get; set; }

        /// <summary>
        /// Total balance
        /// </summary>
        [JsonProperty("totalBalance")]
        public decimal TotalBalance { get; set; }
    }
}
