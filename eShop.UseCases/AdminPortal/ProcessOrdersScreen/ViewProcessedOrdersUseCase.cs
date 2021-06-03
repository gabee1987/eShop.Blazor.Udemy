using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.AdminPortal.ProcessOrdersScreen
{
    public class ViewProcessedOrdersUseCase : IViewProcessedOrdersUseCase
    {
        private readonly IOrderRepository m_orderRepository;

        public ViewProcessedOrdersUseCase( IOrderRepository orderRepository )
        {
            this.m_orderRepository = orderRepository;
        }

        public IEnumerable<Order> Execute()
        {
            return m_orderRepository.GetProcessedOrders();
        }
    }
}
