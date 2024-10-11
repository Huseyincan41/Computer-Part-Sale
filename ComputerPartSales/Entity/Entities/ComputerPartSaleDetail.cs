using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
	public class ComputerPartSaleDetail : BaseEntity
	{
		public int Number { get; set; }
		public decimal UnitPrice { get; set; }
		public int ComputerPartId { get; set; }
		public int ComputerPartSaleId { get; set; }
        public string ComputerPartDescription { get; set; }
        public string ComputerPartName { get; set; }

        public ComputerPartSale ComputerPartSale { get; set; }

		public virtual ComputerPart ComputerPart { get; set; }

        //public object Select(Func<object, ComputerPartSaleDetailViewModel> value)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
