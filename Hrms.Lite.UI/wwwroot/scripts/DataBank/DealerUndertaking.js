var prevTab = "General";
var currentTab = "General";

function OnTabClick(TabIndex, Tab) {


    currentTab = TabIndex;


    $(".btn-title").removeClass("active");
    $("." + Tab).addClass("active");

    $.ajax({

        type: 'GET',
        url: '/DataBank/DealerUndertaking/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#DealerUndertakingList').html("");
                $('#DealerUndertakingList').html(result);
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

function GetDealerUndertaking(EmployeeGI, TabIndex) {
    debugger;
    var url = "/DataBank/DealerUndertaking/GetDealerUndertaking?EmployeeGI=" + EmployeeGI + "&TabIndex=" + TabIndex;
    window.location.href = url;
}

function GetDealerUndertakingDetails(EmployeeGI, RequestCode, TabIndex) {


    debugger
    $.ajax({

        type: 'GET',
        url: '/DataBank/DealerUndertaking/GetDealerUndertakingDetails',
        dataType: 'html',
        data: { EmployeeGI: EmployeeGI, RequestCode: RequestCode, TabIndex: TabIndex },
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

function SaveDealerUndertaking(RequestCode,ActiveStatus) {
    debugger;
    data = new FormData($('#DealerUndertaking_Entry')[0]);
    data.append("RequestCode", RequestCode);
    data.append("ActiveStatus", ActiveStatus);

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
                url: '/DataBank/DealerUndertaking/SaveDealerUndertakingDetails',
                contentType: false,
                processData: false,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            window.location.href = "/DataBank/DealerUndertaking/Index";
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
