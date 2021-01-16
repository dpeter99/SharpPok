using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SharpPok.Database.Model
{
    public class Version
    {
        [Key]
        [JsonIgnore]
        public string VersionID { get; set; }
        
        [JsonIgnore]
        public string PackageID { get; set; }
        
        public Package Package { get; set; }
        
        public string Name { get; set; }
        
        public Version(string name)
        {
            Name = name;
        }


        public string GetVersionFolder()
        {
            return $"/{Package.name}/{Name}/";
        }
    }
}