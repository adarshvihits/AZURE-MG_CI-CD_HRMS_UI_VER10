var currentTab = 'ALL';
var prevTab = 'All';
function OnTabClick(Tab, TabIndex) {
    debugger;
  
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var leaveperiod = $('#LeavePeriod_Code').val();
   

    fillData();

    prevTab = Tab;
}



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var leaveperiod = $('#LeavePeriod_Code').val();
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var Type = $('#classification').val();


    var dataTable = $("#LeaveApplicationTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveApplicationReport/GetLeaveApplicationList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&leaveperiod=" + leaveperiod + "&TabIndex=" + currentTab + "&fromDate=" + fromDate + "&toDate=" + toDate + "&Type=" + Type +'&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {

                $('#LeaveApplicationReportList_Count').val(d.recordsTotal);

               //TabCounts

                $('.AttRegTabCount').html(d.tabcount.attendanceRegularizationCount);
                $('.WFHTabCount').html(d.tabcount.wfhCount);
                $('.COFFTabCount').html(d.tabcount.coffCount);
                $('.ODTabCount').html(d.tabcount.onDutyCount);
                $('.AllTabCount').html(d.tabcount.allCount);
                $('.LeaveTabCount').html(d.tabcount.leaveCount);
                $('.PermHourTabCount').html(d.tabcount.permissionHourCount);

       
                

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].from = _GetFormattedDate(d.data[i].from);
                    d.data[i].to = _GetFormattedDate(d.data[i].to);
                    d.data[i].appliedOn = _GetFormattedDate(d.data[i].appliedOn);
                    //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    d.data[i].designation = '<a onclick="OnTrackerClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].leaveApplicationGI + "'" + ',' + "'" + d.data[i].leaveType + "'" + ')"><i class="icon-xl la la-arrow-circle-right"></i></a>'
                    if (currentTab == "COFF") {
                        d.data[i].leaveOn = _GetFormattedDate(d.data[i].leaveOn);
                    }
                           
                    d.data[i].approvedDate = _GetFormattedDate(d.data[i].approvedDate);
                }

                return d.data;
            }

        },

        "columns": [

            //{ "data": "id","width":"10%" },
            //{ "data": "employeeName","width": "19%" },
            //{ "data": "appliedOn","width": "10%" },
            //{ "data": "type","width": "11%" },
            //{ "data": "from","width": "9%"},
            //{ "data": "to","width": "9%" },
            //{ "data": "noOfDays","width": "8%" },
            //{ "data": "reason","width": "10%" },
            //{ "data": "status","width": "10%"},
            //{ "data": "designation","width": "7%" },

            { "data": "id" },
            { "data": "employeeName" },
            { "data": "appliedOn" },
            { "data": "type" },
            { "data": "from" },
            { "data": "to"},
            { "data": "leaveOn"},
            { "data": "entitleDate"},
            { "data": "noOfDays"},
            { "data": "reason"},
            { "data": "status" },
            { "data": "approvedBy" },
            { "data": "approvedFunDesg" },
            { "data": "approvedDate" },
            { "data": "statusFrom"},
            { "data": "remarks"},
            { "data": "designation"},

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


    if (currentTab =="COFF") {
        let column4 = dataTable.column(4)
        column4.visible(false);
        let column5 = dataTable.column(5)
        column5.visible(false);

        let column6 = dataTable.column(6)
        column6.visible(true);
        let column7 = dataTable.column(7)
        column7.visible(true);
    }
    else {
        let column4 = dataTable.column(4)
        column4.visible(true);
        let column5 = dataTable.column(5)
        column5.visible(true);

        let column6 = dataTable.column(6)
        column6.visible(false);
        let column7 = dataTable.column(7)
        column7.visible(false);
    }
}









function GetList()
{
    showSpinner();
    var leaveperiod = $('#LeavePeriod_Code').val();
    //var url = "/TimeOffice/LeaveApplicationReport/GetList";
    //$('#LeaveApplicationList').load(url, { TabIndex: currentTab, leaveperiod: leaveperiod }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveApplicationReport/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab, leaveperiod: leaveperiod },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LeaveApplicationList').html("");
                $('#LeaveApplicationList').html(result);
            }
            hideSpinner();


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}
function OnTrackerClick(EmployeeGI, LeaveApplicationGI, LeaveType) {
    debugger;
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/LeaveApplicationReport/GetApplicationTracker',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, LeaveApplicationGI: LeaveApplicationGI, LeaveType: LeaveType},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#LeaveApplicationTracker').html("");
                $('#LeaveApplicationTracker').html(result);
            }
            hideSpinner();


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });


}


function ApplyFilter(mode) {
    debugger;
    var leaveperiod = $('#LeavePeriod_Code').val();

    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
        
    }
}



function ExportToExcel() {

    debugger;
    var leaveperiod = $('#LeavePeriod_Code').val();
    var leave = $('#LeavePeriod_Code option:selected').text();
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var Type = $('#classification').val();



    var pendingTableLength = $('#LeaveApplicationReportList_Count').val();
        if (pendingTableLength > 0) {
            var url = "/TimeOffice/LeaveApplicationReport/ExportToExcel?TabIndex=" + currentTab + "&leaveperiod=" + leaveperiod + "&leave=" + leave + "&TotalRows=" + pendingTableLength + "&fromDate=" + fromDate + "&toDate=" + toDate + "&Type=" + Type;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}