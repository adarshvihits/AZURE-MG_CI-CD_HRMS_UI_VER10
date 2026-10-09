function GetList()
{
    debugger;
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();
    //var url = "/DataBank/BranchGrading/GetList";
    //$('#BranchGradinglist').load(url, { DesignationCode: des, RegionCode: reg, GradeCode: Grade }, function () { }); 
    $.ajax({

        type: 'GET',
        url: '/DataBank/BranchGrading/GetList',
        dataType: 'html',
        data: { DesignationCode: des, RegionCode: reg, GradeCode: Grade },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#BranchGradinglist').html("");
                $('#BranchGradinglist').html(result);
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


function GetPopUp(code)
{
    debugger;
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/BranchGrading/GetPopUp?DesignationCode=" + des + "&RegionCode=" + reg + "&GradeCode=" + Grade + "&LocationCode=" + code,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.bd-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
            }
         


        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}


function GetSlider()
{
    debugger;
    //var url = "/DataBank/BranchGrading/GetGradeList";
   
    //$('#gradelistSlider').load(url, {}, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/BranchGrading/GetGradeList',
        dataType: 'html',
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#gradelistSlider').html("");
                $('#gradelistSlider').html(result);
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
function GetListOnChange(code) {
    debugger;
    var gradeCode = $(code).val();
    //var url = "/DataBank/BranchGrading/GetPopUpListOnchange";
    //$('#SliderList').load(url, { GradeCode: gradeCode }, function () { });
    $.ajax({

        type: 'GET',
        url: '/DataBank/BranchGrading/GetPopUpListOnchange',
        dataType: 'html',
        data: { GradeCode: gradeCode },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#SliderList').html("");
                $('#SliderList').html(result);
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


function OnSaveClick() {
    debugger;
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();
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
                    url: '/DataBank/BranchGrading/SaveBranchgradingList',
                    dataType: 'json',
                    data: $('#save_BrachGrading').serialize(),
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                CloseSlider('BranchGradingSlider');
                                var url = "/DataBank/BranchGrading/GetList";
                                $('#BranchGradinglist').load(url, { DesignationCode: des, RegionCode: reg, GradeCode: Grade }, function () { });

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

function PopUpDetailsSave() {
    debugger;
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();

    if ($('#Change_BranchGrade_NewGrade_Code').val() != $('#Change_BranchGrade_OldGrade_Code').val()) {
    

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
                url: '/DataBank/BranchGrading/SavePopUpDetails',
                dataType: 'json',
                data: $('#Save_branchpopUpdetails').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            CloseSlider('BranchGradingSlider');
                            var url = "/DataBank/BranchGrading/GetList";
                            $('#BranchGradinglist').load(url, { DesignationCode: des, RegionCode: reg, GradeCode: Grade }, function () { });
                            $('.modal-backdrop').remove()
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
else
{
        swal.fire("Selected Grade is same as Existing Grade. So Please select another Grade");
}


}


function GetGPPlusPopUp(code,type) {
    debugger;
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/BranchGrading/GetGPPopUp?DesignationCode=" + des + "&RegionCode=" + reg + "&GradeCode=" + Grade + "&LocationCode=" + code + "&Type=" + type,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.gp-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}



function GetGPNegPopUp(code, type) {
    debugger;
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();

    $.ajax({
        type: 'GET',
        url: "/DataBank/BranchGrading/GetGPPopUp?DesignationCode=" + des + "&RegionCode=" + reg + "&GradeCode=" + Grade + "&LocationCode=" + code + "&Type=" + type,
        dataType: 'html',

        success: function (Data) {
            if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                var m = $('.gpneg-example-modal-lg');
                m.find('.modal-body').html('');
                m.find('.modal-body').html(Data);
            }

        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });
}

function ExportToExcel() {
    debugger;

    var desname = $('#Designation_Code option:selected').text()
    var Regname = $('#Region_Code option:selected').text()
    var gradename = $('#Grade_Code option:selected').text()
    var des = $('#Designation_Code').val();
    var reg = $('#Region_Code').val();
    var Grade = $('#Grade_Code').val();

    var Branchgradinglist = $('#MainList_Count').val();

    if (Branchgradinglist > 0) {
        url = "/DataBank/BranchGrading/ExportToExcel?DesignationCode=" + des + "&RegionCode=" + reg + "&GradeCode=" + Grade + "&DesName=" + desname + "&RegName=" + Regname + "&GradeName=" + gradename;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}


