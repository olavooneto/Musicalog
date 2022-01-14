using Microsoft.EntityFrameworkCore.ChangeTracking;
using Musicalog.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Domain.Base
{
    public interface IBaseServices<T> where T : BaseEntity
    {
        Task<IList<T>> ListAll();

        ValueTask<T> GetIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entitites);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entitites);

        EntityEntry<T> Update(T entity);

        Task<int> CommitAsync();
    }
}
