var prevTab = 'Tab1';
var currentTab = 'Pending';
function OnTabClick(Tab, TabIndex) {
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    GetList();
    prevTab = Tab;
}

function GetList() {
    debugger;
    var FinYearCode = $('#FinancialYear_Code').val();
    showSpinner();
    $.ajax({
        type: 'GET',
        url: '/PayRoll/VPFApproval/GetList',
        dataType: 'html',
        data: { Tab: currentTab, FinancialYear: FinYearCode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ApprovalList').html("");
                $('#ApprovalList').html(result);
            }
            hideSpinner();
        },

        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}
function onApproveClick() {
    debugger
    var i = 0;
    if (!validateById("#FinancialYear_Code", 'FyFill'))
        i++;
    if (i == 0) {
        $.ajax({
            type: 'POST',
            url: '/PayRoll/VPFApproval/Approve',
            dataType: 'json',
            data: $('#Approve_vpf').serialize(),
            success: function (result) {
                debugger;
                if (result.success == true) {
                    swal.fire(result.message).then(function () {
                        OnTabClick('Tab1', 'Pending');
                    });
                } else {
                    swal.fire(result.message);
                }
                //if (result.success == true) {

                //    swal.fire(result.message).then(function () {
                //        OnTabClick('Tab1', 'Pending');
                //        //GetList();
                //        //    window.location.href = '/PayRoll/VPFApproval/Index?P_Month='
                //        //        + $("#ProcessMonth_Code").val() + '&F_Month=' + $("#PeriodFrom_Code").val() + '&T_Month=' + $("#PeriodTo_Code").val() +
                //        //        '&B_Group=' + $("#BonusGroup_Code").val();
                //    });
                //}
            },

            error: function (xhr, textStatus, errorThrown) {
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
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
function onSelectAllClick() {
    if ($('#SelectAll').is(':checked')) {
        $('#pendingTable').find('input[type=checkbox]').prop('checked', true);

    }
    else {

        $('#pendingTable').find('input[type=checkbox]').prop('checked', false);

    }
}
function ExportToExcel() {
    debugger;    
    var FinYearCode = $('#FinancialYear_Code').val();
    var tableLength = $('#VPFApprovalList_Count').val();
    if (tableLength > 0) {
        url = '/PayRoll/VPFApproval/ExportToExcel?Tab=' + currentTab + '&FinancialYear=' + FinYearCode;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
    }
}
function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";
    var tab = currentTab;//$("#Tabindex").val();
    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    if (tab == 'Pending')
        table = document.getElementById("pendingTable");
    else
        table = document.getElementById("approveListTable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
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