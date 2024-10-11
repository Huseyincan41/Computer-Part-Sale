using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
	public interface IComputerPartSaleService
	{
		Task<List<ComputerPartSaleViewModel>> GetAll();
		Task<ComputerPartSaleViewModel> Get(int id);
        Task Delete(ComputerPartSaleViewModel model);

        Task<int> Add(ComputerPartSaleViewModel model);
		int AddSale(ComputerPartSaleViewModel model);
		Task<List<ComputerPartSaleViewModel>> GetUserSalesAsync(int id);
		Task<ComputerPartSaleViewModel> GetSaleByIdAsync(int saleId);

    }
}
