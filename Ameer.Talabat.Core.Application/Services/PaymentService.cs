using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Basket;
using Ameer.Talabat.Core.Domain.Entities.Order;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Ameer.Talabat.Core.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceManager _serviceManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IServiceManager serviceManager, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _serviceManager = serviceManager;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<CustomerBasketDTO?> CreateOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSettings:Secretkey"];

            var basket = await _serviceManager.BasketServices.GetCustomBasketAsync(basketId);
            var mapBasket = _mapper.Map<CustomerBasket>(basket);

            if (mapBasket == null) throw new NotFoundException("No Basket with this Id"); 

            var deliveryCost = 0M;
            if (mapBasket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(mapBasket.DeliveryMethodId.Value);

                if (deliveryMethod != null) deliveryCost = deliveryMethod.Cost;
            }

            foreach (var item in mapBasket.Items)
            {
                var product = await _serviceManager.ProductServices.GetProductAsync(item.Id);

                if (product.Price != item.Price)
                    item.Price = product.Price;
            }

            var subTotal = mapBasket.Items.Sum(B => B.Price * B.Quantity);

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(mapBasket.PaymentIntentId)) //Create
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)deliveryCost * 100 + (long)subTotal * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await service.CreateAsync(options);
                mapBasket.PaymentIntentId = paymentIntent.Id;
                mapBasket.ClientSecret = paymentIntent.ClientSecret;
            }
            else //Update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)deliveryCost * 100 + (long)subTotal * 100
                };

                paymentIntent = await service.UpdateAsync(mapBasket.PaymentIntentId, options);

                mapBasket.PaymentIntentId = paymentIntent.Id;
                mapBasket.ClientSecret = paymentIntent.ClientSecret;
            }

            var mapBasketToDto = _mapper.Map<CustomerBasketDTO>(mapBasket);
               
            await _serviceManager.BasketServices.UpdateCustomBasketAsync(mapBasketToDto);

            return mapBasketToDto;

        }
    }
}
