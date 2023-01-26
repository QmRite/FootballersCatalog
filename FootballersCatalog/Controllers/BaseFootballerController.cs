using System.Linq;
using FootballersCatalog.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using FootballersCatalog.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace FootballersCatalog.Controllers
{
    public class BaseFootballerController : Controller
    {
        protected readonly FootballersCatalogDbContext footballersCatalogDb;
        protected readonly IHubContext<FootballersHub> footballersHub;

        public BaseFootballerController(FootballersCatalogDbContext footballersCatalogDb,
            IHubContext<FootballersHub> footballersHub)
        {
            this.footballersCatalogDb = footballersCatalogDb;
            this.footballersHub = footballersHub;
        }   

        protected async Task<SelectList> GetCountriesSelectList()
        {
            var countries = await footballersCatalogDb.Countries.ToListAsync();
            var selectList = new SelectList(countries, "Id", "Name");
            return selectList;
        }

        protected async Task<SelectList> GetTeamsSelectList()
        {
            var teams = await footballersCatalogDb.Teams.ToListAsync();
            teams.Add(new Team{Id = 0, Name = "-Add new team-"});
            var selectList = new SelectList(teams, "Id", "Name");
            return selectList;
        }

        public JsonResult IsTeamExists([Bind(Prefix = "Team.Name")] string name)
        {
            return Json(!footballersCatalogDb.Teams.Any(t => t.Name == name));
        }
    }
}