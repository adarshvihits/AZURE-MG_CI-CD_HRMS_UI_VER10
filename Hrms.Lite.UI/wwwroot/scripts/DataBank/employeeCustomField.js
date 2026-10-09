function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;    
    var Type = $('#CustomField_CustomFieldCode').val();
    
    var employeestatus = $('#CustomField_EmployeeStatus:checked').val();

    if (Type != '') {


        /*  $('#JoiningReportTable').html('');*/
        var dataTable = $("#EmployeeVsCustomFieldTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/EmployeeCustomField/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + Type + '&employeestatus=' + employeestatus + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {





                    //if (d.data.length != 0) {
                    //    $('.alltab').html(d.data[0].active);
                    //    $('.assgnd').html(d.data[0].ASSIGNED);
                    //    $('.notassgnd').html(d.data[0].NOTASSIGNED);
                    //}
                    //else {
                    //    $('.alltab').html(0);
                    //}     
                    console.log(d); 
                    $('#EmployeeVsCustomFieldList_Count').val(d.recordsTotal);
                    $('#customColumn').html(d.headerName);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].customFieldCode = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                        //d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><input class="form-control form-control-sm"id="correctedamt" placeholder="Amount" maxlength="8" onpaste="return false" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)">'

                        d.data[i].customFieldName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].customFieldName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i +'" class="form-control form-control-sm" placeholder="' + $('#CustomField_CustomFieldCode option:selected').text() + '" maxlength="120" ><span id="newVal_'+i+'"></span></span></div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "name", "width": "20%" },
                { "data": "designation", "width": "17%" },
                { "data": "department", "width": "17%" },
                { "data": "location", "width": "13%" },
                { "data": "customFieldName", "width": "10%" },
                { "data": "customFieldCode", "width": "7%" },

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
            dataTable.search('').draw();
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
function onEditClick(i) {
    debugger    

    $('#newValue_' + i).val($('#hideAmount_' + i).html());

    $('#hideAmount_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');


}
function onCancelClick(i) {
    debugger



    $('#hideAmount_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newValue_' + i).val('');

}

function onUpdateClick(index, EmployeeGI) {
    debugger;
    
    var newValue = $('#newValue_' + index).val();
    var i = 0;
    if (!validateById('#newValue_' + index, 'newVal_' + index)) {
        i++;
    }
        //var url = "/PayRoll/VariableAllowance/EmployeeWiseSave";
        //$('#Variableallowancelist').load(url, { UpdatedAmount: $('#editAmount').val(), Month: Month, Allowance: Allowance }, function () { });
    if (i == 0) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/DataBank/EmployeeCustomField/CustomFieldEmployeeSave',
                    dataType: 'json',
                    data: { CustomFieldValue: newValue, CustomFieldCode: $('#CustomField_CustomFieldCode').val(), EmployeeGI: EmployeeGI },
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                fillData();
                                //var url = "/PayRoll/VariableAllowance/GetList";
                                //$('#Variableallowancelist').load(url, { Month: Month, Allowance: Allowance }, function () { });

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
  


}


function GetCustomFieldPopUp() {
    debugger

    $.ajax({
        type: 'GET',
        url: "/DataBank/EmployeeCustomField/GetCustomFieldList",

        dataType: 'html',

        success: function (Data) {
            debugger;
            var m = $('.customFieldPopUp');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}


function AddNewResultRow(tableId, i) {
    debugger;


    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;



    var template = $('#' + tableId + ' #row_OI_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);


        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });

    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });


    template.find('td[id=grid_slno]').text(count + 1);
    $.each(template.find('.addObj'), function () {
        $(this).attr("id", "objAdd_" + count);
    });
    $.each(template.find('.delObj'), function () {
        $(this).attr("id", "objDel_" + count);
    });
    $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
    //------------to clear when new row---------



    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=hidden]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



    //---------------------------------

    //make numbers only inputs.
    $('.numbers-only').keypress(function (e) {
        if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
    });
    debugger
    $('#objAdd_' + (count - 1)).addClass('hide');
    $('#objAdd_' + count).removeClass('hide');
    $('#objDel_' + count).removeClass('hide');
}




function DeleteResultRow(index) {
    debugger;
    if ($('#CustomFieldList_' + index + '__CustomFieldGI').val() != "") {
        CustomFieldDelete(index);
    }

    else { 
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {



            // if (confirm("Do you want to delete..?"))


            //{
            var count = 0;
            count = $('tr[id^="row_OI_"]').length;
            if (count > 1) {
                $("#row_OI_" + index).remove();
                for (var j = index + 1; j < count; j++) {
                    var template = $('#row_OI_' + j).clone(); // clone the row
                    var code = $("#Results_" + j + "__Name").val();

                    $.each(template.find('input, textarea'), function () {
                        var name = $(this).attr('name');
                        if (typeof name != 'undefined') {
                            name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('name', name);
                        }
                        var id = $(this).attr('id');
                        if (typeof id != 'undefined') {
                            id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                            $(this).attr('id', id);
                        }
                        //---------------------------------For row Increment When onkeyup
                        var onkeyup = $(this).attr('onkeyup');
                        if (onkeyup !== undefined) {
                            onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onkeyup', onkeyup);
                        }
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('select'), function () {
                        var name = $(this).attr('name');
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                        var id = $(this).attr('id');
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                        //---------------------------------------For row Increment When onchange
                        var onchange = $(this).attr('onchange');
                        if (onchange !== undefined) {
                            onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                            $(this).attr('onchange', onchange);
                        }
                        //---------------------------------------
                    });
                    $.each(template.find('button[type=button]'), function () {
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });


                    $.each(template.find('a'), function () {
                        debugger
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });
                    $.each(template.find('.addObj'), function () {

                        $(this).attr("id", "objAdd_" + (j - 1));
                    });

                    template.find('td[id=grid_slno]').text(j);
                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));


                    $("#Results_" + (j - 1) + "__Name").val(code);


                }
            }
            else {
                $("#Results_0__Name").val("");


            }
        }
        if ((count - 1) == index) {
            debugger;
            $('#objAdd_' + index).addClass('hide');
            $('#objAdd_' + (index - 1)).removeClass('hide');
        }
    });
    }
}

function fillCustomFieldDropdown() {

    $.getJSON("/DataBank/EmployeeCustomField/GetCustomFieldDropdown", { }, function (data) {

        $('#CustomField_CustomFieldCode option').remove();
        $('#CustomField_CustomFieldCode').append('<option value="0">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#CustomField_CustomFieldCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        $('#CustomField_CustomFieldCode').val(data[0].value);
    });
}

function CustomFieldSave() {

    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/EmployeeCustomField/CustomFieldSave',
                dataType: 'json',
                data: $('#CustomFieldSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            $('.customFieldPopUp').modal('hide');
                            fillCustomFieldDropdown();
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

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
        }
        else {
            return false;
        }
    })
}

function CustomFieldDelete(i) {

    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/EmployeeCustomField/Delete',
                dataType: 'json',
                data: { CustomFieldGI: $('#CustomFieldList_'+i+'__CustomFieldGI').val()},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            //$('.customFieldPopUp').modal('hide');
                            GetCustomFieldPopUp();
                            fillCustomFieldDropdown();
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

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
        }
        else {
            return false;
        }
    })
}



function ApplyFilter(mode) {
       
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
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}

function ExportToExcel() {
    debugger;    
    var employeestatus = $('#CustomField_EmployeeStatus:checked').val();

    var TableLength = $('#EmployeeVsCustomFieldList_Count').val();
        if (TableLength > 0) {
            url = "/DataBank/EmployeeCustomField/ExportToExcel?CustomFieldCode=" + $('#CustomField_CustomFieldCode').val() + "&CustomFieldName=" + $('#CustomField_CustomFieldCode option:selected').text() + "&employeestatus=" + employeestatus + "&totRecords=" + TableLength + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    
}


function ImportFromExcelPopUp() {
    
        //$('#Excel_FileData').trigger('click');
        var m = $('.bd-example-modal-lg');
    $('#fieldName').html($('#CustomField_CustomFieldCode option:selected').text());
    m.modal('show');


}

function onImportExcelClick() {

    //var i = 0;
    //debugger;
    //if (!validateById('#DeductionMonth_Code', 'Month')) {
    //    i++;
    //}
    //if (!validateById('#Deduction_Code', 'deduction')) {
    //    i++;
    //}
    //if (i == 0) {
    //swal.fire({
    //    text: "Do you want to Upload?",
    //    icon: 'warning',
    //    showCancelButton: true,
    //    confirmButtonColor: '#3085d6',
    //    cancelButtonColor: '#d33',
    //    confirmButtonText: 'Ok'
    //}).then((result) => {
    //    if (result.isConfirmed) {
    var MPPersonalInfo = new FormData($('#EmpCustField')[0]);
    $("#Excel_FileData").val('');
    $.ajax({

        type: 'POST',
        url: '/DataBank/EmployeeCustomField/ImportFromExcel?CustomFieldName=' + $('#CustomField_CustomFieldCode option:selected').text(),
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