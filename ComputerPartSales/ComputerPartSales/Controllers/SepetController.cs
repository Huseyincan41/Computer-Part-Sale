using Entity.Services;
using Entity.ViewModels;


using Microsoft.AspNetCore.Mvc;
using Service.Extensions;

namespace ComputerPartSales.Controllers
{
	public class SepetController : Controller
	{
		private readonly IComputerPartService _computerPartService;
		private readonly ISepetDetayService  _sepetDetayService;
		List<SepetDetay> sepet;
		public SepetController(IComputerPartService computerPartService, ISepetDetayService sepetDetayService)
		{
			_computerPartService = computerPartService;
			_sepetDetayService = sepetDetayService;
		}

		public IActionResult Index()
		{
			if (User.Identity.IsAuthenticated)
			{
				sepet = SepetAl();
				TempData["ToplamAdet"] = _sepetDetayService.ToplamAdet(sepet);

				if (_sepetDetayService.ToplamTutar(sepet) > 0)
					TempData["ToplamTutar"] = _sepetDetayService.ToplamTutar(sepet);

				return View(sepet);
			}
			TempData["mesaj"] = "Giriş yapılmadı , öncelikli olarak giriş yapınız!";

			return View("MessageShow");
		}
		public async Task<IActionResult> Ekle(int Id, int Adet)
		{

			var computerPart = await _computerPartService.Get(Id);
			sepet = SepetAl();
			SepetDetay siparis = new SepetDetay();
			siparis.ComputerPartId = computerPart.ComputerPartId;
			siparis.ComputerPartName = computerPart.Name;
			siparis.ComputerPartQuantity = Adet;
			siparis.ComputerPartPrice = computerPart.Price;
			sepet = _sepetDetayService.SepeteEkle(sepet, siparis);
			SepetKaydet(sepet);
			return RedirectToAction("Index");
		}
		public IActionResult Sil(int id)
		{
			sepet = SepetAl();
			sepet = _sepetDetayService.SepettenSil(sepet, id);
			SepetKaydet(sepet);
			return RedirectToAction("Index");
		}
		public IActionResult SepetSil()
		{
			//HttpContext.Session.Clear(); //Oturumda bulunan tüm session'ları siler.
			HttpContext.Session.Remove("sepet"); //Sadece ismi belirtilen session'ı siler.
			return RedirectToAction("Index");
		}
		public List<SepetDetay> SepetAl()
		{
			var sepet = HttpContext.Session.GetJson<List<SepetDetay>>("sepet") ?? new List<SepetDetay>();

			return sepet;
		}
		public void SepetKaydet(List<SepetDetay> sepet)
		{
			HttpContext.Session.SetJson("sepet", sepet);
		}
	}
}
