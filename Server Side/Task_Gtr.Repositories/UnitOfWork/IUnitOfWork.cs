using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task_Gtr.Repositories.GenericRepository;

namespace Task_Gtr.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Register the repository
        /// </summary>
        /// <typeparam name="TEntity">TEntity is the class</typeparam>
        /// <returns>A repository</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        /// <summary>
        /// Save transaction 
        /// </summary>
        /// <returns>Response integer</returns>
        int SaveChanges();

        /// <summary>
        /// Save transaction async
        /// </summary>
        /// <returns>Response integer</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Save transaction async with cancellation token
        /// </summary>
        /// <param name="cancellationToken">A cancellation Token</param>
        /// <returns>Response integer</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
