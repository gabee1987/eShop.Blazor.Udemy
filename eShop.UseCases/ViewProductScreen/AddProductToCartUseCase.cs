using eShop.UseCases.PluginInterfaces.DataStore;
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
        public AddProductToCartUseCase(
            IProductRepository productRepository,
            IShoppingCart shoppingCart )
        {
            this.m_productRepository = productRepository;
            this.m_shoppingCart = shoppingCart;
        }

        public async void Execute( int productId )
        {
            var product = m_productRepository.GetProduct( productId );
            await m_shoppingCart.AddProductAsync( product );
        }
    }
}
