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
    public class ComputerPartService : IComputerPartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ComputerPartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Add(ComputerPartViewModel model)
        {
           ComputerPart computerPart=_mapper.Map<ComputerPart>(model);
            await _unitOfWork.GetRepository<ComputerPart>().Add(computerPart);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(ComputerPartViewModel model)
        {
            ComputerPart product = await _unitOfWork.GetRepository<ComputerPart>().GetByIdAsync(model.ComputerPartId);
            if (product != null)
            {
                _unitOfWork.GetRepository<ComputerPart>().Delete(product);
                await _unitOfWork.CommitAsync();
            }
        }

        public async Task<ComputerPartViewModel> Get(int ComputerPartId)
        {
            var part = await _unitOfWork.GetRepository<ComputerPart>().GetByIdAsync(ComputerPartId);
            return _mapper.Map<ComputerPartViewModel>(part);
        }

        public async Task<IEnumerable<ComputerPartViewModel>> GetAll()
        {
            var list = await _unitOfWork.GetRepository<ComputerPart>().GetAllAsync();
            return _mapper.Map<List<ComputerPartViewModel>>(list);
        }

        public async Task Update(ComputerPartViewModel model)
        {
            

            ComputerPart product = _mapper.Map<ComputerPart>(model);
            _unitOfWork.GetRepository<ComputerPart>().Update(product);
            await _unitOfWork.CommitAsync();
        }
    }
}
