using AutoMapper;
using Entity.Entities;
using Entity.Services;
using Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Service.Extensions;
using Service.Services;

namespace ComputerPartSales.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IComputerPartSaleDetailService _computerPartSaleDetailService;
        private readonly IComputerPartSaleService _computerPartSaleService;
        private readonly IAccountService _accountService;
        private readonly ISepetDetayService _sepetDetayService;
        private readonly IMapper _mapper;
		public PaymentController(IComputerPartSaleDetailService computerPartSaleDetailService, IComputerPartSaleService computerPartSaleService, IAccountService accountService, ISepetDetayService sepetDetayService, IMapper mapper)
		{
			_computerPartSaleDetailService = computerPartSaleDetailService;
			_computerPartSaleService = computerPartSaleService;
			_accountService = accountService;
			_sepetDetayService = sepetDetayService;
			_mapper = mapper;
		}

		public IActionResult ConfirmAddress()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmAddress(UserViewModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                TempData["FirstName"] = model.FirstName;
                TempData["Email"] = model.Email;
                TempData["PhoneNumber"] = model.PhoneNumber;
                TempData["Address"] = model.Address;
                return RedirectToAction("ConfirmPayment");
            }
            TempData["mesaj"] = "Giriş yapılmadı , öncelikli olarak giriş yapınız!";

            return View("MessageShow");
        }
        //      public async Task<IActionResult> ConfirmPayment()
        //      {
        //          var sepet = HttpContext.Session.GetJson<List<SepetDetay>>("sepet");
        //          int toplamAdet = _sepetDetayService.ToplamAdet(sepet);
        //          decimal toplamTutar = _sepetDetayService.ToplamTutar(sepet);

        //          var username = User.Identity.Name;
        //          var user = await _accountService.Find(username);
        //          ComputerPartSaleViewModel pSale = new ComputerPartSaleViewModel()
        //          {
        //              Date = DateTime.Now,
        //              Quantity = toplamAdet,
        //              Price = toplamTutar,
        //          };
        //          InvoiceViewModel model = new InvoiceViewModel()
        //          {
        //              userViewModel = await _accountService.Find(username),
        //              satisViewModel = pSale,
        //              sepetDetayListesi = sepet,
        //          };
        //          model.userViewModel.FirstName = TempData["FirstName"]?.ToString();
        //          model.userViewModel.Email = TempData["Email"]?.ToString();
        //          model.userViewModel.PhoneNumber = TempData["PhoneNumber"]?.ToString();
        //          model.userViewModel.Address = TempData["Address"]?.ToString();
        //          return View(model);
        //      }
        //[HttpPost]
        //public async Task<IActionResult> ConfirmPayment(InvoiceViewModel model)
        //{
        //	// Satış detaylarını session'dan alıyoruz.
        //	var sepet = HttpContext.Session.GetJson<List<SepetDetay>>("sepet");
        //	if (sepet == null || sepet.Count == 0)
        //	{
        //		TempData["mesaj"] = "Sepetiniz boş, lütfen ürün ekleyin!";
        //		return RedirectToAction("ConfirmPayment");
        //	}

        //	// Kullanıcı bilgilerini alıyoruz
        //	var username = User.Identity.Name;
        //	var user = await _accountService.Find(username);

        //	// Yeni bir ComputerPartSale ekliyoruz
        //	ComputerPartSaleViewModel computerPartSaleViewModel = new ComputerPartSaleViewModel
        //	{
        //		UserId = user.Id,
        //		Date = DateTime.Now,
        //		Quantity = _sepetDetayService.ToplamAdet(sepet),
        //		Price = _sepetDetayService.ToplamTutar(sepet)
        //	};

        //	// Satış ID'sini ekliyoruz
        //	var satisId = _computerPartSaleService.AddSale(computerPartSaleViewModel);

        //	// Sepetteki ürünleri ComputerPartSaleDetail tablosuna ekliyoruz
        //	foreach (var item in sepet)
        //	{
        //		ComputerPartSaleDetailViewModel saleDetail = new ComputerPartSaleDetailViewModel
        //		{
        //			Number = item.ComputerPartQuantity,
        //			UnitPrice = item.ComputerPartPrice,
        //			ComputerPartId = item.ComputerPartId,
        //			ComputerPartSaleId = satisId ,// Az önce eklediğimiz satışın ID'si

        //		};

        //		// Her bir ürünü satış detay tablosuna ekle
        //		_computerPartSaleDetailService.Add(saleDetail);
        //	}

        //	// Satış işlemi tamamlandı, mesaj ve session temizliği
        //	TempData["mesaj"] = "Satış ve detaylar başarıyla kaydedildi!";
        //	HttpContext.Session.Remove("sepet");

        //	return View("MessageShow");
        //}
        // 5. Satış detaylarını kaydet
        //bool satisDetayEklendi = _computerPartSaleDetailService.AddRange(sepet, satisId);

        public async Task<IActionResult> ConfirmPayment()
        {
            var sepet = HttpContext.Session.GetJson<List<SepetDetay>>("sepet");
            int toplamAdet = _sepetDetayService.ToplamAdet(sepet);
            decimal toplamTutar = _sepetDetayService.ToplamTutar(sepet);

            var username = User.Identity.Name;
            var user = await _accountService.Find(username);
            ComputerPartSaleViewModel pSale = new ComputerPartSaleViewModel()
            {
                UserId = user.Id,
                Date = DateTime.Now,
                Quantity = toplamAdet,
                Price = toplamTutar,
                
                
            };
            InvoiceViewModel model = new InvoiceViewModel()
            {
                userViewModel = await _accountService.Find(username),
                satisViewModel = pSale,
                sepetDetayListesi = sepet,
            };
            model.userViewModel.FirstName = TempData["FirstName"]?.ToString();
            
            model.userViewModel.Email = TempData["Email"]?.ToString();
            model.userViewModel.PhoneNumber = TempData["PhoneNumber"]?.ToString();
            model.userViewModel.Address = TempData["Address"]?.ToString();
            return View(model);
        }
       
        [HttpPost]
        public async Task<IActionResult> ConfirmPayment(InvoiceViewModel model)
        {
            
            // Satış detaylarını session'dan alıyoruz.
            var sepet = HttpContext.Session.GetJson<List<SepetDetay>>("sepet");
            //int toplamAdet = _sepetDetayService.ToplamAdet(sepet);
            //decimal toplamTutar = _sepetDetayService.ToplamTutar(sepet);
           
            

            
            

            // Satış ID'sini ekliyoruz
            var satisId =await  _computerPartSaleService.Add(model.satisViewModel);

            if ( await _computerPartSaleDetailService.AddRange(sepet, satisId))
            {
                TempData["mesaj"] = "Satış işlemi başarıyla tamamlandı";
                HttpContext.Session.Remove("sepet");
            }
            else
            {
                TempData["mesaj"] = "!!!!!!!!!!!!Satış işlemi gerçekleşmedi bilgilerinizi kontrol edin!";
            }

            return View("MessageShow");



            
        }
       
        
        [HttpPost]
        public async Task<IActionResult> Sale()
        {
            
            

            // 2. Sepet bilgilerini al
            var sepet = HttpContext.Session.GetJson<List<SepetDetay>>("sepet");
            int toplamAdet = _sepetDetayService.ToplamAdet(sepet);
            decimal toplamTutar = _sepetDetayService.ToplamTutar(sepet);

            // 3. Kullanıcı bilgilerini al
            var username = User.Identity.Name;
            var user = await _accountService.Find(username);          

            // 4. Satış bilgilerini kaydet
            ComputerPartSaleViewModel computerPartSaleViewModel = new ComputerPartSaleViewModel
            {
                UserId = user.Id,
                Date = DateTime.Now,
                Quantity = toplamAdet,
                Price = toplamTutar,
                
            };

            var satisId = _computerPartSaleService.AddSale(computerPartSaleViewModel);
            bool detayKaydedildi =await _computerPartSaleDetailService.AddRange(sepet, satisId);

            if (!detayKaydedildi)
            {
                TempData["mesaj"] = "Satış detayları kaydedilemedi.";
                return RedirectToAction("Index", "Home");
            }

            // 6. Sepet'i temizle ve başarı mesajı
            

            // 7. Fatura görünümünü oluştur
            InvoiceViewModel invoice = new InvoiceViewModel
            {
                userViewModel = new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    // Diğer kullanıcı bilgileri eklenebilir
                },
                satisViewModel = computerPartSaleViewModel,
                sepetDetayListesi = sepet
            };

            // 8. Fatura görünümünü döndür
            return View("MessageShow");





        }
    }
}
