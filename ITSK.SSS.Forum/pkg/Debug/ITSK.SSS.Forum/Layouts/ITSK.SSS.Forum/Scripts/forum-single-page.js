
var cntTopic = [];
var newTopicId = 0;
var isSectionArhive = false;

function createForum() {

    $("#pnlbSection").kendoPanelBar({
        expandMode: "MULTIPLE"
    });

    var panelBar = $("#pnlbSection").data("kendoPanelBar");
    panelBar.expand($('[id^="items"]'));
    panelBar.select("#itemActiv");

    createPanelSection();
}

function createPanelSection() {
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Форум Разделы";
    var viewFields = "?$select=ID,Title,Type,DisplayOrder";
    var filter = "";//"&$filter=Type eq 'Activ'&$orderby=DisplayOrder";
    getItemsByFiltrOnlyFields(spHostUrl, listTitle, viewFields, filter, fillSection, error);
}

function fillSection(data) {
    var arrSections = [];
    $.each(data.d.results, function (i, obj) {
        var isArhive = false;
        if (obj.Type == "Архивный") {
            isArhive = true;
        }
        var element = { "sectionTitle": obj.Title, "sectionId": obj.ID, 'isArhive': isArhive };
        arrSections.push(element);
        fillSections(element);
    });

    createTopics(arrSections[0].sectionId, arrSections[0].isArhive);
}

function createTopics(sectionId, isArhive) {
    cntTopic = [];
    isSectionArhive = isArhive;
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Форум Топики";
    var viewFields = "?$select=ID,Title,Section/Id,Section/Title&$expand=Section/Id";
    filter = "&$filter=Section/Id eq " + sectionId;
    getItemsByFiltrOnlyFields(spHostUrl, listTitle, viewFields, filter, fillTopics, error);
   
}

function error(data) {

}

function onChangeSection(arg) {

    var grid = $("#sections").data("kendoGrid");
    var selectedItem = grid.dataItem(grid.select());
    var item = { "ID": selectedItem.ID };
    createGridTopics(item);

}

function fillSections(element) {
    var tmplSectionElement = kendo.template($("#templateSectionElement").html());
    var result = tmplSectionElement(element); 

    if (element.isArhive) {
        $("#sectionArhive").append(result);
    } else {
        $("#sectonActive").append(result);
    }
}

function fillTopics(data) {

    $("#pnlbTopics").children().remove();

    var arrTopics = [];
    $.each(data.d.results, function (i, obj) {
        var element = {"topicTitle":obj.Title, "id":obj.Id}
        arrTopics.push(element);
        cntTopic.push({"topicId":obj.Id, 'messagesId':[]});
        createTopic(element);
    });

    $("#pnlbTopics").kendoPanelBar({
        expandMode: "single",
        select: onSelectTopic
    });
   
    if (newTopicId != 0) {
        setActiveBar(newTopicId);
        addNewMessagesSection(newTopicId);
        newTopicId = 0;
    } else {
        setActiveBar(arrTopics[0].id);
        fillMessageOfTopic(arrTopics[0]);
    }   
}

function setActiveBar(id) {
    var panelBar = $("#pnlbTopics").data("kendoPanelBar");
    panelBar.select('#topic_' + id);
    panelBar.expand('#topic_' + id);
}

function createTopic(element) {
    var tmplTopic = kendo.template($("#templateTopic").html());
    var dataTopic = { title: element.topicTitle, id:element.id };
    var result = tmplTopic(dataTopic); //Execute the template
    $("#pnlbTopics").append(result); //html(result);
}

function onSelectTopic(e) {
    var topicId = $(e.item).attr('id').split('_')[1];
    var element = { "id": topicId };
    fillMessageOfTopic(element);
}

function fillMessageOfTopic(item) {
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Форум Сообщения";
    var viewFields = "?$select=ID,Title,Text,Topic/Id,Topic/Title&$expand=Topic/Id";
    filter = "&$filter=Topic/Id eq " + item.id;

    var params = null;
    params = JSON.stringify({ "topicId": item.id });
    getItemsByFiltrOnlyFields(spHostUrl, listTitle, viewFields, filter, fillMessages, error, params);
}

function fillMessages(data, params) {
    if (data.d.results.length != 0) {
        var topicData = findTopicElement(data.d.results[0].Topic.Id);
        $.each(data.d.results, function(i, obj) {
            var element = { "message": obj.Text, 'topicId': obj.Topic.Id }
            if (topicData.messagesId.indexOf(obj.Id) == -1) {
                createMessage(element);
                topicData.messagesId.push(obj.Id);
            }
        });
    }
    if (!isSectionArhive) {
        var info = JSON.parse(params);
        addNewMessagesSection(info.topicId);
    }
}

function createMessage(element) {
    var tmplMsg = kendo.template($("#templateMessage").html());
    var result = tmplMsg(element); 
    var select = "div[id='topic" + element.topicId + "']";
   $(select).append(result);
}

function showNewTopicForm(sectionId) {

    var tmplNewTopicForm = kendo.template($("#templateNewTopcForm").html());
    var dataTopicForm = { "sectionId": sectionId };
    var result = tmplNewTopicForm(dataTopicForm); //Execute the template
    $("#newTopic").append(result); //html(result);


    var accessWindow = $("#newTopic").kendoWindow({
        actions: ["Pin",
                "Minimize",
                "Maximize",
                "Close"],
        draggable: true,
        height: "100px",
        modal: true,
        resizable: false,
        title: "Access",
        width: "500px",
        visible: false, /*don't show it yet*/
        close: function () { $('#tmplNewTpcFrm').remove(); }
    }).data("kendoWindow").center().open();

    $('#lbl').val(sectionId);
}

function saveNewTopic(sectionId) {

    var text = $('#newTpc').val();
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Форум Топики";
    var listTitleEng = "ForumTopic";
    var itemProperties = {};
    var itemProperties = { 'Title': text, 'SectionId': sectionId };
    addItem(spHostUrl, listTitle, listTitleEng, itemProperties, addNewTopic);

    $("#newTopic").data("kendoWindow").close();
}

function addNewTopic(data) {
    //обновляем топики, зажигаем нужный для получения всех сообщений.
    newTopicId = data.d.ID;
    createTopics(data.d.SectionId);
}

function addNewMessagesSection(topicId) {
    $('#addNewMsg').remove();
    var tmplMsg = kendo.template($("#templateNewMessage").html());
    var result = tmplMsg({ "topicId": topicId });
    var select = "div[id='topic" + topicId + "']";
    $(select).append(result);
}

function findTopicElement(id) {
    var element = null;
    $.each(cntTopic, function(i, obj) {
        if (obj.topicId = id) {
            element = obj;
            return false;
        }
    });
    return element;
}

function findMessageId(arr,id) {
    var element = null;
    $.each(arr, function (i, obj) {
        if (obj = id) {
            element = obj;
            return false;
        }
    });
    return element;
}

function saveMessage(topicId) {
    var text = $('#newMsg').val();
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Форум Сообщения";
    var listTitleEng = "ForumMessage";
    var itemProperties = {};
    var itemProperties = { 'Text': text, 'TopicId': topicId };
    addItem(spHostUrl, listTitle, listTitleEng, itemProperties, updateTopic);
}

function updateTopic(data) {
    var element = { "message": data.d.Text, 'topicId': data.d.TopicId }
    createMessage(element);
    addNewMessagesSection(data.d.TopicId);
}

