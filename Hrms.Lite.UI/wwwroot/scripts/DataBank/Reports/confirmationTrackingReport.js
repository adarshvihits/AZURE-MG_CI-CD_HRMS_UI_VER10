function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    if (fromDate != '' && toDate != '') {
        var dataTable = $("#ConfirmationTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/ConfirmationTrackingReport/GetList",
                "type": "POST",
                "datatype": "json",
                'data':
                    function (d) {
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&From=' + fromDate + '&To=' + toDate + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ConfirmationTrackingReportList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].actions = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="View" onclick="onViewConfirmationClick(' + "'" + d.data[i].hdCode + "','" + d.data[i].employeeGI + "'" + ')"></i></td>'
                    }
                    console.log(d.data);
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "designation" },
                { "data": "funcDesignation" },
                { "data": "region" },
                { "data": "currSalary" },
                { "data": "rmStatus" },
                { "data": "rmExtReason" },
                { "data": "rmAuthority" },
                { "data": "hrExeStatus" },
                { "data": "hrExeExtReason" },
                { "data": "hrExeAuthority" },
                { "data": "actions" },

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
        CloseSlider('basicFilter');
    }
}
function onViewConfirmationClick(HDcode, EmployeeGI) {
    showSpinner();
    debugger
    $.ajax({
        type: 'GET',
        url: '/Databank/ConfirmationTrackingReport/GetTrackingDetails',
        dataType: 'html',
        data: { HDcode: HDcode, EmployeeGI: EmployeeGI },
        success: function (result) {
            var m = $('.trackerPopup');
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
function ExportToExcel() {
    var filter = $('#BasicFilter').serialize();
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var TableLength = $('#ConfirmationTrackingReportList_Count').val();
    var PARAM = 'data=' + filter + '&fromDate=' + fromDate + '&toDate=' + toDate;

    if (TableLength > 0) {
        url = '/DataBank/ConfirmationTrackingReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}