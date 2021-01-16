using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharpPok.Database;

namespace SharpPok.Controllers
{
    [ApiController]
    [Route("/")]
    public class GetPackage : Controller
    {
        
        public IDbContextFactory<PokDatabaseContext> DbContextFactory { get; init; }
        public IOptions<Config> Conf { get; init; }


        public GetPackage(IDbContextFactory<PokDatabaseContext> db, IOptions<Config> conf)
        {
            DbContextFactory = db;
            Conf = conf;
        }
        
        // GET
        [HttpGet("{package}/latest")]
        public IActionResult GetLatest(string package)
        {
            var db = DbContextFactory.CreateDbContext();

            var pac = (from p in db.Packages.Include(p => p.versions)
                where p.name == package
                select p).FirstOrDefault();

            if (pac is null)
            {
                return new PokErrorResult(1,$"Package {package} Not found");
            }
            
            var version = (from v in pac.versions
                select v).FirstOrDefault();

            if (version is null)
            {
                return new PokErrorResult(1,$"Latest version for package {package} was not found");
            }

            string folder_path = Conf.Value.PackagesFolder + version.GetVersionFolder();
            if (System.IO.Directory.Exists(folder_path))
            {
                return new PokErrorResult(1,$"Files For {package}:{version.Name} Not found");
            }

            if (Directory.GetFiles(folder_path).Length > 0)
            {
                //TODO zip
            }
            else
            {
                string file = Directory.GetFiles(folder_path)[0];
                var stream = new FileStream(file,FileMode.Open);
                return new FileStreamResult(stream,"application/octet-stream");
            }
            
            
            
            return new AcceptedResult();
        }
        
        [HttpGet("{package}/{version}")]
        public IActionResult GetVersion()
        {
            return new AcceptedResult();
        }
    }
}