


function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {

        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;
        $('#moduleAll').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}


function getList() {

    var Month = $('#Month_Code').val();
    showSpinner();
    $.ajax({

        type: 'POST',
        url: '/Payroll/ESIExpiryCheck/GetEmployeeList',
        dataType: 'html',
        data: $('#BasicFilter').serialize() + "&Month=" + Month,
        success: function (result) {

            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#listID').html("");
                $('#listID').html(result);
                hideSpinner();
            }
        },


        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);
        }
    });

}






function OnConfirmClick() {

    var monthCode = $("#Month_Code").val();
    debugger
    if (validateDropdowns()) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {

            var table = $('#moduleAllCheckBox').DataTable();
            var data = table.$('input,select,textarea').serializeArray();
            data.push({ name: 'SalaryPeriod', value: monthCode });
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Payroll/ESIExpiryCheck/RemoveApplicabilty',
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            debugger
                            swal.fire("Applicability Removed Successfully").then(function () {

                                getList();
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
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


function validateDropdowns() {
    var i = 0;
    if (!validateById('#Month_Code', 'month'))
        i++;

    if (i == 0)
        return true;
    else
        return false;
}





function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            getList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            getList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        getList();


    }
}





function ExportToexcel() {
    debugger;
    var TableLength = $('#ESIExpiryCheckList_Count').val();
    var Month = $("#Month_Code").val();
    var MName = $("#Month_Code option:selected").text();
    var filterdata = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filterdata;
    debugger;
    if (TableLength > 0) {
        url = "/Payroll/ESIExpiryCheck/ExportToExcel?" + PARAM + "&Month=" + Month+ "&MName=" + MName + "&totRecords=" + TableLength ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


