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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
        private DBContext _db;
        public OrderDetailRepository(DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail orderDetail)
        {
            _db.Update(orderDetail);
            _db.SaveChanges();
        }
    }
}
