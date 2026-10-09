function GetMonth() {

    $.getJSON("/PayRoll/SalaryPosting/GetMonth", { PayrollGrpCode: $('#PayrollGroup_Code').val() }, function (data) {

        $('#PayrollMonth_Code option').remove();
        $('#PayrollMonth_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PayrollMonth_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }

    });
}
function GetPostingList() {
    debugger;
    var Month = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
  
    /* showSpinner();*/
    if (Month !== '') {
        var url = "/PayRoll/SalaryPosting/GetList?Month=" + Month + "&PayrollGroup=" + PayrollGroup;
        $('#SalaryPostingList').load(url, function () { /*hideSpinner();*/ });
    }
    else {

        return false;
    }
}
function onTabNameClick(TabName) {
    debugger
    if (TabName == "Verify") {

        $("#sal_validate").removeClass('hide');
        $("#sal_details").addClass('hide');
        $("#sal_selection").addClass('hide');
        $("#sal_comparison").addClass('hide');

    }
    else if (TabName == "Posting") {
        $("#sal_validate").addClass('hide');
        $("#sal_details").removeClass('hide');
        $("#sal_selection").addClass('hide');
        $("#sal_comparison").addClass('hide');
    }
    else if (TabName == "Comparison")
    {
        $("#sal_validate").addClass('hide');
        $("#sal_details").addClass('hide');
        $("#sal_selection").addClass('hide');
        $("#sal_comparison").removeClass('hide');
    }
   
    else  //selection
    {
   
        $("#sal_validate").addClass('hide');
        $("#sal_details").addClass('hide');
        $("#sal_comparison").addClass('hide');
        $("#sal_selection").removeClass('hide');
    }

}
function onProceedClick() {
    debugger
    var i = 0;
    if (!validateById("#PayrollMonth_Code", 'month'))
        i++;
    if (!validateById("#PayrollGroup_Code", 'payrollgp'))
        
    
        i++;

    if (i == 0) {
        $.ajax({

            type: 'GET',
            url: '/PayRoll/SalaryPosting/SalaryPostingValidations',
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
                    $("#sal_validate").removeClass('hide');
          
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
function onNextClick(TabName) {
    i = 0;
    debugger

    if (TabName == "Verify") {
        $("#sal_validate").addClass('hide');
        $("#sal_details").addClass('hide');
        $("#sal_comparison").removeClass('hide');
        $("#sal_selection").addClass('hide');

    }
    else if (TabName == "Comparison") {
        $("#sal_validate").addClass('hide');
        $("#sal_details").removeClass('hide');
        $("#sal_comparison").addClass('hide');
        $("#sal_selection").addClass('hide');
    }
}
function OnEmployeeCountClick(desc, Type, Code) {
    debugger;
    var Month = $('#PayrollMonth_Code').val();
    var MonthName = $('#PayrollMonth_Code option:selected').text();


    debugger;
    $.ajax({
        type: 'GET',
        url: "/PayRoll/SalaryPosting/GetEmployeeDetails?Month=" + Month + "&MonthName=" + MonthName + "&desc=" + desc + "&Type=" + Type + "&Code=" + Code,
        dataType: 'html',

        success: function (Data) {

            var md = $('.bd-validation-modal-lg');
            md.find('.validationpopup').html('');

            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function getPopUp(Code, Name) {
    debugger
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
   

    debugger;
    $.ajax({
        type: 'GET',
        url: "/PayRoll/SalaryPosting/SalaryPostingValidationsPopUp?Month=" + SalaryPeriodCode + "&PayrollGroup=" + PayrollGroup + "&RowCode=" + Code + "&RowName=" + Name,
        dataType: 'html',

        success: function (Data) {
            var md = $('.bd-example-modal-lg');
            md.find('.modal-body').html('');

            var m = $('.bd-validation-modal-lg');
            m.find('.validationpopup').html('');
            m.find('.validationpopup').html(Data);
            $('#modeltittle').html(Name);
            m.modal('show');


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}



function MonthlySalaryPostingValidation() {
    debugger
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();


    debugger;
    $.ajax({
        type: 'GET',
        url: "/PayRoll/SalaryPosting/MonthlySalaryPostingValidation?SalaryPeriodCode=" + SalaryPeriodCode + "&PayrollGroupCode=" + PayrollGroup,        
        success: function (result) {
           

            if (result.success == true) {
                SalaryPosting();

            }
            else if (result.message == null) {
                var m = $('.bd-validation-modal-lg');
                m.find('.validationpopup').html('');
                m.find('.validationpopup').html(result);
                $('#modeltittle').html($('#Heading').val());
                m.modal('show');

            }
            else {
                swal.fire(result.message).then(function () {
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}



function SalaryPosting() {
    swal.fire({
        text: "After posting,the salary period will be locked,and further changes to employee salaries for this period will not be allowed.Do you want to Proceed?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/PayRoll/SalaryPosting/SalaryPosting',
                dataType: 'json',
                data: $('#sal_save').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            window.location.href = '/PayRoll/SalaryPosting/Index';
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
        else {
            return false;
        }
    })
}
function ExportToExcelPopUp() {
    var SalaryPeriodCode = $("#PayrollMonth_Code").val();
    var PayrollGroup = $("#PayrollGroup_Code").val();
    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();
    var MonthName = $("#PayrollMonth_Code option:selected").text();
    var TableLength = $('#PostingValidation_Popup_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryPosting/ValidationsPopUpExportToExcel?TotalRows=" + TableLength + "&Payroll=" + PayrollGroup + "&month=" + SalaryPeriodCode + "&PayrollGroupName=" + PayrollGroupName + "&MonthName=" + MonthName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function EmpExportToExcelPopUp(desc, Type, Code) {
  
    var Month = $("#PayrollMonth_Code").val();
   
    var MonthName = $("#PayrollMonth_Code option:selected").text();
    var TableLength = $('#PostingEmpList_Popup_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/SalaryPosting/PopupExportToExcel?Month=" + Month + " &MonthName=" + MonthName + " &desc=" + desc + " &Type=" + Type + " &Code=" + Code;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

 
        

