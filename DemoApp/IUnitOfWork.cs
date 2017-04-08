using DemoApp.Data.Entities;
using DemoApp.Data.Repository;
using System;
using System.Data.Entity;

namespace DemoApp.Data
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IRepository<Person> PersonRepository { get; }

        /// <summary>
        /// Commit all changes made during this unit of work
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}