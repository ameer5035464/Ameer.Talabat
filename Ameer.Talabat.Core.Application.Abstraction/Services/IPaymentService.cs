using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;
using Ameer.Talabat.Core.Domain.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ameer.Talabat.Core.Application.Abstraction.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasketDTO?> CreateOrUpdatePaymentIntent(string basketId);
    }
}
