var prevTab = "tab1";
var currentTab = 0;
var currentTabName = "Pending";

function onEditClick(LoanIssueGI, EmployeeGI) {
    debugger;
    // alert(LoanIssueGI);
    if (currentTab == 0) {
        $('#deleteEmpbtn').addClass('hide');
    }
    else
        $('#deleteEmpbtn').removeClass('hide');
    //var url = "/PayRoll/LoanConfirmation/GetLoanConfirmationDetails";
    //$('body').append('<div class="form-overlay"></div>');
    //$('#editLoanConfirmation').load(url, { EmployeeGI: EmployeeGI, LoanIssueGI: LoanIssueGI }, function () { });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/LoanConfirmation/GetLoanConfirmationDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LoanIssueGI: LoanIssueGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#editLoanConfirmation').html("");
                $('#editLoanConfirmation').html(result);
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

function onListWiseEditClick(mode,msg) {

    swal.fire({
        text: "Do you want to " + msg+"?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanConfirmation/LoanConfirmationEmployeeWiseSave',
                dataType: 'json',
                data: $('#loanConfirmationSave').serialize() + "&MonthCode=" + $('#LoanMonth_Code').val() + "&TypeCode=" + $('#LoanSettingsCode').val() + "&Mode=" + mode,
                success: function (result) {
                    debugger
                    if (result.success) {
                       
                        swal.fire(msg+"d Successfully");
                       
                        CloseSlider('closureEdit');

                        var url = "/PayRoll/LoanConfirmation/GetList";
                        $('#LoanConfirmationList').load(url, { Month: $('#LoanMonth_Code').val(), LoanType: $('#LoanType_Code').val(), type: currentTab }, function () { });
                    }
                    else {
                        swal.fire(result.message);
                    }
                },


                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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
   /* alert($('#loanConfirmationSave').serialize());*/
    
}

function tabClick(Tab, TabIndex) {
    debugger;
   
    currentTab = TabIndex;
   
    currentTabName = Tab;
    if (TabIndex == '1') {
        $('#confirmdeletebtn').addClass('btn-danger');
        $('#confirmdeletebtn').removeClass('btn-default');
        $('#confirmdeletebtn').html('Delete');
    }
    else {
        $('#confirmdeletebtn').addClass('btn-default');
        $('#confirmdeletebtn').removeClass('btn-danger');
        $('#confirmdeletebtn').html('Confirm');
    }

   
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

   // alert(currentTab);
    if (validateDropdowns()) {
        var url = "/PayRoll/LoanConfirmation/GetList";
        ApplyFilter('apply');   
        /*$('#LoanConfirmationList').load(url, { Month: $('#LoanMonth_Code').val(), LoanType: $('#LoanType_Code').val(), type: currentTab }, function () { });*/

    }
   
       

       
    
    
    //$('#LoanConfirmationList').load(url, { TabIndex: TabIndex }, function () { });
    prevTab = Tab;
}

function onFilterDropdownChange() {
    debugger;

    //alert(currentTab);
  
    Month = $('#LoanMonth_Code').val();
    LoanType = $('#LoanType_Code').val();
  

    if (validateDropdowns()) {
        showSpinner();
        //var url = "/PayRoll/LoanConfirmation/GetList";
        //$('#LoanConfirmationList').load(url, { Month: $('#LoanMonth_Code').val(), LoanType: $('#LoanType_Code').val(), type: currentTab }, function () { hideSpinner(); });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LoanConfirmation/GetList',
            dataType: 'html',
            data: { Month: Month, LoanType: LoanType, type: currentTab },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#LoanConfirmationList').html("");
                    $('#LoanConfirmationList').html(result);
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
}

function validateDropdowns() {
    if (validateById('#LoanMonth_Code', 'Month')) {
        return true;
    }
    else {
        return false;
    }
}

function AllConfirmClick() {
    debugger;
    var table = $('#LoanConfirmationlist').DataTable();
    var data = table.$('input,select,textarea').serializeArray();
    var LoanMonth = $('#LoanMonth_Code').val();
    var LoanType = $('#LoanType_Code').val();

    data.push({ name: 'LoanTypeCode', value: LoanType });
    data.push({ name: 'MonthCode', value: LoanMonth});
    data.push({ name: 'TabValues', value: currentTab });
    if (currentTab == '1')
        var msg = 'Delete';
    else
        var msg = 'Confirm';
    if (validateDropdowns()) {
        swal.fire({
            text: "Do you want to "+msg+"?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/LoanConfirmation/LoanConfirmationSave',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Confirmed Successfully").then(function () {
                                var url = "/PayRoll/LoanConfirmation/Index";
                                window.location.href = url;
                            });

                        }
                        //else if (result.message == "MAKER_CHECKER") {
                        //    swal.fire("Maker and checker cannot be same.").then(function () {


                        //    });
                        //}
                        else {
                            swal.fire("Something went wrong.");
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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

function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        showSpinner();
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanConfirmation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&month=" + $('#LoanMonth_Code').val() + "&loanType=" + $('#LoanType_Code').val() + "&type=" + currentTab,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#LoanConfirmationList').html("");
                        $('#LoanConfirmationList').html(result);

                        $('#LoanConfirmationList').removeClass('hide');
                    }
                    hideSpinner();

                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    hideSpinner();
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/LoanConfirmation/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&month=" + $('#LoanMonth_Code').val() + "&loanType=" + $('#LoanType_Code').val() + "&type=" + currentTab,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#LoanConfirmationList').html("");
                        $('#LoanConfirmationList').html(result);

                        $('#LoanConfirmationList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');               
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    hideSpinner();
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
            url: '/PayRoll/LoanConfirmation/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&month=" + $('#LoanMonth_Code').val() + "&loanType=" + $('#LoanType_Code').val() + "&type=" + currentTab,
            success: function (result) {
                debugger

                //CloseSlider('assetAllocationFilter');

                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#LoanConfirmationList').html("");
                    $('#LoanConfirmationList').html(result);

                    $('#LoanConfirmationList').removeClass('hide');
                }
                //$('#rewardsListPartial').addClass('hide');
                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                hideSpinner();
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
}

function onExportToExcelClick() {
    debugger;
    var MonthName = $('#LoanMonth_Code option:selected').text();
    var loantype = $('#LoanType_Code option:selected').text();
    if (currentTab == 1) {
        var pendingTableLength = $('#LoanConfirmationList_Count').val();
        if (pendingTableLength > 0) {
            url = "/PayRoll/LoanConfirmation/ExportToExcel?MonthCode=" + $('#LoanMonth_Code').val() + "&TypeCode=" + $('#LoanType_Code').val() + "&Tab=" + currentTab + "&TabName=" + currentTabName + "&MonName=" + MonthName + "&loantype=" + loantype;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {
        var assetAllocationTableLength = $('#LoanConfirmationList_Count').val();
        if (assetAllocationTableLength > 0) {
            url = "/PayRoll/LoanConfirmation/ExportToExcel?MonthCode=" + $('#LoanMonth_Code').val() + "&TypeCode=" + $('#LoanType_Code').val() + "&Tab=" + currentTab + "&TabName=" + currentTabName + "&MonName=" + MonthName + "&loantype=" + loantype;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}