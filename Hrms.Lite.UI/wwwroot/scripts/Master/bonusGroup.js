function GetList() {    
    showSpinner();   
    $.ajax({
        type: 'GET',
        url: '/Master/BonusGroup/GetList',
        dataType: 'html',       
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
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

function AddNewOrEditClick(Type, GI, index) {
    if (Type == 'MODIFY') {
        if ($("#BonusGroupList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);
        else {
            $.ajax({
                type: 'GET',
                url: '/Master/BonusGroup/Edit',
                dataType: 'html',
                data: { GI: GI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#SliderView').html("");
                        $('#SliderView').html(result);
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
        $.ajax({
            type: 'GET',
            url: '/Master/BonusGroup/Create',
            dataType: 'html',            
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#SliderView').html("");
                    $('#SliderView').html(result);
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
function validateBonusGroup() {
    var i = 0;
    if (!validateById('#Name', 'BGName')) {
        i++;
    }
    if (!validateById('#ShortName', 'BGShort')) {
        i++;
    }
    if (!validateById('#SatuatoryPerc', 'BGSatuatoryPerc')) {
        i++;
    }
    //if (!validateById('#NonSatuatoryPerc', 'BGNonSatuatoryPerc')) {
    //    i++;
    //}
    //if (!validateById('#Limit', 'BGLimit')) {
    //    i++;
    //}
    //if (!validateById('#EligibilityLimit', 'BGEligibilityLimit')) {
    //    i++;
    //}
    if ($('.applCheckbox:checked').length == 0) {
        swal.fire("Please Set Atleast one Applicability");
            i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}
function Save(mode) {
    debugger;
    if (validateBonusGroup()) {
    var url;
    if (mode == 'Save')
        url = '/Master/BonusGroup/Save?Mode=CREATE'
    else
        url = '/Master/BonusGroup/Save?Mode=MODIFY'

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
                    url: url,
                    dataType: 'json',
                    data: $('#BonusGroupSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                GetList();
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

function onDeleteClick(GI, index) {
    
    if ($("#BonusGroupList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);
    else {

        $.ajax({
            type: 'GET',
            url: '/Master/BonusGroup/Delete',
            dataType: 'html',
            data: { GI: GI},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#SliderView').html("");
                    $('#SliderView').html(result);
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

function confirmDelete() {
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
                url: '/Master/BonusGroup/Delete',
                dataType: 'json',
                data: $('#deleteBonusGroup').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            GetList();
                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {});
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

function ExportToExcel() {
    var activeTableLength = $('#BonusGroupList_Count').val();
    if (activeTableLength > 0) {
        url = "/Master/BonusGroup/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }       
}