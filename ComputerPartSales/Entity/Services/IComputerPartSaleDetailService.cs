using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
	public interface IComputerPartSaleDetailService
	{
		Task Add(ComputerPartSaleDetailViewModel model);
		Task<List<ComputerPartSaleDetailViewModel>> GetAll();
		Task<List<ComputerPartSaleDetailViewModel>> GetByProductSaleId(int id);
		Task<bool>  AddRange(List<SepetDetay> sepet, int ComputerPartSaleId);

	}
}
