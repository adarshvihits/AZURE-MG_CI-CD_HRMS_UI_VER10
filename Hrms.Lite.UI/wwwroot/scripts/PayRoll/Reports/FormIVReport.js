



function fillData() {
    debugger;
    var month = $('#monthcode').val();

    var filter1 = $('#BasicFilter').serialize();
   
    
    var dataTable = $('#FormIVReport_Table').DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/FormIVReport/GetList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {
                debugger
                console.log(d.data)
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].download = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onDownloadClick(' + d.data[i].employeeCode + ')"  data-bs-toggle="tooltip" title = "Download" type = "button" class="btn   icon-only" ><i class="fa fa-download" aria-hidden="true" style="    font-size: 22px !important;"></i></a>'
                   

}

                debugger;
                console.log(d);
                $('#FormIVReportList_Count').val(d.recordsTotal);
                
                console.log(d.data)
                return d.data;
            }

        },


        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "department"},
            { "data": "designation"},
            { "data": "location" },
            { "data": "doj" },
            { "data": "download" },
        ],



        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "autoWidth": false,

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


    const myTimeout = setTimeout(tooltipHover, 500);




}



function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var month = $('#monthcode').val();



    var PARAM = 'data=' + filter + '&Month=' + month;
    var TableLength = $('#FormIVReportList_Count').val();
    if (TableLength > 0) {
        url = '/PayRoll/FormIVReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
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
function onDownloadClick(EmployeeCode) {
    debugger

    var url = "/PayRoll/FormIVReport/GetFormIVReportPDF?EmployeeCode=" + EmployeeCode;
    window.location.href = url;


}