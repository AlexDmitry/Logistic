<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MainPageUserControl.ascx.cs" Inherits="ITSK.SSS.Forum.WebParts.MainPage.MainPageUserControl" %>


<link href="/_layouts/15/ITSK.SSS.UI/Styles/KendoUI/kendo.common.min.css" rel="stylesheet" />
<link href="/_layouts/15/ITSK.SSS.UI/Styles/KendoUI/kendo.rtl.min.css" rel="stylesheet" />
<link href="/_layouts/15/ITSK.SSS.UI/Styles/KendoUI/kendo.blueopal.min.css" rel="stylesheet" />
<link href="/_layouts/15/ITSK.SSS.UI/Styles/KendoUI/kendo.blueopal.mobile.min.css" rel="stylesheet" />

<script type="text/javascript" src="/_layouts/15/ITSK.SSS.UI/Scripts/kendo.all.min.js"></script>
<script type="text/javascript" src="/_layouts/15/ITSK.SSS.UI/Scripts/kendo.culture.ru-RU.js"></script>
<script type="text/javascript" src="/_layouts/15/ITSK.SSS.Forum/Scripts/forum-single-page.js" ></script>
<script type="text/javascript" src='/_layouts/15/ITSK.SSS.UI/Scripts/helper.js' ></script>

<script type="text/javascript">
    $(document).ready(function () {
        createForum();
    });
</script>


<script id="templateTopic" type="text/x-kendo-template">
    <li id='topic_#=id#'>
        <span class="k-link k-state-selected">#=title #</span>

        <div style="padding: 10px;">
            <div id='topic#=id #'></div>
        </div>
    </li>
</script>

<script id="templateMessage" type="text/x-kendo-template">
    <div>
        <div></div>
        <div><span>#=message #</span></div>
        <hr/>
    </div>
</script>

<script id="templateSectionElement" type="text/x-kendo-template">
    <div style='padding: 3px;'>
        <a href='javascript:createTopics(#=sectionId#, #=isArhive#)'>
            <span>#=sectionTitle #</span>
        </a>
        #if(!isArhive){#
        <input style='float: right' type='button' value='Создать новую тему' onclick='showNewTopicForm(#=sectionId#)'/>
        #}#
    </div>
    <hr/>
</script>

<script id="templateNewMessage" type="text/x-kendo-template">
    <div id='addNewMsg' >
        <div class="content">
            <textarea id='newMsg' class="text-area" placeholder="Введите текст."></textarea>
        </div>
        <div class="f-1">
        <input type='button' value='Отправить' onclick='saveMessage(#=topicId#)' />
        </div>
    </div>
</script>

<script id="templateNewTopcForm" type="text/x-kendo-template">
    <div id='tmplNewTpcFrm'>
        <textarea id='newTpc' class="text-area" rows="1" placeholder="Введите название новой темы"></textarea>
        <input type='button' value='Сохранить' onclick='saveNewTopic(#=sectionId#)' />
    </div>
</script>


<div style="display: inline-block">
    <div style="float: left;min-width: 300px">
        <ul id="pnlbSection">
            <li id="itemsActiv">
                <span class="k-link k-state-selected">Активные</span>
                <div style="padding: 10px;">
                    <div id="sectonActive"></div>
                </div>
            </li>
            <li id="itemsArhive">
                <span class="k-link">Архив</span>
                <div style="padding: 10px;">
                    <div id="sectionArhive"></div>
                </div>
            </li>
        </ul>
    </div>
    <div style="float: left;max-width: 500px" >
        <ul id="pnlbTopics"></ul>  
    </div>
</div>

<div id="newTopic"></div>













