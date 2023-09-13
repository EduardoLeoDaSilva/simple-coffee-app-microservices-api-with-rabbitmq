using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeOnDemandSolution.Common.Reponses
{
    public class BaseResponse<E>
    {
        public bool Success { get; private set; }
        public E Data { get; private set; }
        public string Message { get; private set; }

        public static BaseResponse<E> CreateValidResponse(E data)
        {
            return new BaseResponse<E> { Data = data, Success = true };
        }

        public static BaseResponse<E> CreateValidResponse(E data, string message)
        {
            return new BaseResponse<E> { Data = data, Success = true, Message = message };

        }

        public static BaseResponse<E> CreateInvalidResponse(string message)
        {
            return new BaseResponse<E> { Success = true, Message = message };
        }

    }
}
