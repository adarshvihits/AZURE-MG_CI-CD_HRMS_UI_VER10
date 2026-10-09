function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var LeavePeriod = $('#LeavePeriod_Code').val();
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();
    var Month = -1;

    var dataTable = $("#EncashHistoryTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveEncash/GetLeaveEncashList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod + '&LeaveGroup=' +
                        LeaveGroup + '&Leave=' + Leave + '&Month=' + Month  + "&Form=History" + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#LeaveEncashmentList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                   
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "13%" },
            { "data": "designation.name", "width":"11%"},
            { "data": "department.name", "width": "11%" },
            { "data": "location.name", "width": "9%" },
            { "data": "balance", "width": "6%"},
            { "data": "maxLimit", "width": "7%"},
            { "data": "value", "width": "6%" },
            { "data": "userName", "width": "10%" },
            { "data": "entryDate", "width": "8%" },
            { "data": "entryFrom", "width": "8%" }
          


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

    setTimeout(tooltipHover, 500);






}
function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
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
function validateDropdowns() {
    var i = 0;
    if (!validateById('#LeavePeriod_Code', 'MlevPeriod') && !validateById('.Lperiod', 'levPeriod'))
        i++;
    if (!validateById('#LeaveGroup_Code', 'MlevGroup') && !validateById('.Lgroup', 'levGroup'))
        i++;
    if (!validateById('#Leave_Code', 'Mlev') && !validateById('.Ltype', 'lev'))
        i++;

    if (i == 0)
        return true;
    else {
        return false;
    }
}
function fillLeaveCombo(sender) {
    debugger;

    $.getJSON("/TimeOffice/LeaveEncash/fillLeaveCombo", { LGroup: $(sender).val() }, function (data) {

        $('#Leave_Code option').remove();
        $('#Leave_Code').append('<option value>--Leave--</option');

        for (var i = 0; i < data.length; i++) {

            $('#Leave_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function HistoryExport() {
    debugger;
    if (validateDropdowns()) {
        debugger

        var LeavePeriod = $('#LeavePeriod_Code').val();
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var Leave = $('#Leave_Code').val();

        var tableLength = $('#LeaveEncashmentList_Count').val();

        if (tableLength > 0) {
            url = '/TimeOffice/LeaveEncash/HistoryExportExcel?LeavePeriod=' + LeavePeriod + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&LPName=" + $('#LeavePeriod_Code option:selected').text() + "&LGName=" + $('#LeaveGroup_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&totRecords=" + tableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}