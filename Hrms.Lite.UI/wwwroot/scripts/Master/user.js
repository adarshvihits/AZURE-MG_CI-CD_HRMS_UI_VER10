var prevTab = "tab1";
var currentTab = "ESSP";



function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var TabIndex = 'Essp';


    /* if (Allowance != '') {*/
    var dataTable = $("#esspTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Master/User/GetEsspList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger;
                console.log(d);
                $('.essp').html(d.count.esspCount);
                $('.hr').html(d.count.hrCount);
                $('.nonEmp').html(d.count.nonEmployeeUsersCount);
                $('.inactive').html(d.count.inactiveCount);
                $('.approval').html(d.count.pendingCount);


                debugger
                $('#UserList_Count').val(d.recordsTotal);

                console.log(d.data);

                for (var i = 0, ien = d.data.length; i < ien; i++) {                   
                    d.data[i].passwordExpiry = _GetFormattedDate(d.data[i].passwordExpiry);
                    d.data[i].validTo = _GetFormattedDate(d.data[i].validTo);
                    d.data[i].dob = _GetFormattedDate(d.data[i].dob);
                    d.data[i].userRole = '<div> <span onmouseenter="tooltipHover()" data-bs-toggle="tooltip" title = "Edit" onclick = "onEditEsspUserClick(' + "'" + d.data[i].userMasterGI + "'" + ')" type = "button" class="btn btn-icon btn-edit icon-only" > <i class="icon-xl fas fa-pencil-alt"></i>  </span ></div >'
                    //<a id="userMasterESSPDelete" data-bs-toggle="tooltip" title="Delete" type="button"  class="btn btn-icon slider-btn  icon-only mobile-full-width  btn-delete" onclick="onDeleteEsspClick(' + "'" + d.data[i].userMasterGI + "'" + ')"><i class="icon-xl far fa-trash-alt"></i> </a>

                    d.data[i].type = '<div><a id="userMasterESSPReset" data-bs-toggle="tooltip" title="Reset" type="button"  class="btn btn-icon slider-btn  icon-only mobile-full-width  btn-edit" onclick="onPasswordResetClick(' + "'" + d.data[i].employeeCode + "'" + ',' + "'" + d.data[i].mobile + "'" + ',' + "'" + d.data[i].dob + "'" + ','+"'" + d.data[i].passwordParameter+"'"+')"><i class="icon-xl far fas fa-cog"></i> </a></div>'
                    if (d.data[i].isNewLogin != true) {
                        d.data[i].isNewLogin = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    }
                    else {
                        d.data[i].isNewLogin = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    }
                }
                
                return d.data;
            }

        },

        "columns": [

            { "data": "userName", "width": "10%"},
            { "data": "employeeName", "width": "21%" },
            { "data": "dob", "width": "10%" },
            { "data": "email", "width": "10%" },
            { "data": "mobile", "width": "10%" },
            { "data": "validTo", "width": "12%"},
            { "data": "passwordExpiry", "width": "12%"},
            { "data": "isNewLogin", "width": "5%" },
            { "data": "userRole", "width": "5%" },
            { "data": "type", "width": "5%" },



        ],






        //"dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

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

    setTimeout(tooltipHover, 500);


    
    //}
    //else {
    //    return false;
    //}

}









function onCreateHRBackofficeUsersClick() {
    url = "/Master/User/Create";
    window.location.href = url;
}

function onSelectAllClick() {
    
    debugger;
    if ($('#SelectAll').is(':checked')) {
        $('#dsg').find('input[type=checkbox]').prop('checked', true);
        $('#dept').find('input[type=checkbox]').prop('checked', true);
        $('#loc').find('input[type=checkbox]').prop('checked', true);
        $('#cat').find('input[type=checkbox]').prop('checked', true);
        $('#grd').find('input[type=checkbox]').prop('checked', true);
        $('#pay').find('input[type=checkbox]').prop('checked', true);
        $('#div').find('input[type=checkbox]').prop('checked', true);
        $('#sec').find('input[type=checkbox]').prop('checked', true);
        $('#empType').find('input[type=checkbox]').prop('checked', true);
        $('#grpC').find('input[type=checkbox]').prop('checked', true);
    }
    else {

        $('#dsg').find('input[type=checkbox]').prop('checked', false);
        $('#dept').find('input[type=checkbox]').prop('checked', false);
        $('#loc').find('input[type=checkbox]').prop('checked', false);
        $('#cat').find('input[type=checkbox]').prop('checked', false);
        $('#grd').find('input[type=checkbox]').prop('checked', false);
        $('#pay').find('input[type=checkbox]').prop('checked', false);
        $('#div').find('input[type=checkbox]').prop('checked', false);
        $('#sec').find('input[type=checkbox]').prop('checked', false);
        $('#empType').find('input[type=checkbox]').prop('checked', false);
        $('#grpC').find('input[type=checkbox]').prop('checked', false);
    }
}
function onAllClick(chkid, tableid) {
    debugger
    if ($('#' + chkid).is(':checked')) {
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
        if (
            $('#dsg').find('input[type=checkbox]').is(':checked') &&
            $('#dept').find('input[type=checkbox]').is(':checked') &&
            $('#loc').find('input[type=checkbox]').is(':checked') &&
            $('#cat').find('input[type=checkbox]').is(':checked') &&
            $('#grd').find('input[type=checkbox]').is(':checked') &&
            $('#pay').find('input[type=checkbox]').is(':checked') &&
            $('#div').find('input[type=checkbox]').is(':checked') &&
            $('#sec').find('input[type=checkbox]').is(':checked') &&
            $('#empType').find('input[type=checkbox]').is(':checked')&&
            $('#grpC').find('input[type=checkbox]').is(':checked')&&
       
            $('#dsg').find('input[type="checkbox"]:checked').length == $('#dsg').find('input[type="checkbox"]').length &&
            $('#dept').find('input[type="checkbox"]:checked').length == $('#dept').find('input[type="checkbox"]').length &&
            $('#loc').find('input[type="checkbox"]:checked').length == $('#loc').find('input[type="checkbox"]').length &&
            $('#cat').find('input[type="checkbox"]:checked').length == $('#cat').find('input[type="checkbox"]').length &&
            $('#grd').find('input[type="checkbox"]:checked').length == $('#grd').find('input[type="checkbox"]').length &&
            $('#pay').find('input[type="checkbox"]:checked').length == $('#pay').find('input[type="checkbox"]').length &&
            $('#div').find('input[type="checkbox"]:checked').length == $('#div').find('input[type="checkbox"]').length &&
            $('#sec').find('input[type="checkbox"]:checked').length == $('#sec').find('input[type="checkbox"]').length &&
            $('#empType').find('input[type="checkbox"]:checked').length == $('#empType').find('input[type="checkbox"]').length &&
            $('#grpC').find('input[type="checkbox"]:checked').length == $('#grpC').find('input[type="checkbox"]').length)
        {
            $('#SelectAll').prop('checked', true);
        }


    }
    else {
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
        $('#SelectAll').prop('checked', false);
    }
}

function onChkBoxClick(sender,allCheckBox,tableId) {
    debugger;
    if ($(sender).is(':checked')) {
        if (
            $('#dsg').find('input[type="checkbox"]:checked').length == $('#dsg').find('input[type="checkbox"]').length &&
            $('#dept').find('input[type="checkbox"]:checked').length == $('#dept').find('input[type="checkbox"]').length &&            
            $('#loc').find('input[type="checkbox"]:checked').length == $('#loc').find('input[type="checkbox"]').length &&                        
            $('#cat').find('input[type="checkbox"]:checked').length == $('#cat').find('input[type="checkbox"]').length &&                        
            $('#grd').find('input[type="checkbox"]:checked').length == $('#grd').find('input[type="checkbox"]').length &&                                    
            $('#pay').find('input[type="checkbox"]:checked').length == $('#pay').find('input[type="checkbox"]').length &&                                                
            $('#div').find('input[type="checkbox"]:checked').length == $('#div').find('input[type="checkbox"]').length &&                                                                        
            $('#sec').find('input[type="checkbox"]:checked').length == $('#sec').find('input[type="checkbox"]').length &&                                                                                    
            $('#empType').find('input[type="checkbox"]:checked').length == $('#empType').find('input[type="checkbox"]').length &&
            $('#grpC').find('input[type="checkbox"]:checked').length == $('#grpC').find('input[type="checkbox"]').length)
        {
            debugger;
            $('#SelectAll').prop('checked', true);
            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
        }
        else {
            debugger;
            
            if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length-1)
                $('#' + allCheckBox).prop('checked', true);
            else
                $('#' + allCheckBox).prop('checked', false);
            if ($('#allDsg').is(':checked') && $('#allDept').is(':checked') &&
                $('#allLoc').is(':checked') && $('#allCat').is(':checked') &&
                $('#allGrd').is(':checked') && $('#allPay').is(':checked') &&
                $('#allDiv').is(':checked') && $('#allSec').is(':checked') &&
                $('#allEmpType').is(':checked') && $('#allGrpCmp').is(':checked')) {
                $('#SelectAll').prop('checked', true);
            }
            else {
                $('#SelectAll').prop('checked', false);
            }
        }
    }
    else {
        debugger;
        $('#SelectAll').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}

function onHRCheckboxClick() {
    if ($('.coupon_question').is(':checked')) {
        $(".IsAdminCHeck").removeClass('hide');
        $('.showCheckBox').css("display","block")
    }
    else {
        $('.showCheckBox').css("display", "none")
        $('.isAdmin').prop('checked', false);
    }
}

function onFullPermissonClick() {
    debugger
    if ($('.coupon_question').is(':checked')) {  //if hr

        if ($('.isAdmin').is(':checked'))
            $('.showadminDiv').css("display", "none");
        else
            $('.showadminDiv').css("display", "block");
    }
    else {
        $('.showCheckBox').css("display", "none")
    }

}

function onViewApplicableMenuClick() {
    debugger;
    //if (URCode == 0) {

    //    swal.fire("Please select a User Role");
    //    closePopUp();
    //}
    //else {
    /*}*/


    $.ajax({
        type: 'GET',
        url: "/Master/User/GetApplicableMenus",
        dataType: 'html',
        data: { UserRoleCode: $('#UserRole_Code').val()},
        success: function (Data) {
           
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {               
                   
                var m = $('#user_Applicable_Menus');
                debugger

                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);               
                hideSpinner();
                m.modal('show');
              
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
        }
    });
   

}

function closePopUp() {
    $('.applicable-menus-popup').hide();
    $('.modal-backdrop').hide();
    $(".form-overlay").remove();
}

function tabClick(Tab, TabIndex) {
    debugger;
   /* showSpinner();*/
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");   
   
    if (TabIndex == "ESSP") {
        var url = "/Master/User/GetUserList";
        debugger
        $('#esspList').load(url, { TabIndex: 'Essp' }, function () { hideSpinner(); });
      
    }
    else if (TabIndex == "HR") {
        var url = "/Master/User/GetUserList";
        $('#esspList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    
    }
    else if (TabIndex == "Non_Employee") {
        var url = "/Master/User/GetUserList";
        $('#esspList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
      
    }
    else if (TabIndex == "INACTIVE") {
        var url = "/Master/User/GetUserList";
        $('#esspList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
       
    }
    else {
        var url = "/Master/User/GetUserList";
        $('#esspList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
       
    }
    prevTab = Tab;
}

//function tabClick(val) {
//    debugger;
//    currentTab = $(val).val();
//    $("." + prevTab).removeClass("active");
//    if (currentTab == "ACTIVE")
//        prevTab = 'ActiveTab'
//    else if (currentTab == "INACTIVE")
//        prevTab = 'InctiveTab';
//    else
//        prevTab = 'PendingTab';

//    $("." + prevTab).addClass("active");
//    var url = "/Master/User/GetUserList";
//    $('#esspList').load(url, { TabIndex: currentTab }, function () { });

//}


function onApproveClick(UserMasterGI, LogGI, UserType, Mode, IsAdmin) {
    debugger;
    var url = "/Master/User/Approve";
    if (UserType == "Essp") {
        $('body').append('<div class="form-overlay"></div>');
        $('#Approve').load(url, { UserMasterGI: UserMasterGI, LogGI: LogGI, UserType: UserType, Mode: Mode, IsAdmin: IsAdmin }, function () { });
      
    }

    if (UserType == "HR" && IsAdmin == "Yes") {
        $('body').append('<div class="form-overlay"></div>');
        $('#Approve').load(url, { UserMasterGI: UserMasterGI, LogGI: LogGI, UserType: UserType, Mode: Mode, IsAdmin: IsAdmin }, function () { });

    }
    if (UserType == "Non Emp Users" || (UserType == "HR" && IsAdmin == "No") ) {
        debugger
        url = url + "?UserMasterGI=" + UserMasterGI + "&LogGI=" + LogGI + "&UserType=" + UserType + "&Mode=" + Mode + "&IsAdmin=" + IsAdmin;
        window.location.href = url;
    }
    
}

function onEditEsspUserClick(UserMasterGI, UserType) {
    debugger
    var url = "/Master/User/EditUserEssp?UserMasterGI=" + UserMasterGI + "&UserType=" + UserType;    
    window.location.href = url;
}
function onEditInActiveUserClick(UserMasterGI, UserType) {
    if (UserType == "Essp") {

        var url = "/Master/User/EditUserEssp?UserMasterGI=" + UserMasterGI + "&UserType=" + UserType;
        window.location.href = url;
    }
    else if (UserType == "HR") {
        var url = "/Master/User/EditUserHr?UserMasterGI=" + UserMasterGI;
        window.location.href = url;
    }
    else {
        var url = "/Master/User/EditUserNonEmployee?UserMasterGI=" + UserMasterGI;
        window.location.href = url;
    }
}

function onEditHrClick(UserMasterGI) {
    debugger;
    var url = "/Master/User/EditUserHr?UserMasterGI=" + UserMasterGI;
    window.location.href = url;
}
function onEditNonEmployeeUserClick(UserMasterGI) {
    var url = "/Master/User/EditUserNonEmployee?UserMasterGI=" + UserMasterGI;
    window.location.href = url;
}
function onDeleteNonEmployeeUserClick(UserMasterGI) {
    var url = "/Master/User/DeleteUserNonEmployee?UserMasterGI=" + UserMasterGI;
    window.location.href = url;
}
function onDeleteHrClick(UserMasterGI) {
    var url = "/Master/User/DeleteUserHr?UserMasterGI=" + UserMasterGI;
    window.location.href = url;
}
function onDeleteInActiveClick(UserMasterGI) {
    var url = "/Master/User/DeleteUserInActive?UserMasterGI=" + UserMasterGI;
    window.location.href = url;
}
function onDeleteEsspClick(UserMasterGI) {
    var url = "/Master/User/DeleteUserEssp";
    $('body').append('<div class="form-overlay"></div>');
    $('#deleteEsspUser').load(url, { UserMasterGI: UserMasterGI }, function () { });
}

function _GetDOBPassword(date) {
    debugger


    var formattedDate = new Date(date);
    var cDate = formattedDate.getDate();
    var cMonth = formattedDate.getMonth() + 1;
    var cYear = formattedDate.getFullYear();
    if (cDate < 10)
        cDate = '0' + cDate;
    if (cMonth < 10)
        cMonth = '0' + cMonth;
    formattedDate = (cDate.toString() + cMonth.toString() + cYear.toString());
    return formattedDate;

}

function onPasswordResetClick(employeeCode,mobile,dob,pwdParameter, usercode) {
    debugger
    if (employeeCode == 0) //NON EMPLOYEE USER
    {
        password = 'Password@1234';
    }
    else
    {
        if (pwdParameter == "DOB") {
            if (dob == '') {
                swal.fire('Please add DOB  and try resetting');
                return false;
            }
            password = _GetDOBPassword(dob);
        }
        else {
            if (mobile == '') {
                swal.fire('Please add personal mobile number and try resetting');
                return false;
            }
            password = mobile;
        }
    }
    swal.fire({
        text: "Do you want to Reset?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Master/User/ResetUserEssp',
                dataType: 'json',
                data: { EmployeeCode: employeeCode, Password: password, UserCode: usercode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            fillData();
                            window.location.href = url;
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        }
        else {
            return false;
        }
    })
}
function onDeleteInActiveEsspClick(UserMasterGI) {
    var url = "/Master/User/DeleteUserEssp";
    $('body').append('<div class="form-overlay"></div>');
    $('#deleteInActiveUser').removeClass('hide');
    $('#deleteInActiveUser').load(url, { UserMasterGI: UserMasterGI }, function () { });
}
function DeleteUser(UserMasterGI) {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var url = "/Master/User/DeleteUser?UserMasterGI=" + UserMasterGI;
            window.location.href = url;
        }
        else {
            return false;
        }
    })

   
}

function SaveUser() {
    var i = 0;
   debugger   


    
    if (!validateById('#Name', 'UName')) {
        i++;
    }
    if (!validateById('#UserRole_Code', 'URole')) {
        i++;
    }
    if (!validateMobileNumber('Mobile', 'Mob')) {
        i++;
    }
    if (!validateEmailID(document.getElementById('Email'), 'email')) {
        i++;
    }
    if (!validateById('#ActiveFrom', 'AFrom')) {
        i++;
    }
    if (!validateById('#ActiveTo', 'ATo')) {
        i++;
    }
    if (!validateById('#Remarks', 'Rem')) {
        i++;
    }  

        if ($("#dsg input[type=checkbox]:checked").length == 0 || $("#loc input[type=checkbox]:checked").length == 0 || $("#cat input[type=checkbox]:checked").length == 0
            || $("#dept input[type=checkbox]:checked").length == 0 || $("#grd input[type=checkbox]:checked").length == 0 || $("#empType input[type=checkbox]:checked").length == 0
            || $("#pay input[type=checkbox]:checked").length == 0 || $("#div input[type=checkbox]:checked").length == 0 || $("#sec input[type=checkbox]:checked").length == 0
        ) {
            swal.fire("Please select atleast one from each Employee Classifications");
            return false;
        }


   
    if (i == 0) {



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
                    url: '/Master/User/Create',
                    dataType: 'json',
                    data: $('#createUser').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                var url = '/Master/User/Index'
                                window.location.href = url;
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        })

    }
    else
        return false;
}
function EditUser() {
    debugger
     var i = 0;
    if ($('.coupon_question').is(":checked") == true  )
    {
        if ($('.isAdmin').is(":checked") == false) {
            if ($("#dsg input[type=checkbox]:checked").length == 0 || $("#loc input[type=checkbox]:checked").length == 0 || $("#cat input[type=checkbox]:checked").length == 0
                || $("#dept input[type=checkbox]:checked").length == 0 || $("#grd input[type=checkbox]:checked").length == 0 || $("#empType input[type=checkbox]:checked").length == 0
                || $("#pay input[type=checkbox]:checked").length == 0 || $("#div input[type=checkbox]:checked").length == 0 || $("#sec input[type=checkbox]:checked").length == 0
                || $("#grpC input[type=checkbox]:checked").length == 0
            ) {
                swal.fire("Please select atleast one from each Employee Classifications");
                return false;
            }

        }
    }
    //var i = 0;
    //if (!validateById('#Name', 'UName')) {
    //    i++;
    //}
    //if (!validateById('#UserRole_Code', 'URole')) {
    //    i++;
    //}
    //if (!validateMobileNumber('Mobile', 'Mob')) {
    //    i++;
    //}
    //if (!validateEmailID(document.getElementById('Email'), 'email')) {
    //    i++;
    //}
    //if (!validateById('#ActiveFrom', 'AFrom')) {
    //    i++;
    //}
    //if (!validateById('#ActiveTo', 'ATo')) {
    //    i++;
    //}
    //if (!validateById('#Remarks', 'Rem')) {
    //    i++;
    //}

    //if (i == 0) {



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
                    url: '/Master/User/Modify',
                    dataType: 'json',
                    data: $('#editUser').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                var url = '/Master/User/Index'
                                window.location.href = url;
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }
                });
            }
            else {
                return false;
            }
        })

    //}
    //else
    //    return false;
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
                url: '/Master/User/ApproveRequest',
                dataType: 'json',
                data: $('#ApproveOrRejectNonEmp').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approveSlider');

                            var url = "/Master/User/Index";
                            window.location.href = url;

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            //debugger;
                            //var url = "/Master/Allowance/Approve?AllowanceGI=" + GI + "&LogGI=" + LogGI;
                            //$('#ApproveSlider').load(url, function () { });
                            //$('body').append('<div class="form-overlay"></div>');


                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
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
                url: '/Master/User/Reject',
                dataType: 'json',
                data: $('#ApproveOrRejectNonEmp').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = '/Master/User/Index'
                            window.location.href = url;
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
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
    if (currentTab == "ESSP") {

        var esspTableLength = $('#UserList_Count').val();
        if (esspTableLength > 0) {
            url = "/Master/User/ExportToExcel?TabIndex=" + currentTab + "&totalRecords=" + esspTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == "HR") {
        var hrTableLength = $('#UserList_Count').val();
        if (hrTableLength > 0) {
            url = "/Master/User/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == "Non_Employee") {
        var nonEmployeeTableLength = $('#UserList_Count').val();
        if (nonEmployeeTableLength > 0) {
            url = "/Master/User/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == "INACTIVE") {
        var inActiveTableLength = $('#UserList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/User/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else {
        var approvePendingTableLength = $('#UserList_Count').val();
        if (approvePendingTableLength > 0) {
            url = "/Master/User/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

function ActiveInactiveValidationUser(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/User/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#activeinactiveuser").prop("checked", true);
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
function ActiveInactiveValidationUser1(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/User/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#activeInactiveUser1").prop("checked", true);
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
function ActiveInactiveValidationUser2(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/User/GetActiveInactiveValidation?name=' + name + '&code=' + code,
        dataType: 'json',

        success: function (result) {
            if (result.success == true) {
                //swal.fire(result.message).then(function () {
                debugger;
                // $("#activeInactive").prop("checked", false);

                //});

            }
            else {
                swal.fire(result.message).then(function () {
                    $("#activeinactiveuser2").prop("checked", true);
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

