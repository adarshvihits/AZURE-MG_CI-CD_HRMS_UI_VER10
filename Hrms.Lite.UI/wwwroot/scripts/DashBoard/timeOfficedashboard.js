

var prevpopupclass = "empty";
function onLabelClick(type, popupclass) {
    
    showSpinner();
   /* $("." + prevpopupclass).html("");*/
    if (type == "MISSING" || type == "APPROVAL PENDING")
        var url = '/DashBoard/TimeOfficeDashBoard/GetDashBoardList?Type=' + encodeURIComponent(type);
    else {
        var url = '/DashBoard/TimeOfficeDashBoard/GetDashBoardDetailsList?Type=' + encodeURIComponent(type);
        $("." + prevpopupclass).html("");
    }
    debugger
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (Data) {           
                  
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (type == "MISSING" || type == "APPROVAL PENDING") {
                    if (type == "MISSING")
                        var m = $('.timeDash_missing');

                    if (type == "APPROVAL PENDING")
                        var m = $('.timeDash_ApprovalPending');
                }
                else { 
                if (type == "LESSHOURS")
                    var m = $('.timeDash_lesshour');
                if (type == "AUTHORISED LOP")
                    var m = $('.authorisedlop');
                if (type == "UN AUTHORISED LOP")
                    var m = $('.unauthorisedlop');
                if (type == "PreviousMonthAdjustments")
                    var m = $('.timeDash_previousmonthadjst');
                if (type == "Upcoming Leaves")
                    var m = $('.timeDash_upcomingleaves');
                if (type == "ContinousAbsenteesim")
                    var m = $('.timeDash_absenteesim');
                if (type == "Duty Roster Variance")
                    var m = $('.timeDash_dutyroster');
                if (type == "LowAttendanceRate")
                        var m = $('.lowattendancerate');
                if (type == "Employees completing ML/ESI leave in next 7 days")
                    var m = $('.employee-mlesi-leave');
                    prevpopupclass = popupclass;
            }
                debugger
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                hideSpinner();
                m.modal('show');
               /* prevpopupclass = popupclass;*/
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
   
}

function onRowWiseClick(Type, SubType, popupclass)
{
    showSpinner();
    $("." + prevpopupclass).html("");
    debugger
    $.ajax({
        type: 'GET',
        url: '/DashBoard/TimeOfficeDashBoard/GetDashBoardEmployeewiseList?Type=' + encodeURIComponent(Type) + "&SubType=" + encodeURIComponent(SubType),
        dataType: 'html',
        success: function (Data) {      
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                if (Type == "MISSING") {
                    var m = $('.timeDash_missing');
                    m.modal('hide')
                    var m = $('.timeDash_MissingEmployeeDT');
                }
                if (Type == "APPROVAL PENDING") {
                    var m = $('.timeDash_ApprovalPending');
                    m.modal('hide');
                    var m = $('.timeDashApprPendingEmpDetails');
                }
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#ApprPendingTitle').html(SubType);
                $('#AttndSettingsMissingTitle').html(SubType);
                hideSpinner();
                m.modal('show');
                prevpopupclass = popupclass;
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function onShiftChange(sender) {
    debugger

    if ($("#ManPower").is(':checked')) {
        toggle = 'TODAY';
        $(".dash_ManpowerLabel").html('Today Manpower');
    }
    else {
        toggle = 'YESTERDAY';
        $(".dash_ManpowerLabel").html('Yesterday Manpower');
    }
    var url = '/DashBoard/TimeOfficeDashBoard/GetCheckInTimeChartDetails?ShiftCode=' + $(sender).val();
    //$('#ddManPowerChart').load(url, function () { });

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#checkIn').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

}


                         /*New_TimeOffice_DashBoard*/
/*******************************************************************/

function onLabelClickNew(type, popupclass) {
   
    debugger;
    var m = $('.notif_popup');
    m.find('.modal-body').html('');
    //$('.timeDash_absenteesimPopup').html('');
    //$('.timeDash_lesshourPopup').html('');
    //$('.authorisedlopPopup').html('');
    //$('.timeDash_dutyrosterPopup').html('');
    //$('.timeDash_upcomingleavesPopup').html('');
    showSpinner();
    /* $("." + prevpopupclass).html("");*/
    if (type == "MISSING" || type == "APPROVAL PENDING")
        var url = '/DashBoard/TimeOfficeDashBoard/GetDashBoardList?Type=' + encodeURIComponent(type);
    else {
        var url = '/DashBoard/TimeOfficeDashBoard/GetDashBoardDetailsListNew?Type=' + encodeURIComponent(type);
        //$("." + prevpopupclass).html("");
    }
    debugger
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (Data) {

            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (type == "MISSING" || type == "APPROVAL PENDING") {
                    if (type == "MISSING")
                        var m = $('.timeDash_missing');

                    if (type == "APPROVAL PENDING")
                        var m = $('.timeDash_ApprovalPending');
                }
                else {
                    if (type == "LESSHOURS")
                        var m = $('.timeDash_lesshour');
                    if (type == "AUTHORISED LOP")
                        var m = $('.authorisedlop');
                    if (type == "PendingReg")
                        var m = $('.pendingreg');
                    if (type == "UN AUTHORISED LOP")
                        var m = $('.unauthorisedlop');
                    if (type == "PreviousMonthAdjustments")
                        var m = $('.timeDash_previousmonthadjst');
                    if (type == "Upcoming Leaves")
                        var m = $('.timeDash_upcomingleaves');
                    if (type == "ContinousAbsenteesim")
                        var m = $('.timeDash_absenteesim');
                    if (type == "Duty Roster Variance")
                        var m = $('.timeDash_dutyroster');
                    if (type == "LowAttendanceRate")
                        var m = $('.lowattendancerate');
                    if (type == "Employees completing ML/ESI leave in next 7 days")
                        var m = $('.employee-mlesi-leave');
                    if (type == "LOP")
                        var m = $('.lopTile');
                    if (type == "WFH Leaves")
                        var m = $('.wfhleaves');
                   // prevpopupclass = popupclass;
                }
                debugger
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                hideSpinner();
                m.modal('show');
                /* prevpopupclass = popupclass;*/
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

}
function GetNotificationPopUp(Code, head) {    
    $.ajax({

        type: 'GET',
        url: '/DashBoard/TimeOfficeDashBoard/GetNotificationsPopUp',
        dataType: 'html',
        data: { NotificationCode: Code},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {              
                $('.notif_popup_head').html(head);
                var m = $('.notif_popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);               
              
                m.modal('show');
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

function DaysClickPopUp(employeeGI,type) {
    debugger
    $.ajax({

        type: 'GET',
        url: '/DashBoard/TimeOfficeDashBoard/EmployeeLeaveAndLopDaysPopUp',
        dataType: 'html',
        data: { EmployeeGI: employeeGI, Type: type},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                if (type != 'LOP') {
                    var m = $('.unauthorisedlop');
                    $('.bd-example-modal-lg .close').attr('onclick', "var m = $('.unauthorisedlop');m.modal('show');")
                }
                else {
                    var m = $('.lopTile');
                    $('.bd-example-modal-lg .close').attr('onclick', "var m = $('.lopTile');m.modal('show');")
                }

                m.modal('hide');
                
                var m1 = $('.bd-example-modal-lg');
                m1.find('.modal-body').html('');
                m1.find('.modal-body').html(result);
                debugger
                
                //$('#exampleModalLabelauth').val(m1.);
                m1.modal('show');
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

//function AbsentClickPopUp(employeeCode, type) {
//    debugger
//    $.ajax({

//        type: 'GET',
//        url: '/DashBoard/TimeOfficeDashBoard/GetABDate',
//        dataType: 'html',
//        data: { EmployeeCode: employeeCode},

//        success: function (result) {
//            debugger
//            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }
//            else {
//                debugger
//                  var m = $('.timeDash_absenteesim');
//                    $('.bd-example-modal-lg .close').attr('onclick', "var m = $('.timeDash_absenteesim');m.modal('show');")
//                //if (type != 'LOP') {
//                //    var m = $('.timeDash_absenteesim');
//                //    $('.bd-example-modal-lg .close').attr('onclick', "var m = $('.timeDash_absenteesim');m.modal('show');")
//                //}
//                //else {
//                //    var m = $('.lopTile');
//                //    $('.bd-example-modal-lg .close').attr('onclick', "var m = $('.lopTile');m.modal('show');")
//                //}

//                m.modal('hide');

//                debugger;
//                var m1 = $('.bd-example-modal-lg');
//                m1.find('.modal-body').html('');
//                m1.find('.modal-body').html(result);
//                debugger

//                //$('#exampleModalLabelauth').val(m1.);
//                m1.modal('show');
//            }



//        },
//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });

//}

function fillData_Absenteeism() {
 
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#DashBoard_TimeOffice_Absenteeism_Tbl").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';
                    debugger
                    var start = d.start;
                    var searchValue = $('.dataTableSearch2').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {                    
                    d.data[i].from = _GetFormattedDate(d.data[i].from);
                    //d.data[i].noOfDays = '<a data-bs-toggle="modal" data-target=""onclick="AbsentClickPopUp(' + d.data[i].employeeCode + ')"</a>' + d.data[i].noOfDays


                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "15%" },
           
            { "data": "noOfDays", "width": "15%" },
            { "data": "fromDate", "width": "15%" },
            { "data": "toDate", "width": "15%" },




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


    $(".dataTableSearch2").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {
        /* dataTable.search('').draw();*/
        $('.dataTableSearch2').val('');
        $('.dataTableSearch2').focus();
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

function fillData_pendingreg() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#PENDING_REG_TBL").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);

                $('#exampleModalLabelauthrzd').html("Unauthorized Absence from the Month of " + d.month);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].noOfDays = '<a onchange="DaysClickPopUp()">' + d.data[i].noOfDays + '</a>'
                    d.data[i].noOfDays = '<a data-bs-toggle="modal" data-target=""onclick="DaysClickPopUp(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + 'pendingreg' + "'" + ')"</a>' + d.data[i].noOfDays



                }


                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "designation", "width": "20%" },
            { "data": "department", "width": "19%" },
            { "data": "location", "width": "19%" },


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
        /* dataTable.search('').draw();*/
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
function fillData_AuthAndUnAuth() {
    
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#Auth_And_UnAuthorized_TBL").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },


            

            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);

                $('#exampleModalLabelauthrzd').html("Unauthorized Absence from the Month of " + d.month);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].noOfDays = '<a onchange="DaysClickPopUp()">' + d.data[i].noOfDays + '</a>'
                    d.data[i].noOfDays = '<a data-bs-toggle="modal" data-target=""onclick="DaysClickPopUp(' + "'" + d.data[i].employeeGI + "'" + ',' + "'"+'UN AUTHORISED LOP'+"'"+')"</a>' + d.data[i].noOfDays



                }


                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "designation", "width": "20%" },
            { "data": "department", "width": "19%" },
            { "data": "location", "width": "19%" },
           
            { "data": "noOfDays", "width": "12%" },




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
        /* dataTable.search('').draw();*/
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
function fillData_LopDetails() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#lopDetailsTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);

                $('#exampleModalLabelauthrzd').html("Unauthorized Absence from the Month of " + d.month);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].noOfDays = '<a onchange="DaysClickPopUp()">' + d.data[i].noOfDays + '</a>'
                    d.data[i].noOfDays = '<a data-bs-toggle="modal" data-target=""onclick="DaysClickPopUp(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + 'LOP' + "'" +')"</a>' + d.data[i].noOfDays



                }


                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "designation", "width": "20%" },
            { "data": "department", "width": "19%" },
            { "data": "location", "width": "19%" },

            { "data": "noOfDays", "width": "12%" },




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
        /* dataTable.search('').draw();*/
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
function fillData_UpcomingLeave() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#UpComing_LeaveDetails_PopUp").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('.dataTableSearch4').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {





                    d.data[i].dateofApplication = _GetFormattedDate(d.data[i].dateofApplication);
                    d.data[i].from = _GetFormattedDate(d.data[i].from);
                    d.data[i].to = _GetFormattedDate(d.data[i].to);
                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "12%" },
            { "data": "fullName", "width": "14%" },
            { "data": "type", "width": "10%" },

            { "data": "dateofApplication", "width": "14%" },
            { "data": "from", "width": "12%" },
            { "data": "to", "width": "12%" },
            { "data": "noOfDays", "width": "6%" },
            { "data": "applicationStatus", "width": "18%" },




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


    $(".dataTableSearch4").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {
        /* dataTable.search('').draw();*/
        $('.dataTableSearch4').val('');
        $('.dataTableSearch4').focus();
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
function fillData_LessHour() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#Less_Hour_Tbl").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {





                    d.data[i].dateofApplication = _GetFormattedDate(d.data[i].dateofApplication);
                   
                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "12%" },
            { "data": "fullName", "width": "14%" },
            { "data": "designation", "width": "15%" },

            { "data": "department", "width": "10%" },
            { "data": "location", "width": "15%" },
            { "data": "dateofApplication", "width": "12%" },
            { "data": "lessHours", "width": "12%" },
            { "data": "type", "width": "10%" },




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
        /* dataTable.search('').draw();*/
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
function fillData_DutyRoaster() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#Duty_Roaster_Tbl").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                console.log(d);
                debugger 
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {





                    d.data[i].dateofApplication = _GetFormattedDate(d.data[i].dateofApplication);

                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "12%" },
            { "data": "fullName", "width": "14%" },
            { "data": "designation", "width": "15%" },

            { "data": "department", "width": "10%" },
            { "data": "location", "width": "15%" },
            { "data": "dateofApplication", "width": "12%" },
            { "data": "assignedShift", "width": "12%" },
            { "data": "workedShift", "width": "10%" },




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
        /* dataTable.search('').draw();*/
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



function fillData_Maternityesileave() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();


    var dataTable = $("#DashBoard_TimeOffice_Maternityesileave_Tbl").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DashBoard/TimeOfficeDashBoard/GetDashboardPopUpList",

            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger
                $('#TimeOfficeDashBoardDetailsList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {





                    d.data[i].endDate = _GetFormattedDate(d.data[i].endDate);
                }
                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "30%" },
            { "data": "fullName", "width": "50%" },
            { "data": "endDate", "width": "20%" },
            



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
        /* dataTable.search('').draw();*/
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



// Dashboard

function GetWorkStatus(type) {


    $.ajax({

        type: 'Get',
        url: '/DashBoard/TimeOfficeDashBoard/GetWorkStatus',
        dataType: 'html',
        data: {type : type},
        success: function (result) {
            debugger;
            $('.wrkSt').html('');
            $('.wrkSt').html(result);

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

function GetTimeOfficeNotifications(type) {


    $.ajax({

        type: 'Get',
        url: '/DashBoard/TimeOfficeDashBoard/GetNotifications',
        dataType: 'html',
        data: { type: type },
        success: function (result) {
            debugger;
            $('.notif').html('');
            $('.notif').html(result);

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




function GetGraph() {
    debugger;
    var classification = $('#classification').val()
    $.ajax({
        type: 'GET',
        url: '/DashBoard/TimeOfficeDashBoard/GetGraph',
        dataType: 'html',
        data: { Classification: classification },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                //$('.lineGraph').removeClass('hide');
                $('.lineGraph').html('');
                $('.lineGraph').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Type = 'Duty Roster Variance';
    var SubType = null;


    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&SubType=' + SubType;
    var TableLength = $('#TimeOfficeDashBoardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DashBoard/TimeOfficeDashBoard/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelcontinousabsenteesim() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Type = 'ContinousAbsenteesim';
    var SubType = null;


    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&SubType=' + SubType;
    var TableLength = $('#TimeOfficeDashBoardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DashBoard/TimeOfficeDashBoard/ExportToExcelcontinousabsenteesim?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelupcomingleavedetails() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Type = 'Upcoming Leaves';
    var SubType = null;


    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&SubType=' + SubType;
    var TableLength = $('#TimeOfficeDashBoardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DashBoard/TimeOfficeDashBoard/ExportToExcelupcomingleavedetails?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelpendingreg() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Type = 'pendingreg';
    var SubType = null;


    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&SubType=' + SubType;
    var TableLength = $('#TimeOfficeDashBoardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DashBoard/TimeOfficeDashBoard/ExportToExcelpendingreg?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcelunauthorisedabsence() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Type = 'UN AUTHORISED LOP';
    var SubType = null;


    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&SubType=' + SubType;
    var TableLength = $('#TimeOfficeDashBoardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DashBoard/TimeOfficeDashBoard/ExportToExcelunauthorisedabsence?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ExportToExcellesshours() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var Type = 'LESSHOURS';
    var SubType = null;


    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&SubType=' + SubType;
    var TableLength = $('#TimeOfficeDashBoardDetailsList_Count').val();
    if (TableLength > 0) {
        url = '/DashBoard/TimeOfficeDashBoard/ExportToExcellesshours?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}