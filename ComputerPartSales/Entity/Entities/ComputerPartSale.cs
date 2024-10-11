using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public class ComputerPartSale:BaseEntity
	{
        public int Id { get; set; }
        public DateTime Date { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public int UserId { get; set; }

		//public virtual User User { get; set; }       

		public virtual ICollection<ComputerPartSaleDetail> ComputerPartSaleDetails { get; set; } = new List<ComputerPartSaleDetail>();
    }
}
