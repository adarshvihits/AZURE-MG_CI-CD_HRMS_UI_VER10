


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
   
    var Month = $('#Month_Code').val();

    var dataTable = $("#Monthly_Attn_Summary_Report_Table_ID").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/MonthlyAttendanceSummaryReport/GetMonthlyAttendanceSummaryReportList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Month=' + Month + '&dummy=dummy';
                         
                    d = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



                $('#MonthlyAttendanceProcessing_Data_Count').val(d.recordsTotal);


                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID","width":"6%" },
            { "data": "fullName","width": "9%" },
            { "data": "presentDays","width": "4%"  },
            { "data": "ra","width": "4%"  },
            { "data": "absent","width":"4%" },
            { "data": "lop","width":"4%"  },
            { "data": "esiLeave","width":"4%"  },
            { "data": "leave","width":"4%" },
            { "data": "coff","width":"4%"  },
            { "data": "onDuty","width":"4%"  },
            { "data": "wfh","width":"4%"  },
            { "data": "holiDay","width":"4%" },
            { "data": "holidayPresent","width":"4%"  },
            { "data": "offDay","width":"4%"  },
            { "data": "offdayPresent","width":"4%" },
            { "data": "ljp","width":"4%" },
            { "data": "ltp","width":"4%" },
            { "data": "previousLOP","width":"4%"  },
            { "data": "previousArrear","width":"4%"  },
            { "data": "suspension","width":"4%"  },
            { "data": "totalLOP","width":"4%"  },
            { "data": "paidDays","width":"4%"  },




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
        //dataTable.search('').draw();
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

   // setTimeout(tooltipHover, 500);






}




function ApplyFilter(mode) {
    debugger;

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


    var Month = $("#Month_Code").val();
    var monthName = $("#Month_Code option:selected").text();


    var TableLength = $('#MonthlyAttendanceProcessing_Data_Count').val();
    if (TableLength > 0) {
        url = "/TimeOffice/MonthlyAttendanceSummaryReport/ExportToExcel?totRecords=" + TableLength + "&Month=" + Month + "&Mname=" + monthName;

        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}