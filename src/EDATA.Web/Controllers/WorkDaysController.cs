using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EDATA.API.Models.WorkDays;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EDATA.API.Controllers
{
    [Route("api/[controller]")]
    public class WorkDaysController : Controller
    {
        // GET: api/values
        public object Get([FromQuery]DateTime? from, [FromQuery]DateTime? to)
        {
            if (from == null)
            {
                return this.HttpBadRequest("Укажите дату - from");
            }

            if (to == null)
            {
                return this.HttpBadRequest("Укажите дату - to");
            }

            if (to.Value.Date <= from.Value.Date)
            {
                return this.HttpBadRequest("to должна быть больше from");
            }

            var total = (to.Value.Date - from.Value.Date).Days + 1;
            var work = CountWorkDays(from.Value.Date, to.Value.Date);

            var r = new WorkDaysResult
            {
                from = from.Value,
                to = to.Value,
                work = work,
                other = total - work
            };

            return Json(r);
        }

        private int CountWorkDays(DateTime from, DateTime to)
        {
            var current = from;
            var result = 0;

            while (current <= to)
            {
                if (current.DayOfWeek != DayOfWeek.Saturday
                    && current.DayOfWeek != DayOfWeek.Sunday)
                {
                    result++;
                }
                
                current = current.AddDays(1);
            }

            return result;
        }
    }
}
