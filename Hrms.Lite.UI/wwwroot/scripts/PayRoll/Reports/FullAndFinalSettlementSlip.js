
function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fromdate = $('#SettlementFrom').val();
   var todate = $('#SettlementTo').val();


    
    var dataTable = $("#FullandfinalSlipTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/FullAndFinalSettlementSlip/GetFullAndFinalSlipList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromdate=' + fromdate +'&todate='+todate+'&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                $('#EmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    d.data[i].resignationDate = _GetFormattedDate(d.data[i].resignationDate);
                    d.data[i].settlementDate = _GetFormattedDate(d.data[i].settlementDate);

                    d.data[i].employeeName = '<a class="" onclick="onEmpNameClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].finalSettlementGI + "'" + ')">' + d.data[i].employeeName +'</a>'

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "doj", "width": "10%" },
            { "data": "resignationDate", "width": "10%" },
            { "data": "lwd", "width": "10%" },
            { "data": "settlementDate", "width": "10%" },
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

    $.fn.DataTable.ext.pager.numbers_length = 5;




}

//function OnDateClick()
//{
//    debugger;
//    var fromdate = $('#SettlementFrom').val();
//    var todate = $('#SettlementTo').val();        


//    $.ajax({

//        type: 'GET',
//        url: '/PayRoll/FullAndFinalSettlementSlip/GetList',
//        dataType: 'html',
//        data: { fromdate: fromdate, todate: todate },
//        success: function (result) {
//            debugger
//            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }

//            else {

//                $('#fullandfinalSlipList').html("");
//                $('#fullandfinalSlipList').html(result);
//            }

           
//        },


//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });


//}



function onEmpNameClick(EmployeeGI, FinalSettlementGI)
{
    debugger;
    url = "/PayRoll/FullAndFinalSettlementSlip/GetEmpDetails?EmployeeGI=" + EmployeeGI + "&FinalSettlementGI=" + FinalSettlementGI ;
    window.location.href = url;
}



function ExportToExcel() {
    debugger;
    var fromdate = $('#SettlementFrom').val();
    var todate = $('#SettlementTo').val();

    var tableLength = $('#EmployeeList_Count').val();
    if (tableLength > 0) {
        url = "/PayRoll/FullAndFinalSettlementSlip/ExportToExcel?fromDate=" + fromdate + "&toDate=" + todate + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




function ApplyFilter(mode) {
   
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

function GetFullAndFinalReport(empCode,UserCode, CompanyCode,EmployeeName,ID) {
    debugger;
    var url = "/PayRoll/FullAndFinalKendoReport/GetFullAndFinalReportPdf?EmployeeCode=" + empCode + "&UserCode=" + UserCode + "&CompanyCode=" + CompanyCode + "&EmployeeName=" + EmployeeName + "&ID=" + ID;
    window.location.href = url;

}


var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

function inWords(num) {
    debugger;
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'And ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'Only ' : '';
    return str;
}