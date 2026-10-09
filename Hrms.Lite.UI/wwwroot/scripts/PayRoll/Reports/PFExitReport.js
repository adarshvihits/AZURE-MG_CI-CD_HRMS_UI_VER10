function fillData() {
    debugger;
    var SalPeriod = $('#Month').val();
    var PfGrp = $('#PFGroup').val();
    debugger;
    var filter1 = $('#BasicFilter').serialize();


    var temp = filter1;

    var dataTable = $("#PFExitReportSummary_List").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PFFormExitReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&SalaryPeriod=' + SalPeriod + '&PfGroup=' + PfGrp +'&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {

                $('#PFExitReportSummary_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].fullName = '<div><a onclick="GetPdf(' + d.data[i].employeeCode + ')">' + d.data[i].fullName + ' </a ></div></div>'
                   




                }
                return d.data;
            }
        },
        "columns": [

            { "data": "employeeID", "width": "20%" },
            { "data": "fullName", "width": "20%" },
            { "data": "uan", "width": "15%" },
            
             {"data": "reason", "width": "15%" },
            

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

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        // dataTable.search('').draw();
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


function OnNotepadClick() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var SalPeriod = $('#Month').val();
    var PfGrp = $('#PFGroup').val();
    var monthName = $('#Month option:selected').text();
    var PARAM = 'data=' + filterdata;
    var TableLength = $('#PFExitReportSummary_Count').val();
        if (TableLength > 0) {

            url = '/PayRoll/PFFormExitReport/ExportToNotePad?' + PARAM + "&SalaryPeriod=" + SalPeriod + "&PfGroup=" + PfGrp + "&TableLength=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }

}

function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var SalPeriod = $('#Month').val();
    var PfGrp = $('#PFGroup').val();
    var PfName = $('#PFGroup option:selected').text();
    var monthName = $('#Month option:selected').text();
    var PARAM = 'data=' + filterdata;
    var TableLength = $('#PFExitReportSummary_Count').val();
   
    if (TableLength > 0) {
        url = '/PayRoll/PFFormExitReport/ExportToExcel?' + PARAM + "&SalaryPeriod=" + SalPeriod + "&PfGroup=" + PfGrp + "&SalaryMonth=" + monthName + "&PfGroupName=" + PfName +"&TotalRecords="+ TableLength;
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
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();
        /*CloseSlider('basicFilter');*/
    }
}