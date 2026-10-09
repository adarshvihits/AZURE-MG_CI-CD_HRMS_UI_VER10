function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var Month = $('#Month').val();
    var PayrollGroupCode = $('#PayrollGroup').val();
    var dataTable = $("#SalaryEditTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/SalaryEdit/GetList",
            "type": "POST",
            "datatype": "json",

            "data": function (d) {
                var searchValue = $('#dataTableSearch').val();

                // Add custom parameters to the data object

                d.Month = Month;
                d.PayrollGroupCode = PayrollGroupCode;
                d.searchValue = searchValue;

                // Parse filter1 into key-value pairs and add them to d
                filter1.split('&').forEach(function (pair) {
                    var parts = pair.split('=');
                    var key = decodeURIComponent(parts[0]);
                    var val = decodeURIComponent(parts[1] || '');
                    d[key] = val;
                });
                return d; // Will be sent as form data (x-www-form-urlencoded)
            },



            "dataSrc": function (d) {
                debugger;
                $('#SalaryEditList_Count').val(d.recordsTotal);
                $("#totAmt").html(d.totalAmount);

               



                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeHeader.employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="EmployeeClick(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ')">' + d.data[i].employeeHeader.employeeName + '' + '' + '</a>'

                }


                return d.data;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId" },
            { "data": "employeeHeader.employeeName" },
            { "data": "department" },
            { "data": "grade" },
            { "data": "designation" },
            { "data": "grossAllowance" },
            { "data": "grossDeduction" },
            { "data": "advance" },
            { "data": "loan" },
            { "data": "netAmount" },
     

        ],

        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',


        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

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


    const myTimeout = setTimeout(tooltipHover, 500);



}

function EmployeeClick(EmployeeGI) {
    var Month = $('#Month').val();

    var url = "/PayRoll/SalaryEdit/GetSalaryEditDetails?Month= " + Month + " &EmployeeGI=" + EmployeeGI;


    window.location.href = url;
}

function onEditClick(i) {

    $('#amtLabel2_' + i).addClass('hide');
    $('#editbt2_' + i).addClass('hide');
    $('#amtTxtbox2_' + i).removeClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
    $('#editbt2_' + i).addClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
}
function onCancelClick(i) {
    debugger;
    $('#amtLabel2_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
}
function onUpdateClick(DescriptionCode,i) {
    debugger;
    var EmployeeGI = $('#EmployeeHeader_EmployeeGuid').val();
    var RowType = $('#SalaryEditDetailsList_' + i + '__RowType').val();
    var Amt = $('#SalaryEditDetailsList_' + i + '__Total').val();
    var MonthCode = $('#Month').val();
   if (Amt == "" || Amt == null) {
        swal.fire("Total should not be null");
        return false;
    }


    swal.fire({
        text: "Do you want to Update?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (!result.isConfirmed)
        {
        
                $('#updatecancel_' + i).addClass('hide');
                $('#edit_' + i).removeClass('hide');
                return;
            }
            $.ajax({
                type: 'POST',
                url: '/PayRoll/SalaryEdit/Save',
                dataType: 'json',
                data: { EmployeeGI:EmployeeGI,RowType:RowType,TrnCode:DescriptionCode,Month:MonthCode,Total:Amt },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire("Saved Successfully.");
                        EmployeeClick(EmployeeGI);
                        
                       
                        
                    }


                    else
                        swal.fire(result.message);
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
    });
}