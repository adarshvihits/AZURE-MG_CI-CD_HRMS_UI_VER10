function fillMonth(sender) {
    debugger;
    $.getJSON("/PayRoll/SalaryProcessing/GetMonth", { PayrollGrp: $(sender).val() }, function (data) {

        $('#PayrollMonth_Code option').remove();
        $('#PayrollMonth_Code').append('<option value>--Select-- </option');
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
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
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
    }

}





function onProceedClick() {
    debugger
    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'month') )       
        i++;   
    if (!validateById("#PayrollGroup_Code", 'payrollgp'))
      
        i++;
   
    if (i == 0) {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/SalaryProcessing/GetSalaryProcessingValidations',
            dataType: 'html',
            data: { SalaryPeriodCode: $("#PayrollMonth_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val() },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    debugger
                    $("#sal_selection").addClass('hide');
                    $("#sal_CommonList").html('');
                    $("#sal_CommonList").html(result);
                    $("#sal_Hr").removeClass('hide');
                }



            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }

    else
        return false;
}
function processSalary() {
    swal.fire({
        text: "Do you want to Process?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            proceed();
        }
        else {
            return false;
        }
    })
}
function proceed() {

    $.ajax({

        type: 'POST',
        url: '/PayRoll/SalaryProcessing/SalaryProcessing',
        /*dataType: 'json',*/
        data: $('#sal_save').serialize(),
        success: function (result) {
            debugger;
            if (result.success == true) {
                swal.fire(result.message).then(function () {
                    window.location.href = '/PayRoll/SalaryProcessing/Create';
                });

            }
            else if (result.message == null) {
                swal.fire('Salary Processed Successfully').then(function () {
                    var m = $('.negativeSalaryPopUp');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(result);
                    m.modal('show');
                });
                


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
function NegativeSalryPopUpClose() {
    $('#negativeSalaryList').html('');
    window.location.href = '/PayRoll/SalaryProcessing/Create';
}

function onTabNameClick(TabName)
{
    debugger
    if (TabName == "Hr") {

        $("#sal_Hr").removeClass('hide');
        $("#sal_statutory").addClass('hide');
        $("#sal_settings").addClass('hide');
        $("#sal_trn").addClass('hide');
        $("#sal_selection").addClass('hide');



    }
    else if (TabName == "statutory") {
        $("#sal_Hr").addClass('hide');
        $("#sal_statutory").removeClass('hide');
        $("#sal_settings").addClass('hide');
        $("#sal_trn").addClass('hide');
        $("#sal_selection").addClass('hide');

    }
    else if (TabName == "settings") {
        $("#sal_Hr").addClass('hide');
        $("#sal_statutory").addClass('hide');
        $("#sal_settings").removeClass('hide');
        $("#sal_trn").addClass('hide');
        $("#sal_selection").addClass('hide');

    }
    else  //selection
    {
        $("#sal_Hr").addClass('hide');
        $("#sal_statutory").addClass('hide');
        $("#sal_settings").addClass('hide');
        $("#sal_trn").addClass('hide');
        $("#sal_selection").removeClass('hide');
    }

}
function onNextClick(TabName) {
    i = 0;
    debugger
    if (TabName == "Hr") {
        $("#sal_Hr").addClass('hide');
        $("#sal_statutory").removeClass('hide');
        $("#sal_settings").addClass('hide');
        $("#sal_trn").addClass('hide');
        $("#sal_selection").addClass('hide');

    }
    else if (TabName == "statutory") {
        $("#sal_Hr").addClass('hide');
        $("#sal_statutory").addClass('hide');
        $("#sal_settings").removeClass('hide');
        $("#sal_trn").addClass('hide');
        $("#sal_selection").addClass('hide');

    }
    else // settings
        if (TabName == "settings") {
            $("#sal_Hr").addClass('hide');
            $("#sal_statutory").addClass('hide');
            $("#sal_settings").addClass('hide');
            $("#sal_trn").removeClass('hide');
            $("#sal_selection").addClass('hide');

        }
        else
        {   


            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryProcessing/MonthlySalaryProcessingValidation',
                data: {
                    SalaryPeriodCode: $("#PayrollMonth_Code").val(), PayrollGroup: $("#PayrollGroup_Code").val()
                },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        processSalary();

                    }
                    else if (result.message == null) {

                        var m = $('.sal_validation');
                        m.find('.modal-body').html('');
                        m.find('.modal-body').html(result);
                        m.modal('show');
                    }
                    else {
                        if (result.message == 'Arrear Processing Pending') {
                            swal.fire({
                                text: "Arrear Processing Pending. Do you want to Continue?",
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Ok'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    proceed();
                                }
                                else {
                                    return false;
                                }
                            })
                        }
                        else {
                            swal.fire(result.message);
                        }
                      
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
   
}



function getPopUp(Code, Name, TabName) {
    debugger
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();


    debugger;
    $.ajax({
        type: 'GET',
        url: "/PayRoll/SalaryProcessing/GetEmployeeDetails?Month=" + SalaryPeriodCode + "&PayrollGroup=" + PayrollGroup + "&Code=" + Code + "&RowName=" + Name + "&TabName=" + TabName,
        dataType: 'html',

        success: function (Data) {
            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            $('#salProcessPopup').html(Name);

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}


function ExportToExcelPopUp() {
    debugger
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();
    var MonthName = $("#PayrollMonth_Code option:selected").text();
    var TableLength = $('#SalaryProcessing_popup_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryProcessing/ExportToExcel?TotalRows=" + TableLength + "&Payroll=" + PayrollGroup + "&month=" + SalaryPeriodCode + "&PayrollGroupName=" + PayrollGroupName + "&MonthName=" + MonthName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




