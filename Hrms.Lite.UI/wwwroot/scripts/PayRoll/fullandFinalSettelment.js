var adjstShrtFalldaysPrev = '';
var encashedLeavesPrev = '';
function onEmpClick(EmployeeGI, i) {

    debugger
    var Month = $('#Month_Code').val();
    if ($('#EmployeeList_' + i + '__AttnProcessingValidation').val() == 1 && $('#EmployeeList_' + i + '__SalProcessingValidation').val() == 1) {
        swal.fire("Please process attendance and salary to proceed with Full and Final Settlement")
    }
    //else if ($('#EmployeeList_' + i + '__SalProcessingValidation').val() == 1) {
    //    swal.fire("Please process the salary to proceed with Full and Final Settlement")
    //}
    else {

        var url = "/PayRoll/FullandFinalSettelment/Create?EmployeeGI=" + EmployeeGI + "&Month=" + Month;
        window.location.href = url;
    }

}

function GetMonth()
{

    $.getJSON("/PayRoll/FullandFinalSettelment/GetMonth", { PayrollGrpCode: $('#PayrollGroup_Code').val() }, function (data) {

        $('#PayrollMonth_Code option').remove();
        $('#PayrollMonth_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}

function AddRowGrid(tableId, i) {
    debugger;


    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
    if (count != 5) {
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
            onclick = onclick.replace(/\,0\)/g, ',' + count + ')');            
            $(this).attr('onclick', onclick);
        });




        template.find('td[id=oi_slno]').text(count + 1);
        $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
        //------------to clear when new row---------



        $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



        //---------------------------------

        //make numbers only inputs.
        $('.numbers-only').keypress(function (e) {
            if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
        });
    }
    else {
        swal.fire('Limit Exceeded.Maximum level of point criteria is 5');
    }
}

function DeleteRw(tableId,index) {
    debugger;
    if (confirm("Do you want to delete..?")) {
        var count = 0;
        //count = $('tr[id^="row_OI_"]').length;
        count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
        if (count > 1) {
            //$("#row_OI_" + index).remove();
            //$('#' + tableId + ' tr[id^="row_OI_"' + index+']').remove();
            $('#' + tableId + ' tr[id^="row_OI_'+index+'"]').remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').clone(); // clone the row
                var code = $("#Details_OtherAdditions_" + j + "__Narration").val();
                var desc = $("#Details_OtherAdditions_" + j + "__Amount").val();

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
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});
                //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
                //    dateFormat: "dd-M-yy", changeMonth: true,
                //    changeYear: true
                //});

                $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').html(template.html());
                $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').attr('id', 'row_OI_' + (j - 1));
                $("#Details_OtherAdditions_" + (j - 1) + "__Narration").val(code); 
                $("#Details_OtherAdditions_" + (j - 1) + "__Amount").val(desc);

            }
        }
        else {
            $("#Details_OtherAdditions__0__Narration").val("");
            $("#Details_OtherAdditions__0__Amount").val("");

        }
    }
}

function DeleteRw1(tableId,index) {
    debugger;
    if (confirm("Do you want to delete..?")) {
        var count = 0;
        count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
        if (count > 1) {
            $('#' + tableId + ' tr[id^="row_OI_' + index + '"]').remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').clone(); // clone the row
                var code = $("#Details_OtherDeduction_" + j + "__Narration").val();
                var desc = $("#Details_OtherDeduction_" + j + "__Amount").val();

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
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});
                //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
                //    dateFormat: "dd-M-yy", changeMonth: true,
                //    changeYear: true
                //});

                $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').html(template.html());
                $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').attr('id', 'row_OI_' + (j - 1));
                $("#Details_OtherDeduction_" + (j - 1) + "__Narration").val(code);
                $("#Details_OtherDeduction_" + (j - 1) + "__Amount").val(desc);

            }
        }
        else {
            $("#Details_OtherDeduction__0__Narration").val("");
            $("#Details_OtherDeduction__0__Amount").val("");

        }
    }
}

function CalculateNoticePeriodDeductionAfterAdjustment() {
    var adjstShrtFalldays = parseFloat($('#Details_AdjustedShortfallDays').val());

    var shrtFalldays = parseFloat($('#Details_AdjustedShortfallDays').val());
    var NewNoticePeriodDeductionAfterAdjustment = parseFloat($('#Details_ShortfallDays').val()) - adjstShrtFalldays;

    if (NewNoticePeriodDeductionAfterAdjustment < 0)
        NewNoticePeriodDeductionAfterAdjustment = 0;

    $('#NoticePeriodDeductionAfterAdjustmentDiv').html(NewNoticePeriodDeductionAfterAdjustment);
    //if (adjstShrtFalldays != adjstShrtFalldaysPrev)
    //    $('.processBtn').attr("disabled", false);
    //adjstShrtFalldaysPrev = adjstShrtFalldays;

}

function processBtnDisable() {
    debugger;
    var adjstShrtFalldays = parseFloat($('#Details_AdjustedShortfallDays').val());
    var encashedLeaves = $('#Details_TotalEncashmentLeaves').val();
    if (adjstShrtFalldays != adjstShrtFalldaysPrev || encashedLeaves != encashedLeavesPrev)
        $('.processBtn').attr("disabled", false);
    else
        $('.processBtn').attr("disabled", true);
}

function processClick() {
    debugger;
    var encashedLeaves = $('#Details_TotalEncashmentLeaves').val();
    var adjstShrtFalldays = parseFloat($('#Details_AdjustedShortfallDays').val());
    var shrtFalldays = parseFloat($('#Details_AdjustedShortfallDays').val());
    var NewNoticePeriodDeductionAfterAdjustment = parseInt($('#Details_ShortfallDays').val()) - adjstShrtFalldays;
    adjstShrtFalldaysPrev = adjstShrtFalldays;
    encashedLeavesPrev = encashedLeaves;
    if (NewNoticePeriodDeductionAfterAdjustment < 0)
        NewNoticePeriodDeductionAfterAdjustment = 0;

    $('#NoticePeriodDeductionAfterAdjustmentDiv').html(NewNoticePeriodDeductionAfterAdjustment);
    $('#Details_NoticePeriodDeductionAfterAdjustment').val(NewNoticePeriodDeductionAfterAdjustment);
    $.getJSON("/PayRoll/FullandFinalSettelment/GetDetailsProcess", { EmployeeGI: $('#EmployeeGI').val(), NoticeDedDays: NewNoticePeriodDeductionAfterAdjustment, EncashmentLeaves: encashedLeaves }, function (data) {
        debugger;
        //console.log(data);
        $('#Encash').html(data.leaveEncashment);
        $('#npded').html(data.noticePeriodDeduction);
        $('#Details_LeaveEncashment').val(data.leaveEncashment);
        $('#Details_NoticePeriodDeduction').val(data.noticePeriodDeduction);
        $('.processBtn').attr("disabled", true);
        finalSettelment();
    });
}
function finalSettelment() {
    debugger;
    //var TotAmt = $("#Details_FinalSettlementAmountPayable").val();
    //alert("Total:" + TotAmt);
    var additionTotal = 0.0;
    var deductionTotal = 0.0;
    var gratuity = parseInt($('#Details_Gratuity').val());
    if (isNaN(gratuity)) {
        gratuity = parseInt("0");
    }
    let finalSettlementAmt = (parseInt($('#Details_HeldAmount').val()) + parseInt($('#Details_LeaveEncashment').val()) + gratuity)
    $('.othersettelmentval').each(function (i, obj) {
        var TotAmt = parseFloat($("#Details_OtherAdditions_" + i + "__Amount").val());
        debugger;
        if (isNaN(TotAmt)) {
            TotAmt = parseFloat("0");
        }
        additionTotal = additionTotal + TotAmt;
        /*alert(additionTotal);*/

    });
    $('.othersettelmentvaldeduction').each(function (i, obj) {
        /* alert(i);*/
        var DedAmt = parseFloat($("#Details_OtherDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(DedAmt)) {
            DedAmt = parseFloat("0");
        }
        /*TotAmt = TotAmt - Amt;*/
        deductionTotal = deductionTotal + DedAmt;
        /*alert(deductionTotal);*/
    });
 

   
    finalSettlementAmt = parseInt(finalSettlementAmt) + (additionTotal - deductionTotal);
    if (isNaN(finalSettlementAmt)) {
        finalSettlementAmt = parseFloat("0");
    }
    if (finalSettlementAmt < 0)
    {
        $('.finalSettlementLabel').html('Amount to be Recovered');
        $("#finalsettamt").addClass('span_red');
    }
    else {
        $('.finalSettlementLabel').html('Final Settlement Amount Payable');
        $("#finalsettamt").removeClass('span_red');
    }
    $("#finalsettamt").html(finalSettlementAmt);
    $("#Details_FinalSettlementAmountPayable").val(finalSettlementAmt);
}
function FullandFinalSettelmentSave() {
    var i = 0;
    var AllwTableLength = $('#AllowanceTable tbody tr').length;
    var DedTableLength = $('#AllowanceTable tbody tr').length;
    if (!validateById('#Details_Remarks', 'ffRemarks')) {
        i++;
    }
    for (j = 0; j < AllwTableLength; j++) {
        if ($('#Details_OtherAdditions_' + j + '__Amount').val() != "") {
            if (!validateById('#Details_OtherAdditions_' + j + '__Narration', 'additionname_' + j)) {
                i++;
            }
        }
        if ($('#Details_OtherAdditions_' + j + '__Narration').val() != "") {
            if (!validateById('#Details_OtherAdditions_' + j + '__Amount', 'additionamount_' + j)) {
                i++;
            }
        }
    }
    for (j = 0; j < DedTableLength; j++) {
        if ($('#Details_OtherDeduction_' + j + '__Amount').val() != "") {
            if (!validateById('#Details_OtherDeduction_' + j + '__Narration', 'deductionname_' + j)) {
                i++;
            }
        }
        if ($('#Details_OtherDeduction_' + j + '__Narration').val() != "") {
            if (!validateById('#Details_OtherDeduction_' + j + '__Amount', 'deductionamount_' + j)) {
                i++;
            }
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
                $('#LeaveEncashment').val($('#Encash').val());
                $('#NoticePeriodDeduction').val($('#npded').val());
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/FullandFinalSettelment/SaveSettelment',
                    dataType: 'json',
                    data: $('#SaveSettelment').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {


                                var url = "/PayRoll/FullandFinalSettelment/Index";
                                window.location.href = url;

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

function finalsettelmentSum() {
    debugger;
    var TotAmt = parseFloat("0");
    $('.othersettelmentval').each(function (i, obj) {
        var Amt = parseFloat($("#Details_OtherAdditions_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;
       
    });
    $("#finalsettamt").html(TotAmt);
    $("#Details_FinalSettlementAmountPayable").val(TotAmt);

}


function finalsettelmentDeduction() {
    debugger;
    var TotAmt = $("#Details_FinalSettlementAmountPayable").val();
    $('.othersettelmentvaldeduction').each(function (i, obj) {
        var Amt = parseFloat($("#Details_OtherDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt - Amt;

    });
    $("#finalsettamt").html(TotAmt);
}

function ApplyFilter(mode) {
    debugger
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var Month = $('#Month_Code').val();
    if (PayrollGroup != '' || Month != '') {


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: "/PayRoll/FullandFinalSettelment/ApplyFilter?PayrollGroup=" + PayrollGroup + "&Month=" + Month,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#EmployeeList').html("");
                        $('#EmployeeList').html(result);

                        $('#EmployeeList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: "/PayRoll/FullandFinalSettelment/ApplyFilter?PayrollGroup=" + PayrollGroup + "&Month=" + Month,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#EmployeeList').html("");
                        $('#EmployeeList').html(result);

                        $('#EmployeeList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: "/PayRoll/FullandFinalSettelment/ApplyFilter?PayrollGroup=" + PayrollGroup + "&Month=" + Month,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger

                //CloseSlider('assetAllocationFilter');

                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#EmployeeList').html("");
                    $('#EmployeeList').html(result);

                    $('#EmployeeList').removeClass('hide');
                    //$('#rewardsListPartial').addClass('hide');
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
        }
    }
}

function onExportToExcelClick() {
    debugger;
    var PayrollGroup = $('#PayrollGroup_Code').val();
    var Month = $('#Month_Code').val();
    var TableLength = $('#EmployeeList_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/FullandFinalSettelment/ExportToExcel?PayrollGroup=" + PayrollGroup + "&Month=" + Month;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}