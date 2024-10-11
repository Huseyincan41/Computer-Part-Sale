using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
	public class ComputerPartSaleDetailViewModel
	{
		public int Id { get; set; }
		public int Number { get; set; }
		public decimal UnitPrice { get; set; }
		public int ComputerPartId { get; set; }
		public int ComputerPartSaleId { get; set; }
        public string ComputerPartDescription { get; set; }= string.Empty;
        public string ComputerPartName { get; set; } = string.Empty;
        public virtual ComputerPartViewModel ComputerPart { get; set; }
    }
}
