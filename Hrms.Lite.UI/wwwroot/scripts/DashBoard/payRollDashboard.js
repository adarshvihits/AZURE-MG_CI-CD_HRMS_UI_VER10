
var popupclss;
var prevpopupclass = "empty";
function onLabelClick(type, popupclass) {
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

                var m1 = $('.bd-example-modal-lg1');
                m1.find('.modal-body1').html('');
                var m2 = $(popupclss);
                m2.find('.modal-body').html('');


                if (type == "FandFPending") {
                    var m = $('.payDash_FandFPending');
                     popupclss = '.payDash_FandFPending';
                }

                if (type == "OutstandingLoanBalance") {
                    var m = $('.payDash_loanbalance');
                     popupclss = '.payDash_loanbalance';
                }

                if (type == "SalaryVariance") {
                    var m = $('.payDash_SAlVariance');
                     popupclss = '.payDash_SAlVariance';
                }

                if (type == "OtherVarience") {
                    var m = $('.payDash_OtherVariance');
                     popupclss = '.payDash_OtherVariance';
                }

                if (type == "PayrollConfirmations") {
                    var m = $('.PayrollConfirmations');
                     popupclss = '.PayrollConfirmations';
                }


                if (type == "AcNoVariance") {
                    var m = $('.payDash_AccNoVariance');
                     popupclss = '.payDash_AccNoVariance';
                }
                if (type == "TdsOldRegime" || type == "TdsNewRegime" || type == "ActivePayrollEmployees") {
                    var m = $('.tds_payroll_popup');
                    popupclss = '.tds_payroll_popup';
                }
               
                /*            ----------------------------------------------------------*/

                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                hideSpinner();
                m.modal('show');
                //prevpopupclass = popupclass;
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });

}

function ClickOnLabel() {
    var url = "/PayRoll/SalaryHold/Index";
    window.location.href = url;

}


//function EmployeeSearch(TableID) {
//    debugger
//    var input, filter, table, tr, td, i;
//    var img = "<img src='/images/nodata.png' />";

//    input = document.getElementById("searchList");
//    filter = input.value.toUpperCase();
//    table = document.getElementById(TableID);
//    tr = table.getElementsByTagName("tr");
//    tbody = table.getElementsByTagName("tbody");
//    $('#noDataImg').remove();
//    var noOfRows = tr.length;
//    var show = true;
//    var spannedRows = 0;

//    for (i = 0; i < tr.length; i++) {
//        td1 = tr[i].getElementsByTagName("td")[0];
//        td2 = tr[i].getElementsByTagName("td")[1];
//        if (td1 || td2) {
//            txtValue1 = td1.textContent || td1.innerText;
//            txtValue2 = td2.textContent || td2.innerText;
//            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
//                tr[i].style.display = "";
//            } else {
//                tr[i].style.display = "none";
//                noOfRows--;
//            }
//        }
//    }
//    if (noOfRows == 1) {
//        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
//    }

//}









function onPFESIClick(type, subtype ,popupclass,thead)
{
    
    showSpinner();
    $("." + prevpopupclass).html("");   
    var url = '/DashBoard/PayRollDashBoard/GetPFAndESIDetails?Type=' + encodeURIComponent(type) + "&SubType=" + subtype;

    $.ajax({
        type: 'GET',
        url: url,
        dataType: 'html',
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                var m1 = $('.bd-example-modal-lg1');
                m1.find('.modal-body1').html('');
                var m2 = $(popupclss);
                m2.find('.modal-body').html('');





                if (popupclass == "pfYesPopup") {
                    var m = $('.payDash_PFYes');
                    popupclss = '.payDash_PFYes';
                }

                if (popupclass == "pfNoPopup") {
                    var m = $('.payDash_PFNo');
                    popupclss = '.payDash_PFNo';
                }

                if (popupclass == "pfVarPopup") {
                    var m = $('.payDash_PFVar');
                    popupclss = '.payDash_PFVar';
                }

                if (popupclass == "esiNoPopup") {
                    var m = $('.payDash_ESINo');
                    popupclss = '.payDash_ESINo';
                }

                if (popupclass == "esiYesPopup") {
                    var m = $('.payDash_ESIYes');
                    popupclss = '.payDash_ESIYes';
                }

                if (popupclass == "esiVarPopup") {
                    var m = $('.payDash_ESIVar');
                    popupclss = '.payDash_ESIVar';
                }
                debugger
               




                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                $('#pfOREsiGrp').val(thead);
                $('#pfOREsiType').val(type);
                $('#pfOREsiSubType').val(subtype);
                $('.pfOREsi').html(thead);
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



function ExportToExcel() {
    debugger;
    type = $('#pfOREsiType').val();
    subtype = $('#pfOREsiSubType').val();
    if (subtype == "YES" || subtype == "NO") {

        var TableLength = $('#ESI_PFApplicable_Count').val();
    }
    else {

        var TableLength = $('#ESI_PF_Variance_Applicable_Count').val();
    }
    if (TableLength > 0) {
        url = '/DashBoard/PayRollDashBoard/ExportToExcel?Type=' + encodeURIComponent(type) + "&SubType=" + subtype + "&PfOrEsi=" + $('#pfOREsiGrp').val()
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




function OnNotificationCLick(Type) {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DashBoard/PayRollDashBoard/GetEmployeeDetailsPopUp?Type=" + Type,
        dataType: 'html',

        success: function (Data) {
            debugger;
            var m1 = $(popupclss);
            m1.find('.modal-body').html('');
               

                var mdl = $('.bd-example-modal-lg1');
            mdl.find('.modal-body1').html('');
            mdl.find('.modal-body1').html(Data);
            debugger;
            $('#exampleModalLabel_ID').html(Type);
            mdl.modal('show');
            


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}



function ExportToExcelPopUp() {
    debugger;
    var TableLength = $('#PayrollDashBoardList_Count').val();
    if (TableLength > 0) {
        url = "/DashBoard/PayRollDashBoard/ExportToExcel_PopUp?TableLength=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}



function EmployeeSearch(TableID) {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById(TableID);
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td1 || td2) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }
    if (noOfRows == 1) {
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td colspan='8'><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
    }

}


function GetApplicabilityCount() {


    $.ajax({

        type: 'Get',
        url: '/DashBoard/PayRollDashBoard/GetApplicabilityCount',
        dataType: 'html',        
        success: function (result) {
            debugger;
            $('#applCountList').html('');
            $('#applCountList').html(result);            
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
        url: '/DashBoard/PayRollDashBoard/GetApplicableCountDetails',
        dataType: 'html',
        data: { Type: Type, SubType: SubType},
        success: function (result) {
            var m = $('.applCountPopUp');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');
            if (SubType=="YES")
                var head = Type + " Applicable"
            else if (SubType == "VARIANCE")
                var head = Type + " VARIANCE"
            else
                var head = Type+" Not Applicable"
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



function onClassificationDropdownChange() {
    debugger;
    var classification = $('#classification').val()
    $.ajax({
        type: 'GET',
        url: '/DashBoard/PayRollDashBoard/GetGraph',
        dataType: 'html',
        data: { Classification: classification },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.chartView').html('');
                $('.chartView').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
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
        url = '/DashBoard/PayRollDashBoard/ExportToExcelApplicabilityCount?Type=' + encodeURIComponent(type) + "&SubType=" + subtype
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}