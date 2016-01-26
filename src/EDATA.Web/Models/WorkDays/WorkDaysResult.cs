using System;

namespace EDATA.API.Models.WorkDays
{
    public class WorkDaysResult
    {
         public DateTime from { get; set; }
         public DateTime to { get; set; }
         public int work { get; set; }
         public int other { get; set; }
    }
}