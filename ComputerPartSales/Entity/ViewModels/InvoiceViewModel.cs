using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class InvoiceViewModel
    {
        public UserViewModel userViewModel { get; set; }
        public ComputerPartSaleViewModel satisViewModel { get; set; }
        public List<SepetDetay> sepetDetayListesi { get; set; }
    }
}
