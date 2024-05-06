using StoreMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader orderHeader);

        public void UpdateStatus(int OrderHeaderId, string orderStatus, string? paymentStatus = null);
        public void UpdateStripePaymentID(int OrderHeaderId, string sessionId, string paymentIntentId);

	}
}
