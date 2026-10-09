



function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;




    var Date = $('#Date').val();
    var MealTime = $('#MealTime').val();
    var MealType = $('#MealType').val();

    var dataTable = $("#CanteenReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/CanteenDateWiseReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Date=' + Date + '&MealTime=' + MealTime + '&MealType=' + MealType + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger;
                console.log(d);
                $('#CanteenDateEmployeeList_Count').val(d.recordsTotal);

                debugger;


                if (d.recordsTotal > 0) {
                    debugger;
                    if (MealType == 1) {
                        $('#vegID').css("display", "block");
                        $('#NonID').css("display", "none");
                        $('#vegSpanID').html(d.summaryList[1].totalCount);
                    }
                    if (MealType == 2) {
                        $('#NonID').css("display", "block");
                        $('#vegID').css("display", "none");
                        $('#NonvegSpanID').html(d.summaryList[2].totalCount);
                    }
                    else if (MealType == "" || MealType == 0) {
                        $('#vegID').css("display", "block");
                        $('#NonID').css("display", "block");
                        $('#vegSpanID').html(d.summaryList[1].totalCount);
                        $('#NonvegSpanID').html(d.summaryList[2].totalCount);

                    }

                }

                else {
                    $('#vegID').css("display", "block");
                    $('#NonID').css("display", "block");
                    $('#vegSpanID').html(0);
                    $('#NonvegSpanID').html(0);
                }
             
                //for (var i = 0, ien = d.data.length; i < ien; i++) {


                //}

                return d.data;
                return d.summaryList;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation.name" },
            { "data": "department.name" },
            { "data": "location.name" },
            { "data": "mealType" },
         






        ],






        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',


        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

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
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        CloseSlider('basicFilter');
    }
}
function ExportToExcel() {

    debugger;
    var Date = $('#Date').val();
    var MealTime = $('#MealTime').val();
    var MealType = $('#MealType').val();
    var vegCount = $('#vegSpanID').text();
    var NonvegCount = $('#NonvegSpanID').text();

    var MealTimeName = $('#MealTime option:selected').text();
    var MealTypeName = $('#MealType option:selected').text();


    var TableLength = $('#CanteenDateEmployeeList_Count').val();

    if (TableLength > 0) {
        url = "/DataBank/CanteenDateWiseReport/ExportToExcel?SelectedDate=" + Date + "&MealTime=" + MealTime + "&MealType=" + MealType + "&MealTimeName=" + MealTimeName + "&MealTypeName=" + MealTypeName + "&totalRecord=" + TableLength + "&vegCount=" + vegCount + "&NonvegCount=" + NonvegCount;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}