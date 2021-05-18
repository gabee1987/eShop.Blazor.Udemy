using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataStore.HardCoded
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<int, Order> m_orders;
        public OrderRepository()
        {
            m_orders = new Dictionary<int, Order>();
        }

        public int CreateOrder( Order order )
        {
            order.OrderId  = m_orders.Count + 1;
            m_orders.Add( order.OrderId.Value, order );
            return order.OrderId.Value;
        }

        public IEnumerable<OrderLineItem> GetLineItemByOrderId( int orderId )
        {
            throw new NotImplementedException();
        }

        public Order GetOrder( int id )
        {
            return m_orders[id];
        }

        public Order GetOrderByUniqueId( string uniqueId )
        {
            foreach ( var order in m_orders )
            {
                if ( order.Value.UniqueId == uniqueId )
                    return order.Value;
            }

            return null;
        }

        public IEnumerable<Order> GetOrders()
        {
            return m_orders.Values;
        }

        public IEnumerable<Order> GetOutStandingOrders()
        {
            var allOrders = m_orders.Values as IEnumerable<Order>;
            return allOrders.Where( o => o.DateProcessed.HasValue == false );
        }

        public IEnumerable<Order> GetProcessedOrders()
        {
            var allOrders = m_orders.Values as IEnumerable<Order>;
            return allOrders.Where( o => o.DateProcessed.HasValue );
        }

        public void UpdateOrder( Order order )
        {
            if ( order == null || !order.OrderId.HasValue )
                return;

            var ord = m_orders[order.OrderId.Value];
            if ( ord == null )
                return;

            m_orders[order.OrderId.Value] = order;
        }
    }
}
