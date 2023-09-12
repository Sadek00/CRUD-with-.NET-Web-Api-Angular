using Task_Gtr.DataAccess.Data;
using Task_Gtr.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Task_Gtr.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        /// <summary>
        /// Context declare
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Repositories declare
        /// </summary>
        private Hashtable repositories;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="options">Context options</param>
        public UnitOfWork(DbContextOptions<ApplicationDbContext> options)
        {
            this.context = new ApplicationDbContext(options);
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #endregion
        // todo : currently this has 55 refrences we should really be using 
        //       this function's async sibling instead.

        /// <summary>
        /// Save Transaction
        /// </summary>
        /// <returns>Response integer</returns>
        public int SaveChanges()
        {
            try
            {
                return this.context.SaveChanges();
            }
            finally
            {
                this.repositories = null;
            }
        }

        /// <summary>
        /// Save transection async
        /// </summary>
        /// <returns>Response value</returns>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await this.context.SaveChangesAsync();
            }
            finally
            {
                this.repositories = null;
            }
        }

        /// <summary>
        /// Save transection async
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Response number</returns>
        public async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                return await this.context.SaveChangesAsync(cancellationToken);
            }
            finally
            {
                this.repositories = null;
            }
        }

        /// <summary>
        /// Register the repository
        /// </summary>
        /// <typeparam name="T">T is the class</typeparam>
        /// <returns>A repository</returns>
        public IRepository<T> Repository<T>() where T : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!this.repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this.context);
                this.repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)this.repositories[type];
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose context
        /// </summary>
        public void Dispose()
        {
            this.context.Dispose();
        }
        #endregion
    }
}
