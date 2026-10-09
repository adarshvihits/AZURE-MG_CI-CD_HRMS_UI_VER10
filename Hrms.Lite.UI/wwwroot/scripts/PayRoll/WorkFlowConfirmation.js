var prevTab = "tab1";
var currentTab = 0;
var currentTabName = "Pending";


function validateDropdowns() {
    var i = 0;
    if (!validateById('#MonthCode', 'month'))
        i++;

    if (i == 0)
        return true;
    else
        return false;

}

function Getmonth() {
    debugger;
    $.getJSON("/PayRoll/MonthlyWorkFlowConfirmation/GetMonthDropDown", { PayrollGroup: $('#PayrollGroup_Code').val() }, function (data) {

        debugger

        $('#MonthCode option').remove();

        $('#MonthCode').append('<option value="">--Select--</option');
        for (var i = 0; i < data.length; i++) {
            debugger
            $('#MonthCode').append('<option value="' + data[i].code + '">' + data[i].name + '</option');

        }

        /*   $('#PayrollMonth_Code').multiselect('rebuild')*/
    });
    /*  $('.clubDropdown').toggleClass('hide');*/
}



function getList() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var Month = $('#MonthCode').val();
    //var PayrollGP = $('#PayrollGroup_Code').val();
    var AllowanceCode = $('#Allowance_Code').val();



    var dataTable = $("#Late_Confirm_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/MonthlyWorkFlowConfirmation/ConfirmationList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tab=' + currentTabName + '&Month=' + Month  + '&AllowanceCode=' + AllowanceCode + '&dummy=dummy';

                    d = temp;
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#WorkFlowConfirmationList_Count').val(d.recordsTotal);
                $('.PendingCount').html(d.pendingCount);
                $('.ConfirmedCount').html(d.confirmedCount);

                $('#totEmpCnt').html(d.recordsTotal);
                if (d.data != "")
                {
                    $('#totAmt').html(d.totalSum);
                }
                if ($('#WorkFlowConfirmationList_Count').val() == '0')
                    $('.textDisplay').addClass('footerDiv');
                else
                    $('.textDisplay').removeClass('footerDiv');


                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    debugger;
                    //d.data[i].edit = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].ab + '' + '' + '</a>'
                    // d.data[i].edit = '<span id="editBtn_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit"  data-bs-original-title="Edit"  type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Scehedule"btn btn-icon btn-update icon-only" onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                    if (currentTabName == 'Pending')
                    {
                        d.data[i].edit = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + d.data[i].employeeCode + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + 'Edit' + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                       
                    }
                    else
                    {


                        d.data[i].edit = '<span id="edit_' + i + '"> <a type="button" data-bs-toggle="tooltip" title="Cancel" class="btn btn-icon btn-delete icon-only btn-bg-info " onclick="OnDeleteClick(' + i + ',' + d.data[i].employeeCode + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + 'DELETE' + "'" + ')"> <i class="icon-xl far fa-trash-alt"></i></a></span>'

                    }

                    d.data[i].finalAmount = '<span id="hideAmount_' + i + '">' + d.data[i].finalAmount + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8"  onkeypress = "return onlyAmount(event)" ></span>'


                }

                console.log(d.data);
                return d.data;
            }

        },




        "columns": [
          

            { "data": "employeeId", "width": "10%" },
            { "data": "employeeName", "width": "12%" },
            { "data": "department", "width": "10%" },
            { "data": "designation", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "allowance.name", "width": "10%" },
            { "data": "status", "width": "10%" },
            { "data": "approvedAmount", "width": "10%" },
            { "data": "finalAmount", "width": "10%" },
            { "data": "edit", "width": "8%" },




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








}

function onEditClick(i) {
    debugger

    $('#newAmount_' + i).val($('#hideAmount_' + i).text());
    $('#hideAmount_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');


}

function onUpdateClick(i, EmployeeCode, EmployeeGI, Mode) {
    debugger;
    var Month = $('#MonthCode').val();
    var UpdatedValue = $('#newAmount_' + i).val();
    var AllowanceCode = $('#Allowance_Code').val();
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
                url: '/PayRoll/MonthlyWorkFlowConfirmation/EditUpdate?Tab=' + currentTabName + "EmployeeCode=" + EmployeeCode + "EmployeeGI=" + EmployeeGI,
                dataType: 'json',
                data: { Amount: UpdatedValue, AllowanceCode: AllowanceCode, Month: Month,  EmployeeCode: EmployeeCode, EmployeeGI: EmployeeGI, Mode: Mode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            getList();

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

function onCancelClick(i) {
    debugger
    $('#hideAmount_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');


}



function OnConfirmClick() {

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var Month = $('#MonthCode').val();
    var AllowanceCode = $('#Allowance_Code').val();
    debugger;
    swal.fire({
        text: "Do you want to Confirm?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        debugger;

        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/MonthlyWorkFlowConfirmation/ConfirmWF?data' + temp,
                dataType: 'json',
                data: { Month: Month,  AllowanceCode: AllowanceCode },
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire("Confirmed Successfully").then(function () {

                            getList();
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
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

function tabClick(Tab, TabIndex) {
    debugger;

    currentTabName = TabIndex;

    //currentTabName = Tab;
    getList();


    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    prevTab = Tab;
    if (currentTabName == 'Pending') {
        //$('.edit_btt').removeClass('hide');   // show edit
        //$('.delete_btt').addClass('hide');
        $('#confirmlhbtn').removeClass('hide');
        $('#confirmbtn').html('Confirm');
    }
    else {
        //$('.edit_btt').addClass('hide');      // hide edit
        //$('.delete_btt').removeClass('hide');
        $('#confirmlhbtn').addClass('hide');
        $('#confirmbtn').html('Confirm');
    }


}

function OnDeleteClick(i, EmployeeCode, EmployeeGI, Mode) {
    debugger;
    var Month = $('#MonthCode').val();
    var UpdatedValue = $('#newAmount_' + i).val();
    var AllowanceCode = $('#Allowance_Code').val();
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
                url: '/PayRoll/MonthlyWorkFlowConfirmation/EditUpdate?Tab=' + currentTabName + "EmployeeCode=" + EmployeeCode + "EmployeeGI=" + EmployeeGI,
                dataType: 'json',
                data: { Amount: UpdatedValue, AllowanceCode: AllowanceCode, Month: Month,  EmployeeCode: EmployeeCode, EmployeeGI: EmployeeGI, Mode: Mode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            getList();

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

function ExportToexcel()
{
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var Month = $('#MonthCode').val();

    var MonthName = $('#MonthCode option:selected').text();
    var TabName = $('#Tab option:selected').text();
    var filter = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filter
    var AllowanceCode = $('#Allowance_Code').val();
    var Allowance = $('#Allowance_Code option:selected').text();

    if (validateDropdowns()) {

        var TableLength = $('#WorkFlowConfirmationList_Count').val();
        if (TableLength > 0) {

            url = "/PayRoll/MonthlyWorkFlowConfirmation/ExportToExcel?" + PARAM + "&Month=" + Month + "&Tab=" + currentTabName + "&totalRecords=" + TableLength + "&MonthName=" + MonthName + "&TabName=" + TabName + "&AllowanceCode=" + AllowanceCode + "&Allowance=" + Allowance;




            // url = "/Custom/LateHourConfirmation_PROFANT/ExportToExcel?Month=" + Month + "&Tab=" + currentTab + "&MonthCode=" + $('#Month_Code option:selected').text()
            //+ "&data=" + $('#BasicFilter').serialize() + "&TotalRows=" + TableLength=



            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}