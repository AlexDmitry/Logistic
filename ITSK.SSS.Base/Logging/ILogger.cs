using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSK.SSS.Base.Logging
{
    public interface ILogger
    {
        void WriteError(string category, Exception ex);

        void WriteError(string category, string msg);

        void WriteInfo(string category, string msg);

        void WriteHigh(string category, string msg);

        void WriteMedium(string category, string msg);
    }
}
