function onAssessmentYearChange(sender, id) {
    debugger;
    $.getJSON("/TDS/EmployeePerquisitesReport/GetMonthDropDown", { AssessmentYearCode: $(sender).val() }, function (data) {
        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value="0">--All--</option>');
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
}
function validateDropdowns() {
    debugger
    var i = 0;
    if (!validateById('#AssessmentYear_Code', 'Assessmnt_year'))
        i++;
    if (!validateById("#Month_Code", 'month'))
        i++;
    if (!validateById("#Perquisite_Code", 'Perquisit'))
        i++;
    if (i == 0) 
        return true;
    else 
        return false;
}
function fillData() {
    debugger
    var i = 0;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var AssessmentYear = $('#AssessmentYear_Code').val();
    var Month = $('#Month_Code').val();
    var PerquisiteNature = $('#Perquisite_Code').val();

    if (!validateById("#AssessmentYear_Code", 'Assessmnt_year'))
        i++;
    if (i == 0) {
        var dataTable = $("#PerquisiteReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/TDS/EmployeePerquisitesReport/GetList",
                "type": "POST",
                "datatype": "json",
                'data':
                    function (d) {
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&Month=' + Month + '&PerquisiteNature=' + PerquisiteNature + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {
                    $('#EmployeePerquisitesReportList_Count').val(d.recordsTotal);
                    return d.data;
                }
            },
            "columns": [
                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "department" },
                { "data": "location" },
                { "data": "monthName" },
                { "data": "perquisiteName"},
                { "data": "amount" },
            ],
            "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,
            "order": [0, "asc"],
            "processing": "true",
            "language": {
                emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
                zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',
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
        });
        $('.search-slider-inner').click(function () {
            dataTable.search('').draw();
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
        setTimeout(tooltipHover, 500);
    }
    else
        return false;
}
function ExportToExcel() {

    var AssessmentYearName = $('#AssessmentYear_Code option:selected').text();
    var Month = $('#Month_Code').val();
    var Perquisite = $('#Perquisite_Code').val();
    var MonthName = $('#Month_Code option:selected').text();
    var PerquisiteName = $('#Perquisite_Code option:selected').text();
    var i = 0;
    if (!validateById('#AssessmentYear_Code', 'Assessmnt_year'))
        i++;
    if (i==0) {

        var PerquisitetableLength = $('#EmployeePerquisitesReportList_Count').val();
        if (PerquisitetableLength > 0) {
            url = "/TDS/EmployeePerquisitesReport/ExportToExcel?AssessmentYear=" + $('#AssessmentYear_Code').val() + '&AssessmentYearName=' + AssessmentYearName + '&Month=' + Month + '&MName=' + MonthName + '&PerquisiteName=' + PerquisiteName + '&Perquisite=' + Perquisite + '&Totalrow=' + PerquisitetableLength + "&data=" + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

}
function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        //showSpinner();
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
        //showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();

    }
}