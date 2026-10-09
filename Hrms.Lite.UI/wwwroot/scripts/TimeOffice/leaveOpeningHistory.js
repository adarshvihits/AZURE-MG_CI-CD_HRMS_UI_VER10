var LeavePeriod;
var LeaveGroup;
var Leave;
var LPName;
var LGName;
var LName;
function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var LeavePeriod = $('#LeavePeriod_Code').val();
    var LeaveGroup = $('#LeaveGroup_Code').val();
    var Leave = $('#Leave_Code').val();



    var dataTable = $("#leaveOpeningHistoryTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveOpening/GetLeaveOpeningList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue +
                        '&LeavePeriod=' + LeavePeriod + '&LeaveGroup=' + LeaveGroup + '&Leave=' + Leave + '&Form=History'+ '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {






                $('#LeaveOpeningList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation.name", "width": "17%" },
            { "data": "department.name", "width": "17%" },
            { "data": "location.name", "width": "13%" },
            { "data": "doj", "width": "9%" },
            { "data": "opening", "width": "9%" },
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

function HistoryExport() {
    debugger;
    if (validateDropdowns()) {
        debugger

        var LeavePeriod = $('#LeavePeriod_Code').val();
        var LeaveGroup = $('#LeaveGroup_Code').val();
        var Leave = $('#Leave_Code').val();

        var tableLength = $('#LeaveOpeningList_Count').val();

        if (tableLength > 0) {
            url = '/TimeOffice/LeaveOpening/HistoryExportExcel?LeavePeriod=' + LeavePeriod + "&LeaveGroup=" + LeaveGroup + "&Leave=" + Leave + "&LPName=" + $('#LeavePeriod_Code option:selected').text() + "&LGName=" + $('#LeaveGroup_Code option:selected').text() + "&LName=" + $('#Leave_Code option:selected').text() + "&totRecords=" + tableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}