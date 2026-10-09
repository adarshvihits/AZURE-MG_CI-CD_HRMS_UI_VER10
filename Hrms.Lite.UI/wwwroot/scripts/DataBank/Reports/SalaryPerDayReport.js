


function validateSalaryDays(sender) {
    debugger;

    var salDays = $(sender).val();

    if (parseFloat(salDays) <= 0 || salDays == "") {
        $(sender).val(1);

    }

}

function validateDividingFactor(sender) {
    debugger;

    var divFactor = $(sender).val();

    if (parseFloat(divFactor) <= 0 || divFactor == "") {
        $(sender).val(26);
    }

}

function ValidateRequiredData() {
    var allowance1 = $('#Allowance').val();
    var salarydays1 = $('#SalaryDays').val();
    var dividingfactor1 = $('#DividingFactor').val();


    if (allowance1.length == 0) {
        debugger;
        $("#Allowance").addClass('is-invalid');
        $("#spAllowance").addClass('error-message');
        $('#spAllowance').html("This field is required");
        // return false;*/
    }
    else {
        $("#Allowance").removeClass('is-invalid');
        $("#spAllowance").removeClass('error-message');
        $("#spAllowance").html("");
    }
    return true;
}


function fillData() {


    //if (!ValidateRequiredData()) {
    //    return false;
    //}

    var allowance = $('#Allowance').val();
    var salarydays = $('#SalaryDays').val();
    var dividingfactor = $('#DividingFactor').val();


    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#salaryperdaytable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/SalaryPerDayReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&salarydays=' + salarydays + '&dividingfactor=' + dividingfactor + '&allowance=' + allowance + '&dummy=dummy';
                    d.filter = temp;
                    d.allowance = allowance;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#EmployeeSalaryPerDayList_Count').val(d.recordsTotal);

                debugger;



                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "15%" },
            { "data": "grossPay", "width": "10%" },
            { "data": "ctc", "width": "10%" },
            { "data": "salaryAsPerDay", "width": "10%" },

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
    debugger

    var filterData = $('#BasicFilter').serialize()

    if (mode == 'apply') {
        /* showSpinner();*/
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
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        fillData();
        /*  CloseSlider('basicFilter');*/
        $('#filterForm').removeClass('filter-apply');
    }
}


function ExportToExcel() {
    debugger;

    var allowance = $('#Allowance').val();
    var SalaryDays = $('#SalaryDays').val();
    var DividingFactor = $('#DividingFactor').val();

    var texts = $('#Allowance option:selected').map(function () {
        return $(this).text();
    }).get();



    var tableLength = $('#EmployeeSalaryPerDayList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/SalaryPerDayReport/ExportToExcel?allowance=" + allowance + "&allowanceType=" + encodeURIComponent(texts) + "&SalaryDays=" + SalaryDays + "&DividingFactor=" + DividingFactor + "&totRecords=" + tableLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

