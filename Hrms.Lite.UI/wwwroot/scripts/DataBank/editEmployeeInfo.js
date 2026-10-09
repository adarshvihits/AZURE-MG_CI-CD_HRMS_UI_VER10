var prevTab = 'tab1';
var currentTab = 0;



function fillData() {

    debugger;

    //var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    var tabIndex = currentTab;
    $("#SearchId").val($('#FilterCombo_Code').val())
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    if (tabIndex == 0) {
        debugger

        $("#editEmployeeList").removeClass('hide');
        $("#editEmployeePendingList").addClass('hide');
        var dataTable = $("#editSummaryTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EditEmployeeInfo/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&tabIndex=' + tabIndex + '&searchID=' + search + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {

                    debugger
                    $('.empActiveCount').html(d.activecount);
                    $('.empPendingTab').html(d.pendingcount);

                    $('#EditInfoList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].expectedDOJ = _GetFormattedDate(d.data[i].expectedDOJ);
                        d.data[i].editDate = _GetFormattedDate(d.data[i].editDate);
                        //d.data[i].employeename = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                        d.data[i].fullName = '<td data-label="Candidate Name" ><a onclick="onEmployeeClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].logGI + "'" + ')">' + d.data[i].fullName + '</a></td>'





                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "fullName", "width": "16%" },
                { "data": "designation", "width": "17%" },
                { "data": "department", "width": "17%" },
                { "data": "location", "width": "15%" },
                { "data": "expectedDOJ", "width": "13%" },
                { "data": "editDate", "width": "12%" },



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
    }

    else if (tabIndex == 1) {
        $("#editEmployeeList").addClass('hide');
        $("#editEmployeePendingList").removeClass('hide');
        var dataTable = $("#pendingTableID").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EditEmployeeInfo/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&tabIndex=' + tabIndex + '&searchID=' + search + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {


                    debugger
                    $('.empActiveCount').html(d.activecount);
                    $('.empPendingTab').html(d.pendingcount);

                    debugger
                    $('#MakerChecker').val();
                  /*  $('#EditInfoList_' + d.data[i] + '__MakerChecker').val()*/
                    $('#EditInfoList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].expectedDOJ = _GetFormattedDate(d.data[i].expectedDOJ);
                        d.data[i].editDate = _GetFormattedDate(d.data[i].editDate);

                        //d.data[i].employeename = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <div class="name-post"><div><a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + ' </a ></div><div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'
                        //d.data[i].fullName = '<td data-label="Candidate Name" ><a onclick="onEmployeeClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].logGI + "'" + ')">' + d.data[i].fullName + '</a></td>'

                        d.data[i].fullName = '<td data-label="Candidate Name" ><a onclick="onApproveClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].logGI + "'" + ',' + "'" + i + "'" + ',' + "'" + d.data[i].makerChecker + "'" + ')">' + d.data[i].fullName + '</a></td>'




                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "fullName", "width": "16%" },
                { "data": "designation", "width": "17%" },
                { "data": "department", "width": "17%" },
                { "data": "location", "width": "15%" },
                { "data": "expectedDOJ", "width": "13%" },
                { "data": "editDate", "width": "12%" },



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


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();

        $('#noDataFound').css("display", 'none');
    });

    $('.search-slider-inner').click(function () {

        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
        dataTable.search('').draw();
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

function GetEmployeeDropdown() {
    var Type = $('input[name="Type"]:checked').val();

    $.getJSON("/DataBank/EditEmployeeInfo/GetEmployeeDropdown", { Type: Type }, function (data) {
        debugger;
        $('#EmployeeId option').remove();
        $('#EmployeeId').append('<option value="">Search by Employee name/ID</option');

        for (var i = 0; i < data.length; i++) {
            $('#EmployeeId').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
        }
        $('#EmployeeId').val("");
        $("#EmployeeId").find("option").eq(0).hide();
        $('#EmployeeId').selectpicker('refresh');
       
    });
}

function tabClick(Tab, TabIndex, TabName) {
    debugger;
    /*showSpinner();*/
    currentTab = TabIndex;
    currTabName = TabName;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

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
        return 'tab1'
    else
        return 'tab2';

}


function onEmployeeClick(EmployeeGI, LogGI) {
    var url = "/DataBank/EditEmployeeInfo/EditEmployeeLog?EmployeeGI=" + EmployeeGI + "&LogGI=" + LogGI;
    window.location.href = url;
}

function onPayModeChange(sender) {
    var code = $(sender).val();
    if (code == 2) {       
        $('.bankDiv').css('display', 'none');
    }
    else {      
        $('.bankDiv').css('display', 'block');
    }
}

function onEditEmployeeClick() {
    debugger;
    $('#EmployeeId').val("");
    $('#editEmployee').removeClass('hide');
}

function onApproveClick(EmployeeGI, LogGI, index, makerChecker) {
    debugger;
    if (makerChecker == 'true')
        swal.fire("MAKER AND CHECKER SHOULD BE DIFFERENT");
    else {
        var url = "/DataBank/EditEmployeeInfo/Approve?EmployeeGI=" + EmployeeGI + "&LogGI=" + LogGI;
        window.location.href = url;
    }
}

function OnEmpChange(sender) {
    debugger;
    var Code = $(sender).val();
    if (Code != 0) {
        $.ajax({

            type: 'GET',
            url: '/DataBank/EditEmployeeInfo/EmployeeCard',
            dataType: 'html',
            data: { EmpCode: Code },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('.search-profile-details').html("");
                    $('.search-profile-details').html(result);
                }


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });
        $('.search-profile-details').show(300);
    }

}
function onFilterComboChange(SearchId) {
    debugger;
    /* showSpinner();*/
    //var url = "/DataBank/EditEmployeeInfo/GetList";
    //$("." + prevTab).removeClass("active");
    //$(".all").addClass("active");
    var search = SearchId.value;
    $('#FilterCombo_Code').val(search);
    $('.filterDropDown').val(search);

    ApplyFilter('apply');
    //$('#editEmployeeList').load(url, { TabIndex: currentTab, SearchId: search }, function () { });  
}


function alertMessage(data) {
    var msg = '';
    for (let i = 0; i < data.length; i++) {

        if (data[i].code == 4)
            msg = msg + "Employee ID";

        if (data[i].code == 6) {
            if (msg != '')
                msg = msg + ", PF number";
            else
                msg = msg + "PF number";
        }
        if (data[i].code == 7) {
            if (msg != '')
                msg = msg + ", UAN number";
            else
                msg = msg + "UAN number";
        }
        if (data[i].code == 8) {
            if (msg != '')
                msg = msg + ", ESI number";
            else
                msg = msg + "ESI number";
        }
        if (data[i].code == 9) {
            if (msg != '')
                msg = msg + ", Account number";
            else
                msg = msg + "Account number";
        }
    }


    msg = msg + " already exists. Do you want to continue?";

    return msg;

}
function onChangeDOJ(sender, empGi) {
    debugger

    $.getJSON("/DataBank/EditEmployeeInfo/CheckifAlreadySalaryProcessed", { Date: $(sender).val(), empGi: empGi }, function (data) {
        console.log(data);
        debugger
        if (data.success == false) {
            swal.fire(data.message).then(function () {
                return 0;
            });

        }
        else {
            swal.fire("Changes in DOJ will refelect on Renewal Date").then(function () {
                return 1;
            });

        }

    });
}
function Save() {
    debugger;

    var flag;

    if ($('#StatutoryClassification_PaymentMode_Code').val() == 2) {
        flag = true;
    }
    else {
        if (validationBankDetails()) { //Cash mode doesnt require account name, ifsc validations
            flag = true;
        }
        else {
            false
        }
    }

    $.ajaxSetup({
        async: false
    });

    if (flag == true) {
        $.getJSON("/DataBank/EditEmployeeInfo/CheckifAlreadySalaryProcessed", { Date: $("#EmployeeBasicInfo_AcceptedDOJ").val() }, function (data) {
            console.log(data);
            debugger
            if (data.success == false) {
                swal.fire(data.message);

            }
            else {

                $.getJSON("/DataBank/EditEmployeeInfo/AlreadyExistCheck", { EmployeeGI: $('#EmployeeGI').val(), PFNumber: $("#StatutoryClassification_PFNumber").val(), ESINumber: $("#StatutoryClassification_ESINumber").val(), UANNumber: $("#StatutoryClassification_UANNumber").val(), EmployeeID: $("#EmployeeSettings_EmployeeID").val(), AccountNo: $("#StatutoryClassification_AccountNo").val() }, function (data) {

                    debugger;
                    if (data.alreadyExists_OnBoarding != null) {
                        if (data.alreadyExists_OnBoarding[0].code == 4) {
                            swal.fire("Employee ID is already exists");
                            return false;
                        }


                        debugger;
                        var msg = alertMessage(data.alreadyExists_OnBoarding);



                        if (data.alreadyExists_OnBoarding[0].code == 9) {
                            var msg = data.alreadyExists_OnBoarding[0].message;
                            console.log(data);

                            swal.fire({
                                text: msg,
                                icon: 'warning',

                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Ok'
                            }).then((result) => {
                                debugger;
                                if (result.isConfirmed) {
                                    debugger;
                                    return false;
                                }

                            })

                        }
                        else {
                            swal.fire({
                                text: msg,
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Ok'
                            }).then((result) => {
                                debugger;
                                if (result.isConfirmed) {
                                    debugger;
                                    submit();
                                }
                                else {
                                    return false;
                                }
                            })
                        }


                    }
                    else {
                        submit();
                    }






                });

            }

        });


    }
}








function validationBankDetails() {
    debugger;
    var i = 0;

    if ($('#tabThree').hasClass('active')) {

        debugger;


        if (!validateById('#StatutoryClassification_Bank_Code', 'Bname')) {
            i++;
        }
        if (!validateById('#StatutoryClassification_AccountNo', 'AcNo')) {
            i++;
        }
        if (!validateById('#StatutoryClassification_IFSC', 'ifsc')) {
            i++;
        }

        if (i == 0)
            return true;

        else
            return false;

    }
    else
        return true;

}








function submit() {
    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var datas = new FormData($('#editEmployeesave')[0]);
            $.ajax({

                type: 'POST',
                url: '/DataBank/EditEmployeeInfo/Create',
                dataType: 'json',
                contentType: false,
                processData: false,
                data: datas,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            var url = '/DataBank/EditEmployeeInfo/Index'
                            window.location.href = url;
                        });

                    }
                    else {

                        if (result.message == "EMPLOYEE_ID_DUPLICATION_ERROR")
                            swal.fire("Employee ID is already exists");
                        else
                            swal.fire(result.message);
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


function confirmApprove() {


    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/EditEmployeeInfo/Approve',
                dataType: 'json',
                data: $('#Approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Approved Successfully").then(function () {
                            var url = '/DataBank/EditEmployeeInfo/Index?FirstTab=EditEmployeePending';
                            window.location.href = url;
                        });

                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and Checker should be different");
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


function confirmReject() {
    swal.fire({
        text: "Do you want to Reject?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/EditEmployeeInfo/Reject',
                dataType: 'json',
                data: $('#Approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Rejected Successfully").then(function () {
                            var url = '/DataBank/EditEmployeeInfo/Index?FirstTab=EditEmployeePending';
                            window.location.href = url;
                        });

                    }
                    else if (result.message == "MAKER_CHECKER") {
                        swal.fire("Maker and Checker should be different");
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
function onExportToExcelClick() {
    debugger;
    if ($('#FilterCombo_Code').val() != 0)
        var searchid = $('#FilterCombo_Code').val();
    else
        var searchid = 3;
    if (currentTab == 0) {

        var editSummaryTableLength = $('#EditInfoList_Count').val();
        if (editSummaryTableLength > 0) {
            url = "/DataBank/EditEmployeeInfo/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&totCount=" + editSummaryTableLength + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {
        var pendingTableLength = $('#EditInfoList_Count').val();
        if (pendingTableLength > 0) {
            url = "/DataBank/EditEmployeeInfo/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&totCount=" + editSummaryTableLength + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}


function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterCombo_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/EditEmployeeInfo/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#editEmployeeList').html("");
            //            $('#editEmployeeList').html(result);
            //            $('#editEmployeeList').removeClass('hide');                       
            //        }
            //        hideSpinner();

            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }
            //});
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /* showSpinner();*/


            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/EditEmployeeInfo/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#editEmployeeList').html("");
            //            $('#editEmployeeList').html(result);
            //            $('#editEmployeeList').removeClass('hide');
            //        }
            //        hideSpinner();                              
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }
            //});
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();

        //$.ajax({

        //    type: 'POST',
        //    url: '/DataBank/EditEmployeeInfo/ApplyFilter',
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#editEmployeeList').html("");
        //            $('#editEmployeeList').html(result);
        //            $('#editEmployeeList').removeClass('hide');
        //        }
        //        hideSpinner();
        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //    }
        //});
    }
}

function TransactionHistory() {
    currentTab = 0;
    window.location.href = '/DataBank/EditEmployeeInfo/TransactionHistory'
}



function GetEmpList() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    let radioVal;  
    //if (document.getElementById('r1').checked) {
    //    radioVal = document.getElementById('r1').value;
    //} else {
    //    radioVal = document.getElementById('r2').value;
    //}
    radioVal = $('input[name="Type"]:checked').val();
    var dataTable = $("#EditEmployeeInfoListTable").DataTable({
        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/EditEmployeeInfo/GetEmployeelistDetails",
            "type": "POST",
            "datatype": "json",
            'data':
                function (d) {
                    temp = '';
                    var start = d.start;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + radioVal+  '&dummy=dummy';
                    d.filter = temp;
                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#EditEmpInfoList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].editDate = _GetFormattedDate(d.data[i].editDate);

                    d.data[i].fullName = '<div class="d-flex align-items-center" ><a href="/DataBank/EditEmployeeInfo/Create?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].fullName + '</a></div>' +  '</div></div>'

                }
                console.log(d.data);
                return d.data;
            }
        },
        "columns": [
            { "data": "employeeID", "width": "10%" },
            { "data": "fullName", "width": "20%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "14%" },
            { "data": "editDate", "width": "7%" },
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
        //dataTable.search('').draw();
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
function onAccountNumValidation(AccountNo) {

    debugger;
    var BankCode = $("#StatutoryClassification_Bank_Code").val()
    const accountNum = AccountNo.value;
    const length = accountNum.length;

    $.getJSON("/DataBank/EnrollmentTest/GetDigitsInAccountNumber", { BankCode: BankCode }, function (data) {
        debugger;
        var expectedLength = data.digitsAccountNumber;
        if (expectedLength != length && parseInt(expectedLength) > 0) {
            $('#AccountNoValidation').show();
            $('#AccountNoValidation').text('No of digits should be equal to ' + expectedLength);
        }
        else {
            $('#AccountNoValidation').hide();
        }
    });

}
function onBankChange() {
    debugger;
    $("#AccountNo").val('');
    $("#IFSC").val('');
    $("#BankBranch").val('');
}



function onTrnExportToExcelClick() {
    debugger;
    var filter = $('#BasicFilter').serialize();



    var PARAM = 'data=' + filter  ;
    var TableLength = $('#EditInfoList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/EditEmployeeInfo/ExportToExcelTrnList?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}