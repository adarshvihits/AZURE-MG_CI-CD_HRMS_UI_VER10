

function validatePLCredit() {
    debugger;
    var i = 0;
    if (!validateById('#LeaveCode', 'lvcode'))
        i++;
    if (!validateById('#LeavePeriodCode', 'lpcode'))
        i++;
  
    if (i == 0)
        return true;
    else
        return false;
}



function fillData() {

    //if (!validatePLCredit()) {

    //    return false;
    //}
        debugger;
    var filter1 = $('#BasicFilter').serialize();
    var LeavePeriod = $('#LeavePeriodCode').val()
    var Leave = $('#LeaveCode').val()
    debugger;
    $.ajaxSetup({ async: false });

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var dropdownValues;
    var bgCodes = [];


    var TabIndex = $('input[name="TabIndex"]:checked').val();

    var dataTable = $("#PLCreditTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/PLCredit/GetPLCreditList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    debugger;

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length+ '&LeavePeriodCode=' + LeavePeriod + '&LeaveCode=' + Leave + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                debugger;
                $('#PLCreditList_Count').val(d.recordsTotal);
          
                for (var i = 0, ien = d.data.length; i < ien; i++) {
       
                    d.data[i].employeeGI = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                    d.data[i].opening    = '<span id="hideOpening_' + i + '">' + d.data[i].opening + '</span ><span id="OpeningTxtbox_' + i + '"class="hide"><input id="newOpening_' + i + '" class="form-control form-control-sm" placeholder="Opening" ></span>'
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].postingDate = _GetFormattedDate(d.data[i].postingDate);
                }
                return d.data;
            }
        },
        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "20%" },
            { "data": "designation", "width": "12%" },
            { "data": "location", "width": "12%" },
            { "data": "category", "width": "12%" },
            { "data": "doj", "width": "10%" },
            { "data": "postingDate", "width": "10%" },
            { "data": "loP_AB", "width": "7%" },
            { "data": "opening", "width": "7%" },
            { "data": "employeeGI", "width": "5%" },
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
        //$('#noDataFound').css("display", 'none');

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

}




function onEditClick(i) {
    debugger
    $('#edit_' + i).addClass('hide');
    $('#newOpening_' + i).val($('#hideOpening_' + i).text());
    $('#hideOpening_' + i).addClass('hide');
    $('#OpeningTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
}



function onCancelClick(i) {
    debugger
    $('#edit_' + i).removeClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#hideOpening_' + i).removeClass('hide');
    $('#OpeningTxtbox_' + i).addClass('hide');
    $('#newOpening_' + i).val(0);
}


function onUpdateClick(index, EmployeeGI) {
    debugger;
    var LeavePeriodCode = $('#LeavePeriodCode').val()
    var LeaveCode = $('#LeaveCode').val()

    var LeaveOpening = $('#newOpening_' + index).val()


    if (!LeaveOpening) {
        Swal.fire("Please enter Opening");
        return;
    }

    swal.fire({
        text: "Do you want to Update?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

               $.ajax({
                type: 'POST',
                url: '/TimeOffice/PLCredit/PLCreditSave',
                dataType: 'json',
                data: {
                    EmployeeGI: EmployeeGI,
                    Opening: LeaveOpening,
                    LeavePeriodCode: LeavePeriodCode,
                    LeaveCode: LeaveCode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
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
        else {
            return false;
        }
    })


}

function BatchUpdatePLCredit() {
    var LeavePeriodCode = $('#LeavePeriodCode').val()
    var LeaveCode = $('#LeaveCode').val()

    if (!validatePLCredit()) {
        return false;
    }

    Swal.fire({
        text: "Do you want to Update?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/TimeOffice/PLCredit/PLCreditBatchUpdate',
                dataType: 'json',
                data:
                    $('#BasicFilter').serialize() +
                    '&LeavePeriodCode=' + LeavePeriodCode +
                    '&LeaveCode=' + LeaveCode,

                success: function (result) {

                    if (result.success === true) {
                        Swal.fire("Updated Successfully").then(function () {
                            fillData();
                            CloseSlider('batchUpdateSlider');
                        });
                    } else {
                        Swal.fire(result.message);
                    }
                },
                error: function (xhr) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
    });
}






function ExportToExcel() {
    debugger;

    var filterdata = $('#BasicFilter').serialize();
    var LeavePeriodCode = $('#LeavePeriodCode').val();
    var LeaveCode = $('#LeaveCode').val();
    var LeavePeriodName = $('#LeavePeriodCode option:selected').text();
    var LeaveName = $('#LeaveCode option:selected').text();

    var PARAM = 'data=' + filterdata;
    var TableLength = $('#PLCreditList_Count').val();

    if (TableLength > 0) {
        var url = '/TimeOffice/PLCredit/ExportToExcel?' +
            PARAM +
            "&LeavePeriodCode=" + LeavePeriodCode +
            "&LeaveCode=" + LeaveCode +
            "&LeaveName=" + LeaveName +
            "&LeavePeriodName=" + LeavePeriodName +
            "&TotalRecords=" + TableLength;

        window.location.href = url;
    } else {
        Swal.fire("No data found to export");
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
            /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();
        /*CloseSlider('basicFilter');*/
    }
}