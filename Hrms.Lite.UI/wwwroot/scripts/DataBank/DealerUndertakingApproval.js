function GetDealerUndertakingApprovalList(EmployeeGI) {
    debugger;
    var url = "/DataBank/DealerUndertakingApproval/GetDealerUndertakingApproval?EmployeeGI=" + EmployeeGI;
    window.location.href = url;
}



function GetDealerUndertakingApprovalList(EmployeeGI, makerChecker) {

    debugger;
    if (makerChecker == 'true')
        swal.fire("MAKER AND CHECKER SHOULD BE DIFFERENT");
    else {
        var url = "/DataBank/DealerUndertakingApproval/GetDealerUndertakingApproval?EmployeeGI=" + EmployeeGI;
        window.location.href = url;
    }

}





function GetDealerUndertakingApprovalDetails(EmployeeGI, RequestCode, makerChecker) {


    debugger;
    if (makerChecker == 'True')
        swal.fire("MAKER AND CHECKER SHOULD BE DIFFERENT");
    else {
    $.ajax({

        type: 'GET',
        url: '/DataBank/DealerUndertakingApproval/GetDealerUndertakingApprovalDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, RequestCode: RequestCode},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {

                var m = $('.newApplication');
                //m.find('.modal-body').html('');
                m.find('#dynamic-certification-content').html(result);
                //m.find('.modal-body').html(result);
                m.modal('show');
                //var m = $('.newApplication');
                //m.find('.modal-body').html('');
                //m.find('.modal-body').html(result);
                //m.modal('show');
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

}

function ApproveDealerUndertaking(RequestCode, ApprovalStatus) {
    debugger;
    data = new FormData($('#DealerUndertaking_Approve')[0]);

    data.append("RequestCode", RequestCode);
    data.append("ApprovalStatus", ApprovalStatus);

    swal.fire({
        text: "Do you want to Apply?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;

            $.ajax({
                type: 'POST',
                url: '/DataBank/DealerUndertakingApproval/ApproveDealerUndertakingDetails',
                contentType: false,
                processData: false,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            window.location.href = "/DataBank/DealerUndertakingApproval/Index";
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





