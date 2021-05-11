using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.ViewShoppingCartUseCase
{
    public interface IViewShoppingCartUseCase
    {
        Task<Order> Execute();
    }
}