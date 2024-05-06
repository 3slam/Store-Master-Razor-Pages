using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.Models.ViewModels
{
    public class SummaryOrderViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public List<Cart> Items { get; set; }
        public int TotalNumberOfProducts { get; set; }
		public int TotalPrice { get; set; }
	}
}
