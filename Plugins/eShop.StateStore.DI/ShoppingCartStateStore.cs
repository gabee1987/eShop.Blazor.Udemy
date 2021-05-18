using eShop.UseCases.PluginInterfaces.StateStore;
using eShop.UseCases.PluginInterfaces.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.StateStore.DI
{
    public class ShoppingCartStateStore : StateStoreBase, IShoppingCartStateStore
    {
        private readonly IShoppingCart m_shoppingCart;

        public ShoppingCartStateStore( IShoppingCart shoppingCart )
        {
            this.m_shoppingCart = shoppingCart;
        }

        public async Task<int> GetItemsCount()
        {
            var order = await m_shoppingCart.GetOrderAsync();

            if ( order != null && order.LineItems != null && order.LineItems.Count > 0 )
                return order.LineItems.Count;

            return 0;
        }

        public void UpdateLineItemsCount()
        {
            base.BroadCastStateChanged();
        }

        public void UpdateProductQuantity()
        {
            base.BroadCastStateChanged();
        }
    }
}
