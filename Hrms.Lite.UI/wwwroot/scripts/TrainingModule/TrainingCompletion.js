function fillHeading() {
    debugger;
    var StatusCode = $('input[name="Status"]:checked').val();
    var FinancialYearCode = $('#FinancialYear_Code').val();
    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingCompletion/GetList',
        dataType: 'html',
        data: { StatusCode: StatusCode, FinancialYearCode: FinancialYearCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#TrainingScheduleCompletionList').html("");
                $('#TrainingScheduleCompletionList').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });


}



function Addcost(ScheduleCode, TrainingCode) {

    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingCompletion/Addcost',
        dataType: 'html',
        data: { ScheduleCode: ScheduleCode, TrainingCode: TrainingCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewTrainingSchedule').html("");
                $('#AddNewTrainingSchedule').html(result);
                /*  fillTopicList(ScheduleCode);*/

            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });


}



function TrainingCompletionCostSave() {
    debugger;
    var TabIndex;
    if (validateCompletionCost()) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                debugger
                var hrdata = new FormData($('#TrainingCostList')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/TrainingModule/TrainingCompletion/Save',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: hrdata,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('TrainingCost');
                                fillHeading();
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
    else {
        return false;
    }
}
function validateCompletionCost() {
    var i = 0;
    if (!validateById('#TrainingStatusCode', 'TrainingStatus')) {
        i++;
    }
    if (!validateById('#Duration', 'CompletionCost')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}


function ExportToExcel() {
    debugger;

    var StatusCode = $('input[name="Status"]:checked').val();
    var FinancialYearCode = $('#FinancialYear_Code').val();

    var TableLength = $('#TrainingCompletionScheduleList_Count').val();
    if (TableLength > 0) {
        url = "/TrainingModule/TrainingCompletion/ExportToExcel? &FinancialYearCode=" + FinancialYearCode + "&StatusCode=" + StatusCode;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}


function AddRowGrid(tableId, maxRows = 5) {
    debugger
    var count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
    if (count >= maxRows) {
        alert('Maximum rows exceeded!');
        return;
    }

    var template = $('#' + tableId + ' #row_OI_0').clone(true);
    template.attr('id', 'row_OI_' + count);

    template.find('input, textarea, select').each(function () {
        var name = $(this).attr('name');
        if (name) $(this).attr('name', name.replace(/\[0\]/g, '[' + count + ']'));

        var id = $(this).attr('id');
        if (id) $(this).attr('id', id.replace(/_0__/g, '_' + count + '__'));
        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }

        $(this).val(''); // Clear values for new row
    });

    template.find('button[type=button]').each(function () {
        var onclick = $(this).attr('onclick');
        if (onclick) $(this).attr('onclick', onclick.replace(/\(0\)/g, '(' + count + ')'));
    });

    $.each(template.find('#fileLabel_0'), function () {
        $(this).attr("id", "fileLabel_" + count);
    });


    template.find('td[id=oi_slno]').text(count + 1);
    $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
    //------------to clear when new row---------



    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');
    $('#fileLabel_' + count).html('');
    //$.each(template.find('.fileLabel'), function () {
    //    $(this).attr("id", "fileLabel_" + count);
    //});
    //$('#fileLabel_' + (count - 1)).addClass('hide');

}





function DeleteRw(index) {
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
                    var code = $("#RequestToHRDocList_" + j + "__Name").val();
                    var desc = $("#RequestToHRDocList_" + j + "__Point").val();

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

                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));
                    $("#RequestToHRDocList_" + (j - 1) + "__Name").val(code);
                    $("#RequestToHRDocList_" + (j - 1) + "__Point").val(desc);

                }
            }



        }
        else {
            $("#RequestToHRDocList_0__Name").val("");
            $("#RequestToHRDocList_0__Point").val("");
        }
    })
}
