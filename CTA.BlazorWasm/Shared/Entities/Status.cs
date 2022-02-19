﻿using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Entities
{
    public partial class Status
    {
        public Status()
        {
            Trackings = new HashSet<Tracking>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Tracking> Trackings { get; set; }
    }
}
