using eShop.CoreBusiness.Models;
using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class PlaceOrderUseCase : IPlaceOrderUseCase
    {
        private readonly IOrderService m_orderService;
        private readonly IOrderRepository m_orderRepository;
        private readonly IShoppingCart m_shoppingCart;
        private readonly IShoppingCartStateStore m_shoppingCartStateStore;

        public PlaceOrderUseCase(
            IOrderService orderService,
            IOrderRepository orderRepository,
            IShoppingCart shoppingCart,
            IShoppingCartStateStore shoppingCartStateStore)
        {
            this.m_orderService           = orderService;
            this.m_orderRepository        = orderRepository;
            this.m_shoppingCart           = shoppingCart;
            this.m_shoppingCartStateStore = shoppingCartStateStore;
        }
        public async Task<string> Execute( Order order )
        {
            if ( m_orderService.ValidateCreateOrder( order ) )
            {
                order.DatePlaced = DateTime.Now;
                order.UniqueId   = Guid.NewGuid().ToString();
                m_orderRepository.CreateOrder( order );

                await m_shoppingCart.EmptyAsync();
                m_shoppingCartStateStore.UpdateLineItemsCount();

                return order.UniqueId;
            }

            return null;
        }
    }
}
