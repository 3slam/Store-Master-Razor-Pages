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
    public class ShoppingCartRepository : Repository<Cart>, IShoppingCartRepository
    {
        private DBContext _db;
        public ShoppingCartRepository(DBContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Cart shoppingCart)
        {
            _db.Update(shoppingCart);
            _db.SaveChanges();
        }


        public int DecrementCount(Cart shoppingCart, int count)
        {
            if(shoppingCart.Count == 1)
            {
                Remove(shoppingCart);
            }
            else
            {
                shoppingCart.Count -= count;
                _db.SaveChanges();
            }
         
            return shoppingCart.Count;
        }

        public int IncrementCount(Cart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            _db.SaveChanges();
            return shoppingCart.Count;
        }

    }
}
