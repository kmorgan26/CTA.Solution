using System;
using System.Collections.Generic;

namespace CTA.BlazorWasm.Shared.Models
{
    public partial class Tracking
    {
        public int Id { get; set; }
        public int ThreadId { get; set; }
        public int ToFromId { get; set; }
        public string Subject { get; set; } = null!;
        public int CorrespondenceTypeId { get; set; }
        public int PocId { get; set; }
        public int StatusId { get; set; }
        public string? Comments { get; set; }
        public string? DocumentPath { get; set; }
        public DateTime SentOrReceived { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public virtual CorrespondenceType CorrespondenceType { get; set; } = null!;
        public virtual Poc Poc { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual TrackingThread Thread { get; set; } = null!;
        public virtual ToFrom ToFrom { get; set; } = null!;
    }
}
