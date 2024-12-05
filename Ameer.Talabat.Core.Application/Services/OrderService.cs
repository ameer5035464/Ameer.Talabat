using Ameer.Talabat.Core.Application.Abstraction.Models.Orders;
using Ameer.Talabat.Core.Application.Abstraction.Services;
using Ameer.Talabat.Core.Application.Specifications;
using Ameer.Talabat.Core.Domain.Contracts;
using Ameer.Talabat.Core.Domain.Entities.Order;
using AutoMapper;

namespace Ameer.Talabat.Core.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IServiceManager _serviceManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IServiceManager serviceManager, IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _serviceManager = serviceManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, OrderAddress address)
        {
            var getBasket = await _serviceManager.BasketServices.GetCustomBasketAsync(basketId);
            var orderItems = new List<OrderItem>();
            if (getBasket?.Items.Count() > 0)
            {
                foreach (var item in getBasket.Items)
                {
                    var product = await _serviceManager.ProductServices.GetProductAsync(item.Id);

                    var newOrderItem = new OrderItem(product.Id, product.Name, product.PictureUrl!, product.Price, item.Quantity);
                    orderItems.Add(newOrderItem);
                }
            }
            var subTotal = orderItems.Sum(S => S.Quantity * S.Price);
            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(deliveryMethodId);

            var spec = new OrderPaymentIntentCheckSpecification(getBasket!.PaymentIntentId!);

            var exOrder = await _unitOfWork.GetRepository<Order, int>().GetSpecAsync(spec);

            if (exOrder != null)
            {
                _unitOfWork.GetRepository<Order, int>().Delete(exOrder);
                await _paymentService.CreateOrUpdatePaymentIntent(basketId);
            }


            var newOrder = new Order(buyerEmail, address, deliveryMethod!, orderItems, subTotal, getBasket!.PaymentIntentId!);

            await _unitOfWork.GetRepository<Order, int>().AddAsync(newOrder);
            await _unitOfWork.CompletedAsync();

            return newOrder;
        }

        public async Task<IEnumerable<OrderDto?>> GetAllUserOrdersAsync(string buyerEmail)
        {
            var spec = new OrderSpecifications(buyerEmail);

            var order = await _unitOfWork.GetRepository<Order, int>().GetAllSpecAsync(spec);
            if (order != null)
            {
                var mapOrder = _mapper.Map<IEnumerable<OrderDto>>(order);
                return mapOrder;
            }

            else return Enumerable.Empty<OrderDto>();
        }

        public async Task<OrderDto?> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            var spec = new OrderSpecifications(buyerEmail, orderId);

            var getOrder = await _unitOfWork.GetRepository<Order, int>().GetSpecAsync(spec);

            if (getOrder != null)
            {
                var mapOrder = _mapper.Map<OrderDto>(getOrder);
                return mapOrder;
            }
            throw new NotFoundException($"This  Order is not exist");
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMetodsAsync()
        {
            var deliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();

            if (deliveryMethods != null)
                return deliveryMethods;
            else
                return Enumerable.Empty<DeliveryMethod>();

        }
    }
}
