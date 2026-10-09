var currentTab;
var currentTab = 'ACTIVE';
var prevTab = 'ActiveTab';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownRegion').val(currentTab);
    $.ajax({

        type: 'GET',
        url: '/Admin/RenewalsCategory/GetRenewalsCategoryList',
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


function onAddNewOrEditClick(Mode, RenewalsCategoryCode) {
    debugger;

    if (Mode == 'MODIFY') {

            $.ajax({

                type: 'GET',
                url: '/Admin/RenewalsCategory/Edit',
                dataType: 'html',
                data: { Mode: Mode, RenewalsCategoryCode: RenewalsCategoryCode },
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

    else {

        $.ajax({

            type: 'GET',
            url: '/Admin/RenewalsCategory/Create',
            dataType: 'html',
            data: { Mode: Mode},
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

function onRenewalsCategorySaveClick(mode) {
    debugger;

    if (validateRenewalsCategory()) {
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
                    url: '/Admin/RenewalsCategory/Save',
                    dataType: 'json',
                    data: $('#RenewalsCategorySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Admin/RenewalsCategory/GetRenewalsCategoryList?TabIndex=" + currentTab;
                                $('#ActiveList').load(url, function () { });

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



function validateRenewalsCategory() {
    var i = 0;
    if (!validateById('#Name', 'RegionName')) {
        i++;
    }
    if (!validateById('#ShortName', 'RegionShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function onDeleteClick(RenewalsCategoryCode) {
    debugger;

        $.ajax({
            type: 'GET',
            url: '/Admin/RenewalsCategory/Delete',
            dataType: 'html',
            data: { RenewalsCategoryCode: RenewalsCategoryCode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteRenCatSlider').html("");
                    $('#deleteRenCatSlider').html(result);
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

function confirmDelete(RenewalsCategoryCode) {
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
                url: '/Admin/RenewalsCategory/Delete',
                dataType: 'json',
                data: $('#deleteRenewalsCategory').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteRenewalsCategory');
                            var url = "/Admin/RenewalsCategory/GetRenewalsCategoryList?TabIndex=" + 'ACTIVE';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Admin/RenewalsCategory/Delete?RenewalsCategoryGI=" + GI + "&LogGI=" + LogGI;
                            $('#deleteRenewalsCategory').load(url, function () { });
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

function onExportToExcelClick() {
    debugger;

    var activeTableLength = $('#RenewalsCategoryList_Count').val();
        if (activeTableLength > 0) {
            url = "/Admin/RenewalsCategory/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }


}
