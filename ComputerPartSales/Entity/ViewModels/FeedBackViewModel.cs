using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModels
{
    public class FeedBackViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public int ComputerPartId { get; set; }
        public string Name { get; set; }

    }
}
