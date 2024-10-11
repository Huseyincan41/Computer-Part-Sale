using Entity.Services;
using Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ComputerPartSales.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        // Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // Kategorileri listeleme aksiyonu
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAll();
            return View(categories); // View'e kategoriler listesini gönderiyoruz
        }

        // Yeni kategori ekleme formunu gösteren GET aksiyonu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kategori ekleyen POST aksiyonu
        [HttpPost]
        
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(model); // Yeni kategori ekleme işlemi
                return RedirectToAction("Index"); // İşlem başarılı olursa kategoriler listesine yönlendir
            }
            return View(model); // Hata varsa tekrar formu göster
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = (await _categoryService.GetAll()).FirstOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category); // Silinecek kategoriyi gösteren sayfayı renderla
        }

        // Kategori silme POST aksiyonu
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int CategoryId)
        {
            var category = (await _categoryService.GetAll()).FirstOrDefault(x => x.CategoryId == CategoryId);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.Delete(category); // Kategoriyi sil
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = (await _categoryService.GetAll()).FirstOrDefault(x => x.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category); // Güncellenecek kategori bilgilerini gösteren sayfayı renderla
        }

        // Kategori güncelleme POST aksiyonu
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Update(model); // Kategori güncelleme işlemi
                return RedirectToAction("Index"); // İşlem başarılı olursa kategoriler listesine yönlendir
            }
            return View(model); // Hata varsa tekrar formu göster
        }
    }
}
