using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Threads
{
    public class TrackingThreadVm
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        
        public string ProjectName { get; set; } = String.Empty;
        
        public bool? IsActive { get; set; }
    }
}
