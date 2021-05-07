using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using eShop.UseCases.PluginInterfaces.UI;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.ShoppingCart.LocalStorage
{
    public class ShoppingCart : IShoppingCart
    {
        private const string m_cstrShoppingCart = "eShop.ShoppingCart";
        private readonly IJSRuntime m_jSRuntime;
        private readonly IProductRepository m_productRepository;

        public ShoppingCart( IJSRuntime jSRuntime, IProductRepository productRepository )
        {
            this.m_jSRuntime         = jSRuntime;
            this.m_productRepository = productRepository;
        }

        public async Task<Order> AddProductAsync( Product product )
        {
            var order = await GetOrder();
            order.AddProduct( product.ProductId, 1, product.Price );
            await SetOrder( order );

            return order;
        }

        public async Task<Order> DeleteProductAsync( int productId )
        {
            var order = await GetOrder();
            order.RemoveProduct( productId );
            await SetOrder( order );

            return order;
        }

        public Task EmptyAsync()
        {
            return this.SetOrder( null );
        }

        public async Task<Order> GetOrderAsync()
        {
            return await GetOrder();
        }

        public Task<Order> PlaceOrderAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Order> UpdateOrderAsync( Order order )
        {
            await this.SetOrder( order );
            return order;
        }

        public async Task<Order> UpdateQuantityAsync( int productId, int quantity )
        {
            var order = await GetOrder();
            if ( quantity < 0 )
                return order;
            else if ( quantity == 0 )
                return await DeleteProductAsync( productId );

            var lineItem = order.LineItems.SingleOrDefault( li => li.ProductId == productId );
            if ( lineItem != null )
                lineItem.Quantity = quantity;

            await SetOrder( order );

            return order;
        }



        private async Task<Order> GetOrder()
        {
            Order order = null;

            var strOrder = await m_jSRuntime.InvokeAsync<string>( "localStorage.getItem", m_cstrShoppingCart );

            if ( !string.IsNullOrWhiteSpace( strOrder ) && strOrder.ToLower() != "null" )
                order = JsonConvert.DeserializeObject<Order>( strOrder );
            else
            {
                order = new Order();
                await SetOrder( order );
            }

            foreach ( var item in order.LineItems )
            {
                item.Product = m_productRepository.GetProduct( item.ProductId );
            }

            return order;
        }

        private async Task SetOrder( Order order )
        {
            await m_jSRuntime.InvokeVoidAsync( "localStorage.setItem", m_cstrShoppingCart, JsonConvert.SerializeObject( order ) );
        }
    }
}
