function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var AssessmentYear = $('#AssessmentYear_Code').val();
 
    var dataTable = $("#TDSEmployeeListOldtable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TDS/TDSDeclarationDetailsOld/GetTdsEmpList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';


                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&AssessmentYear=' + AssessmentYear + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#TDSEmployeeList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].employeeHeader.employeeName = '<a class="" data-bs-toggle="modal" data-target=".bd-example-modal-lg" onclick="CreateTdsDeclaration(' + "'" + d.data[i].employeeHeader.employeeGuid + "'" + ')">' + d.data[i].employeeHeader.employeeName + '' + '' + '</a>'
                    //if (d.data[i].rentalCount != true) {
                    //    d.data[i].rentalCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].rentalCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
                    //if (d.data[i].dedCount != true) {
                    //    d.data[i].dedCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].dedCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
                    //if (d.data[i].chapterCount != true) {
                    //    d.data[i].chapterCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].chapterCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
                    
                    //if (d.data[i].repayCount != true) {
                    //    d.data[i].repayCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].repayCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
                    //if (d.data[i].letoutCount != true) {
                    //    d.data[i].letoutCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].letoutCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
                    //if (d.data[i].otherCount != true) {
                    //    d.data[i].otherCount = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                    //}
                    //else {
                    //    d.data[i].otherCount = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                    //}
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
            //{ "data": "rentalCount", "width": "5%" },
            //{ "data": "dedCount", "width": "5%" },
            //{ "data": "chapterCount", "width": "5%" },
            //{ "data": "repayCount", "width": "5%" },
            //{ "data": "letoutCount", "width": "5%" },
            //{ "data": "otherCount", "width": "5%" },

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

function CreateTdsDeclaration(EmployeeGI) {
   
    var AssessmentYear = $('#AssessmentYear_Code').val();
    var url = "/TDS/TDSDeclarationDetailsOld/Create?EmployeeGI=" + EmployeeGI + "&AssessmentYear=" + AssessmentYear;
    console.log(url);
    window.location.href = url;

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


//------------------DOCUMENT VIEW-------------------
function GetUploadDocumentPopUp(idType, TrnCode) {

    DocHead = '';
    if (idType == 'ACT_HORE') {
        DocHead = ' House Rental';
    }
    else if (idType == 'ACT_I80C') {
        DocHead = 'Investment Under Section 80-C';
    }
    else if (idType == 'LIC') {
        DocHead = 'Investment Under Section 80-C-LIC';
    }
   
    else if (idType == 'ACT_SOHL') {
        DocHead = 'Self Occupied House Loan Repayment';
    }


    else if (idType == 'ACT_IFHP') {
        DocHead = 'Income from Let Out House Property';
    }
    else if (idType == 'OtherIncome') {
        DocHead = 'Other Income';
    }
    else if (idType == 'ACT_CH4A' || idType == 'CH4A') {
        DocHead = 'Chapter VI A -' + TrnCode;
    }


    var assYear = $('#AssessmentYear_Code').val();
    var empGI = $('#EmployeeGuid').val();

    $.ajax({
        type: 'GET',
        url: "/TDS/TDSDeclarationDetailsOld/GetPopUpforDocUpload",
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


    var url = "/TDS/TDSDeclarationDetailsOld/ViewDocument?TrnCode=" + TrnCode;

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