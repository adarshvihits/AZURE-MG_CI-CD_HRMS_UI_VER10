var currentTab;
var currentTab = 0;
var prevTab = 'PendingTab';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    if (currentTab == 0) {
        $('#Certifi_Pending_List').removeClass('hide');
        $('#Certifi_Confirm_List').addClass('hide');

    }
    else {
        $('#Certifi_Confirm_List').removeClass('hide');
        $('#Certifi_Pending_List').addClass('hide');

    }
    fillData();
    prevTab = Tab;
}


function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var MonthCode = $('#Month_Code').val();
    hideSpinner();
   
    if (currentTab == 0) {
        var dataTable = $("#Certi_pendingTable").DataTable({

            //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "pagingType": 'full_numbers',
            "destroy": true,
            "ajax": {
                "url": "/PayRoll/CertificationReimbursement/GetCertificationReimbursementList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&TabIndex=' + currentTab + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {
                    debugger;

                    $('#CertificationReimbursementList_Count').val(d.recordsTotal);
                    $('#totEmpCnt').html(d.recordsTotal);
                    if (d.data != "") {
                        $('#empWithAmt').html(d.data[0].employeeswithAmount);
                        $('#totAmt').html(d.data[0].totalAmount);
                    }
                    if ($('#CertificationReimbursementList_Count').val() == '0')
                        $('.textDisplay').addClass('footerDiv');
                    else
                        $('.textDisplay').removeClass('footerDiv');
                    debugger;

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].allowance = '<input type="hidden" id="CertificationReimbursementList_' + i + '__EmployeeCode" name ="CertificationReimbursementList[' + i + '].EmployeeCode" value="' + d.data[i].employeeCode + '"/>' +
                            '<input type="hidden" id="CertificationReimbursementList_' + i + '__EmployeeGI" name ="CertificationReimbursementList[' + i + '].EmployeeGI" value="' + d.data[i].employeeGI + '"/>' +
                            '<input type="hidden" id="CertificationReimbursementList_' + i + '__RequestGI" name ="CertificationReimbursementList[' + i + '].RequestGI" value="' + d.data[i].requestGI + '"/>' +
                            '<input type="hidden" id="CertificationReimbursementList_' + i + '__RequestCode" name ="CertificationReimbursementList[' + i + '].RequestCode" value="' + d.data[i].requestCode + '"/>' +
                            '<input type="hidden" id="CertificationReimbursementList_' + i + '__ExamFee" name ="CertificationReimbursementList[' + i + '].ExamFee" value="' + d.data[i].examFee + '"/><span id="edit_' + i + '">' +
                            ' <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + "'" + d.data[i].requestGI + "'" + ',' + "'" + d.data[i].requestCode + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'


                        if (d.data[i].examFee != null)
                            d.data[i].examFee = '<span id="hideAmount_' + i + '">' + d.data[i].examFee + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="examFee" maxlength="8"  onkeypress = "return onlyAmount(event)" oninput = "process(this)"></span>'

                        else
                            d.data[i].examFee = '<span id="hideAmount_' + i + '">' + '' + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8"  onkeypress = "return onlyAmount(event)" oninput = "process(this)"></span>'

                    }
                    $('.PendingTabCount').html(d.pendingCount);
                    $('.ConfirmedTabCount').html(d.confirmedCount);
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "location" },
                { "data": "designation" },
                { "data": "certificationName" },
                { "data": "examFee" },
                { "data": "allowance" },
            ],

            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "autoWidth": false,

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
    }
    else {
        var dataTable = $("#Certi_confirmTable").DataTable({
            //"lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "pagingType": 'full_numbers',
            "destroy": true,
            "ajax": {
                "url": "/PayRoll/CertificationReimbursement/GetCertificationReimbursementList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&TabIndex=' + currentTab + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {
                    debugger;

                    $('#CertificationReimbursementList_Count').val(d.recordsTotal);
                    $('#totEmpCnt').html(d.recordsTotal);
                    if (d.data != "") {
                        $('#empWithAmt').html(d.data[0].employeeswithAmount);
                        $('#totAmt').html(d.data[0].totalAmount);
                    }
                    if ($('#CertificationReimbursementList_Count').val() == '0')
                        $('.textDisplay').addClass('footerDiv');
                    else
                        $('.textDisplay').removeClass('footerDiv');
                    debugger;

                    //for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //}
                    $('.PendingTabCount').html(d.pendingCount);
                    $('.ConfirmedTabCount').html(d.confirmedCount);
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "location" },
                { "data": "designation" },
                { "data": "certificationName" },
                { "data": "examFee" },
            ],

            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "autoWidth": false,

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
    }

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

    $('#newAmount_' + i).val($('#hideAmount_' + i).text());
    $('#hideAmount_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');


}

function onCancelClick(i) {
    debugger


    $('#hideAmount_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);



}

function onUpdateClick(index, EmployeeGI, EmployeeCode, RequestGI, RequestCode) {
    debugger;
    var UpdatedAmount = $('#newAmount_' + index).val()
    var Month = $('#Month_Code').val();
    if (UpdatedAmount == "" || UpdatedAmount == null) {
        swal.fire("Amount should not be null");
        return false;
    }
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
                url: '/PayRoll/CertificationReimbursement/CertificationEmployeeWiseSave',
                dataType: 'json',
                data: { UpdatedAmount: UpdatedAmount, Month: Month, EmployeeGI: EmployeeGI, EmployeeCode: EmployeeCode, RequestGI: RequestGI, RequestCode: RequestCode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            fillData();
                            //var url = "/PayRoll/VariableAllowance/GetList";
                            //$('#Variableallowancelist').load(url, { Month: Month, Allowance: Allowance }, function () { });

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

function SaveCertification() {
    debugger;
    data = $('#Certification_Entry').serialize();
    var Month = $('#Month_Code').val();
    swal.fire({
        text: "Do you want to Apply?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;

            $.ajax({
                type: 'POST',
                url: '/PayRoll/CertificationReimbursement/SaveCertificationDetails?Month=' + Month ,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            fillData();
                           /* window.location.href = "/PayRoll/CertificationReimbursement/Index";*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });

        }
        else
            return false;
    })


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
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}
function ExportToExcel() {
    debugger;
    var MonthCode = $('#Month_Code').val();
    var tableLength = $('#CertificationReimbursementList_Count').val();
    var MonthName = $('#Month_Code option:selected').text();
    var tabName;
    if (currentTab == 0)
        tabName = "Pending";
    else
        tabName = "Confirmed";
    if (tableLength > 0) {
        var param = $('#BasicFilter').serialize() + '&Month=' + MonthCode + '&MonthName=' + MonthName + '&TabIndex=' + currentTab + '&tabName=' + tabName + '&TotalRows=' + tableLength;
        url = '/PayRoll/CertificationReimbursement/ExportToExcel?data=' + param;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}