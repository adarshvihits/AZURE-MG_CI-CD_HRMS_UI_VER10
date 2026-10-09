function ValidateDate(fromtodate) {
    debugger;
    flag = true;
    var fromDate = $('#FromDate').val();
    var toDate = $('#ToDate').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (fromtodate == 'fromdate') {
        if (D1 > D2) {
            flag = false;
            swal.fire("From date should be less than To date");
        }
    }
    else {
        if (D2 < D1) {
            flag = false;
            swal.fire("To date should be less than From date");
        }
    }
    return flag;

}
function fillData(fromtodate) {
    ValidateDate(fromtodate);

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();


    var dataTable = $("#ExitCheckListReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/AppointmentLetterSubmissionReport/GetList",
            "type": "POST",

            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {


                $('#AppointmentLetterSubmissionReportList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].acceptedDate = _GetFormattedDate(d.data[i].acceptedDate);
                    d.data[i].pdf = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onDownloadClick(' + d.data[i].trnCode + ')"  data-bs-toggle="tooltip" title = "Download" type = "button" class="btn   icon-only" ><i class="fa fa-download" aria-hidden="true" style="    font-size: 22px !important;"></i></a>'


                }
                console.log(d.data)
                return d.data;
            }

        },

        "columns": [

            { "data": "id", "width": "20%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "doj", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "funDesignation", "width": "15%" },
            { "data": "location", "width": "11%" },
            { "data": "acceptedDate", "width": "11%" },
            { "data": "pdf", "width": "11%" },


           




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
    var filter = $('#BasicFilter').serialize();

    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();


    var tableLength = $('#AppointmentLetterSubmissionReportList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/AppointmentLetterSubmissionReport/ExportToExcel?data=" + filter + "&FromDate=" + FromDate + "&ToDate=" + ToDate + "&TotalRows=" + tableLength;
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
function OnEmpNameClick(EmployeeCode) {

    debugger;

    $.ajax({
        type: 'GET',
        url: "/DataBank/AppointmentLetterSubmissionReport/GetPopUp",
        data: { EmployeeCode: EmployeeCode },
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.exitchecklistreport');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);

            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function ExportToExcelPopUp() {

    debugger;
    var EmployeeCode = $('#EmployeeCode').val();

    var tableLength = $('#AppointmentLetterSubmissionReportList_Count').val();
    if (tableLength > 0) {
        //url = "/DataBank/JoiningReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totRecords=" + tableLength;
        url = "/DataBank/AppointmentLetterSubmissionReport/ExportToExcelPopUp?EmployeeCode=" + EmployeeCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

var searchClick = 0

function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("exitpopup");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
        //if (noOfRows > 1) {
        //    $('#noDataImg').remove();
        //}
        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");
            //console.log("rs = " + rs);
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }

    if (noOfRows == 1) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
    }
}
function onSearchClick() {
    debugger;
    if (searchClick == 0) {
        searchClick = 1;
        $('.dataTableSearchPopup').addClass('field-width-big');
        /* CursorFocusOnSearch();*/
    }
    else {
        searchClick = 0;
        $('.dataTableSearchPopup').removeClass('field-width-big');
    }
}
function onDownloadClick(TrnCode) {
    debugger

    var url = "/DataBank/AppointmentLetterSubmissionReport/GetAppointmentLetterSubmissionPDF?TrnCode=" + TrnCode;
    window.location.href = url;


}

