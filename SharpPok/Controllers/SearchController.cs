using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpPok.Database;
using SharpPok.Database.Model.Serialization;

namespace SharpPok.Controllers
{
    [ApiController]
    [Route("/search")]
    public class SearchController : Controller
    {
        public IDbContextFactory<PokDatabaseContext> DbContextFactory { get; init; }


        public SearchController(IDbContextFactory<PokDatabaseContext> db)
        {
            DbContextFactory = db;
        }
        
        // GET
        [HttpGet("{search}")]
        public IActionResult Index([FromRoute]string search)
        {
            var db = DbContextFactory.CreateDbContext();
            
            var res = from p in db.Packages.Include(p=>p.versions)
                where p.name.ToLower().Contains(search.ToLower())
                select p;
            

            if (Request.Headers.Keys.Contains("APER_EXTENSIONS"))
            {
                return new JsonResult(res.ToList());
            }
            else
            {
                var opt = new JsonSerializerOptions();
                opt.Converters.Add(new VersionSerializer());
                
                return new JsonResult(res.FirstOrDefault(), opt);
            }
        }
    }
}