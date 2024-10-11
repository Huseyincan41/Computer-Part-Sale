using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll();
        Task Update(CategoryViewModel model);
        Task Delete(CategoryViewModel model);
        Task Add(CategoryViewModel model);
    }
}
