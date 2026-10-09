function ValidateDate() {
    debugger;
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var D1 = new Date(FromDate);
    var D2 = new Date(ToDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        return false;
    }
    else
        return true;
}

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    var dataTable = $("#LessHourReportListTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LessHourReport/GetLessHourReportList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + start + '&length=' + d.length + '&searchValue=' + searchValue + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {


                $('#LessHourReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].punchingDate = _GetFormattedDate(d.data[i].punchingDate);
                }
                console.log(d.data)
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "5%" },
            { "data": "fullName", "width": "11%" },
            { "data": "department", "width": "8%" },
            { "data": "location", "width": "8%" },
            { "data": "punchingDate", "width": "8%" },
            { "data": "session", "width": "4%" },
            { "data": "shift", "width": "8%" },
            { "data": "in", "width": "5%" },
            { "data": "out", "width": "5%" },
            { "data": "totalTime", "width": "6%" },
            { "data": "inShiftTime", "width": "6%" },
            { "data": "earlyIn", "width": "5%" },
            { "data": "lateIn", "width": "5%" },
            { "data": "earlyOut", "width": "5%" },
            { "data": "lateOut", "width": "5%" },
            { "data": "totalLessHours", "width": "6%" },


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
        dataTable.search('').draw();
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

    var filter = $('#BasicFilter').serialize();

    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();

    var PARAM = 'data=' + filter + '&FromDate=' + FromDate + '&ToDate=' + ToDate;
    var TableLength = $('#LessHourReportList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/LessHourReport/ExportToExcel?' + PARAM + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}