using StoreMaster.DataAccess.Data;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private DBContext _db;
        public CategoryRepository(DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Update(category);
            _db.SaveChanges();
        }
    }
}
