using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharpPok.Database.Model
{
    public class Package
    {
        [Key]
        [JsonIgnore]
        public string packageID { get; set; }

        public string name { get; set; }
        public string author { get; set; }
        
        public List<Version> versions { get; set; }
    }
}