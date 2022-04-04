using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class UpdateQuantityUseCase : IUpdateQuantityUseCase
    {
        private readonly IShoppingCart m_shoppingCart;
        private readonly IShoppingCartStateStore m_shoppingCartStateStore;

        public UpdateQuantityUseCase( IShoppingCart shoppingCart, IShoppingCartStateStore shoppingCartStateStore )
        {
            this.m_shoppingCart = shoppingCart;
            this.m_shoppingCartStateStore = shoppingCartStateStore;
        }
        public async Task<Order> Execute( int productid, int quantity )
        {
            var order = await m_shoppingCart.UpdateQuantityAsync( productid, quantity );
            m_shoppingCartStateStore.UpdateProductQuantity();

            return order;
        }
    }
}
