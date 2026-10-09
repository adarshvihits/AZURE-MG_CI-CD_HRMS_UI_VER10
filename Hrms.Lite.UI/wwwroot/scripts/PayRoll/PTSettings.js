var PtSubmit = true;
function checkAllPtAmt() {
    debugger;
    var count = $('#Details_Count').val();
    for (let i = 0; i < count; i++) {
        var currAmt = parseFloat($('#Details_' + i + '__RateAmount').val());
        var nxtAmt = parseFloat($('#Details_' + (parseInt(i) + 1) + '__RateAmount').val());
        if (currAmt > nxtAmt) {
            $('#Details_' + (parseInt(i) + 1) + '__RateAmount').addClass('is-invalid');
            $('#ptamount_' + (parseInt(i) + 1)).addClass('error-message');
            $('#ptamount_' + (parseInt(i) + 1)).html('Check the PT amount. It is less than the above row');
            PtSubmit = false;
            return;
        }
        else {
            $('#Details_' + (parseInt(i) + 1) + '__RateAmount').removeClass('is-invalid');
            $('#ptamount_' + (parseInt(i) + 1)).removeClass('error-message');
            $('#ptamount_' + (parseInt(i) + 1)).html('');
            PtSubmit = true;
        }
    }
}

function CheckPTAmt(i) {
    debugger;
    var currAmt = parseFloat($('#Details_' + i + '__RateAmount').val());
    var nxtAmt = parseFloat($('#Details_' + (parseInt(i) + 1) + '__RateAmount').val());
    if (currAmt > nxtAmt) {
        $('#Details_' + (parseInt(i) + 1) + '__RateAmount').addClass('is-invalid');
        $('#ptamount_' + (parseInt(i) + 1)).addClass('error-message');
        $('#ptamount_' + (parseInt(i) + 1)).html('Check the PT amount. It is less than the above row');
        //PtSubmit = false;
        
    }
    else {
        $('#Details_' + (parseInt(i) + 1) + '__RateAmount').removeClass('is-invalid');
        $('#ptamount_' + (parseInt(i) + 1)).removeClass('error-message');
        $('#ptamount_' + (parseInt(i) + 1)).html('');
        //PtSubmit = true;
        
    }
    if (i > 0) {
        var prevAmt = parseFloat($('#Details_' + (i - 1) + '__RateAmount').val());
        if (currAmt < prevAmt) {
            $('#Details_' + i + '__RateAmount').addClass('is-invalid');
            $('#ptamount_' + i).addClass('error-message');
            $('#ptamount_' + i).html('Check the PT amount. It is less than the above row');
            //PtSubmit = false;
            
        }
        else {
            $('#Details_' + i + '__RateAmount').removeClass('is-invalid');
            $('#ptamount_' + i).removeClass('error-message');
            $('#ptamount_' + i).html('');
            //PtSubmit = true;
            
        }
    }
    
}
function applicableAllowanceCountCheck(flag) {
    debugger;
    checkAllPtAmt();
    var flagg = parseInt(flag);
    var ptgroup = $('#PTGroup_Code').val();
    if (ptgroup == '') {
        debugger;
        swal.fire("Please select PTGroup");
    }
    else {
        if (PtSubmit == true) {
            $.ajax({

                type: 'GET',
                url: '/PayRoll/PTSettings/GetAllowanceCount',
                dataType: 'html',
                data: { ptGroup: ptgroup },

                success: function (result) {
                    console.log(result);
                    debugger;
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        if (result == 0 && flagg == 0) {
                            debugger;
                            OnApplicableAllowanceClick();
                        }
                        if (result != 0 && flagg == 1) {
                            OnSaveClick();
                        }
                        if (result == 0 && flagg == 1) {
                            debugger;
                            swal.fire("Please Select the Applicable Allowances").then(function () {
                                OnApplicableAllowanceClick();
                            });
                        }

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
}
function validateSalaryTo(index) {
    debugger;
    if (parseInt($('#Details_' + index + '__SalaryTo').val()) <= parseInt($('#Details_' + index + '__SalaryFrom').val())) {
        $('#Details_' + index + '__SalaryTo').addClass('is-invalid');
        $("#salaryto_" + index).addClass('error-message');
        $("#salaryto_" + index).html("Salary To must be greater than Salary From");
        return false;
    }
    else {
        $('#Details_' + index + '__SalaryTo').removeClass('is-invalid');
        $("#salaryto_" + index).removeClass('error-message');
        $("#salaryto_" + index).html("");
        return true;
    }
    //if ($(elementId).val() == "") {
    //    $(elementId).addClass('is-invalid');
    //    $("#" + spanId + "").addClass('error-message');
    //    $("#" + spanId + "").html("This field is required");
    //    return false;
    //}
    //else {
    //    $(elementId).removeClass('is-invalid');
    //    $("#" + spanId + "").removeClass('error-message');
    //    $("#" + spanId + "").html("");
    //    //trim(val);        
    //    return true;
    //}
}

function GetPTSettingsGrid() {
    debugger;
    var ptGroup = $('#PTGroup_Code').val();
    if (ptGroup == '')
        ptGroup = 0;

    $.ajax({

        type: 'GET',
        url: '/PayRoll/PTSettings/GetPTSettingsList',
        dataType: 'html',
        data: { ptGroup: ptGroup },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#GridID').html("");
                $('#GridID').html(result);
                var element = document.getElementById("PTDiv");
                let maxval = 0;
                count = $('#AddNewRow tr[id^="row_OI_"]').length;
                for (let k = 0; k < count - 1; k++) {
                    $('#addrowid_' + (k)).addClass('hide');
                    $('#deleterowid_' + (k)).addClass('hide');
                    $('#Details_' + k + '__SalaryFrom').attr("readonly", true);
                    $('#Details_' + k + '__SalaryTo').attr("readonly", true);
                    maxval = k;
                }
                if (count == 1) {
                    $('#deleterowid_0').addClass('hide');
                    $('#Details_0__SalaryFrom').attr("readonly", false);

                }
                if (count = (maxval + 1))
                    $('#Details_' + count + '__SalaryFrom').attr("readonly", true);
                /* element.style.display = "none";*/
                applicableAllowanceCountCheck(0);
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
    if ($('#Details_' + (count - 1) + '__SalaryTo').val() != "") {                //condition for add button validation
        $('#Details_' + (count - 1) + '__SalaryTo').removeClass('is-invalid');
        $("#salaryto_" + (count - 1)).removeClass('error-message');
        $("#salaryto_" + (count - 1)).html("");
        var validationstatus = validateSalaryTo(count - 1);
        /*alert(validationstatus);*/
        //checkAllPtAmt();
        checkAllPtAmt();
        debugger;
        if (validationstatus == true && PtSubmit == true) {
            if ($('#Details_' + (count - 1) + '__RateAmount').val() != "") {
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
                    $('#Details_' + k + '__SalaryFrom').attr("readonly", true);
                    $('#Details_' + k + '__SalaryTo').attr("readonly", true);
                }
                debugger;
                $('#Details_' + count + '__SalaryFrom').val(parseInt($('#Details_' + (count - 1) + '__SalaryTo').val()) + 1);
                $('#Details_' + count + '__SalaryTo').removeAttr("readonly", false);
            }
            else {
                $('#Details_' + (count - 1) + '__RateAmount').addClass('is-invalid');
                $("#ptamount_" + (count - 1)).addClass('error-message');
                $("#ptamount_" + (count - 1)).html("This field is Required");
                return false;
            }
        }
    }
    else {
        $('#Details_' + (count-1) + '__SalaryTo').addClass('is-invalid');
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
    $('#Details_' + (index - 1) + '__SalaryTo').removeAttr("readonly", false);
    if (index == 1)
        $('#Details_' + (index - 1) + '__SalaryFrom').removeAttr("readonly", false);
}



function OnSaveClick() {
    debugger;
    var countt = 0;
    countt = $('#AddNewRow tr[id^="row_OI_"]').length;
    /*if (count != 5) {*/
    if ($('#Details_' + (countt - 1) + '__SalaryTo').val() != "") {                //condition for add button validation
        $('#Details_' + (countt - 1) + '__SalaryTo').removeClass('is-invalid');
        $("#salaryto_" + (countt - 1)).removeClass('error-message');
        $("#salaryto_" + (countt - 1)).html("");
        var validationstatus = validateSalaryTo(countt - 1);
        /*alert(validationstatus);*/
        if (validationstatus == true) {
            var ptGroup = $('#PTGroup_Code').val();
            var data = $("#AddNewRow :input").serialize();
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
                        url: '/PayRoll/PTSettings/Save',
                        dataType: 'json',
                        data: data /*$('#PTSettingsSave').serialize()*/,
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
    else {
        $('#Details_' + (countt - 1) + '__SalaryTo').addClass('is-invalid');
        $("#salaryto_" + (countt - 1)).addClass('error-message');
        $("#salaryto_" + (countt - 1)).html("This field is Required");
        return false;
    }
}



function OnApplicableAllowanceClick() {
    debugger;
    var ptGroup = $('#PTGroup_Code').val();

    


        $.ajax({
            type: 'GET',
            url: "/PayRoll/PTSettings/ApplicableAllownacePopUp?ptGroup=" + ptGroup,
            dataType: 'html',

            success: function (Data) {
                console.log(Data);
                debugger;                
                var m = $('.ESI_settings_applicable');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('.ESI_settings_applicable').modal('show');
                /*$("#myDiv").css("display", "block");*/

            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });



   
}


function PTSettingsAllowanceSave() {
    debugger
    var ptGroup = $('#PTGroup_Code').val();
    /* var table = $('#PTAllowanceTable').DataTable();*/

    var data = $("#PTAllowanceTable :input").serialize();
    /*   var data = $('#PTAllowanceTable').$('input,select,textarea').serializeArray();*/
    /* data.push({ name: 'Remarks', value: remark, name: 'Mode', value: Mode });*/

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
                url: '/PayRoll/PTSettings/AllowanceSave',
                dataType: 'json',
                /* data: $('#PTSettingsAllowanceSave').serialize(),*/
                data: data,
                success: function (result) {
                    debugger;
                    $('.ESI_settings_applicable').modal('hide');

                    swal.fire("Saved Successfully").then(function () {
                        var url = "/PayRoll/PTSettings/GetPTSettingsList?ptGroup=" + ptGroup;
                        $('#GridID').load(url, function () { });
                    });
                    hideSpinner();
                    //if (result.success == true) {
                    //    swal.fire(result.message).then(function () {

                    //        var url = "/PayRoll/PTSettings/GetPTSettingsList?ptGroup=" + ptGroup;
                    //        $('#GridID').load(url, function () { });


                    //    });

                    //}
                    //else {
                    //    swal.fire(result.message).then(function () {


                    //    });
                    //}

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
//function PTSettingsAllowanceSave() {
//    debugger;
//    var ptGroup = $('#PTGroup_Code').val();
//    var data = table.$('input,select,textarea').serializeArray();
//    swal.fire({
//        text: "Do you want to Save ?",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Ok'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({

//                type: 'POST',
//                url: '/PayRoll/PTSettings/AllowanceSave',
//                dataType: 'json',
//                data: $('#PTSettingsAllowanceSave').serialize(),
//                success: function (result) {
//                    if (result.success == true) {
//                        swal.fire(result.message).then(function () {

//                            var url = "/PayRoll/PTSettings/GetPTSettingsList?ptGroup=" + ptGroup;
//                            $('#GridID').load(url, function () { });

//                        });

//                    }

//                    else {
//                        swal.fire(result.message).then(function () {

//                        });
//                    }

//                },
//                error: function (xhr, textStatus, errorThrown) {
//                    document.body.id = "kt_body";
//                    $('body').html(xhr.responseText);
//                }
//            });
//        }
//        else {
//            return false;
//        }
//    })

//}
