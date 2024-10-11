using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
	public class SepetDetay
	{
		public int ComputerPartId { get; set; }
		public string ComputerPartName { get; set; }
		public string ComputerPartDescription { get; set; }
        public int ComputerPartQuantity { get; set; }
		public decimal ComputerPartPrice { get; set; }
        public string ImageUrl { get; set; } = string.Empty;


    }
}
