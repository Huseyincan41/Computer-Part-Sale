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
	public class ComputerPartSaleDetailService:IComputerPartSaleDetailService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public ComputerPartSaleDetailService(IUnitOfWork uow, IMapper mapper)
		{
			_uow = uow;
			_mapper = mapper;
		}

		public async Task Add(ComputerPartSaleDetailViewModel model)
		{
			ComputerPartSaleDetail pSaleDetail = new ComputerPartSaleDetail();
			pSaleDetail = _mapper.Map<ComputerPartSaleDetail>(model);
			await _uow.GetRepository<ComputerPartSaleDetail>().Add(pSaleDetail);
			await _uow.CommitAsync();
		}
		public async Task<List<ComputerPartSaleDetailViewModel>> GetAll()
		{
			var list = await _uow.GetRepository<ComputerPartSaleDetail>().GetAllAsync();
			return _mapper.Map<List<ComputerPartSaleDetailViewModel>>(list);
		}
		public async Task<List<ComputerPartSaleDetailViewModel>> GetByProductSaleId(int id)
		{
			var list = await _uow.GetRepository<ComputerPartSaleDetail>().GetAsync(c => c.ComputerPartId == id);
			return _mapper.Map<List<ComputerPartSaleDetailViewModel>>(list);
		}
		//public  bool AddRange(List<SepetDetay> sepet, int computerPartSaleId,ComputerPartSaleViewModel satisId)
		//{
		//	foreach (var item in sepet)
		//	{
		//		ComputerPartSaleDetail newDetail = new ComputerPartSaleDetail()
		//		{
		//			ComputerPartSaleId = computerPartSaleId,
		//			ComputerPartId = item.ComputerPartId,
		//			Number = item.ComputerPartQuantity,
		//			UnitPrice = item.ComputerPartPrice,
		//		};
		//		_uow.GetRepository<ComputerPartSaleDetail>().Add(newDetail);

		//	}
		//	try
		//	{
		//		_uow.CommitAsync();
		//		Console.WriteLine("Commit işlemi başarılı.");
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{

		//		string message = ex.Message;
		//	}
		//	return false;

		//}

		public async  Task<bool> AddRange(List<SepetDetay> sepet,  int computerPartSaleId)
		{
			foreach (var item in sepet)
			{
				ComputerPartSaleDetail newDetail = new ComputerPartSaleDetail()
				{
					ComputerPartSaleId = computerPartSaleId,
					ComputerPartId = item.ComputerPartId,
					Number = item.ComputerPartQuantity,
					UnitPrice = item.ComputerPartPrice,
					ComputerPartName = item.ComputerPartName,
					ComputerPartDescription = item.ComputerPartDescription ?? null ??"boş",
					
				};
                Console.WriteLine($"Ekleme: ComputerPartId={newDetail.ComputerPartId}, Number={newDetail.Number}, UnitPrice={newDetail.UnitPrice},ComputerPartName={newDetail.ComputerPartName},ComputerPartDescription={newDetail.ComputerPartDescription}");
                _uow.GetRepository<ComputerPartSaleDetail>().Add(newDetail);

			}
			try
			{
                _uow.Commit();
                return true;
			}
			catch (Exception ex)
			{
               
                
                string message = ex.Message;
				return false;
			}
           
            
		}

		
	}
}
