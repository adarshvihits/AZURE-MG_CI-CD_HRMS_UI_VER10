



function getCombinationDetails(sender, cmbvalue) {
    debugger;
    var ddid = sender.value;
    /*showSpinner();*/

    /*    var url = '/DataBank/PoliciesCirculars/GetCombinationDetails?id=' + ddid + '&type=' + cmb;*/
    if (cmbvalue == 0) {
        selectedCombination1name = $('#MultipleCombination1 option:selected').text();
        selectedCombination2name = $('#MultipleCombination2 option:selected').text();
      

        if (selectedCombination1name == selectedCombination2name) {
            swal.fire("Please Choose Different Combination");
            $('#MultipleCombination1').val("");
         
     

            $.ajax({
                type: 'GET',

                url: '/Master/Quicklinks/GetCombinationDetails',
                dataType: 'html',
                data: { id: "", type: cmbvalue, combinationName: "" },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#CombinationList1').html("");
                        $('#CombinationList1').html(result);
                        $('#totcomb1Count').html($('#Combinations1_Count').val());
                    }
                    hideSmallSpinner();


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSmallSpinner();

                }
            });




        }


        else {
            $('#smallspinnerouter1').addClass('spinner-outer-small');
            $('#smallspinnerinner1').addClass('spinner-small');
            $('.spinner-outer-small').append(showSmallSpinner());
            $.ajax({
                type: 'GET',

                url: '/Master/Quicklinks/GetCombinationDetails',
                dataType: 'html',
                data: { id: ddid, type: cmbvalue, combinationName: selectedCombination1name },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#CombinationList1').html("");
                        $('#CombinationList1').html(result);
                        $('#totcomb1Count').html($('#Combinations1_Count').val());
                    }
                    hideSmallSpinner();


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSmallSpinner();

                }
            });

        }
      
     
        
    }
    else {
        if ($('#MultipleCombination1').val() == ddid) {
            swal.fire("Please Choose Different Combination");
            $('#MultipleCombination2').val("");




            $.ajax({

                type: 'GET',
                url: '/Master/Quicklinks/GetCombinationDetails',
                dataType: 'html',
                data: { id: "", type: cmbvalue, combinationName: ""},

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#CombinationList2').html("");
                        $('#CombinationList2').html(result);
                        $('#totcomb2Count').html($('#Combinations2_Count').val());
                    }
                    hideSmallSpinner();


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSmallSpinner();

                }
            });







            return false;
        }
        else {
            $('#smallspinnerouter2').addClass('spinner-outer-small');
            $('#smallspinnerinner2').addClass('spinner-small');
            $('.spinner-outer-small').append(showSmallSpinner());
            selectedCombination2name = $('#MultipleCombination2 option:selected').text();
            $.ajax({

                type: 'GET',
                url: '/Master/Quicklinks/GetCombinationDetails',
                dataType: 'html',
                data: { id: ddid, type: cmbvalue, combinationName: selectedCombination2name },

                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#CombinationList2').html("");
                        $('#CombinationList2').html(result);
                        $('#totcomb2Count').html($('#Combinations2_Count').val());
                    }
                    hideSmallSpinner();


                },


                error: function (xhr, textStatus, errorThrown) {
                    debugger
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                    hideSmallSpinner();

                }
            });
        }
    }
}


function GetData() {

}









function EditClick(GI,logGI) {
    debugger;

    var url = "/Master/Quicklinks/Edit?QuicklinksGI=" + GI;

    //var mul=$('#Multiple').val();
    //var count = $('#Combinations2_Count').val();
    //if (mul == 1) {
    //    $('.comb2').removeClass('hide');
    //}
    window.location.href = url;
}




function confirmDelete(GI) {
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
                url: '/Master/QuickLinks/Delete',
                dataType: 'json',
                data: { QuicklinksGI: GI },
                success: function (result) {
                    debugger
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/QuickLinks/GetQuicklinksList";
                            $('#QuicklinksList').load(url, function () { });
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







function SaveQuickLink(mode) {
    debugger;
    var modeName = "";
    if (mode == "Create")
        modeName = "Save";
    else
        modeName = mode;

    debugger;
    var i = 0;
    if (!validateById('#UrlDescriptions', 'QuicklinksName')) {
        i++;
    }
    if (!validateById('#Url', 'url')) {
        i++;
    }
    if (!isValidURL()) {
        i++;
    }
    if (!validateById('#VisibleFrom', 'visiblefrom')) {
        i++;
    }
    if (!validateById('#VisibleTo', 'visibleto')) {
        i++;
    }
   
    if ($('#AcceptanceRequired').val() == 1) {
        if (!validateById('#Priority', 'priority')) {
            i++;
        }
    }

    if (!validateById('#FileType', 'filetype')) {
        i++;
    }
 
    if ($('#combination1tblID').find('input[type="checkbox"]:checked').length < 1) {
        i++;
        swal.fire("Please select atleast one combination");
    }
    debugger;
    if ($('#Multiple:checked').val() == 1) {
        debugger;
        if ($('#combination2tblID').find('input[type="checkbox"]:checked').length < 1) {
            i++;
            swal.fire("Please select atleast one multiple combinations");
        }
    }

    if (i == 1) {
        swal.fire({
            text: "Do you want to Save",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var QLData = new FormData($('#Quicklinkssave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/Master/QuickLinks/Create',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: QLData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/Master/QuickLinks/Index";
                                window.location.href = url;

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
    else
        return false;
}









function RadioButtonChange(sender) {
    debugger;
    var radioval = sender.value;
    if (radioval == 1) {
        $('.comb2').removeClass('hide');
        $("#MultipleCombination1").find("option").eq(1).hide();
        $("#MultipleCombination2").find("option").eq(1).hide();
        if ($('#MultipleCombination1 option:selected').text() == "Employee") {
            $('#combination1tblbodyID').html("");
            $('#cmb1theadID').html("");
            $('#cmb1tID').html("");
            $('#MultipleCombination1').val("");
        }

        //$("#MultipleCombination1 option:contains('Employee')").remove();
        //$("#MultipleCombination2 option:contains('Employee')").remove();
    }

    else {
        debugger;
        /*$("#MultipleCombination1 option:contains('Department')").append($('<option></option>').val(1).html("Employee"));*/
        $("#MultipleCombination1").find("option").eq(1).show();
        $('#combination2tblbodyID').html("");
        $('#cmb2theadID').html("");
        $('#MultipleCombination2').val("");
        $('#totcomb2Count').html("");
        $('.comb2').addClass('hide');
    }

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
    $.ajax({

        type: 'GET',
        url: '/Master/Quicklinks/GetQuicklinksList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#QuicklinksList').html("");
                $('#QuicklinksList').html(result);
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





//function QuicklinksSave(mode) {
//    debugger;
//    if (validateQuicklinks()) {

//        swal.fire({
//            text: "Do you want to Save?",
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#d33',
//            confirmButtonText: 'Ok'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                $.ajax({

//                    type: 'POST',
//                    url: '/Master/Quicklinks/Save',
//                    dataType: 'json',
//                    data: $('#Quicklinkssave').serialize(),
//                    success: function (result) {
//                        if (result.success == true) {
//                            swal.fire(result.message).then(function () {
//                                CloseSlider('AddnewOrEditDiv');
//                                OnTabClick(prevTab, currentTab);
//                                tabClick(document.getElementById('tabsDropdownQuicklinks'))
//                                //var url = "/Master/Quicklinks/GetQuicklinksList?TabIndex=" + currentTab;
//                                //$('#QuicklinksList').load(url, function () { });

//                            });

//                        }

//                        else {
//                            swal.fire(result.message).then(function () {

//                            });
//                        }

//                    },
//                    error: function (xhr, textStatus, errorThrown) {
//                        debugger
//                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                            window.location.reload();
//                        }
//                        else {
//                            document.body.id = "kt_body";
//                            $('body').html(xhr.responseText);
//                        }
//                    }
//                });
//            }
//            else {
//                return false;
//            }
//        })

//    }
//    else {
//        return false;
//    }
//}










function OnAddNewClick() {
    var url = "/Master/Quicklinks/Create";
    window.location.href = url;
}








function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#QuickLinksList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

        $.ajax({

            type: 'GET',
            url: '/Master/Quicklinks/Approve',
            dataType: 'html',
            data: { QuicklinksGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#ApproveSlider').html("");
                    $('#ApproveSlider').html(result);
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


function onAllClick(chkid, tableid) {
    if ($('#' + chkid).is(':checked'))
        $('#' + tableid).find('input[type=checkbox]').prop('checked', true);
    else
        $('#' + tableid).find('input[type=checkbox]').prop('checked', false);
}








function validateQuicklinks() {
    var i = 0;
    if (!validateById('#Name', 'QuicklinksName')) {
        i++;
    }
    if (!validateById('#Url', 'url')) {
        i++;
    }
    if (!isValidURL()) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}






function ActiveInactiveValidation(name, code) {
    debugger

    $.ajax({

        type: 'POST',
        url: '/Master/Quicklinks/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#Activeinactivedpt").prop("checked", true);
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



//function CursorFocusOnSearch() {
//    if (focus == 0) {
//        $('.DataSearchField').focus();
//        focus = 1;
//    }
//    else {
//        focus = 0;
//    }
//}

function combination2searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList3");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination2tblID");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[0];
        if (td) {
            txtValue = td.textContent || td.innerText;
            if (txtValue.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }
}
function combination1searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList2");
    filter = input.value.toUpperCase();
    table = document.getElementById("combination1tblID");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;

    for (i = 0; i < tr.length; i++) {
        td1 = tr[i].getElementsByTagName("td")[0];
        td2 = tr[i].getElementsByTagName("td")[1];
        if (td1 || td2) {
            txtValue1 = td1.textContent || td1.innerText;
            txtValue2 = td2.textContent || td2.innerText;
            if (txtValue1.toUpperCase().indexOf(filter) > -1 || txtValue2.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
        }
    }

    //for (i = 0; i < tr.length; i++) {
    //    debugger;
    //    td = tr[i].getElementsByTagName("td")[0];
    //    if (td) {
    //        txtValue = td.textContent || td.innerText;
    //        if (txtValue.toUpperCase().indexOf(filter) > -1) {
    //            tr[i].style.display = "";
    //        } else {
    //            tr[i].style.display = "none";
    //            noOfRows--;
    //        }
    //    }
    //}
}






function clickOnExportToExcel() {

    debugger;
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#QuickLinksList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/Quicklinks/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#QuickLinksList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/Quicklinks/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#QuicklinksList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/Quicklinks/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}






function isValidURL() {
    debugger;
    var url = $("#Url").val();
    var res = url.match(/(http(s)?:\/\/.)?(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)/g);
    if (res == null)
        swal.fire("Please enter a valid URL");

};