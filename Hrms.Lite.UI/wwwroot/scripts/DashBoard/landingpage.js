function HighClick(Type) {
    GetDatabankNotifications(Type);
    GetTimeOfficeNotifications(Type);
    GetPayrollNotifications(Type);    
}

function GetDatabankNotifications(type) {


    $.ajax({

        type: 'Get',
        url: '/DashBoard/LandingPage/GetDatabankNotifications',
        dataType: 'html',
        data: { type: type },
        success: function (result) {
            debugger;
            $('.databank-dashboard-scroll').html('');
            $('.databank-dashboard-scroll').html(result);

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

function GetTimeOfficeNotifications(type) {


    $.ajax({

        type: 'Get',
        url: '/DashBoard/LandingPage/GetTimeOfficeNotifications',
        dataType: 'html',
        data: { type: type },
        success: function (result) {
            debugger;
            $('.timeoffice-dashboard-scroll').html('');
            $('.timeoffice-dashboard-scroll').html(result);

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

function GetPayrollNotifications(type) {
    
    

    $.ajax({

        type: 'Get',
        url: '/DashBoard/LandingPage/GetPayrollNotifications',
        dataType: 'html',
        data: { type: type },
        success: function (result) {
            debugger;
            $('.payroll-dashboard-scroll').html('');
            $('.payroll-dashboard-scroll').html(result);

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



function GetPayrollApplicableCount() {


    $.ajax({

        type: 'Get',
        url: '/DashBoard/LandingPage/GetPayrollApplicableCount',
        dataType: 'html',
        /*data: { type: type },*/
        success: function (result) {
            debugger;
            $('.payroll-applicabilityCount').html('');
            $('.payroll-applicabilityCount').html(result);

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

function GetApplicableCountDetails(Type, SubType) {

    $.ajax({
        type: 'GET',
        async: false,
        url: '/DashBoard/LandingPage/GetApplicableCountDetails',
        dataType: 'html',
        data: { Type: Type, SubType: SubType },
        success: function (result) {
            var p = $('.tile_popup');
            p.find('.modal-body').html('');
            var m = $('.payDash_SAlVariance');
            m.find('.modal-body').html('');
            var d = $('.notif_popup');
            d.find('.modal-body').html('');

            var mod = $('.applCountPopUp');
            mod.find('.modal-body').html('');
            mod.find('.modal-body').html(result);
            mod.modal('show');
            if (SubType == "YES")
                var head = Type + " Applicable"
            else if (SubType == "VARIANCE")
                var head = Type + " VARIANCE"
            else
                var head = Type + " Not Applicable"
            $('#applCountPopUpHead').html(head)
            $('.pfOREsi').html(Type);
            if (Type == 'Holiday Wage' || Type == 'LWF')
                $('.grpName').addClass('hide');
            else
                $('.grpName').removeClass('hide');

            $('#pfOREsiGrp').val(Type);
            $('#pfOREsiType').val(Type);
            $('#pfOREsiSubType').val(SubType);
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}


function ExportToExcelApplicabilityCount() {
    debugger;
    type = $('#pfOREsiType').val();
    subtype = $('#pfOREsiSubType').val();
    if (subtype == "YES" || subtype == "NO") {

        var TableLength = $('#ApplicableList_Count').val();
    }
    else {

        var TableLength = $('#ESI_PF_PT_Variance_Applicable_Count').val();
    }
    if (TableLength > 0) {
        url = '/DashBoard/LandingPage/ExportToExcelApplicabilityCount?Type=' + encodeURIComponent(type) + "&SubType=" + subtype
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

var popupclss;
var prevpopupclass = "empty";
function onLabelClick(type, popupclass) {
    debugger
    showSpinner();
    //$("." + prevpopupclass).html("");
    var url = '/DashBoard/PayRollDashBoard/GetDashBoardList?Type=' + encodeURIComponent(type);
    debugger
    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var mod = $('.applCountPopUp');
                mod.find('.modal-body').html('');
                var p = $('.tile_popup');
                p.find('.modal-body').html('');
                var d = $('.notif_popup');
                d.find('.modal-body').html('');

                if (type == "SalaryVariance") {
                    var m = $('.payDash_SAlVariance');
                    popupclss = '.payDash_SAlVariance';
                }

                
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                hideSpinner();
                m.modal('show');
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

}

function onTileClick(TileName) {
    debugger;

    $.ajax({
        type: 'GET',
        url: '/DashBoard/LandingPage/GetPopupDetails?TileName=' + TileName,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var p = $('.payDash_SAlVariance');
                p.find('.modal-body').html('');
                var mod = $('.applCountPopUp');
                mod.find('.modal-body').html('');
                var d = $('.notif_popup');
                d.find('.modal-body').html('');

                var m = $('.tile_popup');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                hideSpinner();
                m.modal('show');
                if (TileName =='Logins')
                    $('#tile_popup').html('Logins');
                else if (TileName == 'Transfers')
                    $('#tile_popup').html('Transfers');
                else if (TileName == 'Promotions')
                    $('#tile_popup').html('Promotions');
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}

function GetNotificationPopUp(Code, head) {
    $.ajax({

        type: 'GET',
        url: '/DashBoard/LandingPage/GetTimeOfficeNotificationsPopUp',
        dataType: 'html',
        data: { NotificationCode: Code },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var p = $('.payDash_SAlVariance');
                p.find('.modal-body').html('');
                var mod = $('.applCountPopUp');
                mod.find('.modal-body').html('');
                var d = $('.tile_popup');
                d.find('.modal-body').html('');

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

function GetPayRollNotificationPopUp(Code, head) {
    debugger;
    $.ajax({

        type: 'GET',
        url: "/DashBoard/LandingPage/GetPayRollNotificationsPopUp?Type=" + head,
        dataType: 'html',
        data: { NotificationCode: Code },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var p = $('.payDash_SAlVariance');
                p.find('.modal-body').html('');
                var mod = $('.applCountPopUp');
                mod.find('.modal-body').html('');
                var d = $('.tile_popup');
                d.find('.modal-body').html('');

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


function OnPFMissingClick(type,action) {
    debugger
    if (type === "PF group missing") {
        $.ajax({
            type: 'GET',
            url: "/DashBoard/PayRollDashBoard/GetEmployeeDetailsPopUp?Type=" + type,
            dataType: 'html',

            success: function (Data) {
                debugger;
                //var m1 = $(popupclss);
                //m1.find('.modal-body').html('');


                var mdl = $('.PF_missing_popup');
                mdl.find('.modal-body').html('');
                mdl.find('.modal-body').html(Data);
                debugger;
                $('#PF_missing_popupHead').html(type);
                mdl.modal('show');



            },
            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);

            }
        });
    } else {
        window.location.href = action; // Redirect to the action URL
    }
    

}

function GetDataBankNotificationPopUp(Code, head) {
    debugger;
    $.ajax({

        type: 'GET',
        url: "/DashBoard/LandingPage/GetDataBankNotificationsPopUp?Type=" + head,
        dataType: 'html',
        data: { NotificationCode: Code },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var p = $('.payDash_SAlVariance');
                p.find('.modal-body').html('');
                var mod = $('.applCountPopUp');
                mod.find('.modal-body').html('');
                var d = $('.tile_popup');
                d.find('.modal-body').html('');

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
function onDateChange() {
    debugger
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var i = 0;
    if (!onDateClick('#fromDate', 'fromDte')) {
        i++;
    }
    if (!onDateClick('#toDate', 'toDte')) {
        i++;
    }
 
    if (i == 0) {
        $.ajax({
            type: 'Get',
            url: '/DashBoard/LandingPage/GetLoginDetails?fromDate=' + fromDate + '&toDate=' + toDate,
            dataType: 'html',
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    $('#loginlist').empty();
                    $('.customGraph').empty();
                    var tempDiv = $('<div>').html(result);
                    $('#loginlist').html(tempDiv.find('#loginlist'));
                    $('.customGraph').html(tempDiv.find('.customGraph'));
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

function onDateClick() {
    debugger;
    var fromDate = $('#fromDate').val();
    var toDate = $('#toDate').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        return false;
    }
    else
        return true;
}

function validateDates() {
    debugger;
    var i = 0;
    if (!validateById('#FromDate', 'fromDate')) {
        i++;
    }
    if (!validateById('#ToDate', 'toDate')) {
        i++;
    }
  

    if (i == 0)
        return true;
    else
        return false;
}
