function confirmApproveIconClick(PunchRegularizationGI) {
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
                url: '/TimeOffice/PunchingRegularizationApproval/Approve',
                dataType: 'json',
                data: { PunchRegularizationGI: PunchRegularizationGI },
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire("Approved Successfully").then(function () {

                            var url = "/TimeOffice/PunchingRegularizationApproval/GetList";
                            debugger

                            $('#ApprovalList').load(url, function () { });

                            $("#ApprovalList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
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
        else {
            return false;
        }
    })

}



function GetList(EmployeeGI) {
    debugger
    showSpinner();
    var EmployeeGI = $("#EmployeeGI").val()
    $.ajax({

        type: 'GET',
        url: "/TimeOffice/PunchingRegularizationApproval/GetList?=" + EmployeeGI,
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".ESSP_LoginSession").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                $('#ApprovalList').html("");
                $('#ApprovalList').html(result);
                hideSpinner();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

}

function OnRejectClick(PunchRegularizationGI) {
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
                url: '/TimeOffice/PunchingRegularizationApproval/Reject',
                dataType: 'json',
                data: { PunchRegularizationGI: PunchRegularizationGI },
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire("Rejected Successfully").then(function () {

                            var url = "/TimeOffice/PunchingRegularizationApproval/GetList";
                            debugger

                            $('#ApprovalList').load(url, function () { });

                            $("#ApprovalList").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
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
        else {
            return false;
        }
    })

}