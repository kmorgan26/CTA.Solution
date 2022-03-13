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

        public int TotalRecords { get; set; } = 0;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? NextPage { get; set; }
        public string? PreviousPage { get; set; }
        public bool Success { get; set; }
        public List<string> ErrorMessage { get; set; } = new List<string>();


    }
}
