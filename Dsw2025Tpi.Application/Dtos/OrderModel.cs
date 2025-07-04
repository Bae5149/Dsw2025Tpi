using Dsw2025Tpi.Domain.Entities;
using Dsw2025Tpi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dsw2025Tpi.Application.Dtos;
    public record OrderModel
    {
        public record OrderRequest(string shippingAdress, string billingAdress, Guid customerId, List<OrderItemModel> OrderItems);//no implementado

        public record OrderResponse(DateTime date, string shippingAdress, string billingAdress, string notes, decimal totalAmount, Guid customerId, Guid OrderId);// copiado de la entidad tal cual
    }
