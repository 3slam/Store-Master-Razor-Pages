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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
	{
        private DBContext _db;
        public OrderHeaderRepository(DBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            _db.Update(orderHeader);
            _db.SaveChanges();
        }

		public void UpdateStatus(int OrderHeaderId, string orderStatus, string? paymentStatus = null)
		{
		    var orderHeaderItem = GetValue(orderHeaderItem => orderHeaderItem.Id == OrderHeaderId);
			orderHeaderItem.OrderStatus = orderStatus;
			orderHeaderItem.PaymentStatus = paymentStatus;
		 
			if (!string.IsNullOrEmpty(paymentStatus))
			{
				orderHeaderItem.PaymentStatus = paymentStatus;
			}
			_db.SaveChanges();
		}

		public void UpdateStripePaymentID(int OrderHeaderId, string sessionId, string paymentIntentId)
		{
			var orderHeaderItem = GetValue(orderHeaderItem => orderHeaderItem.Id == OrderHeaderId);

			if (!string.IsNullOrEmpty(sessionId))
			{
				orderHeaderItem.SessionId = sessionId;
			}
			if (!string.IsNullOrEmpty(paymentIntentId))
			{
				orderHeaderItem.PaymentIntentId = paymentIntentId;
				orderHeaderItem.PaymentDate = DateTime.Now;
			}
			_db.SaveChanges();
		}
	}
}
