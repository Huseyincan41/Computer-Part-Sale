using AutoMapper;
using Data.Context;
using Data.UnitOfWorks;
using Entity.Entities;
using Entity.Services;
using Entity.UnitOfWorks;
using Entity.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class ComputerPartSaleService:IComputerPartSaleService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
        private readonly ComputerDbContext _dbContext;
        public ComputerPartSaleService(IUnitOfWork uow, IMapper mapper, ComputerDbContext dbContext)
        {
            _uow = uow;
            _mapper = mapper;
            _dbContext = dbContext;
        }


        public async Task<int> Add(ComputerPartSaleViewModel model)
		{
			//ComputerPartSale pSale = new ComputerPartSale();
			//pSale = _mapper.Map<ComputerPartSale>(model);
			//await _uow.GetRepository<ComputerPartSale>().Add(pSale);
			//await _uow.CommitAsync();
            ComputerPartSale pSale = _mapper.Map<ComputerPartSale>(model);
            await _uow.GetRepository<ComputerPartSale>().Add(pSale);
            await _uow.CommitAsync();
            return pSale.Id;

        }

		public    int AddSale(ComputerPartSaleViewModel model)
		{
            //ComputerPartSale pSale = _mapper.Map<ComputerPartSale>(model);

            //// Satış ekleniyor
            //_uow.GetRepository<ComputerPartSale>().Add(pSale);

            //// Veritabanına async olarak kaydediliyor
            //_uow.CommitAsync();

            //// Kaydedilen satışın ID'sini döndür
            //return pSale.Id; // Burada ID sıfırdan farklı olmalı

            ComputerPartSale pSale = new ComputerPartSale();
            pSale = _mapper.Map<ComputerPartSale>(model);
             _uow.GetRepository<ComputerPartSale>().Add(pSale);
             _uow.CommitAsync();
            return pSale.Id;

        }

        public async Task Delete(ComputerPartSaleViewModel model)
        {
            ComputerPartSale product = await _uow.GetRepository<ComputerPartSale>().GetByIdAsync(model.Id);
            if (product != null)
            {
                _uow.GetRepository<ComputerPartSale>().Delete(product);
                await _uow.CommitAsync();
            }
        }

        public async Task<ComputerPartSaleViewModel> Get(int id)
		{
			var productSale = await _uow.GetRepository<ComputerPartSale>().GetByIdAsync(id);
			return _mapper.Map<ComputerPartSaleViewModel>(productSale);
		}

		public async Task<List<ComputerPartSaleViewModel>> GetAll()
		{
			var list = await _uow.GetRepository<ComputerPartSale>().GetAllAsync();
			return _mapper.Map<List<ComputerPartSaleViewModel>>(list);
		}

        public async Task<ComputerPartSaleViewModel> GetSaleByIdAsync(int saleId)
        {
            //    var sale = await _uow.GetRepository<ComputerPartSale>()
            //.GetAsync(x => x.Id == saleId, include: x => x.Include(s => s.ComputerPartSaleDetails).ThenInclude(d => d.ComputerPart));

            var sale = await _uow.GetRepository<ComputerPartSale>()
                .GetAsync(x => x.Id == saleId,
                    include: query => query
                        .Include(s => s.ComputerPartSaleDetails)
                        .ThenInclude(d => d.ComputerPart));

            if (sale == null)
            {
                return null; // Eğer kayıt yoksa null döndür
            }

            // ViewModel'e haritalama işlemi
            var saleViewModel = new ComputerPartSaleViewModel
            {
                Id = sale.Id,
                Date = sale.Date,
                Quantity = sale.Quantity,
                Price = sale.Price,
                UserId = sale.UserId,
                SaleDetails = sale.ComputerPartSaleDetails?
                .Select(detail => new ComputerPartSaleDetailViewModel
            {
                     Id = detail.Id,
                    Number = detail.Number,
                    UnitPrice = detail.UnitPrice,
                    ComputerPartId = detail.ComputerPartId,
                    ComputerPartName = detail.ComputerPart.Name ?? "Ürün Adı Yok",
                    ComputerPartDescription=detail.ComputerPartDescription ??"boş",

                    ComputerPart = detail.ComputerPart == null ? null : new ComputerPartViewModel
                    {

                        Name = detail.ComputerPart.Name,
                        Description = detail.ComputerPart.Description,
                        ImageUrl = detail.ComputerPart?.ImageUrl

                    }
                }).ToList() ?? new List<ComputerPartSaleDetailViewModel>()
            };

            return saleViewModel;

        }

        public async Task<List<ComputerPartSaleViewModel>> GetUserSalesAsync(int userId)
        {
            var sales = await _uow.GetRepository<ComputerPartSale>()
            .GetAllAsync(x => x.UserId == userId);

            // Her satış için ilgili detayları çekip ViewModel'e dönüştürüyoruz
            List<ComputerPartSaleViewModel> salesViewModels = new List<ComputerPartSaleViewModel>();

            foreach (var sale in sales)
            {
                // ComputerPartSaleViewModel'e dönüşüm
                var saleViewModel = new ComputerPartSaleViewModel
                {
                    Id = sale.Id,
                    Date = sale.Date,
                    Quantity = sale.Quantity,
                    Price = sale.Price,
                    UserId = sale.UserId
                };

                // Satış detaylarını da ayrı bir şekilde çekiyoruz
                var saleDetails = await _uow.GetRepository<ComputerPartSaleDetail>()
                    .GetAllAsync(x => x.ComputerPartSaleId == sale.Id);

                saleViewModel.SaleDetails = saleDetails.Select(detail => new ComputerPartSaleDetailViewModel
                {
                    Id = detail.Id,
                    Number = detail.Number,
                    UnitPrice = detail.UnitPrice,
                    ComputerPartId = detail.ComputerPartId,
                    ComputerPartSaleId = detail.ComputerPartSaleId,
                    ComputerPartDescription = detail.ComputerPartDescription,
                    ComputerPartName = detail.ComputerPartName,
                }).ToList();

                salesViewModels.Add(saleViewModel);
            }

            return salesViewModels;
        }
    }
}
