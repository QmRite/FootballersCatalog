using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FootballersCatalog.Models.Data;
using Microsoft.EntityFrameworkCore;
using FootballersCatalog.Models.Domain;
using Microsoft.AspNetCore.SignalR;

namespace FootballersCatalog.Controllers
{
    public class FootballerController : BaseFootballerController
    {
        public FootballerController(FootballersCatalogDbContext footballersCatalogDb,
            IHubContext<FootballersHub> footballersHub) : base(footballersCatalogDb, footballersHub) { }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetFootballers()
        {
            var footballers = await footballersCatalogDb.Footballers
                .Include(f => f.Team)
                .Include(f => f.Country)
                .ToListAsync();

            return Ok(footballers);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.TodaysDateString = DateTime.Today.ToString("yyyy-MM-dd");

            ViewBag.CountriesSelectList = await GetCountriesSelectList();
            ViewBag.TeamsSelectList = await GetTeamsSelectList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                await footballersCatalogDb.Footballers.AddAsync(footballer);
                await footballersCatalogDb.SaveChangesAsync();

                await footballersHub.Clients.All.SendAsync("LoadFootballers");
                TempData["SuccessMessage"] = "Footballer Added Successfully!";
            }

            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var footballer = await footballersCatalogDb.Footballers.FindAsync(id);
            if (footballer == null)
            {
                return NotFound();
            }

            ViewBag.TodaysDateString = DateTime.Today.ToString("yyyy-MM-dd");

            ViewBag.CountriesSelectList = await GetCountriesSelectList();
            ViewBag.TeamsSelectList = await GetTeamsSelectList();

            return PartialView("_EditPartial",footballer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                footballersCatalogDb.Update(footballer);
                await footballersCatalogDb.SaveChangesAsync();

                await footballersHub.Clients.All.SendAsync("LoadFootballers");
                TempData["SuccessMessage"] = "Footballer Edited Successfully!";
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var footballer = await footballersCatalogDb.Footballers.FindAsync(id);
            if (footballer == null)
            {
                return NotFound();
            }

            footballersCatalogDb.Footballers.Remove(footballer);
            await footballersCatalogDb.SaveChangesAsync();

            await footballersHub.Clients.All.SendAsync("LoadFootballers");
            TempData["SuccessMessage"] = "Footballer Deleted Successfully!";

            return RedirectToAction("Index");
        }
    }
}
