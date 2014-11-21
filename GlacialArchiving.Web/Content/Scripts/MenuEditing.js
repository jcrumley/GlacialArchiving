var deleteId;
var deleteTarget;
var addEditId;
var addEditTarget;
var addEditMode;

function setNavs() {
    $(".addNav").off("click");
    $(".addNav").click(function (e) {
        addEditId = $(this).data("navigationid");
        addEditTarget = "addnavitem";
        addEditMode = "add";

        $("#AddEditNavItemDIV .modal-title").html("Add New Top Level Navigation Item");

        $("#AddEditNavItemDIV #NavItemTitle").val("New Nav Item");
        $("#AddEditNavItemDIV #NavItemURL").val("");
        $("#AddEditNavItemDIV #NavItemSequence").val(1000);

        $("#AddEditNavItemDIV").modal("show");
    });

    $(".editNav").off("click");
    $(".editNav").click(function (e) {
        addEditId = $(this).data("navitemid");
        addEditTarget = "editnavitem";
        addEditMode = "edit";

        $("#AddEditNavItemDIV .modal-title").html("Edit New Top Level Navigation Item");

        $("#AddEditNavItemDIV #NavItemTitle").val($(this).data("title"));
        $("#AddEditNavItemDIV #NavItemURL").val($(this).data("url"));
        $("#AddEditNavItemDIV #NavItemSequence").val($(this).data("sequence"));

        $("#AddEditNavItemDIV").modal("show");
    });

    $(".addNavSub").off("click");
    $(".addNavSub").click(function (e) {
        addEditId = $(this).data("navitemid");
        addEditTarget = "addnavsubitem";
        addEditMode = "add";

        $("#AddEditNavItemDIV .modal-title").html("Add New Second Level Navigation Sub Item");

        $("#AddEditNavItemDIV #NavItemTitle").val("New Nav Sub Item");
        $("#AddEditNavItemDIV #NavItemURL").val("");
        $("#AddEditNavItemDIV #NavItemSequence").val(1000);

        $("#AddEditNavItemDIV").modal("show");
    });

    $(".editNavSub").off("click");
    $(".editNavSub").click(function (e) {
        addEditId = $(this).data("navsubitemid");
        addEditTarget = "editnavsubitem";
        addEditMode = "edit";

        $("#AddEditNavItemDIV .modal-title").html("Edit Second Level Navigation Sub Item");

        $("#AddEditNavItemDIV #NavItemTitle").val($(this).data("title"));
        $("#AddEditNavItemDIV #NavItemURL").val($(this).data("url"));
        $("#AddEditNavItemDIV #NavItemSequence").val($(this).data("sequence"));

        $("#AddEditNavItemDIV").modal("show");
    });

    $("#btnSaveAddEditNavItem").off("click");
    $("#btnSaveAddEditNavItem").click(function (e) {
        var data;
        if (addEditMode == "add") {
            data = {
                parentId: addEditId,
                title: $("#AddEditNavItemDIV #NavItemTitle").val(),
                url: $("#AddEditNavItemDIV #NavItemURL").val(),
                sequence: $("#AddEditNavItemDIV #NavItemSequence").val()
            };
        }
        else {
            data = {
                id: addEditId,
                title: $("#AddEditNavItemDIV #NavItemTitle").val(),
                url: $("#AddEditNavItemDIV #NavItemURL").val(),
                sequence: $("#AddEditNavItemDIV #NavItemSequence").val()
            };
        }

        modifyNavigation(addEditTarget, data);
        $("#AddEditNavItemDIV").modal("hide");
    });

    $(".deleteNav").off("click");
    $(".deleteNav").click(function (e) {
        deleteTarget = "deletenavitem";
        deleteId = $(this).data("navitemid");
        $("#DeleteConfirmationDIV .modal-title").html("Confirm Deletion of Top Level Navigation Item '" + $(this).data("title") + "'");
        $("#DeleteConfirmationDIV").modal("show");
    });
    $(".deleteNavSub").off("click");
    $(".deleteNavSub").click(function (e) {
        deleteTarget = "deletenavsubitem";
        deleteId = $(this).data("navsubitemid");
        $("#DeleteConfirmationDIV .modal-title").html("Confirm Deletion of Second Level Navigation Sub Item '" + $(this).data("title") + "'");
        $("#DeleteConfirmationDIV").modal("show");
    });

    $("#DeleteConfirmationDIV .btn-primary").off("click");
    $("#DeleteConfirmationDIV .btn-primary").click(function (e) {
        var data = {
            id: deleteId
        };
        modifyNavigation(deleteTarget, data);
        $("#DeleteConfirmationDIV").modal("hide");
    });
}

function modifyNavigation(target, data) {
    var url = "/core/configuration/" + target

    //var echo = {
    //    target: target,
    //    json: JSON.stringify(data),
    //    url: url
    //}
    //alert(JSON.stringify(echo));

    $.ajax({
        type: "POST",
        url: url,
        data: data,
        //async: false,
        success: function (msg) {
            loadNavigation();
            $('nav ul').jarvismenu({
                accordion: true,
                speed: $.menu_speed,
                closedSign: '<em class="fa fa-expand-o"></em>',
                openedSign: '<em class="fa fa-collapse-o"></em>'
            });
        }
    });
}