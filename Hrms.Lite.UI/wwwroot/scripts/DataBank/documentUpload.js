var prevTab = 'tab1';
var currentTab = 0;



function fillData() {

    debugger;



   


    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var TabIndex = currentTab;
    var search = $('#SearchId').val();
    var filtercode = $("#FilterCombo_Code").val();
    var dataTable = $("#docUploadTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/DocumentUpload/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + TabIndex + '&filtercode=' + filtercode + '&SearchId=' + search + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



                $('#DocumentUploadList_Count').val(d.recordsTotal);

                debugger;
               

                $('.AllTabCount').html(d.allTabCount);
                $('.Tab1Count').html(d.update);
                $('.Tab2Count').html(d.notUpdate);
                 
                var breadcrumb = 'DocumentUpload';
                var breadcrumbname = 'DocumentUpload List';

                for (var i = 0, ien = d.data.length; i < ien; i++) {


                    d.data[i].employeeName = '<a onclick="onEmployeeClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + breadcrumb + "'" + ',' + "'" +breadcrumbname + "'" + ')">' + d.data[i].employeeName+'</a>'

                    if (d.data[i].aadhar != true)
                    {
                        d.data[i].aadhar = '<button type="button" class="btn btn-icon btn-close border-radius"><i class="icon-xl fas fa-times"></i></button>'

                    }
                    else
                    {
                        d.data[i].aadhar = '<button type="button" class="btn btn-icon btn-tick border-radius"><i class="icon-xl fas fa-check"></i></button>'

                    }


                    if (d.data[i].pan!= true) {
                        d.data[i].pan = '<button type="button" class="btn btn-icon btn-close border-radius"><i class="icon-xl fas fa-times"></i></button>'

                    }
                    else {
                        d.data[i].pan = '<button type="button" class="btn btn-icon btn-tick border-radius"><i class="icon-xl fas fa-check"></i></button>'

                    }


                    if (d.data[i].drivingLicense != true) {
                        d.data[i].drivingLicense = '<button type="button" class="btn btn-icon btn-close border-radius"><i class="icon-xl fas fa-times"></i></button>'


                    }
                    else {
                        d.data[i].drivingLicense = '<button type="button" class="btn btn-icon btn-tick border-radius"><i class="icon-xl fas fa-check"></i></button>'

                    }



                    if (d.data[i].passport != true) {
                        d.data[i].passport = '<button type="button" class="btn btn-icon btn-close border-radius"><i class="icon-xl fas fa-times"></i></button>'

                    }
                    else {
                        d.data[i].passport = '<button type="button" class="btn btn-icon btn-tick border-radius"><i class="icon-xl fas fa-check"></i></button>'

                    }



                    if (d.data[i].others != true) {
                        d.data[i].others = '<button type="button" class="btn btn-icon btn-close border-radius"><i class="icon-xl fas fa-times"></i></button>'

                    }
                    else {
                        d.data[i].others = '<button type="button" class="btn btn-icon btn-tick border-radius"><i class="icon-xl fas fa-check"></i></button>'

                    }

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "empId", "width": "13%" },
            { "data": "employeeName", "width": "17%" },
            { "data": "designation.name", "width": "16%" },
            { "data": "department.name", "width": "12%" },
            { "data": "location.name", "width": "12%" },
            { "data": "aadhar", "width": "10%" },
            { "data": "pan", "width": "10%" },
            { "data": "drivingLicense", "width": "8%" },
          
            { "data": "passport", "width": "10%" },
            { "data": "others", "width": "8%" },
          


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
    /*    dataTable.search('').draw();*/
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



   




}




function tabClick(Tab, TabIndex) {
    debugger;
    currentTab = TabIndex;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");  
    if (TabIndex != 2) {
        $('.filterList').addClass('hide')
        var searchid = 0;
    }
    else {
        $('.filterList').removeClass('hide')
        var searchid = 0;
    }

    fillData();
    prevTab = Tab;
}

function TabsDropdown(TabIndex) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //if ($('#FilterList_Code').val() != 0)
    //    var searchid = $('#FilterList_Code').val();
    //else
    //    var searchid = 3;
    if (TabIndex != 2) {
        $('.filterList').addClass('hide')
        var searchid = 0;
    }
    else {
        $('.filterList').removeClass('hide')
        var searchid = 0;
    }

    //var url = "/DataBank/DocumentUpload/GetDocumentUploadList";
    //$('#docUploadList').load(url, { TabIndex: TabIndex, SearchId: searchid }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'tab1'
    else if (TabCode == 1)
        return 'tab2';   
    else
        return 'tab3';

}

function onFilterComboChange(sender) {
    debugger;
    var SearchId = $(sender).val();
    //var url = "/DataBank/DocumentUpload/GetDocumentUploadList";
    //$('#docUploadList').load(url, { TabIndex: currentTab, SearchId: SearchId }, function () { });
   /* showSpinner();*/
    /*ApplyFilter('apply');*/
    fillData();
}

function onAddNewClick(EmployeeGI) {
    debugger;
    var url = "/DataBank/DocumentUpload/AddDocument";
    $('body').append('<div class="form-overlay"></div>');
    $('#addDocument').load(url, { EmployeeGI: EmployeeGI }, function () { });
/*    $('#addDocument').removeClass('hide');*/
    
}

function CheckImageExtensionSize(e) {
    debugger;    
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['pdf','jpeg', 'jpg', 'png', 'bmp'];    
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
        return false;
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
        return false;
    }
    else {
        return true;
    }

}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterCombo_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
           
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
        
    }
    else {
       
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
      
    }
}

function validateAadhar(val, id) {
    debugger;
    var flag;
    aadhar = val.value;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        var filter = /^[2-9]{1}[0-9]{11}$/
        //$(val).addClass('is-invalid');
        if (!filter.test(aadhar)) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html('Invalid Aadhar number')

        }
        else {
            $.getJSON("/DataBank/DocumentUpload/AlreadyExistCheck", { EmployeeGI: $('#EmployeeGI').val(), Pan: $("#PANNumber").val(), Aadhar: $("#AadharNumber").val() }, function (data) {
                if (data.alreadyExists_OnBoarding != null) {
                    debugger;
                    //swal.fire(data.alreadyExists_OnBoarding[0].message);
                    $("#" + id + "").addClass('error-message');
                    $(val).addClass('is-invalid');
                    $("#" + id + "").html(data.alreadyExists_OnBoarding[0].message)
                    flag = false;
                }
                else {
                    debugger;
                    $("#" + id + "").removeClass('error-message');
                    $(val).removeClass('is-invalid');
                    $("#" + id + "").html("");
                    //trim(val);
                    flag = true;
                    return true;
                }
            });
            return flag;
        }

    }
}

function validatePan(val, id) {
    var pan = val.value;
    var flag;
    if (val.value == "") {
        $("#" + id + "").addClass('error-message');
        $(val).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        var panPat = /^([a-zA-Z]{5})(\d{4})([a-zA-Z]{1})$/;
        var code = /([C,P,H,F,A,T,B,L,J,G])/;
        var code_chk = pan.substring(3, 4);
        if (pan.search(panPat) == -1) {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("Invalid Pan Number");
            return false;
        }
        else if (code.test(code_chk) == false) {
            $("#" + id + "").addClass('error-message');
            $(val).addClass('is-invalid');
            $("#" + id + "").html("Invalid Pan Number");
            return false;
        }
        else {
            $.getJSON("/DataBank/DocumentUpload/AlreadyExistCheck", { EmployeeGI: $('#EmployeeGI').val(), Pan: $("#PANNumber").val(), Aadhar: $("#AadharNumber").val() }, function (data) {
                if (data.alreadyExists_OnBoarding != null) {
                    debugger;
                    //swal.fire(data.alreadyExists_OnBoarding[0].message);
                    $("#" + id + "").addClass('error-message');
                    $(val).addClass('is-invalid');
                    $("#" + id + "").html(data.alreadyExists_OnBoarding[0].message)
                    flag = false;
                }
                else {

                    $("#" + id + "").removeClass('error-message');
                    $(val).removeClass('is-invalid');
                    $("#" + id + "").html("");
                    flag = true;
                    return true;
                }
            });
            return flag;
        }
    }
}



function onEmployeeClick(employeeGI, breadcrumb, breadcrumbname, TabName) {
    debugger;
    var url = "/DataBank/DocumentUpload/Create?EmployeeGI=" + employeeGI + "&BreadCrumb=" + breadcrumb + "&BreadCrumbName=" + breadcrumbname + "&TabName=" + TabName;
    window.location.href = url;
}

function EditAadharOrPan(DocName) {
    var url = "/DataBank/DocumentUpload/UploadAadharOrPan";
    var EmployeeGI = $('#EmployeeGI').val();
    $('body').append('<div class="form-overlay"></div>');
    $('#aadharOrPanUpload').load(url, { DocumentName: DocName, EmployeeGI: EmployeeGI }, function () { });
    $('#aadharOrPanUpload').removeClass('hide');
}
function DeleteIdentityDocuments() {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var EmployeeGI = $('#EmployeeGI').val();  
            var sliderTitle = $('#SliderTitle').val();  
            var url = "/DataBank/DocumentUpload/DeleteIdentityDocuments?EmployeeGI=" + EmployeeGI + "&sliderTitle=" + sliderTitle;
            window.location.href = url;
        }
        else {
            return false;
        }
    })
}

function DeleteOtherDocuments() {
    swal.fire({
        text: "Do you want to Delete?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            var LogGI = $('#SliderDocumentLogGI').val();
            var OtherDocGI = $('#SliderDocumentGI').val();
            var EmployeeGI = $('#EmployeeGI').val();
            var url = "/DataBank/DocumentUpload/DeleteOtherDocuments?LogGI=" + LogGI + "&OtherDocGI=" + OtherDocGI + "&EmployeeGI=" + EmployeeGI;
            window.location.href = url;
        }
        else {
            return false;
        }
    })
}

function EditDrivingLicenseOrPassport(DocName) {
    var url = "/DataBank/DocumentUpload/UploadDrivingLicenseOrPassport";
    var EmployeeGI = $('#EmployeeGI').val();
    $('body').append('<div class="form-overlay"></div>');
    $('#licenseOrPassport').load(url, { DocumentName: DocName, EmployeeGI: EmployeeGI }, function () { });
    $('#licenseOrPassport').removeClass('hide');
}

function EditOtherDocuments(OtherDocGI,LogGI) {
    var url = "/DataBank/DocumentUpload/EditOtherDocuments";   
    var EmployeeGI = $('#EmployeeGI').val();
    $('body').append('<div class="form-overlay"></div>');
    $('#addDocument').load(url, { OtherDocGI: OtherDocGI, LogGI: LogGI, EmployeeGI: EmployeeGI }, function () { });
   /* $('#otherDocumentEdit').removeClass('hide');*/
}

function previewDocument(absoluteURI) {
    window.open(absoluteURI, "_blank");
}
function displayDoc(docType, IdentityDocType, OtherDocGI) {


    var url = "/DataBank/DocumentUpload/ViewDoc?EmployeeGI=" + $("#EmployeeGI").val() + "&DocType=" + docType + "&IdentityDocType=" + IdentityDocType
        + "&OtherDocGI=" + OtherDocGI; 


    //var url = "/DataBank/DocumentUpload/ViewDoc?EmployeeGI=" + $("#EmployeeGI").val() +
    //    "&DocType=" + docType +
    //    "&IdentityDocType=" + IdentityDocType +
    //    "&OtherDocGI=" + OtherDocGI;
    window.open(url, "_blank");
    //$.ajax({
    //    url: url,
    //    type: "GET",
    //    success: function () {
    //        // File exists → open in new tab
    //        window.open(url, "_blank");
    //    },
    //    error: function (xhr) {
    //        let response = JSON.parse(xhr.responseText);

    //        Swal.fire({
    //            icon: "error",
    //            title: "Error",
    //            text: response.message  // localized message
    //        });
    //    }
    //});

}
function validateToDate(val, id,CompareElement) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var issueDate = new Date($('#' + CompareElement).val());
        var dueDate = new Date($(val).val());
        var d = new Date();
        var iDate = new Date(issueDate.toLocaleDateString());
        var dDate = new Date(dueDate.toLocaleDateString());

        if (iDate > dDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Valid to date should be greater than valid from date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");

            $('#' + CompareElement).removeClass('is-invalid');
            $("#FromDate").removeClass('error-message');
            $("#FromDate").html("");
            return true;
        }
    }
}

function validateFromDate(val, id, CompareElement) {
    debugger;
    if ($(val).val() == "") {
        $(val).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {

        var issueDate = new Date($('#' + CompareElement).val());
        var dueDate = new Date($(val).val());
        var d = new Date();
        var iDate = new Date(issueDate.toLocaleDateString());
        var dDate = new Date(dueDate.toLocaleDateString());

        if (iDate < dDate) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("Valid from date should be less than valid to date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");


            $('#' + CompareElement).removeClass('is-invalid');
            $("#ToDate").removeClass('error-message');
            $("#ToDate").html("");
            return true;
        }
    }
}





function onExportToExcelClick() {
    debugger;
    //if ($('#FilterList_Code').val() != 0)
    //    var searchid = $('#FilterList_Code').val();
    //else

    if (currentTab != 2) 
        var searchid = 0;
    else
        var searchid = $('#FilterCombo_Code').val();

    var docUploadTableLength = $('#DocumentUploadList_Count').val();
    if (docUploadTableLength > 0) {
        url = "/DataBank/DocumentUpload/ExportToExcel?TabIndex=" + currentTab + "&SearchId=" + searchid + "&totRecords=" + docUploadTableLength + '&data=' + $('#BasicFilter').serialize();
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function GetOtherDocumentsList() {
    var EmployeeGI = $('#EmployeeGI').val()
    url = "/DataBank/DocumentUpload/GetOtherDocumentsList";
    $('#OtherDocsList').load(url, { EmployeeGI: EmployeeGI }, function () { });
}

function validateAll(mode) {
    debugger;
    var msg;
    var data;
    var i = 0;
    if (!validateById('#SliderDocumentType_Code', 'DocType')) {
        i++;
    }
    if (!validateById('#SliderDocumentName', 'DocName')) {
        i++;
    }
    if (mode != 'Modify') {
        if ($('#File_FileData').val() == '') {
            i++;
            swal.fire("Please upload the document");
        }
    }
    if (mode != 'Modify')
        msg = "Do you want to Save?";
    else
        msg = "Do you want to Modify?";
    if (mode != 'Modify')
        data = new FormData($('#otherDocumentsSave')[0]);
    else
        data = new FormData($('#editOtherDocumentsSave')[0]);
    debugger;
    if (i == 0)
        swal.fire({
            text: msg,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {               
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/DocumentUpload/OtherDocumentSave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire(result.message).then(function () {
                                if (mode != 'Modify')
                                    CloseSlider('addnewDoc');
                                else
                                    CloseSlider('editOtherDoc');
                                GetOtherDocumentsList();

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
    else
        return false;
}

function validateAadharOrPan() {
    $.ajaxSetup({
        async: false
    });
    if ($('#SliderTitle').val() != "Pan Card") {
        var AadharNum = document.getElementById("AadharNumber");
        var i = 0;
        //abcd = validateAadhar(AadharNum, 'aadhar');
        debugger;
        if (!validateById('#NameInAadhar', 'NameValidation')) {
            i++;
        }        
        /*if (!validateAadhar('AadharNumber', 'aadhar')) {*/
            if (!validateAadharNew('hdnAadhar', 'aadhar', document.getElementById("hdnAadhar").value)) {
            i++;
        }
        if (!validateById('#AadharIssueDate', 'IssueDateValidation')) {
            i++;
        }
        if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
            i++;
            swal.fire("Please upload the document");
        }       
       
    }
    else {
        var PanNum = document.getElementById("PANNumber");
        var i = 0;
        if (!validateById('#NameInPAN', 'NameValidation')) {
            i++;
        }
        if (!validatePan('PANNumber', 'pan')) {
            i++;
        }
        if (!validateById('#PANIssueDate', 'IssueDateValidation')) {
            i++;
        }
        if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
            i++;
            swal.fire("Please upload the document");
        }   
        
    }
    if (i == 0)
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var data = new FormData($('#AadharOrPanSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/DocumentUpload/IdentityDocumentSave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                CloseSlider('addnewDoc');
                                window.location.reload();                                
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
    else
        return false;
}

function validateDrivingLicenseOrPassport() {
    if ($('#SliderTitle').val() != "Passport") {

        var i = 0;
        if (!validateById('#NameInDrivingLicense', 'NameValidation')) {
            i++;
        }
        if (!validateById('#DrivingLicenseNumber', 'DocNumber')) {
            i++;
        }
        if (!validateFromDate($('#DrivingLicenseValidFrom'), 'FromDate','DrivingLicenseValidTo')) {
            i++;
        }
        if (!validateToDate($('#DrivingLicenseValidTo'), 'ToDate','DrivingLicenseValidFrom')) {
            i++;
        }
        if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
            i++;
            swal.fire("Please upload the document");
        }   
      
    }
    else {
        var i = 0;
        if (!validateById('#NameInPassport', 'NameValidation')) {
            i++;
        }
        if (!validateById('#PassportNumber', 'DocNumber')) {
            i++;
        }
        if (!validateFromDate('#PassportValidFrom', 'FromDate','PassportValidTo')) {
            i++;
        }
        if (!validateToDate('#PassportValidTo', 'ToDate','PassportValidFrom')) {
            i++;
        }
        if ($('#File_FileData').val() == '' && $('#File_AbsoluteUri').val() == '') {
            i++;
            swal.fire("Please upload the document");
        }   
        
    }

    if (i == 0)
        swal.fire({
            text: "Do you want to Modify?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var data = new FormData($('#LicenseOrPassportSave')[0]);
                $.ajax({

                    type: 'POST',
                    url: '/DataBank/DocumentUpload/IdentityDocumentSave',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: data,
                    success: function (result) {
                        if (result.success == true) {
                            swal.fire("Modified Successfully").then(function () {
                                CloseSlider('addnewDoc');
                                window.location.reload();

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
    else
        return false;

}
function onDocumentChange(sender, id) {
    debugger;
    var cid = $(sender).val();
    $.getJSON("/DataBank/DocumentUpload/GetDocument", { id: cid }, function (data) {
        console.log(data);
        $('#docTypes').html(data);

    });
}

function DownloadPan() {
    window.location.href = '/DataBank/DocumentUpload/DownloadAllPan';
}
function maskAadhar(input) {
    debugger
    let val = input.value;
    // Store original in hidden field
    document.getElementById("hdnAadhar").value = val;
    var filter = /^[2-9]{1}[0-9]{11}$/
    if (filter.test(input.value)) {
        debugger;
        // Show only last 4 digits
        input.value = val.replace(/\d(?=\d{4})/g, "*");
    }
}

function validateAadharNew(val, id, mandatory) {
    debugger;
    aadhar = $('#' + val).val();
    if ($("#" + mandatory + "").val() != "False") {

        if ($('#' + val).val() == "") {
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html("This field is required");
        }
        else {
            var filter = /^[2-9]{1}[0-9]{11}$/
            if (!filter.test(aadhar)) {
                debugger;
                $("#" + id + "").addClass('error-message');
                $('#' + val).addClass('is-invalid');
                $("#" + id + "").html('Invalid Aadhar number')
                return false;
            }
            else {
                $("#" + id + "").removeClass('error-message');
                $('#' + val).removeClass('is-invalid');
                $("#" + id + "").html("");

                return true;
            }
        }
    }
    else {
        var filter = /^[2-9]{1}[0-9]{11}$/
        if (!filter.test(aadhar)) {
            debugger;
            $("#" + id + "").addClass('error-message');
            $('#' + val).addClass('is-invalid');
            $("#" + id + "").html('Invalid Aadhar number')
            return false;
        }
        else {
            $("#" + id + "").removeClass('error-message');
            $('#' + val).removeClass('is-invalid');
            $("#" + id + "").html("");

            return true;
        }
        return true;
    }
}