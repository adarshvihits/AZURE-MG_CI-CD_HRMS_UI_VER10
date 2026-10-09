function OnJobTitleClick(DesGI) {
    debugger
    var url = "/Master/RecruitmentSettings/AddNew?DesignationGI=" + DesGI;
    $('#AddNewSlider').load(url, function () { });
}



function validateRecruitmentSettings() {
    debugger
    return true;
}
function OnSaveClick() {
    debugger
    if (validateRecruitmentSettings()) {
        var l1 = document.getElementById("check1");
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
                    url: '/Master/RecruitmentSettings/Save',
                    dataType: 'json',
                    data: $('#Recruitment_Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNew');
                                var url = "/Master/RecruitmentSettings/Index";
                                window.location.href = url;
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

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

    }
    else {
        return false;
    }
}



function getSelectedVal(index) {
    debugger;
    if ($("#RecruitmentSettingsList_" + index + "__Applicable").is(':checked'))
        $("#RecruitmentSettingsList_" + index + "__RecruitmentSettingsType").val(1);
    else {
        $("#RecruitmentSettingsList_" + index + "__RecruitmentSettingsType").val(0);
        $("#RecruitmentSettingsList_" + index + "__Applicable").prop('checked', false);
    }
}