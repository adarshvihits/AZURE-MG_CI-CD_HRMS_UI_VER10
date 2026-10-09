function FillData() {
    debugger;

    var FYCode = $('#FYCode').val();
    var SubjectCode = $('#SubjectCode').val();
    var filter = $("#BasicFilter").serialize();
    var dataTable = $('#TrainingReportListTable').DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "type": "POST",
            "url": "/TrainingModule/Training_EmpVsSubject/GetList",
            "dataType": "json",

            'data':
                function (d) {
                    temp = '';
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FYCode=' + FYCode + '&SubjectCode=' + SubjectCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#TrainingEmployeeReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                }


                return d.data;

            }
        },
        "columns": [
            // { "data": "employeeCode", "width": "10%" },
            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "10%" },
            { "data": "grade", "width": "10%" },
            { "data": "doj", "width": "8%" },
            { "data": "training", "width": "10%" },
            { "data": "subject", "width": "8%" },
           /* { "data": "trainingCountSelected", "width": "8%" },*/



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
}



//function GetTrainingDropdown() {
//    debugger;
//    var FYCode = $('#FYCode').val();
//    var CategoryCode = $('#CategoryCode').val();
//    $.getJSON("/TrainingModule/Training_EmpVsSubject/GetTrainingEmployeeComboData", { FYCode: FYCode, SubjectCode: SubjectCode }, function (data) {

//        $('#SubjectCode option').remove();

//        $('#SubjectCode').append(('<option value>--Select Training--</option'));

//        console.log(data);
//        debugger
//        for (var i = 0; i < data.length; i++) {
//            debugger
//            $('#SubjectCode').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
//        }
//        FillData();

//    });
//}


function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {


        if (filterApplicable >= 1) {
            FillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            FillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        FillData();

    }
}







function ExportToExcel() {
    //alert($('#FYCode option:selected').text())
    //alert($('#SubjectCode option:selected').text())
        
   debugger;
    var filter = $('#BasicFilter').serialize();
    var FYCode = $('#FYCode').val();
    var FYName  = $('#FYCode option:selected').text();
    var SubjectCode = $('#SubjectCode').val();
    var SubjectName = $('#SubjectCode option:selected').text();
   
    var PARAM = 'data=' + filter + '&FYCode=' + FYCode + '&FYName=' + FYName + '&SubjectCode=' + SubjectCode + '&SubjectName=' + SubjectName;
    var TableLength = $('#TrainingEmployeeReportList_Count').val();
    if (TableLength > 0) {
        url = '/TrainingModule/Training_EmpVsSubject/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


