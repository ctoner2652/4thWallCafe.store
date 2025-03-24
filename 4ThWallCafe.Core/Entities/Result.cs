using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4ThWallCafe.Core.Entities
{
    public class Result
    {
        public bool Ok { get; set; }
        public string Message { get; set; }

        public Result (bool success, string message)
        {
            Ok = success; 
            Message = message;
        }
    }

    public class Result<T> : Result
    {
        public T? Data {  get; set; }
        public Result(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }
    }

    public class ResultFactory
    {
        public static Result Success()
        {
            return new Result(true, string.Empty);
        }

        public static Result Fail(string message)
        {
            return new Result (false, message);
        }

        public static Result<T> Success<T>(T data)
        {
            return new Result<T>(data, true, string.Empty);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }
    }

}
