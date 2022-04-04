using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.UseCases.ShoppingCartScreen
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IShoppingCart m_shoppingCart;
        private readonly IShoppingCartStateStore m_shoppingCartStateStore;

        public DeleteProductUseCase( IShoppingCart shoppingCart, IShoppingCartStateStore shoppingCartStateStore )
        {
            this.m_shoppingCart = shoppingCart;
            this.m_shoppingCartStateStore = shoppingCartStateStore;
        }
        public async Task<Order> Execute( int productId )
        {
            var order = await this.m_shoppingCart.DeleteProductAsync( productId );
            this.m_shoppingCartStateStore.UpdateLineItemsCount();

            return order;
        }
    }
}
