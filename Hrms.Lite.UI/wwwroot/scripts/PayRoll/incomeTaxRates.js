function GetList() {
    debugger;
    var AssessmentYr = $('#AssessmentYear_Code').val();
    var ItCat = $('#ITCategory_Code').val();
    var BasedOn = $('input[name="BasedOn"]:checked').val();
    var url = '/PayRoll/IncomeTaxRates/GetList';
    $('#incomeTaxRatesList').load(url, { AssessmentYr: AssessmentYr, ItCategory: ItCat, BasedOn: BasedOn }, function () { });
}

function validateDropdowns() {
    var i = 0;
    if (!validateById('#AssessmentYear_Code', 'assmt'))
        i++;
    if (!validateById('#ITCategory_Code', 'itCate'))
        i++;
    if (i == 0)
        return true;
    else
        return false;

}

function validateTextBoxes(count) {
    var i = 0;   
    if (!validateById('#Details_' + (count - 1) + '__SalaryTo', 'salaryto_' + (count - 1))) 
        i++;
    if (!validateById('#Details_' + (count - 1) + '__SalaryFrom', 'salaryfrom_' + (count - 1)))
        i++;
    if (!validateById('#Details_' + (count - 1) + '__Rate', 'rate_' + (count - 1)))
        i++;
    if (i == 0)
        return true;
    else
        return false;
}

function AddNewRow(tableId, i) {
    debugger;

    if (validateDropdowns()) { 
    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
        if (validateTextBoxes(count)) {                //condition for add button validation
        
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
                onblur = onblur.replace(/\,0\)/g, ',' + count + ')');
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

        $.each(template.find('.rate'), function () {

            $(this).attr("id", "rate_" + count);
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
            $('#Details_' + k + '__SalaryFrom').attr("readonly", true);
            $('#Details_' + k + '__SalaryTo').attr("readonly", true);
        }
        debugger;
        $('#Details_' + count + '__SalaryFrom').val(parseInt($('#Details_' + (count - 1) + '__SalaryTo').val()) + 1);
        $('#Details_' + count + '__SalaryTo').removeAttr("readonly", false);

    }
    else {
        $('#Details_' + (count - 1) + '__SalaryTo').addClass('is-invalid');
        $("#salaryto_" + (count - 1)).addClass('error-message');
        $("#salaryto_" + (count - 1)).html("This field is Required");
        return false;
    }
}
}

function DeleteRow(index) {
    debugger;
    if (validateDropdowns()) {
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
                count = $('#incomeTaxRatesLt tr[id^="row_OI_"]').length;
                //$("#" + index).html('');
                if (count > 1) {
                    $("#incomeTaxRatesLt #row_OI_" + index).remove();
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
                $('#Details_' + (index - 1) + '__SalaryTo').removeAttr("readonly", false);
                if (index == 1)
                    $('#Details_' + (index - 1) + '__SalaryFrom').removeAttr("readonly", false);
            }
            else {
                return false;
            }
        })
    }
}


function OnSaveClick() {
    debugger;
    if (validateDropdowns()) { 
    var data = $("#incomeTaxRatesSave").serialize();
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
                url: '/PayRoll/IncomeTaxRates/Save?',
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {

                            var url = "/PayRoll/PTSettings/GetPTSettingsList?ptGroup=" + ptGroup;
                            $('#GridID').load(url, function () {
                                count = $('#AddNewRow tr[id^="row_OI_"]').length;
                                for (let k = 0; k < count - 1; k++) {
                                    $('#addrowid_' + (k)).addClass('hide');
                                    $('#deleterowid_' + (k)).addClass('hide');
                                    $('#Details_' + k + '__SalaryFrom').attr("readonly", true);
                                    $('#Details_' + k + '__SalaryTo').attr("readonly", true);
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