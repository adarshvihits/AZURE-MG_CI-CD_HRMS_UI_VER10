function ValidateDate() {
    debugger;
    var fmonth = $('#fMonth').val();
    var tmonth = $('#tMonth').val();

    var D1 = new Date(fmonth);
    var D2 = new Date(tmonth);
    if (D1 > D2) {
        swal.fire("To Month should be greater than From Month");
        /*  $("#To").val('');*/



        return false;
    }
    else
        return true;
}




function validateAll() {
    debugger
    var i = 0;

    var fmonth = $('#fMonth').val();
    var tmonth = $('#tMonth').val();

    if (!validateById('#fMonth', 'fMonth')) {
        i++;
    }
    if (!validateById('#tMonth', 'tmonth')) {
        i++;
    }
    debugger

    if (i == 0)
        return true;
    else
        return false;
}



function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fmonth = $('#fMonth').val();
    var tmonth = $('#tMonth').val();
    //var Location = $('#Location').val();
    //if (fmonth != '' && tmonth != '') {


    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#LOPPeriodwiseTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/LOPPeriodwiseRpt/GetLOPPeriodwiseList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fmonth=' + fmonth + '&tmonth=' + tmonth + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {





                //if (d.data.length != 0) {
                //    $('.alltab').html(d.data[0].active);
                //    $('.assgnd').html(d.data[0].ASSIGNED);
                //    $('.notassgnd').html(d.data[0].NOTASSIGNED);
                //}
                //else {
                //    $('.alltab').html(0);
                //}
                $('#LOPPeriodwiseList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].dateofPunch = _GetFormattedDate(d.data[i].dateofPunch);
                    //d.data[i].time = _GetFormattedTime(d.time[i].doj);

                    //d.data[i].employeeid = '<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + '</a>'+ d.data[i].employeeid+'</div>'

                    //if (d.data[i].absoluteUri != '')
                    //    d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                    //else
                    //    d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div > <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'


                }

                return d.data;
            }

        },

        "columns": [



            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "department" },
            { "data": "designation" },
            { "data": "location" },
            { "data": "lopAbsent" },
            { "data": "esiLeave" },
            { "data": "prevLOP" },
            { "data": "prevArrear" },
            { "data": "totCutDays" },
            { "data": "latehrs" },
            { "data": "totCutAmt" },


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
        dataTable.search('').draw();
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

    /*}*/


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
    var fmonth = $('#fMonth').val();
    var tmonth = $('#tMonth').val();
    var fmonthName = $('#fMonth option:selected').text();
    var TmonthName = $('#tMonth option:selected').text();
    var LOPPeriodwiseLength = $('#LOPPeriodwiseList_Count').val();

    if (LOPPeriodwiseLength > 0) {
        url = "/PayRoll/LOPPeriodwiseRpt/ExportToExcel?fmonth=" + fmonth + "&tmonth=" + tmonth + "&fmonthName=" + fmonthName + "&TmonthName=" + TmonthName + "&totalrows=" + LOPPeriodwiseLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}