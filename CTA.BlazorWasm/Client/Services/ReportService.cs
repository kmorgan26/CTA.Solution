using ContractsTracker.DataAccess.Filters;

namespace ContractsTracker.App.Services
{
    public static class ReportService
    {
        public static async Task<TrackingFilter> GetReportFilter(string reportId)
        {
            TrackingFilter filter = new();

            switch(reportId)
            {
                case "open_gov-cont":
                    filter = await Task.Run(() => filter = new() { ToFromIds = new int[] { 1, 2 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 2 } });
                    break;
                case "open_subs_cont":
                    filter =  await Task.Run(() => filter = new() { ToFromIds = new int[] { 3, 4 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 2 } });
                    break;
                case "open_gov_rfp":
                    filter = await Task.Run(() => filter = new() { ToFromIds = new int[] { 1, 2 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 1 } });
                    break;
                case "open_subs_rfp":
                    filter = await Task.Run(() => filter = new() { ToFromIds = new int[] { 3, 4 }, StatusIds = new int[] { 2 }, TypeIds = new int[] { 1 } });
                    break;
            }
            return filter;
        }
    }
}
