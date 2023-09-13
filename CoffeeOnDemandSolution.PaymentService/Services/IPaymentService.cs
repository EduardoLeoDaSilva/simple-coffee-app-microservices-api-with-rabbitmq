using CoffeeOnDemandSolution.Common.Reponses;

namespace CoffeeOnDemandSolution.PaymentService.Services
{
    public interface IPaymentService
    {
        Task<BaseResponse<bool>> PayWithCreditCard();
        Task<BaseResponse<bool>> PayWithPix();
        Task<BaseResponse<bool>> PayWithVoucher();
    }
}