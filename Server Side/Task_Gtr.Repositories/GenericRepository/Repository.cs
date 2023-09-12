using Task_Gtr.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Task_Gtr.Repositories.GenericRepository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Declare the context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Declare the set
        /// </summary>
        private DbSet<TEntity> set;

        /// <summary>
        /// set disposed value
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The Context</param>
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets Entities
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return this.Set; }
        }

        /// <summary>
        /// Gets the set value
        /// </summary>
        protected DbSet<TEntity> Set
        {
            get { return this.set ?? (this.set = this.context.Set<TEntity>()); }
        }

        #region GetMethods
        /// <summary>
        /// Get all IEntity list
        /// </summary>
        /// <returns>IEntity list</returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await this.Set?.ToListAsync();
        }

        /// <summary>
        /// Get all IEntity with cancellation Token
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>IEntity list</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await this.Set.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get all with filters
        /// </summary>
        /// <param name="orderBy">The Order by</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>IEntity list</returns>
        public virtual IEnumerable<TEntity> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return this.GetQueryable(null, orderBy, includeProperties, skip, take);
        }

        /// <summary>
        /// Async IEntity list with filter
        /// </summary>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>IEntity list</returns>

        /// <summary>
        /// Get Entity with predict
        /// </summary>
        /// <param name="filter">The filter value</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>IEntity list</returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return this.GetQueryable(filter, orderBy, includeProperties, skip, take);
        }

        /// <summary>
        /// Get Async Entity with predict
        /// </summary>
        /// <param name="filter">The filter value</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>The Entity</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await this.GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        /// <summary>
        /// Get First Or default entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>Single Entity</returns>
        public virtual TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            return this.GetQueryable(filter, null, includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// Get Async First Or default entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>Single Entity</returns>
        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            return await this.GetQueryable(filter, null, includeProperties).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get First entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>First Entity</returns>
        public virtual TEntity GetFirst(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            return this.GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        /// <summary>
        /// Get Async First entity object
        /// </summary>
        /// <param name="filter">Filter Function</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <returns>First Entity</returns>
        public virtual async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        {
            return await this.GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get counts
        /// </summary>
        /// <param name="filter">A filter</param>
        /// <returns>Counts value</returns>
        public virtual long GetCount(Func<TEntity, long> filter)
        {
            if (this.Set.Count() <= 0)
            {
                return 0;
            }
            else
            {
                return this.Set.Max(filter);
            }
        }

        /// <summary>
        /// Check exists based on prediction
        /// </summary>
        /// <param name="filter">filter Function</param>
        /// <returns>boolean value</returns>
        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.GetQueryable(filter).Any();
        }

        /// <summary>
        /// Check Async exists based on prediction
        /// </summary>
        /// <param name="filter">filter Function</param>
        /// <returns>boolean value</returns>
        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return this.GetQueryable(filter).AnyAsync();
        }
        #endregion

        #region Crud Operations
        /// <summary>
        /// Add Object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Add(TEntity entity) 
        {
            this.Set.Add(entity);
            
        }

        /// <summary>
        /// Add Object range in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            this.Set.AddRange(entity);
        }

        /// <summary>
        /// Update Object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Update(TEntity entity)
        {
            this.Set.Update(entity);
        }

        /// <summary>
        /// Detach Entry in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void DetachEntry(TEntity entity)
        {
            this.context.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// Remove object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        public virtual void Remove(TEntity entity)
        {
            this.Set.Remove(entity);
        }

        /// <summary>
        /// Save the transection
        /// </summary>
        public virtual void Save()
        {
            this.context.SaveChanges();
        }

        /// <summary>
        /// Save Async the transection
        /// </summary>
        /// <returns>Response long value</returns>
        public async virtual Task<long> SaveAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        #endregion

        #region Find
        /// <summary>
        /// Get entity based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity</returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> Include = null)
        {
            return this.Set.Include(Include).FirstOrDefault(match);
        }

        /// <summary>
        /// Get async entity based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity</returns>
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> Include = null)
        {
            if (Include!=null)
            {
                return await this.Set.Include(Include).FirstOrDefaultAsync(match);
            }
            return await this.Set.FirstOrDefaultAsync(match);
        }

        /// <summary>
        /// Get entity list based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity list</returns>
        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match)
        {
            return this.Set.Where(match);
        }

        /// <summary>
        /// Get async entity list based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity list</returns>
        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.Set.Where(match).ToListAsync();
        }

        /// <summary>
        /// Get async entity list based on expression
        /// </summary>
        /// <param name="match">The Expression</param>
        /// <returns>The Entity list</returns>
        public async Task<IEnumerable<TEntity>> FindAllIEnumerableAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.Set.Where(match).ToListAsync();
        }

        #endregion

        /// <summary>
        /// Get Query able Entities with predict
        /// </summary>
        /// <param name="filter">The filter value</param>
        /// <param name="orderBy">The Order By value</param>
        /// <param name="includeProperties">A Include properties</param>
        /// <param name="skip">A skip count</param>
        /// <param name="take">A take count</param>
        /// <returns>The Entity</returns>
        public virtual IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = this.Set;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        /// <summary>
        /// Dispose transaction
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposed based on variable value
        /// </summary>
        /// <param name="disposing">Disposing value</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }
    }
}
