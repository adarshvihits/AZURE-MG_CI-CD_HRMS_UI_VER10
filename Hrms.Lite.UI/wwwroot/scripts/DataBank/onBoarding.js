var prevTab = 'all';
var currentTab = 0;
var CTC = 0;
var currentTabName = 'All';
var searchName = 'Allocated';
function Tab(Tab, TabName) {

    debugger;
    if (Tab != 9) {
        $('.export').removeClass('hide');
    }
    else {
        $('.export').addClass('hide');
    }
    debugger;    
    $('.spinner-outer').show();
    currentTab = Tab;
    currentTabName = TabName;
    $('#TabCode').val(Tab);



    var TabId = GetTabId($("#TabCode").prop('selectedIndex'));

    $(".all").removeClass("active");
    $("." + prevTab).removeClass("active");
    $("." + TabId).addClass("active");    

    $("#Tabindex").val(currentTab);
    var Filter = $('#BasicFilter').serialize();
    $.ajax({

        type: 'GET',
        url: '/DataBank/OnBoarding/OnBoardingTabs',
        dataType: 'html',
        data: Filter + "&Tabindex="+ Tab ,

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
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });


    $("#Tabindex").val(currentTab);
    prevTab = TabId;
}



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;   

    var dataTables = $("#offerLetterListTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/OnBoarding/GetOfferLetterList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocType=' + 1+ '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {

                $('#OfferLetterList.Count').val(d.recordsTotal);


                for (var i = 0, ien = d.data.length; i < ien; i++) {



                    d.data[i].employeeName = '<a href="/DataBank/OnBoarding/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + d.data[i].employeeName + '</a>'

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "16%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "doj", "width": "8%" },
         



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
        debugger
        dataTables.search(this.value).draw();

        $('#noDataFound').css("display", 'none');

    });

    $('.search-slider-inner').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
    });

    dataTables.on('draw', function () {
        $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.next').attr("title", "Next");
        $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.previous').attr("title", "Previous");
        $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.first').attr("title", "First");
        $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
        $('.paginate_button.last').attr("title", "Last");

    });

/*    $.fn.DataTables.ext.pager.numbers_length = 5;*/

}















function GetTabId(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else if (TabCode == 3)
        return 'tab3';
    else if (TabCode == 4)
        return 'tab4';
    else if (TabCode == 5)
        return 'tab5';
    else if (TabCode == 6)
        return 'tab6';
    else if (TabCode == 7)
        return 'tab7';
    else if (TabCode == 8)
        return 'tab8';
    else if (TabCode == 9)
        return 'tab9';
}


function getActiveVacancyValidn(Department, Designation, Location,MRFCode)
{
    debugger
    $.ajax({
        type: 'POST',
        url: "/DataBank/OnBoarding/GetOnboardingActiveVacanciesValidation",
        dataType: 'json',
        data: { MRFCode: MRFCode },
      
        success: function (result) {
            if (result.success == true) {
                
                    var url = "/DataBank/OnBoarding/DirectOffer?Dept=" + Department +
                        "&Desgn=" + Designation + "&Loc=" + Location + "&MRFCode=" + MRFCode;
                    window.location.href = url;               

            }
            else 
                swal.fire(result.message);  

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


function CTCCalculation() {
    debugger;
    var TotAmt = parseFloat("0");
    var TotGrossAmt = parseFloat("0");
    var TotCTCAmt = parseFloat("0");
    var TotDedAmt = parseFloat("0");
    $('.AllowanceAmount').each(function (i, obj) {
        var Amt = parseFloat($("#PreEnrollment_Compensation_FixedAllowance_" + i + "__Amount").val());

        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotAmt = TotAmt + Amt;

        TotGrossAmt = TotGrossAmt + Amt;

        $("#GrossSalary").html(TotGrossAmt);
        console.log(TotGrossAmt);
        $("#PreEnrollment_Compensation_GrossSalary").val(TotGrossAmt);
    });
    $('.ProvisionsAmount').each(function (i, obj) {
        var Amt = parseFloat($("#PreEnrollment_Compensation_FixedProvisions_" + i + "__Amount").val());
        var CtcApp = parseInt($("#PreEnrollment_Compensation_FixedProvisions_" + i + "__CtcApplicable").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        if (CtcApp == 2) {
            TotCTCAmt = TotCTCAmt + Amt;
        }       

    });
        $("#CTC").html(TotCTCAmt + TotGrossAmt);
        $("#PreEnrollment_Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    $('.DeductionsAmount').each(function (i, obj) {//Total Dedcutions
        var Amt = parseFloat($("#PreEnrollment_Compensation_FixedDeduction_" + i + "__Amount").val());
        debugger;
        if (isNaN(Amt)) {
            Amt = parseFloat("0");
        }
        TotDedAmt = TotDedAmt + Amt;

        //$("#Compensation_CTC").val(TotCTCAmt + TotGrossAmt);

    });
        $("#totDeductions").html(TotDedAmt);

    TakeHome = TotGrossAmt - TotDedAmt;//Take Home

    $("#takeHome").html(TakeHome);
}



function RejectPreEnrollment(GI){
    debugger
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
                url: '/Databank/OnBoarding/ApproveRejectPreEnrollment',
                dataType: 'json',
                data: { preEnrollGI:GI,mode:"REJECT"},
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/OnBoarding/Index";
                            window.location.href = url;
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            //var url = "/Master/Country/Approve?CountryGI=" + GI + "&LogGI=" + LogGI;
                            //$('#ApproveSlider').load(url, function () { });
                            //$('body').append('<div class="form-overlay"></div>');

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
function ApprovePreEnrollment(GI) {
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
                url: '/Databank/OnBoarding/ApproveRejectPreEnrollment',
                dataType: 'json',
                data: { preEnrollGI: GI,mode:"APPROVE" },
                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/OnBoarding/Index";
                            window.location.href = url;
                        });

                    }
                    else {
                        swal.fire(result.message).then(function () {
                            //var url = "/Master/Country/Approve?CountryGI=" + GI + "&LogGI=" + LogGI;
                            //$('#ApproveSlider').load(url, function () { });
                            //$('body').append('<div class="form-overlay"></div>');

                        });
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    //toastr.error(xhr.responseText);
                    //$('body').html(xhr.responseText);
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

/*--region checklist*/
function validatestatus() {
    debugger
    status = true;
    flag = 0;
    try {
    $(".ChecklistStatus").each(function (index, object) {

        if ($("#JoiningCheckList_" + index + "__IsMandatory").val() == "True") {


            if ($("#JoiningCheckList_" + index + "__JoiningCheckStatus_Code").val() != 2) {              
                swal.fire("Please complete the mandatory items in the checklist to proceed further");
                flag = 1;
                throw new Error()
            }
        }

    });
    }
    catch (error) {
        debugger
        if (flag == 1) {
           
        status = false;
        return status;
        }
    }
    return status;
}
function onstatuschange(index, spanId)
{
    debugger
    if ($("#JoiningCheckList_" + index + "__IsMandatory").val() == 'True') {
        if ($("#JoiningCheckList_" + index + "__JoiningCheckStatus_Code").val() != 2) {
            $("#JoiningCheckList_" + index + "__JoiningCheckStatus_Code").addClass('is-invalid');
            $("#" + spanId + index).addClass('error-message');
            $("#" + spanId + index).html("This field is required");
            return false;
        } else {
            $("#JoiningCheckList_" + index + "__JoiningCheckStatus_Code").removeClass('is-invalid');
            $("#" + spanId + index).removeClass('error-message');
            $("#" + spanId + index).html("");
            //trim(val);        
            return true;
        }
    }
}
function ChecklistSave() {
    if (validatestatus() != "false") {
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
                    url: "/DataBank/OnBoarding/CheckListSave",
                    dataType: 'json',
                    data: $('#checklistCreate').serialize(),
                    //processData: false,
                    //contentType: false,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                var url = "/DataBank/OnBoarding";
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
        });

    }
}

//end checklist
function ApplyFilter(mode) {
    debugger

    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    debugger;
  
    $("#Tabindex").val(currentTab);

    if (currentTab == 9) {

        if (mode == 'apply') {
            if (filterApplicable >= 1) {
                fillData();
                debugger;
                CloseSlider('basicFilter');
                $('#filterForm').removeClass('filter-apply');
            }
            else {
                /*   showSpinner();*/
                fillData();
                debugger;
                CloseSlider('basicFilter');
                $('#filterForm').addClass('filter-apply');
            }

        }
        else {
            /* showSpinner();*/
            filterApplicable = 1;
            $('.reset').val('');
            $('#Optional_Filter_EmploymentStatus_Code').val(0);
            $('#filterForm').removeClass('filter-apply');
            fillData();
            /*CloseSlider('basicFilter');*/
        }

    }
    else {
        if (mode == 'apply') {
            if (filterApplicable >= 1) {
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/OnBoarding/ApplyFilter',
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
                    url: '/DataBank/OnBoarding/ApplyFilter',
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
                url: '/DataBank/OnBoarding/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize(),
                success: function (result) {
                    debugger


                    //$('#OnBoardingList').html("");
                    //$('#OnBoardingList').html(result);
                    //$('#OnBoardingList').removeClass('hide');

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

   
}

function onEmployeeClick(employeeGI, breadcrumb, breadcrumbname) {
    debugger;
    var url = "/DataBank/DocumentUpload/Create?EmployeeGI=" + employeeGI + "&BreadCrumb=" + breadcrumb + "&BreadCrumbName=" + breadcrumbname ;
    window.location.href = url;
}
function onExportToExcelClick() {
    debugger;
    var actvacCount = $("#ActiveVacancyList_Count").val();
    var tab = $("#OnBoardingList_Count").val();
    if (currentTab != 7) {
   
        if (tab > 0) {
            url = "/DataBank/OnBoarding/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + currentTabName;
            window.location.href = url;



            //$.ajax({
            //    url: '/DataBank/OnBoarding/onExportToExcelClick',
            //    type: 'POST',
            //    contentType: 'application/json',
            //   /* data: JSON.stringify(data),*/
            //    data: $('#BasicFilter').serialize()+ "&TabIndex=" + Tab + "&TabName=" + currentTabName,
            //    success: function (response) {
            //        // Create a download link and trigger a click event to download the Excel file
            //        var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
            //        var url = window.URL.createObjectURL(blob);
            //        var a = document.createElement('a');
            //        a.href = url;
            //        a.download = "OnBoarding(" + currentTabName + ") List.xlsx";
            //        document.body.appendChild(a);
            //        a.click();
            //        window.URL.revokeObjectURL(url);
            //    },
            //    error: function (error) {
            //        console.error('Error exporting data to Excel:', error);
            //    }
            //});


        }
        else {
            swal.fire("No data found to export");
        }
    }
    
    else  {

        if (actvacCount > 0) {
            url = "/DataBank/OnBoarding/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + currentTabName;
            window.location.href = url;
            //$.ajax({
            //    url: '/DataBank/OnBoarding/onExportToExcelClick',
            //    type: 'POST',
            //    contentType: 'application/json',
            //    /* data: JSON.stringify(data),*/
            //    data: $('#BasicFilter').serialize() + "&TabIndex=" + Tab + "&TabName=" + currentTabName,
            //    success: function (response) {
            //        // Create a download link and trigger a click event to download the Excel file
            //        debugger
            //        var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
            //        var url = window.URL.createObjectURL(blob);
            //        var a = document.createElement('a');
            //        a.href = url;
            //        a.download = "OnBoarding (" + currentTabName + ") List.xlsx";
            //        document.body.appendChild(a);
            //        a.click();
            //        window.URL.revokeObjectURL(url);
            //    },
            //    error: function (error) {
            //        console.error('Error exporting data to Excel:', error);
            //    }
            //});
        }
        else {
            swal.fire("No data found to export");
        }
    }

   
}


function RejectEnroll(PreEnrollGI) {

    swal.fire({

        text: "Do you want Reject?",
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Reason is mandatory'
                )
            }
        }

    }).then((result) => {
        if (result.isConfirmed) {
            debugger;
            $.ajax({

                type: 'POST',
                url: '/DataBank/EnrollmentTest/RejectEnroll',
                dataType: 'json',
                data: { PreEnrollGI: PreEnrollGI, Reason: result.value },
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        swal.fire("Rejected Successfully").then(function () {
                            $('.tab3').trigger('click');
                        });

                    }
                    else {
                        swal.fire(result.message);
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