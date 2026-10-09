function fillData() {

    debugger;
    if (validateDropDowns()) {
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        var dataTable = $("#ParentsMediclaimTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/ParentsMediclaimEMI/GetList",
                "type": "POST",
                "datatype": "json",
                'data':
                    function (d) {
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&Month=' + $('#SalaryPeriod_Code').val() + '&FinYear=' + $('#FinancialYear_Code').val() + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {
                    $('#ParentsMediclaimEMIList_Count').val(d.recordsTotal);
                    return d.data;
                }
            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName"},
                { "data": "designation"},
                { "data": "department"},
                { "data": "location" },
                { "data": "premiumAmount" },
                { "data": "tenure"},
                { "data": "emiAmount" },
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


function validateDropDowns() {
    debugger
    var i = 0;
    if (!validateById('#FinancialYear_Code', 'fin_year'))
        i++;
    if (!validateById('#SalaryPeriod_Code', 'sal_month'))
        i++;
    
    if(i==0)
        return true;
    else
        return false;

}
function ApplyFilter(mode) {

    $("#SearchId").val($('#FilterList_Code').val())
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
    }
}
function onSaveMediclaimClick() {
    debugger
    showSpinner();
    var filter1 = $('#BasicFilter').serialize();
    var Month = $('#SalaryPeriod_Code').val();
    var FinYear = $('#FinancialYear_Code').val();
    var param = $('#BasicFilter').serialize() + '&Month=' + Month + '&FinYear=' + FinYear;

    if (validateDropDowns()) {
        $.ajax({
            type: 'POST',
            url: '/PayRoll/ParentsMediclaimEMI/Save',
            dataType: 'json',
            data: param,
            success: function (result) {
                debugger;
                if (result.success == true) {
                    hideSpinner();
                    swal.fire(result.message).then(function () {
                        fillData();
                    });
                }

            },

            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }

    else
        return false;
}
function ExportToExcel() {
    var FinYear = $('#FinancialYear option:selected').text();
    var monthName = $('#SalaryPeriod option:selected').text();
    var param = $('#BasicFilter').serialize() + '&Month=' + $('#SalaryPeriod_Code').val() + '&Financialyear=' + $('#FinancialYear_Code').val() + '&FinYear=' + FinYear + '&monthName=' + monthName;
    var TableLength = $('#ParentsMediclaimEMIList_Count').val();
    if (TableLength > 0) {
        window.location.href = '/PayRoll/ParentsMediclaimEMI/ExportToExcel?data=' + param + '&Total=' + TableLength;
    }
    else {
        swal.fire("No data found to export");
    }
}
