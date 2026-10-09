function validateDropdowns() {
    var i = 0;
    if (!validateById('#LeaveGroup_Code', 'MlevGroup') && !validateById('.leavegroup', 'levGroup'))
        i++;
    if (!validateById('#Leave_Code', 'Mlev') && !validateById('.leavetype', 'lev'))
        i++;

    if (!validateById('#LeavePeriodFrom_Code', 'MlevPeriodFrom') && !validateById('.from', 'levPeriodFrom'))
        i++;

    if (!validateById('#LeavePeriodTo_Code', 'MlevPeriodTo') && !validateById('.to', 'levPeriodTo'))
        i++;
    if (!validateById('#Month_Code', 'Mmonth') && !validateById('.month', 'month'))
        i++;
    if ($('#LeavePeriodFrom_Code').val() == $('#LeavePeriodTo_Code').val())
        i++;
    if (i == 0)
        return true;
    else {
        return false;
    }
}
function fillLeaveCombo(sender) {
    debugger;

    $.getJSON("/TimeOffice/LeaveCarryForward/fillLeaveCombo", { LGroup: $(sender).val() }, function (data) {

        $('#Leave_Code option').remove();
        $('#Leave_Code').append('<option value>--Leave--</option');

        for (var i = 0; i < data.length; i++) {

            $('#Leave_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });

}
function fillToDropdown(sender) {
    debugger;
    $.ajaxSetup({
        async: false
    });
    $.getJSON("/TimeOffice/LeaveCarryForward/fillLeavePeriodToDropdown", { LeavePeriodFrom: $(sender).val() }, function (data) {

        $("#LeavePeriodTo_Code").val(data[0].code);
        $("#LeavePeriodTo_Name").val(data[0].name);
        $('#LeavePeriodTo_ShortName option').remove();
        /*        $('#LeavePeriodTo_Code').append('<option value>--Leave Period To-- </option');*/
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#LeavePeriodTo_ShortName').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
    fillData();
}
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var LeaveGroup = $('.leavegroup').val();
    var Leave = $('.leavetype').val();
    var LeavePeriodFrom = $('.from').val();
    var LeavePeriodTo = $('.to').val();
    var Month = -1;



    var dataTable = $("#CFHistoryTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveCarryForward/GetLeaveCFList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&LeavePeriodFrom=" +
                        LeavePeriodFrom + "&LeavePeriodTo=" + LeavePeriodTo + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&Month=" + Month + "&Form=History"+ '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#LeaveCarryForwardList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation.name", "width": "15%" },
            { "data": "department.name", "width": "13%" },
            { "data": "location.name", "width": "10%" },
            //{ "data": "balance", "width": "8%" },
            //{ "data": "cfMaxLimit", "width": "10%" },
            { "data": "cf", "width": "8%" },     
            { "data": "userName", "width": "9%" },
            { "data": "entryDate", "width": "9%" },
            { "data": "entryFrom", "width": "9%" }



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



function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();



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

function HistoryExport() {
    debugger;
    if (validateDropdowns()) {
        debugger;

        var leaveCarryForwardTableLength = $('#LeaveCarryForwardList_Count').val();
        if (leaveCarryForwardTableLength > 0) {
            url = "/TimeOffice/LeaveCarryForward/HistoryExportToExcel?LeaveGroup=" + $('#LeaveGroup_Code').val() + "&LeavePeriodFrom=" + $('#LeavePeriodFrom_Code').val() + "&LeavePeriodTo=" + $('#LeavePeriodTo_Code').val() + "&Leave=" + $('#Leave_Code').val() + "&LPFrom=" + $('#LeavePeriodFrom_Code option:selected').text() + "&LPTo=" + $('#LeavePeriodTo_Code option:selected').text() + "&LGName=" + $('#LeaveGroup_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&TotalRow=" + leaveCarryForwardTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else
        return false
}
