using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS.DataLayer.Models.User
{
    /// <summary>
    /// Represents User profile class
    /// </summary>
    public class UserProfile : BaseModel
    {

        /// <summary>
        /// Foreign key to identity user table
        /// </summary>
        [ForeignKey("IdentityUser")]
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
    }
}
