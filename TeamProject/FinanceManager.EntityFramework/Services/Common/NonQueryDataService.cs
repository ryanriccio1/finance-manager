using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;

namespace FinanceManager.EntityFramework.Services.Common
{
    // base non querying CRUD operations (CUD)
    public class NonQueryDataService<T> where T : DomainObject
    {
        // generate a DbContext
        private readonly FinanceManagerDbContextFactory _contextFactory;

        // get the db context from dependency injection
        public NonQueryDataService(FinanceManagerDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // create an entity, save it, and return the created entity
                // ex passing in an account will Async add the account to Accounts
                EntityEntry<T> createdEntity = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return createdEntity.Entity;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // update an entity at an id with an instance of that entity
                // then return it
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // find the first item with our id and remove it, then save changes and return success bool
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }
    }
}
