using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.ViewProductScreen
{
    public class AddProductToCartUseCase : IAddProductToCartUseCase
    {
        private readonly IProductRepository m_productRepository;
        private readonly IShoppingCart m_shoppingCart;
        private readonly IShoppingCartStateStore m_shoppingCartStateStore;

        public AddProductToCartUseCase(
            IProductRepository productRepository,
            IShoppingCart shoppingCart,
            IShoppingCartStateStore shoppingCartStateStore )
        {
            this.m_productRepository      = productRepository;
            this.m_shoppingCart           = shoppingCart;
            this.m_shoppingCartStateStore = shoppingCartStateStore;
        }

        public async void Execute( int productId )
        {
            var product = m_productRepository.GetProduct( productId );
            await m_shoppingCart.AddProductAsync( product );

            // Update shopping cart state
            m_shoppingCartStateStore.UpdateLineItemsCount();
        }
    }
}
