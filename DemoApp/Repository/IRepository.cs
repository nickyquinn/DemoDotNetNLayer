using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Data.Repository
{
    /// <summary>
    /// An interface for a generic repository.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Return all TEntities from storage
        /// </summary>
        /// <returns>IEnumerable of TEntity</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Return a page of TEntities from storage
        /// </summary>
        /// <param name="skip">The number of records to skip</param>
        /// <param name="take">The number of records to take</param>
        /// <returns>IEnumerable of TEntity</returns>
        IEnumerable<TEntity> PageAll(int skip, int take);

        /// <summary>
        /// Find a TEntity by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity FindById(object id);

        /// <summary>
        /// Add a TEntity to storage
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Update a TEntity in storage
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Remove a TEntity from storage
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);
    }
}
