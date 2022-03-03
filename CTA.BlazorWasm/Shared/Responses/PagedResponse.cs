using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Responses
{
    public class PagedResponse<TEntity>
    {
        public PagedResponse(){}

        public PagedResponse(IEnumerable<TEntity> data)
        {
            Data = data;
        }

        public IEnumerable<TEntity>? Data { get; set; }

        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string NextPage { get; set; }
        public string PreviousPage { get; set; }


    }
}
