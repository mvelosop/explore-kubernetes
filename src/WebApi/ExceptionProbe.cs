using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public static class ExceptionProbe
    {
        public static void ThrowIf(string exceptionProbe, string exceptionValue)
        {
            if (exceptionProbe == exceptionValue)
            {
                throw new Exception($"Startup Exception: {exceptionValue}");
            }
        }
    }
}
