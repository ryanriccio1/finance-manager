using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    /// <summary>
    /// CRUD interface for data in the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataService<T>
    {
        /// <summary>
        /// Get all of a specific type.
        /// </summary>
        /// <returns>All of the found data of type <typeparamref name="T"/>.</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get an item of a type by Id.
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>The item of type <typeparamref name="T"/> if found.</returns>
        Task<T> Get(int id);

        /// <summary>
        /// Create a new database entry from an entity.
        /// </summary>
        /// <param name="entity">Entity to add to the database.</param>
        /// <returns>The item of type <typeparamref name="T"/>.</returns>
        Task<T> Create(T entity);

        /// <summary>
        /// Update an item by id with a new version of the item.
        /// </summary>
        /// <param name="id">Id of item to update.</param>
        /// <param name="entity">Entity to update item to.</param>
        /// <returns>The update entity of type <typeparamref name="T"/>.</returns>
        Task<T> Update(int id, T entity);

        /// <summary>
        /// Delete an item by id.
        /// </summary>
        /// <param name="id">Id of item to delete.</param>
        /// <returns>True if the delete was successful.</returns>
        Task<bool> Delete(int id);
    }
}
