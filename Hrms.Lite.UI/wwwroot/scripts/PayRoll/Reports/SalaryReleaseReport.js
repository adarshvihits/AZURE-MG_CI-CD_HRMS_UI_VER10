function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1; 
    var month = $('#Month').val();
    var payRollGrp = $('#PayrollGroup_Code').val();

    if (month != '' && payRollGrp != '') {
        var dataTable = $("#SalaryReleaseReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/SalaryReleaseReport/GetSalaryReleaseReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&payrollGrp=' + payRollGrp + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {






                    $('#SalaryReleaseReportList_Count').val(d.recordsTotal);


                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "location" },
                { "data": "designation" },
                { "data": "department" },
                { "data": "doj" },
                { "data": "holdingAmt" },
                { "data": "releaseAmount" },
                { "data": "recoveryAmt" },
               
             
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
            /*  dataTable.search('').draw();*/
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
    var PayrollGrp = $('#PayrollGroup_Code').val();
    var PayrollGrpName = $('#PayrollGroup_Code option:selected').text();
    var monthName = $('#Month option:selected').text();
    var SalaryReleaseReportListLength = $('#SalaryReleaseReportList_Count').val();

    if (SalaryReleaseReportListLength > 0) {
        url = "/PayRoll/SalaryReleaseReport/ExportToexcel?Month=" + month + "&PayrollGrp=" + PayrollGrp + "&PayrollGrpName=" + PayrollGrpName + "&monthName=" + monthName + "&totalrows=" + SalaryReleaseReportListLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}