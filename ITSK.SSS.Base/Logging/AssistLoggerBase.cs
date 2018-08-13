using ITSK.SSS.Base.Constants;
using ITSK.SSS.Base.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSK.SSS.Base.Logging
{
    public abstract class AssistLoggerBase : IAssistLogger
    {
        protected ILogger _logger;
        
        public AssistLoggerBase(ILogger logger)
        {
            _logger = logger ?? new EmptyLogger();
        }

        public AssistLoggerBase()
            : this(null)
        { }

        public void LogError(string category, Exception ex)
        {
            _logger.WriteError(category, ex);
        }
        public void LogError(LogCategory area, Exception ex)
        {
            LogError(DiagnosticArea.GetCategory(area), ex);
        }

        public void LogError(string category, string msg)
        {
            _logger.WriteError(category, msg);
        }
        public void LogError(LogCategory area, string msg)
        {
            LogError(DiagnosticArea.GetCategory(area), msg);
        }

        public void LogHigh(string category, string msg)
        {
            _logger.WriteHigh(category, msg);
        }
        public void LogHigh(LogCategory area, string msg)
        {
            LogHigh(DiagnosticArea.GetCategory(area), msg);
        }

        public void LogMedium(string category, string msg)
        {
            _logger.WriteMedium(category, msg);
        }
        public void LogMedium(LogCategory area, string msg)
        {
            LogMedium(DiagnosticArea.GetCategory(area), msg);
        }

        public void LogInfo(string category, string msg)
        {
            _logger.WriteInfo(category, msg);
        }
        public void LogInfo(LogCategory area, string msg)
        {
            LogInfo(DiagnosticArea.GetCategory(area), msg);
        }
    }
}
