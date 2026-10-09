var prevTab = 'all';
var prevpopupclass = "empty";

function viewListCount(type, popupclass, listCountTitle) {
    showSpinner();
    //$("." + prevpopupclass).html("");
    //if (type == "MISSING" || type == "DOCUMENTS MISSING") {
    debugger;
        $.ajax({
            type: 'GET',
            url: '/DashBoard/DataBankDashBoard/GetDashBoardList?Type=' + encodeURIComponent(type),
            dataType: 'html',

            success: function (Data) {
                debugger
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    var m = $('.listCount');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    $('.listCountTitle').html(listCountTitle)
                    hideSpinner();
                    m.modal('show');
                }
                //prevpopupclass = popupclass;
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger;
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    //}
}

function onLabelClick(type, popupclass,subType)
{  
    //showSpinner();
    $("." + prevpopupclass).html("");
    if (type == "HR APPROVAL PENDING" || type == "APPROVAL TRACKER" || type == "MASTER CHANGES" ) {
        debugger
        $.ajax({
            type: 'GET',
            url: '/DashBoard/DataBankDashBoard/GetDashBoardList?Type=' + encodeURIComponent(type),
            dataType: 'html',

            success: function (Data) {
                debugger
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if (type == "APPROVAL TRACKER") {
                        var m = $('.Approval_Tracker');

                    }
                    if (type == "HR APPROVAL PENDING") {
                        var m = $('.HR_approve');

                    }
                    if (type == "MASTER CHANGES") {
                        var m = $('.recent_master');
                    }
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    hideSpinner();
                    m.modal('show');
                    prevpopupclass = popupclass;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }

    if (type == "NOTICE PERIOD") {
        //showSpinner();
        debugger
        $.ajax({
            type: 'GET',
            url: '/DashBoard/DataBankDashBoard/GetDashBoardDetailsList?Type=' + encodeURIComponent(type),
            dataType: 'html',

            success: function (Data) {
                debugger
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if (type == "NOTICE PERIOD") {

                        var m = $('.notice_period_emp');
                    }



                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    hideSpinner();
                    m.modal('show');
                    prevpopupclass = popupclass;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }

    if (type == "MISSING" || type == "OVERDUE RENEWALS" || type == "UPCOMING RENEWALS" || type == "DOCUMENTS MISSING") {
        showSpinner();
        debugger
        $.ajax({
            type: 'GET',
            url: '/DashBoard/DataBankDashBoard/GetDashBoardDetailsList?Type=' + encodeURIComponent(type) + '&SubType=' + subType,
            dataType: 'html',

            success: function (Data) {
                debugger
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    if (type == "MISSING" || type == "DOCUMENTS MISSING") {
                        var m = $('.listCount');
                        m.modal('hide');
                        var m = $('.dashInfoMissing');
                    }
                    if (type == "UPCOMING RENEWALS" || type == "OVERDUE RENEWALS") {
                        debugger
                        var m = $('.listCount');
                        m.modal('hide');
                        var m = $('.dash_renewel');
                    }

                    debugger;
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    $('#missingTitle').html(subType);
                    $('#missingRenewalsTitle').html(subType);
                    hideSpinner();
                    m.modal('show');
                    prevpopupclass = popupclass;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        });
    }
   
}

function onpreEnrollClick()
{
    debugger
    $.ajax({
        type: 'GET',
        url: '/DashBoard/DataBankDashBoard/PreEnrollApprovalList',
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').html(Data);
                $(".tab1").addClass('active');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}
function onManpowerToggle()
{
    debugger
  
    if ($("#ManPower").is(':checked')) {
        toggle = 'TODAY';
        $(".dash_ManpowerLabel").html('Today Manpower');
    }
    else {
        toggle = 'YESTERDAY';
        $(".dash_ManpowerLabel").html('Yesterday Manpower');
    }
    var url = '/DashBoard/DataBankDashBoard/GetManpowerChartDetails?ToggleActive=' + toggle;
    //$('#ddManPowerChart').load(url, function () { });

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ddManPowerChart').html(Data);               
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

}
function ontabClick(TabIndex,Tab) {
    debugger;  
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var url = '/DashBoard/DataBankDashBoard/GetDashBoardDetailsList?Type=' + TabIndex;
    $('#ddEventsAndcelebrns').load(url, function () { });
    prevTab = Tab;
}
function onPreAndJoiningClick(mode) {
    debugger
    window.location.href = '/DashBoard/DataBankDashBoard/PreEnrollApprovalListAndJoining?mode=' + mode;
}

function onTypechange() {   
    debugger
    var Classsification = $("#Classification").val();
    if (Classsification == 1)
        Classsification = 2;
    else if (Classsification == 2)
        Classsification = 3;
    else
        Classsification = 1;
    var url = '/DashBoard/DataBankDashBoard/GetGeneralChartDetails?Type=Age&Classsification=' + Classsification;
    //$('#ddGeneralChart').load(url, function () { });

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ddGeneralChart').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function onClassificationDropdownChange() {
    var classification = $('#classification').val()
    $.ajax({
        type: 'GET',
        url: '/DashBoard/DataBankDashBoard/GetGraph',
        dataType: 'html',
        data: { Classification: classification},
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.customGraph').removeClass('hide');
                $('.customGraph').html('');
                $('.customGraph').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function GetNotificationPopUp(head) {
    debugger;

    $.ajax({

        type: 'GET',
        url: '/DashBoard/DataBankDashBoard/GetNotificationsPopUp',
        dataType: 'html',
        data: { Type: head },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.notif_popup_head').html(head);
                var m = $('.notif_popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(result);

                m.modal('show');
            }



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}

function ExportToExcel() {

    debugger;

    var tableLength = $('#Notification_PopUp_Count').val();
    if (tableLength > 0) {
        url = "/DashBoard/DataBankDashBoard/ExportToExcel";
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}