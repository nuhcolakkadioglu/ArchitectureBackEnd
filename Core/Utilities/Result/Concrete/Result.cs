using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result.Concrete
{
    public class Result : IResult
    {
        public Result(bool success, string message):this(success)
        {
            Success = success;
            Message = message;
        }

        public Result(bool success) 
        {

            Success = success;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
