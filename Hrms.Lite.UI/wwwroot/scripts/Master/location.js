var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/Master/Location/GetLocationList";

    //$('#LocationList').load(url, { TabIndex: TabIndex }, function () { hideSpinner();});

    $.ajax({

        type: 'GET',
        url: '/Master/Location/GetLocationList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LocationList').html("");
                $('#LocationList').html(result);
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
    //var url = "/Master/Location/GetLocationList";
    //$('#LocationList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/Master/Location/GetLocationList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LocationList').html("");
                $('#LocationList').html(result);
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

function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#LocationList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    //else {

    //    var url = "/Master/Location/Approve?LocationGI=" + GI + "&LogGI=" + LogGI;
    //    $('#ApproveSlider').load(url, function () { });
    //    $('body').append('<div class="form-overlay"></div>');
    //}
    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Location/Approve',
            dataType: 'html',
            data: { LocationGI: GI, LogGI: LogGI },
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


function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#LocationList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);



        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Location/Edit',
                dataType: 'html',
                data: { LocationGI: GI, LogGI: LogGI },
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

   
    else {
        $.ajax({

            type: 'GET',
            url: '/Master/Location/Create',
            dataType: 'html',
            data: { LocationGI: GI, LogGI: LogGI },
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




function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#LocationList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


 
    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Location/Delete',
            dataType: 'html',
            data: { LocationGI: GI, LogGI: LogGI },
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
}



function onCountryChange(sender, id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").html("");
    }
    $.getJSON("/Master/Location/GetState", { id: cid }, function (data) {

        $('#State_Code option').remove();
        $('#State_Code').append('<option value="0">--Select State--</option');

        for (var i = 0; i < data.length; i++) {
            $('#State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
    $.getJSON("/Master/Location/GetDistrict", { id: 0 }, function (data) {

        $('#District_Code option').remove();
        $('#District_Code').append('<option value="0">--Select District--</option');

        for (var i = 0; i < data.length; i++) {
            $('#District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
    //}
}

function onStateChange(sender, id) {

    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").html("");
    }
    $.getJSON("/Master/Location/GetDistrict", { id: cid }, function (data) {

        $('#District_Code option').remove();
        $('#District_Code').append('<option value="0">--Select District--</option');

        for (var i = 0; i < data.length; i++) {
            $('#District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}
function onDistrictChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").html("");
    }
}




function confirmApprove(GI, LogGI) {
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
                url: '/Master/Location/Approve1',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Location/GetLocationList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger
                          
                            $('#LocationList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#LocationList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Location/Approve?LocationGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
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
                url: '/Master/Location/Delete',
                dataType: 'json',
                data: $('#locationDelete').serialize(),
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('Delete');
                            var url = "/Master/Location/GetLocationList?TabIndex=" + 'ACTIVE';
                            $('#LocationList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#LocationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Location/Delete?LocationGI=" + GI + "&LogGI=" + LogGI;
                            $('#Delete').load(url, function () { });
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




function confirmReject(GI, LogGI) {
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
                url: '/Master/Location/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('ApproveDiv');
                            var url = "/Master/Location/GetLocationList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#LocationList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#LocationList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Location/Approve?LocationGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
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




function SaveLocation(mode) {
    debugger;

    if (validateLocation()) {
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
                    url: '/Master/Location/Save',
                    dataType: 'json',
                    data: $('#locationSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit'); $('.AddNewOrEdit').removeClass('location-slidernew')
                                var url = "/Master/Location/GetLocationList?TabIndex=" + currentTab;
                                $('#LocationList').load(url, function () { });


                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
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
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#LocationList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Location/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#LocationList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Location/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#LocationList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Location/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}



function validateLocation() {
    var i = 0;

 
    var MailId = $('#EmailId').val();
    // Basic empty check
    var id = 'MailId';
  

    if (!validateById('#PTgroup_Code', 'PTGroup')) {
        document.getElementById("PTgroup_Code").focus();
        i++;
    }
   
    if (!validateById('#District_Code', 'LocDistrict')) {
        document.getElementById("District_Code").focus();
        i++;
    }
    if (!validateById('#State_Code', 'LocState')) {
        document.getElementById("State_Code").focus();
        i++;
    }
    if (!validateById('#Country_Code', 'LocCountry')) {
        document.getElementById("Country_Code").focus();
        i++;
    }
    if (!validateById('#Region_Code', 'LocRegion')) {
        document.getElementById("Region_Code").focus();
        i++;
    }
    if (!validateById('#LocationType_Code', 'LocationType')) {
        document.getElementById("LocationType_Code").focus();
        i++;
    }
    if (!validateById('#Area', 'LocArea')) {
        document.getElementById("Area").focus();
        i++;
    }
    if (!validateById('#Street', 'LocSreet')) {
        document.getElementById("Street").focus();
        i++;
    }
    if (!validateById('#DoorNumAndBuilding', 'LocDoorNumBuilding')) {
        document.getElementById("DoorNumAndBuilding").focus();
        i++;
    }
    if (!validateById('#ShortName', 'locShortName')) {
        document.getElementById("ShortName").focus();
        i++;
    }

    if (!validateById('#Name', 'LocationName')) {
        document.getElementById("Name").focus();
        i++;
    }
    if (!validateById('#AreaDropdown_Code', 'LocArea')) {
        document.getElementById("Name").focus();
        i++;
    }

    if (!validateById('#FunctionalState_Code', 'LocStatefun')) {
        document.getElementById("Name").focus();
        i++;
    }
    if (!validateById('#ERPCode', 'ERP')) {
        document.getElementById("Name").focus();
        i++;
    }
    if (!validateById('#OpeningDate', 'LocCompanyOpening')) {
        $('.kt_datepicker_2').datepicker('hide');   
        i++;
    }
    if (!validateById('#Category_Code', 'LocCategory')) {
        document.getElementById("Category_Code").focus();
        i++;
    }
    
    if (!validateById('#BusinessType_Code', 'LocBusinessType')) {
        document.getElementById("BusinessType_Code").focus();
        i++;
    }
    if (!validateById('#Zone_Code', 'LocZone')) {
        document.getElementById("Zone_Code").focus();
        i++;
    }
    if (!validateById('#Cluster_Code', 'LocCluster')) {
        document.getElementById("Cluster_Code").focus();
        i++;
    }
    if (!validateById('#WelfareAccNo', 'LocWelfareAccNo')) {
        document.getElementById("WelfareAccNo").focus();
        i++;
    }
    if (!validateById('#LWFAmountEmployer', 'LocLWFAmountEmployer')) {
        document.getElementById("LWFAmountEmployer").focus();
        i++;
    }
    if (!validateById('#LWFAmount', 'LocLWFAmount')) {
        document.getElementById("LWFAmount").focus();
        i++;
    }
    if (!validateById('#EstRegNo', 'LocEstRegNo')) {
        document.getElementById("EstRegNo").focus();
        i++;
    }
    if (!validateById('#M365CostCenter', 'LocM365CostCenter')) {
        document.getElementById("M365CostCenter").focus();
        i++;
    }
    if (!validateById('#Metro_Code', 'LocMetro')) {
        document.getElementById("Metro_Code").focus();
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;


    if (MailId === "") {
        $("#" + id).addClass('error-message');
        $(MailId).addClass('is-invalid');
        $("#" + id).html("This field is required");
        return false;
    }
    else {
        // More robust email regex
        let filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

        if (!filter.test(MailId)) {
            $("#" + id).addClass('error-message');
            $(MailId).addClass('is-invalid');
            $("#" + id).html('Invalid Email ID');
            i++;
        }

    }
}

function validateEmail(val, id) {
    debugger;
    let email = val.value.trim();
    val.value = email;

    // Basic empty check
    if (email === "") {
        $("#" + id).addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id).html("This field is required");
        return false;
    }
    else {
        // More robust email regex
        let filter = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

        if (!filter.test(email)) {
            $("#" + id).addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id).html('Invalid Email ID');
            return false;
        } else {
            $("#" + id).removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id).html("");
            return true;
        }
    }
}



//function validateURL()
//{
//    debugger;
//    var textval = document.getElementsByName("web");
//    var urlregex = /^(https?|ftp):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/;
//    return urlregex.test(textval);
//}


//function isValidURL(val,id) {
//    debugger;
//    var web = document.getElementById('WebAdderss').value;
//    var res = web.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    
//    if (res == "") {
       
//        $("#LocWebAddress").html('Invalid Web Address')
//    }
//    else
//        debugger;
//      return true;
       
//};

function isValidURL(val, id) {

    debugger
    var web = document.getElementById('WebAdderss').value;
 /*   var res = web.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);*/
    var res = web.match(/(http[s]?:\/\/)(www[\w]?\.)[\w]*\.[\w]{2,3}(\.[\w]{2})?$/);


    //if (web== "") {
    //    $("#" + id + "").addClass('error-message');
    //    $(val).addClass('is-invalid');
    //    $("#" + id + "").html("This field is required");
    //}
   
        if (res == null) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
          
            $("#" + id + "").html('Invalid Web Address')
            return false;
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $(val).removeClass('is-invalid');
            $("#" + id + "").html('')
            return true;
        }
       
    
}


function CopypasteValidation(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}

function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Location/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#activeInactiveLoc").prop("checked", true);
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




 