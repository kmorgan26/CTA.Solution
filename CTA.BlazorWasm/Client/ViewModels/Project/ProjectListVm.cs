using System.ComponentModel.DataAnnotations;

namespace CTA.BlazorWasm.Client.ViewModels.Project
{
    public class ProjectListVm
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "You want to create a BLANK Project Name? C'mon, man! Enter something!")]
        [StringLength(20, ErrorMessage="Please use a Project name with 20 characters or less")]
        public string Name { get; set; } = string.Empty;
    }
}
