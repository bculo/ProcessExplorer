using ProcessExplorerWeb.Core.Queries.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessExplorerWeb.Application.Session.Extensions
{
    public static class SessionChartLineModelExtensions
    {
        public static List<SessionDateChartItem> FillDatesThatAreMissing(this List<SessionDateChartItem> sessionChart, DateTime periodStart, DateTime periodEnd)
        {
            var allDaysForGivenPeriod = FillListWithDates(periodStart, periodEnd);

            FillListWithMissingDates(sessionChart, allDaysForGivenPeriod);

            return OrderListByDate(sessionChart);
        }

        private static List<SessionDateChartItem> OrderListByDate(List<SessionDateChartItem> sessionChart)
        {
            return sessionChart.OrderBy(i => i.Date).ToList();
        }

        private static void FillListWithMissingDates(List<SessionDateChartItem> sessionChart, IEnumerable<DateTime> allDaysForGivenPeriod)
        {
            foreach(var date in allDaysForGivenPeriod)
            {
                if (sessionChart.All(i => i.Date.Date != date))
                    sessionChart.Add(new SessionDateChartItem { Date = date.Date, Number = 0 });
            }
        }

        private static IEnumerable<DateTime> FillListWithDates(DateTime start, DateTime end)
        {
            for (var dt = start; dt <= end; dt = dt.AddDays(1))
                yield return dt;
        }
    }
}
