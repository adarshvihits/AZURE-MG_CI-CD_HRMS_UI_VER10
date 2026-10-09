function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fromdate = $('#SettlementFrom').val();
    var todate = $('#SettlementTo').val();



    var dataTable = $("#FullandfinalSlip_table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PostExitProcess/GetPostExitList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromdate=' + fromdate + '&todate=' + todate + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                debugger;


                $('#EmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    d.data[i].resignationDate = _GetFormattedDate(d.data[i].resignationDate);
                    d.data[i].settlementDate = _GetFormattedDate(d.data[i].settlementDate);
                    d.data[i].employeeName = d.data[i].employeeName;

                    //d.data[i].employeeName = '<a class="" onclick="onEmpNameClick(' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].employeeName + '</a>'
                    d.data[i].lastProcessingMonth = '<a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title = "Edit" id = "empattendancesettings" type = "button"class="btn btn-icon btn-edit icon-only slider-btn" onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-pencil-alt"></i></a >';

                }
              
                return d.data;
            },




        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "13%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "9%" },
            { "data": "location", "width": "9%" },
            { "data": "lwd", "width": "6%" },
            { "data": "settlementDate", "width": "11%" },
            { "data": "additions", "width": "11%" },
            { "data": "recoveries", "width": "11%" },
            { "data": "netPayable", "width": "11%" },
            { "data": "remarks", "width": "18%" },
            { "data": "lastProcessingMonth", "width": "5%" },


        ],






        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": "true",
        "language": {

            emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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

    //$.fn.DataTable.ext.pager.numbers_length = 5;




}



function onEditClick(EmpGI) {
    debugger;
    showSpinner();
  
    $.ajax({

        type: 'GET',
        url: '/PayRoll/PostExitProcess/GetPostExitDetails',
        dataType: 'html',
        data: {
            EmpGI: EmpGI
        },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#EditSlider').html("");
                $('#EditSlider').html(result);
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


function Save() {
    debugger;
    //var month = $('#Month').val();
    var netpay = $('#Net_Payable').text();
    if (validatefields()) {
        swal.fire({
            text: "Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger
                var data = new FormData($('#Save_PostExit')[0]);

                $.ajax({
                    type: 'POST',
                    url: '/PayRoll/PostExitProcess/Save',
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    data: data,
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                debugger
                                fillData();
                                CloseSlider('salaryHld');



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
    }
    else {
        return false;
    }
}

function validatefields() {
    debugger;
    var i = 0;
    //if (!validateById('#NetPay', 'holdamnt')) {
    //    i++;
    //}
    if (!validateById('#Remark', 'remarks')) {
        i++;
    }

    if (i == 0)
        return true;
    else {
        return false;
    }
}

function NetpayAmount() {
    debugger;
    var a = $('#Additions').val();
    var b = $('#Recoveries').val();
    var c = a - b;

    $('#Net_Payable').html(c);
    $('#Netpay').val(c);
}


function ExportToExcel() {
    debugger;
    var fromdate = $('#SettlementFrom').val();
    var todate = $('#SettlementTo').val();
    var pendingTableLength = $('#EmployeeList_Count').val();
    if (pendingTableLength > 0) {
        url = "/PayRoll/PostExitProcess/ExportToExcel?fromdate=" + fromdate + " &todate=" + todate + "&TotalRecords=" + pendingTableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}


function ApplyFilter(mode) {
    debugger;
    var search = $('#SearchId').val();

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
//function onEmpNameClick(EmployeeGI) {
//    debugger;
//    url = "/PayRoll/FullAndFinalSettlementSlip/GetEmpDetails?EmployeeGI=" + EmployeeGI;
//    window.location.href = url;
//}