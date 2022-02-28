using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Tracking
{
    public class EditTrackingVm
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Thread")]
        public int ThreadId { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Please Select from the To/From List")]
        public int ToFromId { get; set; }

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Range(1, 100, ErrorMessage = "Please Select a Correspondence Type")]
        public int CorrespondenceTypeId { get; set; }


        [Required]
        [Range(1, 100, ErrorMessage = "Please Select a POC from the list")]
        public int PocId { get; set; }


        [Required]
        [Range(1, 100, ErrorMessage = "Please Select a Status")]
        public int StatusId { get; set; }


        public string? Comments { get; set; }

        public string? DocumentPath { get; set; }

        [Required]
        public DateTime? SentOrReceived { get; set; }
    }
}
