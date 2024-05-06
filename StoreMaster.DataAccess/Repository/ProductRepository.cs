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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private DBContext _db;
        public ProductRepository(DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            _db.Update(product);
            _db.SaveChanges();
        }
    }
}
