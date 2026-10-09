function Save() {
    debugger;
    var drop1 = $('#ShowCelebritiesinsocial').val();
    var drop2 = $('#ShowPhotoinsocial').val();
    if (validateCompanyGeneralSettings()) {
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
                    url: '/Master/CompanyGeneralSettings/Save?Drop1=' + drop1 + "&Drop2=" + drop2,
                    dataType: 'json',
                    data: $('#CompanyGeneralSettings').serialize() ,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                url = "/Master/CompanyGeneralSettings/Index";
                                window.location.href = url;
                            });

                        }

                        else {
                            debugger;
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }

                        else {
                            debugger;
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




function validateCompanyGeneralSettings() {
    var i = 0;
    if (!validateById('#NoticePeriod', 'NoticePeriod')) {
        i++;
    }
    if (!validateById('#NeedExcelPassword', 'genSettings')) {
        i++;
    }
    if (!validateById('#PunchingIDSameasComID', 'genSettings')) {
        i++;
    }
    if (!validateById('#PasswordExpiry', 'PasswordExpiry')) {
        i++;
    }
    if (!validateById('#SocialWallExpiry', 'SocialWallExpiry')) {
        i++;
    }
    if (!validateById('#ShowCelebritiesinsocial', 'ShowCelebinsocial')) {
        i++;
    }
    if (!validateById('#ShowPhotoinsocial', 'ShowPicinsocial')) {
        i++;
    }
    if (!validateById('#RegularizationmaxLimit', 'RegularizationLimit')) {
        i++;
    }
    if (!validateById('#RegularizationApplyDays', 'RegularizationDays')) {
        i++;
    }
    if (!validateById('#LoanWFEnableOrDisable', 'loanwf')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}



function showdropdown() {
    debugger;
    var drop1 = $('#ShowCelebritiesinsocial').val();
    if (drop1 == 1) {
        $('#drop2').removeClass('hide');
    }
    else {
        $('#drop2').addClass('hide');
    }
}







//function validateCompanyGeneralSettings() {
//    debugger;
//    const form = document.querySelector('#sectionForm');
//    const checkboxes = form.querySelectorAll('input[type=checkbox]');
//    const checkboxLength = checkboxes.length;
//    const firstCheckbox = checkboxLength > 0 ? checkboxes[0] : null;


//    for (let i = 0; i < checkboxLength; i++) {
//        if (checkboxes[i].checked == false || $('#DeductionType_Code').val() == 0) {
//            debugger
            

//            return false;
//        }
//        else
//            return true;


//    }
//}
