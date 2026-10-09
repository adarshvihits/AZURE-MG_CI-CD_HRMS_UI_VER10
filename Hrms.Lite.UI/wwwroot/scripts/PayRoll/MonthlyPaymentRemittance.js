var prevTab = 'PF';
var currentTab = 'PF';


function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownmpr').val(currentTab);
    $.ajax({
        type: 'GET',
        url: '/PayRoll/MonthlyPaymentRemittance/GetPFList',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#pfList').html("");
                $('#pfList').html(result);
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
    prevTab = Tab;
}


function AddNewOrEditClick() {  
    debugger;
    var url = "/PayRoll/MonthlyPaymentRemittance/Create";
    $.ajax({
        type: 'GET',
        url: url,  
        dataType: 'html',
        data: { TabIndex: currentTab },  
        success: function (result) {
            debugger;
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddNewOrEditSlider').html("");
                $('#AddNewOrEditSlider').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}


function pfSave(mode) {
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
                url: "/PayRoll/MonthlyPaymentRemittance/Save?TabIndex=" + currentTab,
                dataType: 'json',
                data: $('#Pfsave').serialize() ,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('AddnewOrEditDiv');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdown'));
                        });
                    } else {
                        swal.fire(result.message).then(function () {
                            
                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    debugger;
                    if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    } else {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                }
            });
        } else {
            return false;
        }
    });
}


function clickOnExportToExcel() {
    debugger;
    //var filter = $('#BasicFilter').serialize();

    var PARAM ='&TabIndex=' + currentTab;
    var TableLength = $('#PFdetailsList_Count').val();
    if (TableLength > 0) {
        var url = '/PayRoll/MonthlyPaymentRemittance/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




