
var tab_index = 0;

function GetPopUp(slno, Name, TabName) {
    debugger
    var SalaryPeriodCode = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
   
    debugger;
    $.ajax({
        type: 'GET',
        url: "/TimeOffice/MonthlyAttendanceProcessing/GetEmployeeDetails?Month=" + SalaryPeriodCode + "&PayrollGroup=" + PayrollGroup + "&slno=" + slno + "&RowName=" + Name + "&TabName=" + TabName + "&tab_index=" + tab_index,
        dataType: 'html',

        success: function (Data) {

            if (Name == 'Daily Attendance Processing Pending Days') {
                var m1 = $('.bd-example-modal-lg');
                m1.find('.modal-body').html('');
                m1.modal('hide');
           
                var m2 = $('.bd-example-modal-lg2');
                m2.find('.modal-body').html('');
                m2.modal('hide');

                var m = $('.bd-example-modal-lg1');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#exampleModalLabel').html(Name);
                m.modal('show');
            }
            else {
                var m1 = $('.bd-example-modal-lg1');
                m1.find('.modal-body').html('');
                m1.modal('hide');

                var m2 = $('.bd-example-modal-lg2');
                m2.find('.modal-body').html('');
                m2.modal('hide');

                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#exampleModalLabel1').html(Name);
                m.modal('show');
            }
           


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}




function DaysCountClickPopUp(employeeGI,EmpName) {
    debugger
    var month = $('#Month_Code').val();
    $.ajax({

        type: 'GET',
        url: '/TimeOffice/MonthlyAttendanceProcessing/GetBlankStatDaysPopList',
        dataType: 'html',
        data: { EmployeeGI: employeeGI, month: month },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.modal('hide');
              

                var m1 = $('.bd-example-modal-lg1');
                m.find('.modal-body').html('');
                m1.modal('hide');

              


                var m2 = $('.bd-example-modal-lg2');
                m2.find('.modal-body').html('');
                m2.find('.modal-body').html(result);
            
                $('#exampleModalLabelemp').html(EmpName);
                
                m2.modal('show');
            }



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}




function EmployeeSearch(TableID) {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById(TableID);
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td1 || td2) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }
    if (noOfRows == 1) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
    }

}

function ExportToExcelPopUp() {
    debugger
    var SalaryPeriodCode = $("#Month_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();
    var MonthName = $("#Month_Code option:selected").text();
    var TableLength = $('#MonthlyAttendanceProcessing_Validations_popup_Count').val();
    if (TableLength > 0) {
        url = "/TimeOffice/MonthlyAttendanceProcessing/ExportToExcel?TotalRows=" + TableLength + "&Payroll=" + PayrollGroup + "&month=" + SalaryPeriodCode + "&PayrollGroupName=" + PayrollGroupName + "&MonthName=" + MonthName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}











function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var month = $("#Month_Code").val();
    var payroll = $("#PayrollGroup_Code").val();
    var TabIndex = '5';
    var dataTable = $("#MapTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/MonthlyAttendanceProcessing/GetAttendanceProcessingList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + month + '&PayrollGroup=' + payroll + '&Tabindex=' + TabIndex + '&dummy=dummy';

                    

                    d = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#MonthlyAttendanceProcessing_Data_Count').val(d.recordsTotal);
                $('.map_TotalDays').html(d.take);
                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "fullName" },
            { "data": "presentDays" },
            { "data": "ra" },
            { "data": "absent" },
            { "data": "lop" },
            { "data": "esiLeave" },
            { "data": "leave" },
            { "data": "coff" },
            { "data": "onDuty" },
            { "data": "doubleDuty" },
            { "data": "wfh" },
            { "data": "holiDay" },
            { "data": "holidayPresent" },
            { "data": "offDay" },
            { "data": "offdayPresent" },
            { "data": "ljp" },
            { "data": "ltp" },
            { "data": "previousLOP" },
            { "data": "previousArrear" },
            { "data": "suspension" },
            { "data": "maternityLeave" },
            { "data": "layOff" },
            { "data": "totalLOP" },
            { "data": "paidDays" },
          
         

           
        ],



        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '40vh',
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

function fillDataAttendanceApprPopUp() {

    debugger;
    hideSpinner();
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var month = $("#Month_Code").val();
    var payroll = $("#PayrollGroup_Code").val();
    var TabIndex = '5';
    var dataTable = $("#attApprPendingTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/MonthlyAttendanceProcessing/MonthlyAttendanceProcessingApprovalPending",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    /*var searchValue = '';*/
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + month + '&PayrollGroup=' + payroll + '&Tabindex=' + TabIndex + '&dummy=dummy';



                    d = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#AttendanceApprovalPending_popup_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].fromDate = _GetFormattedDate(d.data[i].fromDate);
                    d.data[i].toDate = _GetFormattedDate(d.data[i].toDate);
                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "fullName" },    
            { "data": "department" },
            { "data": "location" },
            { "data": "type" },
            { "data": "fromDate" },
            { "data": "toDate" },
            { "data": "authority" },

        ],



        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '40vh',
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


function onNextClick(NextTabName, NextTabIndex)
{
    debugger;
    tab_index = NextTabIndex;
    var month = $("#Month_Code").val();
    var payroll = $("#PayrollGroup_Code").val();
    showSpinner();
    if (NextTabName == "map_Processing") {
        var url = "/TimeOffice/MonthlyAttendanceProcessing/GetProcessingList";
    }
    else {
        var url = "/TimeOffice/MonthlyAttendanceProcessing/MonthlyAttendanceProcessingList";
    }

    var i = 0;
    
    debugger
    if (NextTabName == "map_Settings") {
        if (!validateById("#Month_Code", 'month'))
            i++;

        if (!validateById("#PayrollGroup_Code", 'payrollgp'))
            i++;
        if (i != 0)            
            return false;
        else {
            $('#map_Settings').load(url, { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val(), TabIndex: NextTabIndex }, function () { hideSpinner(); });
            $("#map_Selection").addClass('hide');
            $("#map_Status").addClass('hide');
            $("#map_Processing").addClass('hide');
            $("#map_Wages").addClass('hide');
            $(".selectiondiv").addClass('hide');
            $("#map_Settings").removeClass('hide');
            $(".selectiondiv").addClass('hide');
            $(".selectionNext").addClass('hide');

        }
    }
    else if (NextTabName == "map_Status") {

        $('#map_Status').load(url, { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val(), TabIndex: NextTabIndex }, function () { hideSpinner();});
        $("#map_Selection").addClass('hide');
        $("#map_Settings").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Wages").addClass('hide');

        $("#map_Status").removeClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');

    }
    else if (NextTabName == "map_Leave") {

        $('#map_Leave').load(url, { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val(), TabIndex: NextTabIndex }, function () { hideSpinner(); });
        $("#map_Selection").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Wages").addClass('hide');
        $("#map_Settings").addClass('hide');
        $("#map_Leave").removeClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');

    }
    else if (NextTabName == "map_Wages") {

        $('#map_Wages').load(url, { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val(), TabIndex: NextTabIndex }, function () { hideSpinner(); });
        $("#map_Selection").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Leave").addClass('hide');
        $("#map_Settings").addClass('hide');
        $("#map_Wages").removeClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');

    }
    else
    {
        $.ajaxSetup({ async: false });

        fillDataAttendanceApprPopUp();
       
        //m.find('.modal-body').html('');
        //m.modal('show');
        //hideSpinner();


        if ($('#AttendanceApprovalPending_popup_Count').val() > 0) {
            var m = $('.apprPendingPopup');
            m.modal('show');
        }
        else {
            $('#map_Processing').load(url, { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val(), TabIndex: NextTabIndex }, function () { hideSpinner(); });
            $("#map_Selection").addClass('hide');
            $("#map_Status").addClass('hide');
            $("#map_Leave").addClass('hide');
            $("#map_Settings").addClass('hide');
            $("#map_Wages").addClass('hide');
            $("#map_Processing").removeClass('hide');
            $(".selectiondiv").addClass('hide');
            $(".selectionNext").addClass('hide');

        }

        
        


        //$('#map_Processing').load(url, { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val(), TabIndex: NextTabIndex, data: $('#BasicFilter').serialize()}, function () { hideSpinner(); });
        //$("#map_Selection").addClass('hide');
        //$("#map_Status").addClass('hide');   
        //$("#map_Leave").addClass('hide');
        //$("#map_Settings").addClass('hide');
        //$("#map_Wages").addClass('hide');
        //$("#map_Processing").removeClass('hide');
        //$(".selectiondiv").addClass('hide');
        //$(".selectionNext").addClass('hide');

  

        //window.location.href = url + "?Month=" + $("#Month_Code").val() + "&PayrollGroup=" + $("#PayrollGroup_Code").val() + '&TabIndex=' + 5;
    }
}







function onTabNameClick(TabName,index)
{
    debugger
    tab_index = index;
    if (TabName == "map_Settings") {
        $("#map_Selection").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Wages").addClass('hide');
        $("#map_Leave").addClass('hide');
        $("#map_Settings").removeClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');
       

    }
    if (TabName == "map_Selection") {
        $("#map_Settings").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Wages").addClass('hide');
        $("#map_Leave").addClass('hide');
        $("#map_Selection").removeClass('hide');
        $(".selectiondiv").removeClass('hide');
        $(".selectionNext").removeClass('hide');
      

    }
    if (TabName == "map_Status") {
        $("#map_Settings").addClass('hide');
        $("#map_Selection").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Wages").addClass('hide');
        $("#map_Status").removeClass('hide');
        $("#map_Leave").addClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');

    }
    if (TabName == "map_Leave") {
        $("#map_Selection").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Wages").addClass('hide');
        $("#map_Leave").removeClass('hide');
        $("#map_Settings").addClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');

    }
    if (TabName == "map_Wages") {
        $("#map_Selection").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Processing").addClass('hide');
        $("#map_Settings").addClass('hide');
        $("#map_Leave").addClass('hide');
        $("#map_Wages").removeClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');
      

    }
    if (TabName == "map_Processing") {
        $("#map_Selection").addClass('hide');
        $("#map_Status").addClass('hide');
        $("#map_Settings").addClass('hide');
        $("#map_Wages").addClass('hide');
        $("#map_Leave").addClass('hide');
        $("#map_Processing").removeClass('hide');
        $(".selectiondiv").addClass('hide');
        $(".selectionNext").addClass('hide');

 

    }
   
}

function fillMonth(sender) {
    debugger;
    $.getJSON("/TimeOffice/MonthlyAttendanceProcessing/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {
       
        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function onFilterClick() {
    debugger;
    $('#filterSlider').removeClass('hide');
    $('body').append('<div class="form-overlay"></div>');
    $('.basicFilter').addClass('slider-forms-open');
}
function onProceedClick()
{
    debugger

    if (validateById("#Month_Code", 'month') && validateById("#PayrollGroup_Code", 'payrollgp')) {
      
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/MonthlyAttendanceProcessing/GetMonthlyAttendanceSelectionList',
            dataType: 'html',
            data: { Month: $("#Month_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val() },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    //$('#map_Selection').html("");
                    //$('#map_Selection').html(result);
                    onNextClick('map_Settings', 1)
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
}
function OnApplyAndLockClick() {
    debugger
    swal.fire({
        text: "Once the month is locked no further changes is the attendance module can be done. The month will not display in the drop down of any transactions. Do you want to Lock?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
              //  url: '/TimeOffice/MonthlyAttendanceProcessing/AttendanceProcessingSave',
                url: '/TimeOffice/MonthlyAttendanceProcessing/ProcessingPeriodLock',
                dataType: 'json',
                data: $('#BasicFilter').serialize() + "&Month=" + $("#Month_Code").val() + "&PayrollGroup=" + $("#PayrollGroup_Code").val(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            window.location.href = '/TimeOffice/MonthlyAttendanceProcessing/Index';
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }

                }
            });
        }
    })
}
function ApplyFilter(mode)
{ 
    debugger

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
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
     

    }
}

function onExportToExcelClick()
{
    debugger
/*    tab = $('#Map_Table').DataTable();*/
    var AttendanceProcessing = $('#MonthlyAttendanceProcessing_Data_Count').val();
    if (AttendanceProcessing> 0) {
        url = "/TimeOffice/MonthlyAttendanceProcessing/OnExportToExcelClick?Month=" + $('#Month_Code').val() + "&PayrollGroup=" + $("#PayrollGroup_Code").val() + "&MonthName=" + $('#Month_Name').val() + "&PayrollGpName=" + $("#PayrollGroup_Name").val() + "&Period=" + $("#Period").val() + "&TotalRows=" + AttendanceProcessing;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }


}
function ExportToExcel_PopUp()
{
    debugger

    var AttendanceProcessing = $('#AttendanceApprovalPending_popup_Count').val();
    if (AttendanceProcessing> 0) {
        url = "/TimeOffice/MonthlyAttendanceProcessing/ExportToExcel_PopUp?Month=" + $('#Month_Code').val() + "&PayrollGroup=" + $("#PayrollGroup_Code").val() + "&MonthName=" + $('#Month_Code option:selected').text() + "&PGName=" + $("#PayrollGroup_Code option:selected").text() + "&totRecords=" + AttendanceProcessing + '&Tabindex=5' +  "&data=" + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }


}
