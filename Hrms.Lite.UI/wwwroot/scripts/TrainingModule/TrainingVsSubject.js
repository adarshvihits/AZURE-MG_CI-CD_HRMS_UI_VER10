function fillData() {
    debugger;
    var TrainingCode = $("#TrainingCode").val();

    $.ajax({

        type: 'GET',
        url: '/TrainingModule/TrainingVsSubject/GetList',
        dataType: 'html',
        data: { TrainingCode: TrainingCode },
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                $('#SubjectList').html("");
                $('#SubjectList').html(result);
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

}


function Save() {
    var i = 0;
    debugger
    var TrainingCode = $("#TrainingCode").val();
    if ($("#Trainingvssubject input[type=checkbox]:checked").length == 0)
     {
        swal.fire("Please select atleast one Subject");
        return false;
    }
    if (i == 0) {

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
                    url: '/TrainingModule/TrainingVsSubject/Save',
                    dataType: 'json',
                    data: $('#Trainingvssubjectform').serialize() ,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                debugger;
                                var url = '/TrainingModule/TrainingVsSubject/GetList?TrainingCode=' + TrainingCode;
                                $('#SubjectList').load(url, function () { });
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
    else
        return false;
}
function onAllClick(chkid, tableid) {
    debugger
    if ($('#' + chkid).is(':checked')) {
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    }
    else {
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
       
    }
}
function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {
        if (
            $('#dsg').find('input[type="checkbox"]:checked').length == $('#dsg').find('input[type="checkbox"]').length) 
          
            {
            debugger;
            $('#SelectAll').prop('checked', true);
            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
        }
        else {
            debugger;

            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
                $('#' + allCheckBox).prop('checked', true);
        
            else {
                $('#SelectAll').prop('checked', false);
            }
        }
    }
  
}