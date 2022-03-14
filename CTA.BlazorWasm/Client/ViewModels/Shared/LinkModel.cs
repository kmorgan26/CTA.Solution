namespace CTA.BlazorWasm.Client.ViewModels.Shared
{
    public class LinkModel
    {
        public LinkModel(int page) : this(page, true)
        {

        }
        public LinkModel(int page, bool enabled)
            : this(page, enabled, page.ToString())
        {

        }
        public LinkModel(int page, bool enabled, string text)
        {
            PageNumber = page;
            Enabled = enabled;
            Text = text;
        }
        public string Text { get; set; }
        public int PageNumber { get; set; }
        public bool Enabled { get; set; } = true;
        public bool Active { get; set; } = false;
    }
}
