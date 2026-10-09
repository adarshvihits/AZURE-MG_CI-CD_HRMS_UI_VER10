


var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function OnTabClick(Tab, TabIndex) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
   /* var url = "/Master/FinancialYear/GetFinancialYearList";*/
  /*  $('#tabsDropdownFieldofStudy').val(currentTab);*/
   /* $('#FinancialYearList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });*/


    $.ajax({

        type: 'GET',
        url: '/Master/FinancialYear/GetFinancialYearList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#FinancialYearList').html("");
                $('#FinancialYearList').html(result);
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

function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#FinancialYearList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else
        {
            var url = "/Master/FinancialYear/Edit?FinancialYearGI=" + GI + "&LogGI=" + LogGI;
            window.location.href = url;
           
        }

    }

    else {
        var url = "/Master/FinancialYear/AddNew";
        window.location.href = url;
    }

}

function CheckIfAlreadyExist()
{
    if (validatefinancialYear()) {
        $.ajax({

            type: 'POST',
            url: '/Master/FinancialYear/CheckIfAlreadyExist',
            dataType: 'json',
            data: $('#FinancialYearsave').serialize(),
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
function OnProceedClick()
{
    debugger;  
        debugger
        var fromDate = $('#FromDate').val();
        var ToDate = $('#ToDate').val();
     
            if (fromDate !== '' && ToDate !== '') {
                var url = "/Master/FinancialYear/GetFinancialYearDetailsList";

                $('#AddFinancialYearList').load(url, { fromDate: fromDate, ToDate: ToDate }, function () { });

            }
        }
      
   
   

function validatefinancialYear() {
    debugger
    var i = 0;
    if (!validateById('#Name', 'NameFinancial')) {
        i++;
    }
    if (!validateById('#ShortName', 'shortFinancial')) {
        i++;
    }
    if (!validateById('#FromDate', 'from')) {
        i++;
    }
    if (!validateById('#ToDate', 'to')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}
function OnSaveClick(mode) {
    debugger;
    var fromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    console.log($('#FinancialYearsave').serialize());
    /*var data = $("#FinancialYearTable :input").serialize();*/
       /* if (validateFieldOfStudy()) {*/
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
                        url: '/Master/FinancialYear/Save',
                        dataType: 'json',
                        data: $('#FinancialYearsave').serialize(),
                       /* data:data,*/
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire(result.message).then(function () {
                                    var url = "/Master/FinancialYear/Index";
                                    window.location.href = url;

                                    //var url = "/Master/FinancialYear/GetFinancialYearDetailsList?fromDate=" + fromDate + "&ToDate=" + ToDate;
                                    //$('#AddFinancialYearList').load(url, function () { });


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

        //}
        //else {
        //    return false;
        //}
    

}

function ModifyFinanciayear(mode) {
    debugger;
    var fromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    console.log($('#FinancialYearModifY').serialize());
    /* if (validateFieldOfStudy()) {*/
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
                url: '/Master/FinancialYear/Save',
                dataType: 'json',
                data: $('#FinancialYearModifY').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/FinancialYear/Index";
                            window.location.href = url;

                            //var url = "/Master/FinancialYear/GetFinancialYearDetailsList?fromDate=" + fromDate + "&ToDate=" + ToDate;
                            //$('#AddFinancialYearList').load(url, function () { });


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

    //}
    //else {
    //    return false;
    //}


}
function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#FinancialYearList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        var url = "/Master/FinancialYear/Approve?FinancialYearGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
    }

}

function confirmApprove(GI, LogGI) {
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
                url: '/Master/FinancialYear/Approve',
                dataType: 'json',
                data: $('#FinancialYearApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                         
                            var url = "/Master/FinancialYear/Index";
                            window.location.href = url;

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/FinancialYear/Approve?FinancialYearGI=" + GI + "&LogGI=" + LogGI;
                            window.location.href = url;
                           


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

function confirmReject(GI, LogGI) {
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
                url: '/Master/FinancialYear/Reject',
                dataType: 'json',
                data: $('#FinancialYearApprove').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                          
                            var url = "/Master/FinancialYear/Index";
                            window.location.href = url;

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/FinancialYear/Approve?FinancialYearGI=" + GI + "&LogGI=" + LogGI;
                            window.location.href = url;

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

function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#FinancialYearList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        var url = "/Master/FinancialYear/Delete?FinancialYearGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
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
                url: '/Master/FinancialYear/Delete',
                dataType: 'json',
                data: $('#FinancialYearDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                         
                            debugger
                            var url = "/Master/FinancialYear/Index";
                            window.location.href = url;

                         
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/FinancialYear/Delete?FinancialYearGI=" + GI + "&LogGI=" + LogGI;
                            window.location.href = url;

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






function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#FinancialYearList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/FinancialYear/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else
    {
        var activeTableLength = $('#FinancialYearList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/FinancialYear/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    
}


function ActiveInactiveValidationFinancial(name,index, code) {
    debugger
    if (!$("#salaryPeriod_Month_" + index + "__Active").is(":checked")) {
        $.ajax({

            type: 'POST',
            url: '/Master/FinancialYear/GetActiveInactiveValidation?name=' + name + '&code=' + code,
            dataType: 'json',

            success: function (result) {
                if (result.success == false) {
                    
                    swal.fire(result.message).then(function () {

                        $("#salaryPeriod_Month_" + index + "__Active").prop("checked", true);
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
    

}

