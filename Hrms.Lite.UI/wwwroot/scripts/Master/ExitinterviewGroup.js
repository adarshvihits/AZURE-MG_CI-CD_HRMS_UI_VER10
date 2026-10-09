var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var url = "/Master/ExitInterviewGroup/GetInterviewGroupList";
    $('#tabsDropdownCategory').val(currentTab);
    $('#GroupList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });

    prevTab = Tab;
}






function ActiveInactiveValidationExitGp(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/ExitInterviewGroup/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#exitGP").prop("checked", true);
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









function tabClick(val) {
    debugger;
    currentTab = $(val).val();
    showSpinner()
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
    var url = "/Master/Category/GetCategoryList";
    $('#CategoryList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });

}



function onApproveClick(GI, LogGI, Type, index) {

    debugger;
    if ($("#GroupList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {
        var url = "/Master/ExitInterviewGroup/Approve?InterviewGruopGI=" + GI + "&LogGI=" + LogGI;

        $('#ApproveOrRejectSlider').load(url, function () { });
        $('body').append('<div class="form-overlay"></div>');
    }


}




function AddNewOrEdit(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#GroupList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            var url = "/Master/ExitInterviewGroup/Edit?InterviewGruopGI=" + GI + "&LogGI=" + LogGI;

            $('#addNewOrEditSlider').load(url, function () { });
            $('body').append('<div class="form-overlay"></div>');
        }

    }

    else {
        var url = "/Master/ExitInterviewGroup/Create";
        $('#addNewOrEditSlider').load(url, function () { });
        $('body').append('<div class="form-overlay"></div>');
    }


}







function ExitInterviewGroupSave(mode) {
    debugger;
    if (validateExitInterviewGroup()) {

        //console.log($('#Save-category').serialize());

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
                    url: '/Master/ExitInterviewGroup/Save',
                    dataType: 'json',
                    data: $('#Save-Interviewgroup').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddInterviewGroup');
                                var url = "/Master/ExitInterviewGroup/GetInterviewGroupList?TabIndex=" + currentTab;
                                $('#GroupList').load(url, function () { });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        toastr.error(xhr.responseText);
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




function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#GroupList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        var url = "/Master/ExitInterviewGroup/Delete?InterviewGruopGI=" + GI + "&LogGI=" + LogGI;
        $('#DeleteSlider').load(url, function () { });
        $('body').append('<div class="form-overlay"></div>');
    }
}





function validateExitInterviewGroup() {
    var i = 0;
    if (!validateById('#Name', 'InterviewName')) {
        i++;
    }
    if (!validateById('#ShortName', 'GroupShortName')) {
        i++;
    }
  

    if (i == 0)
        return true;
    else
        return false;
}






function confirmReject(GI, LogGI) {

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
                url: '/Master/ExitInterviewGroup/Reject',
                dataType: 'json',
                data: $('#ExitInterview_group_Approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('InterviewApprove');
                            var url = "/Master/ExitInterviewGroup/GetInterviewGroupList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#GroupList').load(url, { TabIndex: currentTab }, function () { });


                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ExitInterviewGroup/Approve?InterviewGruopGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveOrRejectSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    toastr.error(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}





function confirmApprove(GI, LogGI) {
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
                url: '/Master/ExitInterviewGroup/Approve',
                dataType: 'json',
                data: $('#ExitInterview_group_Approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('InterviewApprove');
                            var url = "/Master/ExitInterviewGroup/GetInterviewGroupList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#GroupList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#GroupList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ExitInterviewGroup/Approve?InterviewGruopGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveOrRejectSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    toastr.error(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}






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
                url: '/Master/ExitInterviewGroup/Delete',
                dataType: 'json',
                data: $('#Interview_Group_Delete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('InterviewDelete');
                            var url = "/Master/ExitInterviewGroup/GetInterviewGroupList?TabIndex=" + 'ACTIVE';
                            $('#GroupList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#GroupList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ExitInterviewGroup/Delete?InterviewGruopGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    toastr.error(xhr.responseText);
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
/*        var pendingTableLength = document.getElementById("pendingExitGroupTable").rows.length - 1;*/
        var inActiveTableLength = $('#GroupList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ExitInterviewGroup/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
       /* var activeTableLength = document.getElementById("ExitInterviewGroupTable").rows.length - 1;*/
        var inActiveTableLength = $('#GroupList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ExitInterviewGroup/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        debugger;
     /*   var inActiveTableLength = document.getElementById("ExitInterviewGroupTable").rows.length - 1;*/
        var inActiveTableLength = $('#GroupList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ExitInterviewGroup/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}


function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}