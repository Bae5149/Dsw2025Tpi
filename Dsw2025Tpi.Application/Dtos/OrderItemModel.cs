using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;

    public record OrderItemModel(Guid productId, int quantity, string name, string description, decimal currentUnitPrice);

