var prevTab = 'active';
var currentTab = 'ACTIVE';

function OntabClick(Tab, TabIndex) {
    $.ajaxSetup({
        async: false
    });
    debugger;
    showSpinner();
    //currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //$('#tabsDropdownSkill').val(currentTab);

    $.ajax({

        type: 'GET',
        url: '/Master/InsurancePolicy/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#insurancePolicyList').html("");
                $('#insurancePolicyList').html(result);
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


    currentTab = TabIndex;

    prevTab = Tab;
}

function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {
        debugger
        if ($("#InsurancePolicyList_" + index + "__EditActive").val() == 'True') {
            swal.fire(ApprovalPendingMsg);

        }
        else {
            //var url = "/Master/AssetMaster/Edit?AssetGI=" + GI + "&LogGI=" + LogGI;

            //$('#newRequestSlider').load(url, function () { });
            //$('body').append('<div class="form-overlay"></div>');

            $.ajax({

                type: 'GET',
                url: '/Master/InsurancePolicy/Edit',
                dataType: 'html',
                data: { InsurancePolicyGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('.sliderView').html("");
                        $('.sliderView').html(result);
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
        debugger           
        $.ajax({

            type: 'GET',
            url: '/Master/InsurancePolicy/Create',
            dataType: 'html',            
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('.sliderView').html("");
                    $('.sliderView').html(result);
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


function validateInsurancePolicy() {
    var i = 0;



    if (!validateById('#Name', 'name')) {
        i++;
    }
    if (!validateById('#ShortName', 'shortName')) {
        i++;
    }
    if (!validateById('#InsuranceCompany_Code', 'ICompany')) {
        i++;
    }

    if (i == 0)
        return true;
    else
        return false;
}


function InsurancePolicySave(mode) {
    debugger;

    if (validateInsurancePolicy()) {        
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
                    url: '/Master/InsurancePolicy/Save',
                    dataType: 'json',
                    data: $('#InsurancePolicySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('AddDiv');
                                var url = "/Master/InsurancePolicy/GetList";
                                $('#insurancePolicyList').load(url, { TabIndex: currentTab }, function () { });
                                /* window.location.href = url;*/
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


function onApproveClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#InsurancePolicyList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {     

        $.ajax({

            type: 'GET',
            url: '/Master/InsurancePolicy/Approve',
            dataType: 'html',
            data: { InsurancePolicyGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('.sliderView').html("");
                    $('.sliderView').html(result);
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

function ConfirmApprove(GI, LogGI) {
    //$.ajaxSetup({
    //    async: false
    //});
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
                url: '/Master/InsurancePolicy/Approve',
                dataType: 'json',

                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approvePolicy');
                            var url = "/Master/InsurancePolicy/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger;
                            $('#insurancePolicyList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $(".InctiveTab").removeClass("active");
                            /* $("#ActiveList").addClass("hide");*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/InsurancePolicy/Approve?InsurancePolicyGI=" + GI + "&LogGI=" + LogGI;
                            $('.sliderView').load(url, function () { });
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

function ConfirmReject(GI, LogGI) {

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
                url: '/Master/InsurancePolicy/Reject',
                dataType: 'json',
                data: $('#approve').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('approvePolicy');
                            var url = "/Master/InsurancePolicy/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#insurancePolicyList').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $(".InctiveTab").removeClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/InsurancePolicy/Approve?InsurancePolicyGI=" + GI + "&LogGI=" + LogGI;
                            $('.sliderView').load(url, function () { });
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


function onDeleteClick(GI, LogGI, Type, index) {

    debugger;

    if ($("#InsurancePolicyList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {

        $.ajax({
            type: 'GET',
            url: '/Master/InsurancePolicy/Delete',
            dataType: 'html',
            data: { InsurancePolicyGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('.sliderView').html("");
                    $('.sliderView').html(result);
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

function confirmDelete(GI, LogGI) {

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
                url: '/Master/InsurancePolicy/Delete',
                dataType: 'json',
                data: $('#insurancePolicyDelete').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('deletePolicy');
                            var url = "/Master/InsurancePolicy/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            $('#insurancePolicyList').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");

                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/InsurancePolicy/Delete?AccountGI=" + GI + "&LogGI=" + LogGI;
                            $('.sliderView').load(url, function () { });
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


function ExportToExcel() {
    debugger;    
    var TableLength = $('#InsurancePolicyList_Count').val();
        if (TableLength > 0) {
            url = "/Master/InsurancePolicy/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }          
}


//----------Insurance Company-----------------------------

function AddRowGrid(tableId, i) {
    debugger;


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

    $.each(template.find('a'), function () {
            var onclick = $(this).attr('onclick');
            onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onclick', onclick);
        });

        $.each(template.find('.InsCompany'), function () {

            $(this).attr("id", "ICompanyName_" + count);
        });

        template.find('td[id=oi_slno]').text(count + 1);
        $.each(template.find('.addObj'), function () {
            $(this).attr("id", "objAdd_" + count);
        });
        $('#' + tableId + '').append('<tr id="row_OI_' + count + '">' + template.html() + '  </tr>');
        //------------to clear when new row---------



        $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=text]').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] input[type=hidden]').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] textarea').val('');
        $('#' + tableId + ' tr[id="row_OI_' + count + '"] select').val('');



        //---------------------------------

        //make numbers only inputs.
        $('.numbers-only').keypress(function (e) {
            if (e.which == 8 || (e.which == 46 && $(this).val().indexOf('.') == -1) || (e.which > 47 && e.which < 58)) { } else { return false; }
        });
  
    $('#objAdd_' + (count - 1)).addClass('hide');
    $('#objAdd_' + count).removeClass('hide');
}


function DeleteRow(index) {
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

                    var Name = $("#InsuranceCompanyLt_" + j + "__Name").val();
                    var Code = $("#InsuranceCompanyLt_" + j + "__Code").val();

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
                            /*    onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');*/
                            onchange = onchange.replace(/\(0\)/g, '(' + (j - 1) + ')');
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
                    template.find('td[id=oi_slno]').text(j);
                    $("#row_OI_" + j).html(template.html());
                    $('#row_OI_' + j).attr('id', 'row_OI_' + (j - 1));

                    $("#InsuranceCompanyLt_" + (j - 1) + "__Name").val(Name);
                    $("#InsuranceCompanyLt_" + (j - 1) + "__Code").val(Code);




                }
            }
            else {
                $("#InsuranceCompanyLt_0__Name").val("");
                $("#InsuranceCompanyLt_0__Code").val("");
            }
            if ((count - 1) == index) {
                debugger;
                $('#objAdd_' + index).addClass('hide');
                $('#objAdd_' + (index - 1)).removeClass('hide');
            }
        }
        else {
            return false;
        }
    })
           
}


function InsuranceCompanySave() {
    debugger;

    var i = 0;
    var TableLength = document.getElementById("insuranceCompanyTable").rows.length - 1;
    for (j = 0; j < TableLength; j++) {
        if (!validateById('#InsuranceCompanyLt_' + j + '__Name', 'ICompanyName_' + j)) {
            i++;
        }
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
                    url: '/Master/InsurancePolicy/InsuranceCompanySave',
                    dataType: 'json',
                    data: $('#InsuranceCompanySave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.bd-example-modal-lg').modal('hide')
                                var url = "/Master/InsurancePolicy/GetList";
                                $('#insurancePolicyList').load(url, { TabIndex: currentTab }, function () { });
                                /* window.location.href = url;*/
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
    
}