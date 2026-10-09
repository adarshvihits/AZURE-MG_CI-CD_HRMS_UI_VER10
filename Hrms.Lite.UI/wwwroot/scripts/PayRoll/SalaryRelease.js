
function fillMonth(sender) {
    $('#Month').empty();
    debugger;
    $.getJSON("/PayRoll/SalaryRelease/fillMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month option').remove();
        $('#Month').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
    });
}


function fillData() {

        debugger;

        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var Month = $('#Month').val();
        var PayrollGroupCode = $('#PayrollGroup').val();
        var dataTable = $("#SalaryReleaseTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/SalaryRelease/GetList",
                "type": "POST",
                "datatype": "json",

                "data": function (d) {
                    var searchValue = $('#dataTableSearch').val();

                    // Add custom parameters to the data object

                    d.Month = Month;
                    d.PayrollGroupCode = PayrollGroupCode;
                    d.searchValue = searchValue;

                    // Parse filter1 into key-value pairs and add them to d
                    filter1.split('&').forEach(function (pair) {
                        var parts = pair.split('=');
                        var key = decodeURIComponent(parts[0]);
                        var val = decodeURIComponent(parts[1] || '');
                        d[key] = val;
                    });
                    return d; // Will be sent as form data (x-www-form-urlencoded)
                },



                "dataSrc": function (d) {
                    debugger;
                    $('#SalaryReleaseList_Count').val(d.recordsTotal);
                    $("#totAmt").html(d.totalAmount);

                    if (d.isLocked == 1) {
                        $('#Excel').attr("disabled", true);
                    }
                    else {
                        $('#Excel').attr("disabled", false);
                    }



                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        if (d.data[i].payable != null)
                            d.data[i].payable = '<span id = "payLabel_' + i + '">' + d.data[i].payable + '</span><span id="payTxtbox_' + i + '" class="hide"><input type = "text" id = "newpay_' + i + '" class="form-control form-control-sm" placeholder = "Payable" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                        else
                            d.data[i].payable = '<span id = "payLabel_' + i + '">' + '' + '</span><span id="payTxtbox_' + i + '" class="hide"><input type = "text" id = "newpay_' + i + '" class="form-control form-control-sm" placeholder = "Payable" maxlength = 10 onkeypress = "return onlyAmount(event)"></span>'

                        if (d.data[i].recovery != null)
                            d.data[i].recovery = '<span id = "recLabel_' + i + '">' + d.data[i].recovery + '</span><span id="recTxtbox_' + i + '" class="hide"><input type = "text" id = "newrec_' + i + '" class="form-control form-control-sm" placeholder = "Recovery" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                        else
                            d.data[i].recovery = '<span id = "recLabel_' + i + '">' + '' + '</span><span id="recTxtbox_' + i + '" class="hide"><input type = "text" id = "newrec_' + i + '" class="form-control form-control-sm" placeholder = "Recovery" maxlength = 10 onkeypress = "return onlyAmount(event)"></span>'


                        if (d.data[i].remarks != null)
                            d.data[i].remarks = '<span id = "hrLabel_' + i + '">' + d.data[i].remarks + '</span><span id="hrTxtbox_' + i + '" class="hide"><input type = "text" id = "newhr_' + i + '" class="form-control form-control-sm" placeholder = "Remarks" ></span>'
                        else
                            d.data[i].remarks = '<span id = "hrLabel_' + i + '">' + '' + '</span><span id="hrTxtbox_' + i + '" class="hide"><input type = "text" id = "newhr_' + i + '" class="form-control form-control-sm" placeholder = "Remarks"></span>'


                        d.data[i].action = '<div style="display:flex; justify-content:center;"><span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span></div>'
                      
                        d.data[i].employeeHeader.employeeName = d.data[i].employeeHeader.employeeName + '<input type="hidden" id="SalaryReleaseList_' + i + '__holdAmount' + '" name="SalaryReleaseList[' + i + '].holdAmount' + '" value="' + d.data[i].holdAmount + '"><input type="hidden" id="SalaryReleaseList_' + i + '__balance' + '" name="SalaryReleaseList[' + i + '].balance' + '" value="' + d.data[i].balance + '">'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeHeader.employeeId" },
                { "data": "employeeHeader.employeeName" },
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "doj" },
                { "data": "mode" },
                { "data": "accNo" },
                { "data": "holdAmount" },
                { "data": "balance" },
                { "data": "payable" },
                { "data": "recovery" },
                { "data": "remarks" },
                { "data": "action" },

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



}
function onEditClick(i) {
    debugger
    $('#newpay_' + i).val($('#payLabel_' + i).text());
    $('#payLabel_' + i).addClass('hide');
    $('#payTxtbox_' + i).removeClass('hide');

    $('#newrec_' + i).val($('#recLabel_' + i).text());
    $('#recLabel_' + i).addClass('hide');
    $('#recTxtbox_' + i).removeClass('hide');

    $('#newhr_' + i).val($('#hrLabel_' + i).text());
    $('#hrLabel_' + i).addClass('hide');
    $('#hrTxtbox_' + i).removeClass('hide');

    $('#editbt_' + i).addClass('hide');
    $('#updateCancel_' + i).removeClass('hide');
    //moveCursorToEnd('newAmount_' + i);
}
function onUpdateClick(i, EmployeeGI) {
    debugger

    var release = 0;
    $('#SalaryReleaseList_' + i + '__payable').val($('#newpay_' + i).val());
    $('#payLabel_' + i).html($('#newpay_' + i).val());
    $('#payLabel_' + i).removeClass('hide');
    $('#payTxtbox_' + i).addClass('hide');

    $('#SalaryReleaseList_' + i + '__recovery').val($('#newrec_' + i).val());
    $('#recLabel_' + i).html($('#newrec_' + i).val());
    $('#recLabel_' + i).removeClass('hide');
    $('#recTxtbox_' + i).addClass('hide');

    $('#SalaryReleaseList_' + i + '__remarks').val($('#newhr_' + i).val());
    $('#hrLabel_' + i).html($('#newhr_' + i).val());
    $('#hrLabel_' + i).removeClass('hide');
    $('#hrTxtbox_' + i).addClass('hide');


    $('#editbt_' + i).removeClass('hide');
    $('#updateCancel_' + i).addClass('hide');


    var Remarks = $('#newhr_' + i).val();
    var Month = $('#Month').val();
    var PayrollGroupCode = $('#PayrollGroup').val();
    var payable = $('#newpay_' + i).val();
    var Recovery = $('#newrec_' + i).val();
    var Balance = $('#SalaryReleaseList_' + i + '__balance').val();
    var HoldAmount = $('#SalaryReleaseList_' + i + '__holdAmount').val();

    if (Recovery > 0) {
        if (HoldAmount == payable) {
            if (Balance - Recovery < 0) {
                $('#newpay_' + i).val(0);
                release = 0;
                return false;
            }
            else {

                $('#newpay_' + i).val(Balance - Recovery);
                release = Balance - Recovery;
            }

        }

    }

    if (Balance < (release + Recovery)) {
        $('#newpay_' + i).val(Balance);
        $('#newrec_' + i).val(0);
        swal.fire("Please check balance amount.");
        return false;
    }
 

    if (payable == "" || payable == null) {
        swal.fire("Payable should not be null");
        return false;
    }
    if (Remarks == "" ) {
        swal.fire("Enter Remarks!!");
        return false;
    }
    if (HoldAmount <  payable) {
        swal.fire("Release Amount should be less than or equal to current Hold Amount...");
        return false;
    }
    debugger

        $.ajax({
            type: 'POST',
            url: '/PayRoll/SalaryRelease/Save',
            dataType: 'json',
            data: "payable=" + $('#newpay_' + i).val() + "&Month=" + Month + "&PayrollGroupCode=" + PayrollGroupCode + "&EmployeeGI=" + EmployeeGI + "&Recovery=" + $('#newrec_' + i).val() + "&Remarks=" + Remarks + "&HoldAmount=" + HoldAmount,
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
function validateFields(row) {
    debugger;

        var release = 0;
    var newpayable = $('#newpay_' + row).val();
    var HoldAmount = $('#SalaryReleaseList_' + row + '__holdAmount').val();
    var Recovery = $('#newrec_' + row).val();
    var Balance = $('#SalaryReleaseList_' + row + '__balance').val();
    var i = 0;

    if (newpayable == "" || newpayable == null) {
            swal.fire("Payable should not be null");
            i = 1;
        }
        if (Recovery > 0) {
            if (HoldAmount == newpayable) {
                if (Balance - Recovery < 0) {
                    $('#newpay_' + row).val(0);
                    release = 0;
                }
                else {

                    $('#newpay_' + row).val(Balance - Recovery);
                    release = Balance - Recovery;
                }

            }

        }
    if (Balance < (release + Recovery)) {
            $('#newpay_' + row).val(Balance );
            $('#newrec_' + row).val(0);
           
            swal.fire("Please check balance amount");
            i = 1;
        }
        if (i == 0)
            return true;
        else
            fillData();
            return false;
          

    }
function onCancelClick(i) {
    $('#payLabel_' + i).removeClass('hide');
    $('#payTxtbox_' + i).addClass('hide');
    $('#newpay_' + i).val(0);

    $('#recLabel_' + i).removeClass('hide');
    $('#recTxtbox_' + i).addClass('hide');
    $('#newrec_' + i).val(0);

    $('#hrLabel_' + i).removeClass('hide');
    $('#hrTxtbox_' + i).addClass('hide');
    $('#hrrec_' + i).val('');


    $('#editbt_' + i).removeClass('hide');
    $('#updateCancel_' + i).addClass('hide');
   
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

    var tableLength = $('#SalaryReleaseList_Count').val();
    var month = $('#Month').val();
    var Monthname = $('#Month option:selected').text();
    var PayrollGroupCode = $('#PayrollGroup').val();
    var Pname = $('#PayrollGroup option:selected').text();

    var filterdata = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filterdata;

    if (tableLength > 0) {
        url = '/PayRoll/SalaryRelease/ExportToExcel?' + PARAM + '&Month=' + month + "&MName=" + Monthname +
            "&totRecords=" + tableLength + '&PGroup=' + PayrollGroupCode + '&Pname=' + Pname;
        window.location.href = url;
    }

    else {
        swal.fire("No data found to export");
    }
}
