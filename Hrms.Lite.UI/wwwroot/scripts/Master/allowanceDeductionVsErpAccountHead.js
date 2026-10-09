
var prevTab = 'tab1';
var currentTab = 'ALLOWANCE';
var flag = 0;
var GrpCmpCode = 0;
// 🔥 NEW GLOBAL VARIABLES (IMPORTANT)
var currentTab = "ALLOWANCE"; // default tab
var prevTab = "tab1";         // default active tab

// 🔥 NEW COMMON FUNCTION (REUSABLE)
function loadList() {
    debugger;
    showSpinner();

    var GrpCmpCode = $('#GroupCompanyCode').val();

    $.ajax({
        type: 'GET',
        url: '/Master/AllowanceDeductionVsErpAccountHead/GetList',
        dataType: 'html',
        data: {
            TabIndex: currentTab,
            GroupCode: GrpCmpCode
        },

        success: function (result) {
            debugger;

            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#allowdedaccountheadList').html(result); // 🔥 SIMPLIFIED
            }

            hideSpinner();
        },

        error: function (xhr, textStatus, errorThrown) {
            debugger;
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}


// 🔥 UPDATED TAB CLICK FUNCTION
function OnTabClick(Tab, TabIndex) {
    debugger;

    showSpinner();

    currentTab = TabIndex; // 🔥 IMPORTANT

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    prevTab = Tab; // 🔥 IMPORTANT

    loadList(); // 🔥 CHANGED (AJAX moved to common function)
}

function onEditClick(i) {
    debugger;
    $('#amtLabel2_' + i).addClass('hide');
    $('#editbt2_' + i).addClass('hide');
    $('#amtTxtbox2_' + i).removeClass('hide');
    $('#updateCancel2_' + i).removeClass('hide');
}




function onCancelClick(i) {
    debugger;
    $('#amtLabel2_' + i).removeClass('hide');
    $('#editbt2_' + i).removeClass('hide');
    $('#amtTxtbox2_' + i).addClass('hide');
    $('#updateCancel2_' + i).addClass('hide');
}

function onUpdateClick(i) {
    debugger;

    var ledger = $('#AccountsHeadTablist_' + i + '__Ledger').val();


    var code = $('#AccountsHeadTablist_' + i + '__Code').val();
    var GrpCmpCode = $('#GroupCompanyCode').val();

    $.ajax({
        type: 'POST',
        url: '/Master/AllowanceDeductionVsErpAccountHead/Save?TabName=' + $('#TabName').val() + '&GroupCode=' + GrpCmpCode,
        dataType: 'json',
        data: {
            code: code,
            ledger: ledger
            
        },
        success: function (result) {
            if (result.success === true) {
                swal.fire("Updated Successfully").then(function () {
                    OnTabClick(prevTab, currentTab);
                });
            } else {
                swal.fire(result.message);
            }
        },
        error: function (xhr) {
            if ($($.parseHTML(xhr)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
            }
        }
    });




}


function onParameterChanged(index) {
    debugger;
    var status = true;
    var ParamCode = 0;
    var table = $('#AllowDedtable').DataTable();
    var data = table.rows().data();
    flag = 0;
    data.each(function (value, _index) {
        debugger
        ParamCode = $("#ClassificationVsERPCodeList_" + index + "__ERPCode").val();
        $('#validationDiv').append(value[2]);
        var ParamCode1 = $('#ClassificationVsERPCodeList_' + (_index) + '__ERPCode').val();
        $('#validationDiv').html('');
        if (ParamCode != '' && ParamCode1 != '') {
            if (index != _index && ParamCode == ParamCode1 && ParamCode1 != '') {
                swal.fire("ERP CODE DUPLICATION...!!");
                flag = 1;
                $("#ClassificationVsERPCodeList_" + index + "__ERPCode").val('')

            }
        }
    });
    debugger;
    if (flag == 1)
        status = false;
    return status;
}

function SaveAllowanceDeductionAccountHead() {


    var table = $('#AllowDedtable').DataTable();
    var data = table.$('input,select,textarea').serializeArray();
    var GrpCmpCode = $('#GroupCompanyCode').val();


    swal.fire({
        text: "Do you want to save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                type: 'POST',
                url: '/Master/AllowanceDeductionVsErpAccountHead/Save?TabName=' + $('#TabName').val() + '&GroupCode=' + GrpCmpCode,
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            OnTabClick(prevTab, currentTab);


                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

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
        else {
            return false;
        }
    })

}

function ExportToExcel() {

    debugger;
    var tableLength = 0;
    var GrpCmpCode = $('#GroupCompanyCode').val();
    if (currentTab == "ERP_CODE")
        tableLength = $('#ClassificationVsERPCodeList_Count').val();
    else

        tableLength = $('#AccountsHeadTablist_Count').val();
    if (tableLength > 0) {

        url = "/Master/AllowanceDeductionVsErpAccountHead/ExportToExcel?TabIndex=" + currentTab + "&ClassificationCode=" + $('#Grouping_Code').val() + "&GroupCode=" + GrpCmpCode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}