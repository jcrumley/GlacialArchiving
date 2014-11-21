//DO NOT UPDATE HERE, BUT UPDATE IN CodeSmith Template

var css_browser_selector = function () { var ua = navigator.userAgent.toLowerCase(), is = function (t) { return ua.indexOf(t) != -1; }, h = document.getElementsByTagName('html')[0], b = (!(/opera|webtv/i.test(ua)) && /msie\s(\d)/.test(ua)) ? ('ie ie' + RegExp.$1) : is('firefox/2') ? 'gecko ff2' : is('firefox/3') ? 'gecko ff3' : is('gecko/') ? 'gecko' : is('opera/9') ? 'opera opera9' : /opera\s(\d)/.test(ua) ? 'opera opera' + RegExp.$1 : is('konqueror') ? 'konqueror' : is('chrome') ? 'chrome webkit safari' : is('applewebkit/') ? 'webkit safari' : is('mozilla/') ? 'gecko' : '', os = (is('x11') || is('linux')) ? ' linux' : is('mac') ? ' mac' : is('win') ? ' win' : ''; var c = b + os + ' js'; h.className += h.className ? ' ' + c : c; } ();

String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/g, "");
}
String.prototype.ltrim = function () {
    return this.replace(/^\s+/, "");
}
String.prototype.rtrim = function () {
    return this.replace(/\s+$/, "");
}

Number.prototype.formatMoney = function (decPlaces, thouSeparator, decSeparator) {
    var n = this,
    decPlaces = isNaN(decPlaces = Math.abs(decPlaces)) ? 2 : decPlaces,
    decSeparator = decSeparator == undefined ? "." : decSeparator,
    thouSeparator = thouSeparator == undefined ? "," : thouSeparator,
    sign = n < 0 ? "-" : "",
    i = parseInt(n = Math.abs(+n || 0).toFixed(decPlaces)) + "",
    j = (j = i.length) > 3 ? j % 3 : 0;
    return sign + (j ? i.substr(0, j) + thouSeparator : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thouSeparator) + (decPlaces ? decSeparator + Math.abs(n - i).toFixed(decPlaces).slice(2) : "");
};

var days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];
Date.prototype.getDayName = function () {
    return days[this.getDay()];
};

var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
Date.prototype.getMonthName = function () {
    return months[this.getMonth()];
};

$(function () {
    InitControls();
    UpdateHistory();
});

function InitControls(){
    //http://pietschsoft.com/demo/jHtmlArea/
    if ($(".htmlarea").htmlarea) {
        $(".htmlarea").htmlarea({
            toolbar: ["html", "|", "forecolor", "bold", "italic", "underline", "strikethrough", "|", "increasefontsize", "decreasefontsize", "|",
                "p", "h1", "h2", "h3", "|", "image", "link", "unlink", "horizontalrule", "|", "orderedList", "unorderedList", "superscript", "subscript", "|",
                "indent", "outdent", "|", "justifyleft", "justifycenter", "justifyright", "|", "cut", "copy", "paste"]
        });
    }

    //http://silviomoreto.github.io/bootstrap-select/
    if ($('.selectpicker').selectpicker)
        $('.selectpicker').selectpicker();
    
    //http://jqueryui.com/tabs/
    if ($(".tabs").tabs)
        $(".tabs").tabs();
    
    //http://eonasdan.github.io/bootstrap-datetimepicker
    if ($('.datepicker').datetimepicker) {
        $('.datepicker').datetimepicker({ pickTime: false });
        $('.timepicker').datetimepicker({ pickDate: false, useSeconds: true });
        $('.datetimepicker').datetimepicker({ useSeconds: true });
    }
    
    //This is being done to only hide the bootstrap alerts rather than removing from the DOM
    $("[data-hide]").on("click", function () {
        $(this).closest("." + $(this).attr("data-hide")).hide();
    });
}

//navigation hooks
$('.minifyme').click(function (e) {
    $.get("/Account/ToggleMinify");
});
$('#hide-menu >:first-child > a').click(function (e) {
    $.get("/Account/ToggleCollapse");
});

//Get positions
function getCurrentScrollY() {
    var scrollPosition = document.documentElement.scrollTop;
    if (scrollPosition == 0 && document.body.scrollTop)
        scrollPosition = document.body.scrollTop;
    return scrollPosition;
}
function getMaxScrollY(dir) {
    if (!dir) dir = 1;

    if (dir >= 0 && dir != 1)
        dir = 1;
    if (dir < 0 && dir != -1)
        dir = -1;

    if (dir == -1)
        return 0;
    return document.documentElement.scrollHeight - document.documentElement.clientHeight;
}
function getTop(oElement) {
    var iReturnValue = 0;
    while (oElement != null) {
        iReturnValue += oElement.offsetTop;
        oElement = oElement.offsetParent;
    }
    return iReturnValue;
}
function getLeft(oElement) {
    var iReturnValue = 0;
    while (oElement != null) {
        iReturnValue += oElement.offsetLeft;
        oElement = oElement.offsetParent;
    }
    return iReturnValue;
}
function getWidth(oElement) {
    return oElement.offsetWidth;
}
function getHeight(oElement) {
    return oElement.offsetHeight;
}

//Message Over
var messageOverZIndex = 3000;
function ShowMessageOver(elem, message, showClose, altMode) {
    RemoveMessageOver(elem);
    if (altMode) elem.style.position = "relative";

    var wrapper = document.createElement("div");
    wrapper.id = elem.id + "_wrapper";
    if (!altMode) wrapper.style.position = "relative";
    wrapper.style.zIndex = messageOverZIndex++;

    if (elem.childNodes.length > 0) {
        var child = elem.childNodes[0];
        elem.insertBefore(wrapper, child)
    }
    else
        elem.appendChild(wrapper);

    var grayBox = document.createElement("div");
    grayBox.id = elem.id + "_cover";
    grayBox.className = "grayBox";
    grayBox.style.display = "inline";
    grayBox.style.zIndex = messageOverZIndex++;

    var top = "0px";
    if (elem.tagName.toLowerCase() == "fieldset" &&
        navigator.userAgent.toLowerCase().indexOf("msie") == -1)
        top = "-15px";

    grayBox.style.top = top;
    grayBox.style.left = "0px";
    grayBox.style.width = getWidth(elem) + "px";
    grayBox.style.height = getHeight(elem) + "px";
    wrapper.appendChild(grayBox);

    if (message != null) {
        var messageBox = document.createElement("div");
        messageBox.id = elem.id + "_message";
        messageBox.className = "messageOverlay Centered";
        messageBox.style.padding = "10px";
        messageBox.style.display = "inline";
        messageBox.style.zIndex = messageOverZIndex++;

        messageBox.style.padding = (showClose ? "20px" : "5px");
        messageBox.style.width = "auto";
        messageBox.innerHTML = message;

        if (showClose) {
            messageBox.innerHTML += "<div style=\"background-color:White;border:1px solid Black;padding:3px;" +
            "position:absolute;top:-10px;right:-10px;cursor:pointer;\" onclick=\"RemoveMessageOver(" +
            "document.getElementById('" + elem.id + "'));\">[X]</div>";
        }

        wrapper.appendChild(messageBox);

        messageBox.style.top = (getHeight(elem) / 2) - (getHeight(messageBox) / 2) + "px";
        messageBox.style.left = (getWidth(elem) / 2) - (getWidth(messageBox) / 2) + "px";
    }
}
function UpdateMessageOver(elem, message) {
    var messageBox = document.getElementById(elem.id + "_message");
    messageBox.innerHTML = message;

    messageBox.style.top = (getHeight(elem) / 2) - (getHeight(messageBox) / 2) + "px";
    messageBox.style.left = (getWidth(elem) / 2) - (getWidth(messageBox) / 2) + "px";
}
function RemoveMessageOver(elem) {
    var id = elem.id + "_wrapper";
    var child = document.getElementById(id);
    if (child != null)
        child.parentNode.removeChild(child);
}

//Overlays
var toggleOverZIndex = 5000;
function toggleOverlay(objId, show, notMoveable) {
    var grayBox = document.getElementById(objId + "_grayBox");
    var mode1 = "inline", mode2 = "none";
    if (show) {
        mode1 = "none";
        mode2 = "inline";

        document.getElementById(objId).style.top = "0px";
        document.getElementById(objId).style.left = "0px";

        if (grayBox == null) {
            grayBox = document.createElement("div");
            grayBox.id = objId + "_grayBox";
            grayBox.className = "grayBox";
            document.getElementById(objId).parentNode.appendChild(grayBox);
        }
        grayBox.style.display = mode2;

        grayBox.style.zIndex = toggleOverZIndex++;
        document.getElementById(objId).style.zIndex = toggleOverZIndex++;
    }

    var elem = document.getElementById(objId);
    elem.style.display = mode2;

    if (!notMoveable) {
        var header = null;
        for (var i = 0; i < elem.childNodes.length; i++) {
            var child = elem.childNodes[i];
            if (child == null) continue;

            var cssClass = child.className;
            if (cssClass == null) continue;

            if (cssClass.toLowerCase() == "messageheader") {
                header = elem.childNodes[i];
                break;
            }
        }

        if (header) {
            header.style.cursor = "move";
            header.setAttribute("unselectable", "on");
            header.setAttribute("onmousedown", "HeaderMovable_down('" + objId + "', event)");
            header.setAttribute("onmouseup", "HeaderMovable_up()");
        }
    }

    if (show) {
        ResizeOverlay(objId);
        elem.focus();
    }
    else if (grayBox != null) {
        grayBox.parentNode.removeChild(grayBox);
    }
}
function ResizeOverlay(objId) {
    var width = parseInt(document.getElementById(objId).style.width.replace("px", ""), 10);
    var height = document.getElementById(objId).clientHeight;

    if (isNaN(width))
        width = document.getElementById(objId).clientWidth;

    var scrollPosition = getCurrentScrollY();

    var top = (document.documentElement.clientHeight / 2 - height / 2) + scrollPosition;
    if (top < 0) top = 0;

    var left = document.documentElement.clientWidth / 2 - width / 2;
    if (left < 0) left = 0;

    height = document.documentElement.clientHeight;
    if (document.documentElement.scrollHeight > document.documentElement.clientHeight)
        height = document.documentElement.scrollHeight;

    document.getElementById(objId + '_grayBox').style.height = height + "px";
    document.getElementById(objId + '_grayBox').style.width = document.documentElement.clientWidth + "px";

    document.getElementById(objId).style.top = top + "px";
    document.getElementById(objId).style.left = left + "px";
}

var HeaderOffset_X = 0, HeaderOffset_Y = 0;
var HeaderOffset_Element = null;
var HeaderOffset_BeingDragged = false;
function HeaderMovable_MouseMove(event) {
    if (HeaderOffset_Element == null)
        return;

    var x, y;
    if (event.x || event.y) {
        x = event.x;
        y = event.y;
    }
    else {
        x = event.clientX;
        y = event.clientY;
    }
    if (HeaderOffset_BeingDragged == true) {
        document.getElementById(HeaderOffset_Element).style.left = (x - HeaderOffset_X) + 'px';
        document.getElementById(HeaderOffset_Element).style.top = (y - HeaderOffset_Y) + 'px';
    }
}
function HeaderMovable_down(ele_name, event) {
    var x, y;
    if (event.x || event.y) {
        x = event.x;
        y = event.y;
    }
    else {
        x = event.clientX;
        y = event.clientY;
    }

    HeaderOffset_BeingDragged = true;
    HeaderOffset_Element = ele_name;
    var elem = document.getElementById(HeaderOffset_Element);
    elem.style.cursor = "move";

    HeaderOffset_X = x - parseInt(elem.style.left.replace("px", ""), 10);
    HeaderOffset_Y = y - parseInt(elem.style.top.replace("px", ""), 10);
}
function HeaderMovable_up() {
    HeaderOffset_BeingDragged = false;
    document.getElementById(HeaderOffset_Element).style.cursor = "inherit";
    HeaderOffset_Element = null;
}


//Create/Edit/Delete functionality for generated pages
var currentController = null, currentTabId = null, currentSelectedId = null, currentBagName = null;
var currentModelId = $("#CurrentModelId").val();
var alertTimeoutId = null;

function CreateItem() {
    currentSelectedId = 0;
    InitVariables("Create", "#ModalSave", "Create");
    $.get("/Admin/" + currentController + "/QueryItem/0", ProcessInit);
}
function EditItem(id) {
    currentSelectedId = id;
    InitVariables("Edit", "#ModalSave", "Save");
    $.get("/Admin/" + currentController + "/QueryItem/" + id, ProcessInit);
}
function DeleteItem(id) {
    currentSelectedId = id;
    InitVariables("Delete", "#ModalDelete", "Delete");
    $.get("/Admin/" + currentController + "/DeleteInline/" + id, ProcessInit);
}
function InitVariables(title, btnToShow, btnText) {
    var tab = $("#" + $(".tabs .ui-tabs-active")[0].getAttribute("aria-controls"));
    currentTabId = tab[0].id;
    currentController = tab.data("controller");
    currentBagName = tab.data("bagname");

    $("#Modal .modal-title").html(title);

    $("#ModalDelete").css("display", "none");
    $("#ModalSave").css("display", "none");
    $(btnToShow).css("display", "inline-block").text(btnText);
}
function ProcessInit(response) {
    $("#" + currentTabId + "Messages").empty();
    if (alertTimeoutId) clearTimeout(alertTimeoutId);

    $("#ModalMessages").empty();
    $("#ModalPlaceholder").html(response);
    
    var cntlId = $("#CurrentModelPrimaryKey").val();
    
    $("#ModalPlaceholder #" + cntlId).val(currentModelId);
    if($("#ModalPlaceholder #" + cntlId).val() != null)
        $("#ModalPlaceholder #" + cntlId).prop('disabled', true);
    
    $("#Modal").modal('show');
    InitControls();    
    runAllForms();
}
$("#ModalSave").click(function () {
    var obj = {};
    $("#ModalForm input, #ModalForm select").each(function () {
        if (this.id != "") {
            if (this.type == "checkbox")
                obj[this.id] = $("#ModalForm [name='" + this.id + "'][type='hidden']").val();
            else
                obj[this.id] = $("#ModalForm #" + this.id).val();
        }
    });

    $.ajax({
        type: "POST",
        url: "/Admin/" + currentController + "/InsertOrUpdate",
        data: JSON.stringify(obj),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: ProcessCompleted,
        error: ProcessErrored
    });
});
$("#ModalDelete").click(function () {
    $.ajax({
        type: "POST",
        url: "/Admin/" + currentController + "/DeleteInline/" + currentSelectedId,
        data: null,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: ProcessCompleted,
        error: ProcessErrored
    });
});
function ProcessCompleted(data) {
    if (data.IsValid) {
        $.ajax({
            type: "GET",
            async: false,
            url: "/Admin/" + currentController + "/" + currentBagName + "Inline/" + currentModelId,
            success: function (data) {
                $("#" + currentTabId + "List").html(data);
                UpdateHistory();
            }
        });

        $("#Modal").modal('hide');

        CreateAlert(currentTabId + "Messages", "success", "<strong>Changes saved to the database.</strong>", true);
    }
    else {
        CreateAlert("ModalMessages", "danger", "<strong>Error:</strong><br />" + data.Message, false);
    }
}
function ProcessErrored(data) {
    alert("error");
}
function CreateAlert(cntl, mode, message, includeTimeout) {
    var div = $("<div id=\"" + cntl + "Alert\" class=\"alert alert-" + mode + " alert-dismissable\"></div>");
    div.append("<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-hidden=\"true\">&times;</button>");
    div.append(message);

    $("#" + cntl).empty();
    $("#" + cntl).append(div);

    if(includeTimeout)
        alertTimeoutId = setTimeout("$(\"#" + cntl + "Alert\").alert('close')", 5 * 1000);
}

$("#ModelForm").submit(function (e) {
    e.preventDefault();

    var tab = $("#" + $(".tabs .ui-tabs-active")[0].getAttribute("aria-controls"));
    currentTabId = tab[0].id;

    $("#" + currentTabId + "Messages").empty();
    if (alertTimeoutId) clearTimeout(alertTimeoutId);

    $.post($(this).attr('action'), $(this).serialize(), function (data) {
        //editing the main object
        $("#" + currentTabId + "Panel").empty();
        $("#" + currentTabId + "Panel").html(data);
        window.scrollTo(0, 0);

        if (typeof InitPage == 'function')
            InitPage();

        InitControls();        
        UpdateHistory();
    });
});

function UpdateHistory() {
    $(".history-list").each(function () {
        var panel = $(this);
        var obj = {
            "Model": panel.data("model"),
            "ModelId": panel.data("model-id")
        };

        $.post("/Routine/HistoryLogs", obj, function (data) {
            panel.html(data);
        });
    });
}