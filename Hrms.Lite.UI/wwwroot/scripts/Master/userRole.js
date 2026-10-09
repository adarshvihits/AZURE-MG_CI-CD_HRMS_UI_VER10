var searchClick = 0;
var currentTab;
var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';
function TabChange(res) {
    if (res == 'A') {
        $('#UserRoleList').removeClass('hide');
        $('#InactiveList').addClass('hide');
        $('#PendingList').addClass('hide');
    }
    else if (res == 'I') {
        $('#UserRoleList').addClass('hide');
        $('#InactiveList').removeClass('hide');
        $('#PendingList').addClass('hide');
         }
    else {
        $('#UserRoleList').addClass('hide');
        $('#InactiveList').addClass('hide');
        $('#PendingList').removeClass('hide');
         }
}
function onBackArrowClck(view) {
    debugger;
    if (view == "SubModule") {
        $('#Module').removeClass('hide');
        $('#SubModule').addClass('hide');
        $('#Menu').addClass('hide');
    }
    else {
        $('#SubModule').removeClass('hide');
        $('#Module').addClass('hide');
        $('#Menu').addClass('hide');
    }
}

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {
        
            debugger;           
            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length-1)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);              
    }
    else {
        debugger;
        $('#FullPermission').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}


function onNextClick(TabIndex,count,tableId) {
    debugger;
    if (TabIndex == 'Module') {
        if (onCheckBoxClick(tableId)) {
            var Code = [];
            var Appl = [];
            var Name = [];
            for (i = 0; i < count; i++) {
                if ($('#UserVsModule_' + i + '__Applicable').is(':checked')) {
                    Appl.push($('#UserVsModule_' + i + '__Applicable').is(':checked'));
                    Code.push($('#UserVsModule_' + i + '__Code').val());
                    Name.push($('#UserVsModule_' + i + '__Name').val());
                }

            }
            var url = "/Master/UserRole/ModuleChange";
            /* window.location.href = url + "?Code=" + Code + "&Appl=" + Appl + "&Name=" + Name;*/
            $("#SubModule").load(url, { Code: Code, Appl: Appl, Name: Name, GI: GI, LogGI: LogGI, Editmode: Editmode }, function () {
            });
            console.log(Code);
            console.log(Appl);
            console.log(Name);
            $('#SubModule').removeClass('hide');
            $('#Module').addClass('hide');
            $('#Menu').addClass('hide');
        }
        else {
            swal.fire("Please Select atleast One Module");
        }
    }
    else if (TabIndex == 'SubModule') {
        if (onCheckBoxClick(tableId)) {
            var MCode = [];
            for (i = 0; i < count; i++) {
                if ($('#UserVsSubModule_' + i + '__Applicable').is(':checked')) {
                    MCode.push($('#UserVsSubModule_' + i + '__Code').val());
                }

            }
            //console.log(Code);
            //console.log(Code.length);
            var url = "/Master/UserRole/SubModuleChange";
            /* window.location.href = url + "?Code=" + Code + "&Appl=" + Appl + "&Name=" + Name;*/
            $("#Menu").load(url, { Code: MCode, GI: GI, LogGI: LogGI, Editmode: Editmode }, function () {
            });
            console.log(Code);
            $('#SubModule').addClass('hide');
            $('#Module').addClass('hide');
            $('#Menu').removeClass('hide');
        }
        else {
            swal.fire("Please Select atleast One Sub Module");
        }
    }
    
    else  //job-details
    {
        $('#SubModule').addClass('hide');
        $('#Module').removeClass('hide');
        $('#Menu').addClass('hide');
    }
}


function moduleChange(i) {
    debugger;
    Appl.push($('#z' + i + '__Applicable').is(':checked'));
    Code.push($('#z' + i + '__Code').val());
    Name.push($('#z' + i + '__Name').val());
    //alert(Code);
    //alert(Appl);
    //alert(Name);
    var url = "/Master/UserRole/ModuleChange";
   /* window.location.href = url + "?Code=" + Code + "&Appl=" + Appl + "&Name=" + Name;*/
    $("#submodulen").load(url, { Code: Code,Appl:Appl,Name:Name }, function () {
    });
}
function submoduleChange(i) {
    debugger;
    MAppl.push($('#UserVsSubModule_' + i + '__Applicable').is(':checked'));
    MCode.push($('#UserVsSubModule_' + i + '__Code').val());
    MName.push($('#UserVsSubModule_' + i + '__Name').val());
    alert(MCode);
    alert(MAppl);
    alert(MName);
    var url = "/Master/UserRole/SubModuleChange";
    /* window.location.href = url + "?Code=" + MCode + "&Appl=" + MAppl + "&Name=" + MName;*/
    $("#menu").load(url, { Code: MCode, Appl: MAppl, Name: MName,CCode:Code,AAppl:Appl,NName:Name }, function () {
    });
}
function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var url = "/Master/UserRole/GetUserRoleList";
  /*  $('#tabsDropdownTraining').val(currentTab);*/
    $('#UserRoleList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    prevTab = Tab;
}
//function OnTabClick(tab,mode) {
//    debugger
//    showSpinner();
//    currentTab = mode;
//    if (mode == 'ACTIVE') {
//        var url = "/Master/UserRole/GetUserRoleList?TabIndex=" + mode;
//        $('#UserRoleList').load(url, function () { hideSpinner() });
//        debugger;
//        $(".PendingTab").removeClass("active");
//        $(".InactiveTab").removeClass("active");
//        $(".ActiveTab").addClass("active");
//        $("#UserRoleList").removeClass("hide");
//        $("#pendingList").addClass("hide");
//        $("#InactiveList").addClass("hide");

//    }
//    else if (mode == 'APPROVAL_PENDING') {
//        var url = "/Master/UserRole/GetUserRoleList?TabIndex=" + mode;

//        $('#pendingList').load(url, function () { hideSpinner() });
//        debugger;
//        $("#UserRoleList").addClass("hide");

//        $(".PendingTab").addClass("active");
//        $(".ActiveTab").removeClass("active");
//        $(".InactiveTab").removeClass("active");
//        $("#pendingList").removeClass("hide");
//        $("#InactiveList").addClass("hide");
//    }
//    else {
//        var url = "/Master/UserRole/GetUserRoleList?TabIndex=" + mode;
//        $('#InactiveList').load(url, function () { hideSpinner() });
//        debugger;
//        $("#pendingList").addClass("hide");
//        $("#InactiveList").removeClass("hide");
//        $("#UserRoleList").addClass("hide");
//        $(".PendingTab").removeClass("active");
//        $(".ActiveTab").removeClass("active");
//        $(".InactiveTab").addClass("active");
//    }
//    $('#tabsDropdownUserRole').val(currentTab);

//}


function tabClick(val) {
    debugger;
    showSpinner()
    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
    var url = "/Master/UserRole/GetUserRoleList";
    $('#UserRoleList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

}



function Edit(GI, LogGI,index) {
    debugger;
    if ($("#UserRoleList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);
    else {
    var url = "/Master/UserRole/Edit?UserRoleGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
        }
}

function Delete(GI, LogGI, index) {
    if ($("#UserRoleList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);
    else {
        debugger;
        var url = "/Master/UserRole/Delete?UserRoleGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
}
function Approve(GI, LogGI,index) {
   /* alert($("#UserRoleList_" + index + "__MakerChecker").val());*/
    debugger;
    if ($("#UserRoleList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);
    else {
        var url = "/Master/UserRole/Approve?UserRoleGI=" + GI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
}

function deleteClick(GI) {
    debugger;
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
        url: '/Master/UserRole/UserRoleDelete',
        dataType: 'json',
        data: { UserRoleGI: GI },
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    var url = "/Master/UserRole/Index";
                    window.location.href = url
                });

            }
            else {
                swal.fire(result.message);

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

function onExportToExcelClick() {
    debugger;
    if (currentTab == 'ACTIVE') {
        var ActiveUserRoleTableLength = $('#UserRoleList_Count').val();
        if (ActiveUserRoleTableLength > 0) {
            url = "/Master/UserRole/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

    else if (currentTab == 'INACTIVE') {
        var InactiveUserRoleTableLength = $('#UserRoleList_Count').val();
        if (InactiveUserRoleTableLength > 0) {
            url = "/Master/UserRole/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

    else {
        var InactiveUserRoleTableLength = $('#UserRoleList_Count').val();
        if (InactiveUserRoleTableLength > 0) {
            url = "/Master/UserRole/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

}






function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}


function onCheckBoxClick(tableid) {
    if ($('#' + tableid).find('input[type=checkbox]').is(':checked')) {
        return true;
    }
    else {
        return false;
    }
}

function validateUserRole(tableId) {
    var i = 0;
    if (!onCheckBoxClick(tableId)) {
        swal.fire("Please Select atleast One Menu");
        i++;
    }
    if (!validateById('#userrole', 'userroleval')) {
        i++;
    }
    if (!validateById('#ShortName', 'shortnameval')) {
        i++;
    }
    if (!validateById('#SalaryVisibility_Code', 'salaryvisiblty')) {
        i++;
    }
    if (!validateById('#Remarks', 'remarkss')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        return true;
    }
}

function ApproveReject(mode) {
    if (mode == "APPROVE")
        modeName = "Approve";
    else
        modeName = "Reject";
    swal.fire({
        text: "Do you want to " + modeName + " ?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
    //    // Save it

    $.ajax({
        type: 'POST',
        url: '/Master/UserRole/ApproveSubmit',
        dataType: 'json',
        data: $('#userRoleApprove').serialize() + "&Mode=" + mode,
        success: function (result) {
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    var url = "/Master/UserRole/Index";
                    window.location.href = url

                })
            }
            else {
                if (result.message == MakerCheckerMsg) {
                    swal.fire(result.message);
                }
                else {
                    swal.fire("Error");
                }
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

function userRoleSave(EditMode) {


    if (validateUserRole('MenuAllCheckBox')) {
    if (EditMode == 'SAVE')
        mode = 'Save';
    else
        mode = 'Modify';
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
                url: '/Master/UserRole/Create',
                dataType: 'json',
                data: $('#createUserRole').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('AddNewOrEdit');
                            var url = "/Master/UserRole/Index";
                            window.location.href = url;

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
}




function ActiveInactiveValidationUser(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/UserRole/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#user").prop("checked", true);
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


function AddTabSettings(UserRoleGI) {
    $.ajax({

        type: 'GET',
        url: '/Master/UserRole/GetUserVsTab',
        dataType: 'html',
        data: { UserRoleGI: UserRoleGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m = $('.tab-settings-popup');
                m.find('.modal-body').html("");
                m.find('.modal-body').html(result);
                m.modal('show');
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

function UserVSTabSave() {
   
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
                    url: '/Master/UserRole/UserVSTabSave',
                    dataType: 'json',
                    data: $('#UserVsTab').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var m = $('.tab-settings-popup');
                                m.find('.modal-body').html("");                                
                                m.modal('hide');

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




function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("MenuAllCheckBox");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[2];
        //if (noOfRows > 1) {
        //    $('#noDataImg').remove();
        //}
        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");
            //console.log("rs = " + rs);
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }
    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}




function onSearchClick() {
    if (searchClick == 0) {
        searchClick = 1;
        $('#searchList').addClass('field-width-big');
        CursorFocusOnSearch();
    }
    else {
        searchClick = 0;
        $('#searchList').removeClass('field-width-big');
    }
}