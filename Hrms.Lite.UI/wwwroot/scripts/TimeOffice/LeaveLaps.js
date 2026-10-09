

/*----LEAVE LAPS*/
function validateLeaveLaps() {
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


    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var LeavePeriod = $('#LeavePeriodCode').val()
    var Leave = $('#LeaveCode').val()
    debugger;
    $.ajaxSetup({ async: false });

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
   
    var TabIndex = $('input[name="TabIndex"]:checked').val();

    var dataTable = $("#LeaveLapsTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/LeaveLaps/GetLeaveLapsList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    debugger;

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&LeavePeriodCode=' + LeavePeriod + '&LeaveCode=' + Leave + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                debugger;
                $('#LeaveLapsList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    var empGI = d.data[i].employeeGI;
                    d.data[i].employeeGI = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ', \'' + empGI + '\')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                 
                    d.data[i].lapsCount =
                        '<span id="hideLapsLeave_' + i + '">' + d.data[i].lapsCount + '</span>' +
                        '<span id="LapsLeaveTxtbox_' + i + '" class="hide">' +
                        '<input id="newLapsLeave_' + i + '" ' +
                        'class="form-control form-control-sm" ' +
                        'placeholder="Laps Leave" ' +
                        'onkeypress="return onlyAmount(event, this)">' +
                        '</span>';
                }
                return d.data;
            }
        },
        "columns": [

            { "data": "employeeID", "width": "7%" },
            { "data": "employeeName", "width": "15%" },
            { "data": "funcDesignation", "width": "12%" },
            { "data": "designation", "width": "12%" },
            { "data": "credit", "width": "8%" },
            { "data": "debit", "width": "8%" },
            { "data": "taken", "width": "8%" },
            { "data": "balance", "width": "8%" },
            { "data": "mandatoryTaken", "width": "8%" },
            { "data": "lapsCount", "width": "8%" },
            { "data": "employeeGI", "width": "6%" },
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
    $('#newLapsLeave_' + i).val($('#hideOpening_' + i).text());
    $('#hideLapsLeave_' + i).addClass('hide');
    $('#LapsLeaveTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
}



function onCancelClick(i) {
    debugger
    $('#edit_' + i).removeClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#hideLapsLeave_' + i).removeClass('hide');
    $('#LapsLeaveTxtbox_' + i).addClass('hide');
    $('#newLapsLeave_' + i).val(0);
}




function onUpdateClick(index, EmployeeGI) {
    var LeavePeriodCode = $('#LeavePeriodCode').val();
    var LeaveCode = $('#LeaveCode').val();
    var LeaveLaps = $('#newLapsLeave_' + index).val();

    var row = $('#LeaveLapsTable tbody tr').eq(index);

    var Taken = parseFloat(row.find('td').eq(6).text().trim()) || 0;
    var MandatoryTaken = parseFloat(row.find('td').eq(7).text().trim()) || 0;
    var Balance = parseFloat(row.find('td').eq(8).text().trim()) || 0;

    if (!LeaveLaps) {
        Swal.fire("Please enter Laps Leave");
        return;
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
                url: '/TimeOffice/LeaveLaps/LeaveLapsSave',
                dataType: 'json',
                data: {
                    EmployeeGI: EmployeeGI,
                    Taken: Taken,
                    MandatoryTaken: MandatoryTaken,
                    Balance: Balance,
                    LeaveLaps: LeaveLaps,
                    LeavePeriodCode: LeavePeriodCode,
                    LeaveCode: LeaveCode
                },
                success: function (result) {
                    if (result.success) {
                        Swal.fire(result.message).then(fillData);
                    } else {
                        Swal.fire(result.message);
                    }
                }
            });
        }
    });
}




function BatchUpdateLeaveLaps() {
    var LeavePeriodCode = $('#LeavePeriodCode').val()
    var LeaveCode = $('#LeaveCode').val()

    if (!validateLeaveLaps()) {
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
                url: '/TimeOffice/LeaveLaps/BatchUpdateLeaveLaps',
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
    var TableLength = $('#LeaveLapsList_Count').val();

    if (TableLength > 0) {
        var url = '/TimeOffice/LeaveLaps/ExportToExcel?' +
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