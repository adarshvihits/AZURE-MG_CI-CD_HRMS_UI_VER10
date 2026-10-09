var prevTab = 'PendingTab';
var currentTab ='APPROVAL_PENDING';
function tabClick(Tab, TabIndex) {
    debugger;
    var i = 0;
    if (!validateById("#From", 'fromdate'))
        i++;
    if (!validateById("#To", 'todate'))
        i++;
    if (i == 0)
    {      
        currentTab = TabIndex;
        $("." + prevTab).removeClass("active");
        $("." + Tab).addClass("active");
        AjaxGetList();
        if (TabIndex == "APPROVAL_PENDING")
            $(".coffcreditSaveButton").removeClass('hide');
        else
            $(".coffcreditSaveButton").addClass('hide');
        prevTab = Tab;
    }
}
function AjaxGetList()
{
    debugger
   
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/CoffCredit/GetCoffCreditList',
        dataType: 'html',
        data: { TabIndex: currentTab, Month: $("#Month_Code").val(), Fromdate: $('#From').val(), Todate: $('#To').val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#confirmationlist').html("");
                $('#confirmationlist').html(result);
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
function OnMonthChange(sender) {
  
    var Month = $(sender).val();
    $.getJSON("/TimeOffice/CoffCredit/GetFRomToDates", { Month: Month }, function (data) {
        //console.log(data);
        $("#From").val(_GetFormattedDate(data.fromDate));
        $("#To").val(_GetFormattedDate(data.toDate));
        getList();
    });
    
   
}

function getList() {
  debugger
    var i = 0;
    if (!validateById("#From", 'fromdate'))
        i++;
    if (!validateById("#To", 'todate'))
        i++;
    if (i == 0) {
        AjaxGetList();
    }
}

function OnCheckBoxAllClick(colchkid, tableid) {
    debugger
    if ($('#' + colchkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function OnConfirmClick() {
    debugger
    var TabIndex = 'APPROVAL_PENDING';
    var fromdate = $('#From').val();
    var todate = $('#To').val();
    if ($('#ConfirmationList_Count').val() != '0' && $('#ConfirmationList_Count').val() != '') {
    var table = $('#coffCreditTable').DataTable();
    var data = table.$('input,select,textarea').serializeArray();

        $.ajax({
            type: 'POST',
            url: '/TimeOffice/CoffCredit/ConfirmCoff',
            dataType: 'json',
            data: data,
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire(result.message).then(function () {


                        $.ajax({

                            type: 'GET',
                            url: '/TimeOffice/CoffCredit/GetCoffCreditList',
                            dataType: 'html',
                            data: { TabIndex: TabIndex, fromdate: fromdate, todate: todate },

                            success: function (result) {
                                debugger
                                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                    window.location.reload();
                                }
                                else {
                                    $('#confirmationlist').html("");
                                    $('#confirmationlist').html(result);
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
                    });

                }
                else {
                    swal.fire(result.message).then(function () {


                    });
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
    }
    else {
        swal.fire("No data found");
    }
   
}


function ApproveOrReject(mode) {
    debugger

    if ($('#ConfirmationList_Count').val() != '0' && $('#ConfirmationList_Count').val() != '') {
        var table = $('#coffCreditTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray();
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
                    url: '/TimeOffice/CoffCredit/ApproveOrRejectCoff?Mode=' + mode,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                tabClick('PendingTab', 'APPROVAL_PENDING');

                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {


                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        swal.fire(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        })
        
    }
    else {
        swal.fire("No data found");
    }

}



function ExportToExcel() {

    debugger;

    var Month = $('#Month_Code').val();
    var fromdate = $('#From').val();
    var todate = $('#To').val();

    var pendingTableLength = $('#ConfirmationList_Count').val();
        if (pendingTableLength > 0) {
            url = "/TimeOffice/CoffCredit/ExportToExcel?TabIndex=" + currentTab + "&Month=" + Month + "&fromdate=" + fromdate + "&todate=" + todate;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }


}



function ApplyFilter(mode) {
    debugger;
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/TimeOffice/CoffCredit/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    CloseSlider('basicFilter');

                 
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#confirmationlist').html("");
                        $('#confirmationlist').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            $.ajax({

                type: 'POST',
                url: '/TimeOffice/CoffCredit/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    CloseSlider('basicFilter');

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#confirmationlist').html("");
                        $('#confirmationlist').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/TimeOffice/CoffCredit/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {

                //CloseSlider('assetAllocationFilter');


                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#confirmationlist').html("");
                    $('#confirmationlist').html(result);
                }
                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
    }
}