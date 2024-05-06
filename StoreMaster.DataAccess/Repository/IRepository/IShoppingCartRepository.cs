using StoreMaster.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<Cart>
    {
        void Update(Cart shoppingCart);
        int DecrementCount(Cart shoppingCart, int count);
        int IncrementCount(Cart shoppingCart, int count);
    }
}
