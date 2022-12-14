using FinanceManager.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.WPF.HostBuilders
{
    public static class AddDbContextHostBuilderExtensions
    {
        public static IHostBuilder AddDbContext(this IHostBuilder host)
        {
            host.ConfigureServices((context, services) =>
            {   // get our database configuration from here and register our db context 
                string connectionString = context.Configuration.GetConnectionString("sqlite");
                Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlite(connectionString);
                services.AddDbContext<FinanceManagerDbContext>(configureDbContext);
                services.AddSingleton<FinanceManagerDbContextFactory>(new FinanceManagerDbContextFactory(configureDbContext));
            });
            return host;
        }
    }
}
