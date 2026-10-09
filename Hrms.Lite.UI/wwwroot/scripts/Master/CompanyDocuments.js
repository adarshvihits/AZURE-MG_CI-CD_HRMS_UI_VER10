var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownDesignation').val(currentTab);

    $.ajax({
        type: 'GET',
        url: '/Master/CompanyDocuments/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                $('#companydocList').html(result);  
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
    prevTab = Tab;
}

function tabClick(val) {
    debugger;
    showSpinner();
    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");

    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab';
    else if (currentTab == "INACTIVE")
        prevTab = 'InActiveTab'; 
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");

    $.ajax({
        type: 'GET',
        url: '/Master/CompanyDocuments/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                $('#companydocList').html(result);  
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {
        if ($("#companydocList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);
        else {
            $.ajax({
                type: 'GET',
                url: '/Master/CompanyDocuments/Edit',
                dataType: 'html',
                data: { DocumentGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                       
                        $('#addOrEditSlider').html("");
                        $('#addOrEditSlider').html(result);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
    }
    else {
        var url = "/Master/CompanyDocuments/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/CompanyDocuments/Create',
            dataType: 'html',
            data: { DocTypeGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                 
                    $('#addOrEditSlider').html("");
                    $('#addOrEditSlider').html(result);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}





function validateCompanyDocuments() {
    var i = 0;
    if (!validateById('#Name', 'CompanyDocumentsName')) {
        i++;
    }
    if (!validateById('#ShortName', 'CompanyDocumentsShortName')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}


function onCompanyDocumentsSaveClick(mode) {
    if (!validateCompanyDocuments()) return false;

    swal.fire({
        text: "Do you want to " + mode + "?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (!result.isConfirmed) return false;

        showSpinner();

        $.ajax({
            type: 'POST',
            url: '/Master/CompanyDocuments/Save',
            dataType: 'json',
            data: $('#CompanyDocumentsSave').serialize(),
            success: function (result) {
                swal.fire(result.message).then(function () {                 
                        
                        window.location.reload();
                      
                    
                });
            },
            error: function (xhr) {
                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                } else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            }
        });
    });
}
