using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITSK.SSS.Base.Constants
{
    public static class Common
    {
        public const string CreateOnlyRoleDefinitionName = "Только создание";

        public const string OneSCredentialsName = "SelfServiceToOneS";

        public const string RssCredentialsName = "SelfServiceToRss";

        public const string QlikViewCredentialsName = "SelfServiceToQlikView";

        public const string QlikViewTicketedCredentialsName = "SelfServiceIntoQlikView";

        public const string QlikViewServerName = "http://spb99-qv01.gazprom-neft.local";

        public const string QlikViewUrl = "/QvAJAXZfc/singleobject.htm?document=Test%2FSSS%2FKSUITforSSS.qvw&object=CT05";

        public const string QlikViewUrlEncoded = "%2FQvAJAXZfc%2Fsingleobject.htm%3Fdocument%3DTEST%2FSSS%2FKSUITforSSS.qvw%26object%3DCT05";

        public const string QlikViewAuthorizationUrl = "http://spb99-qv01.gazprom-neft.local/QvAJAXZfc/GetWebTicket.aspx";

        public const string SITE_ID = "site-id";

        public const string WEB_ID = "web-id";
    }
}
