function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var From = $('#From').val();
    var To = $('#To').val();
   var temp = filter1;

    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $('#Gratuity_Table_Report').DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/GratuityProcessingReport/GetgratuityReportList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +'&From='+ From + '&To='+ To + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {


                debugger;                
                $('#GratuityReport_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].gratuityEmpDetails.doj = _GetFormattedDate(d.data[i].gratuityEmpDetails.doj);
                    d.data[i].gratuityEmpDetails.doc = _GetFormattedDate(d.data[i].gratuityEmpDetails.doc);
                    d.data[i].gratuityEmpDetails.lwd = _GetFormattedDate(d.data[i].gratuityEmpDetails.lwd);
                    d.data[i].gratuityPayments.checkDate = _GetFormattedDate(d.data[i].gratuityPayments.checkDate);
                    d.data[i].gratuityEmpDetails.employeeName = '<a onclick="GetReportPDF(' + d.data[i].gratuityEmpDetails.employeeCode + ')">' + d.data[i].gratuityEmpDetails.employeeName + ' </a >'


                }                
                return d.data;
            }

        },

        "columns": [

            { "data": "gratuityEmpDetails.employeeID" },
            { "data": "gratuityEmpDetails.employeeName" },
            { "data": "gratuityEmpDetails.designation" },
            { "data": "gratuityEmpDetails.department" },
            { "data": "gratuityEmpDetails.location" },
            { "data": "gratuityEmpDetails.doj" },
            { "data": "gratuityEmpDetails.doc" },
            { "data": "gratuityEmpDetails.lwd" },
            { "data": "employeeGratuity.breakingYears" },
            { "data": "employeeGratuity.actualYears" },
            { "data": "employeeGratuity.gratuityAmount" },
            { "data": "gratuityPayments.checkNumber" },
            { "data": "gratuityPayments.checkDate" },
            { "data": "employeeGratuity.aadharNo" },
            { "data": "employeeGratuity.uan" },
            { "data": "employeeGratuity.pf" },
         /*   { "data": "employeeGratuity.esi" },*/
/*            { "data": "employeeGratuity.lic" }*/
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


    const myTimeout = setTimeout(tooltipHover, 500);




}



function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var From = $('#From').val();
    var To = $('#To').val();


    var PARAM = 'data=' + filter + '&searchValue=' + $('#dataTableSearch').val() + '&From=' + From + '&To=' + To;
    var TableLength = $('#GratuityReport_Count').val();
    if (TableLength > 0) {
        url = '/PayRoll/GratuityProcessingReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
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
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}
function GetReportPDF(EmployeeCode,GcCode,EmployeeGI) {
    debugger;
    var url = "/PayRoll/GratuityProcessingReport/GetGratuityProcessingPDF?EmpCode=" + EmployeeCode;
/*    window.location.href = url;*/
    window.open(url,"_blank")
    }