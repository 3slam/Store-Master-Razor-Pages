using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public  List<Cart> ShoppingCartProducts {  get; set; }
        public string TotalPrice;
    }
}
