
function GetList() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var Code = $("#Training").val();

    var temp = filter1;

    var dataTable = $("#TrainingPostTestReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TrainingModule/TrainingPostTestReport/GetTrainingPostTestReportList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Code=' + Code + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {
                $('#TrainingPostTestReportList_Count').val(d.recordsTotal);

                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "12%" },
            { "data": "fullName", "width": "15%" },
            { "data": "designation", "width": "13%" },
            { "data": "department", "width": "13%" },
            { "data": "location", "width": "11%" },
            { "data": "category", "width": "12%" },
            { "data": "percentageAcheived", "width": "12%" },
            { "data": "testPassedStatus", "width": "12%" },


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


function ExportToExcel() {

    debugger;
    var Code = $("#Training").val();
    var TrainingCode = $('#Training option:selected').text();

    var tableLength = $('#TrainingPostTestReportList_Count').val();

    if (tableLength > 0) {
        url = "/TrainingModule/TrainingPostTestReport/ExportToExcel?TotalRec=" + tableLength + '&Code=' + Code + '&TrainingCode=' + TrainingCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
