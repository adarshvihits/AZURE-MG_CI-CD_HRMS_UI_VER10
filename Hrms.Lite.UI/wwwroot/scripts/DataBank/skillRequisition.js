function getRequisitionList() {
    debugger;
    if ($('#SkillTypeCode').val() != "" || $('#SkillProficiencyCode').val() != "") {
        $.ajax({

            type: 'GET',
            url: '/DataBank/SkillRequisition/GetRequisitionList',
            dataType: 'html',
            data: { SkillTypeCode: $('#SkillTypeCode').val(), SkillProficiencyCode: $('#SkillProficiencyCode').val() },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    /*$('body').append('<div class="form-overlay"></div>');*/
                    $('#RequisitionList').html("");
                    $('#RequisitionList').html(result);
                }

                //hideSpinner();
            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
    }
    else {
        return false;
    }
}

function SaveRequisition() {
    debugger;
    var i = 0;
    if (!validateById("#SkillTypeCode", 'skilltype')) {
        $('#SkillTypeCode').focus();
        i++;
    }
    if (!validateById("#SkillProficiencyCode", 'skillproficiency')) {
        $('#SkillProficiencyCode').focus();
        i++;
    }
    if ($('#SkillRequisitionList_Count').val() == '') {
        swal.fire("No Skills Available on this combination");
        i++;
    }
    if (i == 0) {
        var table = $('#SkillRequisitionTable').DataTable();
        var data = table.$('input,select,textarea').serializeArray();
        var skilltype = $('#SkillTypeCode').val();
        var SkillProficiency = $('#SkillProficiencyCode').val();
        data.push({ name: 'SkillProficiencyCode', value: SkillProficiency });
        data.push({ name: 'SkillTypeCode', value: skilltype });
        swal.fire({
            text: "Do you want to update",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/SkillRequisition/Create',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                getRequisitionList();
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