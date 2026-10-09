var prevTab = 'pending';
var currentTab = 1;
var valid = 0;
function OnTabClick(Tab, TabIndex) {
    debugger;
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    

    GetList(Tab,TabIndex);
   
    prevTab = Tab;
}

function GetList(Tab,tabindex) {

    $.ajax({
        type: 'GET',
        url: '/DataBank/Suspension/GetList?TabIndex=' + tabindex,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
                if (Tab == 'completed') {
                    $('.suspension_list').addClass('suspension_list_completed');
                    $('.suspension_list_completed').removeClass('suspension_list');
                }
                else {
                    $('.suspension_list_completed').addClass('suspension_list');
                    $('.suspension_list').removeClass('suspension_list_completed');
                }
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}



function onEmployeeClick(IncidentGI,EmpName,EmpId) {

    var EmpNameId = EmpName + ' (' + EmpId + ')';
    window.location.href = '/DataBank/Suspension/GetDetails?IncidentGI=' + IncidentGI + '&EmpName=' + EmpNameId;
    
}

function onSuspensionDateChange(el,spanId) {
    var sFromDate = $('#FromDate').val();
    var sToDate = $('#ToDate').val();
    if (sFromDate != '' && sToDate != '') {
        var formattedFromDate = new Date(sFromDate);
        var fromattedToDate = new Date(sToDate);
        var fFromDate = new Date(formattedFromDate.toLocaleDateString());
        var fToDate = new Date(fromattedToDate.toLocaleDateString());
        var tableLength = $('#SuspensionSalarySplitTable tbody tr').length;
        if (tableLength > 1) {
            $('.nRow').remove();
        }
        else {
            $('#SuspensionSalarySplitTable .form-control').val('');
        }
        if (fFromDate > fToDate) {
            $(el).addClass('is-invalid');
            $(spanId).addClass('error-message');
            $(spanId).html("To date should be greater than From date");
            valid ++;
            return false;
        }
        else {
            $('#FromDate').removeClass('is-invalid');
            $('#sFromDte').removeClass('error-message');
            $('#sFromDte').html("");
            $('#ToDate').removeClass('is-invalid');
            $('#sToDte').removeClass('error-message');
            $('#sToDte').html("");

            InitializeDatePicker(sFromDate, sToDate);
            $('#SuspensionSalarySplit_0__FromDate').val(sFromDate);
            $('#SuspensionSalarySplit_0__ToDate').val(sToDate);
            $('#SuspensionSalarySplit_0__CuttingPercentage').val(100);

            $('#SuspensionSalarySplitTable .form-control').attr('disabled',false);
            $('#SuspensionSalarySplitTable button').attr('disabled',false);
            valid = 0;
            return true;
        }
    }
    else {
        $('#SuspensionSalarySplitTable .form-control').val('');
        $('#SuspensionSalarySplitTable .form-control').attr('disabled', true);
        $('#SuspensionSalarySplitTable button').attr('disabled', true);
    }
}


function AddNewResultRow(tableId) {
    debugger;
    var count = 0;
    count = $('#' + tableId + ' tbody tr[id^="row_OI_"]').length;
    var fDate = $('#SuspensionSalarySplit_' + (count - 1) + '__ToDate').val();
    var date = new Date(fDate);
    date.setDate(date.getDate() + 1);

    var tDate = new Date($('#ToDate').val());
    var fFromDate = new Date(date.toLocaleDateString());
    var fToDate = new Date(tDate.toLocaleDateString());

    var v = 0;
    for (let i = 0; i < count; i++) {
        if (!validateById('#SuspensionSalarySplit_' + i + '__FromDate', 'frmDte_' + i))
            v++;
        if (!validateById('#SuspensionSalarySplit_' + i + '__ToDate', 'toDte_' + i))
            v++;
        if (!validateById('#SuspensionSalarySplit_' + i + '__CuttingPercentage', 'cuttingPer_' + i))
            v++;
        if (!validateById('#SuspensionSalarySplit_' + i + '__AttendanceStatus', 'attnStatus_' + i))
            v++;
    }
    if (v != 0) {
        return;
    }

    if (fFromDate < fToDate) {
    
       
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
                    onchange = onchange.replace(/\(0,/g, '(' + count + ',');
                    debugger
                    $(this).attr('onchange', onchange);
                }
                //---------------------------------------
            });

            $.each(template.find('a'), function () {
                var onclick = $(this).attr('onclick');
                onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
                $(this).attr('onclick', onclick);
            });
            $.each(template.find('button'), function () {
                var onclick = $(this).attr('onclick');
                onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
                $(this).attr('onclick', onclick);
            });
            /*template.find('td[id=grid_slno]').text(count + 1);*/

            $.each(template.find('.addObj'), function () {
                $(this).attr("id", "objAdd_" + count);
            });
            $.each(template.find('.delObj'), function () {
                $(this).attr("id", "objDel_" + count);
            });
            $.each(template.find('#frmDte_0'), function () {
                $(this).attr("id", "frmDte_" + count);
            });
            $.each(template.find('#toDte_0'), function () {
                $(this).attr("id", "toDte_" + count);
            });
            $.each(template.find('#cuttingPer_0'), function () {
                $(this).attr("id", "cuttingPer_" + count);
            });
            $.each(template.find('#attnStatus_0'), function () {
                $(this).attr("id", "attnStatus_" + count);
            });
           
            $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
            //------------to clear when new row---------
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');
            //---------------------------------
            $('#row_OI_'+count).addClass('nRow');//this class is for identifying newly added rows
            //make numbers only inputs.

            $('#objAdd_' + (count - 1)).addClass('hide');
            $('#objDel_' + (count - 1)).addClass('hide');
            $('#objAdd_' + count).removeClass('hide');
            $('#objDel_' + count).removeClass('hide');

            tDate = $('#ToDate').val();
            fDate = _GetFormattedDate(date);
            $('.dtPicker').datepicker('destroy');
            InitializeDatePicker(fDate, tDate);
            $('#SuspensionSalarySplit_'+count+'__FromDate').val(fDate);
            $('#SuspensionSalarySplit_'+count+'__ToDate').val(tDate);
            $('#SuspensionSalarySplit_'+count+'__CuttingPercentage').val(100);
    }
    else
    {
        swal.fire("No further splitting is possible.");
    }

}


function DeleteResultRow(index) {
    debugger;

    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

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
                    //InitializeDatePicker($('#SuspensionSalarySplit_' + (index - 1) + '__FromDate').val(), $('#ToDate').val());
                }
            }
            else {
                $('#SuspensionSalarySplitTable .form-control').val('');


            }
        }
        if ((count - 1) == index) {
            debugger;
            $('#objAdd_' + index).addClass('hide');
            $('#objAdd_' + (index - 1)).removeClass('hide');
            $('#objDel_' + (index - 1)).removeClass('hide');
            $('.dtPicker').datepicker('destroy');
            InitializeDatePicker($('#SuspensionSalarySplit_' + (index - 1) + '__FromDate').val(), $('#ToDate').val());
            //InitializeDatePicker($('#FromDate').val(), $('#ToDate').val());
        }
    });
    
    $('#SuspensionSalarySplit_' + (index - 1) + '__FromDate').removeAttr('disabled');
    $('#SuspensionSalarySplit_' + (index - 1) + '__ToDate').removeAttr('disabled');
    $('#SuspensionSalarySplit_' + (index - 1) + '__CuttingPercentage').removeAttr('disabled');
    $('#SuspensionSalarySplit_' + (index - 1) + '__AttendanceStatus').removeAttr('disabled');
}

function Save() {
    let v = 0;
    if (!validateById('#FromDate', 'sFromDte'))
        v++;
    if (!validateById('#ToDate', 'sToDte'))
        v++;
    var tblLength = $('#SuspensionSalarySplitTable tbody tr').length;
    for (let i = 0; i < tblLength; i++) {
        if (!validateById('#SuspensionSalarySplit_' + i + '__FromDate', 'frmDte_' + i))
            v++;
        if (!validateById('#SuspensionSalarySplit_' + i + '__ToDate', 'toDte_' + i))
            v++;
        if (!validateById('#SuspensionSalarySplit_' + i + '__CuttingPercentage', 'cuttingPer_' + i))
            v++;
        if (!validateById('#SuspensionSalarySplit_' + i + '__AttendanceStatus', 'attnStatus_' + i))
            v++;
    }
    if (!validateById('#Remarks', 'rmks'))
        v++;

    if (valid == 0 && v == 0) {
        var cnt = $('#SuspensionSalarySplitTable tbody tr').length-1;
        if ($('#ToDate').val() == $('#SuspensionSalarySplit_' + cnt + '__ToDate').val()) {
            swal.fire({
                text: "Do you want to Save?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    showSpinner();
                    $.ajax({

                        type: 'POST',
                        url: '/DataBank/Suspension/Save',
                        dataType: 'json',
                        data: $('#SuspensionSave').serialize(),
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire('Saved Successfully').then(function () {
                                    window.location.href = '/DataBank/Suspension/Index';
                                });

                            }

                            else {
                                swal.fire(result.message).then(function () {
                                });
                                hideSpinner();
                            }

                        },
                        error: function (xhr, textStatus, errorThrown) {
                            debugger
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
        else {
            swal.fire("There are still remaining days available for splitting.")
        }
    }

}

function Delete(IncidentGI) {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            showSpinner();
            $.ajax({

                type: 'GET',
                url: '/DataBank/Suspension/Delete',
                dataType: 'json',
                data: { IncidentGI: IncidentGI},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire('Deleted Successfully').then(function () {
                            OnTabClick('completed', 2);
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {
                            
                        });
                    }
                    hideSpinner();
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger
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

function InitializeDatePicker(sDate, eDate) {
    var arrows;
    if (KTUtil.isRTL()) {
        arrows = {
            leftArrow: '<i class="la la-angle-right"></i>',
            rightArrow: '<i class="la la-angle-left"></i>'
        }
    } else {
        arrows = {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        }
    }

    $('.dtPicker, .dtPicker_8_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows,
        endDate: eDate,
        autoclose: true,



        format: "dd-M-yyyy",
        orientation: "auto",
        startDate: sDate,
        clearBtn: true
    });

    // input group layout for modal demo
    $('.dtPicker_8_modal').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",
        templates: arrows
    });
}

function GetTransactionHistoryList() {
    var FromDate = $('#FromDate').val();
    var ToDate = $('#ToDate').val();
    if (FromDate != '' && ToDate != '') {
        $.ajax({
            type: 'GET',
            url: '/DataBank/Suspension/GetTransactionHistoryList?FromDate=' + FromDate + '&ToDate=' + ToDate,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#ListView').html("");
                    $('#ListView').html(result);
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

function ExportToExcel() {
    var TableLength = $('#SuspensionList_Count').val();
    var TabName = 'Pending';
    if (currentTab == 2)
        TabName = 'Completed';
    if (TableLength > 0) {
        url = "/DataBank/Suspension/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + TabName + "&data=" + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}

