using CoffeeOnDemandSolution.Common.Reponses;

namespace CoffeeOnDemandSolution.PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        public async Task<BaseResponse<bool>> PayWithPix()
        {
            Console.WriteLine("Paying with PIX");
            await Task.Delay(5000);
            var randomStatus = new Random().Next(0, 10) > 5;

            if (randomStatus)
            {
                Console.WriteLine("Payment approved");
                return BaseResponse<bool>.CreateValidResponse(true);
            }

            return BaseResponse<bool>.CreateInvalidResponse("Payment denied");

        }

        public async Task<BaseResponse<bool>> PayWithCreditCard()
        {
            Console.WriteLine("Paying with CreditCard");

            await Task.Delay(5000);
            var randomStatus = new Random().Next(0, 10) > 5;

            if (randomStatus)
            {
                Console.WriteLine("Payment approved");
                return BaseResponse<bool>.CreateValidResponse(true);
            }

            return BaseResponse<bool>.CreateInvalidResponse("Payment denied");
        }

        public async Task<BaseResponse<bool>> PayWithVoucher()
        {
            Console.WriteLine("Paying with Voucher");
            var randomStatus = new Random().Next(0, 10) > 5;

            await Task.Delay(5000);

            if (randomStatus)
            {
                Console.WriteLine("Payment approved");
                return BaseResponse<bool>.CreateValidResponse(true);
            }

            return BaseResponse<bool>.CreateInvalidResponse("Payment denied");
        }

    }
}
