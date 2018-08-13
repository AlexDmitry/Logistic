var activArr = [];
var arhiveArr = [];

var timeActiv = new Array();
var $timeArhive = [];

function createForumMainPage() {
    $("#forumTabsTrip").kendoTabStrip({
        animation: {
            open: {
                effects: "fadeIn"
            }
        }
    });

    getActiveSection();
}

function getActiveSection() {
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Разделы форума";
    var viewFields = "?$select=ID,Title,Type,DisplayOrder";
    var filter = "&$filter=Type eq 'Activ'&$orderby=DisplayOrder";
    getItemsByFiltrOnlyFields(spHostUrl, listTitle, viewFields, filter, fillSection, error);
}

function fillSection(data) {
    var spHostUrl = _spPageContextInfo.siteAbsoluteUrl;
    var listTitle = "Топики форума";
    var viewFields = "?$select=ID,Title,Section/Id,Section/Title&$expand=Section/Id";
    var filter = "";

    $.each(data.d.results, function (i, obj) {
        //createSection(obj);
        timeActiv.push({ "id": obj.ID, "result": false });
        filter = "&$filter=Section/Id eq " + obj.ID;
        var params = [obj.ID]; // ид раздела
        getItemsByFiltrOnlyFields(spHostUrl, listTitle, viewFields, filter, fillTopics, error, params);
    });   
}

function fillTopics(data, params) {
    var id = -1;
    $.each(data.d.results, function (index, itm) {
        if (typeof itm.Section.Id != 'indefined') {
            
            id = itm.Section.Id;
            var element = { "titleSection": itm.Section.Title, "titleTopic": itm.Title };
        }
        activArr.push(element);
    });
    if (id != -1) {
        var element = $.grep(timeActiv, function (e) { return e.id == id; });
        if (element.length == 1) {
            element[0].result = true;
        }
        checkFillAlldata(id);
    } else {
        $.each(timeActiv, function(i, obj) { //для каждого раздела, где нет топика не ждем загрузки данных
            if (obj.id == params[0]) {
                obj.result = true;
            }
        });
        checkFillAlldata(id);
    }
}

function checkFillAlldata(id) {

    var result = true;
    $.each(timeActiv, function(i, obj) {
        if (obj.result == false) {
            result = false;
        }
    });

    if (result) {
        fillGrid();
    }
}

function createSection(section) {
    var templateSection = kendo.template($("#templateSection").html());
    var dataSection = { title: section.Title, id: section.ID };
    var result = templateSection(dataSection); //Execute the template
    $("#gridActive").append(result); //html(result);
}

function error(data) {
    
}

function fillGrid() {
    var columns = getColumns_Forum();

    $('#gridActive').kendoGrid({
        dataSource: {
            data:activArr,
            group: {
                field: "titleSection"
            }
        },
        columns: columns,
        //filterable: {
        //    extra: false
        //},
        //sortable: {
        //    mode: "single",
        //    allowUnsort: false
        //},
        //pageable: {
        //    pageSize: 20
        //},
        //scrollable: false,
        sortable: true,
        
        groupable: false
    });
    
}

function getColumns_Forum() {
    return [
            {
                title: "Раздел", field: "titleSection", groupHeaderTemplate: "#= value #", hidden:true
                //template: "<a href='/#:data.FileRef#' onclick='return addCounterToDoc();' target='_blank'><img src='#:data.ImageDoc#' ></img></a>",
                //filterable: false, sortable: false
            },
            {
                title: "Тема", field: "titleTopic",
                //template: "<a href='javascript: showInfoWindow(#:data.ID#)' >#:data.Title#</a>",
                //filterable: false, sortable: false

            }
    ];
}