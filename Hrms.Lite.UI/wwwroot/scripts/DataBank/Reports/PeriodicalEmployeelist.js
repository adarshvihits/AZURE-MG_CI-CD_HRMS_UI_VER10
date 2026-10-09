var salVisible;

function fillData() {

    debugger;
    var filter = $('#BasicFilter').serialize();


    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
   

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#PeriodicalEmployeelistTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/PeriodicalEmployeelist/GetPeriodicalEmployeelistDetails",
            "type": "POST",


            "datatype": "json",


            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FromDate=' + FromDate + '&ToDate=' + ToDate  +  '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#PeriodicalEmployeelistDetails_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


                }

                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "14%" },
            { "data": "doj", "width": "7%" },
            { "data": "yearOfService", "width": "6%" },
            { "data": "lwd", "width": "6%" },
            { "data": "grossPay", "width": "7%" },
            { "data": "ctc", "width": "7%" },



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
    debugger
    if (salVisible == 0) {
        let column8 = dataTable.column(8);
        column8.visible(false);
        let column9 = dataTable.column(9);
        column9.visible(false);
    }

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

}



//function ExportToExcel() {

//    debugger;

//    var FromDate = $('#FromDate').val();
//    var ToDate = $('#ToDate').val();
  
   

//    var pendingTableLength = $('#PeriodicalEmployeelistDetails_Count').val();

//    if (pendingTableLength > 0) {
//        var url = "/DataBank/PeriodicalEmployeelist/ExportToExcel?FromDate=" + FromDate + "&ToDate=" + ToDate +  "&TotalRows=" + pendingTableLength;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }



//}



function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    

    var PARAM = 'data=' + filter + '&FromDate=' + FromDate + '&ToDate=' + ToDate  ;
    var TableLength = $('#PeriodicalEmployeelistDetails_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/PeriodicalEmployeelist/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
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