var prevTab = 'ActiveTab';
var currentTab = 'ACTIVE';








function ActiveInactiveValidationQuestionnaire(name, code) {
    debugger;

    $.ajax({

        type: 'POST',
        url: '/Master/ExitQuestionnaire/GetActiveInactiveValidation?name=' + name + '&code=' + code,
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
                    $("#questionnaire").prop("checked", true);
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




function OnTabClick(Tab, TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");



    $.ajax({

        type: 'GET',
        url: '/Master/ExitQuestionnaire/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#Quetionnairelist').html("");
                $('#Quetionnairelist').html(result);
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
    prevTab = Tab;
}


function AddNewOrEditQuestionSlider(mode) {
    debugger;
    /*    showSpinner();*/
    if (mode == "CREATE") {


        $.ajax({

            type: 'GET',
            url: '/Master/ExitQuestionnaire/NewQuestionCreate',
            dataType: 'html',
            //data: { DepartmentGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddSlider').html("");
                    $('#AddSlider').html(result);
                }


            },


            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
    else {
  


        $.ajax({

            type: 'GET',
            url: '/Master/ExitQuestionnaire/NewQuestionEdit',
            dataType: 'html',
            //data: { DepartmentGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddSlider').html("");
                    $('#AddSlider').html(result);
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



function AddNewOrEditClick(Type, GI, LogGI, index) {
    debugger;
    //currentTab = 'ACTIVE';

    if (Type == 'MODIFY') {

        if ($("#ExitInterviewQuestionnaireList_" + index + "__EditActive").val() == 'True')
            swal.fire(ApprovalPendingMsg);


        else {
    
            $.ajax({

                type: 'GET',
                url: '/Master/ExitQuestionnaire/Edit',
                dataType: 'html',
                data: { QuestionnaireGI: GI, LogGI: LogGI },
                success: function (result) {
                    debugger
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('body').append('<div class="form-overlay"></div>');
                        $('#AddNewOrEdit').html("");
                        $('#AddNewOrEdit').html(result);
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

        var url = "/Master/ExitQuestionnaire/Create";
        $.ajax({

            type: 'GET',
            url: '/Master/ExitQuestionnaire/Create',
            dataType: 'html',
            data: { QuestionnaireGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#AddNewOrEdit').html("");
                    $('#AddNewOrEdit').html(result);
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




function OnApproveClick(Type, GI, LogGI, index) {

    debugger;

    if ($("#ExitInterviewQuestionnaireList_" + index + "__MakerChecker").val() == 'True')
        swal.fire(MakerCheckerMsg);

    else {

  
        $.ajax({

            type: 'GET',
            url: '/Master/ExitQuestionnaire/Approve',
            dataType: 'html',
            data: { QuestionnaireGI: GI, LogGI: LogGI },
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


function onDeleteClick(Type, GI, LogGI, index) {

    debugger;

    if ($("#ExitInterviewQuestionnaireList_" + index + "__EditActive").val() == 'True')
        swal.fire(ApprovalPendingMsg);


    else {
      
        $.ajax({
            type: 'GET',
            url: '/Master/ExitQuestionnaire/Delete',
            dataType: 'html',
            data: { QuestionnaireGI: GI, LogGI: LogGI },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('body').append('<div class="form-overlay"></div>');
                    $('#DeleteSlider').html("");
                    $('#DeleteSlider').html(result);
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

function GetQuestionnairePopUp() {
    debugger
  
    $.ajax({
        type: 'GET',
        url: "/Master/ExitQuestionnaire/GetQuestionnairePopUp",
     
        dataType: 'html',

        success: function (Data) {
            debugger;         
            var m = $('.Exit_Questionnaire');
            m.find('.modal-body').html('');         
            m.find('.modal-body').html(Data);
           

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
  
}








function AddNewResultRow(tableId, i) {
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


        template.find('td[id=grid_slno]').text(count + 1);
        $.each(template.find('.addObj'), function () {
            $(this).attr("id", "objAdd_" + count);           
        });
        $.each(template.find('.delObj'), function () {
            $(this).attr("id", "objDel_" + count);           
        });
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
    debugger
        $('#objAdd_' + (count - 1)).addClass('hide');
        $('#objAdd_' + count).removeClass('hide');
        $('#objDel_' + count).removeClass('hide');
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



            // if (confirm("Do you want to delete..?"))


            //{
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
            debugger;
            $('#objAdd_' + index).addClass('hide');
            $('#objAdd_' + (index - 1)).removeClass('hide');
        }
    });
}







function SaveQuestionnaire(mode) {
    debugger;
    if (validateQuestionaire()) {
    debugger
    console.log($('#QuestionnaireSave').serialize());
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
                url: '/Master/ExitQuestionnaire/Save',
                dataType: 'json',
                data: $('#QuestionnaireSave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('AddNewDiv');
                            var url = "/Master/ExitQuestionnaire/GetList?TabIndex=" + currentTab;
                            $('#Quetionnairelist').load(url, function () { });
                            //$(".PendingTab").removeClass("active");
                            //$(".ActiveTab").addClass("active");
                            //$("#Quetionnairelist").removeClass("hide");

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



function SavePossibleResult() {
    debugger;
        
    debugger
    console.log($('#PossibleResultsave').serialize());
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
                url: '/Master/ExitQuestionnaire/SavePossibleResult',
                dataType: 'json',
                data: $('#PossibleResultsave').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {                        
                            $('.Exit_Questionnaire').modal('hide');

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
                url: '/Master/ExitQuestionnaire/Delete?QuestionGI=' + GI,
                dataType: 'json',
                /* data: $('#EvaluationDelete').serialize(),*/
              
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('delete');
                            debugger
                            var url = "/Master/ExitQuestionnaire/GetList?TabIndex=" + 'ACTIVE';
                            $('#Quetionnairelist').load(url, function () { });
                            $(".PendingTab").removeClass("active");
                            $(".ActiveTab").addClass("active");
                            $("#Quetionnairelist").removeClass("hide");
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            debugger;

                            url = "/Master/ExitQuestionnaire/Delete?QeustionGI=" + QeustionGI;
                            $('#DeleteSlider').load(url, function () { });
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
                url: '/Master/ExitQuestionnaire/Approve',
                dataType: 'json',
                data: $('#approveQuestionnnaire').serialize(),
                success: function (result) {

                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/ExitQuestionnaire/GetList?TabIndex=" + 'APPROVAL_PENDING';
                            debugger

                            $('#Quetionnairelist').load(url, function () { });
                            $(".PendingTab").addClass("active");
                            $(".ActiveTab").removeClass("active");
                            $("#Quetionnairelist").removeClass("hide");
                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ExitQuestionnaire/Approve?QeustionGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
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
    debugger;
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
                url: '/Master/ExitQuestionnaire/Reject',
                dataType: 'json',
                data: $('#approveQuestionnnaire').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('PendingApprove');
                            var url = "/Master/ExitQuestionnaire/GetList?TabIndex=" + currentTab;

                            $('#Quetionnairelist').load(url, { TabIndex: currentTab }, function () { });
                            /*window.location.href = url;*/
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            var url = "/Master/ExitQuestionnaire/Reject?QeustionGI=" + GI + "&LogGI=" + LogGI;
                            $('#ApproveSlider').load(url, function () { });
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
    if (currentTab == 'APPROVAL_PENDING') {
        var pendingTableLength = $('#ExitInterviewQuestionnaireList_Count').val();
        if (pendingTableLength > 0) {
            url = "/Master/ExitQuestionnaire/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }

    else if (currentTab == 'ACTIVE') {
        var activeTableLength = $('#ExitInterviewQuestionnaireList_Count').val();
        if (activeTableLength > 0) {
            url = "/Master/ExitQuestionnaire/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
    else if (currentTab == 'INACTIVE') {
        var inActiveTableLength = $('#ExitInterviewQuestionnaireList_Count').val();
        if (inActiveTableLength > 0) {
            url = "/Master/ExitQuestionnaire/ExportToExcel?TabIndex=" + currentTab;
            window.location.href = url;
        }
        else {
            swal.fire(NoDataFoundMsg);
        }
    }
}



function DisableDropDown(sender) {
    debugger;
    var Typevalue = $(sender).val();



    if (Typevalue == 1) {
        $("#resultDropdown").css("display", "block");
      
       
    } else {
        $("#resultDropdown").css("display", "none");
       /* $("#resultDropdown").addClass('hide');*/
    }
}


function validateResult(val, id, divId) {
    debugger;
    if (val.value == "") {
        $('.' + divId).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
        return false;
    }
    else {
        $('.' + divId).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
        return true;
    }
}

function validateQuestionaire() {
    var i = 0;
    if (!validateById('#Name', 'Qname')) {
        i++;
    }
    if (!validateById('#TypeCode', 'type')) {
        i++;
    }

    if ($('#TypeCode').val() == 1) {
        if (!validateResult(document.getElementById('listBox1'), 'result1', 'Result1')) {
            i++;
        }
    }
    if (!validateById('#GroupCode', 'GrpCode')) {
        i++;
    }
  
  
    if (i == 0)
        return true;
    else
        return false;
}


