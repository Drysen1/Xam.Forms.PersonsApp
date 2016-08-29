using Newtonsoft.Json;

namespace XamarinForms_Demo.Models
{
    public class PersonModel
    {
        [JsonProperty(PropertyName = "IDPerson")]
        public int IDPerson { get; set; }

        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "Age")]
        public int Age { get; set; }
    }
}
