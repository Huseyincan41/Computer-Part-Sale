using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
	public class ComputerPartSaleViewModel
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public int UserId { get; set; }
        public List<ComputerPartSaleDetailViewModel> SaleDetails { get; set; } 
    }
}
