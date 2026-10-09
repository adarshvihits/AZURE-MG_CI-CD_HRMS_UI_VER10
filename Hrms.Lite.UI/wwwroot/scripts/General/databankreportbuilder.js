


function onApplicabilityClick() {
    debugger
    var details = $("#databankLeft :input").serializeArray();
   

    $.ajax({

        type: 'POST',
        url: '/General/ReportBuilder/fillCombinationGrid',
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

function populateDraggableGrid(result) {
debugger
    $("#databankRight_tbody").empty();
    $.each(result.combinations, function (index, item) {
        // Generate an HTML table row for each item
        row = "<tr>";
        row += "<td>" + item.fieldGroup +
            "<input  id='Combinations_" + index + "__Order'  name='Combinations[" + index + "].Order' type='hidden' value=" + item.order + ">" +
            "</td>";
        debugger
        row += "<td>" + item.field + "<input  id='Combinations_" + index + "__Field'     name='Combinations[" + index + "].Field' type='hidden' value='" + item.field + "'>" +
            "<input  id='Combinations_" + index + "__FieldGroup'    name='Combinations[" + index + "].FieldGroup' type='hidden' value='" + item.fieldGroup + "'>" +
            "<input  id='Combinations_" + index + "__Code' name='Combinations[" + index + "].Code' type='hidden' value=" + item.code + ">"
            + "</td>";
        row += "<td>" + item.order + "</td>";

        row += "</tr>";

        // Append the row to the HTML table

        $("#databankRight_tbody").append(row);



    });
    var jQueryScript = document.createElement('script');
    jQueryScript.setAttribute('src', 'https://code.jquery.com/ui/1.12.1/jquery-ui.js');
    document.head.appendChild(jQueryScript);

    $("#databankRight").sortable({
        items: 'tr:not(thead tr)',

        cursor: 'pointer',
        axis: 'y',
        dropOnEmpty: true,
        start: function (e, ui) {
            ui.item.addClass("selected");
        },
        stop: function (e, ui) {
            ui.item.removeClass("selected");

            $(this).find("tr").each(function (index) {
                if (index > 0) {
                    $(this).find("td").eq(2).html(index);
                    //debugger
                    var tdd = $(this).find("td").eq(2);
                    tdd.prevObject[0].getElementsByTagName('input')[0].value = index;


                }
            });
        }
    });
    debugger
}


function onApplicabilityClick_EditMode(index) {
    debugger
    var E_field = $("#Details_" + index + "__Field").val();
    var E_code = $("#Details_" + index + "__Code").val();
    var E_fgroup = $("#Details_" + index + "__FieldGroup").val();
    
    var details = $("#databankRight :input").serializeArray();

    var temp = {};
    for (var i = 0; i < details.length; i++) {
        temp[details[i]['name']] = details[i]['value']; // Map the current key-value pairs into the filter variable object.
    }
   
    details.push({ name: 'E_field', value: E_field });
    details.push({ name: 'E_code', value: E_code });
    details.push({ name: 'E_fgroup', value: E_fgroup });

    $.ajax({

        type: 'POST',
        url: '/General/ReportBuilder/fillEditModeCombinGrid_OnApplClick',
        dataType: 'json',
        cache: false,
        data: details,
        success: function (result) {
            debugger
            populateDraggableGrid(result);
    
           
        
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}





function openFilter() {
    var i = 0;
    var rowCount = document.getElementById('databankRight').rows.length;
    if (rowCount == 1) //table contain only column heading
    {
        swal.fire('Select aleast one field');
        i++;
    }
    debugger
    if (i == 0) {
        $(".databank_rbfilteropen").addClass('slider-btn');
        $('.slider-btn').click(function () {
            $('body').append('<div class="form-overlay"></div>');
            $("div").removeClass('slider-forms-open');
            var elementClassName = $(this).attr('Id');

            $('div#' + elementClassName).toggleClass('slider-forms-open');
         });
    }
    else
        return false;
}

function openSavedreportFilter()
{
    debugger
    $(".databank_rbfilteropenn").addClass('slider-btn');   
        $('body').append('<div class="form-overlay"></div>');   
    var elementClassName = $('.slider-btn').attr('Id');
        $('div#' + elementClassName).toggleClass('slider-forms-open');
   
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
                this.Export('apply', result.value);
               /* this.Save(result.value);*/
            }

        });


    }




function Export(mode,reportName) {
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
                url: '/General/ReportBuilder/ApplyFilterfullForm',
                dataType: 'json',

                data: $("#fullform").serialize() + "&ReportName=" + _reportname,// model,//combinationdata+ filterserialize,
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        debugger
                        CloseSlider('rb_filterslider');
                        /*   window.location.href = "/General/ReportBuilder/ReportBuilder";*/
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
           // showSpinner();           
            $('.reset').val('');            
            $('.multiselectDropdownFields').multiselect('rebuild');
          
        }


    }








function reportBuilderAJAX() {
    debugger
    $.ajax({
        url: '/General/ReportBuilder/ReportBuilderAJAX',
        type: "POST",
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger;
                if (result == true) {
                    CloseSlider('rb_filterslider');
                    window.location.href = "/General/ReportBuilder/ReportBuilder";
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
            url: '/General/ReportBuilder/ApplyFilter_SavedReport',
            dataType: 'json',

            data: $("#fullform").serialize() + "&Re_Code=" + $('#Saved_Report_Code').val() +"&Re_name="+$('#Saved_Report_Name').val(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    CloseSlider('rb_filterslider');
                    close_modal_popup('rp_savedReports');
                    window.location.href = "/General/ReportBuilder/ReportBuilder";
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

        $.ajax({

            type: 'POST',
            url: '/DataBank/ReportBuilder/ApplyFilter_SavedReport',
            dataType: 'json',
            data: $("#fullform").serialize() + "&Re_Code=" + $('#Saved_Report_Code').val() + "&Re_name=" + $('#Saved_Report_Name').val(),

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {

                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }


}
//function Export(mode, reportName) {
//    debugger
//    var grid = $("#grid2").data("kendoGrid");
//    var gridData = grid.dataSource.data();

//    var fil = $("#multiSelect_Filter").serializeArray();


//    var filterdata = JSON.stringify(fil);
//    var combinationdata = JSON.stringify(gridData);



//    var temp = {};
//    for (var i = 0; i < fil.length; i++) {
//        temp[fil[i]['name']] = fil[i]['value']; // Map the current key-value pairs into the filter variable object.    }
//    }
//    var filterserialize = $("#multiSelect_Filter").serialize();
  
//        var model = {
//            Filter: JSON.stringify(temp),
//           Details: combinationdata //combinationdata
//        };
 


  
//        debugger
//        if (mode == 'apply') {

//            showSpinner();
//            $.ajax({

//                type: 'POST',
//                url: '/General/ReportBuilder/ApplyFilter',
//                dataType: 'json',
//                /* contentType: "application/json",*/
//                data: filterserialize + "&CombinationDetails=" + combinationdata + "&ReportName=" + reportName,// model,//combinationdata+ filterserialize,
//                success: function (result) {
//                    debugger
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
//                        window.location.href = "/General/ReportBuilder/ReportBuilder";
//                    }
//                    hideSpinner();
//                    CloseSlider('multiSelect_Filter');
//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    $('body').html(xhr.responseText);
//                }
//            });
//            $('#filterForm').addClass('filter-apply');
//        }


//        else {
//            showSpinner();           
//            $('.reset').val('');       
          
//            $.ajax({

//                type: 'POST',
//                url: '/DataBank/ReportBuilder/ApplyFilter',
//                dataType: 'json',
//                data: $('#multiSelect_Filter').serialize() ,
//                success: function (result) {
//                    debugger
//                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                        window.location.reload();
//                    }
//                    else {
                       
//                    }
//                    hideSpinner();

//                },


//                error: function (xhr, textStatus, errorThrown) {
//                    $('body').html(xhr.responseText);
//                }
//            });
//        }
      

//    }

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
                    Export(mode);
                }
            })

        }
        else {
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

                    Export(mode);

                }
                else {
                    Export(mode);
                }
            })
        }
    }
    else {
        Export(mode);
    }

}


//START  SAVED REPORTS REGION
function viewSavedReports() {
    debugger
    $.ajax({
        type: 'GET',
        url: '/General/ReportBuilder/ViewSavedReports',
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
        url: '/General/ReportBuilder/Delete',
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