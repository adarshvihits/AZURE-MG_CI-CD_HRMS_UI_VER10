

var prevTab = 'All';
var currentTab = 'All';


function getList() {

    debugger;
    showSpinner();
    var TabIndex = currentTab;
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    $.ajax({

        type: 'POST',
        url: '/DataBank/CelebrityList/GetCelebrityList',
        dataType: 'html',
        data: { TabIndex: TabIndex, fromdate: fromdate, todate: todate },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#CelebritylistId').html("");
                $('#CelebritylistId').html(result);
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





function fillData() {

    debugger;
    // $.ajaxSetup({ async: false });
    var TabIndex = currentTab;
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();

    if (fromDate != '' && toDate != '') { 

    if (TabIndex == 'All') {
        debugger;
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        /*  $('#JoiningReportTable').html('');*/
        var dataTable = $("#AllCelebrityList").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/CelebrityList/GetList?TabIndex=" + currentTab,
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&dummy=dummy';

                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('.allCount').html(d.allCount);
                    $('.BdayTab').html(d.bdayCount);
                    $('.weddingtab').html(d.weddingcount);
                    $('.joiningTab').html(d.joiningcount);

                    $('#List_Count').val(d.recordsTotal);                 


                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].date = _GetFormattedDate(d.data[i].date);
                        if (d.data[i].profileFilename != "" && d.data[i].profileFilename != null) 
                            d.data[i].id = `<div class="d-flex align-items-center"><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div> <div class="name-post"><div>${d.data[i].employeeName}</div> <div>(${d.data[i].id})</div></div> </div>`;
                        else
                            d.data[i].id = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div>'+d.data[i].employeeName+'</div><div>('+d.data[i].id+')</div></div> </div>';

                                 

                    }
                    return(d.data);
                }





            },

            "columns": [

                { "data": "id", "width": " 14%" },
                { "data": "type", "width": "7%" },
                { "data": "date", "width": "8%" },
                { "data": "designation", "width": "10%" },
                { "data": "department", "width": "11%" },
                { "data": "location", "width": "8%" },
                { "data": "category", "width": "9%" },
                { "data": "mobile", "width": "8%" },
                { "data": "emailID", "width": "14%" },
                { "data": "remarks", "width": "11%" },

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


        $("#dataTableSearch").on("keyup search paste cut", function () {
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

    else if (TabIndex == 'Birthday') {

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        var tabIndex = "Birthday";

        var dataTable = $("#BirthdayListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/CelebrityList/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&TabIndex=' + tabIndex + '&dummy=dummy';

                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#List_Count').val(d.recordsTotal);

                    $('.allCount').html(d.allCount);
                    $('.BdayTab').html(d.bdayCount);
                    $('.weddingtab').html(d.weddingcount);
                    $('.joiningTab').html(d.joiningcount);



                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].date = _GetFormattedDate(d.data[i].date);
                        if (d.data[i].profileFilename != "" && d.data[i].profileFilename != null)
                            d.data[i].id = `<div class="d-flex align-items-center"><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div> <div class="name-post"><div>${d.data[i].employeeName}</div> <div>(${d.data[i].id})</div></div> </div>`;
                        else
                            d.data[i].id = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div>' + d.data[i].employeeName + '</div><div>(' + d.data[i].id + ')</div></div> </div>';

                    }

                    return d.data;
                }

            },


            "columns": [

                { "data": "id", "width": " 15%" },
                { "data": "date", "width": "8%" },
                { "data": "designation", "width": "12%" },
                { "data": "department", "width": "11%" },
                { "data": "location", "width": "9%" },
                { "data": "category", "width": "10%" },
                { "data": "mobile", "width": "9%" },
                { "data": "emailID", "width": "11%" },
                { "data": "remarks", "width": "10%" },

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
    else if (TabIndex == "Wedding") {

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        var tabIndex = "Wedding";

        var dataTable = $("#WeddingListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/CelebrityList/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&TabIndex=' + tabIndex + '&dummy=dummy';

                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    $('.allCount').html(d.allCount);
                    $('.BdayTab').html(d.bdayCount);
                    $('.weddingtab').html(d.weddingcount);
                    $('.joiningTab').html(d.joiningcount);


                    $('#List_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {                    

                        if (d.data[i].profileFilename != "" && d.data[i].profileFilename != null)
                            d.data[i].id = `<div class="d-flex align-items-center"><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div> <div class="name-post"><div>${d.data[i].employeeName}</div> <div>(${d.data[i].id})</div></div> </div>`;
                        else
                            d.data[i].id = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div>' + d.data[i].employeeName + '</div><div>(' + d.data[i].id + ')</div></div> </div>';
                        d.data[i].date = _GetFormattedDate(d.data[i].date);
                    }

                    return d.data;
                }

            },

            "columns": [


                { "data": "id", "width": " 15%" },
                { "data": "date", "width": "8%" },
                { "data": "designation", "width": "12%" },
                { "data": "department", "width": "11%" },
                { "data": "location", "width": "9%" },
                { "data": "category", "width": "10%" },
                { "data": "mobile", "width": "9%" },
                { "data": "emailID", "width": "11%" },
                { "data": "remarks", "width": "10%" },


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

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        var tabIndex = "Joining";

        var dataTable = $("#JoiningListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/CelebrityList/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&TabIndex=' + tabIndex + '&dummy=dummy';

                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('.allCount').html(d.allCount);
                    $('.BdayTab').html(d.bdayCount);
                    $('.weddingtab').html(d.weddingcount);
                    $('.joiningTab').html(d.joiningcount);


                    $('#List_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        
                        d.data[i].date = _GetFormattedDate(d.data[i].date);
                        if (d.data[i].profileFilename != "" && d.data[i].profileFilename != null)
                            d.data[i].id = `<div class="d-flex align-items-center"><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div> <div class="name-post"><div>${d.data[i].employeeName}</div> <div>(${d.data[i].id})</div></div> </div>`;
                        else
                            d.data[i].id = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div>' + d.data[i].employeeName + '</div><div>(' + d.data[i].id + ')</div></div> </div>';

                    }

                    return d.data;
                }

            },

            "columns": [



                { "data": "id", "width": " 15%" },
                { "data": "date", "width": "8%" },
                { "data": "designation", "width": "12%" },
                { "data": "department", "width": "11%" },
                { "data": "location", "width": "9%" },
                { "data": "category", "width": "10%" },
                { "data": "mobile", "width": "9%" },
                { "data": "emailID", "width": "11%" },
                { "data": "remarks", "width": "10%" },

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



        //}



    }

}

}

$.getJSON("/DataBank/CelebrityList/GetTabCount", {}, function (data) {
    debugger;

    console.log(data);
    if (data.length != 0) {
        debugger;
        $('.allCount').html(data.allCount);
        $('.BdayTab').html(data.birthdayCount);
        $('.weddingtab').html(data.weddingCount);
        $('.joiningTab').html(data.joiningCount);
    }
    else {
        debugger;
        $('.allCount').html(0);
    }


});

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();


    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
  
   
        $.ajax({

            type: 'POST',
            url: '/DataBank/CelebrityList/GetCelebrityList',
            dataType: 'html',
            data: { TabIndex: TabIndex, fromdate: fromdate, todate: todate },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#CelebritylistId').html("");
                    $('#CelebritylistId').html(result);
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
    

    prevTab = Tab;
  
    
}


function ApplyFilter(mode) {
    debugger
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    var TabIndex = currentTab;
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
      /*  CloseSlider('basicFilter');*/
    }
}




function ExportToExcel() {

    debugger;
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();

    var pendingTableLength = $('#List_Count').val();
  
    
        if (pendingTableLength > 0) {
            var url = "/DataBank/CelebrityList/ExportToExcel?TabIndex=" + currentTab + "&fromdate=" + fromdate + "&todate=" + todate + "&totalrow=" + pendingTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}