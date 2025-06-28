using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;
    public record ProductModel
    {
    //public record Request(string Sku, string Name, decimal Price);
    public record Request(string sku, string internalCode, string name, string description, decimal price, int stockQuantity);
    public record Response(Guid Id, string? Sku, string? Name, decimal Price);
    }
