using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Musicalog.Models.Entities.Base;
using Musicalog.Repository.DataContexts;
using System.Linq.Expressions;

namespace Musicalog.Services.Base
{
    public abstract class BaseServices<T> where T : BaseEntity
    {
        protected MusicLogDBDataContext Context { get; private set; }

        public BaseServices(MusicLogDBDataContext context) => (Context) = (context);

        public virtual async Task<IList<T>> ListAll() => await Context.Set<T>().ToListAsync();

        public async Task AddAsync(T entity) => await Context.Set<T>().AddAsync(entity);

        public EntityEntry<T> Update(T entity) => Context.Set<T>().Attach(entity);

        public async Task AddRangeAsync(IEnumerable<T> entitites) => await Context.Set<T>().AddRangeAsync(entitites);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => Context.Set<T>().Where(predicate);

        public async Task<IEnumerable<T>> GetAllAsync() => await Context.Set<T>().ToListAsync();

        public async ValueTask<T> GetIdAsync(int id) => await Context.Set<T>().FindAsync(id);

        public void Remove(T entity) => Context.Set<T>().Remove(entity);

        public void RemoveRange(IEnumerable<T> entitites) => Context.Set<T>().RemoveRange(entitites);

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate) => await Context.Set<T>().SingleOrDefaultAsync(predicate);

        public async Task<int> CommitAsync()
        {
            try
            {
                return await this.Context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"--------------{ DateTime.UtcNow.ToString() }--------------");
                System.Diagnostics.Debug.WriteLine(e.ToString());
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e?.InnerException?.Message);
                System.Diagnostics.Debug.WriteLine(e.StackTrace);

                throw;
            }
        }
    }
}
