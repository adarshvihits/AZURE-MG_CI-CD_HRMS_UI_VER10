var prevTab = 'tab1';
let currentTab = 1;
var needRemarks = 0;


function onOfferedSalaryChange() {
    debugger;
    maxSal = parseInt($('#MaximumSalary').val());
    minSal = parseInt($('#MinimumSalary').val());
    offerredSal = parseInt($('#ApplicantTracking_FinalApproval_OfferedSalary').val());
    if (offerredSal > maxSal) {
        needRemarks = 1;
    }
    else if (offerredSal < minSal) {
        needRemarks = 1;
    }
    else {
        needRemarks = 0;
        $('#ApplicantTracking_FinalApproval_Remarks').removeClass('is-invalid');
        $("#remarks").removeClass('error-message');
        $("#remarks").html("");
    }
}
function onRemarksChange() {
    debugger;
    if (needRemarks == 1) {
        validateById('#ApplicantTracking_FinalApproval_Remarks', 'remarks');
    }
    else {
        $('#ApplicantTracking_FinalApproval_Remarks').removeClass('is-invalid');
        $("#remarks").removeClass('error-message');
        $("#remarks").html("");
    }
}

function Tab(Tab,TabIndex) {
    debugger;  
    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#ApplicantTrackingList').html("");
                $('#ApplicantTrackingList').html(result);
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
function onSelectionChange(sender) {
    var val = $(sender).val();
    if (val != 1) {
        $('#noOflevels').addClass('hide');
    }
    else {
        $('#noOflevels').removeClass('hide');
    }
}
function onEvaluationStatusChange(sender) {
    var val = $(sender).val();
    if (val != 1) {
        $('#scr').addClass('hide');
    }
    else {
        $('#scr').removeClass('hide');
    }
}

function HoldOrReject(ResumeDataBankCode, status) {
    swal.fire({
        text: "Do you want to " + status+"?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            $.ajax({

                type: 'POST',
                url: '/DataBank/ApplicantTracking/HoldReject',
                dataType: 'json',
                data: { ResumeDataBankCode: ResumeDataBankCode, status: status},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('evaluationSlider');
                            Tab('tab1', 1);

                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
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
        else {
            return false;
        }
    })
}
function onApplicantNameClick(ResumeDataBankGI, ApplicantTrackingCode) {

    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetShortListedRARejectDetails',
        dataType: 'html',
        data: { Mode: 'ShortListed', ResumeDataBankGI: ResumeDataBankGI, ApplicantTrackingCode: ApplicantTrackingCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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
//Region RA APPROVAL
function onRAtabClick(TabIndex,span, TabName) {
    debugger;
    currentTab = TabIndex;
    currTabName = TabName;
    $("." + prevTab).removeClass("active");
    $("." + span).addClass("active");
    showSpinner();
  
    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetList',
        dataType: 'html',
        data: { TabIndex: TabIndex, TabName:currTabName},
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#ApplicantTrackingList').html("");
                $('#ApplicantTrackingList').html(result);
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
    prevTab = span;

}

function RAApproveOrReject(Mode, ResumeDataBankGI, ApplicantTrackingCode)
{
    debugger
    if (Mode == "Approve") {
        var data = { ResumeDataBankGI: ResumeDataBankGI, ApplicantTrackingCode: ApplicantTrackingCode}
        var url = '/DataBank/ApplicantTracking/GetSalaryCommunication';
    }
    else {
        var data = { Mode: 'RAReject', ResumeDataBankGI: ResumeDataBankGI, ApplicantTrackingCode: ApplicantTrackingCode }
        var url = '/DataBank/ApplicantTracking/GetShortListedRARejectDetails';
    }
       
    $.ajax({

        type: 'GET',
        url: url,
        dataType: 'html',
        data: data,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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

function RARejectSave() {
    debugger
    var i = 0; 
   
    if (!validateById('#RejectReasonCode', 'rejectreason'))
            i++;
   

    var msg = "";
  
        msg = "Do you want to Reject?";

    if (i == 0) {

        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                //    // Save it
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/ApplicantTracking/RAReject',
                    dataType: 'json',
                    data: $('#RaReject_form').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Rejected Successfully").then(function () {
                                CloseSlider('RaRejectSlider');
                                onRAtabClick(6, 'tab2',"PostApprovalList");
                               
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
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
            else {
                return false;
            }
        })
    }
}
function SalaryCommunicationSave() {
    debugger
    var i = 0;
    if (!validateById('#ApplicantTracking_FinalApproval_ExpectedSalary', 'expectedSal'))
        i++;
    if (!validateById('#ApplicantTracking_FinalApproval_OfferedSalary', 'offeredSal'))
        i++;
    if (!validateById('#InterviewSelection_Code', 'interviewSel'))
        i++;
    if (needRemarks == 1) {
        if (!validateById('#ApplicantTracking_FinalApproval_Remarks', 'remarks'))
            i++;
    }
    else {
        $('#ApplicantTracking_FinalApproval_Remarks').removeClass('is-invalid');
        $("#remarks").removeClass('error-message');
        $("#remarks").html("");
    }
    if ($('#InterviewSelection_Code').val() == 4) {
        if (!validateById('#InterviewSelection_RejectReasonCode', 'interviewSelect'))
            i++;
    }

    var msg = "";
    if (parseInt($('#ApplicantTracking_FinalApproval_OfferedSalary').val()) > parseInt($('#MaximumSalary').val()))
        msg = "Offered salary greater than maximum salary. Do you want to continue?";

    else if (parseInt($('#ApplicantTracking_FinalApproval_OfferedSalary').val()) < parseInt($('#MinimumSalary').val()))
        msg = "Offered salary less than minimum salary. Do you want to continue?";
    else
        msg = "Do you want to approve?";

    if (i == 0) {

        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                //    // Save it
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/ApplicantTracking/FinalApproval',
                    dataType: 'json',
                    data: $('#salryCommunicationSave').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Approved Successfully").then(function () {
                                CloseSlider('salryCommunication');
                                Tab('tab3', 3)
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
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
            else {
                return false;
            }
        })
    }
}


function onActionsClick(PreEnrollGI, Mode) {
    swal.fire({
        title: 'Do you want to '+Mode+'?',        
        inputLabel: 'Remarks',
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',
        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Remarks is mandatory'
                )
            }
        }
    }).then((result) => {
        if (result.value) {         
            this.OfferRevise(PreEnrollGI, Mode, result.value);
        }

    });
}

function OfferRevise(PreEnrollGI, Mode, Remarks) {

    $.ajax({
        type: 'POST',
        url: '/DataBank/ApplicantTracking/OfferRevise',
        dataType: 'json',
        data: { PreEnrollGI: PreEnrollGI, Mode: Mode, Remarks: Remarks },
        success: function (result) {
            debugger
            if (result.success == true) {
                if (Mode == 'Revise')
                    swal.fire("Revised Successfully");
                else
                    swal.fire("Rejected Successfully");

                onRAtabClick(7, 'tab3', 'Offer Rejected List');
            }

            else {
                swal.fire(result.message).then(function () {
                });
            }

           
        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });
}
function GetInterviewScheduleDetails(ResumeDataBankGI,InterviewStatusType) {
    //var url = "/DataBank/ApplicantTracking/GetInterviewScheduleDetails";
    //$('body').append('<div class="form-overlay"></div>');
    //$('#ApplicantTrackingSlider').load(url, { ResumeDataBankGI: ResumeDataBankGI, InterviewStatusType: InterviewStatusType }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetInterviewScheduleDetails',
        dataType: 'html',
        data: { ResumeDataBankGI: ResumeDataBankGI, InterviewStatusType: InterviewStatusType },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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
function GetInterviewEvaluationView(ResumeDataBankGI, InterviewStatusType) {
    //var url = "/DataBank/ApplicantTracking/GetInterviewEvaluationView";
    //$('body').append('<div class="form-overlay"></div>');
    //$('#ApplicantTrackingSlider').load(url, { ResumeDataBankGI: ResumeDataBankGI, InterviewStatusType: InterviewStatusType }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetInterviewEvaluationView',
        dataType: 'html',
        data: { ResumeDataBankGI: ResumeDataBankGI, InterviewStatusType: InterviewStatusType },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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
function GetEvaluationDetails(ResumeDataBankGI, ApplicantTrackingCode) {
    //var url = "/DataBank/ApplicantTracking/GetEvaluationDetails";
    //$('body').append('<div class="form-overlay"></div>');
    //$('#ApplicantTrackingSlider').load(url, { ResumeDataBankGI: ResumeDataBankGI}, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetEvaluationDetails',
        dataType: 'html',
        data: { ResumeDataBankGI: ResumeDataBankGI, ApplicantTrackingCode: ApplicantTrackingCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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

function GetApplicantTracker(ResumeDataBankGI) {
    debugger;
    //var url = "/DataBank/ApplicantTracking/GetApplicantTracker";
    //$('body').append('<div class="form-overlay"></div>');
    //$('#ApplicantTrackingSlider').load(url, { ResumeDataBankGI: ResumeDataBankGI }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/ApplicantTracking/GetApplicantTracker',
        dataType: 'html',
        data: { ResumeDataBankGI: ResumeDataBankGI },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('body').append('<div class="form-overlay"></div>');
                $('#ApplicantTrackingSlider').html("");
                $('#ApplicantTrackingSlider').html(result);
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

function AddMore() {

    debugger
    var count = 0;
    count = $("div[class*='interviewer']").length;

    var template = $('#evaluation_div_0').clone();// clone the row
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
        debugger
        if (onchange !== undefined) {
            /*onchange = onchange.replace(/\(0\)/g, '(' + count + ')');*/
            onchange = onchange.replace(/\,0\)/g, ',' + count);
            $(this).attr('onchange', onchange);
        }
        var onblur = $(this).attr('onblur');
        debugger
        if (onblur !== undefined) {
            /*onchange = onchange.replace(/\(0\)/g, '(' + count + ')');*/
            //onblur = onblur.replace(/\,0\)/g, ',' + count + ')');
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
    debugger
    $.each(template.find('a[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        if (onclick !== undefined) {
            onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onclick', onclick);
        }        
    });

    $.each(template.find('button[type=button]'), function () {
        var onclick = $(this).attr('onclick');
        if (onclick !== undefined) {
            onclick = onclick.replace(/\(0\)/g, '(' + count + ')');
            $(this).attr('onclick', onclick);
        }
    });

   

    $.each(template.find('.evaluation_div_dte'), function () {
        
        $(this).attr("id", "interviewDte_" + count);        
    });
    $.each(template.find('.evaluation_div_name'), function () {
        
        $(this).attr("id", "interviewerName_" + count);
        
    });
    $.each(template.find('.evaluation_div_feedback'), function () {

        $(this).attr("id", "feedback_" + count);
        
    });


    $('.outer_evaluation').append('<div  class ="interviewer" id="evaluation_div_' + count + '">' + template.html() + '  </div>');
    debugger
    //------------to clear when new row---------
    $('.kt_datepicker_2, .kt_datepicker_2_validate').datepicker({
        rtl: KTUtil.isRTL(),
        todayHighlight: true,
        orientation: "bottom left",


        format: "dd-M-yyyy",
        orientation: "auto",
        clearBtn: true
    });
    $('#evaluation_div_' + count).find("input[type=text]").val('');
    $('#evaluation_div_' + count).find("textarea").val('');
    $('#evaluation_div_' + count).find("select").val('');
}


function DeleteRw(index) {
    debugger
    swal.fire({
        text: "Do you want to delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {            
            var count = 0;
            count = $("div[class*='interviewer']").length;
            if (count > 1) {
                $("#evaluation_div_" + index).remove();
                for (var j = index + 1; j < count; j++) {
                    var template = $('#evaluation_div_' + j).clone(); // clone the row
                    var interDate = $("#ApplicantTrackingDTList_" + j + "__InterviewDate").val();
                    var empCode = $('#ApplicantTrackingDTList_' + j + '__Interviewer_EmployeeCode').val();

                    var feedback = $('#ApplicantTrackingDTList_' + j + '__Remarks').val();
                    var file = $('#ApplicantTrackingDTList_' + j + '__File_FileData').val();

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
                            //onchange = onchange.replace('(' + (j) + ')', '(' + (j - 1) + ')');
                            onchange = onchange.replace(',' + (j), ',' + (j - 1));
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
                    $.each(template.find('a[type=button]'), function () {
                        var onclick = $(this).attr('onclick');
                        onclick = onclick.replace(new RegExp("(" + j + ")", "g"), "" + (j - 1));
                        $(this).attr('onclick', onclick);
                    });
                    //$.each(template.find('.hasDatepicker'), function () {
                    //    $(this).removeClass('hasDatepicker');
                    //});




                    $("#evaluation_div_" + j).html(template.html());
                    $('#evaluation_div_' + j).attr('id', 'evaluation_div_' + (j - 1));




                    $("#ApplicantTrackingDTList_" + (j - 1) + "__InterviewDate").val(interDate);

                    $('#ApplicantTrackingDTList_' + (j - 1) + '__Interviewer_EmployeeCode').val(empCode);
                    $('#ApplicantTrackingDTList_' + (j - 1) + '__Remarks').val(feedback);
                    $('#ApplicantTrackingDTList_' + (j - 1) + '__File_FileData').val(file);
                }
            }
            else {
                $("#ApplicantTrackingDTList__0__InterviewDate").val(interDate);
                $('#ApplicantTrackingDTList__0__Interviewer_EmployeeCode').val(empCode);
                $('#ApplicantTrackingDTList__0__Remarks').val(feedback);
                $('#ApplicantTrackingDTList__0__File_FileData').val(file);
            }


        }
        else {
            return false;
        }
    })

}

function ScreeningSave() {
    debugger;
    var i = 0;
    if (!validateById('#InterviewSelection_Code', 'interviewSelect'))
        i++;

    if (!validateById('#Remarks', 'interviewRemarks'))
        i++;

    if ($('#InterviewSelection_Code').val() == 4) {
        if (!validateById('#InterviewSelection_RejectReasonCode', 'rejectreason'))
            i++;
    }
    var table = $('.common-table').DataTable();
    var currentPage = table.page();
    if (i == 0) {
    
    swal.fire({
        text: "Do you want to Proceed?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            $.ajax({

                type: 'POST',
                url: '/DataBank/ApplicantTracking/ProceesToInterview',
                dataType: 'json',
                data: $('#saveScreening').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Proceed Successfully").then(function () {
                            CloseSlider('shortlistSlider');
                            Tab('tab1', 1);
                            $.ajax({

                                type: 'GET',
                                url: '/DataBank/ApplicantTracking/GetList',
                                dataType: 'html',
                                data: { TabIndex: TabIndex },
                                success: function (result) {
                                    debugger
                                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                        window.location.reload();
                                    }

                                    else {
                                        $('#ApplicantTrackingList').html("");
                                        $('#ApplicantTrackingList').html(result);
                                        table = $('.common-table').DataTable();
                                        // Go back to the previous page
                                        table.page(currentPage).draw(false);
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

                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
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
        else {
            return false;
        }
    })
}
}

function Schedule(msg) {
    swal.fire({
        text: "Do you want to "+msg+"?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            $.ajax({

                type: 'POST',
                url: '/DataBank/ApplicantTracking/Schedule',
                dataType: 'json',
                data: $('#interviewSchedule').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('scheduleSlider');
                            CloseSlider('evaluationSlider');
                            Tab('tab2', 2)
                        });

                    }

                    else {
                        swal.fire(result.message).then(function () {

                        });
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
        else {
            return false;
        }
    })
}


function validateEvaluation() {
    for (let index = 0; index < noOfInterviews; index++) {
        validateById('#ApplicantTrackingDTList_' + index + '__InterviewDate', 'interviewDte_' + index)            
        //validateById('#ApplicantTrackingDTList_' + index + '__Interviewer_EmployeeCode', 'interviewerName_' + index)
        validateMultiselect('ApplicantTrackingDTList_' + index + '__Interviewer_EmployeeCode', 'interviewerName_' + index,'interNameDiv')
        validateById('#ApplicantTrackingDTList_' + index + '__Remarks', 'feedback_' + index)
        validateById('#ApplicantTrackingDTList_' + index + '__File_FileData', 'fileUpload_' + index)
    }
}
function SaveEvaluation() {
    var i = 0;
    for (let index = 0; index < noOfInterviews; index++) {
        if (!validateById('#ApplicantTrackingDTList_'+index+'__InterviewDate', 'interviewDte_'+index))
            i++;
        //if (!validateById('#ApplicantTrackingDTList_' + index + '__Interviewer_EmployeeCode', 'interviewerName_' + index))
        if (!validateMultiselect('ApplicantTrackingDTList_' + index + '__Interviewer_EmployeeCode', 'interviewerName_' + index, 'interNameDiv'))
            i++;
        if (!validateById('#ApplicantTrackingDTList_'+index+'__Remarks', 'feedback_'+index))
            i++
        if (!validateById('#ApplicantTrackingDTList_' + index + '__File_FileData', 'fileUpload_' + index))
            i++
    }
    debugger;
    if (!validateById('#InterviewSelection_Code', 'interviewSelect'))
        i++
    if ($('#InterviewSelection_Code').val() == 4) {
        if (!validateById('#InterviewSelection_RejectReasonCode', 'rejectreason'))
            i++;
    }

    if (i == 0) {

        swal.fire({
            text: "Do you want to save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                //    // Save it
                var evalData = new FormData($('#interviewSchedule')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/ApplicantTracking/SaveEvaluation',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: evalData,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Saved Successfully").then(function () {
                                CloseSlider('evaluationSlider');
                                Tab('tab2', 2)
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {

                            });
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
            else {
                return false;
            }
        })
    }
}

function Preview() {
    debugger
    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
    window.open(path, "_blank");
}

function viewResume(GI) {
    debugger;
    var url = "/DataBank/ApplicantTracking/ResumeView";
   /* window.location.href = url + '?ResumeGI=' + GI;*/
    window.open(url + '?ResumeGI=' + GI, '_blank');
}

function rejectReasonChange() {
    debugger;
    Name = $('#InterviewSelection_Code option:selected').text();
    if (Name == 'Rejected') {
        $('#rejectReason').removeClass('hide');
    }
    else {
        $('#rejectReason').addClass('hide');
    }
  
}

function ExportToExcel() {
    debugger;

    /*    var screeningshortlisttable = document.getElementById("screeningOrShortlistingTable").rows.length - 1;*/
    var screeningshortlisttable = $('#ApplicantTrackingList_Count').val();
    if (screeningshortlisttable > 0) {
        url = "/DataBank/ApplicantTracking/ScreeningShortlistExporttoexcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}



function onExportToExcelClick() {
    debugger;

    /*    var screeningshortlisttable = document.getElementById("screeningOrShortlistingTable").rows.length - 1;*/
    var screeningshortlisttable = $('#ApplicantTrackingList_Count').val();
    if (screeningshortlisttable > 0) {
        url = "/DataBank/ApplicantTracking/InterviewAndEvaluationExportToExcel";
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}



function RAapprovalExportExcel() {
    debugger;

    /*    var screeningshortlisttable = document.getElementById("screeningOrShortlistingTable").rows.length - 1;*/
    var RAapprovalTable = $('#ApplicantTrackingList_Count').val();
    if (RAapprovalTable > 0) {
        url = "/DataBank/ApplicantTracking/RAapprovalExportToExcel?Tab=" + currentTab;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}

function GetInterviewDetails() {

    var ApplicantTrackingCode = $('#ApplicantTrackingCode').val();
    $.ajax({

        type: 'POST',
        url: '/DataBank/ApplicantTracking/GetInterviewDetails',        
        dataType: 'html',
        data: { ApplicantTrackingCode: ApplicantTrackingCode},
        success: function (result) {
           
            var m = $('.interviewDetailsPopup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(result);
            m.modal('show');

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
