function ValidateDate(fromtodate) {
    debugger;
    flag = true;
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (fromtodate == 'fromdate') {
        if (D1 > D2) {
            flag = false;
            swal.fire("From date should be less than To date");
        }
    }
    else {
        if (D2 < D1) {
            flag = false;
            swal.fire("To date should be less than From date");
        }
    }
    return flag;

}


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    type = $('#Loans_Code').val();



    var dataTable = $("#LoanIssueReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/LoanIssueReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&type=' + type + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {






                $('#LoanIssueReports_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].employeeName = '<td data-label="Employee Name"><a data-bs-toggle="modal"data-target=".bd-example-modal-lg" type="button"class="btn  btn-sm float-left" onclick="OnEmpNameClick(' + "'" + d.data[i].employeeGI + "'" + ', ' + d.data[i].loanIssueCode + ', ' + "'" + d.data[i].employeeID + "'" + ', ' + "'" + d.data[i].employeeName + "'" + ')">' + d.data[i].employeeName + '</a><input type="hidden" id="empID_' + i + '" value="' + d.data[i].employeeID + '"></td><input type="hidden" id="empNAME_' + i + '" value="' + d.data[i].employeeName + '"></td>'

                    d.data[i].loanIssueDate = _GetFormattedDate(d.data[i].loanIssueDate);


                }
                debugger;
                console.log(d.data)

                //$("#PrincAmount").html(d.principleAmount);
                //$("#TopupAmt").html(d.topUpAmount);
                //$("#AdjstAmount").html(d.adjAmount);
                //$("#AlrdyPaidAmt").html(d.alreadyPaidAmount);
                //$("#balanceAmt").html(d.balanceAmount);
                //$("#emiAmount").html(d.monthlyEMI);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "division" },
            { "data": "loanIssueDate" },
            { "data": "loanSettingsName" },
            { "data": "principleAmount" },
            { "data": "topUpAmount" },
            { "data": "adjustmentAmount" },
            { "data": "totalMonthlyEMI" },
            { "data": "alreadyPaidAmount." },
            { "data": "totalBalanceAmount" },
            { "data": "remainingTenure" },
            { "data": "totalTenure" },
            { "data": "employeeStatus" },
            { "data": "loanStatus" },

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
        //dataTable.search('').draw();
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

function ExportToExcel() {
    debugger;

    var filter = $('#BasicFilter').serialize();

    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var type = $('#Loans_Code').val();
    var typeName = $('#Loans_Code option:selected').text();

    var TableLength = $('#LoanIssueReports_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/LoanIssueReport/ExportToExcel?data=" + filter + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&type=" + type + "&typeName=" + typeName + "&totalRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
