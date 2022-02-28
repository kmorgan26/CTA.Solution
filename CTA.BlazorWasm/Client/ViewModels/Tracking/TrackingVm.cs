using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Tracking
{
    public class TrackingVm
    {
        public int Id { get; set; }
        public string ToFrom { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;
        public string CorrespondenceType { get; set; } = string.Empty;

        public string Poc { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SentOrReceived { get; set; }

        public string Comments { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;

    }
}
