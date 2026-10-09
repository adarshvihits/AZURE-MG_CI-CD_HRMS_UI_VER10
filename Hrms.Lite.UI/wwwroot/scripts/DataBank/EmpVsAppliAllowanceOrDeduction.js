$(document).ready(function () {

    if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
        $('#' + allCheckBox).prop('checked', true);
    else
        $('#' + allCheckBox).prop('checked', false);

});




function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}
function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;

    if ($(sender).is(':checked')) {
      
        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;
     
        $('#FullPermission').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}
function OnTypeChange(sender, id) {
    var type = $('#Type_Code option:selected').text();
    $('#AllowOrDed').text(type);
   
    debugger;
    $.getJSON("/DataBank/EmpVsAppliAllowanceOrDeduction/GetAllowanceOrDeductionDropDown", { TypeCode: $(sender).val() }, function (data) {
        $('#AllowanceOrDeduction_Code option').remove();
        $('#AllowanceOrDeduction_Code').append('<option value="">Select '+type+' </option');

        for (var i = 0; i < data.length; i++) {
            $('#AllowanceOrDeduction_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        
    });

}
function getList() {
    debugger

    var type = $('#Type_Code option:selected').val();
   
    var i = 0;
    if (!validateById("#Type_Code", 'Type'))
        i++;
    if (!validateById("#AllowanceOrDeduction_Code", 'AllowOrDeduction'))
        i++;
  
    if (i == 0) {
        showSpinner();
         ApplyFilter('apply');
      
    }

    else
        return false;
}
function ApplyFilter(mode) {
    var BasedOn = $('input[name="radioSelection"]:checked').val();
  
    if (mode == 'apply') {
        if (filterApplicable >= 1) {

            $.ajax({

                type: 'POST',
                url: '/DataBank/EmpVsAppliAllowanceOrDeduction/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Type=" + $('#Type_Code').val() + "&AllowOrDeduction=" + $('#AllowanceOrDeduction_Code').val() + "&BasedOn=" + BasedOn,
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();

                    }
                    else {
                        $('#AllowOrDedList').html("");
                        $('#AllowOrDedList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');

                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/EmpVsAppliAllowanceOrDeduction/GetList',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&Type=" + $('#Type_Code').val() + "&AllowOrDeduction=" + $('#AllowanceOrDeduction_Code').val() + "&BasedOn=" + BasedOn,
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#AllowOrDedList').html("");
                        $('#AllowOrDedList').html(result);
                    }
                    hideSpinner();
                    CloseSlider('basicFilter');
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
      
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/EmpVsAppliAllowanceOrDeduction/GetList',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&Type=" + $('#Type_Code').val() + "&AllowOrDeduction=" + $('#AllowanceOrDeduction_Code').val() + "&BasedOn=" + BasedOn,
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();

                }
                else {
                    $('#AllowOrDedList').html("");
                    $('#AllowOrDedList').html(result);
                }
                hideSpinner();

            },


            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });

    }
}

function Save() {

    
    var BasedOn = $('input[name="radioSelection"]:checked').val();

    swal.fire({
        text: "Do you want to Save",
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
                url: '/DataBank/EmpVsAppliAllowanceOrDeduction/SAVE',
                dataType: 'json',
                data: $('#EmpVsAppAllowOrDeduction').serialize(),
                success: function (result) {
                    if (result.success == true) {
                      
                        swal.fire(result.message).then(function () {
                          /*  getList();*/
                          
                            var url = "/DataBank/EmpVsAppliAllowanceOrDeduction/Index";
                            window.location.href = url;
                         
                        });
                        //swal.fire(result.message).then(function () {
                        //    showSpinner();
                        //    $.ajax({

                        //        type: 'GET',
                        //        url: '/DataBank/EmpVsAppliAllowanceOrDeduction/Index',
                        //        dataType: 'html',
                               

                        //        success: function (result) {
                        //            debugger
                        //            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        //                window.location.reload();
                        //            }
                        //            else {
                        //                $('#AllowOrDedList').html("");
                        //                $('#AllowOrDedList').html(result);
                        //            }
                        //            hideSpinner();
                        //        },
                        //        error: function (xhr, textStatus, errorThrown) {
                        //            debugger
                        //            document.body.id = "kt_body";
                        //            $('body').html(xhr.responseText);


                        //        }
                        //    });

                        //});

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }
                    hideSpinner();
                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
    /*}*/
}
function ExportToExcel() {
    debugger;

    var TableLength = $('#ApplAllowOrDedList_Count').val();

    if (TableLength != "") {
        var BasedOn = $('input[name="radioSelection"]:checked').val();
        url = "/DataBank/EmpVsAppliAllowanceOrDeduction/ExportToExcel?Type=" + $('#Type_Code').val() + "&AllowOrDed=" + $('#Type_Code option:selected').text() + "&TypeName=" + $('#AllowanceOrDeduction_Code option:selected').text() + "&AllowOrDeduction=" + $('#AllowanceOrDeduction_Code').val() + "&BasedOn=" + BasedOn + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {

        swal.fire("No data found to export");
    }

}
function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("AlwDedEmpSearch");

    filter = input.value.toUpperCase();
    table = document.getElementById("AllowOrDedAllCheckBox");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {

        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }

    }


    if (noOfRows == 1) {

        $(table).find(tbody).append("<tr  id='noDataImg' class='exit-page'><td colspan=12><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");

    }
}
function validateDropdowns() {
    var i = 0;
    debugger;
    if (!validateById('#Type_Code', 'Type')) {
        i++;
    }
    if (!validateById('#AllowanceOrDeduction_Code', 'AllowOrDeduction')) {
        i++;
    }
    if (i == 0) {
     
        var m = $('.bd-example-modal-lg');
        m.modal('show');

    }

}
function onImportExcelClick() {

    swal.fire({
        text: "Do you want to Upload?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var MPPersonalInfo = new FormData($('#EmpVsAppAllowOrDeduction')[0]);
            $("#Excel_FileData").val('');
            $.ajax({

                type: 'POST',
                url: '/DataBank/EmpVsAppliAllowanceOrDeduction/ImportFromExcel',
                contentType: false,
                processData: false,
                data: MPPersonalInfo,
                success: function (result) {
                    if (result.success == true) {
                        $("#Excel_FileData").val('');
                        swal.fire("Uploaded Successfully").then(function () {
                          
                            getList();
                            var m = $('.bd-example-modal-lg');
                            m.modal('hide');
                        });
                    }
                    else {
                        $("#Excel_FileData").val('');
                        if (result.message != null) {
                            $('#Unmatchedlist').html('')
                            swal.fire(result.message);
                        }
                        else {
                            $('#Unmatchedlist').html(result)
                        }
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