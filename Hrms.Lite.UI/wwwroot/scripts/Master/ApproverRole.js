
function getApproverRoleVsAuthority() {
    var url = "/DataBank/ApproverRoleVsAuthority/Index";

    //console.log(url);
    window.location.href = url;
    return true
}
function getDepartmentVsAuthority() {
    var url = "/DataBank/DepartmentVsAuthority/Index";

    //console.log(url);
    window.location.href = url;
    return true
}

function getRegionWiseDepartmentVsAuthority() {
    var url = "/DataBank/RegionWiseDepartmentVsAuthority/Index";

    //console.log(url);
    window.location.href = url;
    return true
}



var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';





function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownApproverRole').val(currentTab);
    //var url = "/Master/ApproverRole/GetApproverRoleList";  
    //$('#ApproverRoleList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });

    var ClassificationType = $('#Applicability').val();
    var Country = $('#Country').val();
    $.ajax({

        type: 'GET',
        url: '/Master/ApproverRole/GetApproverRoleList',
        dataType: 'html',
        data: { TabIndex: TabIndex, ClassificationType: ClassificationType, Country: Country},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ApproverRoleList').html("");
                $('#ApproverRoleList').html(result);
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
    prevTab = Tab;
}

function tabClick(val) {
    debugger;
    var ClassificationType = $('#Applicability').val();
    var Country = $('#Country').val();

    currentTab = $(val).val();
    showSpinner();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
   

    $("." + prevTab).addClass("active");
   
    $.ajax({

        type: 'GET',
        url: '/Master/ApproverRole/GetApproverRoleList',
        dataType: 'html',
        data: { TabIndex: TabIndex, ClassificationType: ClassificationType, Country: Country},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ApproverRoleList').html("");
                $('#ApproverRoleList').html(result);
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

function getList() {
    debugger;
    var ClassificationType = $('#Applicability').val();
    var Country = $('#Country').val();
     
    showSpinner();
     
     

    $.ajax({

        type: 'GET',
        url: '/Master/ApproverRole/GetApproverRoleList',
        dataType: 'html',
        data: { TabIndex: currentTab, ClassificationType: ClassificationType, Country: Country },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ApproverRoleList').html("");
                $('#ApproverRoleList').html(result);
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


function ActiveInactiveValidationpayRollGP(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/ApproverRole/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#activeinactiveApproverRole").prop("checked", true);
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



function onAddNewOrEditClick(Type, AuthorityCode, index) {
    debugger;

    if (Type == 'MODIFY') {


            $.ajax({

                type: 'GET',
                url: '/Master/ApproverRole/Edit',
                dataType: 'html',
                data: { AuthorityEntityCode: AuthorityCode },
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
        $.ajax({

            type: 'GET',
            url: '/Master/ApproverRole/Create',
            dataType: 'html',
            data: { AuthorityEntityCode: AuthorityCode },
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


function getDetails(AuthorityCode) {

    debugger;



    $.ajax({
        type: 'GET',
        url: '/Master/ApproverRole/View',
        dataType: 'html',
        data: { AuthorityEntityCode: AuthorityCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#viewSlider').html("");
                $('#viewSlider').html(result);
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

function onDeleteClick(AuthorityCode, Type, index) {

    debugger;

 

        $.ajax({
            type: 'GET',
            url: '/Master/ApproverRole/Delete',
            dataType: 'html',
            data: { AuthorityEntityCode: AuthorityCode },
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

function confirmDelete(AuthorityCode) {
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
                url: '/Master/ApproverRole/Delete',
                dataType: 'json',
                data: $('#deleteApproverRole').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/ApproverRole/GetApproverRoleList?TabIndex=" + 'ACTIVE';
                            $('#ApproverRoleList').load(url, function () { });
                            $(".ActiveTab").addClass("active");
                            $("#ApproverRoleList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ApproverRole/Delete?AuthorityEntityCode=" + AuthorityCode;
                            $('#delete').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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
function onRoleChange(sender, id) {
    var roleid = $(sender).val();

    if (roleid === "") {
        $("#" + id).addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id).html("This field is required");
    } else {
        $("#" + id).removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id).html("");
    }
    $.ajax({
        url: '/Master/ApproverRole/GetApplicabilityDropdown', // Update this to match your controller route
        type: 'GET',
        data: { id: roleid, Country:$('#Country_Code').val() },
        success: function (response) {
            var $dropdown = $('#ApplicabilityDropdown');
            $dropdown.empty();
            $dropdown.append($('<option>').val('').text('Select'));

            $.each(response.dropdown, function (i, item) {
                $dropdown.append($('<option>').val(item.code).text(item.name));
            });
            debugger;
            $('#ApplicabilityCode').val(response.code); // Update hidden field
            $('#ApplicabilityName').text(response.name); // Update label
            if ($('#ApplicabilityCode').val() == 4) {
                $('#Div_Applicability').hide();
            }
            if (response.dropdown.length > 0) {
                $('#Div_Applicability').show();
            } else {
                $('#Div_Applicability').hide();
            }
        }
,
        error: function () {
            $('#Div_Applicability').hide();
            
        }
    });
}

function onCountryChange(sender, id) {
    var cid = $(sender).val();

    if (cid === "") {
        $("#" + id).addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id).html("This field is required");
    } else {
        $("#" + id).removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id).html("");
    }
    if ($('#ApplicabilityCode').val() == 2) {
        $.getJSON("/Master/ApproverRole/GetCountryVsLocationDropDown", { id: cid }, function (data) {
            var $dropdown = $('#ApplicabilityDropdown');
            $dropdown.empty();
            $dropdown.append($('<option>').val('').text('Select'));

            $.each(data, function (i, item) {
                $dropdown.append($('<option>').val(item.value).text(item.text));
            });

            //var $locations = $('#Locations');
            //$locations.empty(); // Clear old options

            //if (data && data.length > 0) {
            //    $.each(data, function (i, item) {
            //        $locations.append($('<option>', {
            //            value: item.value,
            //            text: item.text
            //        }));
            //    });
            //} else {
            //    // Optional placeholder if no locations
            //    //$locations.append('<option value="">No locations available</option>');
            //}

            //// Rebuild Bootstrap Multiselect UI
            //$locations.multiselect('rebuild');
        });
    }
}


function SaveApproverRole(mode) {
    debugger;
    if (validateApproverRole()) {
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
                    url: '/Master/ApproverRole/Save',
                    dataType: 'json',
                    data: $('#ApproverRolesave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
                                var url = "/Master/ApproverRole/GetApproverRoleList?TabIndex=" + currentTab;
                                $('#ApproverRoleList').load(url, function () { });


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
    else {
        return false;
    }
}




function ExportToExcel() {
    debugger;
     if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#ApproverRoleList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/ApproverRole/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#ApproverRoleList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ApproverRole/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }

    }
}


function validateApproverRole() {
    var i = 0;
    //if (!validateById('#Name', 'ApproverRoleName')) {
    //    i++;
    //}
    if (!validateById('#Applicability_Code', 'applicability')) {
        i++;
    }
    //if (!validateMultiselect('Countrys', 'country', 'countrydiv'))
    //    i++;
    if (!validateById('#Role_Code', 'ApproverRoleName')) {
        i++;
    }
    if (!validateById('#Country_Code', 'country')) {
        i++;
    }
    //if (!validateMultiselect('Locations', 'location', 'Locationdiv'))
    //    i++;
    //if (!validateMultiselect('Departments', 'department', 'Departmentdiv'))
    //    i++;
    //if (!validateMultiselect('Sections', 'section', 'sectiondiv'))
    //    i++;
    if (!validateById('#Employee_Code', 'authority')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}

function getDefaultSettings() {
    var url = "/Master/ApproverRoleDefaultSettings/Index";

    //console.log(url);
    window.location.href = url;
    return true
}