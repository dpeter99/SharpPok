using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SharpPok.Database.Model;

namespace SharpPok.Database
{
    public class PokDatabaseContext: DbContext
    {
        public DbSet<Package> Packages => Set<Package>();
        
        
        IOptions<DatabaseSettings> settings;

        public PokDatabaseContext(IOptions<DatabaseSettings> options)
        {
            settings = options;
            //Database.Migrate();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionString = $"server={settings.Value.Address};port=3306;user={settings.Value.User};password={settings.Value.Password};database={settings.Value.Database_Name}";

            optionsBuilder.UseMySql(
                    settings.Value.ConnectionString,
                    MariaDbServerVersion.FromString("10.4.12-MariaDB-1:10.4.12+maria~bionic"),
                    MysqlOptions)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            
        }
        
        void MysqlOptions(MySqlDbContextOptionsBuilder options)
        {
            options.CharSetBehavior(CharSetBehavior.NeverAppend);
            options.CharSet(CharSet.Utf8);
            //options.MigrationsAssembly("Aper_bot.Database.Migrations");
        }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}