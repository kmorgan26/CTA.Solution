using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Models
{
    public class ApiPagedResponse<TEntity> where TEntity : class
    {
        public bool Success { get; set; }
        public List<string> ErrorMessage { get; set; } = new List<string>();
        public IEnumerable<TEntity> Data { get; set; }
    }
}
