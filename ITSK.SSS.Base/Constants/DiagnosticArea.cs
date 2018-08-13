using ITSK.SSS.Base.Enums;
using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSK.SSS.Base.Constants
{
    public static class DiagnosticArea
    {
        private static readonly Dictionary<LogCategory, string> _dict = new Dictionary<LogCategory, string>{
            { LogCategory.Page, "Page"},
            { LogCategory.WebParts, "WebParts"},
            { LogCategory.Service, "Service"},
            { LogCategory.Security, "Security"},
            { LogCategory.Other, "Other"},
            { LogCategory.Receivers, "Receivers"},
            { LogCategory.Controls, "Controls"},
            { LogCategory.TimerJob, "Page"},
            { LogCategory.SqlExecutor, "Sql Executor"},
            { LogCategory.SssBase, "SSS Base"},
            { LogCategory.SssSql, "SSS SQL"},
            { LogCategory.SssSp, "SSS SP"},
            { LogCategory.SssHPSM, "SSS HPSM"},
            { LogCategory.SssController, "SSS Controller"},
            { LogCategory.SssViewsData, "SSS Views Data"},
            { LogCategory.SssConfig, "SSS Configuration"},
            { LogCategory.SssUI, "SSS UI"},
            { LogCategory.SssViews, "SSS Views"},
            { LogCategory.WebProperties, "Web Properties"},
            { LogCategory.SecureStore, "Service SecureStore"},
            { LogCategory.SSSTimerJob, "SSS TimerJob"}
        };

        public const string Name = "ITSK SSS";

        public static List<SPDiagnosticsCategory> DiagnosticsCategories
        {
            get
            {
                return _dict.Values
                    .Distinct()
                    .Select(e => new SPDiagnosticsCategory(e, TraceSeverity.Verbose, EventSeverity.Verbose))
                    .ToList();
            }
        }

        public static string GetCategory(LogCategory area)
        {
            return _dict.ContainsKey(area) ? _dict[area] : null;
        }
    }
}
