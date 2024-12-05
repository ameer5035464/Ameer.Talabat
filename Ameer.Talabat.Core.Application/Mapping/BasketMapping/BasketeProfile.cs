using Ameer.Talabat.Core.Application.Abstraction.Models.BasketDTOs;
using Ameer.Talabat.Core.Domain.Entities.Basket;
using AutoMapper;

namespace Ameer.Talabat.Core.Application.Mapping.BasketMapping
{
	public class MappingBasket : Profile
	{
		public MappingBasket()
		{
			CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
			CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
		}
	}
}
