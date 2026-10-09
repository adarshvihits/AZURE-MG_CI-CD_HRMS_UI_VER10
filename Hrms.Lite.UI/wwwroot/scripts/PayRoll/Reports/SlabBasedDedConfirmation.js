var prevTab = "tab1";
var currentTab = 0;
var currentTabName = "Pending";

function tabClick(Tab, TabIndex) {
    debugger;

    currentTab = TabIndex;

    currentTabName = Tab;


    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    if (validateDropdowns()) {

        GetList();    }


    prevTab = Tab;
}





function GetList() {
    debugger;



    var MonthCode = $('#Month').val();
    var DeductionCode = $('#DeductionCode').val();
  

    if (currentTab == 1)
        $('#ConfirmBtn').addClass('hide');
    else
        $('#ConfirmBtn').removeClass('hide');

    showSpinner();

    $.ajax({

        type: 'GET',
        url: '/PayRoll/SlabBasedDeductionConfirmtion/GetList',
        dataType: 'html',
        data: { MonthCode: MonthCode, DeductionCode: DeductionCode, Confirmstatus: currentTab },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#ConfirmationList').html("");
                $('#ConfirmationList').html(result);
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


function validateDropdowns() {
    if (validateById('#Month', 'month')) {
        return true;
    }
    if (validateById('#DeductionCode', 'deduction')) {
        return true;
    }
    else {
        return false;
    }
}

//function fillData() {

//    debugger;

//    var filter1 = $('#BasicFilter').serialize();

//    var temp = filter1;




//    var MonthCode = $('#Month').val();
//    var DeductionCode = $('#DeductionCode').val();

//    /*if (MonthCode != '' && AllowanceCode != '') {*/
//    var dataTable = $("#SlabBasedConfirmation").DataTable({

//        "pagingType": 'full_numbers',
//        "destroy": 'true',
//        "ajax": {
//            "url": "/PayRoll/SlabBasedDeductionConfirmtion/GetConfirmationList",
//            "type": "POST",
//            "datatype": "json",
//            'data':

//                function (d) {
//                    temp = '';
//                    var start = d.start;

//                    var searchValue = $('#dataTableSearch').val();

//                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&DeductionCode=' + DeductionCode + '&dummy=dummy';
//                    d.filter = temp;

//                    return JSON.stringify(d);
//                },




//            "dataSrc": function (d) {


//                debugger;

//                $('#SlabDeductionConfirmationList_Count').val(d.recordsTotal);
//                //$('#totEmpCnt').html(d.recordsTotal);
//                //if (d.data != "") {
//                //    $('#empWithAmt').html(d.data[0].employeeswithAmount);
//                //    $('#totAmt').html(d.data[0].totalAmount);
//                //}
//                if ($('#SlabDeductionConfirmationList_Count').val() == '0')
//                    $('.textDisplay').addClass('footerDiv');
//                else
//                    $('.textDisplay').removeClass('footerDiv');

//                for (var i = 0, ien = d.data.length; i < ien; i++) {

//                    d.data[i].employeeCode = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeCode + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
//                    if (d.data[i].amount != null)
//                        d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyAmount(event)" oninput = "process(this)"></span>'

//                    else
//                        d.data[i].amount = '<span id="hideAmount_' + i + '">' + '' + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyAmount(event)" oninput = "process(this)"></span>'

//                    d.data[i].remarks = '<span id="remarkBox_' + i + '">' + '' + '</span ><span id="remarkTxtbox_' + i + '"class="hide"><input id="newRemark_' + i + '" class="form-control form-control-sm" placeholder="Remarks" onpaste="return false"  oninput = "process(this)"></span>'


//                }

//                return d.data;
//            }

//        },

//        "columns": [

//            { "data": "employeeID" },
//            { "data": "employeeName" },
//            { "data": "designation" },
//            { "data": "department" },
//            { "data": "location" },
//            { "data": "grossPay" },
//            { "data": "amount" },
//            { "data": "remarks" },
//            { "data": "employeeCode" },      


//        ],


//        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
//        "serverSide": "true",
//        "scrollY": '52vh',
//        "sScrollX": "100%",
//        "sScrollXInner": "100%",
//        "autoWidth": false,

//        "order": [0, "asc"],
//        "processing": "true",
//        "language": {
//            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',


//            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//            sLengthMenu: "_MENU_",
//            paginate: {


//                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                first: '<img src="/images/icons/Icon metro-next-1.png">',
//                last: '<img src="/images/icons/Icon metro-next.png">'




//            }
//        }



//    });



//    $('.ihits-table table').removeClass("dataTable no-footer");


//    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.next').attr("title", "Next");
//    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.previous').attr("title", "Previous");
//    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.first').attr("title", "First");
//    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.last').attr("title", "Last");


//    $("#dataTableSearch").on("keyup search input paste cut", function () {
//        dataTable.search(this.value).draw();
//        $('#noDataFound').css("display", 'none');

//    });

//    $('.search-slider-inner').click(function () {
//        //dataTable.search('').draw();
//        $('#dataTableSearch').val('');
//        $('#dataTableSearch').focus();
//    });

//    dataTable.on('draw', function () {
//        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.next').attr("title", "Next");
//        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.previous').attr("title", "Previous");
//        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.first').attr("title", "First");
//        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.last').attr("title", "Last");

//    });

//    $.fn.DataTable.ext.pager.numbers_length = 5;


//    const myTimeout = setTimeout(tooltipHover, 500);


//    //}
//    //else {
//    //    return false;
//    //}

//}




function onEditClick(i) {
    debugger 



    $('.amountNew_' + i).val($('#SlabDeductionConfirmationList_' + i + '__Amount').val());
    $('.remarkNew_' + i).val($('#SlabDeductionConfirmationList_' + i + '__Remarks').val());
    $('#hideAmount_' + i).addClass('hide');
    $('#remarkBox_' + i).addClass('hide');
    $('#AmountTxtbox_' + i).removeClass('hide');
    $('#edit_' + i).addClass('hide');

    $('#remarkTxtbox_' + i).removeClass('hide');
    $('#newRemark_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');

    $('#amtTxtbox_' + i).removeClass('hide');
}





function onCancelClick(i) {
    debugger;
    $('#hideAmount_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);
    $('#newRemark_' + i).addClass('hide');
    $('#remarkBox_' + i).removeClass('hide');
    $('#AmountTxtbox_' + i).addClass('hide');

    $('#remarkTxtbox_' + i).addClass('hide');

    $('#hideAmount_' + i).removeClass('hide');





}




function AllConfirmClick() {
    debugger;
    var table = $('#SlabBasedConfirmation').DataTable();
    console.log(table);
    var data = table.$('input,select,textarea').serializeArray();
    var Month = $('#Month').val();
    var DeductionCode = $('#DeductionCode').val();

    data.push({ name: 'DeductionCode', value: DeductionCode });
    data.push({ name: 'Month', value: Month });
    console.log(data);

    swal.fire({
        text: "Do you want to Confirm",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SlabBasedDeductionConfirmtion/ConfirmAll',
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Confirmed Successfully").then(function () {
                            GetList()
                        });

                    }

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




function onUpdateClick(i, EmployeeCode) {
    debugger
    // $('#SlabDeductionConfirmationList_' + i + '__Amount').val($('#newAmount_' + i).val());


    $('#hideAmount_' + i).html($('#newAmount_' + i).val());

    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#hideAmount_' + i).addClass('hide');
    $('#remarkBox_' + i).addClass('hide');
    var Amt = $('.amountNew_' + i).val()

    var Remarks = $('.remarkNew_' + i).val()


    MonthCode = $('#Month').val();
    DedCode = $('#DeductionCode').val();

    if (Amt == "" || Amt == null) {
        swal.fire("Amount should not be null");
        fillData();
        return false;
    }
    debugger
    $.ajax({
        type: 'POST',
        url: '/PayRoll/SlabBasedDeductionConfirmtion/EmployeeWiseSave',
        dataType: 'json',
        data: "UpdatedAmount=" + Amt + "&MonthCode=" + MonthCode + "&DeductionCode=" + DedCode + "&EmployeeCode=" + EmployeeCode + "&Remarks=" + Remarks,
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire("Updated Successfully");
                debugger;
                GetList()



            }
            else {
                swal.fire(result.message).then(function () {
                    GetList()
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





function ApplyFilter(mode) {
    debugger
    var MonthCode = $('#Month').val();
    var DeductionCode = $('#DeductionCode').val();
    var ConfirmStatus = $("input[type='radio']:checked").val();
    if (mode == 'apply') {
        showSpinner();
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SlabBasedDeductionConfirmtion/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + MonthCode + "&DeductionCode=" + DeductionCode + "&ConfirmStatus=" + ConfirmStatus,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#ConfirmationList').html("");
                        $('#ConfirmationList').html(result);

                        $('#ConfirmationList').removeClass('hide');
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
                url: '/PayRoll/SlabBasedDeductionConfirmtion/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Month=" + MonthCode + "&DeductionCode=" + DeductionCode + "&ConfirmStatus=" + ConfirmStatus,
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#ConfirmationList').html("");
                        $('#ConfirmationList').html(result);

                        $('#ConfirmationList').removeClass('hide');
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
            url: '/PayRoll/SlabBasedDeductionConfirmtion/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Month=" + MonthCode + "&DeductionCode=" + DeductionCode + "&ConfirmStatus=" + ConfirmStatus,
            success: function (result) {
                debugger

                //CloseSlider('assetAllocationFilter');

                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#ConfirmationList').html("");
                    $('#ConfirmationList').html(result);

                    $('#ConfirmationList').removeClass('hide');
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





function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();


    var MonthCode = $('#Month').val();
    var DedCode = $('#DeductionCode').val();
    var Monthname = $('#Month option:selected').text();
    var DeductionName = $('#DeductionCode option:selected').text();
    var ConfirmStatus = $("input[type='radio']:checked").val();

    if (currentTab == 0)
        var StatusName = "Not Confirmed"
    else
        var StatusName = "Confirmed"

    var PARAM = 'data=' + filterdata;
    var TableLength = $('#SlabDeductionConfirmationList_Count').val();
    if (TableLength > 0) {


        url = '/PayRoll/SlabBasedDeductionConfirmtion/ExportToExcel?' + PARAM + "&MonthCode=" + MonthCode + "&DeductionCode=" + DedCode + "&MonthName=" + Monthname + "&DeductionName=" + DeductionName + "&ConfirmStatus=" + currentTab + "&TabIndex=" + StatusName;

        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}