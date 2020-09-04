using System;
using System.Threading.Tasks;
using Core.Model;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
         IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
         Task<int> Complete(); //no. of changes in the database
    }
}