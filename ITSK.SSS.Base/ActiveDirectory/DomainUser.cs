using System.DirectoryServices;

namespace ITSK.SSS.Base.ActiveDirectory
{
    public class DomainUser
    {
        public static SearchResult FiendByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) { return null; }
            var directoryLookup = new DirectorySearcher
            {
                Filter = string.Format("(&(objectClass=user)(objectCategory=person)(mail={0}))", email)
            };
            return directoryLookup.FindOne();
        }
    }
}
