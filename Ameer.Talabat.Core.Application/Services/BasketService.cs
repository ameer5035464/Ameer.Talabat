using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Basket;
using AutoMapper;

namespace Ameer.Talabat.Core.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDTO> GetCustomBasketAsync(string basketId)
        {
            var basket = await _basketRepository.GetAsync(basketId);

            if (basket == null)
            {
                throw new NotFoundException($"Cannot get Basket with this ID: {basketId}");
            }

            return _mapper.Map<CustomerBasketDTO>(basket);
        }


        public async Task<CustomerBasketDTO> UpdateCustomBasketAsync(CustomerBasketDTO customBasket)
        {
            var basket = _mapper.Map<CustomerBasket>(customBasket);
            var value = await _basketRepository.UpdateAsync(basket);

            if (value == null)
            {
                throw new NotFoundException($"Cannot Update Basket with this ID: {customBasket.Id}");
            }

            return customBasket;

        }

        public async Task DeleteCustomBasketAsync(string basketId)
        {
            var isDeleted = await _basketRepository.DeleteAsync(basketId);

            if (!isDeleted)
            {
                throw new NotFoundException($"Cannot Delete Basket with this ID: {basketId}");
            }
        }
    }
}
