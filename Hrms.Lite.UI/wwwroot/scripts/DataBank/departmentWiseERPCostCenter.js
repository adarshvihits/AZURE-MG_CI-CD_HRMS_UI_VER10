


//$('#hideCost_' + i).val($('#DepartmentWiseERPCostCenterList_' + i + '__CostDropdown_Code option:selected').text());


function onUpdateClick(i, DepartmentCode) {
    debugger;
    var UpdatedValue = $('#DepartmentWiseERPCostCenterList_' + i + '__CostDropdown_Code').val();
    var GroupCompanyCode = $('#GroupCompany_Code').val();
    if (UpdatedValue == "" || UpdatedValue == null) {
        swal.fire("Value should not be null");
        return false;
    }
    swal.fire({
        text: "Do you want to Update?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {


            $.ajax({
                type: 'POST',
                url: '/DataBank/DepartmentWiseERPCostCenter/Save',
                dataType: 'json',
                data: { UpdatedValue: UpdatedValue, DepartmentCode: DepartmentCode, GroupCompanyCode: GroupCompanyCode },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            GetList(GroupCompanyCode);

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




function GetList(selectedValue) {
    $.ajax({
        type: 'GET',
        url: '/DataBank/DepartmentWiseERPCostCenter/GetList',
        data: { groupCompanyCode: selectedValue },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {

                $('#DepartmentWiseERPCostCenterList').html("");
                $('#DepartmentWiseERPCostCenterList').html(result);
            }

            // You can handle the response here if needed
            // For now, do nothing or show a message
            // swal.fire("Request sent successfully!");
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire("An error occurred while contacting the server.");
        }
    });
}






function onEditClk(i) {
    debugger;
    $('#edit_' + i).addClass('hide');
    $('#hideCost_' + i).addClass('hide');
    $('#dropDownTxtbox_' + i).removeClass('hide');
    $('#updatecancel_' + i).removeClass('hide');
}



function onCancelClick(i) {
    debugger

    $('#edit_' + i).removeClass('hide');
    $('#updatecancel_' + i).addClass('hide');
    $('#hideCost_' + i).removeClass('hide');
    $('#dropDownTxtbox_' + i).addClass('hide');
    $('#newRemark_' + i).val(0);
    $('#DepartmentWiseERPCostCenterList_'+i+'__CostDropdown_Code').val('')


}



function ExportToExcel() {
    debugger;
    var tableLength = $('#DepartmentWiseERPCostCenterList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/DepartmentWiseERPCostCenter/ExportToExcel?totRecords=" + tableLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}
