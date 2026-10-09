var prevTab = 'ActiveTab';
var currentTab = 0;
var Tab_Name = 'All';
var dropdown = 0;
var salVisible;
let debounceTimer;

function tabClick(Tab, TabIndex, TabName) {
    $.ajaxSetup({ async: false });
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
function fillData() {
    debugger
    //$.ajaxSetup({ async: false });
   /* $.ajaxSetup({ async: false });*/
    var filter1 = $('#AdvancedFilter').serialize();

    var temp = filter1;


    if (currentTab == 0) {

        $("#ActiveListDiv").removeClass('hide');
        $("#ResignedDiv").addClass('hide');
        var dataTable = $("#ActiveListtable").DataTable({
            //"paging": true,
            "destroy": true,
            "pagingType": 'full_numbers',
            "ajax": {
                "url": "/Databank/EmployeeProfile/GetActiveList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + currentTab + '&dummy=dummy';

                        d.filter = temp;

                        // alert(d.filter);

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    if (d.data == "") {
                        $('.ActiveCount').html(0);
                        $('.ResignedCount').html(0);
                    }
                    else {
                        $('.ActiveCount').html(d.data[0].active);
                        $('.ResignedCount').html(d.data[0].resigned);
                    }
                    // console.log(d.recordsTotal);

                    $('#EmployeeProfileList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].dob = _GetFormattedDate(d.data[i].dob);
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        
                        if (d.data[i].fileName != null && d.data[i].fileName !="" )
                            d.data[i].employeeName = `<div class="d-flex align-items-center" ><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=${d.data[i].employeeGI}&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">${d.data[i].employeeName}</a></div>${d.data[i].employeeid}</div></div>`;

                            else
                            d.data[i].employeeName = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a></div>' + d.data[i].employeeid + '</div></div>'

                    }
                    console.log(d.data);
                    return d.data;
                }

            },

            "columns": [              
                { "data": "employeeName", "width": "16%" },
                { "data": "grade", "width": "10%" },
                { "data": "designation", "width": "10%" },
                { "data": "department", "width": "11%" },
                { "data": "location", "width": "10%" },
                { "data": "category", "width": "10%" },
                { "data": "dob", "width": "7%" },
                { "data": "age", "width": "4%" },
                { "data": "doj", "width": "6%" },
                { "data": "yos", "width": "4%" },
                { "data": "grossPay", "width": "6%" },
                { "data": "ctc", "width": "6%" },

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
                //   emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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



        debugger
        if (salVisible == 0) {
            let column9 = dataTable.column(9);        
            column9.visible(false);
            let column10 = dataTable.column(10);
            column10.visible(false);
        }
    }
    else {

        $("#ActiveListDiv").addClass('hide');
        $("#ResignedDiv").removeClass('hide');
        var dataTable = $("#Resignedtable").DataTable({
            //"paging": true,
            "destroy": true,
            "pagingType": 'full_numbers',
            "ajax": {
                "url": "/Databank/EmployeeProfile/GetActiveList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';






                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + currentTab + '&dummy=dummy';

                        d.filter = temp;

                        // alert(d.filter);

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    debugger;
                    if (d.data == "") {
                        $('.ActiveCount').html(0);
                        $('.ResignedCount').html(0);
                    }
                    else {
                        $('.ActiveCount').html(d.data[0].active);
                        $('.ResignedCount').html(d.data[0].resigned);
                    }
                    // console.log(d.recordsTotal);

                    $('#EmployeeProfileList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        debugger;
                        d.data[i].dob = _GetFormattedDate(d.data[i].dob);
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);

                        if (d.data[i].fileName != null && d.data[i].fileName != "")
                            d.data[i].employeeName = `<div class="d-flex align-items-center" ><div class="img-report"><img src="/AccessFile/GetEmpThumbnail?EmployeeGI=${d.data[i].employeeGI}"></div><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=${d.data[i].employeeGI}&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">${d.data[i].employeeName}</a></div>${d.data[i].employeeid}</div></div>`;

                        else
                            d.data[i].employeeName = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div ><div class="name-post" ><div><a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a></div>' + d.data[i].employeeid + '</div></div>'

                    }

                    return d.data;
                }

            },








            "columns": [

                { "data": "employeeName", "width": "12%" },
                { "data": "designation", "width": "14%" },
                { "data": "department", "width": "9%" },
                { "data": "location", "width": "11%" },
                { "data": "category", "width": "10%" },
                { "data": "dob", "width": "8%" },
                { "data": "age", "width": "6%" },
                { "data": "doj", "width": "9%" },
                { "data": "lwd", "width": "10%" },
                { "data": "yos", "width": "6%" },
                { "data": "grossPay", "width": "6%" },
                { "data": "ctc", "width": "6%" },
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
                //   emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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
        if (salVisible == 0) {
            let column10 = dataTable.column(10);
            column10.visible(false);
            let column11 = dataTable.column(11);
            column11.visible(false);
            
        }
    }


    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");


    //$("#dataTableSearch").on("keyup", function () {
        
    //    dataTable.search(this.value).draw();
    //    $('#noDataFound').css("display", 'none');
    //});
    $("#dataTableSearch").on("keyup", function () {

        clearTimeout(debounceTimer); // clear previous timer
        debounceTimer = setTimeout(() => {
            dataTable.search(this.value).draw();
            $('#noDataFound').css("display", 'none'); // call your search function
        }, 700);

    });
    $('.search-slider-inner').click(function () {
        dataTable.search('').draw();
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


    $.getJSON("/DataBank/EmployeeProfile/GetTabCount", {}, function (data) {
      
        
        if (data.length != 0) {
          
            $('.ActiveCount').html(data.activeCount);
            $('.ResignedCount').html(data.resignedCount);

        }
        else {
          
            $('.ActiveCount').html(0);
        }


    });
}
function GetTab(TabCode) {
    if (TabCode == 0)
        return 'ActiveTab'   
    else
        return 'ResignedTab';

}

function OnAttendanceFinancialYearSelect(sender) {
   
    FinancialYear = $(sender).val();
    //FinancialYearName = $('#FinancialYear_Name').val();
    EmployeeGI = $("#EmployeeHeader_EmployeeGuid").val();
    var url = "/DataBank/EmployeeProfile/GetEmployeeSalAttDetails";

    $('#AttendanceDiv').load(url, { FinancialYear: FinancialYear, EmployeeGI: EmployeeGI,Tab:'Attendance' }, function () { });
   
}

function OnSalaryFinancialYearSelect(sender) {

    FinancialYear = $(sender).val();
    //FinancialYearName = $('#FinancialYear_Name').val();
    EmployeeGI = $("#EmployeeHeader_EmployeeGuid").val();
    var url = "/DataBank/EmployeeProfile/GetEmployeeSalAttDetails";

    $('#SalaryDiv').load(url, { FinancialYear: FinancialYear, EmployeeGI: EmployeeGI,Tab:'Salary' }, function () { });

}

function validateFields() {
    debugger;
    var i = 0;
    var YOS = $('#Advance_Filter_YrsOfService').val();
    var AGE = $('#Advance_Filter_AgeAboveBelow').val();

  
    if (YOS == "") {
        i = 0;
    }
    if (AGE == "") {
        i = 0;
    }
    if (YOS == 3) {
        if (!validateById('#Advance_Filter_YrsOfExperience', 'yosfrom')) {
            i++;
        }
        if (!validateById('#Advance_Filter_YSTo', 'yrexto')) {
            i++;
        }
    }
    if (AGE == 3) {
        if (!validateById('#Advance_Filter_Age', 'agefrom')) {
            i++;
        }
        if (!validateById('#Advance_Filter_AgeTo', 'ageto')) {
            i++;
        }
    }
    if (YOS != "" && YOS != 3) {
        if (!validateById('#Advance_Filter_YrsOfExperience', 'yosfrom')) {
            i++;
        }
    }
    if (AGE != "" && AGE != 3) {
        if (!validateById('#Advance_Filter_Age', 'agefrom')) {
            i++;
        }
    }
    
      

    if (i == 0)
        return true;
    else
        return false;
}
function ApplyFilter(mode) {
    debugger
    
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    //$("#SearchId").val($('#DurationwiseFilterSlab_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            if (validateFields()) {
                fillData();
                debugger;
                CloseSlider('basicFilter');
                $('#filterForm').removeClass('filter-apply');
            }
        }
        else {
            fillData();
            debugger;
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        debugger
        $('#Advance_Filter_AgeAboveOrBelow').val('');
        $('#Advance_Filter_YSAboveOrBelow').val('');
        $('.reset').val('');
        $('.reset').prop('checked', false);
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        
        fillData();

    }
    
}
function ExportToExcel() {
   
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
        var tableLength = $('#EmployeeProfileList_Count').val();
        if (tableLength > 0) {
            url = "/DataBank/EmployeeProfile/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + "&TotalRows=" + tableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
   /* }*/
}

function summaryExportToExcel() {
   
    var gbyid = $('#GroupByFill_Code').val();
    var gbyname =$('#GroupByFill_Code option:selected').text();
    url = "/DataBank/EmployeeProfile/SummaryExportToExcel?groupbyid=" + gbyid + "&groupbyname=" + gbyname;
    window.location.href = url;
}
function groupbyChange(sender) {
   
    var ddid = $(sender).val();
    var gname = $('#GroupByFill_Code option:selected').text();
    var url = "/DataBank/EmployeeProfile/GroupByBasedSummary";
    $('#Smrylist').load(url, { id: ddid, groupbyname:gname}, function () { });
}
function groupbyChangeval(ddid) {
 
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
function GetEmpProfilePrint(EmployeeCode) {


    var url = "/DataBank/EmployeeProfile/GetEmpProfilePrint?EmployeeCode=" + EmployeeCode;

    //console.log(url);
    window.location.href = url;
    return true
}

var prevsTab = 'ActiveData';
function OnCertificateTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    if (prevsTab) {
        $("." + prevsTab).removeClass("active");
    }
 


    $("." + prevsTab).removeClass("active");
    $("." + Tab).addClass("active");


    $.ajax({

        type: 'GET',
        url: '/DataBank/EmployeeProfile/GetCertificationsList',
        dataType: 'html',
        data: { EmployeeGI: $("#EmployeeHeader_EmployeeGuid").val(), TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#CertificationListID').html("");
                $('#CertificationListID').html(result);
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

    prevsTab = Tab;
}



function EmpProfileDocView(Code, DocType) {
   
    var url = "/DataBank/EmployeeProfile/EmpProfileDocView?Code=" + Code + "&DocType=" + DocType;
    $.ajax({
        url: url,
        type: "GET",
        success: function () {
            // File exists → open in new tab
            window.open(url, "_blank");
        },
        error: function (xhr) {
            let response = JSON.parse(xhr.responseText);

            Swal.fire({

                text: response.message  // localized message
            });
        }
    });
}

//------------------CERTIFICTE DOCUMENT VIEW-------------------
function GetCertificateDocPopUp(LogCode) {


   

    $.ajax({
        type: 'GET',
        url: "/DataBank/EmployeeProfile/GetPopUpforCertiDocView",
        dataType: 'html',
        data: { LogCode: LogCode },
        success: function (Data) {

            var m = $('.bd-Certificte-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            $('#popUpName').text('Uploaded Documents for Certifictions');


        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });
}





