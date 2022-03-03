using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Filters
{
    public class TrackingFilter
    {
        public int? ToFromId { get; set; }
        public int? StatusId { get; set; }
        public int? CorrTypeId { get; set; }
        public int? PocId { get; set; }
        public int ThreadId { get; set; } = 0;
        public string? SubjectText { get; set; }
        public string? CommentsText { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int[]? ToFromIds { get; set; }
        public int[]? TypeIds { get; set; }
        public int[]? PocIds { get; set; }
        public int[]? StatusIds { get; set; }
    }
}
