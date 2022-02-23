using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Models
{
    public partial class Project
    {
        public Project()
        {
            TrackingThreads = new HashSet<TrackingThread>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<TrackingThread> TrackingThreads { get; set; }
    }
}
