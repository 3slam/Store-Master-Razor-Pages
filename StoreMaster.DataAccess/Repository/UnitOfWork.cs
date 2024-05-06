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
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }
		public IOrderDetailRepository OrderDetail { get; private set; }
		public IOrderHeaderRepository OrderHeader  { get; private set; }


		private readonly DBContext _dBContext;
        public UnitOfWork(DBContext dBContext)
        {
            _dBContext = dBContext;
            ShoppingCart = new ShoppingCartRepository(_dBContext);
            Product = new ProductRepository(_dBContext);
            Category = new CategoryRepository(_dBContext);
			OrderDetail = new OrderDetailRepository(_dBContext);
			OrderHeader = new OrderHeaderRepository(_dBContext);
		}

        public void SaveChanges()
        {
            _dBContext.SaveChanges();
        }
    }
}
