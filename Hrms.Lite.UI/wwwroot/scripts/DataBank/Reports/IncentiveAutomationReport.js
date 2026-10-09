function fillData() {
    debugger;
    var filter = $('#BasicFilter').serialize();

    var monthcode = $('#Month_Code').val();

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#IncentiveAutomationReportTable").DataTable({
        info: false,
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/IncentiveAutomationReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&monthcode=' + monthcode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#IncentiveAutomationReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].dateOfChange = _GetFormattedDate(d.data[i].dateOfChange);
                  //  d.data[i].endDate = _GetFormattedDate(d.data[i].endDate);
                }


                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { data: "employeeID", width: "8%" },
            { data: "employeeName", width: "15%" },
            { data: "appraisalType", width: "10%" },
            { data: "dateOfChange", width: "8%" },
            { data: "oldLocation", width: "10%" },
            { data: "newLocation", width: "10%" },
            { data: "inceLocation", width: "10%" },
            { data: "oldDesignation", width: "10%" },
            { data: "newDesignation", width: "10%" },
            { data: "inceDesignation", width: "10%" },
            { data: "oldFunDesignation", width: "10%" },
            { data: "newFunDesignation", width: "8%" },
            { data: "inceFunDesignation", width: "8%" },
            { data: "oldCostCenter", width: "10%" },
            { data: "newCostCenter", width: "10%" },
            { data: "inceCostCenter", width: "10%" }

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





function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var monthcode = $('#Month_Code').val();

    var monthname = $('#Month_Code option:selected').text();

    var PARAM = 'data=' + filter + '&monthcode=' + monthcode + '&monthname=' + monthname;
    var TableLength = $("#IncentiveAutomationReportList_Count").val();
    if (TableLength > 0) {
        url = '/DataBank/IncentiveAutomationReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}







function SaveIDBUpload() {

    if (!validateById('#Month_Code', 'month_Id')) i++;

    var monthCode = $('#Month_Code').val();
    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/DataBank/IncentiveAutomationReport/SaveIDBupload?monthCode=' + monthCode,
                dataType: 'json',
                success: function (result) {

                    if (result.success === true) {
                        swal.fire("Uploaded successfully").then(function () {
                           // ApplyFilter('apply'); 
                        });
                    }
                    else {
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });

        }
        else {
            return false;
        }
    });
}
