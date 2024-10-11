using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class ComputerPartViewModel
    {
        public int ComputerPartId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; } = 1;
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public bool IsPopulated { get; set; } = false;
        public string ImageUrl { get; set; } = string.Empty;
	}
}
