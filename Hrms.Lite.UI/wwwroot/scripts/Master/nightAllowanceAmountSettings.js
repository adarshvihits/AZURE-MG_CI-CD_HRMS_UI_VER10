
//function OnTypeChange(sender, id) {
//    debugger

//    var type = $('#Department_Code option:selected').text();
//    var i = 0;
//    if (!validateById('#Department_Code', 'entrytype')) {
//        i++;
//    }

//    if (i == 0) {
//        $.getJSON("/Master/NightAllowanceAmountSettings/GetPurposeNAASDropdown", { DepartmentCode: $(sender).val() }, function (data) {
//            $('#Purpose_Code option').remove();

//            for (var i = 0; i < data.length; i++) {
//                $('#Purpose_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
//            }

//        });
//    }
//}
function OnTypeChange(sender, Type) {
    debugger;

    var type = $(sender).find("option:selected").text();
    var i = 0;

    if (!validateById(sender, 'entrytype')) {
        i++;
    }

    if (i == 0) {

        $.getJSON("/Master/NightAllowanceAmountSettings/GetPurposeNAASDropdown",
            { DepartmentCode: $(sender).val() },
            function (data) {

                $('#Purpose_Code').empty();
                $('#Purpose_Code').append('<option value="">--Select--</option>');

                for (var i = 0; i < data.length; i++) {
                    $('#Purpose_Code').append(
                        '<option value="' + data[i].value + '">' +
                        data[i].text +
                        '</option>'
                    );
                }

                // 🔥 CHANGED — After loading Purpose, auto-trigger change
                $('#Purpose_Code').trigger('change');
            }
        );
    }
}



function OnTypeChangeS1(sender, Type) {
    debugger;

    var selectedDept = $(sender).val();
    var i = 0;

    if (!validateById(sender, 'entrytype')) {
        i++;
    }

    if (i == 0) {
        $.getJSON("/Master/NightAllowanceAmountSettings/GetPurposeNAASDropdown",
            { DepartmentCode: selectedDept },
            function (data) {

                $('#Purpose_Code_S1').empty();
                $('#Purpose_Code_S1').append('<option value="">--Select--</option>');

                for (var j = 0; j < data.length; j++) {
                    $('#Purpose_Code_S1').append(
                        '<option value="' + data[j].value + '">' +
                        data[j].text +
                        '</option>'
                    );
                }

                // 🔥 CHANGED — Trigger Purpose dropdown change
                $('#Purpose_Code_S1').trigger('change');
            }
        );
    }
}




//function OnTypeChange(sender, Type) {
//    debugger;

    
//    var type = $(sender).find("option:selected").text();   

//    var i = 0;

   
//    if (!validateById(sender, 'entrytype')) {      
//        i++;
//    }

//    if (i == 0) {

//        $.getJSON("/Master/NightAllowanceAmountSettings/GetPurposeNAASDropdown",
//            { DepartmentCode: $(sender).val() },
//            function (data) {

               
//                $('#Purpose_Code').empty();               

//                for (var i = 0; i < data.length; i++) {

                    
//                    $('#Purpose_Code').append(
//                        '<option value="' + data[i].value + '">' +
//                        data[i].text +
//                        '</option>'                      
//                    );
//                }
//            }
//        );
//    }
//}



//function OnTypeChangeS1(sender, Type) {
//    debugger;

//    var selectedDept = $(sender).val();
//    var i = 0;

//    if (!validateById(sender, 'entrytype')) {
//        i++;
//    }

//    if (i == 0) {

//        $.getJSON("/Master/NightAllowanceAmountSettings/GetPurposeNAASDropdown",
//            { DepartmentCode: selectedDept },
//            function (data) {

//                $('#Purpose_Code_S1').empty();
//                $('#Purpose_Code_S1').append('<option value="">--Select--</option>');

//                for (var j = 0; j < data.length; j++) {
//                    $('#Purpose_Code_S1').append(
//                        '<option value="' + data[j].value + '">' +
//                        data[j].text +
//                        '</option>'
//                    );
//                }
//            }
//        );
//    }
//}


function validateFillData() {
    var isValid = true;

    if (!validateById('#Department_Code', 'department')) {
        isValid = false;
    }

    if (!validateById('#Purpose_Code', 'pms_Appraisal')) {
        isValid = false;
    }

    

    return isValid;
}





function fillData() {
    debugger;
    
    var DepartmentCode = $('#Department_Code').val();
    var PurposeCode = $('#Purpose_Code').val();

    if (!validateFillData()) {
        return false;
    }
    showSpinner();
    var GradeCode = $('#Grade_Code').val();
    $.ajax({

        type: 'GET',
        url: '/Master/NightAllowanceAmountSettings/GetNAASList',
        dataType: 'html',
        data: { DepartmentCode: DepartmentCode, PurposeCode: PurposeCode, GradeCode: GradeCode},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#NAASList').html("");
                $('#NAASList').html(result);
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

    prevTab = Tab;
}





var purposeChangeCount = 0;

//function PurposeChangeHandler() {
//    purposeChangeCount++;

    
//    if (purposeChangeCount === 1) {
//        return;
//    }

    
//    fillData();
//}

function PurposeChangeHandler() {
    // 🔥 CHANGED — Always call fillData when dropdown changes
    if (!validateFillData()) {
        return;
    }
    fillData();
}



function AddNewOrEditClick(Code,Type) {
    debugger;
    var DepartmentCode = $('#Department_Code').val();
    var PurposeCode = $('#Purpose_Code').val();
    var GradeCode = $('#Grade_Code').val();
    if (Type == 'MODIFY') {

        
            $.ajax({

                type: 'GET',
                url: '/Master/NightAllowanceAmountSettings/Edit',
                dataType: 'html',
                data: { Code:Code, DepartmentCode: DepartmentCode, PurposeCode: PurposeCode, GradeCode: GradeCode },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewOrEditSlider').html("");
                        $('#AddNewOrEditSlider').html(result);
                    }


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);


                }
            });

        }

    

    else {
        var url = "/Master/NightAllowanceAmountSettings/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/NightAllowanceAmountSettings/Create',
            dataType: 'html',
            data: { DepartmentCode: DepartmentCode, PurposeCode: PurposeCode, GradeCode: GradeCode},
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditSlider').html("");
                    $('#AddNewOrEditSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }


}



function NAASSave(mode) {
    debugger;
    
   
    if (validateNAAS()) {

        var DepartmentCode = $('#Department_Code').val();
        var PurposeCode = $('#Purpose_Code').val();
        var GradeCode = $('#Grade_Code').val();
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/NightAllowanceAmountSettings/Save',
                    dataType: 'json',
                    data: $('#NightAllowanceAmountSettingsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddnewOrEditDiv');
                                //OnTabClick(prevTab, currentTab);
                                //tabClick(document.getElementById('tabsDropdownDepartment'))
                                var url = "/Master/NightAllowanceAmountSettings/GetNAASList?DepartmentCode=" + DepartmentCode + "&PurposeCode=" + PurposeCode + "&GradeCode=" + GradeCode;
                                $('#NAASList').load(url, function () { });

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



function validateNAAS() {
    var i = 0;

    if (!validateById('#Department_Code_S1', 'DepartmentName')) i++;

    if (!validateById('#Purpose_Code_S1', 'PurposeName')) i++; // FIXED

    if (!validateById('#Grid_Code_S1', 'Grade')) i++;
    if (!validateById('#TimeFrom', 'TimeFrom')) i++;
    if (!validateById('#TimeTo', 'TimeTo')) i++;
    if (!validateById('#Amount', 'Amount')) i++;

    return i === 0;
}





function onDeleteClick( Code, Type) {

    debugger;
    var DepartmentCode = $('#Department_Code').val();
    var PurposeCode = $('#Purpose_Code').val();
    var GradeCode = $('#Grade_Code').val();
    
    

        $.ajax({
            type: 'GET',
            url: '/Master/NightAllowanceAmountSettings/Delete',
            dataType: 'html',
            data: { Code:Code,DepartmentCode: DepartmentCode, PurposeCode: PurposeCode, GradeCode: GradeCode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteSlider').html("");
                    $('#DeleteSlider').html(result);
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



function confirmDelete(Code, Type) {
    var DepartmentCode = $('#Department_Code').val();
    var PurposeCode = $('#Purpose_Code').val();
    var GradeCode = $('#Grade_Code').val();
    debugger
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/NightAllowanceAmountSettings/Delete',
                dataType: 'json',
                data: $('#DeleteNAAS').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownDepartment'));
                            var url = "/Master/NightAllowanceAmountSettings/GetNAASList?DepartmentCode=" + DepartmentCode + "&PurposeCode=" + PurposeCode + "&GradeCode=" + GradeCode;
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'ACTIVE';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/NightAllowanceAmountSettings/Delete?Code=" + Code + "&Type=" + Type;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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