using BS.DataLayer.Models.Accounts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BS.DataLayer.Models.Transactions
{
    /// <summary>
    /// Represents User account transactions class
    /// </summary>
    public class AccountTransaction : BaseModel
    {
        /// <summary>
        /// Foreign key to user account table
        /// </summary>
        [ForeignKey("UserAccount")]
        [JsonProperty("accountId")]
        public int AccountId { get; set; }

        /// <summary>
        /// Transaction date
        /// </summary>
        [JsonProperty("recordDate")]
        public DateTime RecordDate { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Record details description
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual UserAccount UserAccount { get; set; }
    }
}
