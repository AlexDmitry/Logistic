using ITSK.SSS.Base.Enums;
using System;

namespace ITSK.SSS.Base.Logging
{
    public interface IAssistLogger
    {
        void LogError(string category, Exception ex);
        void LogError(LogCategory area, Exception ex);

        void LogError(string category, string msg);
        void LogError(LogCategory area, string msg);

        void LogHigh(string category, string msg);
        void LogHigh(LogCategory area, string msg);

        void LogMedium(string category, string msg);
        void LogMedium(LogCategory area, string msg);

        void LogInfo(string category, string msg);
        void LogInfo(LogCategory area, string msg);
    }
}
