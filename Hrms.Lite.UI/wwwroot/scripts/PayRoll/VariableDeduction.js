
var MonthCode;
var Month;
var DedCode;
var Deduction;


function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;




    var deduction = $('#Deduction_Code').val();
    var month = $('#DeductionMonth_Code').val();

   /* if (deduction != '' && month != '') {*/
        var dataTable = $("#VariableDeductionListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/VariableDeduction/GetDeductionList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + month + '&Deduction=' + deduction + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;
                    $('#VariableDeductionList_Count').val(d.recordsTotal);
                    $('#totEmpCnt').html(d.recordsTotal);
                    if (d.data != "") {
                        $('#empWithAmt').html(d.data[0].employeeswithAmount);                    
                        $('#totAmt').html(d.data[0].totalAmount);
                    }
                    if ($('#VariableDeductionList_Count').val() == '0')
                        $('.textDisplay').addClass('footerDiv');
                    else
                        $('.textDisplay').removeClass('footerDiv');
                    debugger;

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        if (d.data[i].amount != null)
                            d.data[i].amount = '<span id = "amtLabel_' + i + '">' + d.data[i].amount + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                        else
                            d.data[i].amount = '<span id = "amtLabel_' + i + '">' + ''+ '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'

                        d.data[i].remark = '<span id="hideRemark_' + i + '">' + d.data[i].remark + '</span ><span id="remarkTxtbox_' + i + '"class="hide"><input id="newRemark_' + i + '" class="form-control form-control-sm" placeholder="Remark" ></span>'
                        d.data[i].deduction = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                       

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID"},
                { "data": "employeeName"},
                { "data": "designation.name" },
                { "data": "department.name"},
                { "data": "location.name" },
                { "data": "remark" },
                { "data": "amount" },
                { "data": "deduction" },


              



            ],






            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',


            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,

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


    //}
    //else {
    //    return false;
    //}

}
function getList() {
    debugger;

    var Month = $('#DeductionMonth_Code').val();
    var Deduction = $('#Deduction_Code').val();
    showSpinner();
    if (Month == '' && Allowance == '') {
        Month = 0;
        Allowance = 0;
    }
    //if (Month !== '' && Deduction!== '') {
    //    var url = "/PayRoll/VariableDeduction/GetList";
    //    $('#VariableDeductionList').load(url, { Month: Month, Deduction: Deduction }, function () { hideSpinner(); });
    //    return true;
    //}
    //else {
    ///* alert('Please select Month and Allowance');*/        
    //    var url = "/PayRoll/VariableDeduction/GetList";
    //    $('#VariableDeductionList').load(url, { Month: 0, Deduction: 0 }, function () { hideSpinner(); });
    //    return false;
    //}
    if (Month !== '' && Deduction !== '') {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/VariableDeduction/GetList',
            dataType: 'html',
            data: { Month: Month, Deduction: Deduction },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#VariableDeductionList').html("");
                    $('#VariableDeductionList').html(result);
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
    hideSpinner();
}



function validateDropdowns() {
   
    var i = 0;
    if (!validateById('#DeductionMonth_Code', 'Month')) {
        i++;
    }
    if (!validateById('#Deduction_Code', 'deduction')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;


}

function onBatchUpdateClick()
{
    debugger;
    preventENterFlag = 0;
    MonthCode = $('#DeductionMonth_Code').val();
    Month = $('#DeductionMonth_Code option:selected').text();
    DedCode = $('#Deduction_Code').val();
    Deduction = $('#Deduction_Code option:selected').text();
    amount = $('#VariableDeductionBatchUpDate_Amount').val();
    
    

    if (validateDropdowns())
    {
        //$('body').append('<div class="form-overlay"></div>');
        //var url = "/PayRoll/VariableDeduction/GetBatchUpdateDetails";
   
        //$('#batchUpdateSlider').load(url,
        //    {
        //        MonthCode: MonthCode,
        //        Month: Month,
        //        DedCode: DedCode,
        //        Deduction: Deduction
        //    },
        //    function () { });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/VariableDeduction/GetBatchUpdateDetails',
            dataType: 'html',
            data: { MonthCode: MonthCode, Month: Month, DedCode: DedCode, Deduction: Deduction},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#batchUpdateSlider').html("");
                    $('#batchUpdateSlider').html(result);
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
function validateAmount() {
    debugger;
    var i = 0;
  
    if (!validateById('#VariableDeductionBatchUpDate_Amount', 'dedBatchAmt'))
    {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function BatchUpdate() {
    debugger;
    if(validateAmount())
        {

        swal.fire({
            text: "Do you want to Update?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/PayRoll/VariableDeduction/BatchUpdate',
                    dataType: 'json',
                    data: $('#VariableDeductionBatchUpdate1').serialize(),

                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                //var url = "/PayRoll/VariableDeduction/GetList";
                                //$('#VariableDeductionlist').load(url, { Month: DeductionMonth_Code, Deduction: Deduction_Code }, function () { });
                               /* ApplyFilter('reset');*/
                                fillData();
                                CloseSlider('batchupdatevariablededuction');
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
            else {
                return false;
            }
        })
    }
    else {
        return true;
    }
   

}




function onEditClick(i) {
    debugger
    $('#newAmount_' + i).val($('#amtLabel_'+i).text());
    $('#amtLabel_' + i).addClass('hide');
    $('#editbt_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#newRemark_' + i).val($('#hideRemark_' + i).text());
    $('#hideRemark_' + i).addClass('hide');
    $('#remarkTxtbox_' + i).removeClass('hide');

    $('#updateCancel_' + i).removeClass('hide');
    //moveCursorToEnd('newAmount_' + i);
}



function onUpdateClick(i, EmployeeGI) {
    debugger
    $('#VariableDeductionList_' + i + '__Amount').val($('#newAmount_' + i).val());
    $('#amtLabel_' + i).html($('#newAmount_' + i).val());
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');

    $('#VariableDeductionList_' + i + '__Remark').val($('#newRemark_' + i).val());
    $('#hideRemark_' + i).html($('#newRemark_' + i).val());
    $('#hideRemark_' + i).removeClass('hide');
    $('#remarkTxtbox_' + i).addClass('hide');
    


    var EmployeeGIValue = EmployeeGI;
    var Amt = $('#newAmount_' + i).val();
    var Remark = $('#newRemark_' + i).val();
    MonthCode = $('#DeductionMonth_Code').val();
    DedCode = $('#Deduction_Code').val();

    if (Amt == "" || Amt == null) {
        swal.fire("Amount should not be null");
        fillData();
        return false;
    }
    debugger
    $.ajax({
        type: 'POST',
        url: '/PayRoll/VariableDeduction/EmployeeWiseSave',
        dataType: 'json',
        data: "UpdatedAmount=" + Amt + "&DeductionMonth=" + MonthCode + "&DeductionType=" + DedCode + "&EmployeeGI=" + EmployeeGI + "&Remark="+Remark,
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







function onCancelClick(i) {
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);

    $('#hideRemark_' + i).removeClass('hide');
    $('#remarkTxtbox_' + i).addClass('hide');
    $('#newRemark_' + i).val(0);
}

//function ExportToExcel() {
//    debugger;
//    if (validateDropdowns()) {

//        var TableLength = $('#VariableDeductionList_Count').val();
//        if (TableLength > 0) {
//            url = "/PayRoll/VariableDeduction/ExportToExcel?Month=" + $('#DeductionMonth_Code').val() + "&Deduction=" + $('#Deduction_Code').val() + "&MName=" + $('#DeductionMonth_Code option:selected').text() + "&DName=" + $('#Deduction_Code option:selected').text();
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//}

function ExportToExcel() {

    debugger;
  
   var TableLength=$('#VariableDeductionList_Count').val();

    if (TableLength > 0) {
        url = "/PayRoll/VariableDeduction/ExportToExcel?Month=" + $('#DeductionMonth_Code').val() + "&Deduction=" + $('#Deduction_Code').val() + "&MName=" + $('#DeductionMonth_Code option:selected').text() + "&DName=" + $('#Deduction_Code option:selected').text() + "&totalRecord=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
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
        CloseSlider('basicFilter');
    }
}

function validateDeductionDropdowns()
{
    var i = 0;
    var checkbox = document.getElementById('append');
    checkbox.checked = false;

    debugger;
    if (!validateById('#DeductionMonth_Code', 'Month')) {
        i++;
    }
    if (!validateById('#Deduction_Code', 'deduction')) {
        i++;
    }
    if (i == 0) {
        //$('#Excel_FileData').trigger('click');
        var m = $('.bd-example-modal-lg');       
        m.modal('show');
    }

}


function onImportExcelClick() {


    var checkbox = document.getElementById('append');
    var checkValue = 0;
    if (checkbox.checked) checkValue = 1;
    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            var MPPersonalInfo = new FormData($('#variableDeductionUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/PayRoll/VariableDeduction/ImportFromExcel?Deduction=' + $('#Deduction_Code option:selected').text() + '&Append=' + checkValue,
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
                            $('#Unmatched').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatched').html(result)
                        }
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


function getDataSetCountAJAX() {
    debugger;
    $.ajax({
        url: '/PayRoll/VariableDeduction/CountCheckAJAX',
        type: "POST",
        async: false,
        dataType: 'json',
        data: { Month: $('#DeductionMonth_Code').val(), Deduction: $('#Deduction_Code').val(), totalRecord: $('#VariableDeductionList_Count').val() },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (result == true) {
                    debugger;
                    ExportToExcel();
                }
                else {
                    swal.fire("No Data Found");
                    return false;

                }

            }


        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });

}