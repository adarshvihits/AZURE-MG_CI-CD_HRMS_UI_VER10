

function fillData() {

    debugger;
    var period = $('#LeavePeriod_Code').val();
    var leave = $('#Leave_Code').val();
    var Filter = $('#BasicFilter').serialize();

    var temp = Filter;


    var dataTable = $("#ProjcetionTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/LeaveEncashmentProjection/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = Filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&LeavePeriod=' + period + '&Leave='+ leave+'&dummy=dummy';
                    d.filter = temp;                 

                    return JSON.stringify(d);
                },


            "dataSrc": function (d) {

                $('#LeaveEncashmentList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);


                }
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "8%" },
            { "data": "employeeName", "width": "11%" },
            { "data": "designation.name","width": "10%" },
            { "data": "department.name","width": "11%" },
            { "data": "location.name","width": "10%" },
            { "data": "doj","width": "9%" },
            { "data": "balance","width": "7%" },
            { "data": "maxLimit", "width": "8%" },
            { "data": "value", "width": "5%" },            
            { "data": "lds","width": "5%" },
            { "data": "enApllicableSalary","width": "7%"},
            { "data": "eligibleENAmount","width": "7%" },


        ],






        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

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
    var i = 0;    
    if (!validateById("#LeavePeriod_Code", 'proj_Period')) {
        i++;
    }
    if (!validateById("#Leave_Code", 'proj_Leave')) {
        i++;
    }
    if (i == 0) {
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
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
    }
}



function ExportToExcel() {
    debugger;
   
    var period = $('#LeavePeriod_Code').val();
    var leave = $('#Leave_Code').val();
    
    var tableLength = $('#LeaveEncashmentList_Count').val();
    if (tableLength > 0) {
        url = "/PayRoll/LeaveEncashmentProjection/ExportToExcel?LeavePeriod=" + period + '&Leave=' + leave + "&LPName="
            + $('#LeavePeriod_Code option:selected').text() +
            "&LName=" + $('#Leave_Code option:selected').text()
            + "&totRecords=" + tableLength;
      window.location.href = url;
         
        }
        else {
            swal.fire("No data found to export");
        }
    }

