using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comparly.Data.Dtos
{
    public class ResponseDto<T>
    {
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
