using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.DeliveryOffices;
using TravelLine.Food.Domain.DishQuotas;
using TravelLine.Food.Domain.Orders;

namespace TravelLine.Food.Core.DishQuotas
{
    public class DishQuotaService : IDishQuotaService
    {
        private const int DefaultQuotaValue = 999;

        private readonly IDishQuotaRepository _dishQuotaRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly IUserService _userService;

        public DishQuotaService(
            IDishQuotaRepository dishQuotaRepository,
            IOrderRepository orderRepository,
            IDeliveryOfficeService deliveryOfficeService,
            IUserService userService )
        {
            _dishQuotaRepository = dishQuotaRepository;
            _orderRepository = orderRepository;
            _deliveryOfficeService = deliveryOfficeService;
            _userService = userService;
        }

        public DishQuotaModel GetDishQuota( int dishId, DateTime date )
        {
            DishQuota dishQuota = _dishQuotaRepository.GetDishQuota( dishId, date );

            return new DishQuotaModel()
            {
                DishId = dishId,
                Date = date,
                Quota = dishQuota != null ? dishQuota.Quota : DefaultQuotaValue
            };
        }

        public int GetAvailableDishQuota( int userId, DateTime date, DishModel dish )
        {
            List<DeliveryOffice> deliveryOffices = _deliveryOfficeService.GetDeliveryOffices();
            DishQuotaModel quota = GetDishQuota( dish.Id, date );

            int orderedDishes = _orderRepository.GetOrderedDishesInOffice( date, dish.Type, dish.Id );
            return quota.Quota - orderedDishes;
        }

        public void SetDishQuota( int dishId, DateTime date, int quota )
        {
            date = date.Date;
            DishQuota dishQuota = _dishQuotaRepository.GetDishQuota( dishId, date );

            if ( dishQuota == null )
            {
                dishQuota = new DishQuota()
                {
                    DishId = dishId,
                    Date = date,
                    Quota = quota
                };
            }
            else
            {
                dishQuota.Quota = quota;
            }

            _dishQuotaRepository.Save( dishQuota );
        }

        public void RemoveDishQuota( int dishId, DateTime date )
        {
            DishQuota dishQuota = _dishQuotaRepository.GetDishQuota( dishId, date );
            if ( dishQuota != null )
            {
                _dishQuotaRepository.Remove( dishQuota );
            }
        }
    }
}
