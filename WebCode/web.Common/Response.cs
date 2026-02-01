using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace web.Common
{
    public class Response
    {
        public int statusCode { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string message { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object errorMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string exceptionMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public dynamic data { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public byte[] fileContent { get; set; }
        public bool success { get; set; }

        public Response()
        {
            this.statusCode = 0;
            this.message = null;
            this.data = null;
            this.errorMessage = null;
            this.exceptionMessage = null;
        }

        public Response(int statusCode, string message, dynamic data)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.data = data;
            this.errorMessage = null;
            this.exceptionMessage = null;
        }

        public Response(int statusCode, string message, byte[] fileContent)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.fileContent = fileContent;
        }

        public Response(int statusCode, string message, dynamic body, byte[] fileContent)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.fileContent = fileContent;
            this.data = body;
        }

        public Response(int statusCode, string message = null, dynamic body = null, string errorMessage = null, string exceptionMessage = null)
        {
            this.statusCode = statusCode;
            this.message = message;
            this.data = body;
            this.errorMessage = errorMessage;
            this.exceptionMessage = exceptionMessage;
        }
    }
}
