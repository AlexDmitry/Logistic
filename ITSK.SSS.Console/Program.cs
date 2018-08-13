using ITSK.SSS.Common.BusinessEntities;
using ITSK.SSS.Common.BusinessLogic;
using ITSK.SSS.Common.DAL;
using ITSK.SSS.Proxy.HPSM;
using ITSK.SSS.Proxy.HPSM.SQL;
using ITSK.SSS.Proxy.HPSM.Contracts;
using ITSK.SSS.SP;
using ITSK.SSS.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using Microsoft.SharePoint;
using ITSK.SSS.SQL;
using ITSK.SSS.Proxy.HPSM.Configurations;
using Impersonator = ITSK.SSS.Proxy.HPSM.SQL.Impersonator;
using ITSK.SSS.Contracts;
using ITSK.SSS.Configuration;
using ITSK.SSS.HPSM;
using ITSK.SSS.HPSM.SOAP;
using ITSK.SSS.Base.Serialization;
using ITSK.SSS.HPSM.TemplateEngine;
using ITSK.SSS.SP.SPController;
using ITSK.SSS.Contracts.Enums;
using ITSK.SSS.ViewsController;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Administration;
using ITSK.SSS.Base.Extensions;
using System.Xml;
using System.Xml.Serialization;
using ITSK.SSS.LogService;
using System.DirectoryServices;
using Microsoft.SharePoint.Utilities;


namespace ITSK.SSS.Console
{


    public class Program
    {
        public static void Main(string[] args)
        {
            System.Console.WriteLine(DateTime.Now.Year.ToString());
            TestAddAttachToItchanges();
            return;
            //TestSOAPDownload();
            //return;


            //testMenu();
            //return;

            //testMsk(null);
            //testMsk("sdsds");
            //return;

            

            

            TestUpdateUserFromAD();
            return;

            TestFindByOU();
            return;
            
            //TestLoggerConsole();
            return;


            try
            {

                using (var site = new SPSite("http://selfservice-qa.gazprom-neft.local"))

                //using (var site = new SPSite("http://spb12-it-crdev3:8080"))
                {
                    using (var web = site.OpenWeb())
                    {
                        //CountersController.UpdateIndicators(web, new SssConfig(web));

                        //var result = CountersController.GetCountOfRequestForLastYear();
                        //CountersController.UpdateCountOfRequestForLastYearToList(web, result);
                        List<string> keys = new List<string>();
                        keys.Add("statisticsqlconnectionstring");
                        keys.Add("HpsmSqlConnectionString");
                        keys.Add("FollowMeConnectionString");
                        keys.Add("ImpersonateUser");
                        keys.Add("ImpersonateDomain");
                        keys.Add("ImpersonatePassword");
                        keys.Add("IsNeedImpersonate");
                        keys.Add("ServiceRESTFullUrl");
                        keys.Add("ServiceSOAPUrl");
                        keys.Add("HpsmServiceUserName");
                        keys.Add("HpsmServicePassword");
                        keys.Add("UosManagement");
                        keys.Add("UosMonitoring");
                        keys.Add("UosSearching");
                        keys.Add("StatisticSqlConnectionString");
                        web.AllowUnsafeUpdates = true;
                        foreach (string key in keys)
                        {
                            if (web.AllProperties.ContainsKey(key.ToLower()))
                            {
                                System.Console.WriteLine("Try - " + key);
                                web.AllProperties.Remove(key);
                                web.Properties[key] = null;
                                web.Update();
                                web.Properties.Update();

                                if (!web.AllProperties.ContainsKey(key))
                                {
                                    System.Console.WriteLine(key + " deleted");
                                }
                            }
                            else
                                System.Console.WriteLine(key + " is not found in web");
                        }
                    }
                }


                return;
                testGetAttachmentInteraction(); return;
                testLogin(); return;

                TestAnnouncmen();
                TestComment();
                TestMailBoxSize();
                //TestINteractionComments();
                //TestParsingTimeZone();
                //TestAgreementCreate();
                //GetDocumentTemplate();
                //TestAddAttachToItchanges();
                //TestUpdateITChanges();
                //++TestConfirm();
                //++TestBackToWork();
                //++TestNonConfirm();
                //TestAttachItChanges();
                //----
                //TestParsing(); return;
                //TestregExpress(); return;
                
                //TestGetDeviseRusName();
                //TestCreateXmlFile();
                //TestGetITChange();
                //TestGetAllCategory();
                //TestCreateITChange();
                //TestTemplateEngine_2();
                
                //using (var site = new SPSite("http://sp2010"))
                //{
                //    using (var web = site.OpenWeb())
                //    {
                //        var controller = new CItemInteractionTemplate(web);
                //        var model = controller.GetTemplatesByParentAnCompany("Доступ к информационным системам", "ИТСК ООО");
                //        var z = string.Empty;
                //    }
                //}

                //Test1();
                //checkSqlConnectHonorDesk();
                //impersonateTest();
                //TestHonorDesk();
                //TestHPSMServices();

                //TestHPSMWinAuth();

                //TestSearchContactName();

                //TestInteractionTemplate();

                //TestLkMenu();

                //TestInteraction();

                //TestTemplateEngine();

                //TestSOAPCreate();

                //TestSOAPUpdate();
                //TestSOAPClose();
                //TestSOAPDownload();
            }
            catch (WebException ex)
            {
                System.Console.WriteLine(ex.Message);
                if (ex.Response != null)
                {
                    using (var responseStream = ex.Response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            var responseReader = new StreamReader(responseStream);
                            var responseResult = responseReader.ReadToEnd();

                            System.Console.WriteLine("Received: " + responseResult);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            finally
            {
                System.Console.WriteLine();
                System.Console.WriteLine("Press enter to exit");
                System.Console.ReadLine();
            }
        }

        private static void testMenu()
        {

            string xmlStr = string.Empty;
            using (var site = new SPSite("http://spb12-it-crdev3:8080"))
            {
                using (var web = site.OpenWeb())
                {

                    var menu = LKController.GetMenu(web);
                    var z = "";
                }
            }
        }

        private static void testMsk(string result)
        {

            var defaultValue = "MSK+3";
            if (string.IsNullOrEmpty(result)) 
                result =  defaultValue;

            if (!result.Contains("MSK")) 
                result =  defaultValue;
        }

        private static void TestUpdateUserFromAD()
        {
            using (var site = new SPSite("http://spb12-it-crdev3:8080"))
            {
                using (var web = site.OpenWeb())
                {
                    var list = new List<ADUser>();
                    string directory = "LDAP://OU=Regions,DC=crdev3,DC=local";

                    using (DirectoryEntry usersDE = new DirectoryEntry(directory))
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(usersDE))
                        {
                            var propertiesToLoad = new[] { "mail", "SAMAccountName", "distinguishedName" };

                            searcher.Filter = "(&(objectCategory=person)(objectClass=user)(company=*ИТСК*) (!userAccountControl:1.2.840.113556.1.4.803:=2))"; 
                            searcher.PropertiesToLoad.AddRange(propertiesToLoad);
                            searcher.PageSize = 10000;

                            SearchResultCollection src = searcher.FindAll();

                            foreach (SearchResult sr in src)
                            {
                                var userEntry = sr.GetDirectoryEntry();

                                string email = userEntry.Properties["mail"].Value != null
                                                ? userEntry.Properties["mail"].Value.ToString()
                                                : "email not found";

                                string accountName = userEntry.Properties["SAMAccountName"].Value != null
                                                ? userEntry.Properties["SAMAccountName"].Value.ToString()
                                                : "SAMAccountName not found";

                                list.Add(new ADUser() { Email = email, AccountName = accountName });
                            }
                        }
                    }
                }
            }
        }

        private static void TestFindByOU()
        {
            //using (var site = new SPSite("http://spb12-it-crdev3:8080"))
            //{
            //    using (var web = site.OpenWeb())
            //    {
                    string directory = "LDAP://OU=ООО ИТСК,OU=SPB,OU=Regions,DC=crdev3,DC=local";

                    using (DirectoryEntry usersDE = new DirectoryEntry(directory))
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(usersDE))
                        {
                            searcher.Filter = "(&(objectClass=user))";
                            var propertiesToLoad = new[] { "mail" };
                            searcher.PropertiesToLoad.AddRange(propertiesToLoad);


                            searcher.PageSize = 10000;
                            SearchResultCollection src = searcher.FindAll();
                            List<object> userlist = new List<object>();
                            foreach (SearchResult sr in src)
                            {
                                var userEntry = sr.GetDirectoryEntry();
                                
                                var str = userEntry.Properties["mail"].Value;

                                userlist.Add(sr);
                            }
                            var z = "";
                        }
                    }


            //    }
            //}
        }




        //private static void TestLoggerConsole()
        //{
        //    using (var site = new SPSite("http://spb12-it-crdev3:8080"))
        //    {
        //        using (var web = site.OpenWeb())
        //       {

                    
        //            var logger = new PublisherLogger();
        //            //SssLoggerNew.

        //            System.Console.WriteLine("подписываем наблюдателей");
        //            var logConsole = new LoggerToConsole();
        //            var logList = new LoggerToList(web);


        //            logger.AddObserver(logConsole);
        //            logger.AddObserver(logList);



        //            //logger.WriteDebug("debug");
        //            //logger.WriteInfo("information");
        //            //logger.WriteWarning("warrning");
        //            //logger.WriteError("error");

        //            try
        //            {
        //                var ziro = 0;
        //                var x = 10 / ziro;
        //            }
        //            catch (Exception ex)
        //            {
        //                logger.WriteError("Ошибка тестовая:", ex);
        //            }

        //            System.Console.WriteLine("отписываем наблюдателя");
        //            logger.RemoveObserver(logConsole);
        //            logger.RemoveObserver(logList);

        //        }
        //    }
        //}

        private static void testGetAttachmentInteraction()
        {
            string str = "Добавлено вложение <a href=\"scattach://5ae44ffd001ddd0382f727e0:SelfAttachment.msg:incidents:SD-6057939\">SelfAttachment.msg</a>";
            string z = str.Split(new string[]{@"//"}, StringSplitOptions.RemoveEmptyEntries)[1].Split(new string[]{":"}, StringSplitOptions.RemoveEmptyEntries)[0];
            return;
            //var accsessor = new InteractionAccessor();
            //var z = accsessor.GetById("SD-6022244");
        }

        private static void testLogin()
        {
            string siteStr = "http://spb12-it-crdev3:8080";
            SPSite tempSite = new SPSite(siteStr);
            SPUserToken systoken = tempSite.SystemAccount.UserToken;
            using (SPSite site = new SPSite(siteStr, systoken))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    //right now, logged in as Site System Account
                    System.Console.WriteLine("Currently logged in as: " +
                                       web.CurrentUser.ToString());

                    System.Console.WriteLine("Currently logged in as: "  + web.CurrentUser.LoginName.Split(new string[]{ @"\\" }, StringSplitOptions.None)[0]);
                    System.Console.WriteLine("Currently logged in as: " + web.CurrentUser.LoginName.Split(new string[] { @"\" }, StringSplitOptions.None)[1]);
                    System.Console.WriteLine("Currently logged in as: " + web.CurrentUser.LoginName.Split(new char[] { '\\' }, StringSplitOptions.None)[0]);
                    var z = "";
                    
                    //switchUser(web, siteStr, "ReportsKSUIT");
                    //switchUser(web, siteStr, "sp_search");
                    //switchUser(web, siteStr, "BlackNinjaSoftware/ShereenQumsieh");
                    //switchUser(web, siteStr, "BlackNinjaSoftware/DonabelSantos");
                }
            }
        }

        private static void TestAnnouncmen()
        {

            string siteStr = "http://spb12-it-crdev3:8080";

            //we just need to get a handle to the site for us
            //to get the system account user token
            SPSite tempSite = new SPSite(siteStr);

            SPUserToken systoken = tempSite.SystemAccount.UserToken;

            using (var site = new SPSite(siteStr, systoken))
            {
                using (var web = site.OpenWeb())
                {
                    var controllers = new CItemAnnouncement(web);
                    var items = controllers.GetAnnouncements(6, web.CurrentUser.ID);

                    

                    if (items == null) return;
                    items.OrderBy(a => a.DateOfAnnonce);

                    var lstString = generateList(items);

                    if (lstString.Count < 6)
                    {
                        for (int a = lstString.Count; a < 6; a++)
                        {
                            lstString.Add(string.Empty);
                        }

                    }

                    var sb = new StringBuilder();

                    var table = @"<table>
                            <tr style='vertical-align: top;padding-right: 10px;'><td style='width:50%;border-bottom: 1px solid rgb(209, 230, 249)'>{0}</td><td style='width:50%;padding-left: 10px;border-bottom: 1px solid rgb(209, 230, 249)'>{1}</td></tr>
                            <tr style='vertical-align: top;padding-right: 10px;'><td style='width:50%;border-bottom: 1px solid rgb(209, 230, 249)'>{2}</td><td style='width:50%;padding-left: 10px;border-bottom: 1px solid rgb(209, 230, 249)'>{3}</td></tr>
                            <tr style='vertical-align: top;padding-right: 10px;'><td style='width:50%'>{4}</td><td style='width:50%;padding-left: 10px;'>{5}</td></tr>
                        </table>";
                    //for (int i = 0; i < items.Count(); i++)
                    //{
                    sb.AppendFormat(table, lstString.ToArray());
                    //}
                    var z = sb.ToString();
                    return;

                }
            }
        }

        private static List<string> generateList(List<SP.SPModel.ModelAnnouncement> items)
        {
            var lst = new List<string>();
            foreach (SP.SPModel.ModelAnnouncement model in items)
            {
                var sb = new StringBuilder();
                if (model.Content.Length > 180)
                    model.Content = model.Content.Remove(180, model.Content.Length - 180) + "...";

                sb.AppendLine("<div class='announce'>");
                sb.AppendLine("<div class='h-2'>");
                sb.AppendFormat("<span class='date' data-date='{0}' data-format='dd MMMM yyyy' data-icon='true'></span>", model.DateOfAnnonce.ToShortDateString());
                sb.AppendLine("</div>");
                sb.AppendLine("<div class='h-1'>");
                sb.AppendFormat("<span class='text link' data-id='{0}' onclick='open_news({0})'>{1}</span>", model.ID, model.Header);
                sb.AppendLine("</div>");
                sb.AppendLine("<div class='h-2'>");
                if (model.Content.Length < 180)
                    sb.AppendFormat("<span class='text'>{0}</span>", model.Content);
                else
                    sb.AppendFormat("<span class='text'>{0}<span class='link' onclick='open_news({1})'>&nbsp;&nbsp;&nbsp;Подробнее</span></span>", model.Content, model.ID);
                sb.AppendLine("</div>");
                sb.AppendLine("<div class='f-1'>");
                sb.AppendFormat("<span id='announceVisit_{0}' class='overview' data-val='{1}' data-active='{2}'></span>", model.ID, model.Visit, model.Visited.ToString().ToLower());
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");

                lst.Add(sb.ToString());

            }

            return lst;
        }
            



        [Serializable]
        [XmlType(TypeName = "comment")]
        public class Comment
        {
            [XmlElement("by")]
            public string By { get; set; }
            [XmlElement("date")]
            public DateTime Date { get; set; }
            [XmlElement("text")]
            public string Text { get; set; }
        }


        public static void TestComment()
        {
            var id = "SD-4827813";

            
            var Item = InteractionController.GetById(id);
            var comm = Item.Comments;
            Item.Comments.AddRange(InteractionController.GetComments(Item, id));
            Item.Comments.AddRange(comm);

            var noduplicates = Item.Comments.Distinct().Select(s=>s).ToList();
            var z = "";
            return;


            //var ArrayOfcomment = new List<Comment>();
            //var commentsXml = "<comment><by>notfound</by><date>2018-04-25T17:18:24</date><text>рапрапр апрапр р рарапр</text></comment>";
            //if (!string.IsNullOrEmpty(commentsXml))
            //{
            //    var comments = string.Format("<ArrayOfcomment xmlns=\"{0}\">{1}</ArrayOfcomment>",
            //        Base.Constants.DataContract.NameSpace, commentsXml)
            //        .ConverXml<List<Comment>>();
            //}

            //var deserializer = new JavaScriptSerializer();
            //var z =  deserializer.Deserialize<List<Comment>>(commentsXml);

//            var commentsXml = @"<comments><comment><by>notfound</by><date>2018-04-25T17:18:24</date><text>рапрапр апрапр р рарапр</text></comment>
//                                <comment><by>zzzzz</by><date>2018-04-25T17:18:24</date><text>5676879</text></comment></comments>";
//            XmlDocument xml = new XmlDocument();
//            xml.LoadXml(commentsXml);
//            var type = (typeof(Comment));
//            Type[] typeArr = new Type[]{};
//            var seri = new System.Xml.Serialization.XmlSerializer(type, typeArr);

//            using (var reader = new XmlNodeReader(xml.DocumentElement))
//            {
//                reader.MoveToContent();
//                reader.ReadStartElement();
//                while (reader.IsStartElement())
//                {
//                    var entry = seri.Deserialize(reader);
//                    var z = ""; ;
//                }
//            }


            var commentsXml = @"<ArrayOfComments><comment><by>notfound</by><date>2018-04-25T17:18:24</date><text>рапрапр апрапр р рарапр</text></comment>
                                <comment><by>zzzzz</by><date>2018-04-25T17:18:24</date><text>5676879</text></comment></ArrayOfComments>";
            //using (var reader = new StringReader(commentsXml))//StreamReader(//("users.xml"))
            //{
            //    var deserializer = new System.Xml.Serialization.XmlSerializer(typeof(ArrayOfComments), new System.Xml.Serialization.XmlRootAttribute("comment_list"));
            //    comments = (ArrayOfComments)deserializer.Deserialize(reader);
            //    var z = "";
            //}

            commentsXml = @"<?xml version='1.0' encoding='utf-16'?><ArrayOfComment xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                <comment><by>notfound</by><date>2018-04-25T17:18:24</date><text>рапрапр апрапр р рарапр</text></comment>
                                <comment><by>zzzzz</by><date>2018-04-25T17:18:24</date><text>5676879</text></comment>
                            </ArrayOfComment>";

            List<Comment> Comments = new List<Comment>();
            Comments.Add(new Comment { Date = DateTime.Now, By = "sdsd", Text = "sss" });
            Comments.Add(new Comment { Date = DateTime.Now, By = "2222", Text = "3333" });
            Comments.Add(new Comment { Date = DateTime.Now, By = "55", Text = "99" });

            var mySerializer = new System.Xml.Serialization.XmlSerializer(Comments.GetType());
            string str; 
            // To write to a file, create a StreamWriter object.
            StringWriter myWriter = new StringWriter();//("myXML.xml");
            mySerializer.Serialize(myWriter, Comments);
            str = myWriter.ToString();
            myWriter.Close();



            var myDeserializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Comment>));
            List<Comment> ToDoList;
            using (var reader = new StringReader(commentsXml))
            {
                ToDoList = (List<Comment>)myDeserializer.Deserialize(reader);
            }

            commentsXml = @"<?xml version='1.0' encoding='utf-8'?><ArrayOfComment xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
                                <comment><by>notfound</by><date>2018-04-25T17:18:24</date><text>рапрапр апрапр р рарапр</text></comment>
                                <comment><by>zzzzz</by><date>2018-04-25T17:18:24</date><text>5676879</text></comment>
                            </ArrayOfComment>";
            if (!string.IsNullOrEmpty(commentsXml))
            {
                var comments = commentsXml.ConverXml<List<Comment>>();
            }


            //return DeserializeFromXmlDocument(xml);

        }

        private static void TestMailBoxSize()
        {
            string resultPage = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://spb99-cas.gazprom-neft.local/owa/");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true))
            {
                resultPage = sr.ReadToEnd();
                sr.Close();
            }
            var z = "";
        }



        //private static void TestINteractionComments()
        //{
        //    var accsessor = new HPSM.SQL.InteractionAccessor();
        //    List<string> lst = accsessor.GetLinkedNumbersOfComments("SD-4090311");
        //    List<Comment> comments = new List<Comment>();

        //    foreach (string num in lst)
        //        comments.AddRange(accsessor.GetLinkedCommentsByNumber(num));

        //    var z = comments.OrderBy(c => c.AddedDate).ToList();

        //    var x = z;
        //}

        private static void TestParsingTimeZone()
        {
            //var result = "MSK+15";
            var result = "MSK";
            var sign = result.Substring("MSK".Length, 1);
            var digit = result.Substring("MSK".Length + 1, result.Length - "MSK".Length - 1);

            var z = "";
        }

        private static void TestAgreementCreate()
        {
            var item = new ContractAgreement();
            item.Assigne = "Богданов Алексей Борисович (bogdanov.ab@ekb.it-sk.ru)";
            item.Approver = "Пивкорец Дмитрий Анатольевич (Pivkorets.DA@gazprom-neft.ru)";
            item.NumberZNI = "C-1900666";
            item.RoundNumber = 1;
            item.StageNumber = 20;

            //formData.append("aprovedBy", $(".lk-page").data("ksuit_contact") || "");
            //            formData.append("parentapproval", $(".lk-page").data("zno_number") || "");
            //            formData.append("stage", $(".lk-page").data("stage") || "");
            //            formData.append("roundnumber", $(".lk-page").data("roundnumber") || "");
            //            formData.append("approveruser", $(".lk-page").data("approveruser") || "");


            var accessor = new HPSM.SOAP.GpnHpcApprovalSelectAccsessor();
            var result =  accessor.Create(item);
        }

        private static void GetDocumentTemplate()
        {
            using (var site = new SPSite("http://spb12-it-crdev3:8080/"))
            {
                using (var web = site.OpenWeb())
                {
                    var controller = new CItemTemplateDoc(web);
                    var result = controller.GetTextFromFileName("111");

                }
            }
        }

        private static void TestAddAttachToItchanges()
        {
            var number = "C-2396649";
            var comment = "comment test";
            var addedBy = "Александров Дмитрий Викторович (Aleksandrov.DVi@it-sk.ru)";
            var pathToFile = @"C:\Users\Aleksandrov.dvi\Downloads\test.txt";
            var data = System.IO.File.ReadAllBytes(pathToFile);

            var attach = new AttachInfo[] 
            {
                new AttachInfo()
                {
                    Id = string.Empty,
                    Name = "test.txt",
                    Data = data,
                    Size = data.Length
                }
            };


            var accsessor = new GPNHpcAttachmentAddAttachAccessor();
            var result = accsessor.AddFiles(number, addedBy, comment, "Частное техническое задание (ЧТЗ)", attach);
        }

        private static void TestUpdateITChanges()
        {
            var number = "C-1592062";
            var comment = "comment test";
            var addedBy = "Абдулнасыров Эльдар Шавкатович (ABDULNASYROV.ESH@gazprom-neft.ru)";
            var accsessor = new GPNChangeInfrCm3rAddProtokolAccessor();
            var z = accsessor.AddProtocol(number, addedBy, comment);
        }

        

        private static void TestBackToWork()
        {
            var number = "C-1592062";
            var comment = "comment test";
            //var modifiedBy = "Абдулнасыров Эльдар Шавкатович (ABDULNASYROV.ESH@gazprom-neft.ru)";
            var accsessor = new GPNChangeInfrCm3rBakeWorkAccessor();
            var z = accsessor.BackWork(number, comment);
        }

        private static void TestNonConfirm()
        {

            var number = "C-1592062";
            var comment = "comment test";
            var modifiedBy = "Абдулнасыров Эльдар Шавкатович (ABDULNASYROV.ESH@gazprom-neft.ru)";
            var accsessor = new GPNChangeInfrCm3rConfirmedAccessor();
            var z = accsessor.NonConfirmed(number, comment, modifiedBy);
        }

        private static void TestConfirm()
        {
            var number = "C-1592062";
            var comment = "comment test";
            var modifiedBy = "Абдулнасыров Эльдар Шавкатович (ABDULNASYROV.ESH@gazprom-neft.ru)";
            var accsessor = new GPNChangeInfrCm3rConfirmedAccessor();
            var z = accsessor.Confirmed(number, comment, modifiedBy);
        }

        private static void TestParsing()
        {
            //string mainstr = @"Добавлено вложение <a href='scattach://59f97123000450a880ae4a48:C-1914695_ChTZ_v1.docx:hpcattachment:C-1914695_ik9mu'>C-1914695_ChTZ_v1.docx</a>Комментарий:213";
            string mainstr = @"Добавлено вложение <a href='scattach://59f9713b003b80aa80ae4a48:C-1914695_ChTZ_v2.docx:hpcattachment:C-1914695_y1q7t'>C-1914695_ChTZ_v2.docx</a>";
            var indexFirst = mainstr.IndexOf("<");
            var indexLast = mainstr.LastIndexOf(">") + 1;
            var substr = mainstr.Substring(indexFirst, indexLast - indexFirst);
            var htmlDom = XElement.Parse(substr);

            var nameDocument = htmlDom.Value;
            var commentaryIndex = mainstr.IndexOf("Комментарий:");// +"Комментарий:".Length;
            if (commentaryIndex != -1)
            {
                var comment = mainstr.Substring(commentaryIndex + "Комментарий:".Length, mainstr.Length - commentaryIndex);
            }

            var str = @"<b><font color='green'>Согласовано</font></b>:dfg dfg dg";
            indexFirst = str.IndexOf('<');
            indexLast = str.LastIndexOf(">") + 1;
            if(indexFirst == 0)
            {
                substr = str.Substring(indexFirst, indexLast - indexFirst);
                htmlDom = XElement.Parse(substr);
                var state = htmlDom.Value;
            }

            commentaryIndex = str.IndexOf(":") + 1;// +"Комментарий:".Length;
            if (commentaryIndex != -1)
            {
                var comment = str.Substring(commentaryIndex, str.Length - commentaryIndex);
            }

            str = @"На устранение замечаний Инициатором:dfg dfgd fgdfg";




        }

        private static void TestregExpress()
        {
            string[] names = new string[] 
            {
                "C-1915663_PT_v1.jpg","C-1915663_ChTZ_v2.rar","C-1915663_AZ_v1.jpg","C-1915663_ZnI_v1.jpg","C-1915663_DO_v1.jpg",
                "C-1915663_SR_v1.jpg","C-1915663_OI_v1.jpg","C-1915663_PN_v1.jpg","C-1915663_ChTZ_v1.rar","184_5.jpg","Scan.zip"
            };

            foreach (string str in names.ToList())
            {
                var z = Regex.IsMatch(str, "_PT_v");
                var match = Regex.Match(str, "_PT_z");
                var r = match.Success;
                var match2 = Regex.Match(str, "_PT_v");
                var index = match2.Index;
                var len = match2.Length;

                var version = str.Substring(index + len).Split('.')[0];

                var zz = "";
            }
        }

        private static void TestAttachItChanges()
        {
            var acsessor = new GPNHpcAttachmentGetAttachAccessor();
            var items = acsessor.GetAttachments("C-1915663");
            // test get one attach
            //var item = acsessor.GetAtachmentByNumberAndCid(items.ToArray()[0].Name.Split('.')[0], items.ToArray()[0].Id); 
        }

        private static void TestGetDeviseRusName()
        {
            var acsessor = new GPNGetDevtypeAccessor();
//            hpcbizproc
//hpcbizsol
//hpcbizsystem
//hpccctv
//hpcconsumable
//hpccontainer
//hpcdataCenter
//hpcdrive
//hpcengineeringsystems
//hpcinstalledsoftware
//hpclocation
//hpcmultimedia
//hpcnetworkdevice
//hpcofficeequipment
//hpcpac
//hpcparts
//hpcrack
//hpcserver
//hpcsoftware
//hpcsoftwarelicense
//hpcsoftwareresource
//hpctechsystem
//hpcworkplaceequipment
            var items = acsessor.GetTypeRusName("hpcsoftware");
        }
        

        private static void TestCreateXmlFile()
        {
            using (var site = new SPSite("http://spb12-it-crdev3:8080/"))
            {
                using (var web = site.OpenWeb())
                {
                    var acsessor = new ItChangesAccessor();
                    var items = acsessor.GetAllConfigUnit();
                    
                    var controller = new CItemConfigUnit(web);
                    var rezult = controller.CreateNewRecord(items, "configuration_of_unit.xml");

                    var tst = controller.GetAllConfigUnits();
                }
            }



        }

        private static void TestGetITChange()
        {
            var item = ItChangesController.GetItChangeByNumber("C-1743307");
        }

        private static void TestGetAllCategory()
        {
            var acsessor = new ItChangesAccessor();
            var items = acsessor.GetAllConfigUnit();
        }

        //private static void TestCreateITChange()
        //{
        //    //var accessor = new GPNGetDeviceInfoAccessor();
        //    //var list = accessor.Update();

        //    var item = new ItChangesInfo();
        //    item.AffectedItem = "APP.1C-EUS"; //
        //    item.Assets = new string[] { "1C.ASUP", "GPN-AERO-SPB-NET-000228" };
        //    item.Company = "Клуб Заречье ООО";
        //    item.Description = new string[] {"описание большое"};
        //    item.EffectNotImpl = new string[] { "риск неисполнения"};
        //    item.HpcCallbackContact = "Абдулнасыров Эльдар Шавкатович (ABDULNASYROV.ESH@gazprom-neft.ru)";
        //    item.HpcCauseObjectId = "Номер Инцидента/Проблемы/Проекта";
        //    item.HpcLocation = "СПБ Зоологический переулок, д. 2-4";
        //    item.HpcNextBreach = DateTime.Now;
        //    item.HpcNotifyList = new string[] { };//???
        //    item.HpcRegion = "Санкт-Петербург";
        //    //item.HpcStatus = "";//???
        //    item.InitialImpact = "Низкое";
        //    item.Initiator = "Кириллова Наталья Евгеньевна (Kirillova.NEv@gazprom-neft.ru)";
        //    //item.Number = 
        //    item.Severity = "Низкое";
        //    item.ShortDescription = "краткое описание";
        //    item.Reason = "Решение инциндента";
        //    item.Justification = new string[] { "описание обоснования" };
        //    item.HpcSolution = "Описание альтернативного или временного решения";
        //    item.RequestDate = DateTime.Now;

        //    var accsessor = new GpnCreateChangeInfrCm3rAccessor();
        //    accsessor.Add(item);





        //}

        #region checkSqlConnectHonorDesk
        private static void checkSqlConnectHonorDesk()
        {
            // тестирование подключение к базе под спец пользователем
            //string siteStr = "http://sp2010/";
            string siteStr = "http://selfservice-qa.gazprom-neft.local/";

            //we just need to get a handle to the site for us
            //to get the system account user token
            SPSite tempSite = new SPSite(siteStr);

            SPUserToken systoken = tempSite.SystemAccount.UserToken;

            using (SPSite site = new SPSite(siteStr, systoken))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    //right now, logged in as Site System Account
                    System.Console.WriteLine("Currently logged in as: " +
                                     web.CurrentUser.ToString());

                    //add your code here
                }
            }
        }
        #endregion

        #region impersonateTest
        private static void impersonateTest()
        {
            //string siteStr = "http://sp2010";
            string siteStr = "http://selfservice-qa.gazprom-neft.local/";
            SPSite tempSite = new SPSite(siteStr);
            SPUserToken systoken = tempSite.SystemAccount.UserToken;
            using (SPSite site = new SPSite(siteStr, systoken))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    //right now, logged in as Site System Account
                    System.Console.WriteLine("Currently logged in as: " +
                                       web.CurrentUser.ToString());
                    switchUser(web, siteStr, "ReportsKSUIT");
                    //switchUser(web, siteStr, "sp_search");
                    //switchUser(web, siteStr, "BlackNinjaSoftware/ShereenQumsieh");
                    //switchUser(web, siteStr, "BlackNinjaSoftware/DonabelSantos");
                }
            }
        }
        #endregion

        #region switchUser
        private static void switchUser(SPWeb web, string siteStr, string userName)
        {
            //impersonate somebody else
            SPUser userKSUIT = null;
            bool isUserFind = false;
            foreach (SPUser user in web.AllUsers)
            {
                System.Console.WriteLine("User login: " + user.Name);
                if (user.Name == userName)
                {
                    userKSUIT = user;
                    isUserFind = true;
                    break;
                }
            }
            if (isUserFind)
            {
                using (var site = new SPSite(siteStr, userKSUIT.UserToken))
                {
                    using (var otherWeb = site.OpenWeb())
                    {
                        System.Console.WriteLine("Currently logged in as: " +
                                         otherWeb.CurrentUser.ToString() +
                                         "(" + otherWeb.CurrentUser.Name + ")"
                    );
                        try
                        {

                            var items = HPSMClient.GetHonorDesk(20);

                            //var connectString = "Server=SPB99-SMDB03.gazprom-neft.local;Database=SMInfo;";
                            //var conn = new SqlConnection(connectString);
                            //conn.Open();
                            //conn.Close();
                            System.Console.WriteLine("****-------------------Подключение успешно-------------------****");
                        }
                        catch (Exception ex)
                        {
                            System.Console.WriteLine("!!!!-------------------Подключиться не удалось-------------------!!!!");
                            System.Console.WriteLine(ex.Message);
                        }
                    }
                }
                SPSite s = new SPSite(siteStr, userKSUIT.UserToken);
                SPWeb w = s.OpenWeb();

            }
        }
        #endregion

        #region clearJsonResponse
        private static string clearJsonResponse(string json)
        {
            json = "{'result':'OK','value':{'avgMark':'4,88','countApp':'114 370','countAppSolved':'22 340','perSLAKC':'98,94','perTelAvail':'98,00'}}";
            var clearJson = json.Split('{');
            if (clearJson.Length == 3)
            {
                clearJson = clearJson[2].Split('}');
                return "{" + clearJson[0] + "}";
            }

            return json;
        }
        #endregion

        #region Test1
        private static void Test1()
        {
            var newVal = new IndicatorsOfGroupItem()
            {
                avgMark = "4,88",
                countAppSolved = "22556",
                perSLAKC = "98,96",
                perTelAvail = "98,01"
            };

            var oldVal = new IndicatorsOfGroupItem()
            {
                avgMark = "4,88",
                countAppSolved = "22555",
                perSLAKC = "98,96",
                perTelAvail = "98,01"
            };

            //var result = RunServices.Statistics.updateSensors(new List<IndicatorsOfGroupItem>() {oldVal, newVal});
            var z = "";




            return;

            //var jsonData = "{'avgMark':'24,71','countApp':'55,45','countAppSolved':'54,14','perSLAKC':'63,88','perTelAvail':'3,82'}";
            //var jsonData =
            //    "{\"avgMark\":\"5,10\",\"countApp\":\"50,04\",\"countAppSolved\":\"95,70\",\"perSLAKC\":\"46,08\",\"perTelAvail\":\"36,45\"}";
            using (var site = new SPSite("http://sp2010"))
            {
                using (var web = site.OpenWeb())
                {
                    var jsonData =
                        "{'result':'OK','value':{'avgMark':'4,88','countApp':'114 370','countAppSolved':'22 340','perSLAKC':'98,94','perTelAvail':'98,00'}}";
                    jsonData = clearJsonResponse(jsonData);
                    //var jsonData = RunServices.GetStatistic();
                    var newItem = IndicatorsOfGroupLoader.ConvertToIndicatorsOfGroupItem(jsonData);
                    var oldItem = new ListItemLoader<IndicatorsOfGroupItem>(web, ListConfigs.IndicatorsOfGroupListConfig).GetItems().ToList().FirstOrDefault(i => i.ID == 2);
                    IndicatorsOfGroupLoader.UploadDataToList(web, newItem, oldItem);
                }
            }
        }
        #endregion

        #region TestHonorDesk
        private static void TestHonorDesk()
        {
            var items = HPSMClient.GetHonorDesk(20);
        }
        #endregion

        #region TestHPSMServices
        private static void TestHPSMServices()
        {
            var result =
                HPSMClient.GetAllInteractionJsonByContact("Глибчук Илья Николаевич (Glibchuk.IN@spb.it-sk.ru)");
            //var result =
            //    HPSMClient.IsContactExists("Глибчук Илья Николаевич (Glibchuk.IN@spb.it-sk.ru)");
            // using (var site = new SPSite("http://sp2010"))
            // {
            //     var tj = new EsisGarthnerTimerJob(site.WebApplication);
            //     tj.Execute(Guid.Empty);
            // }
            //var result = HPSMClient.GetAllInteractionJsonByContact("Mihailov.IV");
            //var result = HPSMClient.GetInteractionAttachment("SD-0024397", "cid:542cefea003510a780f61798", "application/octet-stream");
            //var result = HPSMClient.GetInteractionAttachmentsJson("SD-0030740");

            //var result = HPSMClient.GetInteractionAttachmentsJson("SD-0713124");

            //byte[] fileData = System.IO.File.ReadAllBytes(@"D:\Solutions\TestSolution\TestConsole\TextFile1.txt");
            //var result = HPSMClient.CreateInteractionAttachment("SD-0713124", "TextFile1.txt", fileData);
            //var result = HPSMClient.CreateInteractionAttachment("SD-0713124", "TextFile1.txt", fileData);

            //System.Console.WriteLine(result);
        }
        #endregion

        #region TestHPSMWinAuth
        private static void TestHPSMWinAuth()
        {
            #region Credential

            var domain = "gazprom-neft";
            var user = "gladkiy.am";
            var pswd = "";

            #endregion

            using (new Impersonator(user, domain, pswd))
            {
                var request = (HttpWebRequest)WebRequest.Create("http://tst-hpsm-sso.gazprom-neft.local/SM/9/rest/interactions");
                request.Proxy = null;
                request.Method = WebRequestMethods.Http.Get;
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version10;

                //request.UseDefaultCredentials = true;
                //request.Credentials = CredentialCache.DefaultCredentials;
                request.Credentials = new NetworkCredential(user, domain, pswd);

                var response = (HttpWebResponse)request.GetResponse();
                System.Console.WriteLine(response.StatusCode);

                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        return;
                    }

                    var responseReader = new StreamReader(responseStream);
                    var jsonString = responseReader.ReadToEnd();

                    System.Console.WriteLine(jsonString);
                    var data = JsonSerializer.Deserialize<InteractionContent>(jsonString, new[] { typeof(ITSK.SSS.Contracts.Interaction) });
                }
            }
        }
        #endregion

        #region TestSearchContactName
        private static void TestSearchContactName()
        {
            var json = RunServices.UserInfo.SearchContactName("asd");
            System.Console.WriteLine(json);
        }
        #endregion

        #region TestInteractionTemplate
        private static void TestInteractionTemplate()
        {
            //var result = HPSMClient.GetInteractionTemplateByName("TeamMate: Добавление объекта глобальной иерархии");

            //var result = HPSMClient.GetDepartmentsTemplates();
            try
            {
                var accessor = new InteractionTemplateAccessor();
                var result = accessor.GetByName("TeamMate: Добавление объекта глобальной иерархии");
                System.Console.WriteLine("is Good");
                System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Mazafaka!!!  NOT GOOD");
                System.Console.WriteLine(ex.Message);
                System.Console.ReadKey();
            }


            //var config = new SqlConfig
            //{
            //    ConnectionString = "Server=SPB99-T-HPSMDB.gazprom-neft.local;Database=GPNHPSMNEW;Trusted_Connection=True;",
            //    IsNeedImpersonate = SssConfig.Current.IsNeedImpersonate,
            //    ImpersonateDomain = SssConfig.Current.ImpersonateDomain,
            //    ImpersonatePassword = SssConfig.Current.ImpersonatePassword,
            //    ImpersonateUser = SssConfig.Current.ImpersonateUser
            //};

            //var accessor = new ITSK.SSS.HPSM.SQL.TemplateAccessor();
            //var result = accessor.SelectDepartmentsTemplates();

            //System.Console.WriteLine(result.Length);
        }
        #endregion

        #region TestLkMenu
        private static void TestLkMenu()
        {
            using (var site = new SPSite("http://spb12-it-crdev3:8080/"))
            {
                using (var web = site.OpenWeb())
                {
                    var accessor = new LkMenuAccessor(web);
                    var data = accessor.GetLkMenu();
                }
            }
        }
        #endregion

        #region TestInteraction
        private static void TestInteraction()
        {
            //var accessor = new HPSM.SQL.InteractionAccessor();
            //var result = accessor.GetInteraction("SD-0034799");

            //System.Console.WriteLine("SvcOptions: {0}", result.SvcOptions);
            //System.Console.WriteLine("Attachments: {0}", JsonSerializer.Serialize(result.Attachments));

            //var str = "<attachment><href>5451d0000017a6c280fe4130</href><name>Zaregistrirovano obrashhenie AZS SD-0034799.msg</name><content_type>application/octet-stream</content_type><len>4.096000000000000e+004</len></attachment><attachment><href>5460687b0027704d807e3130</href><name>Obrashhenie AZS SD-0034799  izmenilsja krajjnijj srok.msg</name><content_type>application/octet-stream</content_type><len>4.096000000000000e+004</len></attachment>";
            //var str = "<attachment><href>5451d0000017a6c280fe4130</href><name>Zaregistrirovano obrashhenie AZS SD-0034799.msg</name><content_type>application/octet-stream</content_type><len>4.096000000000000e+004</len></attachment>";
            //var res = XmlSerializer.Deserialize<HpsmAttachment>(str);
            //System.Console.WriteLine("Attachments: {0}", JsonSerializer.Serialize(res));
        }

        #endregion

        private static void TestTemplateEngine_2()
        {
            #region xml

            //const string xmlStr = "<form><select id=\"pole1\" label=\"Профиль «Публикатор» предназначена для сотрудников, ответственных за добавление сканов первичных учетных документов в систему,\" style=\"radio\" type=\"2\" /><select id=\"pole2\" label=\"согласование документов &quot;Заявка на освоение (короткий маршрут)&quot;, &quot;Заявка на освоение&quot;, &quot;Заявки на расходование средств&quot;, &quot;Акт сверки взаиморасчетов&quot;\" style=\"radio\" type=\"2\" /><select id=\"pole3\" label=\"формирование и передачу в архив первичных бухгалтерских документов\" style=\"radio\" type=\"2\">Да<option id=\"id20\" label=\"Да\">Да</option><option id=\"id21\" label=\"Нет\">Нет</option></select><select id=\"pole4\" label=\"Поле 4\" style=\"radio\" type=\"2\">2<option id=\"id30\" label=\"1\">1</option><option id=\"id31\" label=\"2\">2</option><option id=\"id32\" label=\"3\">3</option><option id=\"id33\" label=\"4\">4</option></select><select id=\"pole5\" label=\"Поле 5\" style=\"combo\" type=\"2\">3<option id=\"id4empty\" label=\"\" /><option id=\"id40\" label=\"1\">1</option><option id=\"id41\" label=\"2\">2</option><option id=\"id42\" label=\"3\">3</option><option id=\"id43\" label=\"4\">4</option></select><text id=\"pole6\" label=\"Поле 6\" style=\"combo\" type=\"1\">Проверка строки</text><text id=\"pole7\" label=\"Поле 7\" multiline=\"true\" style=\"combo\" type=\"2\">Проверка&#xD;текстового поля</text><text id=\"pole8\" label=\"Поле Дата\" style=\"combo\" type=\"3\">По факту тут тоже строка</text><checkbox id=\"pole9\" label=\"поле 9\" style=\"combo\" type=\"2\">false</checkbox><checkbox id=\"pole10\" label=\"поле 10\" style=\"combo\" type=\"2\">true</checkbox><select id=\"pole11\" label=\"поле 11\" style=\"radio\" type=\"1\">4<option id=\"id100\" label=\"1\">1</option><option id=\"id101\" label=\"2\">2</option><option id=\"id102\" label=\"3\">3</option><option id=\"id103\" label=\"4\">4</option><option id=\"id104\" label=\"5\">5</option></select><select id=\"pole12\" label=\"поле 12\" style=\"combo\" type=\"1\">3<option id=\"id11empty\" label=\"\" /><option id=\"id110\" label=\"1\">1</option><option id=\"id111\" label=\"2\">2</option><option id=\"id112\" label=\"3\">3</option><option id=\"id113\" label=\"4\">4</option></select><select id=\"srok\" label=\"Срок действия УЗ:\" style=\"radio\" type=\"2\" /><checkbox id=\"ogr\" label=\"Ограничен\" style=\"radio\" type=\"2\">true</checkbox><checkbox id=\"neogr\" label=\"Не ограничен\" style=\"radio\" type=\"2\" /><text id=\"do\" label=\"Дата ограничения доступа (если ограничен)\" style=\"radio\" type=\"2\">СТРОКА</text></form>";
            const string xmlStr = "<form><select id=\"dopoborudovanie\" label=\"К текущему типу АРМ установить дополнительное оборудование*\" mandatory=\"1\" style=\"combo\" type=\"2\">Монитор<option id=\"id0empty\" label=\"\"/><option id=\"id00\" label=\"Монитор\">Монитор</option><option id=\"id01\" label=\"Поточный сканер\">Поточный сканер</option><option id=\"id02\" label=\"Дополнительный телефон\">Дополнительный телефон</option><option id=\"id03\" label=\"ИБП\">ИБП</option><option id=\"id04\" label=\"Сканер штрих-кода\">Сканер штрих-кода</option><option id=\"id05\" label=\"Гарнитура для телефона\">Гарнитура для телефона</option><option id=\"id06\" label=\"Ноутбук\">Ноутбук</option><option id=\"id07\" label=\"Планшет\">Планшет</option></select><text id=\"istochnik\" label=\"Источник финансирования/Номер схемы оказания услуг:*\" mandatory=\"1\" style=\"combo\" type=\"2\">1 2 3 4 5</text><text id=\"mesto\" label=\"Месторасположение (кабинет или этаж)*\" mandatory=\"1\" style=\"combo\" type=\"2\">1-1</text><select id=\"date\" label=\"дата возврата\" style=\"combo\" type=\"3\"/></form>";
            #endregion

            //bool isTemporary = true;
            //bool isCanBeTemporary = true;

            //var form = new TemplateForm(xmlStr,  isCanBeTemporary,  isTemporary, false);

            //var html = form.GetHtml();
            //var js = form.GetJs();

            //File.WriteAllText(@"C:\Setup\test.html", html + js);
          

            //System.Console.WriteLine(html);
            //Console.WriteLine(js);

        }

        private static void TestSOAPCreate()
        {
            var item = new ITSK.SSS.Contracts.Interaction
            {
                Action = InteractionAction.Add,
                ServiceRecipient = "Гладкий Артём Михайлович (GladkiyAM@it-sk.ru)",
                HpsmTitle = "test 100",
                DescriptionString = "description for test 10",
                Contact = "Гладкий Артём Михайлович (GladkiyAM@it-sk.ru)",
                Category = "Инцидент",
            };

            var msg = InteractionCreateController.Add(item);
            System.Console.WriteLine(msg);

        }

        private static void TestSOAPUpdate()
        {
            var item = new ITSK.SSS.Contracts.Interaction
            {
                Action = InteractionAction.Edit,
                Contact = "Гладкий Артём Михайлович (GladkiyAM@it-sk.ru)",
                CallID = "SD-3278841",
                UserComment = "test update"
            };
            var msg = InteractionController.AddProtokol(item);
            System.Console.WriteLine(msg);
        }

        private static void TestSOAPClose()
        {
            var item = new ITSK.SSS.Contracts.Interaction
            {
                Action = InteractionAction.Edit,
                Contact = "Гладкий Артём Михайлович (GladkiyAM@it-sk.ru)",
                CallID = "SD-3166948",
                UserComment = "test close",
                HpcCloseMark = "5"
            };
            var msg = InteractionController.Close(item);
            System.Console.WriteLine(msg);
        }

        private static void TestSOAPDownload()
        {
            var attach = InteractionController.GetAttach("SD-6057939", "5ae44ffd001ddd0382f727e0");
            System.Console.WriteLine("{0}", attach != null && attach.Content != null ? attach.Content.Length.ToString() : "empty");
        }
    }
}
