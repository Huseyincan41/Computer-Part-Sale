using ComputerPartSales.Models;
using Entity.Services;
using Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using System.Diagnostics;

namespace ComputerPartSales.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     private readonly IComputerPartService _computerPartService;
		private readonly IAccountService _accountService;
        private readonly IFeedBackService _feedBackService;
        public HomeController(ILogger<HomeController> logger, IComputerPartService computerPartService, IAccountService accountService, IFeedBackService feedBackService)
        {
            _logger = logger;
            _computerPartService = computerPartService;
            _accountService = accountService;
            _feedBackService = feedBackService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _computerPartService.GetAll();
            var populatedProducts = products.Where(p => p.IsPopulated).ToList();
            return View(populatedProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> Product(int? id) 
        {
            var list = await _computerPartService.GetAll();
            if (id!=null) 
            {
            list=list.Where(m=>m.CategoryId==id).ToList();
            }
            return View(list);
        }
        public async Task<IActionResult> Detail(int ComputerPartId)
        {
            ViewBag.Feedbacks = await _feedBackService.GetAllByProductId(ComputerPartId);
            var part = await _computerPartService.Get(ComputerPartId);
            return View(part);
        }
        public async Task<IActionResult> CreateFeedback(string message, int id)
        {
            var user = await _accountService.Find(User.Identity.Name);
            FeedBackViewModel model = new()
            {
                ComputerPartId = id,
                Description = message,
                UserId = user.Id,
                Name=user.UserName,
                

            };

            await _feedBackService.Add(model);
            return RedirectToAction("Index");


            //try
            //{
            //    var user = await _accountService.Find(User.Identity.Name);
            //    FeedbackViewModel model = new()
            //    {
            //        ProductId = id,
            //        Description = message,
            //        UserId = user.Id
            //    };

            //    await _feedbackService.Add(model);
            //    return RedirectToAction("Details", new { id });
            //}
            //catch (Exception ex)
            //{
            //    // Hata mesajını loglayın veya kullanıcıya gösterin
            //    Console.WriteLine($"Yorum eklenirken bir hata oluştu: {ex.Message}");
            //    return RedirectToAction("Details", new { id });
            //}
        }
        public IActionResult Contact() { return View(); }
        public IActionResult About() { return View(); }
    }
}
