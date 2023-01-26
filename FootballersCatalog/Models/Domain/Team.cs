using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballersCatalog.Models.Domain
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Remote("IsTeamExists", "BaseFootballer", ErrorMessage = "Team already exists.")]
        public string Name { get; set; }

        public List<Footballer> Footballers { get; set; }
    }
}
