using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Entities
{
    public partial class CorrespondenceType
    {
        public CorrespondenceType()
        {
            Trackings = new HashSet<Tracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CorrespondenceSubTypeId { get; set; }

        public virtual CorrespondenceSubType CorrespondenceSubType { get; set; } = null!;
        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}
