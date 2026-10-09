function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $("#EmployeeVsAppraisalTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/EmployeeVsAppraisalDate/GetList",
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




                debugger;
                $('#EmployeeVsAppraisalDateList_Count').val(d.recordsTotal);           
                //$('#totEmpCnt').html(d.recordsTotal);                   
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    var dte = d.data[i].nextAppraisalDate;
                    //d.data[i].nextAppraisalDate = '<span id="AprDate_' + i + '" class="hide"> <input class="form-control kt_datepicker_2" id = "From" readonly="true" type="text"  onchange =""><span>'
                    d.data[i].nextAppraisalDate = '<span id="AprDate_' + i + '"> ' + dte + '</span><div class="input-group  date hide" id = "AppraisalDte_' + i + '"><input class="form-control kt_datepicker_2 RFromDate" id="NextDate_' + i +'" name = "From" onchange = "fillData();validate(this,"fromdate")" placeholder = "Appraisal Date" readonly = "true" type = "text"> <div class="input-group-append "><span class="input-group-text"> <i class="la la-calendar"></i></span></div></div >'

                    d.data[i].employeeGuid = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "empId", "width": "10%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "12%" },
            { "data": "category", "width": "12%" },
            { "data": "doj", "width": "15%" },
            { "data": "nextAppraisalDate", "width": "10%" },
            { "data": "employeeGuid", "width": "10%" },
            /*{ "data": "ptMonth" },*/



        ],


        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "autoWidth": false,

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


    const myTimeout = setTimeout(tooltipHover, 500);
    const myTimeout2 = setTimeout(datePicker, 500);
 
}

function onEditClick(i) {
    debugger
 
    $('#edit_' + i).addClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
    $('#AprDate_' + i).addClass('hide');
    $('#AppraisalDte_' + i).removeClass('hide');

    datePicker();
    
}


function onCancelClick(i) {
    debugger

 
    $('#edit_' + i).removeClass('hide');

    $('#updatecancel_' + i).addClass('hide');
   // $('#AprDate_' + i).addClass('hide');
    $('#AppraisalDte_' + i).addClass('hide');

}




function onUpdateClick(i, EmployeeGI) {
    debugger
    $('#EmployeeVsAppraisalDateList_' + i + '__AppraisalDte_').val($('#newAmount_' + i).val());
    $('#amtLabel_' + i).html($('#newAmount_' + i).val());
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    var EmployeeGIValue = EmployeeGI;
    var AppraisalDate = $('#NextDate_' + i).val();
  

    if (AppraisalDate == "" || AppraisalDate == null) {
        swal.fire("Date should not be null");
        fillData();
        return false;
    }
    debugger
    $.ajax({
        type: 'POST',
        url: '/Databank/EmployeeVsAppraisalDate/Create',
        dataType: 'json',
        data: "AppraisalDate=" + AppraisalDate +"&EmployeeGI=" + EmployeeGI,
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire("Updated Successfully").then(function () {
                    fillData();

                });

            }
            else {
                swal.fire(result.message).then(function () {

                });
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });
}

function ExportToExcel() {

    debugger;

    var TableLength = $('#EmployeeVsAppraisalDateList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/EmployeeVsAppraisalDate/ExportToExcel?totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }


}