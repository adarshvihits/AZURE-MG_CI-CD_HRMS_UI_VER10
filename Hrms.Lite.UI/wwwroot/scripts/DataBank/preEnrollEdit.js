function savePreEnrollEdit() {
    debugger;
    var renewaldate = $('#BasicInfo_RenewalDate').val();
    var i = 0;
   
        url = "/DataBank/PreEnrollmentEdit/Edit";
   
    var a = $("#BasicInfo_File_FileData").length;
    if ($("#FiledValidations_1__Applicability").val() != "False") {
        if ($("#BasicInfo_File_FileName").val() == "") {
            swal.fire('Candidate photo is required');
            i++;
        }
    }
    if (!validateById("#BasicInfo_FullName", 'fullname')) {
        $('#BasicInfo_FullName').focus();
        i++;
    }

    if (!validateMobile('PermanentAddress_MobileNo', 'mobile')) {
        debugger;
        $('#PermanentAddress_MobileNo').focus();
        i++;
    }
    if (!validateEmail(document.getElementById('PermanentAddress_MailID'), 'email', 'FiledValidations_0__Applicability')) {
        $('#PermanentAddress_MailID').focus();
        i++;
    }

    if (!validateDOJ(document.getElementById('BasicInfo_AcceptedDOJ'), 'doj')) {

        $('#BasicInfo_AcceptedDOJ').focus();
        $('.dojpicker').datepicker('hide');
        i++;
    }


    if (!validateById("#PermanentAddress_House", 'house')) {
        $('#PermanentAddress_House').focus();
        i++;
    }
    if (!validateById("#PermanentAddress_Street", 'street')) {
        $('#PermanentAddress_Street').focus();
        i++;
    }
    if (!validateById("#PermanentAddress_City", 'city')) {
        $('#PermanentAddress_City').focus();
        i++;
    }
    if (!validatePin($('#PermanentAddress_PinCode'), 'pin')) {
        $('#PermanentAddress_PinCode').focus();
        i++;
    }
    if (!validateById("#PermanentAddress_Country_Code", 'country')) {
        $('#PermanentAddress_Country_Code').focus();
        i++;
    }
    if (!validateById("#PermanentAddress_State_Code", 'state')) {

        $('#PermanentAddress_State_Code').focus();
        i++;
    }
    if (!validateById("#PermanentAddress_District_Code", 'district')) {
        $('#PermanentAddress_District_Code').focus();
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {

                var MPPersonalInfo = new FormData($('#preEditSave')[0]);
                MPPersonalInfo.append('Renewaldt', renewaldate);
                $.ajax({
                    type: 'POST',
                    url: url,                  
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: MPPersonalInfo,                 
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                               
                                    var url = "/DataBank/PreEnrollmentEdit";
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
        });


    }
}

function CTCCalculationApprove() {
    debugger;
    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    var TakeHome = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {//Gross Salary
        var Amt = parseFloat($("#Compensation_FixedAllowance_" + i + "__Amount").val());

        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;

        TotGrossAmt = TotGrossAmt + Amt;

        $("#GrossSalary").html(TotGrossAmt);
        $("#Compensation_GrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {//CTC
        var Amt = parseFloat($("#Compensation_FixedProvisions_" + i + "__Amount").val());
        var CtcApp = parseInt($("#Compensation_FixedProvisions_" + i + "__CtcApplicable").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        if (CtcApp == 2) {
            TotCTCAmt = TotCTCAmt + Amt;
        }


    });

    $("#CTC").html(TotCTCAmt + TotGrossAmt);
    $("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#Compensation_FixedDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;

        $("#totDeductions").html(TotDedAmt);
        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home

    $("#takeHome").html(TakeHome);
    //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);


}

function PopUp_PreviewEdit() {
    debugger
    var LocationCode = $('#JobDetails_Location_Code').val();
    var WageGradeCode = $('#JobDetails_WageGrade_Code').val();
    var CTC = $('#Compensation_CTC').val();

    if (WageGradeCode != null && WageGradeCode != "" && LocationCode != null && LocationCode != "" && parseFloat(CTC) > 0) {
        debugger
        var MPPersonalInfo = new FormData($('#preEditSave')[0]);

        $.ajax({
            type: 'POST',
            url: '/DataBank/PreEnrollmentTest/SalarySummary',
            dataType: 'html',
            contentType: false,
            processData: false,
            data: MPPersonalInfo,//{'PreEnrollGI' :GI},
            success: function (Data) {
                console.log(Data);
                var m = $('.salary_summary');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
                m.modal('show');
                //  alert($('#BasicInfo_AcceptedDOJ').val());
                $('#edoj').text('EDOJ:    ' + $('#BasicInfo_AcceptedDOJ').val());
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log(xhr.responseText);
                swal.fire(xhr.responseText);

            }
        });
    }

}
function ApplyFilter(mode) {
    debugger  
    $("#SearchId").val($('#FilterList_Code').val())
    debugger;
    if (parseInt($("#Tabindex").val()) = 3)
        url = '/DataBank/PreEnrollmentEdit/ApplyFilter';
    else
        url = '/DataBank/PreEnrollmentEditApproval/ApplyFilter';

  
        if (mode == 'apply') {
            if (filterApplicable >= 1) {
                $.ajax({

                    type: 'POST',
                    url: url,
                    dataType: 'html',
                    data: $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger
                        CloseSlider('basicFilter');



                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#OnBoardingList').html("");
                            $('#OnBoardingList').html(result);
                        }
                        hideSpinner();
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }

                });
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                $.ajax({

                    type: 'POST',
                    url: url,
                    dataType: 'html',
                    data: $('#BasicFilter').serialize(),
                    success: function (result) {
                        debugger
                        CloseSlider('basicFilter');

                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#OnBoardingList').html("");
                            $('#OnBoardingList').html(result);
                        }
                        hideSpinner();
                    },


                    error: function (xhr, textStatus, errorThrown) {
                        document.body.id = "kt_body";
                        $('body').html(xhr.responseText);
                    }

                });
                $('#filterForm').addClass('filter-apply');
            }
        }
        else {
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            $.ajax({

                type: 'POST',
                url: url,
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger


                  

                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }
                    else {
                        $('#OnBoardingList').html("");
                        $('#OnBoardingList').html(result);
                    }
                    hideSpinner();
                },


                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });

        }
   


}
function approveRejectEdit(logcode, mode) {
    if (mode == 'APPROVE')
        textt = 'Do you want to Approve?'
    else
        textt = 'Do you want to Reject?'
    debugger
    swal.fire({
        text: textt,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/Databank/PreEnrollmentEditApproval/ApproveReject',
                dataType: 'json',
                data: { LogCode: logcode, mode: mode },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/PreEnrollmentEditApproval/Index";
                            window.location.href = url;
                        });

                    }
                    else {
                        swal.fire(result.message);
                          
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

