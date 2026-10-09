////var prevTab = 'ActiveTab';
////var currentTab;
var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';


function onAddNewClick() {
    debugger;
    var Code = 1;
    var url = "/Master/RewardsMaster/RewardsSave";
    $('.rewardsSaveclose').load(url, function () { });
    $('#newRequestSlider').removeClass('hide');
    $('.rewardsSaveclose').show();
}
//function AddNewOrEdit(Action, GI, LogGI) {
//    debugger;
//    var Code = 1;
//    debugger;
//    if (Action == 'Edit') {
//        var url = "/Master/RewardsMaster/Edit?RewardsGI=" + GI + "&LogGI=" + LogGI;
//    }
//    else {
//        var url = "/Master/RewardsMaster/Create";
//    }
//    $('.rewardsSaveclose').load(url, function () { });
//    $('#newRequestSlider').removeClass('hide');
//    $('.rewardsSaveclose').show();
//}

function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}

function onChkBoxClick(sender, allCheckBox, tableId) {
    debugger;
    if ($(sender).is(':checked')) {

        debugger;
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length - 1)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
    else {
        debugger;
        $('#FullPermission').prop('checked', false);
        if ($('#' + tableId).find('input[type="checkbox"]:checked').length == $('#' + tableId).find('input[type="checkbox"]').length)
            $('#' + allCheckBox).prop('checked', true);
        else
            $('#' + allCheckBox).prop('checked', false);
    }
}

function AddRowGrid(tableId,i) {
    debugger;
    

        var count = 0;
    count = $('#' + tableId + ' tr[id^="row_OI_"]').length;
    //if (count != 5) {
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

            var onblur = $(this).attr('onblur');
            if (onblur !== undefined) {
                onblur = onblur.replace(/\,0\)/g, ',' + count + ')');
                $(this).attr('onblur', onblur);
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
                onchange = onchange.replace(/\(0\)/g, '(' + count + ')');
                $(this).attr('onchange', onchange);
            }
            //---------------------------------------
        });

        $.each(template.find('button[type=button]'), function () {
            var onclick = $(this).attr('onclick');
            onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onclick', onclick);
        });

    
        $.each(template.find('.pointCriteriaclass'), function () {

            $(this).attr("id", "pointname_" + count);
        });
        $.each(template.find('.pointclass'), function () {

            $(this).attr("id", "point_" + count);
        });

        template.find('td[id=oi_slno]').text(count + 1);    
        $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
        //------------to clear when new row---------

     

        $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



        //---------------------------------

        //make numbers only inputs.
        $('.numbers-only').keypress(function (e) {
            if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
        });
    //}
    //else {
    //    swal.fire('Limit Exceeded.Maximum level of point criteria is 5');
    //}
}

function ActiveInactiveValidationRewards(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/RewardsMaster/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactiverewards").prop("checked", true);
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
//function DeleteRw(index) {
//    debugger;
//    if (confirm("Do you want to delete..?")) {
//        var count = 0;
//        count = $('tr[id^="row_OI_"]').length;
//        if (count > 1) {
//            $("#row_OI_" + index).remove();
//            for (var j = index + 1; j < count; j++) {
//                var template = $('#row_OI_' + j).clone(); // clone the row
//                var code = $("#RewardVsPointCriteriaList_" + j + "__Name").val();
//                var desc = $("#RewardVsPointCriteriaList_" + j + "__Point").val();
               
//                $.each(template.find('input, textarea'), function () {
//                    var name = $(this).attr('name');
//                    if (typeof name != 'undefined') {
//                        name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
//                        $(this).attr('name', name);
//                    }
//                    var id = $(this).attr('id');
//                    if (typeof id != 'undefined') {
//                        id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
//                        $(this).attr('id', id);
//                    }
//                    //---------------------------------For row Increment When onkeyup
//                    var onkeyup = $(this).attr('onkeyup');
//                    if (onkeyup !== undefined) {
//                        onkeyup = onkeyup.replace('(' + (j) + ')', '(' + (j - 1) + ')');
//                        $(this).attr('onkeyup', onkeyup);
//                    }
//                    //---------------------------------------For row Increment When onchange
//                    var onchange = $(this).attr('onchange');
//                    if (onchange !== undefined) {
//                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
//                        $(this).attr('onchange', onchange);
//                    }
//                    //---------------------------------------
//                });
//                $.each(template.find('select'), function () {
//                    var name = $(this).attr('name');
//                    name = name.replace(new RegExp("" + j, "g"), "" + (j - 1));
//                    $(this).attr('name', name);
//                    var id = $(this).attr('id');
//                    id = id.replace(new RegExp("" + j, "g"), "" + (j - 1));
//                    $(this).attr('id', id);
//                    //---------------------------------------For row Increment When onchange
//                    var onchange = $(this).attr('onchange');
//                    if (onchange !== undefined) {
//                        onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
//                        // onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
//                        $(this).attr('onchange', onchange);
//                    }
//                    //---------------------------------------
//                });
//                $.each(template.find('button[type=button]'), function () {
//                    var onclick = $(this).attr('onclick');
//                    onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
//                    $(this).attr('onclick', onclick);
//                });
//                //$.each(template.find('.hasDatepicker'), function () {
//                //    $(this).removeClass('hasDatepicker');
//                //});
//                //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
//                //    dateFormat: "dd-M-yy", changeMonth: true,
//                //    changeYear: true
//                //});
       
//                $("#row_OI_" + j).html(template.html());
//                $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));
//                $("#RewardVsPointCriteriaList_" + (j - 1) + "__Name").val(code);
//                $("#RewardVsPointCriteriaList_" + (j - 1) + "__Point").val(desc);
               
//            }
//        }
//        else {
//            $("#RewardVsPointCriteriaList_0__Name").val("");
//            $("#RewardVsPointCriteriaList_0__Point").val("");
     
//        }
//    }
//}





function DeleteRw(index) {
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
                    var code = $("#RewardVsPointCriteriaList_" + j + "__Name").val();
                    var desc = $("#RewardVsPointCriteriaList_" + j + "__Point").val();

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
                    //$.each(template.find('.hasDatepicker'), function () {
                    //    $(this).removeClass('hasDatepicker');
                    //});
                    //$('#' + tableId + ' tr[id="row_OI_' + count + '"] .datepicker').datepicker({
                    //    dateFormat: "dd-M-yy", changeMonth: true,
                    //    changeYear: true
                    //});

                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));
                    $("#RewardVsPointCriteriaList_" + (j - 1) + "__Name").val(code);
                    $("#RewardVsPointCriteriaList_" + (j - 1) + "__Point").val(desc);

                }
            }


           
        }
        else {
            $("#RewardVsPointCriteriaList_0__Name").val("");
            $("#RewardVsPointCriteriaList_0__Point").val("");
        }
    })
}












function ontabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    var url = "/Master/RewardsMaster/GetRewardsList";
   
    if (TabIndex != 'PendingTab')
        $('#ActiveList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    else
        $('#PendingList').load(url, { TabIndex: TabIndex }, function () { hideSpinner(); });
    currentTab = TabIndex;
    $('#tabsDropdownRewards').val(currentTab);
    prevTab = Tab;
}
function tabClick(val) {
    debugger;
    showSpinner();
    currentTab = $(val).val();
    $("." + prevTab).removeClass("active");
    if (currentTab == "ACTIVE")
        prevTab = 'ActiveTab'
    else if (currentTab == "INACTIVE")
        prevTab = 'InctiveTab';
    else
        prevTab = 'PendingTab';

    $("." + prevTab).addClass("active");
    //var url = "/Master/RewardsMaster/GetRewardsList";
    //$('#ActiveList').load(url, { TabIndex: currentTab }, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/Master/RewardsMaster/GetRewardsList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ActiveList').html("");
                $('#ActiveList').html(result);
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




function onAddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
   
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#RewardsList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
         
            $.ajax({

                type: 'GET',
                url: '/Master/RewardsMaster/Edit',
                dataType: 'html',
                data: { RewardGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#newRequestSlider').html("");
                        $('#newRequestSlider').html(result);
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
       

        var url = "/Master/RewardsMaster/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/RewardsMaster/Create',
            dataType: 'html',
            data: { RewardGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#newRequestSlider').html("");
                    $('#newRequestSlider').html(result);
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



function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#RewardsList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
        


        $.ajax({
            type: 'GET',
            url: '/Master/RewardsMaster/Delete', 
            dataType: 'html',
            data: { RewardGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#deleteRewardstSlider').html("");
                    $('#deleteRewardstSlider').html(result);
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




function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#RewardsList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        debugger;
       

        $.ajax({

            type: 'GET',
            url: '/Master/RewardsMaster/Approve',
            dataType: 'html',
            data: { RewardGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#approveSlider').html("");
                    $('#approveSlider').html(result);
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
function confirmRewardsApprove(GI, LogGI) {

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
                url: '/Master/RewardsMaster/Approve',
                dataType: 'json',
                data: $('#approveRewards').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/RewardsMaster/GetRewardsList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".ActiveTab").removeClass("active");
                            $(".PendingTab").addClass("active");
                            $("#ActiveList").removeClass("hide");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/Master/RewardsMaster/Approve?RewardGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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
                url: '/Master/RewardsMaster/Delete',
                dataType: 'json',
                data: $('#deleteReward').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('DeleteDiv');
                            var url = "/Master/RewardsMaster/GetRewardsList?TabIndex=" + 'ACTIVE';
                            $('#ActiveList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/RewardsMaster/Delete?RewardGI=" + GI + "&LogGI=" + LogGI;
                            $('#DeleteDiv').load(url, function () { });
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



function confirmRewardsReject(GI, LogGI) {

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
                url: '/Master/RewardsMaster/Reject',
                dataType: 'json',
                data: $('#approveRewards').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/RewardsMaster/GetRewardsList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#ActiveList').load(url, function () { });
                            $(".ActiveTab").removeClass("active");
                            $(".PendingTab").addClass("active");
                            $("#ActiveList").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/RewardsMaster/Approve?RewardGI=" + GI + "&LogGI=" + LogGI;
                            $('#approveSlider').load(url, function () { });
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


function SaveRewards(mode) {
    debugger;
    if (validateRewards()) {
        debugger
        //console.log($('#rewardsSave').serialize());
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
                url: '/Master/RewardsMaster/Save',
                dataType: 'json',
                data: $('#rewardsSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('rewardsSaveclose');
                            var url = "/Master/RewardsMaster/GetRewardsList?TabIndex=" + currentTab;
                            $('#ActiveList').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#ActiveList").removeClass("hide");

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
    else {
        return false;
    }
}




//function validateRewards() {
//    debugger;
//    if ($('.Rname').val() == '') {

//        $('#Name').addClass('is-invalid');
//        $("#rewardsname").addClass('error-message');
//        $('#rewardsname').html("This field is required");
//        $('#Name').focus();
//        return false;
//    }

//    else if ($('.RshtName').val() == '') {

//        $('#ShortName').addClass('is-invalid');
//        $("#rewardsshortname").addClass('error-message');
//        $('#rewardsshortname').html("This field is required");
//        $('#ShortName').focus();
//        return false;
//    }
//    else if ($('#rewDep').find('input[type=checkbox]').is(':checked') == false) {
//        swal.fire("Select atleast one department")
//        return false;
//    }
//    //else if ($('.dept').val() == '') {

//    //    $('#RewardsGridFill[i].Name').addClass('is-invalid');
//    //    $("#depart").addClass('error-message');
//    //    $('#depart').html("Select atleast one department");
//    //    $('#RewardsGridFill[i].Name').focus();
//    //    return false;
//    //}
//    else if ($('#RewardVsPointCriteriaList_0__Name').val() == '') {

//        swal.fire("Atleast one Point Criteria required");
//        $('#RewardVsPointCriteriaList_0__Name').focus();
//        return false;
//    }
//    else if ($('#RewardVsPointCriteriaList_0__Point').val() == '') {
//        swal.fire("Please file the Point");
//        $('#RewardVsPointCriteriaList_0__Point').focus();
//        return false;
//    }
  

//}
function validateRewards() {
    var i = 0;
    var TableLength = document.getElementById("PointCriteriaTable").rows.length - 1;
    for (j = 0; j < TableLength; j++) {
        if (!validateById('#RewardVsPointCriteriaList_' + j + '__Name', 'pointname_' + i)) {
            i++;
        }
        if (!validateById('#RewardVsPointCriteriaList_' + j + '__Point', 'point_' + i)) {
            i++;
        }
    }
    if (!validateById('#Name', 'rewardsname')) {
        i++;
    }
    if (!validateById('#ShortName', 'rewardsshortname')) {
        i++;
    }
   
    if ($('#rewDep').find('input[type="checkbox"]:checked').length == 0) {
        i++;
        swal.fire("Please Select atleast one department");
    }

    if (i == 0)
        return true;
    else
        return false;
}


 
function ExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#RewardsList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/RewardsMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else {
        var activeTableLength = $('#RewardsList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/RewardsMaster/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    

}
function process(input) {
    debugger;
    let value = input.value;
    let numbers = value.replace(/[^0-9]/g, "");
    input.value = numbers;
}