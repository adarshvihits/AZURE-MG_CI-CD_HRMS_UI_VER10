function fillData() {


    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


   
    From = $('#From').val();
    To = $('#To').val();
   



    var dataTable = $("#TimeSheetTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/TimeSheetDetailedReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + From +
                        '&F_To=' + To + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {





                console.log(d);
                $('#TimeSheetEntryList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].workDate = _GetFormattedDate(d.data[i].workDate);
                    d.data[i].approvalDate = _GetFormattedDate(d.data[i].approvalDate);

                    //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                    d.data[i].category = '<a data-bs-toggle="tooltip" title="Edit" id="physicallocationedit" type="button" class="btn btn-icon btn-edit icon-only slider-btn" onclick="onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"><i class="icon-xl fas fa-pencil-alt"></i></a>'

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "13%" },
            { "data": "designation", "width": "8%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "6%" },
            {"data": "workDate", "width": "10%" },
            { "data": "projectName", "width": "7%" },
            { "data": "activityName", "width": "7%" },
            { "data": "approvalBy", "width": "7%" },
            { "data": "approvalDate", "width": "7%" },
            {"data": "approvalStatus.name", "width": "6%" },
            { "data": "totalHours", "width": "8%" },


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

function ExportToExcel() {

    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();

    var tableLength = $('#TimeSheetEntryList_Count').val();
    if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        url = "/TimeOffice/TimeSheetDetailedReport/ExportToExcelDataset?From=" + fromDate + "&To=" + toDate + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

function ApplyFilter(mode) {
   
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}