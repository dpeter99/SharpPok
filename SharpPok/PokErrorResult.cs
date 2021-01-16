using System;
using System.Buffers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharpPok
{
    public class PokErrorResult : IActionResult
    {
        private static string Template = 
@"{{
    ""error"": {0},
    ""meaning"": ""{1}"",
    ""versions"": ""N/A""
}}";

        private int Code { get; }
        private string Message { get; }

        private string Text
        {
            get
            {
                return String.Format(Template,Code,Message);
            }
        }

        public PokErrorResult(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(Text)
            {
                
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}