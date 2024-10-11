using AutoMapper;
using Entity.Entities;
using Entity.Services;
using Entity.UnitOfWorks;
using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class FeedBackService :IFeedBackService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public FeedBackService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }


        public async Task Add(FeedBackViewModel model)
        {
            FeedBack feedback = new FeedBack();
            feedback = _mapper.Map<FeedBack>(model);
            await _uow.GetRepository<FeedBack>().Add(feedback);
            await _uow.CommitAsync();
        }
        public async Task<List<FeedBackViewModel>> GetAllByProductId(int ComputerPartId)
        {
            var list = await _uow.GetRepository<FeedBack>().GetAllAsync(c => c.ComputerPartId == ComputerPartId);
            return _mapper.Map<List<FeedBackViewModel>>(list);
        }

        public async Task Delete(FeedBackViewModel model)
        {
            FeedBack fb = new FeedBack();
            fb = _mapper.Map<FeedBack>(model);
            _uow.GetRepository<FeedBack>().Delete(fb);
            await _uow.CommitAsync();
        }
        public async Task Update(FeedBackViewModel model)
        {
            FeedBack fb = new FeedBack();
            fb = _mapper.Map<FeedBack>(model);
            _uow.GetRepository<FeedBack>().Update(fb);
            await _uow.CommitAsync();
        }
    }
}
