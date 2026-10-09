
function FillData() {

    debugger;
    if (validatefilter()) {
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        var dataTable = $("#PreFandFReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/PreFullandFinalSettlementReport/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&MONTH=' + $('#PayrollMonth_Code').val() +'&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {





                    $('#PreFullandFinalSettlementList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                    }

                    return d.data;
                }

            },

            "columns": [
            
                { "data": "employeeHeader.employeeId", "width": "20%" },
                { "data": "employeeHeader.employeeName", "width": "20%" },
                { "data": "employeeHeader.department.name", "width": "15%" },
                { "data": "employeeHeader.location.name", "width": "15%" },
                { "data": "resigDate", "width": "15%" },
                { "data": "lstWD", "width": "15%" },
                { "data": "payDays", "width": "15%" },
                { "data": "totalBasic", "width": "15%" },
                { "data": "earnedBasic", "width": "15%" },
                { "data": "held", "width": "15%" },
                { "data": "release", "width": "15%" },
                { "data": "remarks", "width": "15%" },
                { "data": "approvalstatus", "width": "15%" },
                { "data": "approvedBy", "width": "15%" },
                { "data": "approvedOn", "width": "15%" },
                {
                    "data": "banktransferstatus",
                    "width": "15%",
                    "render": function (data, type, row)
                    {
                        return data == 1 ? '✔' : '✘';
                    }
                },
                { "data": "transferdate", "width": "15%" }


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
            // dataTable.search('').draw();
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

function validatefilter() {
    debugger
    if (!validateById('#PayrollMonth_Code', 'monthF'))
        return false;
    else
        return true;

}



function ExportToExcel() {

    debugger;
    var Month = $('#PayrollMonth_Code').val();
   
    var MonthName = $('#PayrollMonth_Code option:selected').text();
   
   
    var tableLength = $('#PreFullandFinalSettlementList_Count').val();

    if (tableLength > 0) {
        url = "/PayRoll/PreFullandFinalSettlementReport/ExportToExcel?Month=" + Month + "&MonthName=" + MonthName + " &totRecords=" + tableLength +'&data=' + $('#BasicFilter').serialize();
            + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

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
