var dropdownValues;

function fillData() {
    $.ajaxSetup({ async: false });
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var EntrytypeCode = $('#EntryDataType_Code').val();
    var BulkUploadingTypeCode = $('#BulkUploadingType_Code').val();
    var Selection = $('input[name="radioSelection"]:checked').val();
    if (BulkUploadingTypeCode != '' && (EntrytypeCode == 1 || EntrytypeCode == 2)) {
        $('#ApplicableListView').addClass('hide');
        $('#ListView').removeClass('hide');
        var dataTable = $("#DataBulkUploadingTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/DataBulkUploading/GetList",
                "type": "POST",
                "datatype": "json",
                'data':
                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&BulkUploadingTypeCode=' + BulkUploadingTypeCode + '&radioSelection=' + Selection + '&EntrytypeCode=' + EntrytypeCode + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {    
                    debugger;
                    $('#DataBulkUploadingList_Count').val(d.recordsTotal);
                    $('#customColumn').html(d.headerName);
                    dropdownValues = d.bulkUploadDropDown;
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        if (EntrytypeCode == 1) {
                            d.data[i].bulkUploadingCode = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                        }
                        if (BulkUploadingTypeCode == 5) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onkeypress = "return onlyNumberKey(event)" maxlength="12" onblur="validateAadhar(' + "'newValue_" + i + "'" + ',' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'
                        }
                        else if (BulkUploadingTypeCode == 3 || BulkUploadingTypeCode == 4) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onkeypress = "return onlyNumberKey(event)" maxlength="10" onblur="validateMobileNumber(' + "'newValue_" + i + "'" + ',' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'
                        }
                        else if (BulkUploadingTypeCode == 1 || BulkUploadingTypeCode == 2) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" maxlength="120" onblur="validateEmailID(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'
                        }
                        else if (BulkUploadingTypeCode == 6) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" maxlength="10" onblur="validatePan(' + "'newValue_" + i + "'" + ',' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'
                        }
                        else if (BulkUploadingTypeCode == 7) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" minlength="9" maxlength="18" onkeypress = "return onlyNumberKey(event)"  onchange="onBankAcNoClick(this,' + i + ')" onblur="validate(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'

                        }
                        else if (BulkUploadingTypeCode == 8) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" maxlength="20" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onblur="validate(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'

                        }
                        else if (BulkUploadingTypeCode == 9) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" maxlength="20" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onblur="validate(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'

                        }
                        else if (BulkUploadingTypeCode == 10) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" maxlength="25" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onblur="validate(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'

                        }
                        else if (BulkUploadingTypeCode == 11) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onkeypress = "return onlyNumberKey(event)" maxlength="10" onblur="validateContactNumber(' + "'newValue_" + i + "'" + ',' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'

                        }
                        else if (BulkUploadingTypeCode == 12) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" maxlength="20" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onblur="validate(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'
                        }
                        else if (BulkUploadingTypeCode == 28) {
                            d.data[i].bulkUploadingName = '<div style="position:relative;"><span id="hideAmount_' + i + '">' + d.data[i].bulkUploadingName + '</span ><span id="amtTxtbox_' + i + '" class="hide"><input id="newValue_' + i + '" class="form-control form-control-sm" maxlength="50" placeholder="' + $('#BulkUploadingTypeCode option:selected').text() + '" onpaste="return false" onblur="validate(this,' + "'newVal_" + i + "'" + ')"><span id="newVal_' + i + '"></span></span></div>'

                        }
                        else if (EntrytypeCode == 2) {
                            d.data[i].bulkUploadingName = '<select class="form-control form-control-sm BUtype hide" id="uploadItemCode_' + i + '" name="uploadItemCode[' + i + ']"><option value="0">--Select--</option></select><span id="uploadItem_' + i + '">' + d.data[i].bulkUploadingName + '</span>';
                            d.data[i].bulkUploadingCode = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditDropDownClick(' + i + ',' + "'" + d.data[i].bulkUploadingCode + "'" + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateDropdownClick(' + i + ',' + "'" + d.data[i].employeeGuid + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelDropDownClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'

                        }
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
                { "data": "bulkUploadingName", "width": "10%" },
                { "data": "bulkUploadingCode", "width": "7%" },

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

        //    $('#noDataFound').css("display", 'none');
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

        if (EntrytypeCode == 2) {
            $.fn.DataTable.ext.pager.numbers_length = 5;
            for (var j = 0; j < dropdownValues.length; j++) {
                $('.BUtype').append('<option value="' + dropdownValues[j].code + '">' + dropdownValues[j].name + '</option');
            }
        }

        
    }
    else if (BulkUploadingTypeCode != '' && EntrytypeCode == 3)
    {
        showSpinner();
        $.ajax({
            type: 'GET',
            async:true,
            url: '/DataBank/DataBulkUploading/GetApplicableList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&BulkUploadingTypeCode=" + BulkUploadingTypeCode + "&EntrytypeCode=" + EntrytypeCode + "&radioSelection=" + Selection,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ApplicableListView').html("");
                    $('#ApplicableListView').html(result);
                    $('#ApplicableListView').removeClass('hide');
                    $('#ListView').addClass('hide');
                hideSpinner();
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
}

function onEditDropDownClick(i, bgCode) {
    debugger
    $('#uploadItemCode_' + i + ' option').remove();
    $('#uploadItemCode_' + i).append('<option value="0">--Select --</option');
    for (var j = 0; j < dropdownValues.length; j++) {
        $('.BUtype').append('<option value="' + dropdownValues[j].code + '">' + dropdownValues[j].name + '</option');

    }
    $('#uploadItemCode_' + i).val(bgCode);
    $('#uploadItemCode_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
    $('#uploadItem_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
}

function onCancelDropDownClick(i) {
    $('#uploadItemCode_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#uploadItem_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
}

function onBankAcNoClick(sender,index) {
    debugger;
    var accno = $(sender).val();
    var Accnolength = accno.length;
    if (Accnolength < 9) {
        swal.fire("Account Number should be 9 to 18 numbers");
        document.getElementById('newValue_' + index).value = '';
        !validateById('#newValue_' + index, 'newVal_' + index);
        return false;
    }
}

function OnTypeChange(sender, id) {
    debugger
    var type = $('#EntryDataType_Code option:selected').text();
    var i = 0;
    if (!validateById('#EntryDataType_Code', 'entrytype')) {
        i++;
    }
    
    if (i == 0) {
        $.getJSON("/DataBank/DataBulkUploading/GetBulkUploadingTypeDropDown", { TypeCode: $(sender).val() }, function (data) {
            $('#BulkUploadingType_Code option').remove();

            for (var i = 0; i < data.length; i++) {
                $('#BulkUploadingType_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });
    }
}

function ApplyFilter(mode) {

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

function onUpdateDropdownClick(i, EmployeeGI) {
    debugger;

    var dropdowncode = $('#uploadItemCode_' + i).val();

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
                url: '/DataBank/DataBulkUploading/Save',
                dataType: 'json',
                data: { Value: dropdowncode, Code: $('#BulkUploadingType_Code').val(), EmployeeGI: EmployeeGI },
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

            $('#uploadItemCode_' + i).addClass('hide');
            $('#updatecancel_' + i).addClass('hide');
            $('#uploadItem_' + i).removeClass('hide');
            $('#edit_' + i).removeClass('hide');
            return false;

        }
    })


}

function onUpdateClick(index, EmployeeGI) {
    debugger;

    var newValue = $('#newValue_' + index).val();
   
    var i = 0;
    var BulkUploadingTypeCode = $('#BulkUploadingType_Code').val();
    if (BulkUploadingTypeCode == 7) {
        if (newValue.length < 9) {
            swal.fire("Account Number should be 9 to 18 numbers");
            document.getElementById('newValue_' + index).value = '';
            !validateById('#newValue_' + index, 'newVal_' + index);
            i++;
            return false;
        }
    }
    if (BulkUploadingTypeCode == 5) {
        if (!validateAadhar('newValue_' + index, 'newVal_' + index)) {
            i++;
        }
    }
    else if (BulkUploadingTypeCode == 3 || BulkUploadingTypeCode == 4) {
        if (!validateMobileNumber('newValue_' + index, 'newVal_' + index)) {
            i++;
        }
    }
    else if (BulkUploadingTypeCode == 1 || BulkUploadingTypeCode == 2) {
        if (!validateEmailID(document.getElementById('newValue_' + index), 'newVal_' + index)) {
            i++;
        }
    }
    else if (BulkUploadingTypeCode == 6) {
        if (!validatePan('newValue_' + index, 'newVal_' + index)) {
            i++;
        }
    }
    else {
        if (!validateById('#newValue_' + index, 'newVal_' + index)) {
            i++;
        }
    }
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
                    url: '/DataBank/DataBulkUploading/Save',
                    dataType: 'json',
                    data: { Value: newValue, Code: $('#BulkUploadingType_Code').val(), EmployeeGI: EmployeeGI },
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
}



function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var AppliTableLength = $('#ApplicabilityListTable tr').length;

    var TableLength = $('#DataBulkUploadingList_Count').val();
    if (TableLength > 0 || AppliTableLength > 0) {
        url = "/DataBank/DataBulkUploading/ExportToExcel?Code=" + $('#BulkUploadingType_Code').val() + "&EntryType=" + $('#EntryDataType_Code').val()+"&Name=" + $('#BulkUploadingType_Code option:selected').text() + "&radioSelection=" + $('input[name="radioSelection"]:checked').val() + "&totRecords=" + TableLength + "&data=" + filter;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function ImportFromExcelPopUp() {
    debugger
    var EntrytypeCode = $('#EntryDataType_Code').val();
    var m = $('.bd-example-modal-lg');
    if (EntrytypeCode == 3) {
        var bulkUploadingTypeText = $('#BulkUploadingType_Code option:selected').text();
        var additionalInfo = "(" + bulkUploadingTypeText+" Should be 'Y' OR 'N')";
        $('#fieldName').html(`${bulkUploadingTypeText} ${additionalInfo}`);
    }
    else
        $('#fieldName').html($('#BulkUploadingType_Code option:selected').text());

    m.modal('show');
}

function onImportExcelClick() {

    var MPPersonalInfo = new FormData($('#DataBulkUpload')[0]);
    $("#Excel_FileData").val('');
    $.ajax({       
        type: 'POST',
        url: '/DataBank/DataBulkUploading/ImportFromExcel?BulkUploadingName=' + $('#BulkUploadingType_Code option:selected').text(),
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

function onSaveApplicabilityClick() {
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
                url: '/DataBank/DataBulkUploading/ApplicabilitySave',
                dataType: 'json',
                data: $('#EmpList').serialize(),
                success: function (result) {
                    if (result.success == true) {

                        swal.fire(result.message).then(function () {
                            fillData()
                        });
                    }

                    else {
                        swal.fire(result.message).then(function () {
                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}


function searchRows() {
    debugger;
    var input, filter, table, tr, tdID, tdName, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = $("#Tabindex").val();
    input = document.getElementById("dataTableSearch");
    filter = input.value.toUpperCase();
    table = document.getElementById("ApplicabilityListTable");
    tr = table.getElementsByTagName("tr");
    var tbody = table.getElementsByTagName("tbody")[0];
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 1; i < tr.length; i++) {
        tdID = tr[i].getElementsByTagName("td")[0];
        tdName = tr[i].getElementsByTagName("td")[1];

        if (spannedRows > 0) {
            tr[i].style.display = show ? "" : "none";
            spannedRows--;
            if (!show) noOfRows--;
        } else if (tdID || tdName) {
            let idMatch = tdID && tdID.innerHTML.toUpperCase().indexOf(filter) > -1;
            let nameMatch = tdName && tdName.innerHTML.toUpperCase().indexOf(filter) > -1;

            if (idMatch || nameMatch) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            let rs = tdID ? tdID.getAttribute("rowspan") : null;
            if (rs && rs > 1) {
                show = idMatch || nameMatch;
                spannedRows = rs - 1;
            }
        }
    }

    if (noOfRows == 1) {
        $(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='8'><div style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div></td></tr>");
    }
}