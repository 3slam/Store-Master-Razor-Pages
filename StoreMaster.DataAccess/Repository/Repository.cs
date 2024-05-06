using Microsoft.EntityFrameworkCore;
using StoreMaster.DataAccess.Data;
using StoreMaster.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public DBContext _db { set;get; }
        public DbSet<T>  _dbSet { set; get; }

        public Repository(DBContext  db) 
        { 
            _db= db;
            this._dbSet = _db.Set<T>();
        }
        public async Task Add(T entity)
        {
            await  _db.AddAsync(entity);
            _db.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query;
            query = _dbSet;
            return query;
        }

        public T GetValue(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query;
            query = _dbSet;
            return query.FirstOrDefault(filter);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
            _db.SaveChanges();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query;
            query = _dbSet;
            return query.Where<T>(filter);
        }

		public void RemoveRange(IEnumerable<T> entity)
		{
			_dbSet.RemoveRange(entity);
			_db.SaveChanges();
		}
	}
}
