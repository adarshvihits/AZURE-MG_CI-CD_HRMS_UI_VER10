function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var MonthCode = $("#MonthDD_Code").val();
    var temp = filter1;
    var dataTable = $("#NightAllowanceTable").DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/NightAllowanceTracking/GetList",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    temp = '';
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#NightAllowanceTrackingList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].applyDate = _GetFormattedDate(d.data[i].applyDate);
                    d.data[i].action = '<td data-label="Tracker"><i class="icon-xl la la-arrow-circle-right" ' + 'data-bs-toggle="tooltip" title="Tracker" ' + 'onclick="onTrackerClick(' + "'" + d.data[i].hdCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ')"></i></td>';
                }
                return d.data;
            }
        },
        "columns": [
            { "data": "requestID" },
            { "data": "applyDate" },
            { "data": "employeeID"},
            { "data": "employeeName"},
            { "data": "designation"},
            { "data": "funcDesignation"},
            { "data": "authority"},
            { "data": "status" },
            { "data": "purpose" },
            { "data": "amount" },
            { "data": "action" },
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
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
    }
}

function ExportToExcel() {
    debugger;
    var MonthCode = $("#MonthDD_Code").val();
    var Month = $('#MonthDD_Code option:selected').text();
    var tableLength = $('#NightAllowanceTrackingList_Count').val();
    var filter1 = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filter1 + '&TotalRec=' + tableLength + '&MonthCode=' + MonthCode + '&Month=' + Month;
    if (tableLength > 0) {
        url = '/DataBank/NightAllowanceTracking/ExportToExcel?' + PARAM;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
function onTrackerClick(HDCode, EmployeeGI) {
    debugger;
    showSpinner();
    $.ajax({
        type: 'GET',
        url: "/DataBank/NightAllowanceTracking/GetTrackerDetails",
        dataType: 'html',
        data: { HdCode: HDCode, EmployeeGI: EmployeeGI},
        success: function (result) {
            var m = $('.nightAllwTrackerPopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}