using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.DTO
{
    public class ResponseDTO<T> where T : class
    {
       public T Data { get; private set; }
        public int StatusCode { get; private set; }
        public ErrorDTO Error { get; private set; }

        public static ResponseDTO<T> Succes(T Data, int Status)
        {
            return new ResponseDTO<T> { Data = Data, StatusCode = Status };

        }
        public static ResponseDTO<T> Succes(int Status)
        {
            return new ResponseDTO<T> { StatusCode = Status };

        }
        public static ResponseDTO<T> Fail(int status, ErrorDTO error)
        {
            return new ResponseDTO<T> { Error = error, StatusCode = status };
        }
        public static ResponseDTO<T> Fail(int status, string error ,bool isShow) 
        {
            var errordto = new ErrorDTO(error, isShow);
            return new ResponseDTO<T> { Error = errordto , StatusCode = status };
        }
    }
}
