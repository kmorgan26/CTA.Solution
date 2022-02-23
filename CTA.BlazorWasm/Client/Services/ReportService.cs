using CTA.BlazorWasm.Shared.Filters;

namespace CTA.BlazorWasm.Client.Services
{
    public static class ReportService
    {
        public static async Task<TrackingFilter> GetReportFilter(string reportId)
        {
            TrackingFilter filter = new();

            switch(reportId)
            {
                case "1":
                    filter = await Task.Run(() => filter = new() { ToFromIds = new int[] { 1, 2 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 2 } });
                    break;
                case "2":
                    filter =  await Task.Run(() => filter = new() { ToFromIds = new int[] { 3, 4 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 2 } });
                    break;
                case "3":
                    filter = await Task.Run(() => filter = new() { ToFromIds = new int[] { 1, 2 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 1 } });
                    break;
                case "4":
                    filter = await Task.Run(() => filter = new() { ToFromIds = new int[] { 3, 4 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 1 } });
                    break;
            }
            return filter;
        }
    }
}
