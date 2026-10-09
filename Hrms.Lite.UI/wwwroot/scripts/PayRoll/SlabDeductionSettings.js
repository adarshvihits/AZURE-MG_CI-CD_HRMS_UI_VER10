var PtSubmit = true;
function checkAllPtAmt() {
    debugger;
    var count = $('#SlabDeductionSettingsList_Count').val();
    for (let i = 0; i < count; i++) {
        var currAmt = parseFloat($('#SlabDeductionSettingsList_' + i + '__RateAmount').val());
        var nxtAmt = parseFloat($('#SlabDeductionSettingsList_' + (parseInt(i) + 1) + '__RateAmount').val());
        if (currAmt > nxtAmt) {
            $('#SlabDeductionSettingsList_' + (parseInt(i) + 1) + '__RateAmount').addClass('is-invalid');
            $('#Dedamount_' + (parseInt(i) + 1)).addClass('error-message');
            $('#Dedamount_' + (parseInt(i) + 1)).html('Check the PT amount. It is less than the above row');
            PtSubmit = false;
            return;
        }
        else {
            $('#SlabDeductionSettingsList_' + (parseInt(i) + 1) + '__RateAmount').removeClass('is-invalid');
            $('#Dedamount_' + (parseInt(i) + 1)).removeClass('error-message');
            $('#Dedamount_' + (parseInt(i) + 1)).html('');
            PtSubmit = true;
        }
    }
}











function GetSettingsGrid() {
    debugger;
    var Deduction = $('#DeductionCode').val();
    if (Deduction == '')
        Deduction = 0;

    $.ajax({

        type: 'GET',
        url: '/PayRoll/SlabBasedDeduction/GetList',
        dataType: 'html',
        data: { DeductionCode: Deduction },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#DeductionSettingsList').html("");
                $('#DeductionSettingsList').html(result);
                var element = document.getElementById("PTDiv");
                let maxval = 0;
                count = $('#AddNewRow tr[id^="row_OI_"]').length;
                for (let k = 0; k < count - 1; k++) {
                    $('#addrowid_' + (k)).addClass('hide');
                    $('#deleterowid_' + (k)).addClass('hide');
                    $('#DeductionSettingsList' + k + '__SalaryFrom').attr("readonly", true);
                    $('#DeductionSettingsList' + k + '__SalaryTo').attr("readonly", true);
                    maxval = k;
                }
                if (count == 1) {
                    $('#deleterowid_0').addClass('hide');
                    $('#DeductionSettingsList_0__SalaryFrom').attr("readonly", false);

                }
                if (count = (maxval + 1))
                    $('#DeductionSettingsList_' + count + '__SalaryFrom').attr("readonly", true);
                /* element.style.display = "none";*/
             
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






function AddRowGrid(tableId, i) {
    debugger;


    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
    /*if (count != 5) {*/
    if ($('#SlabDeductionSettingsList_' + (count - 1) + '__SalaryTo').val() != "") {                //condition for add button validation
        $('#SlabDeductionSettingsList_' + (count - 1) + '__SalaryTo').removeClass('is-invalid');
        $("#salaryto_" + (count - 1)).removeClass('error-message');
        $("#salaryto_" + (count - 1)).html("");
        var validationstatus = validateSalaryTo(count - 1);
        /*alert(validationstatus);*/
        //checkAllPtAmt();
        checkAllPtAmt();
        debugger;
        if (validationstatus == true && PtSubmit == true) {
            if ($('#SlabDeductionSettingsList_' + (count - 1) + '__RateAmount').val() != "") {
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
                    //---------------------------------------For row Increment When onkeypress
                    var onkeypress = $(this).attr('onkeypress');
                    if (onkeypress !== undefined) {
                        onkeypress = onkeypress.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onkeypress', onkeypress);
                    }
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------For row Increment When onblur
                    var onblur = $(this).attr('onblur');
                    if (onblur !== undefined) {
                        onblur = onblur.replace(/\(0\)/g, '(' + count + ')');
                        $(this).attr('onblur', onblur);
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

                $.each(template.find('input[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
                    $(this).attr('onclick', onclick);
                });
                $.each(template.find('a'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
                    $(this).attr('onclick', onclick);
                });
                //$.each(template.find('.hasDatepicker'), function () {
                //    $(this).removeClass('hasDatepicker');
                //});


                template.find('td[id=oi_slno]').text(count + 1);
                $.each(template.find('.addrowid'), function () {
                    $(this).attr("id", "addrowid_" + count);
                });
                $.each(template.find('.deleterowid'), function () {
                    $(this).attr("id", "deleterowid_" + count);
                });

                //spanclone
                $.each(template.find('.spanfrom'), function () {

                    $(this).attr("id", "salaryfrom_" + count);
                });
                $.each(template.find('.spanto'), function () {

                    $(this).attr("id", "salaryto_" + count);
                });

                $.each(template.find('.amountpt'), function () {

                    $(this).attr("id", "ptamount_" + count);
                });
                $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
                //------------to clear when new row---------

                //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
                //    dateFormat: "dd-M-yy", changeMonth: true,
                //    changeYear: true
                //});

                $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
                $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
                $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



                //---------------------------------

                //make numbers only inputs.
                $('.numbers-only').keypress(function (e) {
                    if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
                });
                /*   }*/
                //else {
                //    swal.fire('Limit Exceeded.Maximum level of point criteria is 5');
                //}
                $('#addrowid_' + count).removeClass('hide');
                $('#deleterowid_' + count).removeClass('hide');

                for (let k = 0; k < count; k++) {
                    $('#addrowid_' + (k)).addClass('hide');
                    $('#deleterowid_' + (k)).addClass('hide');
                    $('#SlabDeductionSettingsList_' + k + '__SalaryFrom').attr("readonly", true);
                    $('#SlabDeductionSettingsList_' + k + '__SalaryTo').attr("readonly", true);
                }
                debugger;
                $('#SlabDeductionSettingsList_' + count + '__SalaryFrom').val(parseInt($('#SlabDeductionSettingsList_' + (count - 1) + '__SalaryTo').val()) + 1);
                $('#SlabDeductionSettingsList_' + count + '__SalaryTo').removeAttr("readonly", false);
            }
            else {
                $('#SlabDeductionSettingsList_' + (count - 1) + '__RateAmount').addClass('is-invalid');
                $("#Dedamount_" + (count - 1)).addClass('error-message');
                $("#Dedamount_" + (count - 1)).html("This field is Required");
                return false;
            }
        }
    }
    else {
        $('#SlabDeductionSettingsList_' + (count - 1) + '__SalaryTo').addClass('is-invalid');
        $("#salaryto_" + (count - 1)).addClass('error-message');
        $("#salaryto_" + (count - 1)).html("This field is Required");
        return false;
    }
}

function DeleteRw(index) {
    debugger;
    var count = 0;
    count = $('#AddNewRow tr[id^="row_OI_"]').length;
    //$("#" + index).html('');
    if (count > 1) {
        $("#AddNewRow #row_OI_" + index).remove();
        for (var j = index + 1; j < count; j++) {
            var template = $('#row_OI_' + j).clone(); // clone the row


            $.each(template.find('input[type=text]'), function () {
                var name = $(this).attr('name');
                name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                $(this).attr('name', name);

                var id = $(this).attr('id');
                id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                $(this).attr('id', id);

                //---------------------------------For row Increment When onkeyup
                var onkeyup = $(this).attr('onkeyup');
                if (onkeyup !== undefined) {
                    onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                    $(this).attr('onkeyup', onkeyup);
                }
                //----------------------------

            });

            $.each(template.find('select'), function () {
                var name = $(this).attr('name');
                name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                $(this).attr('name', name);

                var id = $(this).attr('id');
                id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                $(this).attr('id', id);


            });

            $.each(template.find('input[type=button]'), function () {
                var onclick = $(this).attr('onclick');
                onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                $(this).attr('onclick', onclick);
            });

            template.find('td[id=grid_slno]').text(j);
            //$("#row_OI_" + j).html(template.html());
            $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));


        }
        debugger;
        if ($('#AddNewRow tr[id^="row_OI_"]').length == 1) {
            $('#deleterowid_' + (index - 1)).addClass('hide');
            $('#addrowid_' + (index - 1)).removeClass('hide');
        }
        else {
            $('#deleterowid_' + (index - 1)).removeClass('hide');
            $('#addrowid_' + (index - 1)).removeClass('hide');
        }

        //for (let k = 0; k < count; k++) {
        //    $('#addrowid_' + (k)).addClass('hide');
        //    $('#deleterowid_' + (k)).addClass('hide');
        //}
    }
    //else {
    //    swal.fire('Atleast One level is mandatory')
    //}
    $('#SlabDeductionSettingsList_' + (index - 1) + '__SalaryTo').removeAttr("readonly", false);
    if (index == 1)
        $('#SlabDeductionSettingsList_' + (index - 1) + '__SalaryFrom').removeAttr("readonly", false);
}



function OnSaveClick() {
    debugger;
    var countt = 0;
    countt = $('#AddNewRow tr[id^="row_OI_"]').length;
    /*if (count != 5) {*/
    if ($('#SlabDeductionSettingsList_' + (countt - 1) + '__SalaryTo').val() != "") {                //condition for add button validation
        $('#SlabDeductionSettingsList_' + (countt - 1) + '__SalaryTo').removeClass('is-invalid');
        $("#salaryto_" + (countt - 1)).removeClass('error-message');
        $("#salaryto_" + (countt - 1)).html("");
        var validationstatus = validateSalaryTo(countt - 1);
        /*alert(validationstatus);*/
        if (validationstatus == true) {
            var DeductionCode = $('#DeductionCode').val();
            var data = $("#AddNewRow :input").serialize();
            console.log(data);
            debugger;
            swal.fire({
                text: "Do you want to Save ?",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({

                        type: 'POST',
                        url: '/PayRoll/SlabBasedDeduction/Save',
                        dataType: 'json',
                        data: data /*$('#PTSettingsSave').serialize()*/,
                        success: function (result) {
                            if (result.success == true) {
                                swal.fire("Saved Successfully").then(function () {

                                    var url = "/PayRoll/SlabBasedDeduction/GetList?DeductionCode=" + DeductionCode;
                                    $('#DeductionSettingsList').load(url, function () {
                                        count = $('#AddNewRow tr[id^="row_OI_"]').length;
                                        for (let k = 0; k < count - 1; k++) {
                                            $('#addrowid_' + (k)).addClass('hide');
                                            $('#deleterowid_' + (k)).addClass('hide');
                                            $('#SlabDeductionSettingsList_' + k + '__SalaryFrom').attr("readonly", true);
                                            $('#SlabDeductionSettingsList_' + k + '__SalaryTo').attr("readonly", true);
                                        }
                                        if (count == 1) {
                                            $('#deleterowid_0').addClass('hide');
                                        }
                                    });

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
    }
    else {
        $('#SlabDeductionSettingsList_' + (countt - 1) + '__SalaryTo').addClass('is-invalid');
        $("#salaryto_" + (countt - 1)).addClass('error-message');
        $("#salaryto_" + (countt - 1)).html("This field is Required");
        return false;
    }
}

function validateSalaryTo(index) {
    debugger;
    if (parseInt($('#SlabDeductionSettingsList_' + index + '__SalaryTo').val()) <= parseInt($('#Details_' + index + '__SalaryFrom').val())) {
        $('#SlabDeductionSettingsList_' + index + '__SalaryTo').addClass('is-invalid');
        $("#salaryto_" + index).addClass('error-message');
        $("#salaryto_" + index).html("Salary To must be greater than Salary From");
        return false;
    }
    else {
        $('#SlabDeductionSettingsList_' + index + '__SalaryTo').removeClass('is-invalid');
        $("#salaryto_" + index).removeClass('error-message');
        $("#salaryto_" + index).html("");
        return true;
    }
  
}