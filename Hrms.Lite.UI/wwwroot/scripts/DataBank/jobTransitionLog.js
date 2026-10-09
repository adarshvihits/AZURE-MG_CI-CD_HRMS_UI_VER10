var prevTab = 'all';
var currentTab = 0;  //code for 'all'
var currTabName = 'All';
var clickedTab = 'all';

function ontabClick(Tab, TabIndex,tabName) {
   
    debugger;
    clickedTab=Tab;
    currentTab = TabIndex;
    currTabName = tabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
 /*   fillData();*/
    ApplyFilter('apply');  
    prevTab = Tab;

}

function TabsDropdown(TabIndex) {
    debugger;
    currentTab = TabIndex;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    ApplyFilter('apply');

    prevTab = Tab;
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab_1';
    else if (TabCode == 2)
        return 'tab_2';
    else if (TabCode == 3)
        return 'tab_3';
    else if (TabCode == 4)
        return 'tab_4';
    else
        return 'more';
}

function onFilterDropdownChange(SearchId) {
    debugger;
   /* showSpinner();*/
   
    
    var search = SearchId.value;
    $('#Filter_Code').val(search);
    $('.filterDropDown').val(search);
   
     ApplyFilter('reset');
//    fillData();
}

function onExportToExcelClick() {
    debugger;
    var searchid = $('#Filter_Code').val();
    let radioVal;
    radioVal = $('input[name="Type"]:checked').val();

    //var TableLength = document.getElementById("jobTransitionLogTable").rows.length - 1;
    var TableLength = $('#JobTransitionLogList_Count').val();

    if (TableLength > 0) {
        url = "/DataBank/JobTransitionLog/ExportToExcel?TabIndex=" + currentTab + '&FromDate=' + $('#FromDate').val() + '&ToDate=' + $('#ToDate').val() + "&TransitionType=" + $('#JobTransition_JobDetail_JobTransitionsType_Code').val() + "&TabName=" + $('#JobTransition_JobDetail_JobTransitionsType_Code option:selected').text() + "&totRecords=" + TableLength + '&Type=' + radioVal;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




function fillData() {
    debugger;



    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    //var searchid = $('#Filter_Code').val();
    let radioVal;
    radioVal = $('input[name="Type"]:checked').val();
    if ($('#FromDate').val() != '' && $('#ToDate').val() != '' && $('#JobTransition_JobDetail_JobTransitionsType_Code').val() != '') {
    
    var dataTable = $("#jobTransitionLogTable").DataTable({
        //"paging": true,
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/JobTransitionLog/GetActiveList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    // var current = $('#ActiveListtable').DataTable();

                    var searchId = $('#Filter_Code').val();
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    var tabindex = $("#TabCode").val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FromDate=' + $('#FromDate').val() + '&ToDate=' + $('#ToDate').val() + '&TransitionType=' + $('#JobTransition_JobDetail_JobTransitionsType_Code').val() + '&Tabindex=' + tabindex + '&Type=' + radioVal +  '&dummy=dummy';

                    d.filter = temp;

                    // alert(d.filter);

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger;
                console.log(d);
                $('#JobTransitionLogList_Count').val(d.recordsTotal);
                if (d.data.length != 0) {
                    $('.AllTabCount').html(d.count.allTabCount);
                    $('.Tab1Count').html(d.count.tab1Count);
                    $('.Tab2Count').html(d.count.tab2Count);
                    $('.Tab3Count').html(d.count.tab3Count);
                    $('.Tab4Count').html(d.count.tab4Count);
                    $('.MoreCount').html(d.count.moreCount);
                }
                else {
                    debugger;
                    $('.alltab').html(0);
                }


                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    // alert(JSON.stringify(d));
                    d.data[i].employeeName = '<a href="/DataBank/JobTransitionLog/LOg?EmployeeGI=' + d.data[i].employeeGI + '&HistoryGI=' + d.data[i].historyGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Job Transition Log List">' + d.data[i].employeeName + '</a>'


                    d.data[i].orderDate = _GetFormattedDate(d.data[i].orderDate);//d.data[i].orderDate.split('T')[0];
                    d.data[i].effectiveDate = _GetFormattedDate(d.data[i].effectiveDate);
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                    //d.data[i].effectiveDate.split('T')[0];
                    // alert('<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].AbsoluteUrl + '"></div><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a></div>' + d.data[i].Employeeid + '</div></div>');

                    //d.data[i].employeeName = '<a class="d-flex align-items-center" href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a>'


                    //alert( jsonData.data[i].employeeName);
                }

                return d.data;
            }

        },








        "columns": [

            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "newGp" },
            { "data": "oDesignation" },
            { "data": "oDepartment" },
            { "data": "oLocation" },
            { "data": "oldGP" },


            { "data": "transitionType" },
            { "data": "orderDate" },
            { "data": "effectiveDate" },
            { "data": "entryDate" },
            { "data": "gP_DIF" },




        ],







        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "order": [0, "asc"],
        "processing": "true",
        "language": {
            processing: '<img width="100%" src="/images/Loader_img1 (7).gif">',
            emptyTable: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div id="noDataFound" style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

            sLengthMenu: "_MENU_",
            paginate: {


                next: '<img width="100%" src="/images/icons/Icon awesome-arrow-right-1.png" >',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                first: '<img src="/images/icons/Icon metro-next-1.png">',
                last: '<img src="/images/icons/Icon metro-next.png">'




            }
        }



    });



    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();
        $('#noDataFound').css("display", 'none');


    });

    $('.search-slider-inner').click(function () {
        /* dataTable.search('').draw();*/
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
    });

    dataTable.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

    });

    $.fn.DataTable.ext.pager.numbers_length = 5;



}





}



function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#Filter_Code').val();
    //var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#Filter_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/JobTransitionLog/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#jobTransition-Log-List').html("");
            //            $('#jobTransition-Log-List').html(result);
            //            $('#jobTransition-Log-List').removeClass('hide');
            //        }
            //        hideSpinner();
                         
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        $('body').html(xhr.responseText);
            //    }
            //});
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/JobTransitionLog/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#jobTransition-Log-List').html("");
            //            $('#jobTransition-Log-List').html(result);
            //            $('#jobTransition-Log-List').removeClass('hide');
            //        }
            //        hideSpinner();
                               
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        $('body').html(xhr.responseText);
            //    }
            //});
            $('#filterForm').addClass('filter-apply');
        }
      
    }
    else {
       
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
       
        //$.ajax({

        //    type: 'POST',
        //    url: '/DataBank/JobTransitionLog/ApplyFilter',
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#jobTransition-Log-List').html("");
        //            $('#jobTransition-Log-List').html(result);
        //            $('#jobTransition-Log-List').removeClass('hide');
        //        }
        //        hideSpinner();
               

        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        $('body').html(xhr.responseText);
        //    }
        //});
    }
}


function CTCCalculation() {
    debugger;
    var TotAmt = parseFloat("0");
    var CurrTotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var CurrTotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var CurrTotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    var CurrTotDedAmt = parseFloat("0");
    var TakeHome = parseFloat("0");
    var CurrTakeHome = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseFloat($("#JobTransition_FixedAllowance_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedAllowance_" + i + "__CurrentAmount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
            CurrAmt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;
        CurrTotAmt = CurrTotAmt + CurrAmt;

        TotGrossAmt = TotGrossAmt + Amt;
        CurrTotGrossAmt = CurrTotGrossAmt + CurrAmt;

        $("#NewGrossSal").html(TotGrossAmt);
        $("#CurrGrossSal").html(CurrTotGrossAmt);
        $("#JobTransition_JobDetail_CurrentGrossSalary").val(CurrTotGrossAmt);
        $("#JobTransition_JobDetail_NewGrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {
        var Amt = parseFloat($("#JobTransition_FixedProvision_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedProvision_" + i + "__CurrentAmount").val());

        debugger;
        if (isNaN(Amt) || Amt == 'undefined') {
            Amt = parseFloat("0");
            CurrAmt = parseFloat("0");
        }
        TotCTCAmt = TotCTCAmt + Amt;
        CurrTotCTCAmt = CurrTotCTCAmt + CurrAmt;
        debugger

    });
    $("#NewCTC").html(TotCTCAmt + TotGrossAmt);
    $("#CurrCTC").html(CurrTotCTCAmt + CurrTotGrossAmt);
    $("#JobTransition_JobDetail_CurrentCTC").val(CurrTotCTCAmt + CurrTotGrossAmt);
    $("#JobTransition_JobDetail_NewCTC").val(TotCTCAmt + TotGrossAmt);

    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#JobTransition_FixedDeduction_" + i + "__NewAmount").val());
        var CurrAmt = parseFloat($("#JobTransition_FixedDeduction_" + i + "__CurrentAmount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;
        CurrTotDedAmt = CurrTotDedAmt + CurrAmt;

        //$("#totDeductions").html(TotDedAmt);
        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home
    CurrTakeHome = CurrTotGrossAmt - CurrTotDedAmt;//Take Home

    $("#CurrTakeHome").html(CurrTakeHome);
    $("#NewTakeHome").html(TakeHome);
    //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

}
