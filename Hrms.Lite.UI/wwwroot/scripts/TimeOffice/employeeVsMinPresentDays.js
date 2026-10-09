


function fillData() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var tabindex = $('input[name="Status"]:checked').val();
    var dataTable = $("#emp_DaysTable").DataTable({
        //"paging": true,
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/EmployeeVsMinPresentDays/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var searchId = $('#Filter_Code').val();
                    var start = d.start;
                    debugger;
                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' +searchValue + '&TabIndex=' + tabindex + '&dummy=dummy';                  
                    d.filter = temp;
                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#EmployeeVsMinPresentDaysList_Count').val(d.recordsTotal);
              
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    if (d.data[i].presentDays!=null)
                        d.data[i].presentDays = '<span id = "amtLabel_' + i + '">' + d.data[i].presentDays + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Present Days" maxlength =2 onkeypress = "return onlyNumberKey(event)"></span>'
                    else
                        d.data[i].presentDays = '<span id = "amtLabel_' + i + '">'  + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Present Days" maxlength =2 onkeypress = "return onlyNumberKey(event)"></span>'

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    
                    d.data[i].action = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit"  class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()"  data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'

                }

                return d.data;
            }

        },



        "columns": [

            { "data": "employeeId" },
            { "data": "employeeName" },
            { "data": "designationName" },
            { "data": "departmentName" },
            { "data": "locationName" },
            { "data": "doj" },
            { "data": "presentDays" },
            { "data": "action" }
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




function ApplyFilter(mode) {
    debugger;


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



function onEditClick(i) {
    debugger
    $('#newAmount_' + i).val($("#amtLabel_"+i).html());
    $('#amtLabel_' + i).addClass('hide');
    $('#editbt_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#updateCancel_' + i).removeClass('hide');
   
}




function onCancelClick(i) {
    $('#amtLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);
}

function onUpdateClick(i, EmployeeGI) {
    debugger 
    var PresentDays = $('#newAmount_' + i).val();
    debugger
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/EmployeeVsMinPresentDays/Save',
        dataType: 'json',
        data: { EmployeeGI: EmployeeGI, PresentDays: PresentDays },
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
            debugger
            if ($($.parseHTML(xhr)).filter(".Login_Session").length > 0) {
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
    var searchValue = $('#dataTableSearch').val();
    var _TabIndex = $('input[name="Status"]:checked').val();
    var TabIndex=$("input[name='Status']:checked").parent('label').text();
    var filter = $('#BasicFilter').serialize();

    var TableLength = $('#EmployeeVsMinPresentDaysList_Count').val();
    var PARAM = 'data=' + filter + '&_TabIndex=' + _TabIndex + '&TabIndex=' + TabIndex + '&searchValue=' + searchValue;
        if (TableLength > 0) {
            url = '/TimeOffice/EmployeeVsMinPresentDays/ExportToExcel?'+ PARAM + "&TotalRows=" + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}