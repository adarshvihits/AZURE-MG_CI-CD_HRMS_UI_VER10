
function getList() {
    $.ajax({

        type: 'GET',
        url: '/Master/DueClearanceSettings/GetDueClearanceSettingsList',
        dataType: 'html',
        data: {},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#dueList').html("");
                $('#dueList').html(result);
            }   
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;

    if (Type == 'MODIFY') {

            $.ajax({

                type: 'GET',
                url: '/Master/DueClearanceSettings/Edit',
                dataType: 'html',
                data: { ParameterGI: GI, MODE: Type },
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
        var url = "/Master/Department/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/DueClearanceSettings/Create',
            dataType: 'html',
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

function onDepartmentChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == 0 || cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
   /* window.location.href = "/Master/DueClearanceSettings/GetResponsiblePerson?id="+cid;*/
    $.getJSON("/Master/DueClearanceSettings/GetResponsiblePerson", { id: cid }, function (data) {   

        $('#ResponsiblePersons option').remove();
        for (var i = 0; i < data.length; i++) {
            $('#ResponsiblePersons').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        $('.multiselectDropdownField').multiselect('rebuild');
    });

}

function DueClearanceSettingsSave(mode) {

    debugger;
    var i = 0;  
    if (!validateById('#ParameterName', 'paramname')) {
        i++;
    }
    if (!validateById('#Department_Code', 'department')) {

        i++;
    }
    if (!validateMultiselect('ResponsiblePersons', 'responsibleperson', 'respPerson')) {
        i++;
    }
    //if (!validateReqAuthority(document.getElementById('BatchUpdate_RequestAuthority_Code'), 'reqAuth', 'reqauthority')) {
    //    i++;
    //}
    if (i > 0) {
        return false;
    }
    else {
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
                    url: '/Master/DueClearanceSettings/Save',
                    dataType: 'json',
                    data: $('#DueClearanceSettingsSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                getList();
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
}

function onDeleteClick(mode,GI) {

    debugger;

   

        $.ajax({
            type: 'GET',
            url: '/Master/DueClearanceSettings/Delete',
            dataType: 'html',
            data: { ParameterGI: GI,Mode:mode },
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
function confirmDelete() {
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
                url: '/Master/DueClearanceSettings/Delete',
                dataType: 'json',
                data: $('#DueClearanceSettingsDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            getList();
                        });

                    }
                    else {
                        swal.fire(result.message)
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

function onExportToExcelClick() {
    debugger;
    var TableLength = $('#List_Count').val();
    if (TableLength > 0) {
        url = "/Master/DueClearanceSettings/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}