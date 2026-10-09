function OnRequesttypechange() {
    var Type = $('#Requesttyp').val();
    $.ajax({

        type: 'GET',
        url: '/DashBoard/TravelClaimDashBoard/GetRequestList',
        dataType: 'html',
        data: { Type: Type },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#RquestTypeList').html("");
                $('#RquestTypeList').html(result);
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
function OnBudjetTrackingchange() {
    debugger
    var Type = $('#Requesttyp').val();
    $.ajax({

        type: 'GET',
        url: '/DashBoard/TravelClaimDashBoard/GetBudgetTracking',
        dataType: 'html',
        data: { Type: Type },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#BudgetTrackingList').html("");
                $('#BudgetTrackingList').html(result);
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