


function onDateClick() {
    debugger;
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2)
    {
        swal.fire("To date should be greater than From date");
        $("#ToDate").val('');


        
        return false;
    }
    else
        return true;
}

function validateDates() {
    debugger;
    var i = 0;
    if (!validateById('#FromDate', 'fromdate')) {
        i++;
    }
    if (!validateById('#ToDate', 'todate')) {
        i++;
    }
    if (!validateById('#Type_Code', 'type')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}



function GetFillData() {
    debugger;

    var Type = $('#Type_Code').val();
   

  /*  if (validateDates()) {*/
    
        if (Type == 1 || Type == 2) {
            var element = document.getElementById("searchID");
            element.style.display = "none";
            var element = document.getElementById("filterForm");
            element.style.display = "none";
        }
        else if (Type == 3 || Type == 4) {
            var element = document.getElementById("searchID");
            element.style.display = "block";
            var element = document.getElementById("filterForm");
            element.style.display = "block";
        }


        var url = '/TimeOffice/AbsenteeismReport/GetFillDataList?TypeCode=' + Type;
        $('#ListID').load(url, function () { });
    //}
    //else
    //    return false;
}


function fillDataList() {

    debugger;
 
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#FromDate').val();
        var Todate = $('#ToDate').val();
        var Type = $('#Type_Code').val();


        if (Type == 1) {
            var dataTable = $("#DatewiseStatusTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/TimeOffice/AbsenteeismReport/GetAbsenteeismList",

                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromDate=' + fromDate + '&Todate=' + Todate + '&Type=' + Type + '&dummy=dummy';

                            d = temp;
                            return JSON.stringify(d);
                        },




                    "dataSrc": function (d) {

                        $('#AttendanceStatus_Count').val(d.recordsTotal);

                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            d.data[i].date = _GetFormattedDate(d.data[i].date);

                            debugger;

                            debugger;
                            //d.data[i].ab = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ', ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].ab + '</a>'


                            d.data[i].ab = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ', ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].ab +''+''+ '</a>'



                            debugger

                        }
                        console.log(d.data);
                        return d.data;
                    }

                },




                "columns": [


                    { "data": "date", "width": "10%" },
                    { "data": "actualManPower", "width": "9%" },
                    { "data": "phyPR", "width": "9%" },
                    { "data": "od", "width": "7%" },
                    { "data": "ra", "width": "7%" },
                    { "data": "leave", "width": "7%" },
                    { "data": "ml", "width": "7%" },
                    { "data": "esi", "width": "7%" },
                    { "data": "offDay", "width": "7%" },
                    { "data": "holiday", "width": "7%" },
                    { "data": "lop", "width": "7%" },
                    { "data": "ab", "width": "7%" },
                    { "data": "aB_PER", "width": "9%" },




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

        else if (Type == 2) {

            var dataTable = $("#DateWiseReasonTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/TimeOffice/AbsenteeismReport/GetAbsenteeismList",

                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromDate=' + fromDate + '&Todate=' + Todate + '&Type=' + Type + '&dummy=dummy';

                            d = temp;
                            return JSON.stringify(d);
                        },




                    "dataSrc": function (d) {

                        $('#ReasonWise_Count').val(d.recordsTotal);

                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            d.data[i].date = _GetFormattedDate(d.data[i].date);


                            d.data[i].lin = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'"+',' + "'" + d.data[i].lin + "'" + ',1,' + ')">' + d.data[i].lin + '</a>'
                            d.data[i].eout = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].eout + "'" + ',2, '+ ')">' + d.data[i].eout + '</a>'
                            d.data[i].misIn = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' +"'" + d.data[i].misIn + "'" + ',3, '  + ')">' + d.data[i].misIn + '</a>'
                            d.data[i].misOut = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].misOut + "'" + ',4, '  + ')">' + d.data[i].misOut + '</a>'
                            d.data[i].misInOut = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].misInOut + "'" + ',5, '  + ')">' + d.data[i].misInOut + '</a>'
                            d.data[i].cidMis = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].cidMis + "'" + ',6, ' + ')">' + d.data[i].cidMis + '</a>'
                            d.data[i].shiftMis = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].shiftMis + "'" + ',7, '  + ')">' + d.data[i].shiftMis + '</a>'
                            d.data[i].shiftNotAllocated = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'"+ ','  + "'" + d.data[i].shiftNotAllocated + "'" + ',8, ' + ')">' + d.data[i].shiftNotAllocated + '</a>'
                            d.data[i].totHrsNotAchieved = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].totHrsNotAchieved + "'" + ',9, '  + ')">' + d.data[i].totHrsNotAchieved + '</a>'
                            d.data[i].lap = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="fillData(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].lap + "'" + ',10, '  + ')">' + d.data[i].lap + '</a>'


                         






                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "date", "width": "10%" },
                    { "data": "lin", "width": "6%" },
                    { "data": "eout", "width": "7%" },
                    { "data": "misIn", "width": "8%" },
                    { "data": "misOut", "width": "8%" },
                    { "data": "misInOut", "width": "10%" },
                    { "data": "cidMis", "width": "8%" },
                    { "data": "shiftMis", "width": "9%" },
                    { "data": "shiftNotAllocated", "width": "9%" },
                    { "data": "totHrsNotAchieved", "width": "14%" },
                    { "data": "lap", "width": "9%" },

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

        else if (Type == 3) {
            debugger;
            var dataTable = $("#EmpStatusTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/TimeOffice/AbsenteeismReport/GetAbsenteeismList",

                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromDate=' + fromDate + '&Todate=' + Todate + '&Type=' + Type + '&dummy=dummy';

                            d = temp;
                            return JSON.stringify(d);
                        },




                    "dataSrc": function (d) {

                        $('#AttendanceStatus_Count').val(d.recordsTotal);

                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            d.data[i].date = _GetFormattedDate(d.data[i].date);

                            debugger;

                            debugger;
                            //d.data[i].ab = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ', ' + "'" + d.data[i].employeeGI + "'" +','+null+','+null+')">' + d.data[i].ab + '</a>'

                            d.data[i].ab = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ', ' + null + ',' + null+','+ "'" + d.data[i].employeeGI + "'"  + ')">' + d.data[i].ab + '</a>'




                            debugger


                        }

                        return d.data;
                    }

                },




                "columns": [



                    { "data": "employeeID", "width": "9%" },
                    { "data": "employeeName", "width": "9%" },
                    { "data": "actualManPower", "width": "8%" },
                    { "data": "phyPR", "width": "7%" },
                    { "data": "od", "width": "7%" },
                    { "data": "ra", "width": "7%" },
                    { "data": "leave", "width": "7%" },
                    { "data": "ml", "width": "7%" },
                    { "data": "esi", "width": "7%" },
                    { "data": "offDay", "width": "6%" },
                    { "data": "holiday", "width": "6%" },
                    { "data": "lop", "width": "6%" },
                    { "data": "ab", "width": "7%" },
                    { "data": "aB_PER", "width": "7%" },




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

        else {
            var dataTable = $("#EmpReasonTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/TimeOffice/AbsenteeismReport/GetAbsenteeismList",

                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromDate=' + fromDate + '&Todate=' + Todate + '&Type=' + Type + '&dummy=dummy';

                            d = temp;
                            return JSON.stringify(d);
                        },




                    "dataSrc": function (d) {

                        $('#ReasonWise_Count').val(d.recordsTotal);

                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            d.data[i].date = _GetFormattedDate(d.data[i].date);




                            d.data[i].lin = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].lin + "'" + ',1, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].lin + '</a>'
                            d.data[i].eout = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].eout + "'" + ',2, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].eout + '</a>'
                            d.data[i].misIn = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].misIn + "'" + ',3, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].misIn + '</a>'
                            d.data[i].misOut = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].misOut + "'" + ',4, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].misOut + '</a>'
                            d.data[i].misInOut = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].misInOut + "'" + ',5, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].misInOut + '</a>'
                            d.data[i].cidMis = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].cidMis + "'" + ',6, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].cidMis + '</a>'
                            d.data[i].shiftMis = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].shiftMis + "'" + ',7, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].shiftMis + '</a>'
                            d.data[i].shiftNotAllocated = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].shiftNotAllocated + "'" + ',8, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].shiftNotAllocated + '</a>'
                            d.data[i].totHrsNotAchieved = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].totHrsNotAchieved + "'" + ',9, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].totHrsNotAchieved + '</a>'
                            d.data[i].lap = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg1" onclick="FillData1(' + "'" + d.data[i].date + "'" + ',' + "'" + d.data[i].lap + "'" + ',10, ' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].lap + '</a>'






                        }

                        return d.data;
                    }

                },

                "columns": [
                    { "data": "employeeID", "width": "12%" },
                    { "data": "employeeName", "width": "10%" },
                    { "data": "lin", "width": "6%" },
                    { "data": "eout", "width": "7%" },
                    { "data": "misIn", "width": "8%" },
                    { "data": "misOut", "width": "8%" },
                    { "data": "misInOut", "width": "10%" },
                    { "data": "cidMis", "width": "8%" },
                    { "data": "shiftMis", "width": "9%" },
                    { "data": "shiftNotAllocated", "width": "9%" },
                    { "data": "totHrsNotAchieved", "width": "14%" },
                    { "data": "lap", "width": "9%" },

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
    
}
















function getList() {
    debugger
    var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var Type = $('#Type_Code').val();
   

    if (fromDate !== '' && Todate !== '' && Type !== '') {
        showSpinner();
        //var url = "/TimeOffice/AbsenteeismReport/GetList"
        //$('#ListID').load(url, { fromDate: fromDate, Todate: Todate, Type: Type}, function () { hideSpinner(); });
        $.ajax({

            type: 'GET',
            url: '/TimeOffice/AbsenteeismReport/GetList',
            dataType: 'html',
            data: { fromDate: fromDate, Todate: Todate, Type: Type},

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ListID').html("");
                    $('#ListID').html(result);
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

        return true;
    }
    else {

        return false;
    }

}


function FillData1(date, Value,column,EmployeeGI) {
    debugger;
    var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var type = $('#Type_Code').val();
    var Filter = $('#BasicFilter').serialize();

    var temp = Filter;


    var dataTable = $("#ReasonTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/AbsenteeismReport/GetEmpPopUpList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearchPopUp').val();


                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&date=' + date + '&Value=' + Value + '&column=' + column + '&EmployeeGI=' + EmployeeGI + '&fromDate=' + fromDate + '&Todate=' + Todate + '&type=' + type + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                debugger
                $('#Details_Count').val(d.recordsTotal);




                //if (type == 3) {
                //    $("#DatePopReason").css("display", "none");
                //}
                //else if (type == 4) {
                //    $("#DatePopReason").css("display", "block");
                //}
                debugger
                $('#popupHeading').html(d.popupHead);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].date = _GetFormattedDate(d.data[i].date);
                }
                return d.data;
            }

        },

        "columns": [

            { "data": "slNo", "width": "10%"  },
            { "data": "date", "width": "10%" },
            { "data": "shift", "width": "15%" },
            { "data": "in", "width": "10%"  },
            { "data": "out", "width": "10%"  },
            { "data": "attnStatus", "width": "24%"  },
            { "data": "reason", "width": "21%"  },
      
                                                        

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


    $("#dataTableSearchPopUp").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });

    $('.search-slider-inner2').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearchPopUp').val('');
        $('#dataTableSearchPopUp').focus();
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

function fillData(date, EmployeeGI, Value, column) {
    debugger;


    var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var Type = $('#Type_Code').val();
    var Filter = $('#BasicFilter').serialize();

    var temp = Filter;


    var dataTable = $("#StatusTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/AbsenteeismReport/GetDatePopupList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearchPopUp').val();


                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&date=' + date + '&EmployeeGI=' + EmployeeGI + '&fromDate=' + fromDate + '&Todate=' + Todate + '&Value=' + Value + '&column=' + column+ '&Type=' + Type + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {
                debugger
                $('#Details_Count').val(d.recordsTotal);


                //if (Type == 1) {
                //    $("#DatePopReason").css("display", "none");
                    
                //}
                //else if (Type == 2) {
                //    $("#DatePopReason").css("display", "block");
                //}
                $('#exampleModalLabel12').html(d.popupHead);
                //for (var i = 0, ien = d.data.length; i < ien; i++) {
                  

                //}
                return d.data;
            }

        },
        "columns": [

            { "data": "slNo","width":"10%" },
            { "data": "employeeID", "width": "15%" },
            { "data": "employeeName", "width": "18%" },
            { "data": "shift", "width": "15%"},
            { "data": "in", "width": "10%" },
            { "data": "out", "width": "10%"},
            { "data": "attnStatus", "width": "12%" },
            { "data": "reason", "width": "15%" },


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


    $("#dataTableSearchPopUp").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');
    });

    $('.search-slider-inner2').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearchPopUp').val('');
        $('#dataTableSearchPopUp').focus();
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

function GetStatusPopUp(date, EmployeeGI) {
    debugger
    var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var Type = $('#Type_Code').val();
    //var type = $('#Type_Code option:selected').text();


    $.ajax({
        type: 'GET',
        url: "/TimeOffice/AbsenteeismReport/GetStatusPopUp?date=" + date + "&EmployeeGI=" + EmployeeGI + "&fromDate=" + fromDate + "&Todate=" + Todate + "&Type=" + Type,
        dataType: 'html',

        success: function (Data) {
            debugger;
            console.log(Data);
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            debugger;
            m.find('.modal-body').html(Data);
            //$("#myDiv").css("display", "block");

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}

function GetReasonPopUp(date, Value, column, EmployeeGI) {
    debugger
    var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var type = $('#Type_Code').val();
    $.ajax({
        type: 'GET',
        url: "/TimeOffice/AbsenteeismReport/GetReasonPopUp?date=" + date + "&Value=" + Value + "&column=" + column + "&EmployeeGI=" + EmployeeGI + "&fromDate=" + fromDate + "&Todate=" + Todate + "&type=" + type,
        dataType: 'html',

        success: function (Data) {
            debugger;
            console.log(Data);
            var m = $('.bd-example-modal-lg1');
            m.find('.modal-body1').html('');
            debugger;
            m.find('.modal-body1').html(Data);
            $("#myDiv1").css("display", "block");

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}


function ExportToExcel() {
    debugger;
   var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var Type = $('#Type_Code').val();
    var sel = $('#Type_Code option:selected').text();
    if (Type == 1 || Type == 3) {

        var leaveOpeningTableLength = $('#AttendanceStatus_Count').val();
        if (leaveOpeningTableLength > 0) {
            url = "/TimeOffice/AbsenteeismReport/ExportToExcel?fromDate=" + fromDate + "&Todate=" + Todate + "&Type=" + Type + "&selectedVal=" + sel + "&TotalRows=" + leaveOpeningTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {

        var leaveOpeningTableLength = $('#ReasonWise_Count').val();
        if (leaveOpeningTableLength > 0) {
            url = "/TimeOffice/AbsenteeismReport/ExportToExcel?fromDate=" + fromDate + "&Todate=" + Todate + "&Type=" + Type + "&selectedVal=" + sel + "&TotalRows=" + leaveOpeningTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

function ApplyFilter(mode) {
    debugger;
    var fromDate = $('#FromDate').val();
    var Todate = $('#ToDate').val();
    var Type = $('#Type_Code').val();




    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            GetFillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetFillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetFillData();
       /* CloseSlider('basicFilter');*/
    }
}





function ExportToexcelPopUP() {
    debugger;
   

    var TableLength = $('#Details_Count').val();
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    var Type = $('#Type_Code').val();
    var sel = $('#Type_Code option:selected').text();


    //if (Type == 1 || Type == 3) {
    //    var TableLength = $('#Details_Count').val();
    //}
    if (TableLength > 0)
    {
        url = "/TimeOffice/AbsenteeismReport/ExportToexcelPopUp?fromDate=" + fromdate + "&Todate=" + todate + "&Type=" + Type + "&selectedVal=" + sel + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
        else {
            swal.fire("No data found to export");
        }
    
}