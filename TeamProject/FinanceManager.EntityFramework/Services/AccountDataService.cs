using FinanceManager.Domain.Services;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;
using FinanceManager.EntityFramework.Services.Common;

namespace FinanceManager.EntityFramework.Services
{
    // very similar to IDataService<Account>, except the getters perform SQL join to bring back objects
    // from AccountHolder and AssetTransactions rather than the values that they store
    public class AccountDataService : IAccountService
    {
        private readonly FinanceManagerDbContextFactory _contextFactory;    // get the DB context (db connection in this case)
        private readonly NonQueryDataService<Account> _nonQueryDataService; // common operations


        public AccountDataService(FinanceManagerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<Account>(contextFactory);
        }

        public async Task<Account> Get(int id)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // same as normal getter, except get objects instead of values       
                Account entity = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // same as normal get all, except get objects instead of values       
                IEnumerable<Account> entities = await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .ToListAsync();
                return entities;
            }
        }

        public async Task<Account> GetByUsername(string username)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // this is the implementation of GetByUsername, which adds the accounts and assets
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(a => a.AccountHolder.Username == username);
            }
        }

        public async Task<Account> GetByEmail(string email)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // this is the implementation of GetByEmail, which adds the accounts and assets
                return await context.Accounts
                    .Include(a => a.AccountHolder)
                    .Include(a => a.AssetTransactions)
                    .FirstOrDefaultAsync(a => a.AccountHolder.Email == email);
            }
        }

        // allow our default common data service to handle the rest of CRUD
        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }


    }
}
