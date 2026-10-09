
function FromChange(sender)
{
    $.getJSON("/General/PayRollReportBuilder/fillToMonthDropdown", { fromMonth: $(sender).val() }, function (data) {
        debugger;      
        $('#To_Code option').remove();
        $('#To_Code').append('<option value>--Select--</option');
       
            for (var i = 0; i < data.length; i++) {
                $('#To_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

       
        

    });
}
function ToChange() {
    debugger
    var diff = monthDiff(new Date($('#From_Code option:selected').text()), new Date($('#To_Code option:selected').text()))
    var i = 0;
    if (!validateById('#To_Code', 'todate')) {
        i++;
    }
    else {
        if (diff > 12) {
            $('#To_Code').addClass('is-invalid');
            $("#todate").addClass('error-message');
            $("#todate").html("From and To period should be less than or equal to 12 months");
            i++;
        }
        else {
            $('#To_Code').removeClass('is-invalid');
            $("#todate").removeClass('error-message');
            $("#todate").html("");
        }
    }
    if (i == 0)
        return true;
    else
        return false;
}

function onReportTypeChange(sender) {
    debugger
    $.ajaxSetup({ async: false });
    if ($("#Type_Code option:selected").text() == "Yearly") {
        $("#BasedOn_Code option[value='2']").show();
        $('#BasedOn_Code').val($('#BasedOn_Code option:first').val());
        $(".payrb_monthdiv").addClass('hide');
        $(".payrb_from_todiv").addClass('hide');
        $('.finyr').removeClass('hide');
    }
    else if ($("#Type_Code option:selected").text() == "Periodic") {
        $("#BasedOn_Code option[value='2']").show();
        $('#BasedOn_Code').val($('#BasedOn_Code option:first').val());
        $(".payrb_monthdiv").addClass('hide');
        $(".payrb_from_todiv").removeClass('hide');
        $('.finyr').addClass('hide');
        $.getJSON("/General/PayRollReportBuilder/fillMonthDropdown", { ReportType: $(sender).val(), FinancialYrCode: $('#FinancialYear_Code').val() }, function (data) {
            debugger;
            $('#From_Code option').remove();
            $('#From_Code').append('<option value>--Select--</option');


            for (var i = 0; i < data.length; i++) {
                $('#From_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });

    }
    else {
        if ($("#Type_Code option:selected").text() == "Monthly") {
            $("#BasedOn_Code option[value='2']").hide();
            $('#BasedOn_Code').val($('#BasedOn_Code option:first').val());
            $(".Type_Label").html('Month');
        }
           
        else if ($("#Type_Code option:selected").text() == "Quarterly") {
            $("#BasedOn_Code option[value='2']").show();
            $('#BasedOn_Code').val($('#BasedOn_Code option:first').val());
            $(".Type_Label").html('Quarterly');
        }
        else {
            $("#BasedOn_Code option[value='2']").show();
            $('#BasedOn_Code').val($('#BasedOn_Code option:first').val());
            $(".Type_Label").html('Half Year');
        }
           
        $(".payrb_from_todiv").addClass('hide');
        $(".payrb_monthdiv").removeClass('hide');
        $.getJSON("/General/PayRollReportBuilder/fillMonthDropdown", { ReportType: $(sender).val(), FinancialYrCode: $('#FinancialYear_Code').val() }, function (data) {
            debugger;           
            $('#Month_Code option').remove();
            $('#Month_Code').append('<option value>--Select--</option');

            if ($(sender).val() == 1) {
                for (var i = 0; i < data.length; i++) {
                    $('#Month_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
                }

            }
            else {
                for (var i = 0; i < data.length; i++) {
                    $('#Month_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
                }
            }

        });
        if ($("#Type_Code").val() ==1)
            $("#BasedOn_Code option[value='2']").hide();
        else
            $("#BasedOn_Code option[value='2']").show();
    }
   
}


function onApplicabilityClick() {
    debugger
    var details = $("#databankLeft :input").serializeArray();


    $.ajax({

        type: 'POST',
        url: '/General/PayRollReportBuilder/fillCombinationGrid',
        dataType: 'html',
        data: details,
        success: function (result) {
           
            $('#databank_combnGrid').html("");
            $('#databank_combnGrid').html(result);
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function populateDraggableGrid(result)
{
    debugger
    $("#databankRight_tbody").empty();
    $.each(result.combinations, function (index, item) {
        // Generate an HTML table row for each item
        row = "<tr>";
        row += "<td>" + item.fieldGroup +
            "<input  id='Combinations_" + index + "__Order'  name='Combinations[" + index + "].Order' type='hidden' value=" + item.order + ">" +
            "</td>";
 
        row += "<td>" + item.field + "<input  id='Combinations_" + index + "__Field'     name='Combinations[" + index + "].Field' type='hidden' value='" + item.field + "'>" +
            "<input  id='Combinations_" + index + "__FieldGroup'    name='Combinations[" + index + "].FieldGroup' type='hidden' value='" + item.fieldGroup + "'>" +
            "<input  id='Combinations_" + index + "__Code' name='Combinations[" + index + "].Code' type='hidden' value=" + item.code + ">"
            + "</td>";
        row += "<td>" + item.order + "</td>";

        row += "</tr>";

        // Append the row to the HTML table

        $("#databankRight_tbody").append(row);



    });
    debugger
    var jQueryScript = document.createElement('script');
    jQueryScript.setAttribute('src', 'https://code.jquery.com/ui/1.12.1/jquery-ui.js');
    document.head.appendChild(jQueryScript);

    $("#databankRight").sortable({
        /*   items: 'tr:not(tr:first-child)',*/
        items: 'tr:not(thead tr)',
        cursor: 'pointer',
        axis: 'y',
        dropOnEmpty: false,
        start: function (e, ui) {
            ui.item.addClass("selected");
        },
        stop: function (e, ui) {
            ui.item.removeClass("selected");

            $(this).find("tr").each(function (index) {
                if (index > 0) {
                    $(this).find("td").eq(2).html(index);
                    debugger
                    var tdd = $(this).find("td").eq(2);
                    tdd.prevObject[0].getElementsByTagName('input')[0].value = index;

                }
            });
        }
    });
}
function onApplicabilityClick_EditMode(index) {
    debugger
    var E_field = $("#Details_" + index + "__Field").val();
    var E_code = $("#Details_" + index + "__Code").val();
    var E_fgroup = $("#Details_" + index + "__FieldGroup").val();

    var details = $("#databankRight :input").serializeArray();

   
    details.push({ name: 'E_field', value: E_field });
    details.push({ name: 'E_code', value: E_code });
    details.push({ name: 'E_fgroup', value: E_fgroup });
    $.ajax({

        type: 'POST',
        url: '/General/PayRollReportBuilder/fillEditModeCombinGrid_OnApplClick',
        dataType: 'json',
        cache: false,
        data: details,
        success: function (result) {
            populateDraggableGrid(result);           


        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}
function sort() {
    debugger

}




function openFilter()
{

   $(".payroll_rbfilteropen").removeClass('slider-btn');
    var i = 0;
    if (!validateById('#Type_Code', 'pay_Type'))
        i++;
   

    if ($("#Type_Code").val() != 4 && $("#Type_Code").val() != 5) {
        debugger
        if (!validateById('#Month_Code', 'pay_month'))
            i++;
       
    }
    if ($("#Type_Code").val() == 5) {
        debugger
        if (!validateById('#From', 'fromdate'))
            i++;
        if (!validateById('#To', 'todate'))
            i++;
        if ($("#From").val() > $("#To").val()) {
            swal.fire('To Date should be greater than From Date');
            i++;
        }
        if (!ToChange())
            i++;

    }
    else {
        if (!validateById('#FinancialYear_Code', 'pay_Fyear'))
            i++;
    }
        
        if (!validateById('#BasedOn_Code', 'pay_BasedOn'))
            i++;
    
    var rowCount = document.getElementById('databankRight').rows.length;
    if (rowCount == 1) //table contain only column heading
    {
        swal.fire('Select aleast one field');
        i++;
    }
    debugger
    if (i == 0) {
        $(".payroll_rbfilteropen").addClass('slider-btn');       
        $('body').append('<div class="form-overlay"></div>');   
        $('#filterForm').addClass('slider-forms-open');
       
    }
    else
        return false;
}
function onYOSChange(val) {
    debugger;
    $('#Filter_Advance_Filter_YSFrom').val('');
    $('#Filter_Advance_Filter_YSTo').val('');
    YosText = $('#Filter_Advance_Filter_YrsOfExperience option:selected').text();
    if (YosText == 'Between') {

        $('#Filter_Advance_Filter_YSTo').removeClass('hide');
    }
    else {
        $('#Filter_Advance_Filter_YSTo').addClass('hide');

    }
}


function onAgeChange(val) {
    debugger;
    $('#Filter_Advance_Filter_AgeFrom').val('');
    $('#Filter_Advance_Filter_AgeTo').val('');
    AgeText = $('#Filter_Advance_Filter_Age option:selected').text();
    if (AgeText == 'Between') {
        $('#Filter_Advance_Filter_AgeTo').removeClass('hide');

    }
    else {
        $('#Filter_Advance_Filter_AgeTo').addClass('hide');

    }

}
function enterReportName() {
    debugger;
    swal.fire({

        text: "Report Name",
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Report Name is mandatory'
                )
            }
        }

    }).then((result) => {
        if (result.value) {
            console.log("Result: " + result.value);
            this.Export('apply', result.value,'PleaseSave');
          
        }

    });


}




function Export(mode, reportName, saveflag) {
    debugger

    if ($('#Mode').val() == "Edit") {
        _reportname = $("#ReportName").val();
    }
    else
        _reportname = reportName;
    if (mode == 'apply') {

        showSpinner();
        $.ajax({

            type: 'POST',
            url: '/General/PayRollReportBuilder/ApplyFilterfullForm',
            dataType: 'json',

            data: $("#fullform").serialize() + "&ReportName=" + _reportname + "&SaveFlag=" + saveflag,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    CloseSlider('rb_filterslider');
                    reportBuilderAJAX();

                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
        $('#filterForm').addClass('filter-apply');
    }


    else {
       /* showSpinner();*/
        $('.reset').val('');
        $('.multiselectDropdownFields').multiselect('rebuild');
      
    }


}



function reportBuilderAJAX() {
    $.ajax({
        url: '/General/PayRollReportBuilder/ReportBuilderAJAX',
        type: "POST",
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                if (result == true)
                {
                    CloseSlider('rb_filterslider');
                    window.location.href = "/General/PayRollReportBuilder/ReportBuilder?ReportTypeName=" + $('#Type_Code option:selected').text() + "&FinancialYearName=" + $('#FinancialYear_Code option:selected').text() + "&Month=" + $('#Month_Code option:selected').text() + "&BasedOn=" + $('#BasedOn_Code option:selected').text() + "&PaySelectionBasis=" + $('input[name="PaySelectionBasis"]:checked').next().next()[0].outerText + "&From=" + $('#From_Code option:selected').text() + "&To=" + $('#To_Code option:selected').text();
                }
                else {
                    CloseSlider('rb_filterslider');
                    swal.fire('No data found to Export');
                }
                
            }
            hideSpinner();

        },


        error: function (xhr, textStatus, errorThrown) {
            $('body').html(xhr.responseText);
        }
    });

}



function ExportFromSavedReports(mode) {
    debugger

    if (mode == 'apply') {

        showSpinner();
        $.ajax({

            type: 'POST',
            url: '/General/PayRollReportBuilder/ApplyFilter_SavedReport',
            dataType: 'json',

            data: $("#fullform").serialize() + "&Re_Code=" + $('#Saved_Report_Code').val() + "&Re_name=" + $('#Saved_Report_Name').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    CloseSlider('rb_filterslider');
                    close_modal_popup('rp_savedReports');
                    window.location.href = "/General/PayRollReportBuilder/ReportBuilder";
                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
        $('#filterForm').addClass('filter-apply');
    }


    else {
        showSpinner();
        $('.reset').val('');
        $(".reset").multiselect('destroy');
        $(".reset").multiselect();

        //$.ajax({

        //    type: 'POST',
        //    url: '/General/PayRollReportBuilder/ApplyFilter_SavedReport',
        //    dataType: 'json',
        //    data: $("#fullform").serialize() + "&Re_Code=" + $('#Saved_Report_Code').val() + "&Re_name=" + $('#Saved_Report_Name').val(),

        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {

        //        }
        //        hideSpinner();

        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        $('body').html(xhr.responseText);
        //    }
        //});
    }


}


function ApplyFilter(mode) {
    debugger
    var entryform = $("#Mode").val();
    if (mode == 'apply') {
        if (entryform == "FromSavedReports") {
            ExportFromSavedReports(mode);
        }
        else if (entryform == "Create") {
            $.ajaxSetup({ async: false });

            debugger
            swal.fire({
                text: "Do you want to Save this report for future Use?",
                icon: 'warning',
                showDenyButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                denyButtonText: 'No',
            }).then((result) => {
                if (result.isConfirmed) {

                    enterReportName();

                }
                else {
                    Export(mode,'','Dontsave');
                }
            })
        } else {
            swal.fire({
                text: "Do you want to Save this report for future Use?",
                icon: 'warning',
                showDenyButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes',
                denyButtonText: 'No',
            }).then((result) => {
                if (result.isConfirmed) {

                    enterReportName();

                }
                else {
               
                    Export(mode,'','Dontsave');
                }
            })

        }
    }
    else
        Export(mode, '','Dontsave');

}

//START  SAVED REPORTS REGION
function viewSavedReports() {
    debugger
    $.ajax({
        type: 'GET',
        url: '/General/PayRollReportBuilder/ViewSavedReports',
        dataType: 'html',
        success: function (Data) {
            debugger
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $("#Mode").val('FromSavedReports');
                var m = $('.rp_savedReports');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}











function onDeleteClick(ReportCode) {
    debugger
    $.ajax({

        type: 'POST',
        url: '/General/PayRollReportBuilder/Delete',
        dataType: 'json',
        data: { ReportCode: ReportCode },
        success: function (result) {
            if (result.success == true) {
                swal.fire(DeletedSuccessfulyMsg).then(function () {

                    close_modal_popup('rp_savedReports');
                });
            }
            else
                swal.fire(result.message);
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}



//END SAVED REPORTS REGION