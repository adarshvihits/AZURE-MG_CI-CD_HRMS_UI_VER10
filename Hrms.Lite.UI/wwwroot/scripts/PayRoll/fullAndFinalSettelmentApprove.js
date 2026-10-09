
function validateRemarks() {
    debugger;
    var i = 0;
    if (!validateById('#Details_ApproverComments', 'comments')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}


function confirmApproval() {
    debugger
    if (validateRemarks()) {
        swal.fire({
            text: "Do you want to Approve?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/PayRoll/FullAndFinalSettlementApprove/ConfirmApproval',
                    dataType: 'json',
                    data: $('#SettelmentApprove').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                url = "/PayRoll/FullAndFinalSettlementApprove/Index";
                                window.location.href = url;
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },

                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".ESSP_LoginSession").length > 0) {
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

function ApplyFilter(mode) {
    debugger
    var Month = $('#Month_Code').val();

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: "/PayRoll/FullAndFinalSettlementApprove/ApplyFilter?Month=" + Month,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".ESSP_LoginSession").length > 0) {
                        window.location.reload();
                    }

                    else {
                        CloseSlider('basicFilter');

                        $('#EmployeeList').html("");
                        $('#EmployeeList').html(result);

                        $('#EmployeeList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            $.ajax({

                type: 'POST',
                url: "/PayRoll/FullAndFinalSettlementApprove/ApplyFilter?Month=" + Month,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".ESSP_LoginSession").length > 0) {
                        window.location.reload();
                    }

                    else {
                        CloseSlider('basicFilter');

                        $('#EmployeeList').html("");
                        $('#EmployeeList').html(result);

                        $('#EmployeeList').removeClass('hide');
                    }
                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    swal.fire(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: "/PayRoll/FullAndFinalSettlementApprove/ApplyFilter?Month=" + Month,
            dataType: 'html',
            data: $('#BasicFilter').serialize(),
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".ESSP_LoginSession").length > 0) {
                    window.location.reload();
                }

                else {
                    //CloseSlider('assetAllocationFilter');


                    $('#EmployeeList').html("");
                    $('#EmployeeList').html(result);

                    $('#EmployeeList').removeClass('hide');
                    //$('#rewardsListPartial').addClass('hide');
                }
            },


            error: function (xhr, textStatus, errorThrown) {
                swal.fire(xhr.responseText);
            }
        });
    }
}

function onExportToExcelClick() {
    debugger;
    var Month = $('#Month_Code').val();
    //var TableLength = document.getElementById("FFApproveEmpList").rows.length - 1;
    var TableLength = $('#EmployeeList_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/FullAndFinalSettlementApprove/ExportToExcel?Month=" + Month;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];

function inWords(num) {
    debugger;
    if ((num = num.toString()).length > 9) return 'overflow';
    n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
    if (!n) return; var str = '';
    str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
    str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
    str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
    str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
    str += (n[5] != 0) ? ((str != '') ? 'And ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'Only ' : '';
    return str;
}