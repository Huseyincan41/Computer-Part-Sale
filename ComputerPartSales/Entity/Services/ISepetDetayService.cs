using Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
	public interface ISepetDetayService
	{
		List<SepetDetay> SepeteEkle(List<SepetDetay> sepet, SepetDetay siparis);
		List<SepetDetay> SepettenSil(List<SepetDetay> sepet, int id);
		int ToplamAdet(List<SepetDetay> sepet);
		decimal ToplamTutar(List<SepetDetay> sepet);
	}
}
