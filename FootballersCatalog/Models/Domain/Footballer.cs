using System;
using System.ComponentModel.DataAnnotations;
using FootballersCatalog.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FootballersCatalog.Models.Domain
{
    public class Footballer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        [Required]
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [JsonConverter(typeof(DateFormatConverter), "dd-MM-yyyy")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Team")]
        [Required]
        public int TeamId { get; set; }
        public Team Team { get; set; }

        [Display(Name = "Country")]
        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }

    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}
