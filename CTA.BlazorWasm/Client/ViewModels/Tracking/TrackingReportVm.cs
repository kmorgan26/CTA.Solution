using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Tracking
{
    public class TrackingReportVm
    {
        [Display(Name = "Tracking #")]
        public int Id { get; set; }

        [Display(Name = "Thread")]
        public int ThreadId { get; set; }

        [Display(Name = "To/From")]
        public string ToFromName { get; set; } =string.Empty;

        [Display(Name = "Project")]
        public string ProjectName { get; set; } =string.Empty;
        
        [Display(Name ="Topic")]
        public string TopicName { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty ;

        [Display(Name ="Sent/Rec")]
        public DateTime SentOrReceived { get; set; }

        [Display(Name = "Type")]
        public string CorrespondenceType { get; set; } = string.Empty;

        public string Poc { get; set; } = string.Empty;
        
        public string Status { get; set; } = string.Empty;
        
        public string? Comments { get; set; }
        
    }
}
