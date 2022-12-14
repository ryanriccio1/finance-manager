using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Domain.Models;
using FinanceManager.Domain.Services;
using FinanceManager.EntityFramework.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinanceManager.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly FinanceManagerDbContextFactory _contextFactory;    // get the db context (db connection in this case)
        private readonly NonQueryDataService<T> _nonQueryDataService;   // common operations

        public GenericDataService(FinanceManagerDbContextFactory contextFactory)
        {
           _contextFactory = contextFactory;
           _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            // manage the scope of the entity
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // get data without including extra fields
                IEnumerable<T> entities = await context.Set<T>().ToListAsync();
                return entities;
            }
        }

        public async Task<T> Get(int id)
        {
            using (FinanceManagerDbContext context = _contextFactory.CreateDbContext())
            {
                // get data without including extra fields
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        // allow our common data service to handle the rest of CRUD
        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<T> Update(int id, T entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

    }
}
