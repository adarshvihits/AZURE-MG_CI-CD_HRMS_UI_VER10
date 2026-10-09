var prevTab = 'VerificationNotDone';
var currentTab = 'Not Done';


//function previewDocument(absoluteURI) {
//    debugger;
//    window.open(absoluteURI, "_blank");
//}


//if (TabIndex = 'VerificationDone')
//    var prevTab = 'VerificationDone';
//else
//    var prevTab = 'VerificationNotDone';

function viewFile() {
    debugger;
    var AbsoluteURI = $('#Attachment_AbsoluteUri').val();
    if (AbsoluteURI == "") {
        swal.fire("No supporting files added");
    }
    else {
        window.open(AbsoluteURI);
    }
}
function previewDocument() {
    debugger;

    if ($('#Attachment_FileData').val() == '')
        swal.fire("No supporting files added");
    else {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
}

function fillData() {

    debugger;
    var TabIndex = currentTab;
    if (TabIndex == 'Not Done') {

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

        //var tabIndex = 'Not Done';



        var dataTable = $("#verificationTabble").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/PoliceVerifications/GetVerificationList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {





                    




                    $('#PoliceVerificationList_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].employeeName = '<a onclick="onNotDoneEmpClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].policeVerificationGI + "'" + ')">' + d.data[i].employeeName + '</a>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID","width":"12%" },
                { "data": "employeeName","width":"25 %" },
                { "data": "designation", "width": "22 %" },
                { "data": "department", "width": "21 %" },
                { "data": "location", "width": "20 %" },



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
            dataTable.search('').draw();
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
    else
    {
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

      /*  var tabIndex = 'Done';*/



        var dataTable = $("#DoneList").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/PoliceVerifications/GetVerificationList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&TabIndex=' + TabIndex + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {










                    $('#PoliceVerificationList_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].verificationDate = _GetFormattedDate(d.data[i].verificationDate);
                        d.data[i].employeeName = '<a onclick="OnDoneListEmpClick(' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].policeVerificationGI + "'" + ')">' + d.data[i].employeeName + '</a>'

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "18%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "location", "width": "13%" },
                { "data": "verificationDate", "width": "12%" },
                { "data": "policeStationDetails", "width": "17%"},
                



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
            dataTable.search('').draw();
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




}








function OnTabClick(Tab, TabIndex) {
    debugger;
    //$.ajaxSetup({
    //    async: false
    //});
    showSpinner();
    currentTab = TabIndex;
   
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");

    //var url = "/DataBank/PoliceVerifications/GetList?TabIndex=" + currentTab;

    //$('#verificationNotDoneList').load(url, {}, function () { hideSpinner(); });


    $.ajax({

        type: 'GET',
        url: '/DataBank/PoliceVerifications/GetList',
        dataType: 'html',
        data: { TabIndex: currentTab },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#verificationNotDoneList').html("");
                $('#verificationNotDoneList').html(result);
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

function OnDoneListEmpClick(EmpGI, VerificationGI) {
    debugger;
    showSpinner();
    var Type = 0;
   
    //var url = "/DataBank/PoliceVerifications/GetDeleteDetails?Type=" + Type + "&EmpGI=" + EmpGI + "&VerificationGI=" + VerificationGI;
    //$('#VerificationDelete').load(url, function () { hideSpinner(); });

    $.ajax({

        type: 'GET',
        url: '/DataBank/PoliceVerifications/GetDeleteDetails',
        dataType: 'html',
        data: { Type: Type, EmpGI: EmpGI, VerificationGI: VerificationGI },

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#VerificationDelete').html("");
                $('#VerificationDelete').html(result);
            }
            hideSpinner();
            $('body').append('<div class="form-overlay"></div>');

        },


        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();

        }
    });

}

function onNotDoneEmpClick(EmpGI, VerificationGI) {
    debugger;

    var Type = 0;
    var url = "/DataBank/PoliceVerifications/Create?Type=" + Type + "&EmpGI=" + EmpGI + "&VerificationGI=" + VerificationGI;
    window.location.href = url;

}
function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp', 'pdf'];
    console.log(size);
    if (size > 1024 * 1024) {
        swal.fire('File size cannot exceeds 1 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }

}


function SaveVerificationDetails() {
    debugger;
   
  if (ValidateVerification()) {

    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            var data = new FormData($('#Verification_Save')[0]);
            $.ajax({

                type: 'POST',
                url: '/DataBank/PoliceVerifications/Save',
                contentType: false,
                processData: false,
                dataType: 'json',
               
                data : data,
                

                success: function (result) {
                    if (result.success == true) {
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/DataBank/PoliceVerifications/Index";
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
        else {
            return false;
        }
    })

    }
    else {
        return false;
    }

}




function ValidateVerification() {
    var i = 0;
    if (!validateById('#VerificationDate', 'Vdate')) {
        i++;
    }
    if (!validateById('#PoliceStationDetails', 'details')) {
        i++;
    }
    if (!validateById('#DetailsAuthority', 'authority')) {
        i++;
    }


    if (i == 0)
        return true;
    else
        return false;
}












function ConfirmDelete(VerificationGI) {
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
            url = "/DataBank/PoliceVerifications/Delete?VerificationGI=" + VerificationGI;
            window.location.href = url;

        }
        else {
            return false;
        }
    })
}





function ExportExcel() {

    debugger;
    if (currentTab == 'Not Done') {
        var tableLength = $('#PoliceVerificationList_Count').val();
        if (tableLength > 0) {
            url = "/DataBank/PoliceVerifications/ExportToExcel?TabIndex=" + currentTab + "&totRecords=" + tableLength + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }

   
    else if (currentTab == 'Done') {
        debugger;
        var tableLength = $('#PoliceVerificationList_Count').val();
        if (tableLength > 0) {
            url = "/DataBank/PoliceVerifications/ExportToExcel?TabIndex=" + currentTab + "&totRecords=" + tableLength + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

//function ApplyFilter(mode) {
//    debugger
   
//    $("#SearchId").val($('#FilterList_Code').val())
  
//    if (mode == 'apply') {
//        if (filterApplicable >= 1) {
//            fillData();
//            CloseSlider('basicFilter');
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//         /*   showSpinner();*/
//            fillData();
//            CloseSlider('basicFilter');
//            $('#filterForm').addClass('filter-apply');
//        }

//    }
//    else {
//       /* showSpinner();*/
//        filterApplicable = 1;
//        $('.reset').val('');
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $('#filterForm').removeClass('filter-apply');
//        fillData();
       
//    }
//}






function ApplyFilter(mode) {
    debugger

    $("#SearchId").val($('#FilterList_Code').val())

    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*   showSpinner();*/
            fillData();
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
function onCountryChange(sender, id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/Enrollment/GetState", { id: cid }, function (data) {

        $('#PreEnrollment_PermanentAddress_State_Code option').remove();
        $('#PreEnrollment_PermanentAddress_State_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_PermanentAddress_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
    $.getJSON("/DataBank/Enrollment/GetDistrict", { id: 0 }, function (data) {

        $('#PreEnrollment_PermanentAddress_District_Code option').remove();
        $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
    //}
}

function onStateChange(sender, id) {

    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/Enrollment/GetDistrict", { id: cid }, function (data) {

        $('#PreEnrollment_PermanentAddress_District_Code option').remove();
        $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#PreEnrollment_PermanentAddress_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
} function onCountryChange(sender, id) {

    debugger
    var cid = $(sender).val();
    if (cid == "") {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetState", { id: cid }, function (data) {

        $('#State_Code option').remove();
        $('#State_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
    $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: 0 }, function (data) {

        $('#District_Code option').remove();
        $('#District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });
    //}
}

function onStateChange(sender, id) {

    debugger;
    var cid = $(sender).val();
    if (cid == "" || cid == 0) {
        $("#" + id + "").addClass('error-message');
        $(sender).addClass('is-invalid');
        $("#" + id + "").html("This field is required");
    }
    else {
        $("#" + id + "").removeClass('error-message');
        $(sender).removeClass('is-invalid');
        $("#" + id + "").html("");
    }
    $.getJSON("/DataBank/PersonalDetails/GetDistrict", { id: cid }, function (data) {

        $('#District_Code option').remove();
        $('#District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}