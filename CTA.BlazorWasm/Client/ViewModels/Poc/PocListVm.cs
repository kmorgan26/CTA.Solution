using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Poc
{
    public class PocListVm
    {
        public int Id { get; set; }

        [Required, StringLength(5)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required, StringLength(20)]
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
