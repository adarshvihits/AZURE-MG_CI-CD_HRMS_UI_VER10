var dropdownValues;
function onEditClick(i, bgCode) {
    debugger
    $('#workLocationCode_' + i + ' option').remove();
    $('#workLocationCode_' + i).append('<option value="0">--Select Work Location--</option');
    for (var j = 0; j < dropdownValues.length; j++) {
        $('.bGrp').append('<option value="' + dropdownValues[j].code + '">' + dropdownValues[j].name + '</option');

    }
    $('#workLocationCode_' + i).val(bgCode);
    $('#workLocationCode_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
    $('#workLocation_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');

}

function onCancelClick(i) {
    $('#workLocationCode_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#workLocation_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');

}
function fillData() {

    debugger;
    $.ajaxSetup({ async: false });

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var bgCodes = [];


    var TabIndex = $('input[name="TabIndex"]:checked').val();


    /*if (MonthCode != '' && AllowanceCode != '') {*/
    var dataTable = $("#EmployeeVsWorkLocationTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/EmployeeVsWorkLocation/GetList",
            "type": "POST",
            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                debugger;
                console.log(d);
                $('#EmployeeVsWorkLocationList_Count').val(d.recordsTotal);
                dropdownValues = d.workLocationDropDown;
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].workLocation.name = '<select class="form-control form-control-sm bGrp hide" id="workLocationCode_' + i + '" name="workLocationCode[' + i + ']"><option value="0">--Select Work Location--</option></select><span id="workLocation_' + i + '">' + d.data[i].workLocation.name + '</span>';
                    d.data[i].employeeGuid = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ',' + "'" + d.data[i].workLocation.code + "'" + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                }
                return d.data;

            }
        },
        "columns": [
            { "data": "employeeID" },
            { "data": "name" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "workLocation.name" },
            { "data": "employeeGuid" },
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

    for (var j = 0; j < dropdownValues.length; j++) {
        $('.bGrp').append('<option value="' + dropdownValues[j].code + '">' + dropdownValues[j].name + '</option');
    }

}



function ApplyFilter(mode) {
    debugger

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
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}



function onUpdateClick(i, EmployeeGI) {
    debugger;

    var workLocCode = $('#workLocationCode_' + i).val();

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
                url: '/DataBank/EmployeeVsWorkLocation/SaveEmployeeVsWorkLocation',
                dataType: 'json',
                data: { EmployeeGI: EmployeeGI, WorkLocationCode: workLocCode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            fillData();
                        });
                    }
                    else
                        swal.fire(result.message);
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

    $('#workLocationCode_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#workLocation_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');

            return false;

        }
    })


}



function ExportToExcel() {
    var TableLength = $('#EmployeeVsWorkLocationList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/EmployeeVsWorkLocation/ExportToExcel?totRecords=" + TableLength + "&data=" + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}

