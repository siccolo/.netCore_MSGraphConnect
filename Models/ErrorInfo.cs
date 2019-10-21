using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class ErrorInfo
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        public Microsoft.Graph.Error AsGraphError()
        {
            return new Microsoft.Graph.Error() { Code = StatusCode.ToString(), Message = Message };
        }
    }
}
