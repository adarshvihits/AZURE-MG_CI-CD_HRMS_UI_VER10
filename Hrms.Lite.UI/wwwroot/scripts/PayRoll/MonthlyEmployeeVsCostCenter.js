
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function GetList() {
    debugger;
    showSpinner();
   
    $.ajax({
        type: 'GET',
        url: '/PayRoll/MonthlyEmployeeVsCostCenter/GetList?MonthCode=' + $('#Month').val(),
        dataType: 'html',
        data: $('#BasicFilter').serialize(),       
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
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


function checkBoxChange(val) {
    debugger;
    if ($('#CostCenterList_' + val + '__Applicability').is(':checked')) {
        $('#CostCenterList_' + val + '__Percentage').attr("disabled", false);
    }
    else {
        $('#CostCenterList_' + val + '__Percentage').val(0);
        $('#CostCenterList_' + val + '__Percentage').attr("disabled", true);
    }

}


function BatchUpdateClick() {
   

    var MonthCode = $('#Month').val();
    debugger;
    if (validateDropDowns()) {
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
                    type: 'GET',
                    url: '/PayRoll/MonthlyEmployeeVsCostCenter/BatchUpdate',
                    dataType: 'json',
                    data: { MonthCode: MonthCode},
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
    else {
        return false;
    }
}




function AddNewOrEditClick(RowIndex) {
    {

        var monthCode = $('#Month').val();
        var EmployeeGI = $("#employeeGI_" + RowIndex).val();

            $.ajax({
                type: 'GET',
                url: '/PayRoll/MonthlyEmployeeVsCostCenter/getEmpVsCostCenterEditList',
                dataType: 'html',
                data: { EmployeeGI: EmployeeGI,monthCode:monthCode },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#SliderView').html("");
                        $('#SliderView').html(result);
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


function Save(mode) {


    var table = document.getElementById("CostCenterTable");
    var rows = table.getElementsByTagName("tr");
    var total = 0;
    var j = 0;
    for (var ind = 0; ind < (rows.length); ind++) {
        if ($('#CostCenterList_' + ind + '__Applicability').is(':checked'))
        {
            j++;
            if (document.getElementById('CostCenterList_' + ind + '__Percentage').value == '') {

                $('#CostCenterList_' + ind + '__Percentage').val(0);              
            }
            total = parseFloat(total) + parseFloat(document.getElementById('CostCenterList_' + ind + '__Percentage').value);
        }
    }

 
    if (parseFloat(total) != 100 & j > 0) {
        swal.fire(" Cost centers percentage total should be 100");
        return false;
    }


        url = '/PayRoll/MonthlyEmployeeVsCostCenter/SaveMonthlyEmployeeVsCostCenter'

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
                    url: url,
                    dataType: 'json',
                    data: $('#EmployeeVsCostCenterSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddNewOrEdit');
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


function validateDropDowns() {
    debugger;
    var i = 0;
    if (!validateById('#Month', 'CostMonth')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;

}






function ApplyFilter(mode) {
    debugger;


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            GetList();
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            GetList();
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


function ExportToExcel() {

    if (validateDropDowns()) {
        url = '/PayRoll/MonthlyEmployeeVsCostCenter/ExportToExcel?MonthCode=' + $('#Month').val() + '&MonthName=' + $('#Month option:selected').text() + '&data=' + $('#BasicFilter').serialize()
    window.location.href = url;
    }
   
}

