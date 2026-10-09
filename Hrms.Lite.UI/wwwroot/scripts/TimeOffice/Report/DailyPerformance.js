function fillData() {

    debugger;
    var Date = $('#Date').val();
    var NoOfPunch = $('#NoOfPunchCode').val();
    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;

    showSpinner();
    var dataTable = $("#DailyPerformanceTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/DailyPerformance/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue
                        + '&Date=' + Date + '&NoOfPunch=' + NoOfPunch + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {

                $('#DailyPerformanceList_Count').val(d.recordsTotal);
                console.log(d);
                for (var i = 0, ien = d.data.length; i < ien; i++)
                {
                    d.data[i].employeeName = '<div><a onclick="GetpunchStatus(' + "'" + d.data[i].employeeGI + "'" + ')" >' + d.data[i].employeeName + '</a></div>'


                }
                hideSpinner();
                return d.data;
            },
           

        },

        "columns": [

            { "data": "employeeID", "width": "5%" },
            { "data": "employeeName", "width": "10%" },
            { "data": "departmentName", "width": "10%" },
            { "data": "designationName", "width": "10%" },
            { "data": "locationName", "width": "10%" },
            { "data": "division", "width": "10%" },
            { "data": "pintime", "width": "5%" },
            { "data": "pouttime", "width": "5%" },
            { "data": "totInTime", "width": "5%" },
            { "data": "totOUTTime", "width": "10%" },
            { "data": "ptotaltime", "width": "10%" },
            { "data": "noOfPunch", "width": "5%" },

        ],

        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": "true",
        "language": {
            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',

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


function GetpunchStatus(EmployeeGI) {

    var Date = $('#Date').val();
    debugger;
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/DailyPerformance/GetDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, Date: Date },
        success: function (Data) {
            var m = $('#DetailsPOPUP');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}




function ExportToexcel()
{
    debugger;

    var Date = $('#Date').val();
    var NoOfPunch = $('#NoOfPunchCode').val();

    var Filter = $('#BasicFilter').serialize();
    var temp = Filter;
    var TableLength = $('#DailyPerformanceList_Count').val();

    if (TableLength > 0) {
        url = "/TimeOffice/DailyPerformance/ExportToExcel?data=" + temp + "&Date=" + Date + "&NoOfPunch=" + NoOfPunch + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();

    $("#SearchId").val($('#FilterList_Code').val())
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
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        fillData();

        $('#filterForm').removeClass('filter-apply');
    }
}


function ExportToExcelPunchStatusPOPUP(PDate, EmployeeGI) {
    debugger;

    var TableLength = $('#DailyPerformancePopUpList_Count').val();
    if (TableLength > 0) {
        url = "/TimeOffice/DailyPerformance/ExportToExcelPunchStatusPOPUP?PDate= " + PDate + '&EmployeeGI=' + EmployeeGI + '&totalrecords=' + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}
