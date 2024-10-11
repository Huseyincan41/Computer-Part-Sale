using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface IOrderService
    {
        Task<List<ComputerPartSaleViewModel>> GetUserSalesAsync(int userId);
    }
}
