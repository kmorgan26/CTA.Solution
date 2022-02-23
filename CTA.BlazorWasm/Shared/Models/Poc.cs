using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Models
{
    public partial class Poc
    {
        public Poc()
        {
            Trackings = new HashSet<Tracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}
