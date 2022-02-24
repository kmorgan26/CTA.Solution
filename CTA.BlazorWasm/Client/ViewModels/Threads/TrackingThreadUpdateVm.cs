using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Threads
{
    public class TrackingThreadUpdateVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You want to create a BLANK Thread Name? C'mon, man! Enter something!")]
        [StringLength(20, ErrorMessage = "Please use a Thread name with 50 characters or less")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage ="A Project is required for a Thread")]
        public int ProjectId { get; set; }
        
        public bool IsActive { get; set; } = true;
    }
}
