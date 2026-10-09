
var Month;
var Deduction;
var isselected;
function validateDropdowns() {
    if (validateById('#TDSMonth_Code', 'Month') && validateById('#Deduction_Code', 'deduction')) {
        return true;
    }
    else {
        return false;
    }
}



function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


            var month = $('#TDSMonth_Code').val();
            var deduction = $('#Deduction_Code').val();

            /*if (month != '' && deduction != '')*/
            var dataTable = $("#TDSConfirmationlist").DataTable({
                //"paging": true,
                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/PayRoll/TDSConfirmation/GetConfirmationList",
                    "type": "POST",


                    "datatype": "json",


                    'data':

                        function (d) {
                            temp = '';



                            var searchId = $('#Filter_Code').val();
                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&Month=' + month + '&Deduction=' + deduction + '&dummy=dummy';
                            /*   d.LeavePeriodCode = LeavePeriod;*/
                            d.filter = temp;

                            // alert(d.filter);

                            return JSON.stringify(d);
                        },




                    "dataSrc": function (d) {


                        $('#TDSConfirmationList_Count').val(d.recordsTotal);
                        $('#totEmpCnt').html(d.recordsTotal);
                        if (d.data != "") {
                            $('#empWithAmt').html(d.data[0].employeeswithAmount);
                            $('#totAmt').html(d.data[0].totalAmount);
                        }
                        if ($('#TDSConfirmationList_Count').val() == '0')
                            $('.textDisplay').addClass('footerDiv');
                        else
                            $('.textDisplay').removeClass('footerDiv');

                        for (var i = 0, ien = d.data.length; i < ien; i++) {





                            d.data[i].amount = '<span id = "amtLabel_' + i + '">' + d.data[i].amount + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyNumberKey(event)"></span>'


                            d.data[i].deduction = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'

                        }

                        return d.data;
                    }

                },



                "columns": [

                    { "data": "employeeID" },
                    { "data": "employeeName" },
                    { "data": "designation.name" },
                    { "data": "department.name" },
                    { "data": "location.name" },
                    { "data": "amount" },
                    { "data": "deduction" },




                ],






                "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

                "serverSide": "true",
                "scrollY": '52vh',
                "sScrollX": "100%",
                "sScrollXInner": "100%",

                "order": [0, "asc"],
                "processing": "true",
                "language": {

                    processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
                    emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                    zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

                    sLengthMenu: "_MENU_",
                    paginate: {


                        next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                        previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                        first: '<img src="/images/icons/Icon metro-next-1.png">',
                        last: '<img src="/images/icons/Icon metro-next.png">'




                    }
                }



            });



            $('.ihits-table table').removeClass("dataTable no-footer");


            $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.next').attr("title", "Next");
            $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.previous').attr("title", "Previous");
            $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.first').attr("title", "First");
            $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
            $('.paginate_button.last').attr("title", "Last");


            $("#dataTableSearch").on("keyup search input paste cut", function () {
                dataTable.search(this.value).draw();
                $('#noDataFound').css("display", 'none');

            });

            $('.search-slider-inner').click(function () {
                dataTable.search('').draw();
                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
            });

            dataTable.on('draw', function () {
                $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
                $('.paginate_button.next').attr("title", "Next");
                $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
                $('.paginate_button.previous').attr("title", "Previous");
                $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
                $('.paginate_button.first').attr("title", "First");
                $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
                $('.paginate_button.last').attr("title", "Last");

            });

            $.fn.DataTable.ext.pager.numbers_length = 5;
            const myTimeout = setTimeout(tooltipHover, 500);
        }









function getList() {
    debugger;
    $("#edit").css("display", "block");
    $("#updatecancel").css("display", "none");
     Month = $('#TDSMonth_Code').val();
    Deduction = $('#Deduction_Code').val();
    SMonth = $('#TDSMonth_Code option:selected').text();
    SDeduction = $('#Deduction_Code option:selected').text();
    if (Month == '' && Deduction == '') {
        Month = 0;
        Allowance = 0;
    }
    else {
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
    }

    $.ajax({

        type: 'GET',
        url: '/PayRoll/TDSConfirmation/GetList',
        dataType: 'html',
        data: { Month: Month, Deduction: Deduction },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#TDSConfirmationList').html("");
                $('#TDSConfirmationList').html(result);
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

//function onEditClick(index,origAmt) {
//    debugger
//    if (isselected == null) {
//        isselected = 1;
//        $('#hideAmount_' + index).hide();

//        $('#row_' + index).append($('<input class="form-control form-control-sm"id="correctedamt" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)">'));
      
//        $('#correctedamt').val(origAmt);
//        $('#updatecancel_' + index).css("display", "block");
//        $('#edit_' + index).css("display", "none");        
//        moveCursorToEnd('correctedamt');
//    }
//}

function onEditClick(i) {
    debugger
    $('#newAmount_' + i).val($('#TDSConfirmationList' + i + '__Amount').val());
    $('#amtLabel_' + i).addClass('hide');
    $('#editbt_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#updateCancel_' + i).removeClass('hide');
    //moveCursorToEnd('newAmount_' + i);
}




function onCancelClick(i) {
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);
}






//function EmployeeWiseTDSConfirmationSave(EmployeeGI) {
//    debugger
//    var EmployeeGIValue = EmployeeGI;
//    var Amt = $("#correctedamt").val();
//    var ded = $('#Deduction_Code').val();
//    var mon = $('#TDSMonth_Code').val();
//    if (Amt == "" || Amt== null)
//    {
//        swal.fire("Amount should not be null");
//        return false;
//    }
    
//    isselected = null;
//    //var dataobject = {};
//    // dataobject.EmployeeGIValue = EmployeeGI;
//    //dataobject.Amount = $("#correctedamt").val();
//    //dataobject.Deduction = $('#Deduction_Code').val();
//    //dataobject.TDSMonth = $('#TDSMonth_Code').val();
//    $.ajax({
//        type: 'POST',
//        url: '/PayRoll/TDSConfirmation/Edit',
//        dataType: 'json',
//        data: "EmployeeGIValue=" + EmployeeGIValue + "&TDSMonth=" + mon + "&Deduction=" + ded + "&Amount=" + Amt,
//        success: function (result) {
//            if (result.success == true) {
//                swal.fire(result.message).then(function () {
//                    getList();
//                });

//            }
//            else {
//                swal.fire(result.message).then(function () {

//                });
//            }

//        },
//        error: function (xhr, textStatus, errorThrown) {
//            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }

//            else {
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        }
//    });

//}


function onUpdateClick(i, EmployeeGI) {
    debugger
    $('#PTConfirmationList_' + i + '__Amount').val($('#newAmount_' + i).val());
    $('#amtLabel_' + i).html($('#newAmount_' + i).val());
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    var EmployeeGIValue = EmployeeGI;
    var Amt = $('#newAmount_' + i).val()
    var ded = $('#Deduction_Code').val();
    var mon = $('#TDSMonth_Code').val();

    debugger
    $.ajax({
        type: 'POST',
        url: '/PayRoll/TDSConfirmation/Edit',
        dataType: 'json',
        data: "EmployeeGIValue=" + EmployeeGIValue + "&TDSMonth=" + mon + "&Deduction=" + ded + "&Amount=" + Amt,
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire("Updated Successfully").then(function () {
                    fillData();

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




function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {

        var tdsConfirmationTableLength = $('#TDSConfirmationList_Count').val();
        if (tdsConfirmationTableLength > 0) {
            url = "/PayRoll/TDSConfirmation/ExportToExcel?Month=" + $('#TDSMonth_Code').val() + "&Deduction=" + $('#Deduction_Code').val() + "&SMonth=" + $('#TDSMonth_Code option:selected').text() + "&SDeduction=" + $('#Deduction_Code option:selected').text() + "&totalRows=" + tdsConfirmationTableLength ;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}








function ApplyFilter(mode) {
    debugger

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

function validateTDSConfirmationDropdowns() {
    var i = 0;
    debugger;
    if (!validateById('#TDSMonth_Code', 'HRPlanning')) {
        i++;
    }
    if (!validateById('#Deduction_Code', 'Designation')) {
        i++;
    }
    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }

}


function onImportExcelClick() {

    //var i = 0;
    //debugger;
    //if (!validateById('#DeductionMonth_Code', 'Month')) {
    //    i++;
    //}
    //if (!validateById('#Deduction_Code', 'deduction')) {
    //    i++;
    //}
    //if (i == 0) {
    //swal.fire({
    //    text: "Do you want to Upload?",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Ok'
    //}).then((result) => {
    //    if (result.isConfirmed) {
    var MPPersonalInfo = new FormData($('#TDSConfirmationUpload')[0]);
    $("#Excel_FileData").val('');
    $.ajax({

        type: 'POST',
        url: '/PayRoll/TDSConfirmation/ImportFromExcel',
        contentType: false,
        processData: false,
        data: MPPersonalInfo,
        success: function (result) {
            debugger;
            if (result.success == true) {
                $("#Excel_FileData").val('');
                swal.fire("Uploaded Successfully").then(function () {
                    //url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
                    //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
                    //});
                    fillData()
                    var m = $('.bd-example-modal-lg');
                    m.modal('hide');
                });
            }
            else {
                $("#Excel_FileData").val('');
                if (result.message != null) {
                    $('#Unmatchedlist').html('')
                    swal.fire(result.message);
                }
                else {
                    $('#Unmatchedlist').html(result)
                }
            }
            //else {
            //    if (result.message == "Bugdeted numbers already given against the Selected Designation and Planning Period Do you want to overwrite...") {
            //        debugger
            //        $('#IsOverwrite').val("YES");
            //        swal.fire({
            //            text: result.message,
            //            icon: 'warning',
            //            showCancelButton: true,
            //            confirmButtonColor: '#3085d6',
            //            cancelButtonColor: '#d33',
            //            confirmButtonText: 'Ok'
            //        }).then((result) => {
            //            if (result.isConfirmed) {
            //                var MPPersonalInfo = new FormData($('#budgetUpload')[0]);
            //                $.ajax({

            //                    type: 'POST',
            //                    url: '/DataBank/HRPlanningBudgetUpload/ImportFromExcel',
            //                    contentType: false,
            //                    processData: false,
            //                    dataType: 'json',
            //                    data: MPPersonalInfo,
            //                    success: function (result) {
            //                        if (result.success == true) {
            //                            $("#Excel_FileData").val('');
            //                            swal.fire("Uploaded Successfully").then(function () {
            //                                url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
            //                                $('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
            //                                });
            //                                $.ajax({

            //                                    type: 'GET',
            //                                    url: '/DataBank/HRPlanningBudgetUpload/GetUploadList',
            //                                    dataType: 'html',
            //                                    data: { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() },
            //                                    success: function (result) {
            //                                        debugger
            //                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //                                            window.location.reload();
            //                                        }

            //                                        else {
            //                                            $('#uploadList').html("");
            //                                            $('#uploadList').html(result);
            //                                        }

            //                                        hideSpinner();
            //                                    },


            //                                    error: function (xhr, textStatus, errorThrown) {
            //                                        debugger
            //                                        document.body.id = "kt_body";
            //                                        $('body').html(xhr.responseText);
            //                                        hideSpinner();

            //                                    }
            //                                });

            //                            });
            //                        }
            //                        else {
            //                            swal.fire(result.message).then(function () {
            //                                $("#Excel_FileData").val('');
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
            //        swal.fire(result.message).then(function () {
            //            $("#Excel_FileData").val('');
            //        });
            //    }
            //}

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


