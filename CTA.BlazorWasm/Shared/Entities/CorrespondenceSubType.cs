using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Entities
{
    public partial class CorrespondenceSubType
    {
        public CorrespondenceSubType()
        {
            CorrespondenceTypes = new HashSet<CorrespondenceType>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<CorrespondenceType> CorrespondenceTypes { get; set; }
    }
}
