using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWorldWeb.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetDate()
        {
            return DateTime.Now;
        }
    }
}
