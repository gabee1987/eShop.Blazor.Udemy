using eShop.CoreBusiness.Services;
using eShop.UseCases.PluginInterfaces.DataStore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.UseCases.AdminPortal.OrderDetailScreen
{
    public class ProcessOrderUseCase : IProcessOrderUseCase
    {
        private readonly IOrderRepository m_orderRepository;
        private readonly IOrderService m_orderService;

        public ProcessOrderUseCase( IOrderRepository orderRepository, IOrderService orderService )
        {
            this.m_orderRepository = orderRepository;
            this.m_orderService = orderService;
        }

        public bool Execute( int orderId, string adminUserName )
        {
            var order = m_orderRepository.GetOrder( orderId );
            order.AdminUser = adminUserName;
            order.DateProcessed = DateTime.Now;

            if ( m_orderService.ValidateProcessOrder( order ) )
            {
                m_orderRepository.UpdateOrder( order );
                return true;
            }

            return false;
        }
    }
}
