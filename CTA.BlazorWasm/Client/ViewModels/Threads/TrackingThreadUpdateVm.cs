namespace CTA.BlazorWasm.Client.ViewModels.Threads
{
    public class TrackingThreadUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
