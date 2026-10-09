var prevTab = 'ActiveTab';
var currentTab = 0;
var Tab_Name = 'All';
var dropdown = 0;
function tabClick(Tab, TabIndex, TabName) {
    $.ajaxSetup({ async: false });
    debugger;


    currentTab = TabIndex;
    currentTabName = TabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/EmployeeProfile/GetEmployeeProfileList";
    //$('#activeListPartial').load(url, { TabIndex: TabIndex }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;
}

function TabsDropdown(TabIndex, TabName) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    currentTabName = TabName;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/EmployeeProfile/GetEmployeeProfileList";
    //$('#activeListPartial').load(url, { TabIndex: TabIndex }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;
}


function employeeParam() {



    var formArray = $("#AdvancedFilter").serializeArray();

    var tabIndex = 0;
    var searchValue = $('#dataTableSearch').val();


    var temp = {};
    for (var i = 0; i < formArray.length; i++) {
        temp[formArray[i]['name']] = formArray[i]['value']; // Map the current key-value pairs into the filter variable object.
    }

    temp["TabIndex"] = tabIndex;//$("#F_To").val();
    temp["searchValue"] = searchValue;

    return { filter1: temp };
}



function GetTab(TabCode) {
    if (TabCode == 0)
        return 'ActiveTab'
    else
        return 'ResignedTab';

}

function OnFinancialYearSelect() {
    debugger;
    FinancialYear = $('#FinancialYear_Code').val();
    FinancialYearName = $('#FinancialYear_Name').val();
    EmployeeGI = $("#EmployeeHeader_EmployeeGuid").val();;
    var url = "/DataBank/EmployeeProfile/GetEmployeeSalAttDetails";

    $('#tabSeven').load(url, { FinancialYear: FinancialYear, EmployeeGI: EmployeeGI }, function () { });

}


function ExportToExcel() {
    debugger;
    /*if (validateDropdowns()) {*/
    if (currentTab == 0) {
        var currentTabName = "Active";
        /* var ActiveListtableLength = document.getElementById("ActiveListtable").rows.length - 1;*/
        var ActiveListtableLength = $('#EmployeeProfileList_Count').val();
        if (ActiveListtableLength > 0) {
            url = "/DataBank/EmployeeProfile/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + "&TotalRows=" + ActiveListtableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 1) {
        var currentTabName = "Resigned";
        var ResignedListTableLength = document.getElementById("ActiveListtable").rows.length - 1;
        if (ResignedListTableLength > 0) {
            url = "/DataBank/EmployeeProfile/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    /* }*/
}

function summaryExportToExcel() {
    debugger;
    var gbyid = $('#GroupByFill_Code').val();
    var gbyname = $('#GroupByFill_Code option:selected').text();
    url = "/DataBank/EmployeeProfile/SummaryExportToExcel?groupbyid=" + gbyid + "&groupbyname=" + gbyname;
    window.location.href = url;
}
function groupbyChange(sender) {
    debugger;
    var ddid = $(sender).val();
    var gname = $('#GroupByFill_Code option:selected').text();
    var url = "/DataBank/EmployeeProfile/GroupByBasedSummary";
    $('#Smrylist').load(url, { id: ddid, groupbyname: gname }, function () { });
}
function groupbyChangeval(ddid) {
    debugger;
    var url = "/DataBank/EmployeeProfile/GroupByBasedSummary";
    $('#Smrylist').load(url, { id: ddid }, function () { });
}
function ViewFile(absoluteURI) {
    //window.open(absoluteURI, "_blank");

    if (absoluteURI == "") {
        swal.fire("No file Uploaded")
    }
    else {
        window.open(absoluteURI);
    }
}
