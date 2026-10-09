var currentTab = 'ACTIVE';
var prevTab = 'ActiveTab';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/Reader/GetList";
    //$('#tabsDropdownRegion').val(currentTab);
    //$('#ReaderList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/Reader/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ReaderList').html("");
                $('#ReaderList').html(result);
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
function validateReader() {
    var i = 0;
    if (!validateById('#UserCode', 'UsrCod')) {
        i++;
    }
    if (!validateById('#ReaderName', 'ReadName')) {
        i++;
    }
    if (!validateById('#IPAddress', 'IP')) {
        i++;
    }
    if (!validateById('#Port', 'portNo')) {
        i++;
    }
    if (!validateById('#ReaderType_Code', 'ReadTpe')) {
        i++;
    }
    if (!validateById('#Location_Code', 'Loc')) {
        i++;
    }
    //if (!validateById('#BiometricIDInAttendanceDB', 'Biometric')) {
    //    i++;
    //}
    
    if (i == 0)
        return true;
    else
        return false;
}


function Save(mode) {
    debugger
    if(validateReader())
    {
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
                    url: '/Master/Reader/Create',
                    dataType: 'json',
                    data: $('#readerSave').serialize(),

                    success: function (result) {
                        debugger;
                        if (result.success == true) {

                            swal.fire(result.message).then(function () {
                                CloseSlider('readerSaveSlider');
                                var url = "/Master/Reader/GetList";
                                $('#ReaderList').load(url, { TabIndex: currentTab }, function () { });
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
    else
    {
        return false;
    }
    
}

function Edit(mode) {
    if (validateReader()) {
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
                    url: '/Master/Reader/Edit',
                    dataType: 'json',
                    data: $('#readerEdit').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {

                            swal.fire(result.message).then(function () {
                                CloseSlider('editSlider');
                                var url = "/Master/Reader/GetList";
                                $('#ReaderList').load(url, { TabIndex: currentTab }, function () { });
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
   
   
}

function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#ReaderList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        debugger;
        //var url = "/Master/Reader/Approve?ReaderGI=" + GI + "&LogGI=" + LogGI;
        //$('#SliderContent').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/Master/Reader/Approve',
            dataType: 'html',
            data: { ReaderGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#SliderContent').html("");
                    $('#SliderContent').html(result);
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

}


function onCreateOrEditClick(Type, GI, LogGI, index) {
    debugger;

    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#ReaderList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            //var url = "/Master/Reader/Edit?ReaderGI=" + GI + "&LogGI=" + LogGI;

            //$('#CreateSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');



            $.ajax({

                type: 'GET',
                url: '/Master/Reader/Edit',
                dataType: 'html',
                data: { ReaderGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#CreateSlider').html("");
                        $('#CreateSlider').html(result);
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
        //var url = "/Master/Reader/Create";
        //$('#CreateSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');



        $.ajax({

            type: 'GET',
            url: '/Master/Reader/Create',
            dataType: 'html',
            data: { ReaderGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#CreateSlider').html("");
                    $('#CreateSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }





    //currentTab = 'ACTIVE';

    //if (Type == 'MODIFY') {
    //    if ($("#ReaderList_" + index + "__EditActive").val() == 'True')
    //        swal.fire(ApprovalPendingMsg);


    //    else {
    //        var url = "/Master/Reader/Edit?ReaderGI=" + GI + "&LogGI=" + LogGI;

    //        $('#SliderContent').load(url, function () { });
    //        $('body').append('<div class="form-overlay"></div>');
    //    }

    //}

    //else {
    //    var url = "/Master/Region/Create";
    //    $('#SliderContent').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}


}

function onDeleteClick(Type,GI, LogGI,  index) {

    debugger;

    if ($("#ReaderList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        //var url = "/Master/Reader/Delete?ReaderGI=" + GI + "&LogGI=" + LogGI;
        //$('#SliderContent').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({
            type: 'GET',
            url: '/Master/Reader/Delete',
            dataType: 'html',
            data: { ReaderGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#SliderContent').html("");
                    $('#SliderContent').html(result);
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
}

function confirmApprove() {
    debugger
    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/Reader/Approve',
                dataType: 'json',
                data: $('#readerApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approveSlider');
                            var url = "/Master/Reader/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ReaderList').load(url, function () { });                            
                        });

                    }
                    else {
                        debugger;
                        if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.")
                        }
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }
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



function confirmReject() {
    debugger
    swal.fire({
        text: "Do you want to Reject?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {


            $.ajax({

                type: 'POST',
                url: '/Master/Reader/Reject',
                dataType: 'json',
                data: $('#readerApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approveSlider');
                            var url = "/Master/Reader/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ReaderList').load(url, function () { });
                            
                        });

                    }
                    else {
                        debugger;
                        if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.")
                        }
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }
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

function confirmDelete(GI, LogGI) {
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
                url: '/Master/Reader/Delete',
                dataType: 'json',
                data: $('#readerDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deleteSlider');
                            var url = "/Master/Reader/GetList?TabIndex=" + currentTab;
                            $('#ReaderList').load(url, function () { });
                            
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Reader/Delete?ReaderGI=" + GI + "&LogGI=" + LogGI;
                            $('#SliderContent').load(url, function () { });
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
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#ReaderList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Reader/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#ReaderList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Reader/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

}


function ActiveInactiveValidationReader(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Reader/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#reader").prop("checked", true);
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