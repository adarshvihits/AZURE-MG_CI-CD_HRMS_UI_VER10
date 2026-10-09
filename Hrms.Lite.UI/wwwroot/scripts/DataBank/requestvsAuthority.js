var prevTab = 'all';
var currentTab = 0;
var currentTabName = 'All';
var levelListData = '';
function batchUpdateClick() {
    debugger;
    $('#BatchUpdate_RequestType_Code').removeClass('is-invalid');
    $('#reqtype').removeClass('error-message');
    $('#reqtype').html("");
    $('#BatchUpdate_RequestLevel_Code').removeClass('is-invalid');
    $('#reqlevel').removeClass('error-message');
    $('#reqlevel').html("");


    $.ajax({

        type: 'GET',
        url: '/DataBank/RequestVsAuthority/BatchUpdate',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#BatchUpdateSlider').html("");
                $('#BatchUpdateSlider').html(result);
            }
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}

function onEmployeeClick(EmpGI) {
    var url = '/DataBank/RequestVsAuthority/RequestAuthority?EmployeeGI=' + EmpGI;
    window.location.href = url;
}

function addLevelClick() {
    debugger;
    var ReqType = $("#BatchUpdate_RequestType_Code").val();
    $.ajax({

        type: 'GET',
        url: '/DataBank/RequestVsAuthority/AddLevel',
        dataType: 'html',
        data: { EmployeeGI: EmpGI, code: ReqType },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddLevelSlider').html("");
                $('#AddLevelSlider').html(result);
            }


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
    $('#AddLevelSlider').removeClass('hide');
}

function onRequestTypeChange(id, EmpGI) {
    debugger;

    $.ajax({

        type: 'GET',
        url: '/DataBank/RequestVsAuthority/RequestTypeChange',
        dataType: 'html',
        data: { EmployeeGI: EmpGI, RequestID: $(id).val() },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#LevelList').html("");
                $('#LevelList').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });


}

function tabClick(Tab, TabIndex, TabName) {
    debugger;
    /* showSpinner();*/
    var searchVal = 0;
    $('#TabCode').val(TabIndex);
    if (Tab == "tab2") {
        $('#UnassignedDropDown').addClass('hide');
        $('#MUnassignedDropdown').addClass('hide');
        $('.bulkUploadTab').addClass('hide');
        $('#otherTabs').removeClass('hide');
        //$('#RequestVsAuthorityList').addClass('hide');
        //$('#assignDiv').addClass('hide');
        //$('#notAssignDiv').removeClass('hide');
    }
    else if (Tab == "tab1") {
        $('#UnassignedDropDown').addClass('hide');
        $('#MUnassignedDropdown').addClass('hide');
        $('.bulkUploadTab').addClass('hide');
        $('#otherTabs').removeClass('hide');
        //$('#RequestVsAuthorityList').addClass('hide');
        //$('#notAssignDiv').addClass('hide');
        //$('#assignDiv').removeClass('hide');
    }
    else if (Tab == "tab3") {
        $('#otherTabs').addClass('hide');
        $('.bulkUploadTab').removeClass('hide');
        $('#MUnassignedDropdown').addClass('hide');
        //$('#RequestVsAuthorityList').addClass('hide');
        //$('#notAssignDiv').addClass('hide');
        //$('#assignDiv').removeClass('hide');
    }
    else {
        $('#UnassignedDropDown').removeClass('hide');
        $('#MUnassignedDropdown').removeClass('hide');
        $('.bulkUploadTab').addClass('hide');
        $('#otherTabs').removeClass('hide');
        //$('#notAssignDiv').addClass('hide');
        //$('#assignDiv').addClass('hide');
        //$('#RequestVsAuthorityList').removeClass('hide');
    }
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");


    //var url = "/DataBank/RequestVsAuthority/GetRequestVsAuthorityList";
    //$('#RequestVsAuthorityList').load(url, { TabIndex: TabIndex, SearchID: searchVal}, function () { });
    ApplyFilter('apply');
    currentTabName = TabName;
    prevTab = Tab;
}

function TabsDropdown(TabIndex, TabName) {
    debugger;
    var searchVal = 0;
    currentTab = TabIndex;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    if (Tab == "tab2") {
        $('#UnassignedDropDown').removeClass('hide');
        $('#MUnassignedDropdown').removeClass('hide');
    }
    else {
        $('.RUnassignedList').val("");
        $('#UnassignedList').val("");
        $('#UnassignedDropDown').addClass('hide');
        $('#MUnassignedDropdown').addClass('hide');
    }

    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/RequestVsAuthority/GetRequestVsAuthorityList";
    //$('#RequestVsAuthorityList').load(url, { TabIndex: TabIndex, SearchID: searchVal}, function () { });
    ApplyFilter('apply');
    currentTabName = TabName;
    prevTab = Tab;
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else
        return 'tab2';

}

function getLevelDetails(ReqVsAuthGI, mode) {
    $.ajax({

        type: 'GET',
        url: '/DataBank/RequestVsAuthority/LevelDetails',
        dataType: 'html',
        data: { EmpGI: EmpGI, ReqVsAuthGI: ReqVsAuthGI, mode: mode },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#AddLevelSlider').html("");
                $('#AddLevelSlider').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#AddLevelSlider').removeClass('hide');
}

function onUnassignedFilterChange(sender) {
    debugger;
    /* showSpinner();*/
    var tabIndex = 2;
    var searcId = sender.value;
    $('.RUnassignedList').val(searcId);
    $('#UnassignedList').val(searcId);
    if (parseInt(searcId) == 0)
        $('#requestAthorityBachUpdate').removeClass('hide');
    else
        $('#requestAthorityBachUpdate').addClass('hide');
    //var url = '/DataBank/RequestVsAuthority/GetRequestVsAuthorityList?TabIndex=' + tabIndex + '&SearchID=' + searcId;
    //$('#RequestVsAuthorityList').load(url, { TabIndex: tabIndex, SearchID: searcId }, function () { });
    ApplyFilter('apply');
}


//function ApplyFilter(mode) {

//    var tabIndex = $('#Tabindex').val();

//    $("#Tabindex").val(currentTab);


//    fillData();
//    CloseSlider('basicFilter');
//}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#UnassignedList').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /* showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        /*   CloseSlider('basicFilter');*/
    }
}


function ExportToExcel() {
    debugger;
    var searchid = $('#Filter_Code').val();

    var tableLength = $('#RequestVsAuthorityList_Count').val();
    if (tableLength > 0) {

        url = "/DataBank/RequestVsAuthority/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&TabName=" + currentTabName + "&totRecords=" + tableLength + "&RequestTypeCode=" + $('#RequestType_Code').val() + "&AuthorityLevelCode=" + $('#AuthorityLevel_Code').val() + '&data=' + $('#BasicFilter').serialize();;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ValidateAuthority() {
    var i = 0;
    if (!validateById('#AddLevel_RequestLevel_Code', 'level')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}

function RequestVsAuthorityBatchUpdateValidation() {   

    var i = 0;
    if (!validateById('#BatchUpdate_RequestType_Code', 'reqtype')) {
        i++;
    }
    if ($('#BatchUpdate_RequestType_Code').val() != 1) {
        if (!validateById('#BatchUpdate_RequestLevel_Code', 'reqlevel')) {

            i++;
        }
    }
    if (!validateReqAuthority(document.getElementById('BatchUpdate_RequestAuthority_Code'), 'reqAuth', 'reqauthority')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        $.ajax({

            type: 'POST',
            url: '/DataBank/RequestVsAuthority/RequestVsAuthorityBatchUpdateValidation?Tabindex=' + currentTab + '&SearchId=' + $('.RUnassignedList').val(),
            dataType: 'json',
            data: $('#batchSaveId').serialize() + $('#BasicFilter').serialize(),
         
            success: function (result) {
                debugger;
                if (result.success == false) {
                    batchSave(result.message);
                }

                else {
                    batchSave("Do you want to Update?");
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
function existCheck() {
    levelListData = $('#AddevelId').serialize();
    $.ajax({

        type: 'POST',
        url: '/DataBank/RequestVsAuthority/ExistCheck',
        dataType: 'json',
        data: $('#AddevelId').serialize(),
        success: function (result) {
            debugger;
            if (result.alreadyExists_RequestVsAuthority == null) {
                var mode = $('#AddLevel_Mode').val();
                debugger;
                if (mode == "Create")
                    modeName = "Save";
                else if (mode == "MODIFY")
                    modeName = "Modify";
                else
                    modeName = "Delete";
                var msg = "Do you want to " + modeName + "?";
                LevelListSave(msg, modeName);
            }

            else {
                if (result.alreadyExists_RequestVsAuthority[0].code == 1) {
                    var msg = result.alreadyExists_RequestVsAuthority[0].message + ".Do you want to Continue?";
                    LevelListSave(msg);
                }
                else {
                    swal.fire(result.alreadyExists_RequestVsAuthority[0].message);
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
function LevelListSave(msg, mode) {
    console.log(levelListData);
    debugger;
    $.ajaxSetup({
        async: false
    });

    swal.fire({
        text: msg,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var i = 0;
            debugger;
            if (mode != "Delete") {
                if (!validateReqAuthority(document.getElementById('AddLevel_RequestLevel_Code'), 'reqAuth', 'level')) {
                    i++;
                }
            }
            debugger;
            if (i > 0) {
                return false;
            }
            else {
                showSpinner();
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/RequestVsAuthority/AddLevel?RequestTypeName=' + $('#BatchUpdate_RequestType_Code option:selected').text(),
                    dataType: 'json',
                    data: levelListData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('add-batch');


                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/RequestVsAuthority/GetLevelList',
                                    dataType: 'html',
                                    data: { EmployeeGI: EmpGI, ReqTypeId: $('#BatchUpdate_RequestType_Code').val() },

                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#LevelList').html("");
                                            $('#LevelList').html(result);
                                        }
                                        hideSpinner();
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        hideSpinner();
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/RequestVsAuthority/GetLogList',
                                    dataType: 'html',
                                    data: { EmployeeGI: EmpGI },

                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#LogList').html("");
                                            $('#LogList').html(result);
                                        }
                                        hideSpinner();
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        hideSpinner();
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                hideSpinner();
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
        }
        else {
            CloseSlider('add-batch');
            return false;
        }
    })

}

function batchSave(txtMsg) {
    debugger;
    //$.ajaxSetup({
    //    async: false
    //});
    debugger;
    var i = 0;
    if (!validateById('#BatchUpdate_RequestType_Code', 'reqtype')) {
        i++;
    }
    if ($('#BatchUpdate_RequestType_Code').val() != 1) {
        if (!validateById('#BatchUpdate_RequestLevel_Code', 'reqlevel')) {

            i++;
        }
    }
    if (!validateReqAuthority(document.getElementById('BatchUpdate_RequestAuthority_Code'), 'reqAuth', 'reqauthority')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {

        swal.fire({
            text: txtMsg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                //    // Save it
                showSpinner();
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/RequestVsAuthority/BatchUpdate?Tabindex=' + currentTab + '&SearchId=' + $('.RUnassignedList').val(),
                    dataType: 'json',
                    data: $('#batchSaveId').serialize() + $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('add-batch');
                                ApplyFilter('apply');
                                hideSpinner();
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                hideSpinner();
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

}

function validateReqAuthority(val, divId, id) {
    debugger;
    if (val.value == "") {
        $("." + divId + "").addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        $("." + divId + "").removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}


function fillData() {
    debugger;


    // $.ajaxSetup({ async: false });

    var tabIndex = currentTab;
    

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        /* var tabIndex = $('#Tabindex').val();*/
        var RequestCode = 0;
        var LEVEL = 0;
        if (tabIndex == 3) {
             RequestCode = $('#RequestType_Code').val();
             LEVEL = $('#AuthorityLevel_Code').val();
        }


        var dataTable = $("#RequestVsAuthorityTable").DataTable({
            //"paging": true,
            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/RequestVsAuthority/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + tabIndex + '&RequestCode=' + RequestCode + '&LEVEL=' + LEVEL+ '&dummy=dummy';
                        d.filter = temp;



                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;
                    $('.alltab').html(d.count.allTabCount);
                    $('.assgnd').html(d.count.assigned);
                    $('.notassgnd').html(d.count.notAssigned);
                    $('.blkUpload').html(d.count.bulkUpload);




                    $('#RequestVsAuthorityList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].employeeName = '<div class="name-with-img"> <a href="/DataBank/RequestVsAuthority/RequestAuthority?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Request Authority List">' + d.data[i].employeeName + '</a></div> </div></div></a> </div>'

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        if (tabIndex != 3) {
                            if (d.data[i].rm != true) {
                                d.data[i].rm = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].rm = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }
                            if (d.data[i].leave != true) {
                                d.data[i].leave = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].leave = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }
                            if (d.data[i].loan != true) {
                                d.data[i].loan = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].loan = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }
                            if (d.data[i].letters != true) {
                                d.data[i].letters = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].letters = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }
                            if (d.data[i].assets != true) {
                                d.data[i].assets = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].assets = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }
                            if (d.data[i].carrer != true) {
                                d.data[i].carrer = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].carrer = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }
                            if (d.data[i].overtime != true) {
                                d.data[i].overtime = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                            }
                            else {
                                d.data[i].overtime = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                            }

                        }

                    }
                    if (tabIndex == 3) {
                        $('.otherTabsCols').addClass('hide');
                        $('#authRow').removeClass('hide');
                    }
                    else {
                        $('.otherTabsCols').removeClass('hide');
                        $('#authRow').addClass('hide');
                    }
                    return d.data;
                }

            },

            "columns": [

                //{ "data": "empId", "width": "8%" },
                //{ "data": "employeeName", "width": "12%" },
                //{ "data": "designation", "width": "11%" },
                //{ "data": "department", "width": "11%" },
                //{ "data": "location", "width": "9%" },
                //{ "data": "doj", "width": "8%" },
                //{ "data": "rm", "width": "5%" },
                //{ "data": "leave", "width": "6%" },
                //{ "data": "loan", "width": "7%" },
                //{ "data": "letters", "width": "6%" },
                //{ "data": "assets", "width": "6%" },
                //{ "data": "carrer", "width": "6%" },
                //{ "data": "overtime", "width": "5%" },

                { "data": "empId"},
                { "data": "employeeName" },
                { "data": "designation"},
                { "data": "department" },
                { "data": "location"},
                { "data": "doj" },
                { "data": "authorityDetails" },
                { "data": "rm" },
                { "data": "leave"},
                { "data": "loan" },
                { "data": "letters" },
                { "data": "assets"},
                { "data": "carrer"},
                { "data": "overtime" },

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





    if (tabIndex == 3) {
        $('.request-authority-list').addClass('bulk_upload_list');
        $('.bulk_upload_list').removeClass('request-authority-list');
        let column6 = dataTable.column(6)
        column6.visible(true);

        let column7 = dataTable.column(7);
        column7.visible(false);

        let column8 = dataTable.column(8)
        column8.visible(false);

        let column9 = dataTable.column(9)
        column9.visible(false);

        let column10 = dataTable.column(10)
        column10.visible(false);

        let column11 = dataTable.column(11)
        column11.visible(false);

        let column12 = dataTable.column(12)
        column12.visible(false);

        let column13 = dataTable.column(13)
        column13.visible(false);


    }
    else {
        $('.bulk_upload_list').addClass('request-authority-list');
        $('.request-authority-list').removeClass('bulk_upload_list');
        let column6 = dataTable.column(6)
        column6.visible(false);

        let column7 = dataTable.column(7);
        column7.visible(true);

        let column8 = dataTable.column(8)
        column8.visible(true);

        let column9 = dataTable.column(9)
        column9.visible(true);

        let column10 = dataTable.column(10)
        column10.visible(true);

        let column11 = dataTable.column(11)
        column11.visible(true);

        let column12 = dataTable.column(12)
        column12.visible(true);

        let column13 = dataTable.column(13)
        column13.visible(true);
    }
       
    



    


  




}








function ontypeChange() {
    debugger;
    if ($('#BatchUpdate_RequestType_Code').val() == 1) {
        $('#levelhide').addClass('hide');
        $("#BatchUpdate_RequestLevel_Code").val(1);
    }
    else {
        $('#levelhide').removeClass('hide');
        $("#BatchUpdate_RequestLevel_Code").val('');
    }
}

function ontypeBulkUploadChange() {
    debugger;
    if ($('#RequestType_Code').val() == 1) {
        $('#authLvl').addClass('hide');
        $("#AuthorityLevel_Code").val(1);
    }
    else {
        $('#authLvl').removeClass('hide');
        $("#AuthorityLevel_Code").val('');
    }
}

function validateBulkUploadDropdowns() {
    var i = 0;
    debugger;
    if (!validateById('#RequestType_Code', 'ReqCode')) {
        i++;
    }
    if (!validateById('#AuthorityLevel_Code', 'AuthCode')) {
        i++;
    }
    if (i == 0) {
        //$('#Excel_FileData').trigger('click');
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }

}

function onImportExcelClick() {

    
    var MPPersonalInfo = new FormData($('#BulkUpload')[0]);
    $("#Excel_FileData").val('');
    $.ajax({

        type: 'POST',
        url: '/DataBank/RequestVsAuthority/ImportFromExcel',
        contentType: false,
        processData: false,
        data: MPPersonalInfo,
        success: function (result) {
            debugger;
            if (result.success == true) {
                $("#Excel_FileData").val('');
                swal.fire("Uploaded Successfully").then(function () {                    
                    fillData()
                    var m = $('.bd-example-modal-lg');
                    m.modal('hide');
                });
            }
            else {
                $("#Excel_FileData").val('');
                if (result.message != null) {
                    $('#Unmatched').html('')
                    swal.fire(result.message);
                }
                else {
                    $('#Unmatched').html(result)
                }
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

//REGION AUTHORITY CHANGE
function ChangeAuthority() {
    var url = '/DataBank/RequestVsAuthority/ChangeAuthority_Index';
    window.location.href = url;
}
function authorityChangeFilldata() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var Authority = $('#Authority_Code').val();
    var Level = $('#RequestLevel_Code').val();
    var Type = $('#RequestType_Code').val();

    var dataTable = $("#ReporteesTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/RequestVsAuthority/ChangeAuthority_EmployeeList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1+'&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + Type + '&Level=' + Level + '&Authority=' + Authority + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                $('#ReporteesList_Count').val(d.recordsTotal);

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId", "width": "8%" },
            { "data": "employeeHeader.employeeName", "width": "22%" },
            { "data": "designation", "width": "15%" },
            { "data": "department", "width": "15%" },
            { "data": "location", "width": "10%" },
            { "data": "type", "width": "22%" },
            { "data": "level", "width": "8%" }



        ],






        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',


        "serverSide": "true",
        "scrollY": '50vh',
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


}

function onReqTypeChange(sender, id) {
    debugger;



    $.getJSON("/DataBank/Reportees/GetLevelDropDown", { TypeCode: $(sender).val() }, function (data) {
        $('#RequestLevel_Code option').remove();
        $('#RequestLevel_Code').append('<option value="0">ALL</option');

        for (var i = 0; i < data.length; i++) {
            $('#RequestLevel_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        authorityChangeFilldata();
    });

}
function onReqLevelChange(sender, id) {
    debugger;

    var Type = $('#RequestType_Code').val();

    $.getJSON("/DataBank/Reportees/GetAuthorityDropDown", { LevelCode: $(sender).val(), TypeCode: Type }, function (data) {
        $('#Authority_Code option').remove();
        $('#Authority_Code').append('<option value="0">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Authority_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        authorityChangeFilldata();
    });

}

function validatechangeauth() {
    var i = 0;
    if (!validateMultiselect('ChangeAuthority_Code', 'autchange', 'ChangeAuthoritydiv'))
        i++;

    if (!validateById('#Authority_Code', 'aut'))
        i++;
    if (i == 0) {
        if ($("#ChangeAuthority_Code").val() == $("#Authority_Code").val()) {
            swal.fire('Change Authority should be different from existing Authority');
            return false;
        }
    }
    if (i == 0)
        return true;
    else {
        return false;
    }
}

function ChangeAuthoritySave() {
    if (validatechangeauth()) {


        swal.fire({
            text: 'Do you want to Confirm?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {

                $.ajax({

                    type: 'POST',
                    url: '/DataBank/RequestVsAuthority/ChangeAuthoritySave',
                    dataType: 'json',
                    data: $('#authorityChangeform').serialize(),
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                authorityChangeFilldata();

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
}