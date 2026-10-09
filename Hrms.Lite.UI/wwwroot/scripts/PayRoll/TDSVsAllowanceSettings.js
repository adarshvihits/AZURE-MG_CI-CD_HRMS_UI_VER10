function TDSVsAllowanceSave() {
    debugger;
    
    if (validateTDSVsAllowance()) {
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
            url: '/PayRoll/TDSVsAllowanceSettings/Save',
            dataType: 'json',
            data: $('#TDSVsAllowanceSave').serialize(),
            success: function (result) {
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        url = "/PayRoll/TDSVsAllowanceSettings/Index";
                        window.location.href = url;
                    });

                }

                else {
                    swal.fire(result.message).then(function () {

                    });
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
    else {
        return false;
    }
}

function validateTDSVsAllowance() {
    //debugger;
    //const form = document.querySelector('#sectionForm');
    //const checkboxes = form.querySelectorAll('input[type=checkbox]');
    //const checkboxLength = checkboxes.length;
    //const firstCheckbox = checkboxLength > 0 ? checkboxes[0] : null;
    

    //for (let i = 0; i < checkboxLength; i++) {
    //    if (checkboxes[i].checked == false || $('#DeductionType_Code').val()==0) {
    //        debugger
          

    //        return false;
    //    }
    //    else
    //        return true;


    //}
    if ($('#tdsVsAllowCheckBox').find('input[type="checkbox"]:checked').length > 0) {

        return true;
    }
    else {
       
        swal.fire("No Data to Save!")
        return false;
    }
}


function validateComponent(rowid,val, id) {
   
 
    if (val.value == 0 && $('#settings_' + rowid + '__YN').is(':checked')) {
      
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }

  else  if (val.value >0 && !$('#settings_' + rowid + '__YN').is(':checked')) {
        $('#settings_' + rowid + '__YN').prop('checked', true);

    }
    else {
      
        $(val).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");

      
        val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");

        return val.value;
    }
}

function validateCheckBox(id, rowid) {

    if ($('#settings_' + rowid + '__YN').is(':checked') && $('#settings_' + rowid + '__ComponentType_Code').val()==0 ) {
        $('#settings_' + rowid + '__ComponentType_Code').addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else if (!$('#settings_' + rowid + '__YN').is(':checked')  && $('#settings_' + rowid + '__ComponentType_Code').val() > 0)
    {
        $('#settings_' + rowid + '__ComponentType_Code').val(0);
        $('#settings_' + rowid + '__ComponentType_Code').removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
       
    }
    else if (!$('#settings_' + rowid + '__YN').is(':checked') && $('#settings_' + rowid + '__ComponentType_Code').val() == 0) {
 
        $('#settings_' + rowid + '__ComponentType_Code').removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");

    }
    else if ($('#settings_' + rowid + '__YN').is(':checked') && $('#settings_' + rowid + '__ComponentType_Code').val() > 0) {
 
        $('#settings_' + rowid + '__ComponentType_Code').removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
       
    }
    
}