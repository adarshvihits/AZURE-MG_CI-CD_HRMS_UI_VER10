

var prevTab = 'PendingTab';
var currentTab = "PENDING";

function tabClick(Tab, TabIndex) {
    debugger;
    //showSpinner();
    var LeavePeriod = $('#LeavePeriod_Code').val();
    var Reason = $('#Reason').val();
    currentTab = TabIndex;

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    fillData();
 
    prevTab = Tab;
}

function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Reason = $('#Reason').val();

    var LPeriod = $('#LeavePeriod_Code').val();
    var Month = $('#Month_Code').val();
    var MonthName = $('#Month_Code option:selected').text();
    var Reasonname = $('#Reason option:selected').text();
    var LName = $('#LeavePeriod_Code option:selected').text();

    var PARAM = 'data=' + filterdata;
    var TableLength = $('#PunchingRegularizationList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/PunchingRegularizationReport/ExportToExcel?' + PARAM + "&TabIndex=" + currentTab + "&Month=" + Month + "&Reason=" + Reason + "&LeavePeriod=" + LPeriod + "&TotalRecords=" + TableLength + "&LeaveName=" + LName + "&MName=" + MonthName + "&ReasonName=" + Reasonname;
        //url = '/Custom/CautionDeposit/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}






function fillData() {

    debugger;


    var Reason = $('#Reason').val();

    var LPeriod = $('#LeavePeriod_Code').val();
    var Month = $('#Month_Code').val();

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var dataTable = $("#PunchingRegTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/PunchingRegularizationReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + currentTab + '&Month=' + Month + '&Reason=' + Reason + '&LeavePeriod=' + LPeriod + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {

                debugger;
                $('#PunchingRegularizationList_Count').val(d.recordsTotal);
                $('.PendingCount').html(d.pending);
                $('.ApprovedCount').html(d.approved);
                $('.RejectedCount').html(d.rejected);
                $('.AllCount').html(d.all);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    debugger;
               
                    d.data[i].date = _GetFormattedDate(d.data[i].date);
                    d.data[i].appliedOn = _GetFormattedDate(d.data[i].appliedOn);
                } 

                return d.data;
            }

        },

        "columns": [
            { "data": "employeeID", "width": "20%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "appliedOn", "width": "20%" },
            { "data": "date", "width": "20%" },
            { "data": "in", "width": "15%" },
            { "data": "out", "width": "15%" },
            { "data": "reason", "width": "15%" },
            { "data": "remarks", "width": "15%" },
            { "data": "status", "width": "15%" },



       

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

function onLeavePeriodChange(sender, spanId) {
    debugger;

    var code = $(sender).val();
    if (code == 0) {
        $("#" + spanId + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + spanId + "").html("This field is required");
    }
    else {
        $("#" + spanId + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + spanId + "").html("");
        $.getJSON("/TimeOffice/PunchingRegularizationReport/GetMonth", { LeavePeriod: code }, function (data) {
            console.log(data);
            $('#Month_Code option').remove();
            $('#Month_Code').append('<option value>Select Month</option');
            debugger;
            for (var i = 0; i < data.length; i++) {
                $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });
    }
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