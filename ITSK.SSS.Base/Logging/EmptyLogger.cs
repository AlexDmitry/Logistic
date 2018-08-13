using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSK.SSS.Base.Logging
{
    internal class EmptyLogger : ILogger
    {
        public void WriteError(string category, Exception ex)
        { }

        public void WriteError(string category, string msg)
        { }

        public void WriteInfo(string category, string msg)
        { }

        public void WriteHigh(string category, string msg)
        { }

        public void WriteMedium(string category, string msg)
        { }
    }
}
