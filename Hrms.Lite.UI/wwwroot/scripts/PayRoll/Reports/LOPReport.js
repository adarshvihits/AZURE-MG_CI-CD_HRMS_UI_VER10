
//function fillData() {

//    debugger;
//    var filter1 = $('#BasicFilter').serialize();
//    var Type = $("input[type='radio']:checked").val();
    
//    var temp = filter1;
//    var month = $('#Month').val();


//    if (Type == 0) {
//        $("#LOPListID").removeClass('hide');
//        $("#LOPAmtListID").addClass('hide');
//        var dataTable = $("#EsiSettingsTable").DataTable({

//            "pagingType": 'full_numbers',
//            "destroy": 'true',
//            "ajax": {
//                "url": "/PayRoll/LOPReport/GetLOPReportList",
//                "type": "POST",
//                "datatype": "json",
//                'data':

//                    function (d) {
//                        temp = '';
//                        var start = d.start;
//                        var searchValue = $('#dataTableSearch').val();
//                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&Type=' + Type +'&dummy=dummy';
//                        d.filter = temp;
//                        console.log("Payload sent to server:", d);
//                        return JSON.stringify(d);
//                    },

//                "dataSrc": function (d) {
//                    $('#LOPReportList_Count').val(d.recordsTotal);
                    

//                    return d.data;
//                }

//            },

//            "columns": [

//                { "data": "employeeID" },
//                { "data": "employeeName" },
//                { "data": "department" },
//                { "data": "designation" },
//                { "data": "location" },
//                { "data": "lopAbsent" },
//                { "data": "esiLeave" },
//                { "data": "ljp" },
//                { "data": "ltp" },
//                { "data": "prevLOP" },
//                { "data": "totCutDays" },
//                { "data": "suspension" },
//                { "data": "layoff" },
//                { "data": "latehrs" },
//                { "data": "totCutAmt" },

//            ],
//            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

//            "serverSide": "true",
//            "scrollY": '52vh',
//            "sScrollX": "100%",
//            "sScrollXInner": "100%",

//            "order": [0, "asc"],
//            "processing": "true",
//            "language": {

//                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
//                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//                sLengthMenu: "_MENU_",
//                paginate: {


//                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                    first: '<img src="/images/icons/Icon metro-next-1.png">',
//                    last: '<img src="/images/icons/Icon metro-next.png">'
//                }
//            }



//        });
//    }
//    //else {
        
//    //    $("#LOPListID").addClass('hide');
//    //    $("#LOPAmtListID").removeClass('hide');
//    //    var dataTable = $("#LopReportTable").DataTable({

//    //        "pagingType": 'full_numbers',
//    //        "destroy": 'true',
//    //        "ajax": {
//    //            "url": "/PayRoll/LOPReport/GetLOPReportList",
//    //            "type": "POST",
//    //            "datatype": "json",
//    //            'data':
//    //                function (d) {
//    //                    temp = '';
//    //                    var start = d.start;
//    //                    var searchValue = $('#dataTableSearch').val();
//    //                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&dummy=dummy';
//    //                    d.filter = temp;
//    //                    console.log("Payload sent to server:", d);
//    //                    return JSON.stringify(d);

//    //                },
//    //            "dataSrc": function (d) {
//    //                $('#LOPReportList_Count').val(d.recordsTotal);


//    //                return d.data;
//    //            }

//    //        },

//    //        "columns": [

//    //            { "data": "employeeID" },
//    //            { "data": "employeeName" },
//    //            { "data": "department" },
//    //            { "data": "designation" },
//    //            { "data": "location" },
//    //            { "data": "lopAbsent" },
//    //            { "data": "esiLeave" },
//    //            { "data": "ljp" },
//    //            { "data": "ltp" },
//    //            { "data": "prevLOP" },
//    //            { "data": "totCutDays" },
//    //            { "data": "suspension" },
//    //            { "data": "layoff" },
//    //            { "data": "latehrs" },
//    //            { "data": "totCutAmt" },
//    //            { "data": "lopAbsentAmount" },
//    //            { "data": "esiLeaveAmount" },
//    //            { "data": "ljpAmount" },
//    //            { "data": "ltpAmount" },
//    //            { "data": "previousLopAmount" },
//    //            { "data": "totDaysToCutAmount" },
//    //            { "data": "suspensionAmount" },
//    //            { "data": "layOffAmount" },
//    //            { "data": "lessHoursAmount" },

//    //        ],
//    //        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

//    //        "serverSide": "true",
//    //        "scrollY": '52vh',
//    //        "sScrollX": "100%",
//    //        "sScrollXInner": "100%",

//    //        "order": [0, "asc"],
//    //        "processing": "true",
//    //        "language": {

//    //            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
//    //            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//    //            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

//    //            sLengthMenu: "_MENU_",
//    //            paginate: {


//    //                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//    //                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//    //                first: '<img src="/images/icons/Icon metro-next-1.png">',
//    //                last: '<img src="/images/icons/Icon metro-next.png">'

//    //            }
//    //        }

//    //    });
//    //}

//    else {
//        $("#LOPListID").addClass('hide');
//        $("#LOPAmtListID").removeClass('hide');

//        var dataTable = $("#LopReportTable").DataTable({
//            "pagingType": 'full_numbers',
//            "destroy": 'true',
//            "ajax": {
//                "url": "/PayRoll/LOPReport/GetLOPReportList",
//                "type": "POST",
//                "contentType": "application/json", // Ensure the payload is sent as JSON
//                "datatype": "json",
//                'data': function (d) {
//                    debugger;

//                    var requestData = {
//                        filter: filter1,        // Send the filter as an object
//                        Type: Type,             // Include the Type
//                        month: month,           // Include the month
//                        searchValue: $('#dataTableSearch').val()// Include the search value
//                          // Include the length value
//                    };
//                    // Prepare custom parameters
//                    //d.filter = filter1; // Send filter1 as part of the request
//                    //d.Type = Type; // Include the Type
//                    //d.month = month; // Include the month
//                    //d.searchValue = $('#dataTableSearch').val(); // Include the search value
//                    console.log("LOP sent to server:", requestData);
//                    return JSON.stringify(requestData); // Send data as JSON string
//                },
//                "dataSrc": function (d) {
//                    $('#LOPReportList_Count').val(d.recordsTotal); // Update count
//                    return d.data;
//                }
//            },
//            "columns": [
//                { "data": "employeeID" },
//                { "data": "employeeName" },
//                { "data": "department" },
//                { "data": "designation" },
//                { "data": "location" },
//                { "data": "lopAbsent" },
//                { "data": "esiLeave" },
//                { "data": "ljp" },
//                { "data": "ltp" },
//                { "data": "prevLOP" },
//                { "data": "totCutDays" },
//                { "data": "suspension" },
//                { "data": "layoff" },
//                { "data": "latehrs" },
//                { "data": "totCutAmt" },
//                { "data": "lopAbsentAmount" },
//                { "data": "esiLeaveAmount" },
//                { "data": "ljpAmount" },
//                { "data": "ltpAmount" },
//                { "data": "previousLopAmount" },
//                { "data": "totDaysToCutAmount" },
//                { "data": "suspensionAmount" },
//                { "data": "layOffAmount" },
//                { "data": "lessHoursAmount" }
//            ],
//            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
//            "serverSide": "true",
//            "scrollY": '52vh',
//            "sScrollX": "100%",
//            "sScrollXInner": "100%",
//            "order": [0, "asc"],
//            "processing": "true",
//            "language": {
//                processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
//                emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div>',
//                zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div>',
//                sLengthMenu: "_MENU_",
//                paginate: {
//                    next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
//                    previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
//                    first: '<img src="/images/icons/Icon metro-next-1.png">',
//                    last: '<img src="/images/icons/Icon metro-next.png">'
//                }
//            }
//        });
//    }


//    $('.ihits-table table').removeClass("dataTable no-footer");


//    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.next').attr("title", "Next");
//    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.previous').attr("title", "Previous");
//    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.first').attr("title", "First");
//    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//    $('.paginate_button.last').attr("title", "Last");


//    $("#dataTableSearch").on("keyup search input paste cut", function () {
//        dataTable.search(this.value).draw();

//        $('#noDataFound').css("display", 'none');
//    });

//    $('.search-slider-inner').click(function () {

//        $('#dataTableSearch').val('');
//        $('#dataTableSearch').focus();
//        /*  dataTable.search('').draw();*/
//    });

//    dataTable.on('draw', function () {
//        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.next').attr("title", "Next");
//        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.previous').attr("title", "Previous");
//        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.first').attr("title", "First");
//        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
//        $('.paginate_button.last').attr("title", "Last");

//    });

//    $.fn.DataTable.ext.pager.numbers_length = 5;


//}



function GetList() {
    showSpinner();
    debugger;
    
    
    var month = $('#Month').val();
    var Type = $("input[type='radio']:checked").val();
    var filter = $("#BasicFilter").serialize();
    if (Type == 0) {
      
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LOPReport/GetList',
            dataType: 'html',
            data: { Month: month, Type: Type },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#LOPListID').html("");
                    $('#LOPListID').html(result);
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
    else {
        showSpinner();
        $.ajax({

            type: 'GET',
            url: '/PayRoll/LOPReport/GetList',
            dataType: 'html',
            data: { Month: month, Type: Type },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#LOPListID').html("");
                    $('#LOPListID').html(result);
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




function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            fillData();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            fillData();
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
    var month = $('#Month').val();
    var monthName = $('#Month option:selected').text();
    var LOPReportListLength = $('#LOPReportList_Count').val();
    var Type = $("input[type='radio']:checked").val();
    if (LOPReportListLength > 0) {
        url = "/PayRoll/LOPReport/ExportToExcel?Month=" + month + "&monthName=" + monthName + "&Type=" + Type + "&totalrows=" + LOPReportListLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}