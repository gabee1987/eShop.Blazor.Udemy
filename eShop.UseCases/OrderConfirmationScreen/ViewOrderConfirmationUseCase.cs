using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.OrderConfirmationScreen
{
    public class ViewOrderConfirmationUseCase : IViewOrderConfirmationUseCase
    {
        private readonly IOrderRepository m_orderRepository;

        public ViewOrderConfirmationUseCase( IOrderRepository orderRepository )
        {
            this.m_orderRepository = orderRepository;
        }
        public Order Execute( string uniqueId )
        {
            return m_orderRepository.GetOrderByUniqueId( uniqueId );
        }
    }
}
