function onMonthChange(sender, id) {
    debugger;
    monthcode = $(sender).val();
    if (monthcode == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    debugger;
    $.getJSON("/PayRoll/PreviousMonthAdjustments/GetEffectiveMonth", { id: monthcode }, function (data) {

        $('.effMonth option').remove();

        $('.effMonth').append(('<option value>--Select--</option'));


        for (var i = 0; i < data.length; i++) {
            debugger
            $('.effMonth').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}



function onPayrollGroupChange(sender, id) {
    debugger;
    payrollGroup = $(sender).val();
    if (payrollGroup == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    debugger;
    $.getJSON("/PayRoll/PreviousMonthAdjustments/GetProcessingMonth", { id: payrollGroup }, function (data) {

        $('.processMnth option').remove();

        $('.processMnth').append(('<option value>--Select--</option'));


        for (var i = 0; i < data.length; i++) {
            debugger
            $('.processMnth').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}




function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var adjtype = $('input[name="AdjustmentType"]:checked').val();


    var PayrollGroup = $('#PayrollGroup_Code').val();
    var ProcessingMonth = $('#ProcessingMonth_Code').val();
    var EffectiveMonth = $('#EffectiveMonth_Code').val();
    if (adjtype=="LOP")
        $("#amountLabel").html("LOP Days");
    else
        $("#amountLabel").html("Arrear Days");

    var dataTable = $("#PrevMonthAdjustTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PreviousMonthAdjustments/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&PayrollGroup=' + PayrollGroup + '&ProcessingMonth=' + ProcessingMonth + '&EffectiveMonth=' + EffectiveMonth
                        + "&AdjustmentType=" + adjtype+'&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger;
                $('#PrevMonthAdjList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].days = '<span id = "amtLabel_' + i + '">' + d.data[i].days + '</span><span id="amtTxtbox_' + i + '" class="hide"><input type = "text" id = "newAmount_' + i + '" class="form-control form-control-sm" placeholder = "Days" maxlength = 8 onkeypress = "return onlyAmount(event)"></span>'
                    d.data[i].remarks = '<span id = "rmkLabel_' + i + '" >' + d.data[i].remarks + '</span><span style="position:relative;" id="rmkTxtbox_' + i + '" class="hide"><input type = "text" id = "rmk_' + i + '" class="form-control form-control-sm" placeholder = "Remarks" onblur="validate(this,' + "'" + 'rmkSpan_' + i + "'" + ')"><span id="rmkSpan_' + i +'"></span></span>'


                    d.data[i].edit = '<span id="editbt_' + i + '"><a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation.name" },
            { "data": "department.name" },
            { "data": "location.name" },      
            { "data": "days" },
            { "data": "remarks" },
            { "data": "edit" },






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
    $('#newAmount_' + i).val($('#amtLabel_'+i).text());
    $('#rmk_' + i).val($('#rmkLabel_'+i).text());
    $('#amtLabel_' + i).addClass('hide');
    $('#rmkLabel_' + i).addClass('hide');
    $('#editbt_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#rmkTxtbox_' + i).removeClass('hide');
    $('#updateCancel_' + i).removeClass('hide');
}



function onUpdateClick(i, EmployeeGI) {
    debugger
    
    var EmployeeGIValue = EmployeeGI;
    var days = $('#newAmount_' + i).val()
    var Remarks = $('#rmk_' + i).val()
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var ProcessingMonth = $('#ProcessingMonth_Code').val();
    var EffectiveMonth = $('#EffectiveMonth_Code').val();
    var adjtype = $('input[name="AdjustmentType"]:checked').val();

    if (days == "" || days == null) {
        swal.fire("Days should not be null");
       fillData();
       return false;
    }
    var index = 0;
    if (!validateById('#rmk_' + i,'rmkSpan_'+i)) {       
        index++;
    }
    debugger
    if (index == 0) {
        $('#PrevMonthAdjList_' + i + '__days').val($('#newAmount_' + i).val());
        $('#PrevMonthAdjList_' + i + '__remarks').val($('#rmk_' + i).val());
        $('#amtLabel_' + i).html($('#newAmount_' + i).val());
        $('#rmkLabel_' + i).html($('#rmk_' + i).val());
        $('#amtLabel_' + i).removeClass('hide');
        $('#editbt_' + i).removeClass('hide');
        $('#amtTxtbox_' + i).addClass('hide');
        $('#rmkTxtbox_' + i).addClass('hide');
        $('#updateCancel_' + i).addClass('hide');

        $.ajax({
            type: 'POST',
            url: "/PayRoll/PreviousMonthAdjustments/EmployeeWiseSave?days=" + days + "&Remarks=" + Remarks + " &PayrollGroup=" + PayrollGroup + "&ProcessingMonth=" +
                ProcessingMonth + "&EffectiveMonth=" + EffectiveMonth + "&AdjustmentType=" + adjtype + "&EmployeeGI=" + EmployeeGI,
            dataType: 'json',
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
    
}


function onCancelClick(i) {
    $('#amtLabel_' + i).removeClass('hide');
    $('#rmkLabel_' + i).removeClass('hide');
    $('#editbt_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#rmkTxtbox_' + i).addClass('hide');
    $('#updateCancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);
    $('#rmk_' + i).val(0);
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
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        CloseSlider('basicFilter');
    }
}
function ExportToExcel() {

    debugger;
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var ProcessingMonth = $('#ProcessingMonth_Code').val();
    var EffectiveMonth = $('#EffectiveMonth_Code').val();

    var PayrollGroupName = $('#PayrollGroup_Code option:selected').text();
    var ProcessingMonthName = $('#ProcessingMonth_Code option:selected').text();
    var EffectiveMonthName = $('#EffectiveMonth_Code option:selected').text();



    var TableLength = $('#PrevMonthAdjList_Count').val();

    if (TableLength > 0) {
        url = "/PayRoll/PreviousMonthAdjustments/ExportToExcel?PayrollGroup=" + PayrollGroup + "&ProcessingMonth=" + ProcessingMonth + "&EffectiveMonth=" + EffectiveMonth
            + "&PayrollGroupName=" + PayrollGroupName + "&ProcessingMonthName=" + ProcessingMonthName + "&EffectiveMonthName=" + EffectiveMonthName +
            "&AdjustmentType=" + $('input[name="AdjustmentType"]:checked').val() + "&totalRecord=" + TableLength  + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function validatePreviousMonthAdjustmentsDropdowns() {
    var i = 0;
    debugger;
        var m = $('.bd-example-modal-lg');
        m.modal('show');
}


function onImportExcelClick() {


    var MPPersonalInfo = new FormData($('#prevMonthAdjst')[0]);
    $("#Excel_FileData").val('');
    $.ajax({

        type: 'POST',
        url: '/PayRoll/PreviousMonthAdjustments/ImportFromExcel',
        contentType: false,
        processData: false,
        data: MPPersonalInfo,
        success: function (result) {
            debugger;
            if (result.success == true) {
                $("#Excel_FileData").val('');
                swal.fire("Uploaded Successfully").then(function () {
                    fillData()
                    var m = $('.bd-example-modal-lg');
                    m.modal('hide');
                });
            }
            else {
                $("#Excel_FileData").val('');
                if (result.message != null) {
                    $('#Unmatched').html('')
                    swal.fire(result.message);
                }
                else {
                    $('#Unmatched').html(result)
                }
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