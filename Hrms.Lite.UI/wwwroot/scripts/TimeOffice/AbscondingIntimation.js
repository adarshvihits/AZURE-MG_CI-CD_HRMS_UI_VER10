function View(EmployeeGI) {
    showSpinner();
    debugger
    $.ajax({
        type: 'GET',
        url: '/TimeOffice/AbscondingIntimation/Detag',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },
        success: function (result) {
            var m = $('.addRecipientPopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);

        }
    });
}



function validateEntry() {
    debugger;
    var i = 0;
    
    if (!validateById('#DetagRemarks', 'RemarksEntered'))
        i++;
    if (!validateById('#DatagDate', 'Date'))
        i++;
    
    if (i == 0)
        return true;

    else {
        return false;
    }

}



function onSubmit() {
    debugger
    if (validateEntry()) {
        var data = new FormData($('#erEntry_Form')[0]);

        swal.fire({
            text: "Do you want to Apply?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/TimeOffice/AbscondingIntimation/Submit',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Applied Successfully").then(function () {
                                window.location.href = "/TimeOffice/AbscondingIntimation/Index";
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
    else
        return false;
}



function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();

        $("#SearchId").val($('#FilterList_Code').val())

        if (mode == 'apply') {

            if (filterApplicable >= 1) {
                GetList();
                CloseSlider('basicFilter');
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                GetList();
                CloseSlider('basicFilter');
                $('#filterForm').addClass('filter-apply');
            }
        }
        else {

            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            GetList();

            $('#filterForm').removeClass('filter-apply');
        }
    
}





function GetList() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();

    showSpinner();
    $.ajax({

        type: 'Post',
        url: '/TimeOffice/AbscondingIntimation/FilterList',
        dataType: 'html',
        data: filter1,

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
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



