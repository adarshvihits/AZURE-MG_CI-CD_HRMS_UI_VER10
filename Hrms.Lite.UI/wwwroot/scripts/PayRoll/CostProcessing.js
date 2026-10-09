function onPayrollGroupChange(sender, id) {
    debugger;
    payrollGroup = $(sender).val();
    if (payrollGroup == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    debugger;
    $.getJSON("/PayRoll/CostProcessing/GetProcessingMonth", { PayrollGroupCode: payrollGroup }, function (data) {

        $('.processMnth option').remove();

        $('.processMnth').append(('<option value>--Select--</option'));


        for (var i = 0; i < data.length; i++) {
            debugger
            $('.processMnth').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}


function GetPopUp() {

    var MonthCode = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
    if (validateDropDowns()) {

        $.ajax({
            type: 'GET',
            url: "/PayRoll/CostProcessing/GetPopUp?MonthCode=" + MonthCode + "&PayrollGroup=" + PayrollGroup,
            dataType: 'html',

            success: function (Data) {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
                /*   $('#exampleModalLabel').html(state);*/
            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });


    }
    else {
        return false;
    }

}




//function onNextClick(TabName) {

//    var MonthCode = $('#Month_Code').val();
//    var PayrollGroup = $('#PayrollGroup_Code').val();

//    debugger;
//    if (validateDropDowns()) {

//        $.ajax({

//            type: 'GET',
//            url: '/PayRoll/SalaryProcessing/MonthlySalaryProcessingValidation',
//            data: {
//                SalaryPeriodCode: $("#PayrollMonth_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val()
//            },
//            success: function (result) {
//                debugger;
//                if (result.success == true) {
//                    processSalary();

//                }
//                else if (result.message == null) {

//                    var m = $('.sal_validation');
//                    m.find('.modal-body').html('');
//                    m.find('.modal-body').html(result);
//                    m.modal('show');
//                }
//                else {
//                    swal.fire(result.message);

//                }
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }

//            }
//        });

//    }

//}




function onProcessClick(TabName) {
  
        debugger
        var MonthCode = $('#Month_Code').val();
        var PayrollGroup = $('#PayrollGroup_Code').val();

    if (validateDropDowns()) {
        swal.fire({
            text: "Do you want to Continue?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {


            $.ajax({

                type: 'GET',
                url: "/PayRoll/CostProcessing/GetPopUp?MonthCode=" + MonthCode + "&PayrollGroup=" + PayrollGroup,
                data: {
                    MonthCode: MonthCode, PayrollGroup: PayrollGroup
                },
                success: function (result) {
                    debugger;
               
                    if (result.success==true) {
                        CostProcessing();

                    }
                    else if (result.message == null) {

                        var m = $('.cost_ProcPopup');
                        m.find('.modal-body').html('');
                        m.find('.modal-body').html(result);
                        m.modal('show');
                    }
                    else {
                        swal.fire('Error!');

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


    else {
        return false;
    }
}




    function CostProcessing() {
        var MonthCode = $('#Month_Code').val();
        var PayrollGroup = $('#PayrollGroup_Code').val();
        debugger;
        $.ajax({
            type: 'GET',
            url: '/PayRoll/CostProcessing/ProcessCost',
            dataType: 'json',
            data: { MonthCode: MonthCode, PayrollGroup: PayrollGroup },
            success: function (result) {
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        GetList();
                        /* --CloseSlider('batchUpdateDiv');*/
                    });

                }
                else {
                    swal.fire(result.message).then(function () {

                    });
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
function validateDropDowns() {

    var i = 0;
    if (!validateById('#PayrollGroup_Code', 'payrollGroup')) {
        i++;
    }
    if (!validateById('#Month_Code', 'payrollM')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;

}




function ExportToExcelPendingList() {
    debugger;
   /* var filter = $('#BasicFilter').serialize();*/
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var MonthCode = $('#Month_Code').val();
    var MonthName = $('#Month_Code option:selected').text()
    var PayrollName = $('#PayrollGroup_Code option:selected').text();


   /* var PARAM = 'data=' + filter;*/
    var TableLength = $('#EmployeePendingList_Count').val();

/*    url = '/PayRoll/CostProcessing/ExportToExcelPendingList?PayrollGroup=' + PayrollGroup + "&MonthCode=" + MonthCode + "&totRecords=" + TableLength;*/
   /* url = '/PayRoll/CostProcessing/ExportToExcelPendingList?' + PARAM + "&PayrollGroup=" + PayrollGroup + "&MonthCode=" + MonthCode + "&totRecords=" + TableLength;*/
    if (TableLength > 0) {
        url = '/PayRoll/CostProcessing/ExportToExcelPendingList?&PayrollGroup=' + PayrollGroup + "&MonthCode=" + MonthCode + "&MonthName=" + MonthName + "&PayrollName=" + PayrollName + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {

    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {

            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}


    //function onProcessClick() {
    //    var MonthCode = $('#Month_Code').val();
    //    var PayrollGroup = $('#PayrollGroup_Code').val();
    //    debugger;
    //    if (validateDropDowns()) {
    //        swal.fire({
    //            text: "Do you want to Continue?",
    //            icon: 'warning',
    //            showCancelButton: true,
    //            confirmButtonColor: '#3085d6',
    //            cancelButtonColor: '#d33',
    //            confirmButtonText: 'Ok'
    //        }).then((result) => {


    //            if (result.isConfirmed) {

    //                $.ajax({
    //                    type: 'GET',
    //                    url: '/PayRoll/CostProcessing/ProcessCost',
    //                    dataType: 'json',
    //                    data: { MonthCode: MonthCode, PayrollGroup: PayrollGroup},
    //                    success: function (result) {
    //                        if (result.success == true) {
    //                            swal.fire(result.message).then(function () {
    //                                GetList();
    //                                /* --CloseSlider('batchUpdateDiv');*/
    //                            });

    //                        }
    //                        else {
    //                            swal.fire(result.message).then(function () {

    //                            });
    //                        }

    //                    },
    //                    error: function (xhr, textStatus, errorThrown) {
    //                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
    //                            window.location.reload();
    //                        }

    //                        else {
    //                            document.body.id = "kt_body";
    //                            $('body').html(xhr.responseText);
    //                        }
    //                    }
    //                });


    //            }



    //            else {
    //                return false;
    //            }
    //        })
    //    }


    //    else {
    //        return false;
    //    }
    //}

