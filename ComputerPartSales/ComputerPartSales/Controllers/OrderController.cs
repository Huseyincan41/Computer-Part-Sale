using Entity.Services;
using Entity.UnitOfWorks;
using Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace ComputerPartSales.Controllers
{
	public class OrderController : Controller
	{
		private readonly IComputerPartSaleService _computerPartSaleService;
		private readonly IAccountService _accountService;
		private readonly IUnitOfWork unitOfWork;

        public OrderController(IUnitOfWork unitOfWork, IComputerPartSaleService computerPartSaleService, IAccountService accountService)
        {

            this.unitOfWork = unitOfWork;
            _computerPartSaleService = computerPartSaleService;
            _accountService = accountService;
        }

        public async Task<IActionResult> Index(int userId)
		{
            var username = User.Identity.Name;
            var user = await _accountService.Find(username);

            if (user == null)
            {
                TempData["Error"] = "Kullanıcı bulunamadı.";
                return RedirectToAction("Login", "Account");
            }

            var userSales = await _computerPartSaleService.GetUserSalesAsync(user.Id);
            return View(userSales);

        }
        public async Task<IActionResult> OrderDetails(int saleId)
        {
            var sale = await _computerPartSaleService.GetSaleByIdAsync(saleId);
            if (sale == null)
            {
                TempData["Error"] = "Sipariş bulunamadı.";
                return RedirectToAction("UserOrders");
            }

            return View(sale);
        }
    }
}
