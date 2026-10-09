var prevTab = 'PendingTab';
var currentTab = 0;

function OntabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    GetList();
    currentTab = TabIndex;
    prevTab = Tab;
}
function GetList() {
    showSpinner();
    var monthCode = $('#PayrollMonth_Code').val();
    $.ajax({
        type: 'GET',
        url: '/PayRoll/SalaryReleaseWorkFlow/GetList?' + $('#BasicFilter').serialize(),
        dataType: 'html',
        data: { Month: monthCode, Tab: currentTab },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#salaryReleaseList').html("");
                $('#salaryReleaseList').html(result);
                $('.PendingTabCount').html($('#PendingCount').val());
                $('.ConfirmedTabCount').html($('#ConfirmedCount').val());
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

function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {


        if (filterApplicable >= 1) {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetList();

    }
}
function onSaveClick() {
    debugger;
    var i = 0;
    if (!validateById('#PayrollMonth_Code', 'payrollM')) {
        i++;
        return false;
    }        
    if (i==0) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: 'POST',
                    url: '/PayRoll/SalaryReleaseWorkFlow/Save?' + $('#BasicFilter').serialize(),
                    dataType: 'json',
                    data: { Month: $('#PayrollMonth_Code').val() },
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                GetList();
                            });
                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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
            else {
                return false;
            }
        })

    }
    else {
        return false;
    }
}

function ExportToExcel() {
    var Month = $('#PayrollMonth_Code').val();
    var MonthName = $('#PayrollMonth_Code option:selected').text();
    var TableLength = $('#SalaryReleaseWorkFlowList_Count').val();
    var TabName;
    if (currentTab == 0)
        TabName = 'Pending';
    else
        TabName = 'Confirmed';
    if (TableLength > 0) {
        url = "/PayRoll/SalaryReleaseWorkFlow/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + TabName + "&Month=" + Month + "&MonthName=" + MonthName + "&totRecords=" + TableLength + "&data=" + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}