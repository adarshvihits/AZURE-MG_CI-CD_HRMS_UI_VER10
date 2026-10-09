
function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;

    var MonthCode = $('#AllowanceMonth_Code').val();
    var AllowanceCode = $('#Allowance_Code').val();

    var dataTable = $("#variable_Provision_And_Perk_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/VariableProvisionsAndPerk/GetVariableProvisionsAndPerkList",
            "type": "POST",

            "datatype": "json",
            'data':

                function (d) {
                    temp = '';

                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();

                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + MonthCode + '&AllowanceCode=' + AllowanceCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                debugger;

                $('#VariableAllowanceList_Count').val(d.recordsTotal);
                $('#totEmpCnt').html(d.recordsTotal);
                if (d.data != "") {
                    $('#empWithAmt').html(d.data[0].employeeswithAmount);
                    $('#totAmt').html(d.data[0].totalAmount);
                    $('#tottdsAmt').html(d.data[0].totalTDSAmount);
                }
                if ($('#VariableAllowanceList_Count').val() == '0')
                    $('.textDisplay').addClass('footerDiv');
                else
                    $('.textDisplay').removeClass('footerDiv');

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].mode = '<span id="edit_' + i + '"> <a onmouseenter="tooltipHover()" onclick = "onEditClick(' + i +')"  data-bs-toggle="tooltip" title = "Edit" type = "button" class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updatecancel_' + i + '"class="hide"><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a onmouseenter="tooltipHover()" type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'
                    d.data[i].remark = '<span id="hideRemark_' + i + '">' + d.data[i].remark + '</span ><span id="remarkTxtbox_' + i + '"class="hide"><input id="newRemark_' + i + '" class="form-control form-control-sm" placeholder="Remark" ></span>'

                    if (d.data[i].amount != null)
                        d.data[i].amount = '<span id="hideAmount_' + i + '">' + d.data[i].amount + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)"></span>'
                    else
                        d.data[i].amount = '<span id="hideAmount_' + i + '">' + '' + '</span ><span id="amtTxtbox_' + i + '"class="hide"><input id="newAmount_' + i + '" class="form-control form-control-sm" placeholder="Amount" maxlength="8" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)"></span>'

                    if (d.data[i].tdsAmount != null)
                        d.data[i].tdsAmount = '<span id="hideTDSAmount_' + i + '">' + d.data[i].tdsAmount + '</span ><span id="tdsamtTxtbox_' + i + '"class="hide"><input id="newTDSAmount_' + i + '" class="form-control form-control-sm" placeholder="TDSAmount" maxlength="8" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)"></span>'
                    else
                        d.data[i].tdsAmount = '<span id="hideTDSAmount_' + i + '">' + '' + '</span ><span id="tdsamtTxtbox_' + i + '"class="hide"><input id="newTDSAmount_' + i + '" class="form-control form-control-sm" placeholder="TDSAmount" maxlength="8" onkeypress = "return onlyNumberKey(event)" oninput = "process(this)"></span>'

                }
                console.log(d.data);
                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation.name" },
            { "data": "department.name" },
            { "data": "location.name" },
            { "data": "remark" },
            { "data": "amount" },
            { "data": "tdsAmount" },
            { "data": "mode" },

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
 
}
function onEditClick(i) {
    debugger
 
    $('#newAmount_' + i).val($('#hideAmount_' + i).text());
    $('#hideAmount_' + i).addClass('hide');
    $('#newTDSAmount_' + i).val($('#hideTDSAmount_' + i).text());
    $('#hideTDSAmount_' + i).addClass('hide');
    $('#edit_' + i).addClass('hide');
    $('#amtTxtbox_' + i).removeClass('hide');
    $('#tdsamtTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
    $('#newRemark_' + i).val($('#hideRemark_' + i).text());
    $('#hideRemark_' + i).addClass('hide');
    $('#remarkTxtbox_' + i).removeClass('hide');

}


function onUpdateClick(index, EmployeeGI) {
    debugger;
    var UpdatedAmount = $('#newAmount_' + index).val()
    var UpdatedTDSAmount = $('#newTDSAmount_' + index).val()

    var Month = $('#AllowanceMonth_Code').val();
    var Remark = $('#newRemark_' + index).val()
    var Allowance = $('#Allowance_Code').val();

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
        url: '/PayRoll/VariableProvisionsAndPerk/EmployeeWiseUpdate',
        dataType: 'json',
        data: { TDSUpdatedAmount: UpdatedTDSAmount, UpdatedAmount: UpdatedAmount, allowanceMonth: Month, allowanceType: Allowance, EmployeeGI: EmployeeGI, Remark: Remark },
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
function onCancelClick(i) {
    debugger
    


    $('#hideAmount_' + i).removeClass('hide');
    $('#hideTDSAmount_' + i).removeClass('hide');
    $('#edit_' + i).removeClass('hide');
    $('#amtTxtbox_' + i).addClass('hide');
    $('#tdsamtTxtbox_' + i).addClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#newAmount_' + i).val(0);
    $('#newTDSAmount_' + i).val(0);
    $('#hideRemark_' + i).removeClass('hide');
    $('#remarkTxtbox_' + i).addClass('hide');
    $('#newRemark_' + i).val(0);

}
function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#AllowanceMonth_Code', 'MonthAllowance')) {
        i++;
    }
    if (!validateById('#Allowance_Code', 'TypeAllowance')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;

}

function clickOnBatchUpdate() {
    debugger
    preventENterFlag = 0;
    MonthName = $('#AllowanceMonth_Code option:selected').text();
    AllowanceName = $('#Allowance_Code option:selected').text();
    Month = $('#AllowanceMonth_Code').val();
    Allowance = $('#Allowance_Code').val();

    if (validateDropdowns()) {
      
        $.ajax({

            type: 'GET',
            url: '/PayRoll/VariableProvisionsAndPerk/GetBatchUpdateSlider',
            dataType: 'html',
            data: { MonthCode: Month, AllowanceCode: Allowance, MonthName: MonthName, AllowanceName: AllowanceName },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#batchUpdate').html("");
                    $('#batchUpdate').html(result);
                }

                hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
}


function validateAmount() {
    debugger;
    var i = 0;

    if (!validateById('#Amount', 'BatchAmt')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function CopypasteValidation(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

function BatchUpdateClick() {
    debugger;
    if (validateAmount()) {
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
                    url: '/PayRoll/VariableProvisionsAndPerk/BatchUpdate',
                    dataType: 'json',
                    data: $('#BatchUpdate_Allowance_Provisions_Perk').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire("Updated Successfully").then(function () {
                                fillData();
                                CloseSlider('batchUpdateDiv');
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
    else {
        return true;
    }



}

function getDataSetCountAJAX(type) {
    debugger;
    $.ajax({
        url: '/PayRoll/VariableProvisionsAndPerk/CountCheckAJAX',
        type: "POST",
        async: false,
        dataType: 'json',
        data: { Month: $('#AllowanceMonth_Code').val(), Allowance: $('#Allowance_Code').val(), totRecords: $('#VariableAllowanceList_Count').val() ,Type:type},
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (result == true) {
                    debugger;
                    if (type =="Excel")
                        ExportToExcel(type);
                    else
                        ExportToBankStatement(type)
                }
                else {
                    swal.fire("No Data Found");
                    return false;

                }

            }


        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });

}
function ExportToExcel(type) {
    debugger;
    if (validateDropdowns()) {

        var TableLength = $('#VariableAllowanceList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/VariableProvisionsAndPerk/ExportToExcel?Month=" + $('#AllowanceMonth_Code').val() + "&Allowance=" + $('#Allowance_Code').val() + "&AllowanceMonth=" + $('#AllowanceMonth_Code option:selected').text() + "&AllowanceType=" + $('#Allowance_Code option:selected').text() + "&totRecords=" + TableLength +"&Type="+type;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}



function ExportToBankStatement(type) {
    debugger;
    if (validateDropdowns()) {

        var TableLength = $('#VariableAllowanceList_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/VariableProvisionsAndPerk/ExportToBankStatement?Month=" + $('#AllowanceMonth_Code').val() + "&Allowance=" + $('#Allowance_Code').val() + "&AllowanceMonth=" + $('#AllowanceMonth_Code option:selected').text() + "&AllowanceType=" + $('#Allowance_Code option:selected').text() + "&totRecords=" + TableLength + "&Type=" + type;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

function validateAllowanceDropdowns() {
    var i = 0;
    var checkbox = document.getElementById('append');
    checkbox.checked = false;

    debugger;
    if (!validateById('#AllowanceMonth_Code', 'MonthAllowance')) {
        i++;
    }
    if (!validateById('#Allowance_Code', 'TypeAllowance')) {
        i++;
    }
    if (i == 0) {
        /*$('#Excel_FileData').trigger('click')*/
        var m = $('.bd-example-modal-lg');
        m.modal('show');

    }

}

function onImportExcelClick() {

    var checkbox = document.getElementById('append');
    var checkValue = 0;
    if (checkbox.checked) checkValue = 1;

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#variableProvisionsUpload')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/PayRoll/VariableProvisionsAndPerk/ImportFromExcel?Allowance=' + $('#Allowance_Code option:selected').text() + '&Append=' + checkValue,
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                            
                            fillData();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
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
        else {
            return false;
        }
    })

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
    }
}
