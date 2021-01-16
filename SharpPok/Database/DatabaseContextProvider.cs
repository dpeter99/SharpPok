using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharpPok.Database
{
    class DatabaseContextProvider: IDbContextFactory<PokDatabaseContext>
    {
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="JellyfinDbProvider"/> class.
        /// </summary>
        /// <param name="serviceProvider">The application's service provider.</param>
        /// <param name="appPaths">The application paths.</param>
        public DatabaseContextProvider(IServiceProvider serviceProvider, IHostEnvironment environment)
        {
            _serviceProvider = serviceProvider;

            using var jellyfinDb = CreateDbContext();
            //Console.WriteLine($"THIS IS : {environment.EnvironmentName} NOT PATRIC");
            if(environment.EnvironmentName != "Design")
            {
                jellyfinDb.Database.Migrate();
            }
            
        }

        public PokDatabaseContext CreateDbContext()
        {
            return ActivatorUtilities.CreateInstance<PokDatabaseContext>(_serviceProvider);
        }
    }
}