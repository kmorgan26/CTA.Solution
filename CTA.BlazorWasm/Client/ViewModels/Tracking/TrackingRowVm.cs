namespace CTA.BlazorWasm.Client.ViewModels.Tracking
{
    public class TrackingRowVm
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string ToFrom { get; set; } = string.Empty ;

        public DateTime SentOrReceived { get; set; }

    }
}
