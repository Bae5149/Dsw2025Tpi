using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; } // Informativo
        public string Description { get; set; } // Informativo
        public decimal CurrentUnitPrice { get; set; }
    }
}
