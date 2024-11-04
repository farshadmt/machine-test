using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Response<T>
    {
        public bool success { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; } 
        public T? data { get; set; }
    }
}
