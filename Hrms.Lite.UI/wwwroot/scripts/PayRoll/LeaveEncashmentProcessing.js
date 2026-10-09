function GetList() {
    debugger;
    var month = $('#Month').val();
    var allowance = $('#Allowance').val();
    var filter = $('#BasicFilter').serialize();
    console.log(filter);
    showSpinner();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/LeaveEncashmentProcessing/GetList',
        dataType: 'html',

        data: $('#BasicFilter').serialize() + "&Month=" + month + "&Allowance=" + allowance,

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Processinglist').html("");
                $('#Processinglist').html(result);
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






function SaveAllList() {
    debugger;
    if (validateAll()) {
        var month = $('#Month').val();
        var allowance = $('#Allowance').val();
        var table = $('#Leave_Encashment_list').DataTable();
        console.log(table);
        var data = table.$('input,select,textarea').serializeArray();
        data.push({ name: 'Allowance123', value: allowance });
        data.push({ name: 'Allowance234', value: allowance });
        debugger
        //console.log($('#rewardsSave').serialize());
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
                    url: '/PayRoll/LeaveEncashmentProcessing/SaveAll',
                    dataType: 'json',
                    data: data ,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                GetList();


                            });
                            debugger;


                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

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
            else {
                return false;
            }
        })

    }
    else {
        return false;
    }
}

function validateAll() {
    var i = 0;
    if (!validateById('#Month', 'MonthFill')) {
        i++;
    }
    if (!validateById('#Allowance', 'AllowanceFill')) {
        i++;
    }



    if (i == 0)
        return true;
    else
        return false;
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
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetList();
    }
}


function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var month = $('#Month').val();
    var allowance = $('#Allowance').val();
    var monthName = $('#Month option:selected').text();
    var allowanceName = $('#Allowance option:selected').text();



    var PARAM = 'data=' + filterdata;
    var TableLength = $('#LeaveEncashmentList_Count').val();
    if (TableLength > 0) {
        url = '/PayRoll/LeaveEncashmentProcessing/ExportToExcel?' + PARAM + "&Month=" + month + "&Allowance=" + allowance + "&MonthName=" + monthName + "&AllowanceName=" + allowanceName + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}