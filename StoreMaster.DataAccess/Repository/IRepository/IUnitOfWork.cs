using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IProductRepository Product { get; }
		public IOrderDetailRepository OrderDetail { get;   }
		public IOrderHeaderRepository OrderHeader { get;   }
		public void SaveChanges();
    }
}
