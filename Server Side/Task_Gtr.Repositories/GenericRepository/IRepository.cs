using System.Linq.Expressions;

namespace Task_Gtr.Repositories.GenericRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        
        IQueryable<TEntity> Entities
        {
            get;
        }


        Task<List<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);


        IEnumerable<TEntity> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);


        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);


        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);


        TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");

        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null);


        TEntity GetFirst(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");


        Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null);


        bool GetExists(Expression<Func<TEntity, bool>> filter = null);


        Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null);


        #region Crud Operations
        /// <summary>
        /// Add Object range in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        void AddRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Add Object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Add(TEntity entity);

        /// <summary>
        /// Update Object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Update(TEntity entity);

        /// <summary>
        /// Detach Entry in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        void DetachEntry(TEntity entity);

        /// <summary>
        /// Remove object in database
        /// </summary>
        /// <param name="entity">Entity object</param>
        void Remove(TEntity entity);

        /// <summary>
        /// Save the transection
        /// </summary>
        void Save();

        /// <summary>
        /// Save Async transection
        /// </summary>
        /// <returns>Long value</returns>
        Task<long> SaveAsync();
        #endregion


        TEntity Find(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> Include = null);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match, Expression<Func<TEntity, object>> Include = null);


        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> match);


        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);

        Task<IEnumerable<TEntity>> FindAllIEnumerableAsync(Expression<Func<TEntity, bool>> match);



        IQueryable<TEntity> GetQueryable(
                    Expression<Func<TEntity, bool>> filter = null,
                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                    string includeProperties = null,
                    int? skip = null,
                    int? take = null);


        void Dispose();

        

    }
}
