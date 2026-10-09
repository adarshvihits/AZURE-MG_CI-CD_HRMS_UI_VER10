

function getList() {
    debugger;
    var Month = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();
   
    showSpinner();
    $.ajax({

        type: 'POST',
        url: '/Payroll/ProcessReadinessCheck/GetProcessReadinessCheckList',
        dataType: 'html',
        data: "PayrollGroup=" + PayrollGroup + "&Month=" + Month  ,
        success: function (result) {
            debugger
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
    prevTab = Tab;
}


function getPopUpSalary(Name, TabName) {
    debugger
    var SalaryPeriodCode = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();


    debugger;
    $.ajax({
        type: 'GET',
        url: "/PayRoll/ProcessReadinessCheck/GetEmployeeDetails?Month=" + SalaryPeriodCode + "&PayrollGroup=" + PayrollGroup + "&RowName=" + Name + "&TabName=" + TabName,
        dataType: 'html',

        success: function (Data) {
            var m = $('.readinessEmpList');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            $('#exampleModalLabel').html(Name);
            m.modal('show');


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}




function getPopUpDetails(Name, ProcessType, AttendType, SalaryType) {
    debugger
   
   
    var SalaryPeriodCode = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();


    debugger;
    $.ajax({
        type: 'GET',
        url: "/PayRoll/ProcessReadinessCheck/GetPopUpDetails?Month=" + SalaryPeriodCode + "&PayrollGroup=" + PayrollGroup + "&RowName=" + Name + "&ProcessType=" + ProcessType + "&AttendType=" + AttendType + "&SalaryType=" + SalaryType,
        dataType: 'html',

        success: function (Data) {
            var m = $('.readinessEmpList');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            $('#exampleModalLabel').html(Name);
            m.modal('show');


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}



function PRcheckListExportToExcel() {
    debugger;

    var SalaryPeriodCode = $('#Month_Code').val();
    var PayrollGroup = $('#PayrollGroup_Code').val();

    var SalPeriodName = $('#Month_Code option:selected').text();
    var PayrollName = $('#PayrollGroup_Code option:selected').text();



    var TableLength = $('#ProcessReadinessCheckList_Count').val();
    if (TableLength > 0) {
        url = '/PayRoll/ProcessReadinessCheck/GetProcessReadinessCheckList_Excel?&SalaryPeriodCode=' + SalaryPeriodCode + "&PayrollGroup=" + PayrollGroup + "&SalPeriodName=" + SalPeriodName + "&PayrollName=" + PayrollName + "&totRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
    }






function ExportToExcelSalaryPopUp(TabName, RowName, TableId) {
  
    debugger
    var SalaryPeriodCode = $('#Month_Code').val();
    var MonthName = $('#Month_Code option:selected').text();

    var PayrollGroup = $('#PayrollGroup_Code').val();
    var PayrollGroupName = $('#PayrollGroup_Code option:selected').text();

    var TableLength = $('#' + TableId).val();

    if (TableLength > 0) {
        url = "/PayRoll/ProcessReadinessCheck/ExportToExcelSalaryPopUp?TotalRows=" + TableLength + "&Payroll=" + PayrollGroup + "&month=" + SalaryPeriodCode + "&PayrollGroupName=" + PayrollGroupName + "&MonthName=" + MonthName + "&TabName=" + TabName + "&RowName=" + RowName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function AttendanceExportToExcelPopUp(TabName, RowName, tab_index) {
    debugger


    var SalaryPeriodCode = $("#Month_Code").val();
    var MonthName = $("#Month_Code option:selected").text();

    var PayrollGroup = $("#PayrollGroup_Code").val();
    var PayrollGroupName = $("#PayrollGroup_Code option:selected").text();

    var TableLength = $('#MonthlyAttendanceProcessing_MonthlyAttendanceProcessing_Validations_popup_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/ProcessReadinessCheck/ExportToExcelAttendancePopup?TotalRows=" + TableLength + "&Payroll=" + PayrollGroup + "&month=" + SalaryPeriodCode + "&PayrollGroupName=" + PayrollGroupName + "&MonthName=" + MonthName + "&tabName=" + TabName + "&rowName=" + RowName + "&tab_index=" + tab_index ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}





function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("ProcessReadinessCheck_Table");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
        //if (noOfRows > 1) {
        //    $('#noDataImg').remove();
        //}
        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");
            //console.log("rs = " + rs);
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }
    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}
function onSearchClick() {
    if (searchClick == 0) {
        searchClick = 1;
        $('#dataTableSearch').addClass('field-width-big');
        CursorFocusOnSearch();
    }
    else {
        searchClick = 0;
        $('#dataTableSearch').removeClass('field-width-big');
    }
}

function DaysCountClickPopUp(employeeGI, EmpName, Description) {
  
    debugger;
    var month = $("#Month_Code").val();
    $.ajax({

        type: 'GET',
        url: '/PayRoll/ProcessReadinessCheck/GetBlankStatDaysPopList',
        dataType: 'html',
        data: { EmployeeGI: employeeGI, month: month, Description: Description },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                debugger
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.modal('hide');


                var m1 = $('.bd-example-modal-lg1');
                m.find('.modal-body').html('');
                m1.modal('hide');




                var m2 = $('.bd-example-modal-lg2');
                m2.find('.modal-body').html('');
                m2.find('.modal-body').html(result);

                $('#exampleModalLabelemp').html(EmpName);

                m2.modal('show');
            }



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}