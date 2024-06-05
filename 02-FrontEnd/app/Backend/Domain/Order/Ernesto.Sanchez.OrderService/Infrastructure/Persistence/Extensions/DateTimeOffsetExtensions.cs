using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace Ernesto.Sanchez.OrderService.Infrastructure.Persistence.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static int GetCurrentDay(this DateTimeOffset dateTimeOffset, DateTimeOffset? dateOfOrder)
        {
            var dateToCalculateTo = DateTime.UtcNow;
            //if (dateOfOrder != null)
            //{
            //    dateToCalculateTo = dateOfOrder.Value.UtcDateTime;
            //}

            int daysorder = dateToCalculateTo.Day - dateTimeOffset.Day;

            if (dateToCalculateTo < dateTimeOffset.AddDays(daysorder))
            {
                daysorder--;
            }

            return daysorder;
        }
    }
}
