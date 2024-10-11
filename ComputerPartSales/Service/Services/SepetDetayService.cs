using Entity.Services;
using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class SepetDetayService:ISepetDetayService
	{
		public List<SepetDetay> SepeteEkle(List<SepetDetay> sepet, SepetDetay siparis)
		{
			if (sepet.Any(s => s.ComputerPartId == siparis.ComputerPartId))
			{
				foreach (var item in sepet)
				{
					//aynı ürünü bulup, miktarını sipariş miktarı kadar artırıyoruz.
					if (item.ComputerPartId == siparis.ComputerPartId)
						item.ComputerPartQuantity += siparis.ComputerPartQuantity;
				}
			}
			else
			{
				//yeni ürün, ilk defa sepete atılacak.
				sepet.Add(siparis);
			}
			return sepet;
		}
		public List<SepetDetay> SepettenSil(List<SepetDetay> sepet, int id)
		{
			sepet.RemoveAll(s => s.ComputerPartId == id);
			return sepet;
		}
		public int ToplamAdet(List<SepetDetay> sepet)
		{
			var toplamAdet = sepet.Sum(s => s.ComputerPartQuantity);
			return toplamAdet;
		}
		public decimal ToplamTutar(List<SepetDetay> sepet)
		{
			var toplamTutar = sepet.Sum(s => s.ComputerPartQuantity * s.ComputerPartPrice);
			return toplamTutar;
		}
	}
}
