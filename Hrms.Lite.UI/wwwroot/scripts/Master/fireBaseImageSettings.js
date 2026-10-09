var prevTab = 'Tab2';
var currentTab = 'MobileSettings';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabssettings').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/FireBaseImageSettings/GetMobileGeneralSettingsDetails',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Listview').html("");
                $('#Listview').html(result);
            }
            hideSpinner();
        },
    });

    prevTab = Tab;
}
function AddNewOrEditClick(Code) {
    debugger;


        $.ajax({

            type: 'GET',
            url: '/Master/FireBaseImageSettings/Edit',
            dataType: 'html',
            data: { Code: Code },
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


function Save() {
    debugger;
    var EmployeeCode = $('#Employee_Code').val();
    data = new FormData($('#FireBaseImageSave')[0]);

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
                url: '/Master/FireBaseImageSettings/Save',
                contentType: false,
                processData: false,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger
                            url = "/Master/FireBaseImageSettings/GetMobileGeneralSettingsDetails?TabIndex=" + currentTab;
                            $('#FireBaseImageSettingslist').load(url, function () { });

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
    });
}


function previewDocument(absoluteURI) {
    debugger;
    if (absoluteURI != '' && absoluteURI != null)
        window.open(absoluteURI, "_blank");
    else
        swal.fire("No file uploaded");
}

function CheckImageExtensionSize(e) {
    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpg'];
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
        return false;
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only  jpg formats are allowed");
        $(e).val('');
        return false;
    }
    else {
        return true;
    }

}

function SaveMobileSettings() {
    debugger;
        
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
                    url: '/Master/FireBaseImageSettings/MobileGeneralSettingsSave',
                    dataType: 'json',
                    data: $('#MobileGeneralSettings').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                url = "/Master/FireBaseImageSettings/GetMobileGeneralSettingsDetails?TabIndex=" + currentTab;
                                $('#Listview').load(url, function () { });
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






