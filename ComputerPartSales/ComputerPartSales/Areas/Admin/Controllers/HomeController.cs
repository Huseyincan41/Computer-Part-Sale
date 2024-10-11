using Entity.Services;
using Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service.Services;
using System.Collections.Generic;

namespace ComputerPartSales.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly IComputerPartService _computerPartService;
        private readonly ICategoryService _categoryService;
        private readonly IAccountService _accountService;
        private readonly IComputerPartSaleService _computerPartSaleService;
        public HomeController(IComputerPartService computerPartService, ICategoryService categoryService, IAccountService accountService, IComputerPartSaleService computerPartSaleService)
        {
            _computerPartService = computerPartService;
            _categoryService = categoryService;
            _accountService = accountService;
            _computerPartSaleService = computerPartSaleService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> List(int? id, string? search)
        {
            var list = await _computerPartService.GetAll();

            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(a => a.Name.ToLower().Contains(search.ToLower().Trim())).ToList();
            }

            if (id.HasValue)
            {
                list = list.Where(p => p.CategoryId == id.Value).ToList();
                ViewBag.SelectedCategoryId = id.Value;
            }
            else
            {
                ViewBag.SelectedCategoryId = null;
            }

            return View(list);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComputerPartViewModel model, IFormFile formFile)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", formFile.FileName);
            var stream = new FileStream(path, FileMode.Create);
            formFile.CopyTo(stream);
            model.ImageUrl = "/images/" + formFile.FileName;
            await _computerPartService.Add(model);
            return RedirectToAction("List");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _computerPartService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            await _computerPartService.Delete(product);
            return RedirectToAction("Index");
        }
        //[Route("Admin/Home/Edit/{id}")]
        public async Task<IActionResult> Edit(int ComputerPartId)
        {

            var product = await _computerPartService.Get(ComputerPartId);

            
            var categories = await _categoryService.GetAll();
            ViewBag.Categories = new SelectList(categories, "CategoryId", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ComputerPartViewModel model, IFormFile? formFile)
        {
           
            if (formFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//images", formFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                model.ImageUrl = "/images/" + formFile.FileName;
            }

            await _computerPartService.Update(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Users()
        {
            var users = await _accountService.GetAllUsers();
            return View(users);
        }
        public async Task<IActionResult> ShowSales()
        {
            var list = await _computerPartSaleService.GetAll();
            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> SalesDelete(int id)
        {
            var product = await _computerPartSaleService.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            await _computerPartSaleService.Delete(product);
            return RedirectToAction("ShowSales");
        }
    }
}
