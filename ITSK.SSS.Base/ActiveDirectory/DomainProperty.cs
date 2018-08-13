using ITSK.SSS.Base.Impersonation;
using System;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace ITSK.SSS.Base.ActiveDirectory
{
    public sealed class DomainProperty
    {
        public string ImpersonateUser { get; private set; }
        public string ImpersonateDomain { get; private set; }
        public string ImpersonatePassword { get; private set; }

        public bool IsNeedImpersonate
        {
            get
            {
                return !string.IsNullOrEmpty(ImpersonateUser)
                    && !string.IsNullOrEmpty(ImpersonateDomain)
                    && !string.IsNullOrEmpty(ImpersonatePassword);
            }
        }

        public DomainProperty()
        { }

        public DomainProperty(string userName, string domain, string password)
        {
            ImpersonateUser = userName;
            ImpersonateDomain = domain;
            ImpersonatePassword = password;
        }

        private object GetDomainProperty(string name)
        {
            if (!IsNeedImpersonate)
            {
                return GetProperty(name);
            }

            using (new Impersonator(ImpersonateUser, ImpersonateDomain, ImpersonatePassword))
            {
                return GetProperty(name);
            }
        }

        private object GetProperty(string name)
        {
            object res = null;


            using (var d = Domain.GetCurrentDomain())
            {
                using (var domain = d.GetDirectoryEntry())
                {
                    var ds = new DirectorySearcher(domain, "(objectClass=*)", null, SearchScope.Base);
                    var sr = ds.FindOne();
                    if (sr.Properties.Contains(name))
                    {
                        res = sr.Properties[name][0];
                    }
                }
            }
            return res;
        }

        public TimeSpan? MaxPwdAge
        {
            get
            {
                var val = GetDomainProperty("maxPwdAge");
                return val != null ? TimeSpan.FromTicks((long)val) : (TimeSpan?)null;
            }
        }
    }
}
