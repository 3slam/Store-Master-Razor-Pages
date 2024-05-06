using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
        T GetValue(Expression<Func<T, bool>> filter);
        Task Add(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);

	}
}
