

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;



    var dataTable = $("#ExitInterviewReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/ExitInterviewReport/GetList",
            "type": "POST",

            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {


                $('#ExitInterviewReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].employeeName = '<td data-label="Employee Name"><a data-bs-toggle="modal"data-target=".exitinterviewreport" type="button"class="btn p-0 btn-sm float-left" onclick="OnEmpNameClick(' + d.data[i].employeeCode + ')">' + d.data[i].employeeName + '</a></td>'

                    d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);

                }
                console.log(d.data)
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "11%" },
            { "data": "category", "width": "12%" },
            { "data": "lwd", "width": "9%" },
            { "data": "entryFrom", "width": "12%" },
            { "data": "stage", "width": "12%" },




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

function ExportToExcel() {

    debugger;
    var filter = $('#BasicFilter').serialize();
   
    var tableLength = $('#ExitInterviewReportList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/ExitInterviewReport/ExportToExcel?data=" + filter +"&totalRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function OnEmpNameClick(EmployeeCode) {

    debugger;

    $.ajax({
        type: 'GET',
        url: "/DataBank/ExitInterviewReport/GetExitInterviewReportPopUpDetails",
        data: { EmployeeCode: EmployeeCode },
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.exitinterviewreport');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);

            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;

    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("ExitInterviewDetailsTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var dataRowsCount = 0;
    var visibleRowsCount = 0;
    var spannedRows = 0;
    var show = true;

    for (i = 1; i < tr.length; i++) { // Start from 1 to skip the header row
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
        if (td) {
            dataRowsCount++;
            if (spannedRows > 0) {
                if (show)
                    tr[i].style.display = "";
                else {
                    tr[i].style.display = "none";
                    visibleRowsCount--;
                }
                spannedRows--;
            } else {
                tr[i].style.display = "none";
                if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                    tr[i].style.display = "";
                    visibleRowsCount++;
                }
                let rs = td.getAttribute("rowspan");
                if (rs && rs > 1) {
                    show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                    spannedRows = rs - 1;
                }
            }
        }
    }

    if (visibleRowsCount === 0) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='5'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }

}

function ExportToExcelPopUp() {

    debugger;
    var TableLength = $('#ExitInterviewReportList_Count').val();
    var EmployeeCode = $('#EmployeeCode').val();

    if (TableLength > 0) {
        url = "/DataBank/ExitInterviewReport/ExitInterviewPopUpExportToExcel?EmployeeCode=" + EmployeeCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}