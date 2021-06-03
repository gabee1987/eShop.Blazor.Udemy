using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.AdminPortal.OrderDetailScreen
{
    public class ViewOrderDetailUseCase : IViewOrderDetailUseCase
    {
        private readonly IOrderRepository m_orderRepository;

        public ViewOrderDetailUseCase( IOrderRepository orderRepository )
        {
            this.m_orderRepository = orderRepository;
        }

        public Order Execute( int orderId )
        {
            return m_orderRepository.GetOrder( orderId );
        }
    }
}
