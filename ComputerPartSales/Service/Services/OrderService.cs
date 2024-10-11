using AutoMapper;
using Data.Context;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ComputerDbContext _dbContext;

        public OrderService(IUnitOfWork uow, IMapper mapper, ComputerDbContext dbContext)
        {
            _uow = uow;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<ComputerPartSaleViewModel>> GetUserSalesAsync(int userId)
        {
            var sales = await _dbContext.ComputerPartSales.Where(r=>r.UserId==userId).Select(r=>r.UserId).ToListAsync();

            // Satışları ViewModel'e map'leyelim
            return _mapper.Map<List<ComputerPartSaleViewModel>>(sales);
        }
    }
}
