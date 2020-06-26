using Microsoft.EntityFrameworkCore.Internal;
using ProcessExplorerWeb.Application.Common.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessExplorerWeb.Application.Session.Extensions
{
    public static class SessionChartLineModelExtensions
    {
        public static List<SessionChartLineModel> FillDatesThatAreMissing(this List<SessionChartLineModel> sessionChart, DateTime periodStart, DateTime periodEnd)
        {
            var allDaysForGivenPeriod = FillListWithDates(periodStart, periodEnd);

            FillListWithMissingDates(sessionChart, allDaysForGivenPeriod);

            return OrderListByDate(sessionChart);
        }

        private static List<SessionChartLineModel> OrderListByDate(List<SessionChartLineModel> sessionChart)
        {
            return sessionChart.OrderBy(i => i.Date).ToList();
        }

        private static void FillListWithMissingDates(List<SessionChartLineModel> sessionChart, IEnumerable<DateTime> allDaysForGivenPeriod)
        {
            foreach(var date in allDaysForGivenPeriod)
            {
                if (sessionChart.All(i => i.Date.Date != date))
                    sessionChart.Add(new SessionChartLineModel { Date = date.Date, Number = 0 });
            }
        }

        private static IEnumerable<DateTime> FillListWithDates(DateTime start, DateTime end)
        {
            for (var dt = start; dt <= end; dt = dt.AddDays(1))
                yield return dt;
        }
    }
}
