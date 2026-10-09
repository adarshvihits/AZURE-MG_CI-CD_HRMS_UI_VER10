

function AddNewOrEditClick(mode, DocCategoryGI) {
    debugger;
    if (mode == 'Create') {

        $.ajax
            ({
                type: 'GET',
                url: '/Admin/DocumentCategory/Create',
                dataType: 'html',
                data: { Mode: mode },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#newRequestSlider').html("");
                        $('#newRequestSlider').html(result);
                    }
                },

                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);

                }
            });
    }
    if (mode == 'MODIFY') {
        $.ajax
            ({

                type: 'GET',
                url: '/Admin/DocumentCategory/Edit',
                dataType: 'html',
                data: { Mode: mode, DocCategoryGI: DocCategoryGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#newRequestSlider').html("");
                        $('#newRequestSlider').html(result);
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

function DocCategorySave(mode) {
    debugger;

    if (validateDocumentCategory()) {
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Admin/DocumentCategory/Save',
                    dataType: 'json',
                    data: $('#documentCategorySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('documentCategorySaveclose');
                                var url = "/Admin/DocumentCategory/Index?TabIndex";
                                window.location.href = url;

                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            }
            else {
                return false;
            }
        })
    }
    else {
        return false;
    }
}


function validateDocumentCategory() {
    var i = 0;
    if (!validateById('#Name', 'DocumentName')) {
        i++;
    }
    if (!validateById('#ShortName', 'DocumentShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}



function onDeleteClick(Mode,GI) {
    debugger;
        $.ajax({
            type: 'GET',
            url: '/Admin/DocumentCategory/Delete',
            dataType: 'html',
            data: { DocCategoryGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteDocCategorySlider').html("");
                    $('#deleteDocCategorySlider').html(result);
                }
            },

            error: function (xhr, textStatus, errorThrown) {
                debugger
                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }

            }
        });
}

function confirmDelete(GI) {
    debugger
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Admin/DocumentCategory/DocCategoryDelete',
                dataType: 'json',
                data: $('#deleteDocumentCategory').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteDocCategory');
                            var url = "/Admin/DocumentCategory/Index";
                            window.location.href = url;
                            
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Admin/DocumentCategory/DocCategoryDelete?DocCategoryGI=" + GI ;
                            $('#deleteDocumentCategory').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        }
        else {
            return false;
        }
    })
}


var currentTab;
var currentTab = 'ACTIVE';
var prevTab = 'ActiveTab';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $.ajax({

        type: 'GET',
        url: '/Admin/DocumentCategory/GetDocCategoryList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html("");
                $('#ActiveList').html(result);
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

function onExportToExcelClick() {
    debugger;
 
    var activeTableLength = $('#DocumentCategoryList_Count').val();
        if (activeTableLength > 0) {
            url = "/Admin/DocumentCategory/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
}

function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Admin/DocumentCategory/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                debugger;
              
            }
            else {
                swal.fire(result.message).then(function () {
                    $("#ActiveinactivedocCat").prop("checked", true);
                });
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });

}