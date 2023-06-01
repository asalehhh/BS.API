using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BS.DataLayer.Models
{
    /// <summary>
    /// Represents base model class with common properties for each class
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// Id - PK - Identity
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
