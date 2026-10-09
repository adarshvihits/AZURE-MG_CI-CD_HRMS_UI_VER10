var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';

function OnTabClick(Tab, TabIndex) {
    debugger;
  
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    $('#tabsDropdownCircle').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/Circle/GetCircleList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#CircleList').html("");
                $('#CircleList').html(result);
            }
          


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
          

        }
    });

    prevTab = Tab;
}


function tabClick(val) {
    debugger;

    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
    $.ajax({

        type: 'GET',
        url: '/Master/Circle/GetCircleList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#CircleList').html("");
                $('#CircleList').html(result);
            }
      


        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
      

        }
    });


}





function CircleSave(mode) {
    debugger;
    if (validateCircle()) {
        debugger;
        swal.fire({
            text: "Do you want to " + mode + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/Master/Circle/Save',
                    dataType: 'json',
                    data: $('#Circlesave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseCircleSlider('AddnewOrEditDiv');
                                OnTabClick(prevTab, currentTab);
                                tabClick(document.getElementById('tabsDropdownCircle'))
                                //var url = "/Master/Department/GetDepartmentList?TabIndex=" + currentTab;
                                //$('#DepartmentList').load(url, function () { });

                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        debugger
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
    else {
        return false;
    }
}







function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;

    if (Type == 'MODIFY') {

        if ($("#CircleList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
            $.ajax({

                type: 'GET',
                url: '/Master/Circle/Edit',
                dataType: 'html',
                data: { CircleGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#circleSlider').html("");
                        $('#circleSlider').html(result);
                    }


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);


                }
            });

        }

    }

    else {
     
        var url = "/Master/Circle/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/Circle/Create',
            dataType: 'html',
            data: { CircleGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#circleSlider').html("");
                    $('#circleSlider').html(result);
                  
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }


}







function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#CircleList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/Circle/Approve',
            dataType: 'html',
            data: { CircleGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#circleSlider').html("");
                    $('#circleSlider').html(result);
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






function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#CircleList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'GET',
            url: '/Master/Circle/Delete',
            dataType: 'html',
            data: { CircleGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#circleSlider').html("");
                    $('#circleSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
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




function validateCircle() {
    var i = 0;
    var TableLength = document.getElementById("CircleTable").rows.length - 1;
    for (j = 0; j < TableLength; j++) {
        if (!validateCircleAuthorityDD(document.getElementById('CircleAuthorities_' + j + '__Authority_Code'),'circleval','authorityname_' + j,'authorityname_')) {
            i++;
        }
        if (!validateAuthorityPermissionById('#CircleAuthorities_' + j + '__Permission_Code', 'permissionname_' + j,'permissionname_')) {
            i++;
        }
    }
    if (!validateById('#Name', 'CircleName')) {
        i++;
    }
    if (!validateById('#ShortName', 'CircleShortName')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}









function confirmApprove(GI, LogGI) {
    debugger
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
                url: '/Master/Circle/Approve',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseCircleSlider('Approve');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownCircle'));
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'APPROVAL_PENDING';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        debugger
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Circle/Approve?CircleGI=" + GI + "&LogGI=" + LogGI;
                            $('#circleSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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



function confirmReject(GI, LogGI) {
    debugger
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
                url: '/Master/Circle/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseCircleSlider('Approve');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownCircle'));
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'APPROVAL_PENDING';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").addClass("active");
                            //$(".ActiveTab").removeClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/Circle/Approve?CircleGI=" + GI + "&LogGI=" + LogGI;
                            $('#circleSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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


function confirmDelete(GI, LogGI) {
    debugger
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
                url: '/Master/Circle/Delete',
                dataType: 'json',
                data: $('#DeleteCircle').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseCircleSlider('delete');
                            OnTabClick(prevTab, currentTab);
                            tabClick(document.getElementById('tabsDropdownCircle'));
                            //var url = "/Master/Department/GetDepartmentList?TabIndex=" + 'ACTIVE';
                            //$('#DepartmentList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#DepartmentList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/DeleteCircle/Delete?CircleGI=" + GI + "&LogGI=" + LogGI;
                            $('#circleSlider').load(url, function () { });
                            $('body').append('<div class="form-overlay"></div>');

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

function CloseCircleSlider(id) {
    debugger
    $("." + id).removeClass('slider-forms-open');
    $(".form-overlay").remove();
    $(".AddnewOrEditDiv").remove();
}


function clickOnExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#CircleList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Circle/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#CircleList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Circle/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#CircleList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Circle/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}

function AddNewResultRow(tableId, i) {
    debugger;
    $('.loadselectpicker').selectpicker('destroy');
    $('#objDelete_0').removeClass('hide');
    var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
    var template = $('#' + tableId + ' #row_OI_0').clone(); // clone the row

    $.each(template.find('input, textarea'), function () {
        var name = $(this).attr('name');
        if (typeof name != 'undefined') {
            name = name.replace(/\[0\]./g, '[' + count + '].');
            $(this).attr('name', name);
        }

        var id = $(this).attr('id');
        if (typeof id != 'undefined') {
            id = id.replace(/_0__/g, '_' + count + '__');
            $(this).attr('id', id);
        }
        //---------------------------------For row Increment When onkeyup
        var onkeyup = $(this).attr('onkeyup');
        if (onkeyup !== undefined) {
            onkeyup = onkeyup.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onkeyup', onkeyup);
        }

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('select'), function () {
        var name = $(this).attr('name');
        name = name.replace(/\[0\]./g, '[' + count + '].');
        $(this).attr('name', name);

        var id = $(this).attr('id');
        id = id.replace(/_0__/g, '_' + count + '__');
        $(this).attr('id', id);

        //---------------------------------------For row Increment When onchange
        var onchange = $(this).attr('onchange');
        if (onchange !== undefined) {
            onchange = onchange.replace(/\,0\)/g, ',' + count + ')');

            $(this).attr('onchange', onchange);
        }
        //---------------------------------------
    });
    $.each(template.find('.authority'), function () {

        $(this).attr("id", "authorityname_" + count);

    });
    $.each(template.find('.permission'), function () {

        $(this).attr("id", "permissionname_" + count);

    });
   

    $.each(template.find('a'), function () {
        var onclick = $(this).attr('onclick');
        onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
        $(this).attr('onclick', onclick);
    });


    template.find('td[id=grid_slno]').text(count + 1);
    $.each(template.find('.addObj'), function () {
        $(this).attr("id", "objAdd_" + count);
    });
    $.each(template.find('.objDelete'), function () {
        $(this).attr("id", "objDelete_" + count);
    });
    $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
    //------------to clear when new row---------
    /*$('#CircleAuthorities_' + count + '__Authority_Code').selectpicker('refresh');*/
/*    template.find('.bootstrap-select').replaceWith(function () { return $('select', this); });*/
    debugger;
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
    $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



    //---------------------------------

    //make numbers only inputs.
    //$('.numbers-only').keypress(function (e) {
    //    if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
    //});

    //$('#objAdd_' + (count - 1)).addClass('hide');
    //$('#objAdd_' + count).removeClass('hide');
    $('#objDelete_0').addClass('hide');
    $('.loadselectpicker').selectpicker('refresh');
}
function validateAuthorityPermission(val, id, index) {
    debugger;
    if (val.value == "") {
        $(val).addClass('is-invalid');
        $("#" + id + index + "").addClass('error-message');
        if (id == "authorityname_")
            $("#" + id + index + "").html("Please select circle authority");
        else {
            $("#" + id + index + "").html("Please add permission");
        }
    }
    else {
        $(val).removeClass('is-invalid');
        $("#" + id + index + "").removeClass('error-message');
        $("#" + id + index + "").html("");

        /* val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");*/
        val.value = val.value.replace(/(^\s*)|(\s*$)/gi, "");
        return val.value;
    }
}
function validateAuthorityPermissionById(elementId, spanId,id) {
    debugger;
    if ($(elementId).val() == "") {
        $(elementId).addClass('is-invalid');
        $("#" + spanId + "").addClass('error-message');
        if (id == "authorityname_")
            $("#" + spanId).html("Please select circle authority");
        else {
            $("#" + spanId).html("Please add permission");
        }
        return false;
    }
    else {
        $(elementId).removeClass('is-invalid');
        $("#" + spanId + "").removeClass('error-message');
        $("#" + spanId + "").html("");
        //trim(val);        
        return true;
    }
}
function DeleteResultRow(index) {
    debugger;
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
        var count = 0;
        count = $('tr[id^="row_OI_"]').length;
        if (count > 1) {
            $("#row_OI_" + index).remove();
            for (var j = index + 1; j < count; j++) {
                var template = $('#row_OI_' + j).clone(); // clone the row
                var code = $("#Results_" + j + "__Name").val();

                $.each(template.find('input, textarea'), function () {
                    var name = $(this).attr('name');
                    if (typeof name != 'undefined') {
                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('name', name);
                    }
                    var id = $(this).attr('id');
                    if (typeof id != 'undefined') {
                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                        $(this).attr('id', id);
                    }
                    //---------------------------------For row Increment When onkeyup
                    var onkeyup = $(this).attr('onkeyup');
                    if (onkeyup !== undefined) {
                        onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onkeyup', onkeyup);
                    }
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('select'), function () {
                    var name = $(this).attr('name');
                    name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('name', name);
                    var id = $(this).attr('id');
                    id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
                    $(this).attr('id', id);
                    //---------------------------------------For row Increment When onchange
                    var onchange = $(this).attr('onchange');
                    if (onchange !== undefined) {
                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                        // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
                        $(this).attr('onchange', onchange);
                    }
                    //---------------------------------------
                });
                $.each(template.find('button[type=button]'), function () {
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                    $(this).attr('onclick', onclick);
                });


                $.each(template.find('a'), function () {
                    debugger
                    var onclick = $(this).attr('onclick');
                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                    $(this).attr('onclick', onclick);
                });
                $.each(template.find('.addObj'), function () {

                    $(this).attr("id", "objAdd_" + (j - 1));
                });
               
                template.find('td[id=grid_slno]').text(j);
                $("#row_OI_" + j).html(template.html());
                $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));

           
                $("#Results_" + (j - 1) + "__Name").val(code);


            }
        }
        else {
            $("#Results_0__Name").val("");


        }
    }
    if ((count - 1) == index) {       
        $('#objAdd_' + index).addClass('hide');
        $('#objAdd_' + (index - 1)).removeClass('hide');
        }
    
        else {
            return false;
        }
    })
}

function validateCircleAuthorityDD(val, divId, id) {
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




function ActiveInactiveValidationCircle(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Circle/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#circle").prop("checked", true);
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