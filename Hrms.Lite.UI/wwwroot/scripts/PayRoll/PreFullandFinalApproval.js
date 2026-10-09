function fillMonth(sender) {
    debugger;
    $.getJSON("/PayRoll/PreFullandFinalApproval/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#PayrollMonth_Code option').remove();
        $('#PayrollMonth_Code').append('<option value>--Select Month-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function getList() {
    debugger
    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'Month'))
        i++;
    if (!validateById("#PayrollGroup_Code", 'Payrollgroup'))
        i++;
    if (i == 0) {
        showSpinner();
        ApplyFilter('apply');
    }
    else
        return false;
}
function ApplyFilter(mode) {
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({
                type: 'POST',
                url: '/PayRoll/PreFullandFinalApproval/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#PreFandFApprovalList').html("");
                        $('#PreFandFApprovalList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');

                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/PayRoll/PreFullandFinalApproval/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#PreFandFApprovalList').html("");
                        $('#PreFandFApprovalList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/PayRoll/PreFullandFinalApproval/GetList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Month=" + $('#PayrollMonth_Code').val() + "&PayrollGroup=" + $('#PayrollGroup_Code').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#PreFandFApprovalList').html("");
                    $('#PreFandFApprovalList').html(result);
                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}

function onEditClick(EmployeeGI) {
    debugger;
    showSpinner();

    MonthCode = $('#PayrollMonth_Code').val();
  


    $.ajax({

        type: 'GET',
        url: '/PayRoll/PreFullandFinalApproval/PreFandFApprovalDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, Month: MonthCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay testt"></div>');
                $('#editEmployeefandf').html("");
                $('#editEmployeefandf').html(result);
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


function onApproveOrRejectClick(PreSettlementGI, mode) {

    Remarks = $('#ApproverComments').val();

    var i = 0;

    //if (!validateById('#Remarks', 'Remarks')) {
    //    i++;
    //}



    if (i == 0) {
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
                    url: '/PayRoll/PreFullandFinalApproval/PreFandFApproveOrReject',
                    dataType: 'json',
                    data: { PreSettlementGI: PreSettlementGI, Mode: mode, Remarks: Remarks },
                    success: function (result) {
                        debugger
                        if (result.success) {

                            swal.fire("Saved Successfully");
                            CloseSlider('closeEdit');


                            ApplyFilter('apply')
                        }
                        else {
                            swal.fire(result.message);
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
        });

    }
    else
        return false;
}

function ExportToExcel() {
   
    var tableLength = $('#PreFullandFinalSettlementList_Count').val();
    var month = $('#PayrollMonth_Code').val();
    var Monthname = $('#PayrollMonth_Code option:selected').text();
    var PayrollGroupCode = $('#PayrollGroup_Code').val();
    var Pname = $('#PayrollGroup_Code option:selected').text();

    var filterdata = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filterdata;

    if (tableLength > 0) {
        url = '/PayRoll/PreFullandFinalApproval/ExportToExcel?' + PARAM + '&Month=' + month + "&MName=" + Monthname +
            "&totRecords=" + tableLength + '&PGroup=' + PayrollGroupCode + '&PGroupName=' + Pname;
        window.location.href = url;
    }

    else {
        swal.fire("No data found to export");
    }
}