

function fillMonth(sender) {
    debugger;
    $.getJSON("/PayRoll/ArearProcessing/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#Month_Code option').remove();
        $('#Month_Code').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function fillData() {

    debugger;
    if (validatefilter()) {
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        var dataTable = $("#ArearProcessingTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/ArearProcessing/Process",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&Month=' + $('#Month_Code').val() + '&PayrollGroup=' + $('#PayrollGroup_Code').val()+'&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {





                    $('#ArearProcessingList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                        d.data[i].employeeName = '<div><a class="" data-bs-toggle="modal" data-target="#arear_popup"  onclick="POPUP('  + d.data[i].employeeCode + ')">' + d.data[i].employeeName + ' </a ></div>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "20%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "location", "width": "15%" },
                { "data": "withEffectFrom", "width": "15%" },
                { "data": "totalDays", "width": "15%" },
                { "data": "lop", "width": "15%" },
                { "data": "presentDays", "width": "15%" },
                { "data": "amount", "width": "15%" },
                { "data": "pf", "width": "15%" },
                { "data": "esi", "width": "15%" }


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

function validatefilter() {
    debugger
    if (!validateById('#Month_Code', 'arear_month'))
        return false;
    else
    return true;
       
}

function POPUP(empcode)
{
     
    $.ajax({
    type: 'GET',
        url: '/PayRoll/ArearProcessing/GetDetails',
        dataType: 'html',
        data: { EmpCode: empcode,  Month:$("#Month_Code").val() },
    success: function (Data) {
    var m = $('#arear_popup');
    m.find('.modal-body').html('');
    m.find('.modal-body').html(Data);
    m.modal('show');
            },
    error: function (xhr, textStatus, errorThrown) {
        swal.fire(xhr.responseText);

            }
        });
    }


function ExportToExcel() {

    debugger;

    var TableLength = $('#ArearProcessingList_Count').val();
    if (TableLength > 0) {
    
        var Month = $('#Month_Code').val();
        var MonthName = $('#Month_Code option:selected').text();
        var PgGroup = $('#PayrollGroup_Code').val();
        var PgGroupName = $('#PayrollGroup_Code option:selected').text();
        var param = $('#BasicFilter').serialize() + '&Month=' + Month + "&MonthName=" + MonthName + "&PgGroup=" + PgGroup + "&TotalRows=" + TableLength + "&PgGroupName=" + PgGroupName;
        url =  "/PayRoll/ArearProcessing/ExportToExcel?data=" + param;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}
