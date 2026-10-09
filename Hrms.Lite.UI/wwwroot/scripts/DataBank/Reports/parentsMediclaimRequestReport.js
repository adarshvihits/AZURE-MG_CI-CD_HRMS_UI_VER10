




function fillData(status) {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var FinancialYearcode = $('#FinancialYearcode').val();

    /* if (Allowance != '') {*/
    var dataTable = $("#ParentsMediclaimRequestReporttable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": true,
        "ajax": {
            "url": "/DataBank/ParentsMediclaimRequestReport/GetParentsMediclaimRequestReportList",
            "type": "POST",

            "datatype": "json",
            'data':
                function (d) {
                    var searchValue = $('#dataTableSearch').val();

                    // 🔥 IMPORTANT — PASS TAB INDEX HERE
                    d.filter = filter1 +
                        '&draw=' + d.draw +
                        '&start=' + d.start +
                        '&length=' + d.length +
                        '&searchValue=' + searchValue +
                        '&FinancialYearcode=' + FinancialYearcode +  // 🔥 CHANGED
                        '&dummy=dummy';

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#ParentsMediclaimRequestReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].parentDOB = _GetFormattedDate(d.data[i].parentDOB);
                    d.data[i].requestDate = _GetFormattedDate(d.data[i].requestDate);

                }
                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeId" },
            { "data": "name" },
            { "data": "parentName" },
            { "data": "relation" },
            { "data": "gender" },
            { "data": "parentDOB" },
            { "data": "requestDate" },
            { "data": "sumInsured" },
            { "data": "emiAmount" },
            { "data": "emiPeriod" },
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

    /*setTimeout(datepicker, 500);*/

}


























function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        /* showSpinner();*/
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
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}










function ExportToExcel() {
    debugger;
    var FinancialYearcode = $('#FinancialYearcode').val();

    var tlength = 0;

    var currentTabName = 'ero';
    tlength = $('#ParentsMediclaimRequestReportList_Count').val();


    if (tlength != 0) {
        url = "/DataBank/ParentsMediclaimRequestReport/ExportToExcel?FinancialYearcode=" + FinancialYearcode + "&currentTabName=" + currentTabName
            + "&TotalRows=" + tlength + "&" + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}













