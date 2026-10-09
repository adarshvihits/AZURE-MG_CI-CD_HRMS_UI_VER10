const { applyStyle } = require("../../js/fullcalendar.bundle");
var prevTab = "tab1";
var currentTab = 0;
var currentTabName = "Pending";

function tabClick(Tab, TabIndex) {
    debugger;


    $("#TabIndex").val(TabIndex);
    currentTab = TabIndex;

    currentTabName = Tab;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    prevTab = Tab;
    $('#TDSEmployeeListtable').html("");

}
function fillData(assessmentyear) {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var AssessmentYear = assessmentyear;
    var TabIndex = $("#TabIndex").val();
    var dataTable = $("#TDSEmployeeListtable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/InvestmentDeclarationApproval/GetInvestDeclarationApprovalEmpList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&TabIndex=' + TabIndex + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger;
                $('#TDSEmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeHeader.employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="CreateTdsDeclaration(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ')">' + d.data[i].employeeHeader.employeeName + '' + '' + '</a>'
                    if (d.data[i].rentalStatus == 'AP') {
                        d.data[i].rentalStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].rentalStatus + '</span>'
                    }
                    else if (d.data[i].rentalStatus == 'R') {
                        d.data[i].rentalStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].rentalStatus + '</span>'
                    }
                    else if (d.data[i].rentalStatus == 'A') {
                        d.data[i].rentalStatus = '<span style="color:#37B678;font-weight: bold;" >' + d.data[i].rentalStatus + '</span>'
                    }
                    if (d.data[i].dedStatus == 'AP') {
                        d.data[i].dedStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].dedStatus + '</span>'
                    }
                    else if (d.data[i].dedStatus == 'R') {
                        d.data[i].dedStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].dedStatus + '</span>'
                    }
                    else if (d.data[i].dedStatus == 'A') {
                        d.data[i].dedStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].dedStatus + '</span>'
                    }
                    if (d.data[i].licStatus == 'AP') {
                        d.data[i].licStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].licStatus + '</span>'
                    }
                    else if (d.data[i].licStatus == 'R') {
                        d.data[i].licStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].licStatus + '</span>'
                    }
                    else if (d.data[i].licStatus == 'A') {
                        d.data[i].licStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].licStatus + '</span>'
                    }
                    if (d.data[i].chapterStatus == 'AP') {
                        d.data[i].chapterStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].chapterStatus + '</span>'
                    }
                    else if (d.data[i].chapterStatus == 'R') {
                        d.data[i].chapterStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].chapterStatus + '</span>'
                    }
                    else if (d.data[i].chapterStatus == 'A') {
                        d.data[i].chapterStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].chapterStatus + '</span>'
                    }
                    //if (d.data[i].donStatus == 'AP') {
                    //    d.data[i].donStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].donStatus + '</span>'
                    //}
                    //else if (d.data[i].donStatus == 'R') {
                    //    d.data[i].donStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].donStatus + '</span>'
                    //}
                    //else if (d.data[i].donStatus == 'A') {
                    //    d.data[i].donStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].donStatus + '</span>'
                    //}
                    if (d.data[i].repayStatus == 'AP') {
                        d.data[i].repayStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].repayStatus + '</span>'
                    }
                    else if (d.data[i].repayStatus == 'R') {
                        d.data[i].repayStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].repayStatus + '</span>'
                    }
                    else if (d.data[i].repayStatus == 'A') {
                        d.data[i].repayStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].repayStatus + '</span>'
                    }
                    if (d.data[i].letoutStatus == 'AP') {
                        d.data[i].letoutStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].letoutStatus + '</span>'
                    }
                    else if (d.data[i].letoutStatus == 'R') {
                        d.data[i].letoutStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].letoutStatus + '</span>'
                    }
                    else if (d.data[i].letoutStatus == 'A') {
                        d.data[i].letoutStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].letoutStatus + '</span>'
                    }
                    if (d.data[i].otherStatus == 'AP') {
                        d.data[i].otherStatus = '<span style="color:blue;font-weight: bold;">' + d.data[i].otherStatus + '</span>'
                    }
                    else if (d.data[i].otherStatus == 'R') {
                        d.data[i].otherStatus = '<span style="color:red;font-weight: bold;">' + d.data[i].otherStatus + '</span>'
                    }
                    else if (d.data[i].otherStatus == 'A') {
                        d.data[i].otherStatus = '<span style="color:#37B678;font-weight: bold;">' + d.data[i].otherStatus + '</span>'
                    }
                }

                return d.data;
                debugger;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId", "width": "10%" },
            { "data": "employeeHeader.employeeName", "width": "15%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "rentalStatus", "width": "5%" },
            { "data": "dedStatus", "width": "5%" },
            { "data": "licStatus", "width": "5%" },
            { "data": "chapterStatus", "width": "5%" },
   /*         { "data": "donStatus", "width": "5%" },*/
            { "data": "repayStatus", "width": "5%" },
            { "data": "letoutStatus", "width": "5%" },
            { "data": "otherStatus", "width": "5%" },

        ],






        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

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

}
function HouseRentTotal() {

    var TotalActual = 0;
    var TotalApproval = 0;

    for (let i = 0; i < 12; i++) {
        var Amount = $("#HouseRentalData_" + i + "__Amount").val();
        TotalActual += parseFloat(Amount);
    }
    for (let j = 0; j < 12; j++) {
        var Amount = $("#HouseRentalData_" + j + "__ApprvdAmount").val();
        TotalApproval += parseFloat(Amount);
    }

    $("#actualamt").html(TotalActual);
    $("#approvalamt").html(TotalApproval);

}
function DeductionTotal() {
    var Length = $('#EightyCDeductionList_Count').val();

    var TotalActual = 0;
    var TotalApproval = 0;
    for (let i = 0; i < Length; i++) {
        var Amount = $("#EightyCDeductionList_" + i + "__Amount").val();
        TotalActual += parseFloat(Amount);
    }
    for (let j = 0; j < Length; j++) {
        var Amount = $("#EightyCDeductionList_" + j + "__ApprovedAmount").val();
        TotalApproval += parseFloat(Amount);
    }

    $("#DedActAmt").html(TotalActual);
    $("#DedAppAmt").html(TotalApproval);

}
function onDeductionAmtChange() {
    DeductionTotal();
}
function onDonationAmtChange() {
    DonationTotal();
}
//function DonationTotal() {
  
//    var Length = $('#DonationList_Count').val();
//    var TotalActual = 0;
//    var TotalApproval = 0;

//    for (let i = 0; i < Length; i++) {
//        var Amount = $("#DonationList_" + i + "__Amount").val();
//        TotalActual += parseFloat(Amount);
//    }
//    for (let j = 0; j < Length; j++) {
//        var Amount = $("#DonationList_" + j + "__ApprovedAmount").val();
//        TotalApproval += parseFloat(Amount);
//    }

//    $("#DonActAmt").html(TotalActual);
//    $("#DonAppAmt").html(TotalApproval);

//}
function CreateTdsDeclaration(EmployeeGI) {

    var AssessmentYear = $('#AssessmentYear_Code').val();
    var url = "/TDS/InvestmentDeclarationApproval/Approve?EmployeeGI=" + EmployeeGI + "&AssessmentYear=" + AssessmentYear;
    console.log(url);
    window.location.href = url;

}
function HouseRentalSave(Status) {
    var HouseRentalDataLength = $('#HouseRentalData_Count').val() - 1;
  
    var TotalRent = 0;
    var ApproveReject = "";
    var Remarks = $('#ApproverComments').val();

    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    for (let j = 0; j < HouseRentalDataLength; j++) {

 
        var Amount = $("#HouseRentalData_" + j + "__ApprvdAmount").val();
     
        TotalRent += parseFloat(Amount);
    } 

    var i = 0;
    //if (Status == 'Approved') {
    //     if (TotalRent == "0") {
    //    swal.fire("Approved amount should be greater than zero!");
    //    i++;
    //    return false;
    //}
    //}
   
    $('#tbllandlord tr').each(function () {
        if ($.trim($(this).text()) == 'No table data') {
            swal.fire("Add Landlord Details.");
            i++;
            return false;
        }

    });

    if (i > 0) {
        return false;
    }
    else {


        swal.fire({
            text: "Do you want to " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/HouseRentalandLandlordApproval',
                    dataType: 'json',
                    data: $('#HouseRentalApprove').serialize() + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {

                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabOne').addClass('active');
                                $('#tabOne').addClass('active');
                                $('#tabOne').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclarationApproval/GetRentalDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val() },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabOne').html("");
                                            $('#tabOne').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


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
        });

    }
}
function onCityChange(sender, id) {
    var citycode = $("#HouseRentalData_" + id + "__MetroCity_Code option:selected").val();

    if ($("#HouseRentalData_" + id + "__MetroCity_Code option:selected").text() == "Others") {
        for (let i = id; i < 12; i++) {

            $("#HouseRentalData_" + i + "__MetroCity_Code").val(citycode);

            $("#HouseRentalData_" + i + "__MetroNonMetro").val('NonMetro');
            $("#HouseRentalData_" + i + "__ApprvdAmount").val(0);
            $("#HouseRentalData_" + i + "__MetroNonMetro").attr("readonly", true);
            $("#HouseRentalData_" + i + "__OtherCityName").attr("disabled", false);
        }
    }
    else {
        for (let j = id; j < 12; j++) {
            $("#HouseRentalData_" + j + "__MetroCity_Code").val(citycode);

            $("#HouseRentalData_" + j + "__OtherCityName").val('');
            $("#HouseRentalData_" + j + "__MetroNonMetro").val('Metro');
            $("#HouseRentalData_" + j + "__ApprvdAmount").val(0);
            $("#HouseRentalData_" + j + "__MetroNonMetro").attr("readonly", false);
            $("#HouseRentalData_" + j + "__OtherCityName").attr("disabled", true);
        }

    }
    validateCity(id);

}
function onRentAmountChange(sender, id) {
    var Amount = $("#HouseRentalData_" + id + "__ApprvdAmount").val();
    for (let i = id + 1; i < 12; i++) {
        $("#HouseRentalData_" + i + "__ApprvdAmount").val(Amount);
    }
    HouseRentTotal();
}
function onOtherCityChange(sender, id) {

    var othercity = $("#HouseRentalData_" + id + "__OtherCityName").val();
    for (let i = id + 1; i < 12; i++) {

        if ($("#HouseRentalData_" + i + "__MetroCity_Code option:selected").text() == "Others") {
            $("#HouseRentalData_" + i + "__OtherCityName").val(othercity);
        }
    }
}
//------------REJECTION-----------------------------

function RejectionPopUp(index, Tab) {

    $('#ApproverComments').removeClass('is-invalid');
    $('#rejremarks').removeClass('error-message');
    $('#rejremarks').html("");

    DocHead = '';

    if (Tab == '80CSection') {
        DocHead = 'Investment Under Section 80-C';
        $('#DeductionbtnId_' + index).attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80GSection') {
        DocHead = '80-G';
        $('#DonationbtnId_' + index).attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == 'HLRepay') {
        DocHead = 'Self Occupied House Loan Repayment';
        $('#HLRepaybtnId_' + index).attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == 'LetOut') {
        DocHead = 'Income from Let Out House Property';
        $('#LetOutbtnId_' + index).attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == 'OtherIncome') {
        DocHead = 'Other Income';
        $('#OtherIncmebtnId_' + index).attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == 'lic') {
        DocHead = 'LIC';
        $('#licbtnId_' + index).attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80CCC') {
        DocHead = '80CCC';
        $('#SixACCCbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80CCD') {
        DocHead = '80CCD';
        $('#SixACCDbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80D') {
        DocHead = '80D';
        $('#SixADbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80DD') {
        DocHead = '80DD';
        $('#SixADDbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80DDB') {
        DocHead = '80DDB';
        $('#SixADDBbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80E') {
        DocHead = '80E';
        $('#SixAEbtnId').attr('data-target', '.bd-reject-modal-lg');
    }

    if (Tab == '80EE') {
        DocHead = '80EE';
        $('#SixAEEbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80EEA') {
        DocHead = '80EEA';
        $('#SixAEEAbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80EEB') {
        DocHead = '80EEB';
        $('#SixAEEBbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80TTA') {
        DocHead = '80TTA';
        $('#SixATTAbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == '80U') {
        DocHead = '80U';
        $('#SixAUbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == 'LT') {
        DocHead = 'LT';
        $('#SixALTbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    if (Tab == 'HouseRental') {
        DocHead = 'House Rental';
        $('#HouseRentalbtnId').attr('data-target', '.bd-reject-modal-lg');
    }
    $('#RejectionPopUp').text('Rejection for ' + DocHead);
    $('#Rowid').val(index);
    $('#TabIndex').val(Tab);
}
function onRejectionClick() {
    var rowid = $('#Rowid').val();

    var Tab = $('#TabIndex').val();
    var ApprStatus = 'Rejected';
    var i = 0;
    if (!validateById('#ApproverComments', 'rejremarks')) {
        i++;
    }
    if (i > 0) {
        return false;
    }
    else {
        if (Tab == 'HouseRental') {
            HouseRentalSave(ApprStatus)
        }
        else if (Tab == '80CSection') {
            DeductionApproval(rowid, ApprStatus, Tab)
        }
        else if (Tab == '80GSection') {
            DonationApproval(rowid, ApprStatus, Tab)
        }
        else if (Tab == 'lic') {
            LICApproval(rowid, ApprStatus, Tab)
        }
        else if (Tab == 'HLRepay') {
            HLRepayApproval(rowid, ApprStatus, Tab)
        }
        else if (Tab == 'LetOut') {
            LetOutApproval(rowid, ApprStatus, Tab)
        }
        else if (Tab == 'OtherIncome') {
            OtherIncomeApproval(rowid, ApprStatus, Tab)
        }
        else if (Tab == '80CCC' || Tab == '80CCD' || Tab == '80D' || Tab == '80DD' || Tab == '80DDB' || Tab == '80E' || Tab == '80EE' || Tab == '80EEA' || Tab == '80EEB' || Tab == '80TTA' || Tab == '80U' || Tab == 'LT') {
            ChapterSixAApproval(ApprStatus, Tab)
        }

    }
}
//---------------------80C DEDUCTION------------------
function DeductionApproval(Index, Status, TabIndex) {

    debugger;
    var Remarks = $('#ApproverComments').val();

    $('#EightyCDeductionList_' + Index + '__flag').val(1);
    var ApproveReject = "";
    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    var i = 0;
    if (Status == 'Approved') {

        if ($('#EightyCDeductionList_' + Index + '__ApprovedAmount').val() == "0.0") {
            swal.fire("Approved Amount should be greater than Zero!");

            i++;
            return false;
        }

    }

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to  " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/RowWiseApproveOrReject',
                    dataType: 'json',
                    data: $('#deduction').serialize() + "&TabIndex=" + TabIndex + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                $('.tabOne').removeClass('active');
                                $('#tabOne').removeClass('active');
                                $('#tabOne').css('display', 'none');
                                $('.tabTwo').addClass('active');
                                $('#tabTwo').addClass('active');
                                $('#tabTwo').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                           
                                            $('#tabTwo').html("");
                                            $('#tabTwo').html(result);
                                            DeductionTotal();
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                $('#EightyCDeductionList_' + Index + '__flag').val(0);
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#EightyCDeductionList_' + Index + '__flag').val(0);
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            }
        });

    }
}
//---------------------LIC------------------
function LICApproval(Index, Status, TabIndex) {

    debugger;
    var Remarks = $('#ApproverComments').val();

    $('#LICList_' + Index + '__flag').val(1);
    var ApproveReject = "";
    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    var i = 0;
    if (Status == 'Approved') {

        if ($('#LICList_' + Index + '__PremiumAmount').val() == "0" || $('#LICList_' + Index + '__PremiumAmount').val() == "") {
            swal.fire("Premium Amount should be greater than Zero!");

            i++;
            return false;
        }
        if ($('#LICList_' + Index + '__SumAssured').val() == "0" || $('#LICList_' + Index + '__SumAssured').val() == "") {
            swal.fire("Sum Assured should be greater than Zero!");

            i++;
            return false;
        }

    }

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to  " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/RowWiseApproveOrReject',
                    dataType: 'json',
                    data: $('#lic').serialize() + "&TabIndex=" + TabIndex + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabTwo').addClass('active');
                                $('#tabTwo').addClass('active');
                                $('#tabTwo').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabTwo').html("");
                                            $('#tabTwo').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                $('#LICList_' + Index + '__flag').val(0);
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#LICList_' + Index + '__flag').val(0);
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            }
        });

    }
}
//---------------------80G DONATION------------------
//function DonationApproval(Index, Status, TabIndex) {

//    debugger;
//    var Remarks = $('#ApproverComments').val();

//    $('#DonationList_' + Index + '__flag').val(1);
//    var ApproveReject = "";
//    if (Status == 'Approved') {
//        ApproveReject = 'Approve';
//    }
//    else {
//        ApproveReject = 'Reject';
//    }
//    var i = 0;
//    if (Status == 'Approved') {

//        if ($('#DonationList_' + Index + '__ApprovedAmount').val() == "0.0") {
//            swal.fire("Approved Amount should be greater than Zero!");

//            i++;
//            return false;
//        }

//    }

//    if (i > 0) {
//        return false;
//    }
//    else {
//        debugger;
//        swal.fire({
//            text: "Do you want to  " + ApproveReject + "?",
//            icon: 'warning',
//            showCancelButton: true,
//            confirmButtonColor: '#3085d6',
//            cancelButtonColor: '#d33',
//            confirmButtonText: 'Ok'
//        }).then((result) => {
//            if (result.isConfirmed) {
//                $.ajax({

//                    type: 'POST',
//                    url: '/TDS/InvestmentDeclarationApproval/RowWiseApproveOrReject',
//                    dataType: 'json',
//                    data: $('#donation').serialize() + "&TabIndex=" + TabIndex + "&Status=" + Status + "&Remarks=" + Remarks,
//                    success: function (result) {
//                        if (result.success == true) {
//                            swal.fire(result.message).then(function () {
//                                $('.tabTwo').removeClass('active');
//                                $('#tabTwo').removeClass('active');
//                                $('#tabTwo').css('display', 'none');
//                                $('.tabFour').addClass('active');
//                                $('#tabFour').addClass('active');
//                                $('#tabFour').css('display', 'block');
//                                var m = $('.bd-reject-modal-lg');
//                                m.modal('hide');
//                                $('#ApproverComments').val('');
//                                $.ajax({

//                                    type: 'GET',
//                                    url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
//                                    dataType: 'html',
//                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
//                                    success: function (result) {
//                                        debugger
//                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                                            window.location.reload();
//                                        }
//                                        else {
                                          
//                                            $('#tabFour').html("");
//                                            $('#tabFour').html(result);
//                                            DonationTotal();
//                                        }
//                                    },
//                                    error: function (xhr, textStatus, errorThrown) {
//                                        debugger
//                                        document.body.id = "kt_body";
//                                        $('body').html(xhr.responseText);


//                                    }
//                                });
//                            });

//                        }

//                        else {
//                            swal.fire(result.message).then(function () {
//                                $('#DonationList_' + Index + '__flag').val(0);
//                                var m = $('.bd-reject-modal-lg');
//                                m.modal('hide');
//                            });
//                        }

//                    },
//                    error: function (xhr, textStatus, errorThrown) {
//                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
//                            window.location.reload();
//                        }
//                        else {
//                            $('#DonationList_' + Index + '__flag').val(0);
//                            document.body.id = "kt_body";
//                            $('body').html(xhr.responseText);
//                        }
//                    }
//                });
//            }
//        });

//    }
//}
//---------------------CHAPTER Six A Approval------------------
function ChapterSixAApproval(Status, SectionIndex) {

    debugger;
    var Remarks = $('#ApproverComments').val();

    var ApproveReject = "";
    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    var i = 0;
    //if (Status == 'Approved') {

    //    //if ($('#ChapterVIADetails_PPSAmt').val() == "0.0") {
    //    //    swal.fire("Approved PPS Amount should be greater than Zero!");

    //    //    i++;
    //    //    return false;
    //    //}

    //}

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to  " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/ChapterVIAApproveOrReject',
                    dataType: 'json',
                    data: $('#ChapterVIA').serialize() + "&SectionIndex=" + SectionIndex + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabThree').addClass('active');
                                $('#tabThree').addClass('active');
                                $('#tabThree').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                RefreshTabs('ChapterVIA');
                            });

                        }
                        else {
                            swal.fire(result.message).then(function () {
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
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
        });

    }
}
function PPSChange() {
    var PPSAmt = parseFloat($('#ChapterVIADetails_PPSAmt').val());
    if (isNaN(PPSAmt) || PPSAmt == '' || PPSAmt == null) {
        PPSAmt = parseFloat("0.00");
    }
    $('#ChapterVIADetails_PPSEAmt').val(PPSAmt);
}
function NPSChange() {
    var NPSAmt = parseFloat($('#ChapterVIADetails_NPSAmt').val());
    if (isNaN(NPSAmt) || NPSAmt == '' || NPSAmt == null) {
        NPSAmt = parseFloat("0.00");
    }
    $('#ChapterVIADetails_NPSEAmt').val(NPSAmt);
}
function MedExpChange() {
    var MExpAmt = parseFloat($('#ChapterVIADetails_MedExpSrCtznAmt').val());
    if (isNaN(MExpAmt) || MExpAmt == '' || MExpAmt == null) {
        MExpAmt = parseFloat("0.00");
    }
    $('#ChapterVIADetails_MedExpSrCtznEAmt').val(MExpAmt);
}
function IsHandicaped() {
    if ($('#ChapterVIADetails_IsDepndHandicaped_Code option:selected').text() == "Yes") {
        $('#ChapterVIADetails_DisabilitySevere_Code').attr("disabled", false);
        $('#ChapterVIADetails_DependentRelation_Code').attr("disabled", false);

    }
    else {
        $('#ChapterVIADetails_DisabilitySevere_Code').val('');
        $('#ChapterVIADetails_DependentRelation_Code').val('');
        $('#ChapterVIADetails_DisabilitySevere_Code').attr("disabled", true);
        $('#ChapterVIADetails_DependentRelation_Code').attr("disabled", true);

    }
}
function CriticalAilments() {

    if ($('#ChapterVIADetails_CriticalAilments_Code').val() >= 1) {
        $('#ChapterVIADetails_RelationShip_Code').attr("disabled", false);
        $('#ChapterVIADetails_PatientAge').attr("disabled", false);
        $('#ChapterVIADetails_DepndExpenditure').attr("disabled", false);

    }
    else {
        $('#ChapterVIADetails_RelationShip_Code').val('');
        $('#ChapterVIADetails_PatientAge').val('0');
        $('#ChapterVIADetails_DepndExpenditure').val('0.0');
        $('#ChapterVIADetails_RelationShip_Code').attr("disabled", true);
        $('#ChapterVIADetails_PatientAge').attr("disabled", true);
        $('#ChapterVIADetails_DepndExpenditure').attr("disabled", true);

    }

}
function EducationLoan() {
    var interest = parseFloat($('#ChapterVIADetails_EduLoanInterest').val());
    if (isNaN(interest) || interest == '' || interest == null) {
        interest = parseFloat("0.00");
    }
    $('#ChapterVIADetails_EduLoanInterestEAmt').val(interest);
}
function HouseLoan() {
    var InterestPaid = parseFloat($('#ChapterVIADetails_InterestPaid').val());
    if (isNaN(InterestPaid) || InterestPaid == '' || InterestPaid == null) {
        InterestPaid = parseFloat("0.00");
    }
    $('#ChapterVIADetails_InterestEAmt').val(InterestPaid);
}
function EEAHouseLoan() {
    var EEAInterestPaid = parseFloat($('#ChapterVIADetails_EEAInterestPaid').val());
    if (isNaN(EEAInterestPaid) || EEAInterestPaid == '' || EEAInterestPaid == null) {
        EEAInterestPaid = parseFloat("0.00");
    }
    $('#ChapterVIADetails_EEAInterestEAmt').val(EEAInterestPaid);
}
function ElectricCarLoan() {
    var ECInterest = parseFloat($('#ChapterVIADetails_ECInterest').val());
    if (isNaN(ECInterest) || ECInterest == '' || ECInterest == null) {
        ECInterest = parseFloat("0.00");
    }
    $('#ChapterVIADetails_ECInterestEA').val(ECInterest);
}
function SBAccountInterest() {
    var SBAccInterest = parseFloat($('#ChapterVIADetails_SBAccInterest').val());
    if (isNaN(SBAccInterest) || SBAccInterest == '' || SBAccInterest == null) {
        SBAccInterest = parseFloat("0.00");
    }
    $('#ChapterVIADetails_SBAccInterestEA').val(SBAccInterest);
}
function DisabilityNature() {

    if ($('#ChapterVIADetails_NatureofDisability_Code').val() >= 1) {
        $('#ChapterVIADetails_PercentageofDisability_Code').attr("disabled", false);
    }
    else {
        $('#ChapterVIADetails_PercentageofDisability_Code').val('');
        $('#ChapterVIADetails_PercentageofDisability_Code').attr("disabled", true);
    }

}
function RefreshTabs(TabIndex) {
    $.ajax({

        type: 'GET',
        url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
        dataType: 'html',
        data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#tabThree').html("");
                $('#tabThree').html(result);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
}
//---------------------HOUSE LOAN REPAY------------------
function HLRepayApproval(Index, Status, TabIndex) {

    debugger;
    var Remarks = $('#ApproverComments').val();

    $('#HLRepayList_' + Index + '__flag').val(1);
    var ApproveReject = "";
    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    var i = 0;
    if (Status == 'Approved') {

        if (!validateById('#HLRepayList_' + Index + '__HouseAddress', 'address')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__LocationStatus_Code', 'locationstatus')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__OwnerStatus_Code', 'ownerstatus')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__Percentage', 'percentage')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__Purpose_Code', 'purpose')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__DateofLoan', 'dateofloan')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__DateofCompletion', 'dateofcmptn')) {
            i++;
        }

        if (!validateById('#HLRepayList_' + Index + '__PrincipalAmt', 'principleamt')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__Interest', 'interest')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__NameofBank', 'nameofbank')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__PANofBank', 'panofbank')) {
            i++;
        }
        if (!validateById('#HLRepayList_' + Index + '__BankAddress', 'bankaddress')) {
            i++;
        }
        if ($('#HLRepayList_' + Index + '__Percentage').val() == "0") {
            $('#HLRepayList_' + Index + '__Percentage').addClass('is-invalid');
            $("#percentage").addClass('error-message');
            $("#percentage").html("Percentage should n't be less than Zero.");
            i++;
        }
        if (!validatePan('HLRepayList_' + Index + '__PANofBank', 'panofbank')) {
            i++;
        }
    }

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to  " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/RowWiseApproveOrReject',
                    dataType: 'json',
                    data: $('#HouseLoanRepay').serialize() + "&TabIndex=" + TabIndex + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabFive').addClass('active');
                                $('#tabFive').addClass('active');
                                $('#tabFive').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabFive').html("");
                                            $('#tabFive').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                $('#HLRepayList_' + Index + '__flag').val(0);
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#HLRepayList_' + Index + '__flag').val(0);
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            }
        });

    }
}
function HLOwnerStausChange(Index, sender, id) {

    debugger;

    if ($('#HLRepayList_' + Index + '__OwnerStatus_Code option:selected').text() == "Self") {
        $('#HLRepayList_' + Index + '__Percentage').val(100);
        $('#HLRepayList_' + Index + '__Percentage').attr("disabled", true);
    }
    else {
        $('#HLRepayList_' + Index + '__Percentage').val(0);
        $('#HLRepayList_' + Index + '__Percentage').attr("disabled", false);
    }
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    if ($('#HLRepayList_' + Index + '__Percentage').val() == "") {
        $('#HLRepayList_' + Index + '__Percentage').addClass('is-invalid');
        $('#percentage').addClass('error-message');
        $('#percentage').html("This field is required");
    }
    else {
        $('#HLRepayList_' + Index + '__Percentage').removeClass('is-invalid');
        $('#percentage').removeClass('error-message');
        $('#percentage').html("");
    }

}
function HLInterestChange(Index, sender, id) {
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    var interest = parseFloat($('#HLRepayList_' + Index + '__Interest').val());
    var emiinterest = parseFloat($('#HLRepayList_' + Index + '__PreEMIInterest').val());

    if (isNaN(interest) || interest == '' || interest == null) {
        interest = parseFloat("0.00");
    }

    if (isNaN(emiinterest) || emiinterest == '' || emiinterest == null) {
        emiinterest = parseFloat("0.00");
    }

    var totalInterest = (interest + emiinterest);

    $('#HLRepayList_' + Index + '__TotalInterest').val(totalInterest);

}
//---------------------LET OUT PROPERTY------------------
function LetOutOwnerStausChange(Index, sender, id) {

    debugger;


    var cid = $(sender).val();

    if ($('#LetOutList_' + Index + '__OwnerStatus_Code option:selected').text() == "Self") {
        $('#LetOutList_' + Index + '__Percentage').val(100);
        $('#LetOutList_' + Index + '__Percentage').attr("disabled", true);
    }
    else {
        $('#LetOutList_' + Index + '__Percentage').val(0);
        $('#LetOutList_' + Index + '__Percentage').attr("disabled", false);
    }

    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }

    if ($('#LetOutList_' + Index + '__Percentage').val() == "") {
        $('#LetOutList_' + Index + '__Percentage').addClass('is-invalid');
        $('#percentage').addClass('error-message');
        $('#percentage').html("This field is required");
    }
    else {
        $('#LetOutList_' + Index + '__Percentage').removeClass('is-invalid');
        $('#percentage').removeClass('error-message');
        $('#percentage').html("");
    }




}
function LOInterestChange(Index, sender, id) {

    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    var interest = parseFloat($('#LetOutList_' + Index + '__Interest').val());
    var emiinterest = parseFloat($('#LetOutList_' + Index + '__PreEMIInterest').val());

    if (isNaN(interest) || interest == '' || interest == null) {
        interest = parseFloat("0.00");
    }

    if (isNaN(emiinterest) || emiinterest == '' || emiinterest == null) {
        emiinterest = parseFloat("0.00");
    }

    var totalInterest = (interest + emiinterest);
    $('#LetOutList_' + Index + '__TotalInterest').val(totalInterest);

}
function LetOutApproval(Index, Status, TabIndex) {

    debugger;
    var Remarks = $('#ApproverComments').val();

    $('#LetOutList_' + Index + '__flag').val(1);
    var ApproveReject = "";
    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    var i = 0;
    if (Status == 'Approved') {

        if (!validateById('#LetOutList_' + Index + '__HouseAddress', 'address')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__OwnerStatus_Code', 'ownerstatus')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__Percentage', 'percentage')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__Purpose_Code', 'purpose')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__DateofLoan', 'dateofloan')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__DateofCompletion', 'dateofcmptn')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__DateofLetOut', 'dateofLetOut')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__RentalIncome', 'rentalincme')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__PrincipalAmt', 'principleamt')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__Interest', 'interest')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__NameofBank', 'nameofbank')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__PANofBank', 'panofbank')) {
            i++;
        }
        if (!validateById('#LetOutList_' + Index + '__BankAddress', 'bankaddress')) {
            i++;
        }
        if ($('#LetOutList_' + Index + '__Percentage').val() == "0") {
            $('#LetOutList_' + Index + '__Percentage').addClass('is-invalid');
            $("#percentage_" + Index).addClass('error-message');
            $("#percentage_" + Index).html("Percentage should n't be less than Zero.");
            i++;
        }



        if (!validatePan('LetOutList_' + Index + '__PANofBank', 'panofbank')) {
            i++;
        }

    }

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to  " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/RowWiseApproveOrReject',
                    dataType: 'json',
                    data: $('#LetOutIncome').serialize() + "&TabIndex=" + TabIndex + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabSix').addClass('active');
                                $('#tabSix').addClass('active');
                                $('#tabSix').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabSix').html("");
                                            $('#tabSix').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                $('#LetOutList_' + Index + '__flag').val(0);
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#LetOutList_' + Index + '__flag').val(0);
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            }
        });

    }
}
//---------------------OTHER INCOME------------------
function OtherIncomeApproval(Index, Status, TabIndex) {

    debugger;
    var Remarks = $('#ApproverComments').val();

    $('#OtherIncomeList_' + Index + '__flag').val(1);
    var ApproveReject = "";
    if (Status == 'Approved') {
        ApproveReject = 'Approve';
    }
    else {
        ApproveReject = 'Reject';
    }
    var i = 0;
    if (Status == 'Approved') {

        if ($('#OtherIncomeList_' + Index + '__Amount').val() == "0" || $('#OtherIncomeList_' + Index + '__Amount').val() == "") {
            swal.fire("Amount should be greater than Zero!");

            i++;
            return false;
        }

    }

    if (i > 0) {
        return false;
    }
    else {
        debugger;
        swal.fire({
            text: "Do you want to  " + ApproveReject + "?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({

                    type: 'POST',
                    url: '/TDS/InvestmentDeclarationApproval/RowWiseApproveOrReject',
                    dataType: 'json',
                    data: $('#OtherIncme').serialize() + "&TabIndex=" + TabIndex + "&Status=" + Status + "&Remarks=" + Remarks,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {

                                $('.tabSeven').addClass('active');
                                $('#tabSeven').addClass('active');
                                $('#tabSeven').css('display', 'block');
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                                $('#ApproverComments').val('');
                                $.ajax({

                                    type: 'GET',
                                    url: '/TDS/InvestmentDeclarationApproval/RefeshTabDetails',
                                    dataType: 'html',
                                    data: { EmployeeGI: $('#EmployeeGuid').val(), AssessmentYear: $('#AssessmentYear_Code').val(), TabIndex: TabIndex },
                                    success: function (result) {
                                        debugger
                                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                                            window.location.reload();
                                        }
                                        else {
                                            $('#tabSeven').html("");
                                            $('#tabSeven').html(result);
                                        }
                                    },
                                    error: function (xhr, textStatus, errorThrown) {
                                        debugger
                                        document.body.id = "kt_body";
                                        $('body').html(xhr.responseText);


                                    }
                                });
                            });

                        }

                        else {
                            swal.fire(result.message).then(function () {
                                $('#OtherIncomeList_' + Index + '__flag').val(0);
                                var m = $('.bd-reject-modal-lg');
                                m.modal('hide');
                            });
                        }

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        if ($($.parseHTML(xhr.responseText)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }
                        else {
                            $('#OtherIncomeList_' + Index + '__flag').val(0);
                            document.body.id = "kt_body";
                            $('body').html(xhr.responseText);
                        }
                    }
                });
            }
        });

    }
}


//------------------DOCUMENT VIEW-------------------
function GetUploadDocumentPopUp(idType, TrnCode) {

    DocHead = '';
    if (idType == 'RENTAL') {
        DocHead = ' House Rental';
    }
    else if (idType == '80C_DED') {
        DocHead = 'Investment Under Section 80-C';
    }
    else if (idType == 'LIC') {
        DocHead = 'Investment Under Section 80-C-LIC';
    }
    else if (idType == '80G_DON') {
        DocHead = '80-G';
    }
    else if (idType == 'LoanRepay') {
        DocHead = 'Self Occupied House Loan Repayment';
    }


    else if (idType == 'LetOut') {
        DocHead = 'Income from Let Out House Property';
    }
    else if (idType == 'OtherIncome') {
        DocHead = 'Other Income';
    }
    else if (idType == 'CH6A') {
        DocHead = 'Chapter VI A -' + TrnCode;
    }


    var assYear = $('#AssessmentYear_Code').val();
    var empGI = $('#EmployeeGuid').val();

    $.ajax({
        type: 'GET',
        url: "/TDS/InvestmentDeclarationApproval/GetPopUpforDocUpload",
        dataType: 'html',
        data: { idType: idType, idRef: TrnCode, assYear: assYear, EmployeeGI: empGI },
        success: function (Data) {

            var m = $('.bd-example-modal-lg');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
            $('#popUpName').text('Uploaded Documents for ' + DocHead);


        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });
}

function ViewDocument(TrnCode) {


    var url = "/TDS/InvestmentDeclarationApproval/ViewDocument?TrnCode=" + TrnCode;

    $.ajax({
        url: url,
        type: "GET",
        success: function () {
            // File exists → open in new tab
            window.open(url, "_blank");
        },
        error: function (xhr) {
            let response = JSON.parse(xhr.responseText);
            Swal.fire({
                icon: "error",
                title: "Error",
                text: response.message  // localized message
            });
        }
    });

}

