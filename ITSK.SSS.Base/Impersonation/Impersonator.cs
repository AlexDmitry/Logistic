using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace ITSK.SSS.Base.Impersonation
{
    public class Impersonator : IDisposable
    {
        private WindowsImpersonationContext _impersonationContext;

        public Impersonator(string userName, string domainName, string password)
        {
            ImpersonateValidUser(userName, domainName, password);
        }

        public void Dispose()
        {
            UndoImpersonation();
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int LogonUser(string lpszUserName, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

        private const int Logon32ProviderDefault = 0;
        private const int Logon32LogonInteractive = 2;
        private const int Logon32LogonNewCredentials = 9;


        private void ImpersonateValidUser(string userName, string domain, string password)
        {
            var token = IntPtr.Zero;
            var tokenDuplicate = IntPtr.Zero;

            try
            {
                if (RevertToSelf())
                {
                    if (LogonUser(userName, domain, password, Logon32LogonNewCredentials, Logon32ProviderDefault, ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            var tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            _impersonationContext = tempWindowsIdentity.Impersonate();
                        }
                        else
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            finally
            {
                if (token != IntPtr.Zero)
                {
                    CloseHandle(token);
                }
                if (tokenDuplicate != IntPtr.Zero)
                {
                    CloseHandle(tokenDuplicate);
                }
            }
        }

        private void UndoImpersonation()
        {
            if (_impersonationContext != null)
            {
                _impersonationContext.Undo();
            }
        }
    }
}