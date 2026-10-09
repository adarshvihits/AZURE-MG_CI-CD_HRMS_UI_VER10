var planningPeriod;
var CombnameIndex = '';
var CombnameSummary;
var monthNames = new Array("Jan", "Feb", "Mar",
    "Apr", "May", "Jun", "Jul", "Aug", "Sep",
    "Oct", "Nov", "Dec");
function GetFormattedDate(date)
{
     debugger
 

    var formattedDate = new Date(date);
    var cDate = formattedDate.getDate();
    var cMonth = formattedDate.getMonth();
    var cYear = formattedDate.getFullYear();
    if (cDate < 10)
        cDate = '0' + cDate;
    formattedDate = (cDate + "-" + monthNames[cMonth] + "-" + cYear);
    return formattedDate;

}


function onHRPlanningNameChange(sender,formname)
{
    var label = '';
    debugger;
    $.ajaxSetup({
        async: false
    });
  
    var code = $(sender).val();
    if (formname == 'create') {
        
        $.getJSON("/DataBank/HRPlanning/GetPlanningDetails", { HRPlanningCode: code }, function (data) {
           
            $('.combination1Label').html(data.combination1Label);
            label = data.combination2Label;
           
            if (code != '') {
                $('.fromDate').html(GetFormattedDate(data.fromDate));
                $('.toDate').html(GetFormattedDate(data.toDate));
            }
            else {
                $('.fromDate').html('');
                $('.toDate').html('');
            }

        });

        $.getJSON("/DataBank/HRPlanning/GetHRPlanningAddNewCombination1DropDown", { HRPlanningCode: code }, function (data) {
            $('#Combination1_Code option').remove();
     
                $('#Combination1_Code').append('<option value> --Select--' + '</option');
         
            for (var i = 0; i < data.length; i++) {
                $('#Combination1_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });
        onHRPlanningCombination1Change('#Combination1_Code', 'slider_plan');
        $('.create_Combination2Label').html(label);
    }
    else if (formname == 'index') {
       
        $.getJSON("/DataBank/HRPlanning/GetPlanningDetails", { HRPlanningCode: code }, function (data) {
            $('.combination1Name').html(data.combination1Label);
            label = data.combination2Label;
        
            planningPeriod = GetFormattedDate(data.fromDate) + " To " + GetFormattedDate(data.toDate);
            $('.planningPeriod').html(planningPeriod);

        });
        $.getJSON("/DataBank/HRPlanning/GetHRPlanningCombination1DropDown", { HRPlanningCode: code }, function (data) {
            debugger
            console.log(data);
            $('#Combination1Main_Code option').remove();
            $('#Combination1Main_Code').append('<option value> --Select--'+'</option');
         
            for (var i = 0; i < data.length; i++) {
                $('#Combination1Main_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });
        onCombination1MainChange('#Combination1Main_Code');
        $('.main_Combination2Label').html(label);
    }
    else  {

        $.getJSON("/DataBank/HRPlanning/GetPlanningDetails", { HRPlanningCode: code }, function (data) {
            $('.combination1NameSummary').html(data.combination1Label);         
            planningPeriod = GetFormattedDate(data.fromDate) + " To " + GetFormattedDate(data.toDate);
            $('.planningPeriodSummary').html(planningPeriod);

        });
        $.getJSON("/DataBank/HRPlanning/GetHRPlanningCombination1DropDown", { HRPlanningCode: code }, function (data) {

            $('#Combination1Main_Code option').remove();
            $('#Combination1Main_Code').append('<option value>' + '--Select--'+'</option');
            debugger;
            for (var i = 0; i < data.length; i++) {
                $('#Combination1Main_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }

        });
       
    }
}
function onHRPlanningCombination1Change(sender,planingClasName)
{
    debugger;
    var Combination1code = $(sender).val();
    var label2 = $('.create_Combination2Label').text();
    var HRPlanningCode = $('.' + planingClasName).val();

    //var url = "/DataBank/HRPlanning/FillHRPlanningCombination2Grid"
    //$('#combination2PlanningGrid').load(url, { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code},function () { });
    //$('.create_Combination2Label').html(label2);
    $.ajax({

        type: 'GET',
        url: '/DataBank/HRPlanning/FillHRPlanningCombination2Grid',
        dataType: 'html',
        data: { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#combination2PlanningGrid').html("");
                $('#combination2PlanningGrid').html(result);
                $('.create_Combination2Label').html(label2);
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

function showMonthCalender(sender) {
    debugger

    if ($(sender).val() == 0) {

        $('.monthCalender').addClass('hide');
        //onBasedOnChange(sender);
        GetList();
    }
    else
    {
        var toDate = new Date();
        var month = toDate.getMonth();
        var Year = toDate.getFullYear();
        $('#PreviousMonth').val(monthNames[month]+ '-' + Year);
        $('.monthCalender').removeClass('hide');
        
    }
       
}
function onCombination1MainChange(sender) {
    debugger;
    $.ajaxSetup({
        async: true
    });
    showSpinner();
    var Combination1code = $(sender).val();
    var BasedOn = $('input[name="BasedOn"]:checked').val();

   
     var monthCode = $('#PreviousMonth').val();
    var HRPlanningCode = $('#HRPlanningNameMain_Code').val();



    $.ajax({
        type: 'POST',
        url: '/DataBank/HRPlanning/GetHRPlanningList?HRPlanningCode=' + HRPlanningCode + '&Combination1TrnCode=' + Combination1code + '&BasedOn=' + BasedOn + '&monthCode=' + monthCode,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#hRplanningMainGrid').html("");
                $('#hRplanningMainGrid').html(result);
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


function GetList() {

    showSpinner();
    var Combination1code = $('#Combination1Main_Code').val();
    var BasedOn = $('input[name="BasedOn"]:checked').val();


    var monthCode = $('#PreviousMonth').val();
    var HRPlanningCode = $('#HRPlanningNameMain_Code').val();

    var c1 = $('.combination1Name').html();
    var c2 = $('.main_Combination2Label').html();
    $('#Basic_Filter_' + c1 + '_Code').attr('disabled', false);
    $('#Optional_Filter_' + c1 + '_Code').attr('disabled', false);
    $('#Basic_Filter_' + c2 + '_Code').attr('disabled', false);
    $('#Optional_Filter_' + c2 + '_Code').attr('disabled', false);

    $.ajax({
        type: 'POST',
        url: '/DataBank/HRPlanning/GetHRPlanningList?HRPlanningCode=' + HRPlanningCode + '&Combination1TrnCode=' + Combination1code + '&BasedOn=' + BasedOn + '&monthCode=' + monthCode,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#hRplanningMainGrid').html("");
                $('#hRplanningMainGrid').html(result);
                var c1 = $('.combination1Name').html();
                var c2 = $('.main_Combination2Label').html();
                $('#Basic_Filter_' + c1 + '_Code').attr('disabled', true);
                $('#Optional_Filter_' + c1 + '_Code').attr('disabled', true);
                $('#Basic_Filter_' + c2 + '_Code').attr('disabled', true);
                $('#Optional_Filter_' + c2 + '_Code').attr('disabled', true); 
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
            $('#filterForm').removeClass('filter-apply');
            CloseSlider('basicFilter');
        }
        else {
            showSpinner();
            GetList();
            $('#filterForm').addClass('filter-apply');
            CloseSlider('basicFilter');
        }

    }
    else {
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        GetList();
    }
}


function onBasedOnChange(sender) {
    debugger;
    $.ajaxSetup({
        async: true
    });
    showSpinner();
    var BasedOn= $('input[name="BasedOn"]:checked').val();
    var monthCode = $('#PreviousMonth').val();
   
    

    var Combination1code = $('#Combination1Main_Code').val();
    var HRPlanningCode = $('#HRPlanningNameMain_Code').val();
    var RegionCode = $('#Region_Code').val();
    var StateCode = $('#State_Code').val();

    $.ajax({

        type: 'GET',
        url: '/DataBank/HRPlanning/GetHRPlanningList',
        dataType: 'html',
        data: { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code, BasedOn: BasedOn, monthCode: monthCode, RegionCode: RegionCode, StateCode: StateCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#hRplanningMainGrid').html("");
                $('#hRplanningMainGrid').html(result);
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

function onPreviousMonthChange(sender) {
    debugger;
    $.ajaxSetup({
        async: true
    });
    showSpinner();
    var monthCode = $(sender).val();
    var BasedOn = $('input[name="BasedOn"]:checked').val();

   var Combination1code = $('#Combination1Main_Code').val();
   /* var Combination1code = $('#DesignationPlanningCombo').val();*/
    
   
    var HRPlanningCode = $('#HRPlanningNameMain_Code').val();

  /*  var RegionCode = $('#region-code').val();*/
   var RegionCode = $('#Region_Code').val();
    var StateCode = $('#State_Code').val();

    $.ajax({

        type: 'GET',
        url: '/DataBank/HRPlanning/GetHRPlanningList',
        dataType: 'html',
        data: { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code, BasedOn: BasedOn, monthCode: monthCode, RegionCode: RegionCode, StateCode: StateCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#hRplanningMainGrid').html("");
                $('#hRplanningMainGrid').html(result);
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





function onCombination2Click(hrPlanningCode, combination1code, combination2code, mode) {
  
 
    $.ajax({

        type: 'GET',
        url: '/DataBank/HRPlanning/GetHRPlanningDetailsForModify',
        dataType: 'html',
        data: { HRPlanningCode: hrPlanningCode, Combination1TrnCode: combination1code, Combination2TrnCode: combination2code, Mode: mode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                if (mode == 'edit') {
                    $('#edit').html("");
                    $('#edit').html(result);
                }
                else {
                    $('#delete').html("");
                    $('#delete').html(result);
                }
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

function onAddPlanningClick()
{
    debugger;
    var url = "/DataBank/HRPlanning/Create"
    $('#create').load(url, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/HRPlanning/Create',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#create').html("");
                $('#create').html(result);
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

function SaveHRPlanning() {
    debugger;
    $.ajaxSetup({
        async: false
    });
    if (validateHRPlanning()) {
        var combnName = $("#Combination1Main_Code option:selected").text();
        var monthCode = $('#PreviousMonth').val();
        var BasedOn = $('input[name="BasedOn"]:checked').val();

        var Combination1code = $('#Combination1Main_Code').val();
        var HRPlanningCode = $('#HRPlanningNameMain_Code').val();

        var RegionCode = $('#Region_Code').val();
        var StateCode = $('#State_Code').val();

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
                    url: '/DataBank/HRPlanning/Save',
                    dataType: 'json',
                    data: $('#HRPlanning-Save').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {

                                CloseSlider('create');
                                var url = "/DataBank/HRPlanning/GetHRPlanningList";
                                $('#hRplanningMainGrid').load(url, { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code, BasedOn: BasedOn, monthCode: monthCode, RegionCode: RegionCode, StateCode: StateCode}, function () { });

                                $.getJSON("/DataBank/HRPlanning/GetHRPlanningCombination1DropDown", { HRPlanningCode: HRPlanningCode }, function (data) {

                                    console.log(data);
                                    $('#Combination1Main_Code option').remove();
                                    $('#Combination1Main_Code').append('<option value>' + combnName + '</option');

                                    for (var i = 0; i < data.length; i++) {
                                        $('#Combination1Main_Code').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
                                    }

                                });

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
        });
        

    }


}
function onGraphClick()
{
    debugger;
    var url = '/DataBank/HRPlanning/PlanningSummary?HRPlanningCode=' + $('#HRPlanningNameMain_Code').val() + "&StateCode="+ $('#State_Code').val() + '&RegionCode=' + $('#Region_Code').val();
    window.location.href = url;
}
  
function onChartMOdeChange(sender) {
    if (validateById("#Combination1Main_Code", 'chart_combn1')) {
        $.ajaxSetup({
            async: true
        });
        showSpinner();
        var mode = $("#BudgetOrManpower :selected").text();
        debugger
        var Combination1code = $('#Combination1Main_Code').val();
        var HRPlanningCode = $('#HRPlanningNameMain_Code').val();
        var url = '/DataBank/HRPlanning/GetHRplanningSummaryChart?HRPlanningCode=' + HRPlanningCode + '&Combination1TrnCode=' + Combination1code + "&Mode=" + mode;//+ "" + "+";
        $('.planning-list-chart').load(url, function () { hideSpinner(); });
        /*   window.location.href = url;*/
    }
    else
    {
        $('.planning-list-chart').html('');
    }
 
   
}
function onExportToExcelClick() {
    debugger;
    var TableLength = $("#HRPlanningList_Count").val();
    if (TableLength != "") {
        var BasedOn = $('input[name="BasedOn"]:checked').val();
        var monthCode = $('#PreviousMonth').val();
        var Combination1code = $('#Combination1Main_Code').val();
  
        var hrplanningName = $("#HRPlanningNameMain_Code option:selected").text();
        var HRPlanningCode = $('#HRPlanningNameMain_Code').val();
        var combn1Name = $("#Combination1Main_Code option:selected").text();
      
        var RegionCode = $('#Region_Code').val();
        var StateCode = $('#State_Code').val();
        url = '/DataBank/HRPlanning/ExportToExcel?HRPlanningCode=' + HRPlanningCode + '&Combination1TrnCode=' + Combination1code + '&combn1Name=' + combn1Name + '&BasedOn=' + BasedOn + '&monthCode=' + monthCode + '&RegionCode=' + RegionCode + '&StateCode=' + StateCode + '&HrplanningName=' + hrplanningName;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ModifyHRPlanning() {
    debugger
    var zeroflag = true;
    var __Budget = parseInt('0');
    var __ManPower = parseInt('0');
    __Budget = parseInt($("#HRPlanningSettingsList_0__Budget").val());
    __ManPower = parseFloat($("#HRPlanningSettingsList_0__ManPower").val());
    if (isNaN(__Budget) || isNaN(__ManPower)) {
        $("#HRPlanningSettingsList_0__Budget").val(0);
        $("#HRPlanningSettingsList_0__ManPower").val(0);
        __Budget = 0;
        __ManPower = 0;
    }
    if (__Budget != 0 || __ManPower != 0) {
        zeroflag = false;

    }
    if (zeroflag == true) {
        swal.fire("Please Set Manpower or Budget");
    }
    else {
        var monthCode = $('#PreviousMonth').val();
        var BasedOn = $('input[name="BasedOn"]:checked').val();

        var Combination1code = $('#Combination1Main_Code').val();
        var HRPlanningCode = $('#HRPlanningNameMain_Code').val();

        var RegionCode = $('#Region_Code').val();
        var StateCode = $('#State_Code').val();

        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/HRPlanning/Save',
                    dataType: 'json',
                    data: $('#Modify-HRPlanning').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                CloseSlider('edit');
                                var url = "/DataBank/HRPlanning/GetHRPlanningList"
                                $('#hRplanningMainGrid').load(url, { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code, BasedOn: BasedOn, monthCode: monthCode, RegionCode: RegionCode, StateCode: StateCode }, function () { });

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
        });

    }
   

}
function DeleteHRPlanningClick() {
    var monthCode = $('#PreviousMonth').val();
    var BasedOn = $('input[name="BasedOn"]:checked').val();

    var Combination1code = $('#Combination1Main_Code').val();
    var HRPlanningCode = $('#HRPlanningNameMain_Code').val();

    var RegionCode = $('#Region_Code').val();
    var StateCode = $('#State_Code').val();

    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/HRPlanning/Delete',
                dataType: 'json',
                data: $('#Delete-HRPlanning').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Deleted Successfully").then(function () {
                            CloseSlider('delete');
                            //var url = "/DataBank/HRPlanning/GetHRPlanningList"
                            //$('#hRplanningMainGrid').load(url, { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code, BasedOn: BasedOn, monthCode: monthCode, RegionCode: RegionCode, StateCode: StateCode }, function () { });
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/HRPlanning/GetHRPlanningList',
                                dataType: 'html',
                                data: { HRPlanningCode: HRPlanningCode, Combination1TrnCode: Combination1code, BasedOn: BasedOn, monthCode: monthCode, RegionCode: RegionCode, StateCode: StateCode },
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }

                                    else {
                                        $('#hRplanningMainGrid').html("");
                                        $('#hRplanningMainGrid').html(result);
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
                            debugger
                            if ($("#ListCount").val() == 0)
                                $("#Combination1Main_Code option[value=" + $('#Combination1Main_Code').val() + "]").remove();
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
    });

    
}


function validateHRPlanning() {
    $.ajaxSetup({
        async: false
    });
    var i = 0;
    var zeroflag = true;
    var __Budget = parseInt('0');
    var __ManPower = parseInt('0');
    if (!validateById('#HRPlanningName_Code', 'Name')) {
            i++;
        }
    if (!validateById('#Combination1_Code', 'comb1')) {
            i++;
        }
    if (i == 0) {
        $('.plan_ifEmpty').each(function (i, obj) {

            __Budget = parseInt($("#HRPlanningSettingsList_" + i + "__Budget").val());
            __ManPower = parseFloat($("#HRPlanningSettingsList_" + i + "__ManPower").val());
            if (isNaN(__Budget) || isNaN(__ManPower)) {
                $("#HRPlanningSettingsList_" + i + "__Budget").val(0);
                $("#HRPlanningSettingsList_" + i + "__ManPower").val(0);
                __Budget = 0;
                __ManPower = 0;
            }
            if (__Budget != 0 || __ManPower != 0) {
                zeroflag = false;
                             
            }
           
        });
        debugger
        if (zeroflag == true) {
            swal.fire("Please Set Manpower or Budget");
            return false;
        }
        else
            return true;
    }
    else
    return false;          
    }




function OnNoticePerdEmpClick(HRplanCode, Combination1Code, Combination2Code) {
    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/HRPlanning/GetEmployeeDetails?HRplanCode=" + HRplanCode + "&Combination1Code=" + Combination1Code + "&Combination2Code=" + Combination2Code,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
            }
           


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}