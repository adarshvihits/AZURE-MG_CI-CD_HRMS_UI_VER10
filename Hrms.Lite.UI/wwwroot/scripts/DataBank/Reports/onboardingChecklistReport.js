function ValidateDate() {
    debugger;
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        return false;
    }
    else
        return true;
}


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var dataTable = $("#OnboardingChecklistTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/OnboardingChecklistReport/GetOnboardingChecklistReport",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FromDate=' + fromDate + '&ToDate=' + toDate + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {


                $('#OnboardingChecklistReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeName = '<td data-label="Employee Name"><a data-bs-toggle="modal"data-target=".empchecklistdetails" type="button"class="btn p-0 btn-sm float-left" onclick="OnEmpNameClick('+ d.data[i].employeeCode + ')">' + d.data[i].employeeName + '</a></td>'

                    //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div > <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                }
                console.log(d.data)
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "15%" },
            { "data": "category", "width": "10%" },
            { "data": "doj", "width": "10%" },



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
}


function ApplyFilter(mode) {
    debugger
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
        
        var filter = $('#BasicFilter').serialize();
       
        var fromDate = $('#fromDate').val();
        var toDate = $('#toDate').val();

        var PARAM = 'data=' + filter + '&fromDate=' + fromDate + '&toDate=' + toDate ;
        var TableLength = $('#OnboardingChecklistReportList_Count').val();
        if (TableLength > 0) {
            url = '/DataBank/OnboardingChecklistReport/ExportToExcel?' + PARAM + "&totRecords=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

function OnEmpNameClick(employeeCode) {

    debugger;
   
    $.ajax({
        type: 'GET',
        url: "/DataBank/OnboardingChecklistReport/GetDetails",
        data: { EmployeeCode: employeeCode }, 
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.empchecklistdetails');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);

            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
   
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("ChecklistDetailsTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var dataRowsCount = 0;
    var visibleRowsCount = 0;
    var spannedRows = 0;
    var show = true;

    for (i = 1; i < tr.length; i++) { // Start from 1 to skip the header row
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            dataRowsCount++;
            if (spannedRows > 0) {
                if (show)
                    tr[i].style.display = "";
                else {
                    tr[i].style.display = "none";
                    visibleRowsCount--;
                }
                spannedRows--;
            } else {
                tr[i].style.display = "none";
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                    visibleRowsCount++;
                }
                let rs = td.getAttribute("rowspan");
                if (rs && rs > 1) {
                    show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                    spannedRows = rs - 1;
                }
            }
        }
    }

    if (visibleRowsCount === 0) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='5'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }
 
}



function ItemDetailsExportToExcel() {
    debugger
   
    var employeecode = $('#EmployeeCode').val();
    var TableLength = $('#OnboardingChecklistDetails_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/OnboardingChecklistReport/ItemDetailsExportToExcel?EmployeeCode=" + employeecode ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

