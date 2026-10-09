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
        CloseSlider('basicFilter');
    }
}


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var Status = $('#Status_Code').val();




        /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#TaskReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/TaskReport/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Status=' + Status + '&dummy=dummy';



                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    $('#EmployeeTaskReportList_Count').val(d.recordsTotal);
                    debugger;
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "assignedToEmployee" },
                { "data": "department" },
                { "data": "designation" },
                { "data": "task" },
                { "data": "expectedCompletetionDate" },
                { "data": "givenBy" },
                { "data": "actionCount" },
                { "data": "taskStatus" },

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
        debugger
        $.fn.DataTable.ext.pager.numbers_length = 5;
        






}

function ExportToExcel() {
    debugger;

    var TaskReportList = $('#EmployeeTaskReportList_Count').val();
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var Status = $('#Status_Code').val();
    var statusRemarks = '';
    if (Status == '' || Status == null) {
        statusRemarks = 'ALL';
    }
    else if (Status == 1) {
        statusRemarks = 'Acceptance Pending';
    }
    else if (Status == 2) {
        statusRemarks = 'Accepted';
    }

    else if (Status == 3) {
        statusRemarks = 'In Progress';
    }

    else if (Status == 4) {
        statusRemarks = 'Completed';
    }

    else if (Status == 5) {
        statusRemarks = 'Rejected';
    }
    else
    {
        statusRemarks = 'OverDue';
    }


    if (TaskReportList > 0) {
        url = "/DataBank/TaskReport/ExportToExcel?TotalRows=" + TaskReportList + "&Status=" + Status + "&data=" + temp + "&statusRemarks=" + statusRemarks;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}
