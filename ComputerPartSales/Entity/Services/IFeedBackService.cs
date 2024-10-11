using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface IFeedBackService
    {
        Task<List<FeedBackViewModel>> GetAllByProductId(int id);
        Task Update(FeedBackViewModel model);
        Task Delete(FeedBackViewModel model);
        Task Add(FeedBackViewModel model);
    }
}
