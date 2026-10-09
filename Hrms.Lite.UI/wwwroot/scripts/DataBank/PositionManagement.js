
function onLabelClick(type) {
    showSpinner();
    $.ajax({

        type: 'Post',
        url: '/DataBank/PositionManagement/GetPositionDetails?type='+type ,
        data: $('#popupFilter').serialize(),
        dataType: 'html',
        success: function (result) {
            debugger;
            $('#positionDetailList').html('');
            $('#positionDetailList').html(result);
            hideSpinner();
            $('#type').val(type);
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
function OnFilterClick() {
    var m = $('.bd-example-modal-lg');
    m.modal('show');
}
function onApplyFilter() {
    debugger;
    var Type = $('#type').val();
    onLabelClick(Type);
    var m = $('.bd-example-modal-lg');
    m.modal('hide');

}
function onResetFilter() {
    debugger;
    var Type = $('#type').val();
    onLabelClick(Type);
    $('.reset').val('');
}

function ExportToExcel() {

    debugger;
    var Type = $('#type').val();
    var tableLength = $('#PositionDetailedList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/PositionManagement/ExportToExcel?type=" + Type + "&totRecords=" + tableLength + '&data=' + $('#popupFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
function GetPositionApprovalDetails() {
    debugger;

    $.ajax({

        type: 'Get',
        url: '/DataBank/PositionManagement/GetPositionApprovalDetails',
        dataType: 'html',
        success: function (result) {
            debugger;
            $('#AddApproveList').html('');
            $('#AddApproveList').html(result);
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
function CheckIfAlreadyExist() {
    {
        $.ajax({

            type: 'POST',
            url: '/DataBank/PositionManagement/CheckIfAlreadyExist',
            dataType: 'json',
            data: $('#Positionsave').serialize(),
            success: function (result) {
                if (result.success == true) {
                    OnProceedClick();
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
}
function OnProceedClick() {
    debugger;
    var LocationCode = $('#Location_Code').val();
    var DepartmentCode = $('#Department_Code').val();
    var Designation = $('#Designation_Code').val();
    var NoofPosition = $('#NoofPosition').val();

    if (LocationCode !== '' && DepartmentCode !== '' && Designation !== '' && NoofPosition !== '') {
        var url = "/DataBank/PositionManagement/GetPositionDetailsList";

        $('#AddPositionList').load(url, { LocationCode: LocationCode, DepartmentCode: DepartmentCode, Designation: Designation, NoofPosition:NoofPosition }, function () { });

    }
}
function OnSaveClick(mode) {
    debugger;
 

    console.log($('#Positionsave').serialize());
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
                url: '/DataBank/PositionManagement/Save',
                dataType: 'json',
                data: $('#Positionsave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PositionManagement/Index";
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

function onDeleteClick(GI, Code, Type, index) {

    debugger;

     {

        $.ajax({
            type: 'POST',
            url: '/DataBank/PositionManagement/Delete',
            dataType: 'html',
            data: { PositionGI: GI, PositionCode: Code },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteSlider').html("");
                    $('#DeleteSlider').html(result);
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
function onApproveDeleteClick(GI, Code, Type, index) {

    debugger;

    {

        $.ajax({
            type: 'POST',
            url: '/DataBank/PositionManagement/Approve_Delete',
            dataType: 'html',
            data: { PositionGI: GI, PositionCode: Code },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteSlider').html("");
                    $('#DeleteSlider').html(result);
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
function confirmDelete(GI, Code) {
    debugger
    var Type = $('#type').val();
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
                url: '/DataBank/PositionManagement/DeletePosition',
                dataType: 'json',
                data: $('#deletePosition').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            debugger
                            //GetPositionDetails();
                            //var url = "/DataBank/PositionManagement/Index";
                            //window.location.href = url;
                            onLabelClick(Type);
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PositionManagement/Delete?PositionGI=" + GI + "&PositionCode=" + Code;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');
                           
                        });
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
            return false;
        }
    })
}
function confirmApproveDelete(GI, Code) {
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
                url: '/DataBank/PositionManagement/DeletePosition',
                dataType: 'json',
                data: $('#deletePosition').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            debugger
                            GetPositionDetails();
                            var url = "/DataBank/PositionManagement/Index_Approve";
                            window.location.href = url;

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PositionManagement/Delete?PositionGI=" + GI + "&PositionCode=" + Code;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
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
            return false;
        }
    })
}
function onApproveClick(GI, Code, Type, index) {

    debugger;

    {

        $.ajax({
            type: 'POST',
            url: '/DataBank/PositionManagement/GetPositionDetailsApprove',
            dataType: 'html',
            data: { PositionGI: GI, PositionCode: Code},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#ApproveSlider').html("");
                    $('#ApproveSlider').html(result);
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
function confirmApprove(GI) {
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
                url: '/DataBank/PositionManagement/Approve',
                dataType: 'json',
                data: $('#ApprovePosition').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PositionManagement/Index_Approve";
                            window.location.href = url;
                        });

                    }
                    else {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PositionManagement/GetPositionDetails?PositionGI=" + GI ;
                            $('#ApproveSlider').load(url, function () { });
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