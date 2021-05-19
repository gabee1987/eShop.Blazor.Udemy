using eShop.CoreBusiness.Models;
using System.Threading.Tasks;

namespace eShop.UseCases.PlaceOrderScreen
{
    public interface IPlaceOrderUseCase
    {
        Task<string> Execute( Order order );
    }
}