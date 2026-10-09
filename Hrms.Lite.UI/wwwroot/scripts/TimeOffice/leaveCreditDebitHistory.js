function fillData() {


    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    LeavePeriod = $('#LeavePeriod_Code').val();
    Month = $('#Month_Code').val();
    Leave = $('#Leave_Code').val();
    DebitCredit = $("#DebitCredit_Code").val();



    var dataTable = $("#cre_deb_HistoryTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveCreditDebit/GetLeaveCreditDebitList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + LeavePeriod
                        + '&Month=' + Month + '&Leave=' + Leave + '&DebitCredit=' + DebitCredit + "&Form=History" + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {


                $('#LeaveCreditDebitList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "9%" },
            { "data": "employeeName", "width": "16%" },
            { "data": "designation.name", "width": "14%" },
            { "data": "department.name", "width": "14%" },
            { "data": "location.name", "width": "11%" },
            { "data": "balance", "width": "7%" },
            { "data": "appliedLeave", "width": "7%" },
            { "data": "userName", "width": "7%" },
            { "data": "entryDate", "width": "7%" },
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

function ExportToExcel() {
    debugger;
    if (validateDropdowns()) {
        debugger;
        var leaveCreditdebitTableLength = $('#LeaveCreditDebitList_Count').val();
        if (leaveCreditdebitTableLength > 0) {
            url = "/TimeOffice/LeaveCreditdebit/HistoryExportToExcel?LeavePeriod=" + $('#LeavePeriod_Code').val() + "&Leave=" + $('#Leave_Code').val() + "&Month=" + $('#Month_Code').val() + "&DebitCredit=" + $('#DebitCredit_Code').val() + "&LPName=" + $('#LeavePeriod_Code option:selected').text() + "&MName=" + $('#Month_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&DCName=" + $('#DebitCredit_Code option:selected').text() + "&totRecords=" + leaveCreditdebitTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}
function ApplyFilter(mode) {
    debugger
    if (validateDropdowns()) {
        debugger
        var tabIndex = $('#Tabindex').val();
        var search = $('#SearchId').val();
       
        $("#SearchId").val($('#FilterList_Code').val())
        if (mode == 'apply') {
            debugger
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
}

function validateDropdowns() {
    var i = 0;
    debugger
    if (!validateById('#LeavePeriod_Code', 'leaveperiod')) {
        i++;
    }
    if (!validateById('#Leave_Code', 'leave'))
        i++;
    if (!validateById('#Month_Code', 'month'))
        i++;
        if (!validateById('#DebitCredit_Code', 'debitcredit'))
            i++;
    if (i == 0)
        return true;
    else {
        return false;
    }
}