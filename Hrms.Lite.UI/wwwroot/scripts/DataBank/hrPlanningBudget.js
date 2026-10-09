var PlanningType = 1;
function onChange() {
    debugger   
        //var url = '/DataBank/HRPlanningBudgetUpload/GetUploadList';
        //$('#uploadList').load(url, { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() }, function () {
        //});
    $.ajax({

        type: 'GET',
        url: '/DataBank/HRPlanningBudgetUpload/GetUploadList',
        dataType: 'html',
        data: { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#uploadList').html("");
                $('#uploadList').html(result);
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

function CheckExtensionAndSize(e) {
    debugger;


    var size = $(e)[0].files[0].size; //Size is in Bytes
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['xlsx'];
    if (size > 1024 * 1024)  //1,048,576 Bytes=1 MB
    {
        swal.fire('File size should be less than 1 MB');
        $(e).val('');
        return false;
    }
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only Excel file is allowded");
        $(e).val('');
        return false;

    }
    return true;

}

function validateHRPlanningDropdowns() {
    var i = 0;
    debugger;
    if (!validateById('#HRPlanning_Code', 'HRPlanning')) {
        i++;
    }
    if (!validateById('#Designation_Code', 'Designation')) {
        i++;
    }
    if (i == 0) {
        var m = $('.bd-example-modal-lg');
        m.modal('show');
    }

}
function onImportExcelClick() {
        $.ajaxSetup({
            async: false
        });
        swal.fire({
            text: "Do you want to Upload?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var MPPersonalInfo = new FormData($('#budgetUpload')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/HRPlanningBudgetUpload/ImportFromExcel',
                    contentType: false,
                    processData: false,
                    data: MPPersonalInfo,
                    success: function (result) {
                        debugger;
                        if (result.success == true) {
                            $("#Excel_FileData").val('');
                            swal.fire("Uploaded Successfully").then(function () {
                                var m = $('.bd-example-modal-lg');
                                m.modal('hide');
                                $.ajax({

                                    type: 'GET',
                                    url: '/DataBank/HRPlanningBudgetUpload/GetUploadList',
                                    dataType: 'html',
                                    data: { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }

                                        else {
                                            $('#uploadList').html("");
                                            $('#uploadList').html(result);
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
                            if (result.message == "Bugdeted numbers already given against the Selected Designation and Planning Period Do you want to overwrite...")
                            {
                                debugger
                                $('#IsOverwrite').val("YES");
                                swal.fire({
                                    text: result.message,
                                    icon: 'warning',
                                    showCancelButton: true,
                                    confirmButtonColor: '#3085d6',
                                    cancelButtonColor: '#d33',
                                    confirmButtonText: 'Ok'
                                }).then((result) => {
                                    if (result.isConfirmed) {
                                        var MPPersonalInfo = new FormData($('#budgetUpload')[0]);
                                        $.ajax({

                                            type: 'POST',
                                            url: '/DataBank/HRPlanningBudgetUpload/ImportFromExcel',
                                            contentType: false,
                                            processData: false,
                                            data: MPPersonalInfo,
                                            success: function (result) {
                                                if (result.success == true) {
                                                    $("#Excel_FileData").val('');
                                                    swal.fire("Uploaded Successfully").then(function () {
                                                        var m = $('.bd-example-modal-lg');
                                                        m.modal('hide');
                                                        $.ajax({

                                                            type: 'GET',
                                                            url: '/DataBank/HRPlanningBudgetUpload/GetUploadList',
                                                            dataType: 'html',
                                                            data: { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val() },
                                                            success: function (result) {
                                                                debugger
                                                                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                                                    window.location.reload();
                                                                }

                                                                else {
                                                                    $('#uploadList').html("");
                                                                    $('#uploadList').html(result);
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
                                                    $("#Excel_FileData").val('');
                                                    if (result.message != null) {
                                                        $('#Unmatchedlist').html('')
                                                        swal.fire(result.message);
                                                    }
                                                    else {
                                                        $('#Unmatchedlist').html(result)
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
                                    else {
                                        return false;
                                    }
                                })
                            }
                            else
                            {
                                $("#Excel_FileData").val('');
                                if (result.message != null) {
                                    $('#Unmatchedlist').html('')
                                    swal.fire(result.message);
                                }
                                else {
                                    $('#Unmatchedlist').html(result)
                                }
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
            else {
                return false;
            }
        })
}
function onApproveClick(mode) {
    debugger
    if (mode == "REJECT") {
       
        text1 = "Do you want to Reject?";
    }
    else {
        text1 = "Do you want to Approve?";
     
    }

    swal.fire({
        text: text1,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/HRPlanningBudgetApproval/ApproveOrReject',
                dataType: 'json',
                data: $("#budgetApprove").serialize() + "&Mode=" + mode,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/HRPlanningBudgetApproval/Index";
                            window.location.href = url;
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

function GetVarianceList() {
    var HrPlanning = $("#HRPlanning_Code").val();
    var Designation = $("#Designation_Code").val();    
    if (HrPlanning != '' && Designation != '') {
        //var url = "/DataBank/HRPlanningBudgetVariance/GetList";
        //$('#varianceList').load(url, { HrPlanningCode: HrPlanning, DesignationCode: Designation, TypeCode: PlanningType }, function () {
        //    if (PlanningType == 0) {
        //        $('.chkTd').addClass('hide');
        //        $('.chkTh').addClass('hide');
        //        $('.mrfVarianceBtn').addClass('hide');
        //    }
        //    else {
        //        $('.chkTd').removeClass('hide');
        //        $('.chkTh').removeClass('hide');
        //        $('.mrfVarianceBtn').removeClass('hide');
        //    }
        //});
        $.ajax({

            type: 'GET',
            url: '/DataBank/HRPlanningBudgetVariance/GetList',
            dataType: 'html',
            data: { HrPlanningCode: $("#HRPlanning_Code").val(), DesignationCode: $("#Designation_Code").val(), TypeCode: PlanningType },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#varianceList').html("");
                    $('#varianceList').html(result);
                    if (PlanningType == 0) {
                        $('.chkTd').addClass('hide');
                        $('.chkTh').addClass('hide');
                        $('.mrfVarianceBtn').addClass('hide');
                    }
                    else {
                        $('.chkTd').removeClass('hide');
                        $('.chkTh').removeClass('hide');
                        $('.mrfVarianceBtn').removeClass('hide');
                    }
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
}

function onTypeChange(sender) {
    PlanningType = $(sender).val();
    GetVarianceList();
    if (PlanningType == 0) {
        $('.chkTd').addClass('hide');
        $('.chkTh').addClass('hide');
        $('.mrfVarianceBtn').addClass('hide');
    }
    else {
        $('.chkTd').removeClass('hide');
        $('.chkTh').removeClass('hide');
        $('.mrfVarianceBtn').removeClass('hide');
    }
}

function CreateMRFAgainstVariance() {
    debugger
    var i = 0;
    if (!validateById('#HRPlanning_Code', 'HRPlanCode')) {
        i++;
    }
    if (!validateById('#Designation_Code', 'DsgCode')) {
        i++;
    }

    if (i == 0) {
        var table = $('#VarianceList').DataTable();
        var data = table.$('input,select,textarea').serializeArray();

        //var table = $('#example').DataTable();


        var isChecked = false;
        debugger
        table.rows().nodes().each(function () {
            debugger
            var checkbox = $(this).find('input[type="checkbox"]');
            debugger
            if (checkbox.is(':checked')) {
                isChecked = true;
                return false; // Exit the loop if at least one checkbox is checked
            }
        });

        if (!isChecked) {
            swal.fire('Please select at least one row.');
            return;
        }
        else {


            swal.fire({
                text: "On Saving the pending count will be directly created as vacancies without any approval. Do you want to Save? ",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Ok'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        type: 'POST',
                        url: '/DataBank/HRPlanningBudgetVariance/Create',
                        dataType: 'json',
                        data: data,
                        success: function (result) {
                            debugger;
                            if (result.success == true) {
                                swal.fire("Saved Successfully").then(function () {

                                    window.location.href = "/DataBank/HRPlanningBudgetVariance/Index";


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

}

function CheckPendingMRF(sender, pendingMRF) {
    if (pendingMRF == 0) {
        swal.fire("The Pending MRF is zero for the selection").then(function () {
            $(sender).prop("checked", false);
        })
    }
}





   

