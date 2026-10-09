function addRowInFamilyGrid(tableId) {
    debugger;
    //if (ValidationMain() == true) {
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

        $.each(template.find('input[type=button]'), function () {
            var onclick = $(this).attr('onclick');
            onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onclick', onclick);
        });

        //$.each(template.find('.kt_datepicker_2'), function () {
        //    $(this).removeClass('kt_datepicker_2');
        //});


        //template.find('td[id=oi_slno]').text(count + 1);
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
    //}
    //else {
    //    toastr.warning("please fill out the current  row before adding new row");
    //}
}

function DeleteRwFromFamilyGrid(tableId,index) {
    debugger;
    if (confirm("Do you want to delete..?")) {
        var count = 0;
        count = $('tr[id^="row_OI_"]').length;        
        if (count > 1) {
            $('#' + tableId + ' tr[id^="row_OI_'+index+'"]').remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').clone(); // clone the row
                //var qid = $("#InstrumentVsParameterMain_" + j + "__ParameterCode").val();



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

                $.each(template.find('input[type=button]'), function () {
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

                //template.find('td[id=oi_slno]').text(j);                
                $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').html(template.html());
                $('#' + tableId + ' tr[id^="row_OI_' + j + '"]').attr('id', 'row_OI_' + (j - 1));
                //$("#InstrumentVsParameterMain_" + (j - 1) + "__ParameterCode").val(qid);

            }

        }
        else {
            $("#InstrumentVsParameterMain_0__ParameterCode").val("");



        }
    }
}