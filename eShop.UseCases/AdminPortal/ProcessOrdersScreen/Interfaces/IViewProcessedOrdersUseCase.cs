using eShop.CoreBusiness.Models;
using System.Collections.Generic;

namespace eShop.UseCases.AdminPortal.ProcessOrdersScreen
{
    public interface IViewProcessedOrdersUseCase
    {
        IEnumerable<Order> Execute();
    }
}