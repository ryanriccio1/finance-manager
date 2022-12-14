using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanceManager.EntityFramework
{
    public class FinanceManagerDbContextFactory
    {
        // this comes from our app.xaml.cs host builder
        // it contains a lambda that is used to apply a config to dbOptions
        private readonly Action<DbContextOptionsBuilder> _configureDbContext;

        public FinanceManagerDbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
        {
            _configureDbContext = configureDbContext;
        }

        public FinanceManagerDbContext CreateDbContext()
        {
            // create the default db options
            DbContextOptionsBuilder<FinanceManagerDbContext> options = new DbContextOptionsBuilder<FinanceManagerDbContext>();
            
            // apply our options from the host builder
            _configureDbContext(options);

            // return a DbContext with the host builder options
            return new FinanceManagerDbContext(options.Options);
        }
    }
}
