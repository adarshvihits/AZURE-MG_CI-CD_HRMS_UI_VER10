function GetAuthorityDetails(RequestTypeCode,flag) {
    debugger;
    var WorkunitCode = $('#WorkUnitCode').val();
    var savedData = $("#authorityTable :input").serialize();
    console.log(savedData)
    $.ajax({
        type: 'POST',
        url: '/DataBank/WorkUnitVsAuthoritySettings/GetAuthorityDetails?WorkunitCode=' + WorkunitCode + '&RequestTypeCode=' + RequestTypeCode + '&flag=' + flag,
        dataType: 'html',
        data:  savedData,
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.addAuthorityPopup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function requestTypeDropdnChange(sender) {
    debugger
    var tableLen = $('#addleveltable tbody tr').length;
    for (let i = 0; i < tableLen; i++) {
        if (i != 0)
            $('#row_OI_' + i).remove();
        else {

            $('#WorkUnitAuth_' + i + '__AuthorityName').val('');
            $('#WorkUnitAuth_' + i + '__AuthorityCode').val('');
            $('.authCodeDropdn_0').val('')
            $('.authCodeDropdn_0').selectpicker('refresh');
        }
    }

    if (parseInt($(sender).val()) == 1) {
        $('#objAdd_0').addClass('hide');
    }
    else {
        $('#objAdd_0').removeClass('hide');
    }
}


function onDropdownChange(WorkUnitCode,RequestTypeCode) {
    debugger;
    var flag = $('#flag').val();
    var savedData = $("#authorityTable :input").serialize();
    $.ajax({
        type: 'GET',
        url: '/DataBank/WorkUnitVsAuthoritySettings/GetPopupList?WorkunitCode=' + WorkUnitCode + '&RequestTypeCode=' + RequestTypeCode+'&flag='+flag,
        dataType: 'html',
        data: savedData,
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                $('#popupListDiv').html("");
                $('#popupListDiv').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function OnAuthorityDropdownChange(index, sender) {
    debugger;
    //$('.popupAuthName_' + index).val($(sender).find("option:selected").text());
    $('#WorkUnitAuth_' + index+'__AuthorityName').val($(sender).find("option:selected").text());
    $('#WorkUnitAuth_' + index + '__AuthorityCode').val($(sender).val());
    validateByIdForSearchDropdn('#WorkUnitAuth_' + index + '__AuthorityCode', 'authCode_' + index, '.authCodeDropdn_' + index);

}

function AddNewResultRow(tableId) {
    debugger;

    var valid = 0;
    for (let i = 0; i < $('#addleveltable tbody tr').length; i++) {
        if ($('#WorkUnitAuth_' + i + '__AuthorityCode').val() == '' || $('#WorkUnitAuth_' + i + '__AuthorityCode').val() == '0') {
            $('#WorkUnitAuth_' + i + '__AuthorityCode').val('');
            validateByIdForSearchDropdn('#WorkUnitAuth_' + i + '__AuthorityCode', 'authCode_' + i, '.authCodeDropdn_' + i);
            valid++;
        }
    }
    if (valid == 0) {
        $('.authoritydropdn').selectpicker('destroy');
        var count = 0;
        count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
        if (count < 5) {
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
            /*template.find('td[id=grid_slno]').text(count + 1);*/
            $.each(template.find('.addObj'), function () {
                $(this).attr("id", "objAdd_" + count);
            });
            $.each(template.find('.delObj'), function () {
                $(this).attr("id", "objDel_" + count);
            });
            $.each(template.find('#lvlName_0'), function () {
                $(this).attr("id", "lvlName_" + count);
            });
            $.each(template.find('#authCode_0'), function () {
                $(this).attr("id", "authCode_" + count);
            });
            $.each(template.find('.authCodeDropdn_0'), function () {
                $(this).removeClass('authCodeDropdn_0');
                $(this).addClass('authCodeDropdn_' + count);
            });
            $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
            //------------to clear when new row---------
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
            $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');
            //---------------------------------
            $('.authoritydropdn').selectpicker();
            //make numbers only inputs.

            $('#objAdd_' + (count - 1)).addClass('hide');
            $('#objAdd_' + count).removeClass('hide');
            $('#objDel_' + count).removeClass('hide');
            $('#lvlName_' + count).html('');
            $('#lvlName_' + count).html('Level ' + (count + 1));
            $('#WorkUnitAuth_' + count + '__LevelCode').val((count + 1));
            $('#WorkUnitAuth_' + count + '__LevelName').val('Level ' + (count + 1));
            $('#WorkUnitAuth_' + count + '__AuthorityCode').val('');
            $('#WorkUnitAuth_' + count + '__AuthorityName').val('');
        }
        else {
            swal.fire('Cannot add more than 5 Level');
        }

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

function AddToList() {
    debugger
    valid = 0;
    for (let i = 0; i < $('#addleveltable tbody tr').length; i++) {
        if ($('#WorkUnitAuth_' + i + '__AuthorityCode').val() == '' || $('#WorkUnitAuth_' + i + '__AuthorityCode').val() == '0') {
            $('#WorkUnitAuth_' + i + '__AuthorityCode').val('');
            validateByIdForSearchDropdn('#WorkUnitAuth_' + i + '__AuthorityCode', 'authCode_' + i, '.authCodeDropdn_' + i);
            valid++;
        }
    }
    if (valid == 0) {
        debugger   
        $('.reqCode').val($('#RequestType_Code').val());
        $('.reqName').val($('#RequestType_Code option:selected').text());
        debugger
        var popupData = $('#addleveltable :input').serialize();
        var savedData = $("#authorityTable :input").serialize();

        console.log(popupData);
        console.log(savedData);
        $.ajax({
            type: 'POST',
            url: '/DataBank/WorkUnitVsAuthoritySettings/AddToList?ReqCode=' + $('#RequestType_Code').val(),
            dataType: 'html',
            data: popupData + savedData,
            success: function (result) {
                debugger;
                $('#reqvsAuthListView').html('');
                $('#authorityTableDiv').html('');
                $('#authorityTable tbody').empty();
                $('#reqvsAuthListView').html(result);

                var m = $('.addAuthorityPopup');
                m.find('.modal-body').html('');
                m.modal('hide');

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

function onSave(){
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
                    url: '/DataBank/WorkUnitVsAuthoritySettings/Save',
                    dataType: 'json',
                    data: $('#authorityTable :input').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/WorkUnitVsAuthoritySettings/Index";
                                window.location.href = url;

                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
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

function onApplicableShiftClick(WorkUnitCode) {
    debugger;
    $.ajax({
        type: 'GET',
        url: '/DataBank/WorkUnitVsAuthoritySettings/GetApplicableShiftPopup?WorkUnitCode=' + WorkUnitCode,
        dataType: 'html',
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.addShiftPopup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);
                m.modal('show');
                $(".form-overlay").remove();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}


function onSaveShiftClick() {
    debugger;

    swal.fire({
        text: "Do you want to Save Shift ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/WorkUnitVsAuthoritySettings/ApplicableShiftSave',
                dataType: 'json',
                data: $('#ShiftSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/WorkUnitVsAuthoritySettings/Index";
                            window.location.href = url;

                        });
                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
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