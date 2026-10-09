function onAddNewOrEditClick(mode) {
    debugger;

    
        var url = "/Admin/OrganizationTask/Create";
        $.ajax({

            type: 'GET',
            url: '/Admin/OrganizationTask/Create',
            dataType: 'html',
            data: { mode:mode },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#organizationslider').html("");
                    $('#organizationslider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    

}



function OnOrganizationSaveClick(mode) {

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
                url: '/Admin/OrganizationTask/Save',
                dataType: 'json',
                data: $('#organizationsave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('organizationsave');
                            var url = "/Admin/OrganizationTask/Index";
                            window.location.href = url;
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },



            });
        }

    })


}

function onDeleteClick(TaskCode, mode) {


    $.ajax({
        type: 'GET',
        url: '/Admin/OrganizationTask/Delete',
        dataType: 'html',
        data: { TaskCode: TaskCode, Mode: mode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#organizationslider').html("");
                $('#organizationslider').html(result);
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



function OnOrganizationDeleteClick(TaskCode) {

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
                url: '/Admin/OrganizationTask/TaskDelete',
                dataType: 'json',
                data: $('#OrganizationDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            var url = "/Admin/OrganizationTask/Index";
                            window.location.href = url;
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },



            });
        }

    })


}

function OnTaskClick(TaskCode, Task) {

    debugger;
    $.ajax({
        type: 'GET',
        url: "/Admin/OrganizationTask/GetTaskPopup?TaskCode= " + TaskCode +  "&Task=" + Task,

        dataType: 'html',

        success: function (Data) {
            var m = $('#Task_Popup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function statusUpdate(TaskCode) {
    debugger;
    var StatusCode = $('#StatusCode').val();
    swal.fire({
        text: "Do you want to Update",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'GET',
                url: '/Admin/OrganizationTask/TaskUpdate',
                dataType: 'html',
                data: { TaskCode: TaskCode, StatusCode: $('#StatusCode').val() },
                success: function (result) {
                  
                    window.location.reload()
                 
                },
             });
        }
     })

}
