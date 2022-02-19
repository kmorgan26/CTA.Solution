using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Entities
{
    public partial class TrackingThread
    {
        public TrackingThread()
        {
            Trackings = new HashSet<Tracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int ProjectId { get; set; }
        public bool? IsActive { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}
