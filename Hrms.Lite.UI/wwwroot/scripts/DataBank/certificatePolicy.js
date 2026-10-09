function AddNewOrEditClick(Type,CertificateCode) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        
        $.ajax({

            type: 'GET',
            url: '/DataBank/CertificatePolicy/Edit',
            dataType: 'html',
            data: { CertificateCode: CertificateCode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditCertificatePolicySlider').html("");
                    $('#AddNewOrEditCertificatePolicySlider').html(result);
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
        $.ajax({

            type: 'GET',
            url: '/DataBank/CertificatePolicy/Create',
            dataType: 'html',
            data: {  },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditCertificatePolicySlider').html("");
                    $('#AddNewOrEditCertificatePolicySlider').html(result);
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




function YearClick() {
  

    debugger;
    var year = $('#Years').prop('checked');
    if (year) {
        $('#LifeTime').prop('checked', false);
        $('.yeartxtbox').show();
    } else {
        $('.yeartxtbox').hide();
    }

}


function LifeTimeClick() {
    if ($('#LifeTime').is(':checked')) {
        $('#Years').prop('checked', false);    
        $('.yeartxtbox').hide();               
    }
}








function Save(mode, CertificateCode) {
    debugger;
    var Validity;
    if ($('#Years').prop('checked') == true) {
        Validity = 'Year';
    } else {
        Validity = 'LifeTime';
    }

    if (validateNew()) {
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
                    url: '/DataBank/CertificatePolicy/Save?Validity=' + Validity + '&CertificateCode=' + CertificateCode,
                    dataType: 'json',
                    data: $('#CertificatePolicySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                                var url = "/DataBank/CertificatePolicy/GetList";
                                $('#CertificatePolicylist').load(url, function () { });
                                window.location.reload();
                            });
                        } else {
                            swal.fire(result.message).then(function () {
                                // Optional: handle any post-error UI reset here
                            });
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        } else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            } else {
                return false;
            }
        }); // <-- This closing bracket was missing

    }
    else {
        return false;
    }
}






function onDeleteClick(mode,CertifcateCode) {

    debugger;

        $.ajax({
            type: 'GET',
            url: '/DataBank/CertificatePolicy/Delete',
            dataType: 'html',
            data: { CertifcateCode: CertifcateCode },
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


function confirmDelete(CertificateCode) {
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
                url: '/DataBank/CertificatePolicy/ConfirmDelete',
                dataType: 'json',
                data: { CertificateCode: CertificateCode },
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/DataBank/CertificatePolicy/GetList";
                            $('#CertificatePolicylist').load(url, function () { });
                            window.location.reload();
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            

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



function validateNew() {

    debugger;
    var i = 0;

    if (!validateById('#Name', 'CertificateName')) {
        document.getElementById("Name").focus();
        i++;
    }
    if (!validateById('#ShortName', 'CertificateShortName')) {
        document.getElementById("ShortName").focus();
        i++;
    }

    if (!validateById('#CertificateCategory_Code', 'Category')) {
        document.getElementById("CertificateCategory_Code").focus();
        i++;
    }
    if (!validateById('#MaximumReimbursement', 'MaximumReimbursement')) {
        document.getElementById("MaximumReimbursement").focus();
        i++;
    }
    if ($('#Years').prop('checked') == true) {
        if (!validateById('#YearsValue', 'yeartxtbox')) {
            document.getElementById("YearsValue").focus();
            i++;
        }
    }


    if (i == 0)
        return true;
    else
        return false;
}









function AddNewPopUp() {
    debugger

    $.ajax({
        type: 'GET',
        url: "/DataBank/CertificatePolicy/CertificationPolicyPopUp",

        dataType: 'html',

        success: function (Data) {
            debugger;
            var m = $('.Exit_Questionnaire');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}




function SaveCertificate() {
    debugger;
    var Validity;
    

   
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
                    url: '/DataBank/CertificatePolicy/SaveCertificate' ,
                    dataType: 'json',
                    data: $('#CertificatePopupsave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewDiv');
                               // var url = "/DataBank/CertificatePolicy/GetList";
                                $('#CertificatePopupsave').load(url, function () { });
                                window.location.reload();
                            });
                        } else {
                            swal.fire(result.message).then(function () {
                                // Optional: handle any post-error UI reset here
                            });
                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        } else {
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            } else {
                return false;
            }
        }); // <-- This closing bracket was missing

    
}





function SaveCertificate() {
    debugger;
    data = new FormData($('#Certificate_Entry')[0]);

    swal.fire({
        text: "Do you want to Apply?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;

            $.ajax({
                type: 'POST',
                url: '/DataBank/CertificatePolicy/SaveCertificate',
                contentType: false,
                processData: false,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            window.location.href = "/DataBank/CertificatePolicy/Index";
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
                        window.location.reload();
                    }
                    else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });

        }
        else
            return false;
    })


}


function showDecompressedDocument() {
    debugger;
    var url = "/DataBank/CertificatePolicy/DownloadDoc" ;
    window.open(url, "_blank");
}