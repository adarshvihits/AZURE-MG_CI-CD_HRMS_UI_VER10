    


function fillData() {

    debugger;
 
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
  

 

    var MonthCode = $('#PTMonth_Code').val();
    var DeductionCode = $('#Deduction_Code').val();

   /* if (MonthCode != '' && DeductionCode != '') {*/
        var dataTable = $("#PTConfirmationListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/PTConfirmation/GetPTConfirmationList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';
                        



                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&PTMonthCode=' + MonthCode + '&DeductionCode=' + DeductionCode + '&dummy=dummy';
                        d.filter = temp;
                      
                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    debugger;

                    $('#PTConfirmationList_Count').val(d.recordsTotal);
                    $('#totEmpCnt').html(d.recordsTotal);
                    if (d.data != "") {
                        $('#empWithAmt').html(d.data[0].employeeswithAmount);
                        $('#totAmt').html(d.data[0].totalAmount);
                    }
                    if ($('#PTConfirmationList_Count').val() == '0')
                        $('.textDisplay').addClass('footerDiv');
                    else
                        $('.textDisplay').removeClass('footerDiv');

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        //d.data[i].deduction = '<a type="button" data-bs-toggle="tooltip" title="Update" class="btn btn-icon btn-update btn-cancel icon-only"><i class="icon-xl fas fa-sync-alt update-icon" onclick="EmployeeWiseTDSConfirmationSave(' + "'" + d.data[i].employeeGI + "'" + ')"></i></a> <a type="button" data-bs-toggle="tooltip" title = "Cancel"  class="btn btn-icon btn-delete icon-only btn-bg-info" onclick="onCancelClick(' + "'" + +"'" + ')"> <i class="icon-xl fas fa-times"></i></a>

                        d.data[i].amount = '<span id = "amtLabel_'+i+'">' + d.data[i].amount+'</span><span id="amtTxtbox_'+i+'" class="hide"><input type = "text" id = "newAmount_'+i+'" class="form-control form-control-sm" placeholder = "Amount" maxlength = 8 onkeypress = "return onlyNumberKey(event)"></span>'
                            

                        d.data[i].deduction = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit"  data-bs-original-title="Edit"  type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" +')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                                    
                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID","width":"10%" },
                { "data": "employeeName", "width": "22%" },
                { "data": "designation.name","width": "20%"},
                { "data": "department.name","width": "20%" },
                { "data": "location.name","width": "13%" },
                { "data": "amount","width": "8%" },

                { "data": "deduction","width": "7%" },
                /*{ "data": "ptMonth" },*/



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
            //.search('').draw();
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


















function GetList() {
    debugger;
    var PTMonthCode = $('#PTMonth_Code').val();
    var DeductionCode = $('#Deduction_Code').val();
    validateDropdowns();
    if (PTMonthCode == '' & DeductionCode == '') {
        PTMonthCode = 0;
        DeductionCode = 0;
    }
   
        showSpinner();
    //    var url = "/PayRoll/PTConfirmation/GetList";
    //$('#ptConfirmationList').load(url, { MonthCode: PTMonthCode, DeductionCode: DeductionCode }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/PayRoll/PTConfirmation/GetList',
        dataType: 'html',
        data: { MonthCode: PTMonthCode, DeductionCode: DeductionCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#ptConfirmationList').html("");
                $('#ptConfirmationList').html(result);
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

function onEditClick(i) {
    debugger
    $('#newAmount_' + i).val($('#PTConfirmationList_' + i + '__Amount').val());
    $('#amtLabel_'+i).addClass('hide');
    $('#editbt_'+i).addClass('hide');
    $('#amtTxtbox_'+i).removeClass('hide');
    $('#updateCancel_' + i).removeClass('hide');
    //moveCursorToEnd('newAmount_' + i);
}

function onUpdateClick(i, EmployeeGI) {
    debugger
    $('#PTConfirmationList_' + i + '__Amount').val($('#newAmount_' + i).val());
    $('#amtLabel_' + i).html($('#newAmount_' + i).val());
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    debugger
    $.ajax({
        type: 'POST',
        url: '/PayRoll/PTConfirmation/EmployeeWisePTConfirmationSave',
        dataType: 'json',
        data: { UpdatedAmount: $('#newAmount_' + i).val(), EmployeeGI: EmployeeGI },
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
function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#PTMonth_Code', 'PtMth')) {
        i++;
    }
    if (!validateById('#Deduction_Code', 'Dedtn')) {
        i++;
    }
    if (!validateById('.RMonth', 'RPtMth')) {
        i++;
    }
    if (!validateById('.RDeduction', 'RDedtn')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;

}
function PTConfirmationSave() {    
    
    if (validateDropdowns()) {
        var table = $('#PTConfirmationListTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray();
        swal.fire({
            text: "Do you want to Confirm?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/PayRoll/PTConfirmation/PTConfirmationSave',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Confirmed Successfully").then(function () {
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
            else {
                return false;
            }
        })
    }
}

function onCancelClick(i) {
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);
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

function ExportToExcel() {

    debugger;
    var MonthCode = $('#PTMonth_Code').val();
    var DeductionCode = $('#Deduction_Code').val();

    var tableLength = $('#PTConfirmationList_Count').val();
    if (tableLength > 0) {
        url = "/PayRoll/PTConfirmation/ExportToExcel?MonthCode=" + MonthCode + "&DeductionCode=" + DeductionCode + "&MonthName=" + $('#PTMonth_Code option:selected').text() + "&DeductionName=" + $('#Deduction_Code option:selected').text() + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

function validateDeductionDropdowns() {
    var i = 0;
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
    var MPPersonalInfo = new FormData($('#ptConfirmationUpload')[0]);
    $("#Excel_FileData").val('');
    $.ajax({

        type: 'POST',
        url: '/PayRoll/PTConfirmation/ImportFromExcel',
        contentType: false,
        processData: false,
        data: MPPersonalInfo,
        success: function (result) {
            debugger;
            if (result.success == true) {
                $("#Excel_FileData").val('');
                swal.fire("Uploaded Successfully").then(function () {
                    
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