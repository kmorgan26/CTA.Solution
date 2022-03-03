using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Responses
{
    public class Response<TEntity>
    {
        public Response(){}

        public Response(TEntity response)
        {
            Data = response;
        }

        public TEntity Data { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorMessage { get; set; } = new List<string>();

    }
}
