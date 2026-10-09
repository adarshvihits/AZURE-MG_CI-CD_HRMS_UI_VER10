
var currentTab = 'monthTab';
var prevTab = 'monthTab';
$(document).ready(function () {
    debugger
   // kendo.ui.progress($(".chart-loading"), true);
    kendo.resize($(".k-grid, .k-chart"));

    var queryString = window.location.search;

    // Parse the query string to extract parameters
    var params = new URLSearchParams(queryString);

    // Get the value of the "id" parameter
    var id = params.get("id");

    var radioGroup = $("#radiogroup").data("kendoRadioGroup");

    $("#dashBoardId").val(id);


    if (id == 1 || id == 2 || id == 6) {
        $("#fromDate").removeClass("hide");
        $("#toDate").removeClass();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").hide();
        $('#employees').hide();
        if (id == 1) {
            // $("#range").removeClass("hide");
            $('#dashName').html('New Hires');
        }
        else if (id == 2) {
            $('#dashName').html('Resignation');
        }
        else {
            $('#dashName').html('Absenteeism');
        }

    }
    else if (id == 3) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").hide();
        $('#employees').removeClass("hide");
        $('#dashName').html('Employee Analysis');


        //   $('#employee').data('kendoMultiColumnComboBox').dataSource.read();
    }



    else if (id == 4) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").removeClass("hide");

        $("#attendance").removeClass("hide");


        $("#fy").hide();
        $('#employees').hide();
        $('#dashName').html('Leave Analysis');
    }
    else if (id == 5) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").removeClass("hide");;
        $('#employees').hide();
        $('#dashName').html('Payroll Analysis');
    }
    else if (id == 7) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").hide();
        $('#employees').hide();
        $('#dashName').html('Historical Analysis');
    }
    else if (id == 8) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#dashName').html('Recruitment Analysis');
    }
    else if (id == 9 || id == 11) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").hide();
        $('#employees').hide();
        $("#reportType").removeClass("hide");
        if (id == 11)
            radioGroup.value(2);
        $('#dashName').html('HR At a Glance');
    }
    else if (id == 10) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        
        $("#fy").hide();
        $('#employees').hide();
        $('#dashName').html('Workforce Time Trends');
    }
    else if (id == 12) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#dashName').html('Attendance OverView');
    }
    else if (id == 13) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#dashName').html('Payroll Overview');
    }
    else if (id == 14) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#dashName').html('Leave Overview');
    }

    else if (id == 15) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#dashName').html('WorkForce Time Overview');
    }

    else if (id == 16) {
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#employeesOv').removeClass("hide");
        $('#dashName').html('Employee Overview');
    }

    else if (id == 17) {
        var age = params.get("age");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html('Resignation Age ' + age);
    }

    else if (id == 18) {
        var age = params.get("age");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html('Resignation based on Service ' + age);
    }
    else if (id == 19) {
        var age = params.get("age");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();
        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html('Resignation based on Gender-' + age);
    }
    else if (id == 20) {
        var age = params.get("name");

        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 21) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 22) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 23) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 26 || id == 27) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 28) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 29) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 30) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 31) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 32) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 33) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 34) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 35) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 36) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 37) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 38) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 40) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        // $("#groupCompany").addClass("hide");
        $("#fy").removeClass("hide");
        $("#transitionjobType").removeClass("hide");
        $('#employees').hide();
        $('#employeesOv').hide();
        $('#dashName').html('Job Transition Type');

    }

    else if (id == 41) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        //  $("#groupCompany").addClass("hide");
        $("#fy").removeClass("hide");;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html('Grievance');
    }

    else if (id == 45) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }

    else if (id == 50) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 51) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 52) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 53) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 59) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 80) {
        //var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
       // $('#dashName').html(age);
    }
    else if (id == 128) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        // $("#groupCompany").addClass("hide");
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#employeesOv').hide();
        $('#dashName').html('Bad Hires');

    }
    else if (id == 129) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(age);
    }
    else if (id == 130) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").removeClass("hide");
        $('#employees').hide();
        $('#employeesOv').hide();
        $('#dashName').html('Training');
    }
    else if (id == 131) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();
        $('#employees').hide();
        $('#employeesOv').hide();
        $('#dashName').html(age);
    }
    else if (id == 132) {
        var age = params.get("name");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();
        $('#employees').hide();
        $('#employeesOv').hide();
        $('#dashName').html(age);
    }

    else if (id == 81) {
        var age = params.get("type");
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
         $('#dashName').html(age);
    }
    else if (id == 60) {
        var age = params.get("name");
        var dateObject = new Date(age);

        // Format the date without the time component
        var dateWithoutTime = dateObject.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'short',
            day: 'numeric'
        });
       
        $("#fromDate").hide();
        $("#toDate").hide();
        $("#lp").hide();
        $("#attendance").hide();

        $("#groupCompany").addClass("hide");
        $("#fy").hide();;
        $('#employees').hide();
        $('#employeesOv').hide();;
        $('#dashName').html(dateWithoutTime);
    }
    $("#From").change(function () {
        var startDate = $(this).val();
        var endDate = $("#To").val();
        updateCharts(startDate, endDate);
    });

    $("#To").change(function () {
        var startDate = $("#From").val();
        var endDate = $(this).val();
        updateCharts(startDate, endDate);
    });


    $("#TransitionTypeCode").change(function () {

        updateTransitionTypeCharts();
    });

    function updateTransitionTypeCharts() {
        var transitionType = $("#transitionType").data("kendoChart");
        transitionType.dataSource.read();
        var transitionDepartment = $("#transitionDepartment").data("kendoChart");
        transitionDepartment.dataSource.read();
        var transitionLocation = $("#transitionLocation").data("kendoChart");
        transitionLocation.dataSource.read();
        var transitionDivision = $("#transitionDivision").data("kendoChart");
        transitionDivision.dataSource.read();
        var transitionCategory = $("#transitionCategory").data("kendoChart");
        transitionCategory.dataSource.read();
        var transitionGrade = $("#transitionGrade").data("kendoChart");
        transitionGrade.dataSource.read();
        var transitionRegion = $("#transitionRegion").data("kendoChart");
        transitionRegion.dataSource.read();
    }

  

    $("#LeavePeriod_Code").change(function () {
        var currntLp = $("#LeavePeriod_Code").val();
        updateLeaveCharts(currntLp);
    });

    $("#finYear").change(function () {
        var currntFY = $("#finYear").val();
       
        if (id == 8) {
            updateRecruitmentCharts(currntFY);
        }
        else if (id == 5) {
            updateSalaryCharts(currntFY);
        }
        else if (id == 40) {

            updateTransitionTypeCharts();
        }
        else if (id == 41) {

            updateGrievanceCharts();
        }
        else if (id == 128) {
            updateBadHiresCharts();
        }
        else if (id == 130) {
            updateTrainingCharts();
        }
        else {
            $.ajax({

                type: 'GET',
                url: '/MIS/MISDashBoard/GetMonths',
                dataType: 'html',
                data: { FinancialYrCode: $('#finYear').val() },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {

                        $('#monthsDiv').html("");
                        $('#monthsDiv').html(result);
                    }

                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);

                }
            });
        }
    });


    $("#GroupCompanyCode").change(function (){
        
        if (id == 1 || id == 2 || id == 6) {
            var startDate = $("#From").val();
            var endDate = $("#To").val();
            updateCharts(startDate, endDate);
        }
        else if (id == 3) {
            var gc = $("#GroupCompanyCode").val();
            var dataSource = new kendo.data.DataSource({
                transport: {
                    read: {
                        url: '/MIS/MISDashBoard/GetEmpList',
                        data: { groupCom: gc },
                        dataType: "json"
                    }
                }
            });

        
            var multicolumncombobox = $("#employee").data("kendoMultiColumnComboBox");
           
            multicolumncombobox.setDataSource(dataSource); 
         
        }
        else if (id == 4) {
            var currntLp = $("#LeavePeriod_Code").val();
            updateLeaveCharts(currntLp);
        }
        else if (id == 5) {
            var currntFY = $("#finYear").val();
            updateSalaryCharts(currntFY);
        }
        else if (id == 7) {
           
            updateHistoricalTrends();
        }
        else if (id == 8) {
            var currntFY = $("#finYear").val();
            updateRecruitmentCharts(currntFY);
        }
        else if (id == 9) {

            updateHRTrends();
        }
        else if (id == 10) {

            updatePunchingCharts();
        }
        else if (id == 40) {

            updateTransitionTypeCharts();
        }
        else if (id == 41) {

            updateGrievanceCharts();
        }
        else if (id == 128) {
            updateBadHiresCharts();
        }
    });

    if (id !== null) {
        // Load partial views accordingly based on the "id" value
        loadPartialViews(id);
    }
   
});



$(document).ready(function () {
    // Spin all loading indicators on the page
    kendo.ui.progress($(".chart-loading"), true);
});

function loadPartialViews(id) {
    debugger
    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/ReloadDiv',
        dataType: 'html',
        data: { id: id },

        success: function (result) {

            $("#MisDashBoard").html(result);
           // $("#joiningCount").html(result.joiningCount);

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
          

        }
    });


}

function getParamForAtt() {
    var currntLp = $("#CurrentSalaryPeriodCode").val();
    var gc = $("#GroupCompanyCode").val();
    var day = $("#hdnDay").val();
    return { day:day, month: currntLp, groupCom: gc };
}
function getParamForAttList() {

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var currntLp = params.get("month");   
    var gc = $("#GroupCompanyCode").val();
    var day = params.get("day");
    
    return { day: day, month: currntLp, groupCom: gc };
}
var currntLp = $("#CurrentSalaryPeriodCode").val();
var gc = $("#GroupCompanyCode").val();

function onEmpChange() {
    $(".red").find(".k - tilelayout - item - header").css("background-color", "red");
    var empCode = $('#employee').val();
    updateEmployeeCharts(empCode);  
   
}

function getParamEmpTraining() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var financialYr = params.get("financialYr");
    var code = params.get("code");
    return { financialYr: financialYr, code: code };
}
function getParamTrainingCost() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var code = params.get("code");
    return { code: code };
}
function onEmpChangeOv() {
    $(".red").find(".k - tilelayout - item - header").css("background-color", "red");
    var empCode = $('#employeeOv').val();
    updateEmployeeChartsOv(empCode);
}

function updateEmployeeChartsOv(empCode) {

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetEmployeeInformation',
        dataType: 'json',
        data: { empCode: empCode },

        success: function (result) {

            $("#fullName").html(result.fullName);
            $("#doj").html(result.doj);
            $("#dob").html(result.dob);
            $("#dot").html(result.dot);
            $("#department").html(result.department);
            $("#designation").html(result.designation);
            $("#location").html(result.location);
            $("#grossPay").html(result.grossPay);
            $("#ctc").html(result.ctc);
            $("#leave").html(result.leave);
            $("#leaveBalance").html(result.leaveBalance);
            $('#proPic').attr("src", result.proPic);

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

    var currntLp = $("#CurrentSalaryPeriodCode").val();

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetAttendanceEmpMonthData',
        dataType: 'json',
        data: { month: currntLp, empCode: empCode },

        success: function (result) {

            $("#allowance").html(result.allowance);
            $("#deduction").html(result.deduction);
            $("#netPay").html(result.netPay);
            $("#lateIn").html(result.lateIn);
            $("#earlyOut").html(result.earlyOut);
            $("#ot").html(result.ot);
            $("#th").html(result.th);
           

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
   
    var monthAttEmpOv = $("#monthAttEmpOv").data("kendoChart");
    monthAttEmpOv.dataSource.read();

    var hrTOOv = $("#hrTOOv").data("kendoChart");
    hrTOOv.dataSource.read();

    //var punchOv = $("#punchOv").data("kendoChart");
    //punchOv.dataSource.read();
}


function updateEmployeeCharts(empCode) {

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetEmployeeInformation',
        dataType: 'json',
        data: { empCode: empCode },

        success: function (result) {
           
            $("#fullName").html(result.fullName);
            $("#doj").html(result.doj);
            $("#dob").html(result.dob);
            $("#dot").html(result.dot);
            $("#department").html(result.department);
            $("#designation").html(result.designation);
            $("#location").html(result.location);
            $("#grossPay").html(result.grossPay);
            $("#ctc").html(result.ctc);
            $("#leave").html(result.leave);
            $("#leaveBalance").html(result.leaveBalance);
            $('#proPic').attr("src", result.proPic);
            
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
    var monthLeave = $("#monthLeave").data("kendoChart");
    monthLeave.dataSource.read();
    var empGrowth = $("#empGrowth").data("kendoChart");
    empGrowth.dataSource.read();
   
    var empReward = $("#empReward").data("kendoChart");
    empReward.dataSource.read();

    var empSkill = $("#empSkill").data("kendoChart");
    empSkill.dataSource.read();

}

function updateLeaveCharts(currntLp) {
  
    var gc = $("#GroupCompanyCode").val();
   

        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetLeave',
            dataType: 'json',
            data: { currntLp: currntLp, groupCom:gc },

            success: function (result) {

                $("#leaveCount").html(result.leaveCount);
                $("#avgLeave").html(result.avgLeave);
                $("#lopCount").html(result.lopCount);
                $("#absCount").html(result.absCount);

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
        var leaveAge = $("#leaveAge").data("kendoChart");
        leaveAge.dataSource.read();
        var monthLeave = $("#monthLeave").data("kendoChart");
        monthLeave.dataSource.read();
        var leaveDepartment = $("#leaveDepartment").data("kendoChart");
    leaveDepartment.dataSource.read();
    var leaveDivision = $("#leaveDivision").data("kendoChart");
    leaveDivision.dataSource.read();
    var leaveCat = $("#leaveCat").data("kendoChart");
    leaveCat.dataSource.read();
    var leaveReg = $("#leaveReg").data("kendoChart");
    leaveReg.dataSource.read();
    var leaveGrade = $("#leaveGrade").data("kendoChart");
    leaveGrade.dataSource.read();
    var leaveLocation = $("#leaveLocation").data("kendoChart");
    leaveLocation.dataSource.read();
        var leaveCatagory = $("#leaveCatagory").data("kendoChart");
        leaveCatagory.dataSource.read();   
   

}


function updateCharts(startDate, endDate) {
    var queryString = window.location.search;

    // Parse the query string to extract parameters
    var params = new URLSearchParams(queryString);

    // Get the value of the "id" parameter
    var id = params.get("id");
    var gc = $("#GroupCompanyCode").val();
 
   
    if (id == 1) {

        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetList',
            dataType: 'json',
            data: { from: startDate, to: endDate, groupCom: gc },

            success: function (result) {
                
                $("#avgJoin").html(result.averageJoinersCount);
                $("#hiresCount").html(result.joiningCount);
                $("#badJoins").html(result.badHires);

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
               

            }
        });
        var genderJoins = $("#genderJoins").data("kendoChart");
        genderJoins.dataSource.read();
        var monthResignation = $("#monthResignation").data("kendoChart");
        monthResignation.dataSource.read();
        var joinsAge = $("#joinsAge").data("kendoChart");
        joinsAge.dataSource.read();
        var joinsDepartment = $("#joinsDepartment").data("kendoChart");
        joinsDepartment.dataSource.read();
        var joinsLocation = $("#joinsLocation").data("kendoChart");
        joinsLocation.dataSource.read();
        var bHires = $("#bHires").data("kendoChart");
        bHires.dataSource.read();
       
        var joinsGrade = $("#joinsGrade").data("kendoChart");
        joinsGrade.dataSource.read();
        var joinsDivision = $("#joinsDivision").data("kendoChart");
        joinsDivision.dataSource.read();
        var joinsCategory = $("#joinsCategory").data("kendoChart");
        joinsCategory.dataSource.read();
        var joinsRegion = $("#joinsRegion").data("kendoChart");
        joinsRegion.dataSource.read();
    }
    else if (id == 2) {
        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetList',
            dataType: 'json',
            data: { from: startDate, to: endDate, groupCom: gc },

            success: function (result) {
               
                $("#attrition").html(result.attritionRate);
                $("#hiresCount").html(result.joiningCount);
                $("#resignationCount").html(result.resignationCount);
                $("#averageResignationCount").html(result.averageResignationCount);
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
               

            }
        });
        var resignationbyReason = $("#resignationbyReason").data("kendoChart");
        resignationbyReason.dataSource.read();
        var genderBand = $("#genderBand").data("kendoChart");
        genderBand.dataSource.read();
        var resignationTenure = $("#resignationTenure").data("kendoChart");
        resignationTenure.dataSource.read();
        var resignationAge = $("#resignationAge").data("kendoChart");
        resignationAge.dataSource.read();
        var monthResignation = $("#monthResignation").data("kendoChart");
        monthResignation.dataSource.read();
        var termDepartment = $("#termDepartment").data("kendoChart");
        termDepartment.dataSource.read();

        var termLocation = $("#termLocation").data("kendoChart");
        termLocation.dataSource.read();

        var termDivision = $("#termDivision").data("kendoChart");
        termDivision.dataSource.read();
        var termGrade = $("#termGrade").data("kendoChart");
        termGrade.dataSource.read();
        var termCategory = $("#termCategory").data("kendoChart");
        termCategory.dataSource.read();
        var termRegion = $("#termRegion").data("kendoChart");
        termRegion.dataSource.read();

    }

    else if (id == 6) {

        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetAbsRate',
            dataType: 'json',
            data: { from: startDate, to: endDate },

            success: function (result) {

                $("#absenRate").html(result.absRate);
                $("#absCount").html(result.absenteesmCount);

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });
        var monthAbs = $("#monthAbs").data("kendoChart");
        monthAbs.dataSource.read();

        var absbyAge = $("#absAge").data("kendoChart");
        absbyAge.dataSource.read();

        var absTenure = $("#absTenure").data("kendoChart");
        absTenure.dataSource.read();

        var absDepartment = $("#absDepartment").data("kendoChart");
        absDepartment.dataSource.read();

        var absLocation = $("#absLocation").data("kendoChart");
        absLocation.dataSource.read();

        var absDivision = $("#absDivision").data("kendoChart");
        absDivision.dataSource.read();

        var absCategory = $("#absCategory").data("kendoChart");
        absCategory.dataSource.read();

        var absRegion = $("#absRegion").data("kendoChart");
        absRegion.dataSource.read();

        var absGrade = $("#absGrade").data("kendoChart");
        absGrade.dataSource.read();

        var absWeek = $("#absWeek").data("kendoChart");
        absWeek.dataSource.read();

    }
    
}

//function updateSalaryCharts(currntFY) {

//    var gc = $("#GroupCompanyCode").val();

//    $.ajax({

//        type: 'GET',
//        url: '/MIS/MISDashBoard/GetSalaryInformation',
//        dataType: 'json',
//        data: { finCode: currntFY,groupCom:gc },
       
//        success: function (result) {

           

//            $("#totSalary").html(result.totalSalary);
//            $("#avgSalary").html(result.avgSalary);
           
          

//        },
//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });

//    $.ajax({

//        type: 'GET',
//        url: '/MIS/MISDashBoard/GetSalaryIncrementInformation',
//        dataType: 'json',
//        data: { finCode: currntFY, groupCom: gc },

//        success: function (result) {

//            $("#totIncrement").html(result.totalIncrement);
//            $("#avgIncrement").html(result.avgIncrement);

//        },
//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });

//    var monthSalary = $("#monthSalary").data("kendoChart");
//    monthSalary.dataSource.read();
//    var incDepartment = $("#incDepartment").data("kendoChart");
//    incDepartment.dataSource.read();

//    var salaryDpt = $("#salaryDpt").data("kendoChart");
//    salaryDpt.dataSource.read();

//    var salaryLoc = $("#salaryLoc").data("kendoChart");
//    salaryLoc.dataSource.read();

//    var salaryDiv = $("#salaryDiv").data("kendoChart");
//    salaryDiv.dataSource.read();

//    var salaryGrade = $("#salaryGrade").data("kendoChart");
//    salaryGrade.dataSource.read();

//    var salaryCat = $("#salaryCat").data("kendoChart");
//    salaryCat.dataSource.read();

//    var salaryReg = $("#salaryReg").data("kendoChart");
//    salaryReg.dataSource.read();

//    var alwComp = $("#alwComp").data("kendoChart");
//    alwComp.dataSource.read();

//    var dedComp = $("#dedComp").data("kendoChart");
//    dedComp.dataSource.read();

//    var salAlw = $("#salAlw").data("kendoChart");
//    salAlw.dataSource.read();

//    var salDed = $("#salDed").data("kendoChart");
//    salDed.dataSource.read();



//}

//function updateHistoricalTrends() {

//    var histJoins = $("#histJoins").data("kendoChart");
//    histJoins.dataSource.read();
//    var histTerminations = $("#histTerminations").data("kendoChart");
//    histTerminations.dataSource.read();
//    var histBadHires = $("#histBadHires").data("kendoChart");
//    histBadHires.dataSource.read();
//    var histLOPMonth = $("#histLOPMonth").data("kendoChart");
//    histLOPMonth.dataSource.read();
//    var histWFHMonth = $("#histWFHMonth").data("kendoChart");
//    histWFHMonth.dataSource.read();
//    var hisSalInc = $("#hisSalInc").data("kendoChart");
//    hisSalInc.dataSource.read();
//    var hisSal = $("#hisSal").data("kendoChart");
//    hisSal.dataSource.read();
//}
function updateRecruitmentCharts(currntFY) {

    var gc = $("#GroupCompanyCode").val();

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetRecruitmentInformation',
        dataType: 'json',
        data: { finCode: currntFY, groupCom: gc },

        success: function (result) {

            

            $("#totVacancy").html(result.vacancy);
            $("#totInterviewed").html(result.interviewed);
            $("#totOffered").html(result.offered);
            $("#totJoined").html(result.joined);


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

    var vacancyTrend = $("#vacancyTrend").data("kendoChart");
    vacancyTrend.dataSource.read();
    var vacancyDsg = $("#vacancyDsg").data("kendoChart");
    vacancyDsg.dataSource.read();
}

//function updateHRTrends() {

 

//    var salRange = $("#salRange").data("kendoChart");
//    salRange.dataSource.read();
  
//    var genderHR = $("#genderHR").data("kendoChart");
//    genderHR.dataSource.read();

//    var hrByDept = $("#hrByDept").data("kendoChart");
//    hrByDept.dataSource.read();

//    var hrByAg = $("#hrByAg").data("kendoChart");
//    hrByAg.dataSource.read();

//    var hrByTen = $("#hrByTen").data("kendoChart");
//    hrByTen.dataSource.read();

//    var hrbyLoc = $("#hrbyLoc").data("kendoChart");
//    hrbyLoc.dataSource.read();

//    var hrByReg = $("#hrByReg").data("kendoChart");
//    hrByReg.dataSource.read();

//    var hrByDiv = $("#hrByDiv").data("kendoChart");
//    hrByDiv.dataSource.read();

//    var hrByGrd = $("#hrByGrd").data("kendoChart");
//    hrByGrd.dataSource.read();

//    var hrByCat = $("#hrByCat").data("kendoChart");
//    hrByCat.dataSource.read();

//    var hrByTimeToQuitJob = $("#hrByTimeToQuitJob").data("kendoChart");
//    hrByTimeToQuitJob.dataSource.read();

//}

function updatePunchingCharts() {
    var hrTO = $("#hrTO").data("kendoChart");
    hrTO.dataSource.read();

    var hrDept = $("#hrDept").data("kendoChart");
    hrDept.dataSource.read();

    var hrLoc = $("#hrLoc").data("kendoChart");
    hrLoc.dataSource.read();
    var hrDiv = $("#hrDiv").data("kendoChart");
    hrDiv.dataSource.read();
    var hrCat = $("#hrCat").data("kendoChart");
    hrCat.dataSource.read();
    var hrReg = $("#hrReg").data("kendoChart");
    hrReg.dataSource.read();
    var hrGrd = $("#hrGrd").data("kendoChart");
    hrGrd.dataSource.read();
}

function updateAttCharts() {
    var monthAtt = $("#monthAtt").data("kendoChart");
    monthAtt.dataSource.read();

   
}

function getParam() {
   
    var from = $("#From").val();
    var to = $("#To").val();
    var gc = $("#GroupCompanyCode").val();
  
    return { from: from, to: to, groupCom: gc };
}

function getLeavePeriod() {

    var currntLp = $("#LeavePeriod_Code").val();
    var gc = $("#GroupCompanyCode").val();
  
    return { currntLp: currntLp,groupCom:gc };
}

function getFY() {
    var gc = $("#GroupCompanyCode").val();
    var currntFY = $("#finYear").val();


    return { finCode: currntFY, groupCom: gc };
}

function getEmpCode() {

    var empCode = $('#employee').val()

    return { empCode: empCode };
}

function getGroupCompany() {

    var gc = $("#GroupCompanyCode").val();

    return { groupCom: gc };
}

function onTileResize(e) {
    if (e.container) {

        // for widgets that do not auto resize
        // https://docs.telerik.com/kendo-ui/styles-and-layout/using-kendo-in-responsive-web-pages
        kendo.resize(e.container, true);
    }
}

function onTypeChange(e) {
    if (e.newValue == 2) {
        loadPartialViews(11)
    }
    else {
        loadPartialViews(9)
    }
}

function onMonthTabClick(Tab, index) {
  

    var queryString = window.location.search;

    var params = new URLSearchParams(queryString);

    // Get the value of the "id" parameter
    var id = params.get("id");
    $("#" + prevTab).removeClass("active");
    $("#" + Tab).addClass("active");
    //$('#tabsDropdownSection').val(currentTab);  
    $('#CurrentSalaryPeriodCode').val($('#Months_' + index + '__SalaryPeriodCode').val());
    
    
    prevTab = Tab;
    if (id == 12) {
        var gc = $("#GroupCompanyCode").val();
        var currntLp = $("#CurrentSalaryPeriodCode").val();
        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetAttendanceMonthData',
            dataType: 'json',
            data: { month: currntLp, groupCom: gc },

            success: function (result) {

              
                $("#lateIn").html(result.lateIn);
                $("#earlyOut").html(result.earlyOut);
                $("#ot").html(result.ot);
                $("#th").html(result.th);


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });

        updateAttCharts();
        var attDate = $("#attDate").data("kendoChart");
        attDate.dataSource.read();
        $("#attDate").parent().parent().find(".k-card-title").html("1" + " " + $("#" + Tab).find("p").text())
    }
    else if (id == 13) {

        var currntLp = $("#CurrentSalaryPeriodCode").val();
        var gc = $("#GroupCompanyCode").val();


        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetEmpData',
            dataType: 'json',
            data: { month: currntLp, groupCom: gc },

            success: function (result) {
                console.log(result.difference);

                $("#totalEmp").html(result.totalEmp);

                if (result.difference < 0) {
                    // $("#prvData").html('<img src="/images/decrease2-.svg" class="downarrow" /><div id ="totalPrvEmp" style = "color: #fc2d2d;font-size:2rem;padding-right:4px;" > result.difference</div><div style="font-size:20px;">-vs Previous Month</div>');
                    $("#prvData").html('<img src="/images/decrease2-.svg" class="downarrow" /><div id ="totalPrvEmp" style = "color: #fc2d2d;font-size:2rem;padding-right:4px;" > result.difference</div>');
                }
                else {
                    //  $("#prvData").html('<img src="/images/decrease-.svg" class="uparrow" /><div id ="totalPrvEmp" style = "color: #37B400;font-size:2rem;padding-right:4px;" > result.difference</div><div style="font-size:20px;">-vs Previous Month</div>');
                    $("#prvData").html('<img src="/images/decrease-.svg" class="uparrow" /><div id ="totalPrvEmp" style = "color: #37B400;font-size:2rem;padding-right:4px;" > result.difference</div>');
                }

                $("#totalPrvEmp").html(result.difference);
                $("#newHr").html(result.newHires);
                $("#newResg").html(result.resignations);
                $("#employeeMaleCount").html('<i class="fa fa-male" aria-hidden="true" style="padding-right: 5px;font-size: 22px;padding-top: 5px;color: #97d5f2;"></i>' + result.totalEmpMale);
                $("#employeeFeMaleCount").html('<i class="fa fa-female" aria-hidden="true" style="padding-right: 5px;padding-top: 3px;padding-left: 15px; font-size: 22px;color:#f6b2d9;"></i>' + result.totalEmpFemale);
                $("#pfEmployer").html(result.pfEmployer);
                $("#pfEmployee").html(result.pfEmployee);
                $("#esiEmployer").html(result.esiEmployer);
                $("#esiEmployee").html(result.esiEmployee);
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });


        var salAlw = $("#salAlw").data("kendoChart");
        salAlw.dataSource.read();
        var salDed = $("#salDed").data("kendoChart");
        salDed.dataSource.read();
        var salCTC = $("#salCTC").data("kendoChart");
        salCTC.dataSource.read();

        var salPG = $("#salPG").data("kendoChart");
        salPG.dataSource.read();

        var alwComp = $("#alwComp").data("kendoChart");
        alwComp.dataSource.read();

        var dedComp = $("#dedComp").data("kendoChart");
        dedComp.dataSource.read();
    }
    else if (id == 14) {
        var leaveCatagoryOv = $("#leaveCatagoryOv").data("kendoChart");
        leaveCatagoryOv.dataSource.read();
        var leaveWeek = $("#leaveWeek").data("kendoChart");
        leaveWeek.dataSource.read();
        var leaveDepartmentOv = $("#leaveDepartmentOv").data("kendoChart");
        leaveDepartmentOv.dataSource.read();
        var leaveLocationOv = $("#leaveLocationOv").data("kendoChart");
        leaveLocationOv.dataSource.read();
        var leaveDivisionOv = $("#leaveDivisionOv").data("kendoChart");
        leaveDivisionOv.dataSource.read();
        var leaveCatOv = $("#leaveCatOv").data("kendoChart");
        leaveCatOv.dataSource.read();
        var leaveRegOv = $("#leaveRegOv").data("kendoChart");
        leaveRegOv.dataSource.read();
        var leaveGradeOv = $("#leaveGradeOv").data("kendoChart");
        leaveGradeOv.dataSource.read();
    }
    else if (id == 15) {
        var hrDeptOv = $("#hrDeptOv").data("kendoChart");
        hrDeptOv.dataSource.read();
        var hrLocOv = $("#hrLocOv").data("kendoChart");
        hrLocOv.dataSource.read();
        var hrDivOv = $("#hrDivOv").data("kendoChart");
        hrDivOv.dataSource.read();
        var hrCatOv = $("#hrCatOv").data("kendoChart");
        hrCatOv.dataSource.read();
        var hrGrdOv = $("#hrGrdOv").data("kendoChart");
        hrGrdOv.dataSource.read();
        var hrRegOv = $("#hrRegOv").data("kendoChart");
        hrRegOv.dataSource.read();
    }

    else {
        var currntLp = $("#CurrentSalaryPeriodCode").val();
        var empCode = $('#employeeOv').val()

        $.ajax({

            type: 'GET',
            url: '/MIS/MISDashBoard/GetAttendanceEmpMonthData',
            dataType: 'json',
            data: { month: currntLp, empCode: empCode },

            success: function (result) {

                $("#allowance").html(result.allowance);
                $("#deduction").html(result.deduction);
                $("#netPay").html(result.netPay);
                $("#lateIn").html(result.lateIn);
                $("#earlyOut").html(result.earlyOut);
                $("#ot").html(result.ot);
                $("#th").html(result.th);


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });

        var monthAttEmpOv = $("#monthAttEmpOv").data("kendoChart");
        monthAttEmpOv.dataSource.read();

        var hrTOOv = $("#hrTOOv").data("kendoChart");
        hrTOOv.dataSource.read();

        //var punchOv = $("#punchOv").data("kendoChart");
      //  punchOv.dataSource.read();
    }



}

function getSalPeriod() {

    var currntLp = $("#CurrentSalaryPeriodCode").val();
    var gc = $("#GroupCompanyCode").val();
    //console.log(currntLp, gc);
    return { month: currntLp, groupCom: gc };
}

function getEmpOv() {

    var currntLp = $("#CurrentSalaryPeriodCode").val();
    var empCode = $('#employeeOv').val()
    //console.log(currntLp, gc);
    return { month: currntLp, empCode: empCode };
}

$(window).on("resize", function () {
    kendo.resize($(".k-chart, .k-grid"));
});

//function getParamResg() {
//    var from = $("#From").val();
//    var to = $("#To").val();
//    var gc = $("#GroupCompanyCode").val();
//    var queryString = window.location.search;   
//    var params = new URLSearchParams(queryString);
//    var id = params.get("age");
//    return { from: from, to: to, groupCom: gc, age: id };
//}
//function getParamResgDept() {
//    var from = $("#From").val();
//    var to = $("#To").val();
//    var gc = $("#GroupCompanyCode").val();
//    var queryString = window.location.search;
//    var params = new URLSearchParams(queryString);
//    var id = params.get("age");
//    var type = params.get("type");
//    return { from: from, to: to, groupCom: gc, type:type , age: id };
//}
//function getParamHR() {
 
//    var gc = $("#GroupCompanyCode").val();
//    var queryString = window.location.search;
//    var params = new URLSearchParams(queryString);
    
//    var type = params.get("type");
//    var age = params.get("age");
//    return {  groupCom: gc, type: type,age:age };
//}
//function getParamSal() {

//    var gc = $("#GroupCompanyCode").val();
//    var queryString = window.location.search;
//    var params = new URLSearchParams(queryString);
//    var currntFY = $("#finYear").val();
//    var type = params.get("type");
//    var age = params.get("age");
//    return { finCode:currntFY,groupCom: gc, type: type, age: age };
//}
//function getParams() {
//    var Fy = $("#finYear").val();
//    var gc = $("#GrpCompanyCode").val();


//    return { grpCode: gc, fYear: Fy };
//}

function getVal() {
    var groupCom = $("#GroupCompanyCode").val();
    var financialYr = $("#finYear").val();
    var typeCode = $("#TransitionTypeCode").val();

    return { groupCom: groupCom, financialYr: financialYr, typeCode: typeCode };
}
//function getParamTransition() {
//    var groupCom = $("#GroupCompanyCode").val();
//    var financialYr = $("#finYear").val();
//    var typeCode = $("#TransitionTypeCode").val();
//    var queryString = window.location.search;
//    var params = new URLSearchParams(queryString);

//    var type = params.get("type");
//    var text = params.get("code");
//    return { groupCom: groupCom, financialYr: financialYr, typeCode: typeCode, type: type, text: text };
//}
function getParamPF() {
    var groupCom = $("#GroupCompanyCode").val();    
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);

    var month = params.get("month");
    var type = params.get("type");
    return {month:month, groupCom: groupCom,type:type};
}
function getParamGrievanceTotal() {
    debugger;
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var groupCom = params.get("groupCom");
    var FinancialYr = params.get("financialYr");
    var type = params.get("type");
    var GrievanceCatid = params.get("GrievanceCatid");
    return { GrpCompanyCode: groupCom, FinancialYear: FinancialYr, type: type, GrievanceCatid: GrievanceCatid };
}


function getParamEmployeeCarrer() {
    debugger;
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var EmployeeCode = params.get("EmployeeCode");

    return { EmployeeCode: EmployeeCode };
}
function getParamLateInAttList() {

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var currntLp = params.get("month");
    var gc = $("#GroupCompanyCode").val()
    var type = params.get("type");
    var code = params.get("code");

    return {  groupCom: gc,month: currntLp,type:type,code:code };
}
function GetMonthwiseDetail() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var monthcode = params.get("MONTHCODE");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, MONTHCODE: monthcode };
}

function GetAgeBanddetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var ageslab = params.get("AGESLAB");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, AGESLAB: ageslab };
}
function GetServiceBanddetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var yearslab = params.get("YEARSLAB");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, YEARSLAB: yearslab };
}
function GetDepartmentBanddetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var department = params.get("DEPARTMENT");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, DEPARTMENT: department };
}

function GetLocationdetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var location = params.get("LOCATION");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, LOCATION: location };
}
function GetWeeklydetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var weeklyname = params.get("DAY");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, DAY: weeklyname };
}

function GetDivisiondetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var division = params.get("DIVISION");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, DIVISION: division };
}
function GetCategorydetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var Category = params.get("CATEGORY");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, CATEGORY: Category };
}
function GetRegiondetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var region = params.get("REGION");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, REGION: region };
}
function GetGradedetaails() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var fromdate = params.get("FROMDATE");
    var todate = params.get("TODATE");
    var gc = params.get("GROUPCOMPANYCODE");


    var grade = params.get("GRADE");



    return { FROMDATE: fromdate, TODATE: todate, GROUPCOMPANYCODE: gc, GRADE: grade };
}
function GetVacancydetaaills() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var finyear = params.get("FinancialYear");
    var grp = params.get("GrpCompanyCode");


    var month = params.get("MONTH");



    return { FinancialYear: finyear, GrpCompanyCode: grp, MONTH: month };
}

function GetVacancyInfo() {
    debugger;

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var finyear = params.get("FinancialYear");
    var grp = params.get("GrpCompanyCode");


    var type = params.get("TYPE");



    return { FinancialYear: finyear, GrpCompanyCode: grp, TYPE: type };
}
function getParamResg() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var from = params.get("from");
    var to = params.get("to");
    var groupCom = params.get("groupCom");
    var id = params.get("age");
    return { from: from, to: to, groupCom: groupCom, age: id };
}
function getParamResgDept() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var from = params.get("from");
    var to = params.get("to");
    var groupCom = params.get("groupCom");
    var id = params.get("age");
    var type = params.get("type");
    return { from: from, to: to, groupCom: groupCom, type: type, age: id };
}
function getParamTransition() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var groupCom = params.get("groupCom");
    var financialYr = params.get("financialYr");
    var typeCode = params.get("typeCode");
    var type = params.get("type");
    var text = params.get("code");
    return { groupCom: groupCom, financialYr: financialYr, typeCode: typeCode, type: type, text: text };
}
function getParamLeave() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var groupCom = params.get("groupCom");
    var crrntLp = params.get("crrntLp");
    var type = params.get("type");
    var code = params.get("code");
    console.log(groupCom);
    return { groupCom: groupCom, crrntLp: crrntLp, type: type, code: code };

}
function getParamHR() {

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);

    var gc = params.get("groupCom");
    var type = params.get("type");
    var age = params.get("age");
    return { groupCom: gc, type: type, age: age };
}
function getParams() {
    var Fy = $("#finYear").val();
    var gc = $("#GroupCompanyCode").val();


    return { grpCode: gc, fYear: Fy };
}
function updateGrievanceCharts() {
    var grvByFy = $("#grvByFy").data("kendoChart");
    grvByFy.dataSource.read();
    var grvByCompleyedFy = $("#grvByCompleyedFy").data("kendoChart");
    grvByCompleyedFy.dataSource.read();
    var grvByAcceptedFy = $("#grvByAcceptedFy").data("kendoChart");
    grvByAcceptedFy.dataSource.read();
    var grvByNonAcceptedFy = $("#grvByNonAcceptedFy").data("kendoChart");
    grvByNonAcceptedFy.dataSource.read();
    var grvByRejectedFy = $("#grvByRejectedFy").data("kendoChart");
    grvByRejectedFy.dataSource.read();
}
function updateHistoricalTrends() {

    var histJoins = $("#histJoins").data("kendoChart");
    histJoins.dataSource.read();
    var histTerminations = $("#histTerminations").data("kendoChart");
    histTerminations.dataSource.read();
    var histBadHires = $("#histBadHires").data("kendoChart");
    histBadHires.dataSource.read();
    var histJoinersMonth = $("#histJoinersMonth").data("kendoChart");
    histJoinersMonth.dataSource.read();
    var histTerminationsMonth = $("#histTerminationsMonth").data("kendoChart");
    histTerminationsMonth.dataSource.read();
    var histAttritionMonth = $("#histAttritionMonth").data("kendoChart");
    histAttritionMonth.dataSource.read();
    var histLOPMonth = $("#histLOPMonth").data("kendoChart");
    histLOPMonth.dataSource.read();
    var histWFHMonth = $("#histWFHMonth").data("kendoChart");
    histWFHMonth.dataSource.read();
    var hisSalInc = $("#hisSalInc").data("kendoChart");
    hisSalInc.dataSource.read();
    var hisSal = $("#hisSal").data("kendoChart");
    hisSal.dataSource.read();
}
function updateHRTrends() {
    var gc = $("#GroupCompanyCode").val();

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetHrInformation',
        dataType: 'json',
        data: { groupCom: gc },

        success: function (result) {



            $("#employeeCount").html(result.employeeCount);
            $("#employeeMaleCount").html(result.employeeMaleCount);
            $("#employeeFeMaleCount").html(result.employeeFeMaleCount);
            $("#avgAge").html(result.avgAge);
            $("#avgTenure").html(result.avgTenure);
            $("#avgSal").html(result.avgGP);



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

    var salRange = $("#salRange").data("kendoChart");
    salRange.dataSource.read();

    //var genderHR = $("#genderHR").data("kendoChart");
    //genderHR.dataSource.read();

    var hrByDept = $("#hrByDept").data("kendoChart");
    hrByDept.dataSource.read();

    var hrByAg = $("#hrByAg").data("kendoChart");
    hrByAg.dataSource.read();

    var hrByTen = $("#hrByTen").data("kendoChart");
    hrByTen.dataSource.read();

    var hrByLoc = $("#hrByLoc").data("kendoChart");
    hrByLoc.dataSource.read();

    var hrByReg = $("#hrByReg").data("kendoChart");
    hrByReg.dataSource.read();

    var hrByDiv = $("#hrByDiv").data("kendoChart");
    hrByDiv.dataSource.read();

    var hrByGrd = $("#hrByGrd").data("kendoChart");
    hrByGrd.dataSource.read();

    var hrByCat = $("#hrByCat").data("kendoChart");
    hrByCat.dataSource.read();

    var hrByTimeToQuitJob = $("#hrByTimeToQuitJob").data("kendoChart");
    hrByTimeToQuitJob.dataSource.read();

}

function getParamSal() {

    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var gc = params.get("groupCom");
    var currntFY = params.get("finCode");
    var type = params.get("type");
    var age = params.get("age");
    return { finCode: currntFY, groupCom: gc, type: type, age: age };
}
function updateSalaryCharts(currntFY) {

    var gc = $("#GroupCompanyCode").val();

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetSalaryInformation',
        dataType: 'json',
        data: { finCode: currntFY, groupCom: gc },

        success: function (result) {



            $("#totSalary").html(result.totalSalary);
            $("#avgSalary").html(result.avgSalary);



        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetSalaryIncrementInformation',
        dataType: 'json',
        data: { finCode: currntFY, groupCom: gc },

        success: function (result) {

            $("#totIncrement").html(result.totalIncrement);
            $("#avgIncrement").html(result.avgIncrement);

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

    var monthSalary = $("#monthSalary").data("kendoChart");
    monthSalary.dataSource.read();

    var salaryDpt = $("#salaryDpt").data("kendoChart");
    salaryDpt.dataSource.read();

    var salaryLoc = $("#salaryLoc").data("kendoChart");
    salaryLoc.dataSource.read();

    var salaryDiv = $("#salaryDiv").data("kendoChart");
    salaryDiv.dataSource.read();

    var salaryGrade = $("#salaryGrade").data("kendoChart");
    salaryGrade.dataSource.read();

    var salaryCat = $("#salaryCat").data("kendoChart");
    salaryCat.dataSource.read();

    var salaryReg = $("#salaryReg").data("kendoChart");
    salaryReg.dataSource.read();

    var salAlw = $("#salAlw").data("kendoChart");
    salAlw.dataSource.read();

    var salDed = $("#salDed").data("kendoChart");
    salDed.dataSource.read();

    var alwComp = $("#alwComp").data("kendoChart");
    alwComp.dataSource.read();

    var dedComp = $("#dedComp").data("kendoChart");
    dedComp.dataSource.read();

    var incDepartment = $("#incDepartment").data("kendoChart");
    incDepartment.dataSource.read();
}
function getParamBadHires() {
    var queryString = window.location.search;
    var params = new URLSearchParams(queryString);
    var groupCom = params.get("groupCom");
    var financialYr = params.get("financialYr");
    var type = params.get("type");
    var code = params.get("code");
    return { groupCom: groupCom, financialYr: financialYr, type: type, code: code };
}
function updateBadHiresCharts() {
    var bhMonth = $("#bhMonth").data("kendoChart");
    bhMonth.dataSource.read();
    var bhAge = $("#bhAge").data("kendoChart");
    bhAge.dataSource.read();
    var bhGender = $("#bhGender").data("kendoChart");
    bhGender.dataSource.read();
    var bhDepartment = $("#bhDepartment").data("kendoChart");
    bhDepartment.dataSource.read();
    var bhLocation = $("#bhLocation").data("kendoChart");
    bhLocation.dataSource.read();
    var bhDivision = $("#bhDivision").data("kendoChart");
    bhDivision.dataSource.read();
    var bhCategory = $("#bhCategory").data("kendoChart");
    bhCategory.dataSource.read();
    var bhGrade = $("#bhGrade").data("kendoChart");
    bhGrade.dataSource.read();
    var bhRegion = $("#bhRegion").data("kendoChart");
    bhRegion.dataSource.read();
}
function getValBH() {
    var groupCom = $("#GroupCompanyCode").val();
    var financialYr = $("#finYear").val();

    return { groupCom: groupCom, financialYr: financialYr };
}

function updateTrainingCharts() {
    var yearlyTr = $("#yearlyTr").data("kendoChart");
    yearlyTr.dataSource.read();
    var perHeadTr = $("#perHeadTr").data("kendoChart");
    perHeadTr.dataSource.read();
    var empCount = $("#empCount").data("kendoChart");
    empCount.dataSource.read();
    var trCount = $("#trCount").data("kendoChart");
    trCount.dataSource.read();
}

function getTrainingVal() {
    var financialYr = $("#finYear").val();

    return { financialYr: financialYr };
}


function onClassificationDropdownChange() {
    var classification = $('#classification').val()
    var tabType = $('input[name="Type"]:checked').val();

    $.ajax({
        type: 'GET',
        url: '/MIS/MISDashBoard/GetGraph',
        dataType: 'html',
        data: { Classification: classification, Type: tabType},
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.customGraph').removeClass('hide');
                $('.customGraph').html('');
                $('.customGraph').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}



function empfilterradio(tabType) {
    debugger
    var classification = $('#classification').val();
    //var tabType = $('input[name="Type"]:checked').val();
    var tabType = tabType;
    $.ajax({
        type: 'GET',
        url: '/MIS/MISDashBoard/GetGraph',
        dataType: 'html',
        data: { Classification: classification, Type: tabType },
        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                $('.customGraph').removeClass('hide');
                $('.customGraph').html('');
                $('.customGraph').html(Data);
                $('#classification').removeClass('hide');

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error("An error occurred: " + textStatus, errorThrown);
            alert("An error occurred while processing your request.");
        }
    });
}


function onChartDropdownChange(Category) {
    debugger
    //var Category = $('#chartNew').val()
    var Category = Category;
    $.ajax({
        type: 'GET',
        url: '/MIS/MISDashBoard/GetGraphTwo',
        dataType: 'html',
        data: { Category: Category },
        success: function (Data) {
            debugger
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('.customGraph').removeClass('hide');
                $('.customGraph').html('');
                $('.customGraph').html(Data);
                $('#classification').addClass('hide');

            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
}

function onComboChange(e) {
    debugger
    var comboBox = this; // 'this' refers to the ComboBox instance
    var selectedValue = comboBox.value();
    var selectedText = comboBox.text();
    var financialYr = $("#finYear").val();
    var groupCom = $("#GroupCompanyCode").val();
    // Log the selected value and text (or implement any logic you need)
    console.log("ComboBox changed to value: " + selectedValue + " (Text: " + selectedText + ")");

    // Additional logic can go here...


    $.ajax({

        type: 'GET',
        url: '/MIS/MISDashBoard/GetAttendanceCount',
        dataType: 'json',
        data: { financialYr: financialYr, groupCom: groupCom, AttendanceCode: selectedValue },

        success: function (result) {
            console.log(result);
            debugger
            $("#TodayCount").html(result.todayCount);
            $("#TomorrowCount").html(result.tomorrowCount);
            $("#YesterdayCount").html(result.yesterdayCount);
            $("#CurrentWeakCount").html(result.currentWeakCount);
            $("#CurrentMonthCount").html(result.currentMonthCount);
            $("#PreviousMonthCount").html(result.previousMonthCount);

         

        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
  
}

