using System;
using System.Collections;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Model;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly skinetContext _context;
        private Hashtable _repositories; //any repository we use under unit of work will be store inside this hash table
        public UnitOfWork(skinetContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if(_repositories == null) _repositories = new Hashtable(); //check anything inside hashtable or hashtable created

            var type = typeof(TEntity).Name; //name of entity and see what it is actually is

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType
                (typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}