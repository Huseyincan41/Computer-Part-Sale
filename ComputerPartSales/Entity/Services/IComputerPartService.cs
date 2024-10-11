using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface IComputerPartService
    {
        Task<IEnumerable<ComputerPartViewModel>> GetAll();
        Task<ComputerPartViewModel> Get(int id);
        Task Add(ComputerPartViewModel model);
        Task Update(ComputerPartViewModel model);
        Task Delete(ComputerPartViewModel model);
    }
}
