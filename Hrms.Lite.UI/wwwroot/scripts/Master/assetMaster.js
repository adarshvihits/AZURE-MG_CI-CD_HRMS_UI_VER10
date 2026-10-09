var prevTab = 'ActiveTab';
var currentTab='ACTIVE';
//function TabChange(res) {
//    if (res == 'A') {
//        $('#ActiveList').removeClass('hide');
//        $('#InactiveList').addClass('hide');
//        $('#PendingList').addClass('hide');
//    }
//    else if (res == 'I') {
//        $('#UserRoleList').addClass('hide');
//        $('#InactiveList').removeClass('hide');
//        $('#PendingList').addClass('hide');
//    }
//    else {
//        $('#UserRoleList').addClass('hide');
//        $('#InactiveList').addClass('hide');
//        $('#PendingList').removeClass('hide');
//    }
//}


function OntabClick(val) {
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
    //var url = "/Master/AssetMaster/GetAssetList";
    //$('#ActiveList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/AssetMaster/GetAssetList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html("");
                $('#ActiveList').html(result);
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



function onAddNewClick() {
    debugger;
    var Code = 1;
    var url = "/Master/AssetMaster/AssetSave";
    $('.assetSaveclose').load(url, function () { });
    $('#newRequestSlider').removeClass('hide');
    $('.assetSaveclose').show();
}




//function AddNewOrEdit(Action, GI, LogGI) {
//    debugger;
//    var Code = 1;
//    debugger;
//    if (Action == 'Edit') {
//        var url = "/Master/AssetMaster/Edit?AssetGI=" + GI + "&LogGI=" + LogGI;
//    }
//    else {
//        var url = "/Master/AssetMaster/Create";
//    }
//    $('.assetSaveclose').load(url, function () { });
//    $('#newRequestSlider').removeClass('hide');
//    $('.assetSaveclose').show();
//}



function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {
        debugger
        if ($("#AssetList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);

        }
        else {
            //var url = "/Master/AssetMaster/Edit?AssetGI=" + GI + "&LogGI=" + LogGI;

            //$('#newRequestSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/AssetMaster/Edit',
                dataType: 'html',
                data: { AssetGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#newRequestSlider').html("");
                        $('#newRequestSlider').html(result);
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
        debugger
        //var url = "/Master/AssetMaster/Create";
        //$('#newRequestSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');
        var url = "/Master/AssetMaster/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/AssetMaster/Create',
            dataType: 'html',
            data: { AssetGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#newRequestSlider').html("");
                    $('#newRequestSlider').html(result);
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



function tabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
   /* var url = "/Master/AssetMaster/GetAssetList";*/
    $('#tabsDropdownAsset').val(currentTab);
    //if (TabIndex != 'PendingTab')
    //    $('#ActiveList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    //else
    //    $('#ActiveList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    $.ajax({

        type: 'GET',
        url: '/Master/AssetMaster/GetAssetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html("");
                $('#ActiveList').html(result);
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



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#AssetList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
       

        $.ajax({
            type: 'GET',
            url: '/Master/AssetMaster/GetDeleteAssetSlider',
            dataType: 'html',
            data: { AssetGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteAssetSlider').html("");
                    $('#deleteAssetSlider').html(result);
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



//function ConfirmDelete(GI, LogGI) {

//    if (confirm("Do you want to Delete?")) {
//        debugger;
//        var url = "/Master/AssetMaster/Delete" + "?GI=" + GI;
//        $.getJSON(url, function (result) {
//            swal.fire(result.message).then(function () {
//                CloseSlider('DeleteDiv');
//                if (result.success) {
//                    var url = "/Master/AssetMaster/Delete";
                   
//                }
//            })
//        })
       
//    }
//    else {
//        return false;
//    }
//}



function confirmDelete(GI, LogGI) {

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
                url: '/Master/AssetMaster/Delete',
                dataType: 'json',
                data: $('#DeleteAsset').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            var url = "/Master/AssetMaster/GetAssetList?TabIndex=" + 'ACTIVE';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                           
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AssetMaster/Delete?AssetGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteDiv').load(url, function () { });
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








function onApproveClick(GI, LogGI, Type, index) {

    debugger;
  
    if ($("#AssetList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        //var url = "/Master/AssetMaster/Approve?AssetGI=" + GI + "&LogGI=" + LogGI;

        //$('#approveSlider').load(url, function () { });
        //$('body').append('<div class="form-overlay"></div>');


        $.ajax({

            type: 'GET',
            url: '/Master/AssetMaster/Approve',
            dataType: 'html',
            data: { AssetGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#approveSlider').html("");
                    $('#approveSlider').html(result);
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


//function ConfirmAssetApprove() {
//    if (confirm("Do you want to Confirm?")) {
//        debugger;
//        console.log($('#approve').serialize());
//        $.ajax({

//            type: 'POST',
//            url: '/Master/AssetMaster/Approve',
//            dataType: 'json',
//            data: $('#approve').serialize(),
//            success: function (result) {
//                if (result.success == true) {
//                    debugger;
//                    swal.fire(result.message).then(function () {
//                        CloseSlider('PendingApprove');
//                        var url = "/Master/AssetMaster/GetAssetList?TabIndex=" + 'APPROVAL_PENDING';
//                        $('#PendingList').load(url, function () { });
//                        $(".PendingTab").addClass("active");
//                        $(".ActiveTab").removeClass("active");
//                        $(".InctiveTab").removeClass("active");
//                        $("#pendingList").removeClass("hide");
//                    });
                   

//                }
//                else {
//                    debugger;
//                    if (result.message == "MAKER_CHECKER")
//                    {
//                        swal.fire("Maker and checker cannot be same.")
//                    }
//                    else {
//                        swal.fire(result.message).then(function () {


//                        });
//                    }
//                }

//            },
//            error: function (xhr, textStatus, errorThrown) {
//                debugger;
//                toastr.error(xhr.responseText);
//            }
//        });
//    }
//    else {
//        debugger;
//        return false;
//    }
//}

function ConfirmAssetApprove(GI, LogGI) {
    //$.ajaxSetup({
    //    async: false
    //});
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
                url: '/Master/AssetMaster/Approve',
                dataType: 'json',

                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/AssetMaster/GetAssetList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger;
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $(".InctiveTab").removeClass("active");
                           /* $("#ActiveList").addClass("hide");*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AssetMaster/Approve?AssetGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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


function ConfirmAssetReject(GI, LogGI) {

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
                url: '/Master/AssetMaster/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/AssetMaster/GetAssetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $(".InctiveTab").removeClass("active");
                            
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/AssetMaster/Approve?AssetGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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

function onExportToExcelClick() {
    debugger;
    /*    var pendingTableLength = $('#LeavePeriodList_Count').val();*/
    var assetlist = $('#AssetList_Count').val();
    if (currentTab != "APPROVAL_PENDING") {
  
      /*  var trainingMasterTableLength = document.getElementById("AssetMasterTable").rows.length - 1;*/
        if (assetlist > 0) {
            url = "/Master/AssetMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else {
        /*var trainingMasterPendingTableLength = document.getElementById("AssetMasterPendingTable").rows.length - 1;*/
        if (assetlist > 0) {
            url = "/Master/AssetMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}



function assetSave(mode) {
    debugger;
    var ab = $('#dopp').val();
    if (validateAsset())
    {
        //console.log(new Date(ab));

        var i = 0;
        var model = { Name: $('#Name').val(), shortname: $('#ShortName').val(), assetcategorycode: $('#AssetCategory_Code').val(), assetcode: $('#AssetCode').val(), dop: Date($('.dop').val()), brand: $('#Brand').val(), model: $('#ModelNo').val(), cost: $('#Cost').val(), issuingdept: $('#IssuingDepartment_Code').val(), conditioncode: $('#Condition_Code').val(), status: $('#Active').val(), description: $('#Description').val(), mode: $('#Mode').val(), loggi: $('#LogGI').val(), gi: $('.assetgi').val() }

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
                    url: '/Master/AssetMaster/Save',
                    dataType: 'json',
                    data: $('#assetSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('assetSaveclose');
                                var url = "/Master/AssetMaster/Index";
                                window.location.href = url;
                            });

                        }
                        else if (result.message == "MAKER_CHECKER") {
                            swal.fire("Maker and checker cannot be same.").then(function () {
                                //var url = "/Master/Country/Approve?CountryGI=" + GI + "&LogGI=" + LogGI;
                                //$('#ApproveSlider').load(url, function () { });
                                //$('body').append('<div class="form-overlay"></div>');

                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {
                                //var url = "/Master/Country/Approve?CountryGI=" + GI + "&LogGI=" + LogGI;
                                //$('#ApproveSlider').load(url, function () { });
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
    else {
        return false;
    }
}


function validateAsset() {
    var i = 0;
    if (!validateById('#Name', 'AssetName')) {
        i++;
    }
    if (!validateById('#ShortName', 'AssetShortName')) {
        i++;
    }
    if (!validateById('#AssetCategory_Code', 'AssetCategory')) {
        i++;
    }
    if (!validateById('#AssetCode', 'AssetCod')) {
        i++;
    }
    if (!validateById('.dop', 'DOP')) {
        debugger;
        i++;
    }
    if (!validateById('#Brand', 'AsBrand')) {
        i++;
    }
    if (!validateById('#ModelNo', 'modelno')) {
        i++;
    }
    if (!validateById('#Cost', 'Ascost')) {
        i++;
    }
    if (!validateById('#IssuingDepartment_Code', 'assetdepartment')) {
        i++;
    }
    if (!validateById('#Condition_Code', 'assetcondition')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}

function firstLetterCaps(val, id) {
    debugger;

    stringval = val.value;
    if (val.value == "") {
        $("#" + id + "").html("This field is required");
    }
    else {
        val.value = val.value.toLowerCase();
        val.value = val.value.
            replace(/(^\s*)|(\s*$)/gi, ""). // removes leading and trailing spaces
            replace(/[ ]{2,}/gi, "").       // replaces multiple spaces with one space 
            replace(/\n +/, "\n");
        val.value = val.value.substr(0, 1).toUpperCase() + val.value.substr(1).toLowerCase();
        $("#" + id + "").html("");
        return val.value;
    }
}

function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}


function ActiveInactiveValidationAsset(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/AssetMaster/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#asset").prop("checked", true);
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


