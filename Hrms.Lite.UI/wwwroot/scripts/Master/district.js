
var prevTab = 'ActiveTab';
var currentTab='ACTIVE';
var Type;

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownDistrict').val(currentTab);
   


    $.ajax({

        type: 'GET',
        url: '/Master/District/GetDistrictList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#districtlist').html("");
                $('#districtlist').html(result);
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

function tabClick(val)
{
    debugger;
    showSpinner();
    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
 
    $.ajax({

        type: 'GET',
        url: '/Master/District/GetDistrictList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#districtlist').html("");
                $('#districtlist').html(result);
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


function ActiveInactiveValidationDist(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/District/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivedist").prop("checked", true);
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

function onAddOrEditClick(Type,  LogGI,GI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#DistrictList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/District/Edit',
                dataType: 'html',
                data: { LogGI: LogGI, DistrictGI: GI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewOrEditDistrictSlider').html("");
                        $('#AddNewOrEditDistrictSlider').html(result);
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

  
    else {
        $.ajax({

            type: 'GET',
            url: '/Master/District/Create',
            dataType: 'html',
            data: { LogGI: LogGI, DistrictGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEditDistrictSlider').html("");
                    $('#AddNewOrEditDistrictSlider').html(result);
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




function onDeleteClick(LogGI, GI,Type, index) {

    debugger;

    if ($("#DistrictList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


   

    else {

        $.ajax({
            type: 'GET',
            url: '/Master/District/Delete',
            dataType: 'html',
            data: { LogGI: LogGI, DistrictGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteDistrictSlider').html("");
                    $('#DeleteDistrictSlider').html(result);
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

}


function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#DistrictList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

  

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/District/Approve',
            dataType: 'html',
            data: { LogGI: LogGI , DistrictGI: GI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#ApproveSlider').html("");
                    $('#ApproveSlider').html(result);
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

}



function confirmApproveDistrict(GI, LogGI) {
    debugger
    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/District/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/District/GetDistrictList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#districtlist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#districtlist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/District/Approve?DistrictGI=" + GI + "&LogGI=" + LogGI;
                            $('#PendingApprove').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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




function confirmRejectDistrict(GI, LogGI) {
    debugger
    swal.fire({
        text: "Do you want to Reject?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/District/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/District/GetDistrictList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#districtlist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".AtiveTab").removeClassaddClass("active");
                            $("#districtlist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/District/Approve?DistrictGI=" + GI + "&LogGI=" + LogGI;
                            $('#PendingApprove').load(url, function () { });
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


function confirmDelete(GI, LogGI) {
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
                url: '/Master/District/Delete',
                dataType: 'json',
                data: $('#DistrictDelete').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            var url = "/Master/District/GetDistrictList?TabIndex=" + 'ACTIVE';
                            $('#districtlist').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#districtlist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            //var url = "/Master/District/Delete?DistrictGI=" + GI + "&LogGI=" + LogGI;
                            //$('#delete').load(url, function () { });
                            //$('body').append('<div class="form-overlay"></div>');

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





function ExportToExcel() {
 
    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#DistrictList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/District/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#DistrictList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/District/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#DistrictList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/District/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}






function validateByClass(elementId, spanId) {
    debugger;
    if ($(elementId).val() == "") {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        $("#" + spanId + "").html("This field is required");
        return false;
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
}




function validateDistrict() {
    var i = 0;
    if (!validateById('#Name', 'districtName')) {
        i++;
    }
    if (!validateById('#ShortName', 'districtshortName')) {
        i++;
    }
    if (!validateById('#Country_Code', 'CountryName')) {
        i++;
    }
    if (!validateById('.dStCode', 'stateName')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}




function onCountryChange(sender, id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    debugger;
      $.getJSON("/Master/District/GetState", { id: cid }, function (data) {

          $('.dStCode option').remove();
         
          $('.dStCode').append(('<option value>--Select State--</option'));

        
        for (var i = 0; i < data.length; i++) {
            debugger
            $('.dStCode').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });

   
   
    
}

function DistrictSaveClick(mode) {
    debugger;
     if (validateDistrict()) {
    //console.log($("#State_Code").val());
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
                     url: '/Master/District/Save',
                     dataType: 'json',
                     data: $('#DistrictSave').serialize(),
                     success: function (result) {
                         if (result.success == true) {
                             swal.fire(result.message).then(function () {
                                 CloseSlider('AddNewDiv');
                                 var url = "/Master/District/GetDistrictList?TabIndex=" + currentTab;
                                 $('#districtlist').load(url, function () { });


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

