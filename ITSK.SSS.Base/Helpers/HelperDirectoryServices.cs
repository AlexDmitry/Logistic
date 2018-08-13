using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;

namespace ITSK.SSS.Base.Helpers
{
    public static class HelperDirectoryServices
    {
        public static int GetLifeTime()
        {
            //Есть маза вынести в кеш с использованием имперсонации...
            var maxPwdAge = TimeSpan.MinValue;
            using (var d = Domain.GetCurrentDomain())
            {
                using (var domain = d.GetDirectoryEntry())
                {
                    var ds = new DirectorySearcher(domain, "(objectClass=*)", null, SearchScope.Base);
                    var sr = ds.FindOne();
                    if (sr.Properties.Contains("maxPwdAge"))
                    {
                        maxPwdAge = TimeSpan.FromTicks((long) sr.Properties["maxPwdAge"][0]);
                    }

                }
            }

            var p = UserPrincipal.Current;
            var lastPwdAge = TimeSpan.MinValue;
            if (p.LastPasswordSet.HasValue)
            {
                var lastPwdSet = p.LastPasswordSet.Value.ToLocalTime();
                lastPwdAge = TimeSpan.FromTicks(DateTime.Now.Ticks - lastPwdSet.Ticks);
            }
            else
            {
                return 0;
            }
            return Convert.ToInt32(((maxPwdAge.Duration() - lastPwdAge.Duration()).TotalDays));

        }
    }
}

