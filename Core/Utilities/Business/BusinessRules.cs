using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public  class BusinessRules
    {
        public static IResult Run(params IResult[] results)
        {
            SuccessResult successResult = new();
            foreach (var result in results)
            {
                if (!result.Success)
                {
                    return result;
                }

                successResult.Message = result.Message;
            }
            return successResult;
        }
       
    }
}
