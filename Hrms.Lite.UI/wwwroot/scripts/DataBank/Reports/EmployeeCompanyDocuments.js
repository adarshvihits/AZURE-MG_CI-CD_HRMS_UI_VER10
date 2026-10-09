

function GetFormatCode(TypeCode) {



    $.ajax({
        type: 'GET',
        async: false,
        url: "/DataBank/CompanyLettersDetails/GetFormatCode",
        data: { TypeCode: TypeCode },
        dataType: 'json',
        success: function (Data) {
            debugger

            return Data;

        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });




}

function findLocalSigner(data, EmployeeName, EmployeeId) {

    const ports = [9001, 9002, 9003, 9004, 9010];

    for (const port of ports) {

        const url = `http://localhost:${port}`;

        try {

            const result =  $.ajax({
                type: "GET",
                url: url + "/ping",
                dataType: "text",
                timeout: 1000,
                success: function (result) {
                    if (result.trim() === "OK") {

                        console.log("Local Signer found at: " + url);
                        sign(data, EmployeeName, EmployeeId, url);
                        return true;
                    }
                }
            });

            // Verify this is actually our Local Signer
            //if (result.trim() === "OK") {

            //    console.log("Local Signer found at: " + url);
            //    sign(data, EmployeeName, EmployeeId,url);
            //    //return url;
            //}

        }
        catch (error) {

            console.log("Local Signer not found on port: " + port);
        }
    }

    // Signer not found
    //return null;
}
function SignPDF(UserCode, empCode, LetterFormatCode, GcCode, Flag,EmployeeName,EmployeeId) {

    $.ajax({
        type: 'GET',
        async: false,
        url: "/DataBank/CompanyLettersDetails/SignPDF",
        data: { UserCode: UserCode, empCode: empCode, LetterFormatCode: LetterFormatCode, GcCode: GcCode, Flag: Flag },
        dataType: 'json',
        success: function (result) {
            debugger
            findLocalSigner(result.data, EmployeeName, EmployeeId)
            //sign(result.data, EmployeeName, EmployeeId);
            //window.location.href = 'http://localhost:9001/sign?frompath=' + result.data;
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });
}
function ping() {
    $.ajax({
        type: 'GET',
        url: "http://localhost:9001/ping",
        success: function (result) {
            debugger

            console.log(result)


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });
}
function sign(data, EmployeeName, EmployeeId,url) {

    $.ajax({
        type: 'POST',        
        url: url+'/sign',
        data: JSON.stringify({
            PdfBase64: data
        }),
        success: function (result) {
            debugger

            //console.log(result)
            var filename = 'Appointment Letter_'+EmployeeName +'_'+ EmployeeId+'.pdf'
            downloadBase64Pdf(result,filename);
            
        },
        error: function (xhr, textStatus, errorThrown) {
            debugger;
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });
}
function downloadBase64Pdf(base64, fileName) {

    // Remove data URI prefix if present
    if (base64.indexOf(",") > -1) {
        base64 = base64.split(",")[1];
    }

    // Base64 -> binary
    var byteCharacters = atob(base64);

    var byteNumbers = new Array(byteCharacters.length);

    for (var i = 0; i < byteCharacters.length; i++) {
        byteNumbers[i] =
            byteCharacters.charCodeAt(i);
    }

    var byteArray =
        new Uint8Array(byteNumbers);

    // Create PDF Blob
    var blob = new Blob(
        [byteArray],
        {
            type: "application/pdf"
        }
    );

    // Create temporary download URL
    var url =
        window.URL.createObjectURL(blob);

    var link =
        document.createElement("a");

    link.href = url;
    link.download = fileName;

    document.body.appendChild(link);

    link.click();

    document.body.removeChild(link);

    // Release memory
    window.URL.revokeObjectURL(url);
}
//function tabClick(Tab, TabIndex, TabName) {
//    $.ajaxSetup({ async: false });



//    currentTab = TabIndex;
//    currentTabName = TabName;
//    $('#TabCode').val(TabIndex);
//    $("." + prevTab).removeClass("active");
//    $("." + Tab).addClass("active");
//    //var url = "/DataBank/EmployeeProfile/GetEmployeeProfileList";
//    //$('#activeListPartial').load(url, { TabIndex: TabIndex }, function () { });
//    ApplyFilter('apply');
//    prevTab = Tab;
//}
//function GetTab(TabCode) {
//    if (TabCode == 0)
//        return 'ActiveTab'
//    else
//        return 'ResignedTab';

//}
//function GetDocumentTypeCode()
//{
//    var docVal = $("#DocumentType_Code option:selected").text();
//docVal = docVal.trim().toUpperCase();
//var Type = docVal.includes("EXPERIENCE") ? 'RESIGNED' : 'ACTIVE';
//if (Type == 'ACTIVE') {
//    $('input[id="Type"][value="ACTIVE"]').prop('checked', true);
//}
//else {
//    $('input[id="Type"][value="RESIGNED"]').prop('checked', true);
//    }
//    return Type;
//}
function fillData() {

    debugger;
    var FormatCode = 0;
    var TypeCode = $('#DocumentType_Code').val();
    var docVal = $("#DocumentType_Code option:selected").text();
    //var Type = $('#Type').val();
    /*  $('input[id="Type"]:checked').val('ACTIVE');*/
    var Type = $('input[id="Type"]:checked').val();


    $.ajax({
        type: 'GET',
        async: false,
        url: "/DataBank/CompanyLettersDetails/GetFormatCode",
        data: { TypeCode: TypeCode, Type: Type },
        dataType: 'json',
        success: function (Data) {


            FormatCode = Data;

        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.responseText)
            toastr.error(xhr.responseText);
        }
    });

    if (FormatCode == 2) {
        debugger;
        var letterCode = 2;
        var filter1 = $('#BasicFilter').serialize();
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();

        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            //$('#apptLetter').removeClass('hide');
            //$('#DoucuentTableID').addClass('hide');
            //$('#salarycertificate').addClass('hide');
            //$('#transferletter').addClass('hide');
            //$('#Increment_List').addClass('hide');
            //$('#Promotion_Letter').addClass('hide');
            //$('#Revision_Letter').addClass('hide');
            //$('#Experience_Letter').addClass('hide');




            /*  $('#JoiningReportTable').html('');*/
            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetAppointmentLetterList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetAppointmentLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].downloadBtn = '<div><a onclick="SignPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].groupCompanyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ','+"'" + d.data[i].employeeName +"'"+ ',' + d.data[i].employeeID + ')">Download</a></div>'
                            if (TypeCode == 191)
                                d.data[i].employeeName = '<div><a onclick="showCheckBoxSave(' + d.data[i].employeeCode + ',' + "'" + d.data[i].groupCompanyCode + "'" + ',' + FormatCode + ',' + "'" + d.data[i].historyCode + "'" + ',' + d.data[i].preEnrollCode + ')">' + d.data[i].employeeName + ' </a ><a onclick="SignPDF()">Sign PDF</a></div></div>'
                            else
                                d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].groupCompanyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'

                            
                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" },


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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }








    else if (FormatCode == 3) {
        debugger;
        var filter1 = $('#BasicFilter').serialize();
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {
            //$('#Increment_List').removeClass('hide');
            //$('#apptLetter').addClass('hide');
            //$('#transferletter').addClass('hide');
            //$('#DoucuentTableID').addClass('hide');
            //$('#salarycertificate').addClass('hide');
            //$('#Revision_Letter').addClass('hide');
            //$('#Experience_Letter').addClass('hide');
            //$('#Promotion_Letter').addClass('hide');


            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetIncrementLetterList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();
                            debugger;
                            // temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + DocTypeCode + '&dummy=dummy';
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';

                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetIncrementLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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



        }
    }
    else if (FormatCode == 4) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        /*  $('input[id="Type"]:checked').val('ACTIVE');*/
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        /*       var Type = $('input[id="Type"]:checked').val();*/

        if (fromDate != '' && toDate != '') {


            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetPromotionList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetPromotionLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            debugger
                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            if (TypeCode == 192)
                                d.data[i].employeeName = '<div><a onclick="showPopUpSave(' + d.data[i].employeeCode + ',' + "'" + d.data[i].groupCompanyGI + "'" + ',' + FormatCode + ',' + "'" + d.data[i].historyCode + "'" + ')">' + d.data[i].employeeName + ' </a ></div></div>'

                            else
                                d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }
    else if (FormatCode == 5) {
        debugger;
        $('#From_Period').addClass('hide');
        $('#To_Period').addClass('hide');
        var filter1 = $('#BasicFilter').serialize();
        /* $('input[id="Type"]:checked').val('RESIGNED');*/
        var FormatType = $('input[id="Type"]:checked').val();
        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        var Type = FormatType == 'RESIGNED' ? 'RESIGNED' : $('input[id="Type"]:checked').val();

        /*if (fromDate != '' && toDate != '') {*/

        //$('#apptLetter').addClass('hide');
        //$('#DoucuentTableID').addClass('hide');
        //$('#salarycertificate').addClass('hide');
        //$('#transferletter').addClass('hide');
        //$('#Experience_Letter').removeClass('hide');
        //$('#Revision_Letter').addClass('hide');
        //$('#Increment_List').addClass('hide');
        //$('#Promotion_Letter').addClass('hide');
        /*  $('#JoiningReportTable').html('');*/
        var dataTable = $("#companyDocumentsTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EmployeeCompanyDocuments/GetExperienceLetterList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    console.log(d.data);
                    $('#GetExperienceLetterList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        //d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        //if (TypeCode == 233 || TypeCode == 234 || TypeCode ==235)

                        //    d.data[i].employeeName = '<div><a onclick="ViewCheckBoxSave(' + d.data[i].employeeCode + ',' + "'" + d.data[i].groupCompanyCode + "'" + ',' + FormatCode + ',' + "'" + d.data[i].historyCode + "'" + ',' + d.data[i].preEnrollCode + ',' + "'" + d.data[i].employeeName + "'" +',' + "'" + d.data[i].employeeID+"'"+')">' + d.data[i].employeeName + ' </a ></div></div>'
                        //else
                        d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ',' + "'" + d.data[i].employeeGI + "'"  + ',' + d.data[i].groupCompanyCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "20%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "location", "width": "11%" },
                { "data": "doj", "width": "9%" },
                { "data": "downloadBtn", "width": "9%" }

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

            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable.search('').draw();
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

        //$.fn.DataTable.ext.pager.numbers_length = 5;

    }
    //}

    else if (FormatCode == 6) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();

        if (fromDate != '' && toDate != '') {

            //$('#apptLetter').addClass('hide');
            //$('#DoucuentTableID').addClass('hide');
            //$('#salarycertificate').addClass('hide');
            //$('#transferletter').addClass('hide');
            //$('#Experience_Letter').addClass('hide');
            //$('#Revision_Letter').addClass('hide');
            //$('#Promotion_Letter').removeClass('hide');

            //$('#Increment_List').addClass('hide');

            /*  $('#JoiningReportTable').html('');*/
            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetRelievingLetterList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetPromotionLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 8) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetTransferList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetPromotionLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 9) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();

        if (fromDate != '' && toDate != '') {

            //$('#apptLetter').addClass('hide');
            //$('#transferletter').addClass('hide');
            //$('#DoucuentTableID').addClass('hide');
            //$('#salarycertificate').addClass('hide');
            //$('#Revision_Letter').removeClass('hide');
            //$('#Experience_Letter').addClass('hide');
            //$('#Increment_List').addClass('hide');
            //$('#Promotion_Letter').addClass('hide');
            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetFTCRevisionList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();
                            debugger;
                            // temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + DocTypeCode + '&dummy=dummy';
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';

                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetFTCRevisionLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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



        }
    }

    else if (FormatCode == 10) {
        debugger;
        $('#From_Period').addClass('hide');
        $('#To_Period').addClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var FromDate = $('#From').val();
        var ToDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();

        /* if (FromDate != '' && ToDate != '') {*/



        var dataTable = $("#companyDocumentsTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EmployeeCompanyDocuments/GetSalaryLetterList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();
                        debugger;
                        // temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + DocTypeCode + '&dummy=dummy';
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&FromDate=' + FromDate + '&ToDate=' + ToDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';

                        d.filter = temp;
                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    console.log(d.data);
                    $('#EmployeeSalaryList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].employeeName = '<div><a onclick="showReasonBoxAndSave(' + d.data[i].employeeCode + ',' + "'" + d.data[i].groupCompanyGI + "'" + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "20%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "location", "width": "11%" },
                { "data": "doj", "width": "9%" },
                { "data": "downloadBtn", "width": "9%" }

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

            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable.search('').draw();
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



    }
    /* }*/
    else if (FormatCode == 11) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetConfirmationList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetConfirmationLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 13) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            //$('#apptLetter').addClass('hide');
            //$('#DoucuentTableID').addClass('hide');
            //$('#salarycertificate').addClass('hide');
            //$('#transferletter').addClass('hide');
            //$('#Experience_Letter').removeClass('hide');
            //$('#Revision_Letter').addClass('hide');
            //$('#Increment_List').addClass('hide');
            //$('#Promotion_Letter').addClass('hide');
            /*  $('#JoiningReportTable').html('');*/
            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetEmploymentCertificateList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetEmploymentCertificateList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 14) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            //$('#apptLetter').addClass('hide');
            //$('#DoucuentTableID').addClass('hide');
            //$('#salarycertificate').addClass('hide');
            //$('#transferletter').addClass('hide');
            //$('#Experience_Letter').removeClass('hide');
            //$('#Revision_Letter').addClass('hide');
            //$('#Increment_List').addClass('hide');
            //$('#Promotion_Letter').addClass('hide');
            /*  $('#JoiningReportTable').html('');*/
            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetWarningLettertList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#EmployeeCompanyDocumentsList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            if (TypeCode == 198)   //EmpCode, GcCode, FormatCode, HistoryCode, PreEnrollCode
                                d.data[i].employeeName = '<div><a onclick="showDatePicker(' + d.data[i].employeeCode + ', ' + d.data[i].companyCode + ', ' + FormatCode + ', ' + d.data[i].historyCode + ',' + d.data[i].preEnrollCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'
                            else
                                d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 12) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetTerminationLetterList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#TerminationList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetTerminationLetterPDF(' + d.data[i].employeeCode + ',  ' + d.data[i].companyCode + ', ' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 15) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetShowCauseNoticeList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#EmployeeCompanyDocumentsList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetShowCausePDF(' + d.data[i].employeeCode + ', ' + d.data[i].companyCode + ', ' + FormatCode + ', ' + TypeCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 16) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetDeploymentList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&DocumentFormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#GetPromotionLetterList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 17) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetNoDueEmpList",
                    "type": "POST",
                    "datatype": "json",
                    'data':

                        function (d) {
                            temp = '';

                            var start = d.start;

                            var searchValue = $('#dataTableSearch').val();

                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&FormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        console.log(d.data);
                        $('#EmployeeCompanyDocumentsList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {

                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                        }

                        return d.data;
                    }

                },

                "columns": [

                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }

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

                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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

            //$.fn.DataTable.ext.pager.numbers_length = 5;

        }
    }

    else if (FormatCode == 19) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetAbscondingLetterList",
                    "type": "POST",
                    "datatype": "json",
                    'data':
                        function (d) {
                            temp = '';
                            var start = d.start;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&FormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {
                        console.log(d.data);
                        $('#EmployeeCompanyDocumentsList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="showDateAndSave(' + d.data[i].employeeCode + ',' + "'" + d.data[i].groupCompanyGI + "'" + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'

                        }
                        return d.data;
                    }
                },
                "columns": [
                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }
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
                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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
            //$.fn.DataTable.ext.pager.numbers_length = 5;
        }
    }




    else if (FormatCode == 20) {
        debugger;
        $('#From_Period').removeClass('hide');
        $('#To_Period').removeClass('hide');
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        if (fromDate != '' && toDate != '') {

            var dataTable = $("#companyDocumentsTable").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/EmployeeCompanyDocuments/GetProbationExtensionList",
                    "type": "POST",
                    "datatype": "json",
                    'data':
                        function (d) {
                            temp = '';
                            var start = d.start;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&FormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                            d.filter = temp;
                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {
                        console.log(d.data);
                        $('#EmployeeCompanyDocumentsList_Count').val(d.recordsTotal);
                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', ' + d.data[i].historyCode + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'

                        }
                        return d.data;
                    }
                },
                "columns": [
                    { "data": "employeeID", "width": "20%" },
                    { "data": "employeeName", "width": "20%" },
                    { "data": "designation", "width": "15%" },
                    { "data": "department", "width": "15%" },
                    { "data": "location", "width": "11%" },
                    { "data": "doj", "width": "9%" },
                    { "data": "downloadBtn", "width": "9%" }
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
                $('#dataTableSearch').val('');
                $('#dataTableSearch').focus();
                dataTable.search('').draw();
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
            //$.fn.DataTable.ext.pager.numbers_length = 5;
        }
    }

    else if (FormatCode == 1) {
        $('#From_Period').addClass('hide');
        $('#To_Period').addClass('hide');
        var filter1 = $('#BasicFilter').serialize();
        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        debugger;
        var temp = filter1;





        var dataTable = $("#companyDocumentsTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/EmployeeCompanyDocuments/GetOfferLetterList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';
                        debugger;
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocTypeCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    console.log(d.data);
                    $('#EmployeeCompanyDocumentsList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].employeeName = '<div><a onclick="GetReportPDF(' + d.data[i].employeeCode + ', ' + d.data[i].preEnrollCode + ', ' + d.data[i].companyCode + ', '+ d.data[i].historyCode + ',' + FormatCode + ',' + "'" + d.data[i].employeeGI + "'" + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "20%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "location", "width": "11%" },
                { "data": "doj", "width": "9%" },
                { "data": "downloadBtn", "width": "9%" }

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

            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable.search('').draw();
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

    else if (FormatCode == 18) {
        debugger;
        $('#From_Period').addClass('hide');
        $('#To_Period').addClass('hide');
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;
        var fromDate = $('#From').val();
        var toDate = $('#To').val();

        //$('input[id="Type"]:checked').val('ACTIVE');
        //var Type = $('input[id="Type"]:checked').val();
        var FormatType = $('input[id="Type"]:checked').val();
        var Type = FormatType == 'ACTIVE' ? 'ACTIVE' : $('input[id="Type"]:checked').val();
        /*if (fromDate != '' && toDate != '') {*/

        var dataTable = $("#companyDocumentsTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EmployeeCompanyDocuments/GetStillWorkingLetterList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&DocTypeCode=' + FormatCode + '&FormatCode=' + TypeCode + '&Type=' + Type + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    console.log(d.data);
                    $('#GetStillWorkingLetterList_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].employeeName = '<div><a onclick="showReasonAndSave(' + d.data[i].employeeCode + ',' + "'" + d.data[i].groupCompanyGI + "'" + ',' + FormatCode + ')">' + d.data[i].employeeName + ' </a ></div></div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "20%" },
                { "data": "employeeName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "department", "width": "15%" },
                { "data": "location", "width": "11%" },
                { "data": "doj", "width": "9%" },
                { "data": "downloadBtn", "width": "9%" }

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

            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable.search('').draw();
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

        //$.fn.DataTable.ext.pager.numbers_length = 5;

    }
    /*}*/
    if (FormatCode == 2) {
        $('.companyDocumentsTableDiv').removeClass('Employe_Company_Document_All')
        $('.companyDocumentsTableDiv').addClass('Employe_Company_Document_list')
        let column6 = dataTable.column(6);
        column6.visible(true);

    }
    else {
        $('.companyDocumentsTableDiv').removeClass('Employe_Company_Document_list')
        $('.companyDocumentsTableDiv').addClass('Employe_Company_Document_All')

        let column6 = dataTable.column(6);
        column6.visible(false);
    }

}



function showCheckBoxSave(EmpCode, GcCode, FormatCode, HistoryCode, PreEnrollCode) {

    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';
    swal.fire({
        title: title,


        //text: "Reason to unlock",
        inputLabel: 'Enter Reason',
        html: `
            <label for="confirmationCheckbox" style="display: flex; align-items: center;">
                <input type="checkbox" id="confirmationCheckbox" style="margin-right: 8px;">
                The employee resides in a hostel.
            </label>
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Reason for Salary Certificate is mandatory'
                )
            }
        }

    }).then((result) => {
        debugger;
        if (result.value) {
            debugger
            var result = $('#confirmationCheckbox').is(':checked')



            this.getAppointmentLetterPDF(EmpCode, result, TypeCode, HistoryCode, GcCode, PreEnrollCode);
            //var url = "/DataBank/CompanyLettersDetails/GetAppointmentLetterPDF?PreEnrollCode=" + PreEnrollCode + "&LetterFormatCode=" + docType + "&EmpCode=" + EmployeeCode + "&GcCode=" + GcCode;


        }

    });


}
//function ViewCheckBoxSave(
//    EmpCode,
//    GcCode,
//    FormatCode,
//    HistoryCode,
//    PreEnrollCode,
//    employeeName,
//    employeeID
//) {
//    debugger;

//    var TypeCode = $('#DocumentType_Code').val();

//    Swal.fire({
//        title: 'Employee Details',
//        html: `
//            <input 
//                id="empId" 
//                class="swal2-input" 
//                value="${employeeID}" 
//                placeholder="Employee ID"
//                readonly
//            >

//            <input 
//                id="empName" 
//                class="swal2-input" 
//                value="${employeeName}" 
//                placeholder="Employee Name"
//                readonly
//            >

//            <input 
//                id="fromDate" 
//                type="date" 
//                class="swal2-input"
//            >
//        `,
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Ok',

//        preConfirm: () => {
//            const fromDate = document.getElementById('fromDate').value;

//            if (!fromDate) {
//                Swal.showValidationMessage('Please select a date');
//                return false;
//            }

//            return {
//                empId: employeeID,
//                empName: employeeName,
//                fromDate: fromDate
//            };
//        }
//    }).then((result) => {
//        debugger;
//        if (result.isConfirmed) {
//            // values available here
//            console.log(result.value.empId);
//            console.log(result.value.empName);
//            console.log(result.value.fromDate);

//            this.GetExperienceLetter(
//                EmpCode,
//                result.value,
//                TypeCode,
//                HistoryCode,
//                GcCode,
//                PreEnrollCode
//            );
//        }
//    });
//}


function ViewCheckBoxSave(
    EmpCode,
    GcCode,
    FormatCode,
    HistoryCode,
    PreEnrollCode,
    employeeName,
    employeeID,
    GroupCompanyCode
) {
    debugger;

    var TypeCode = $('#DocumentType_Code').val();

    Swal.fire({
        title: 'Employee Details',
        html: `
            <input 
                id="empId" 
                class="swal2-input" 
                value="${employeeID}" 
                placeholder="Employee ID"
                readonly
            >

            <input 
                id="empName" 
                class="swal2-input" 
                value="${employeeName}" 
                placeholder="Employee Name"
                readonly
            >

            <input 
                id="fromDate" 
                type="date" 
                class="swal2-input"
            >
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: () => {
            const fromDate = document.getElementById('fromDate').value;

            if (!fromDate) {
                Swal.showValidationMessage('Please select a date');
                return false;
            }

            return {
                fromDate: fromDate
            };
        }
    }).then((result) => {
        if (result.isConfirmed) {
            debugger;

            // redirect to controller
            var url = "/DataBank/CompanyLettersDetails/GetExperienceLetter"
                + "?EmpCode=" + EmpCode
                + "&TypeCode=" + TypeCode
                + "&GcCode=" + GcCode
                + "&LetterFormatCode=" + FormatCode
                + "&From=" + result.value.fromDate
                + "&To=" + result.value.fromDate
                + "&GroupCompanyCode=" + GroupCompanyCode;

            window.location.href = url;
        }
    });
}









function showDatePicker(EmpCode, GcCode, FormatCode, HistoryCode, PreEnrollCode) {
    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';

    swal.fire({
        title: title,
        html: `
            <label for="datePickerInput">Select Date Of Action:</label>
            <input type="text" id="datePickerInput" class="form-control kt_datepicker_2" readonly placeholder="Date Of Action">
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',
        didOpen: () => {
            // Initialize the datepicker after the modal opens
            $('.kt_datepicker_2').datepicker({
                format: "dd-M-yyyy",
                autoclose: true
            });
        },
        preConfirm: () => {
            var selectedDate = $('#datePickerInput').val();
            if (!selectedDate) {
                Swal.showValidationMessage('Date selection is mandatory');
            }
            return selectedDate; // Return the selected date
        }
    }).then((result) => {
        debugger;
        if (result.isConfirmed && result.value) {
            var selectedDate = result.value;
            this.getWarningLetterPDF(EmpCode, selectedDate, TypeCode, HistoryCode, GcCode, PreEnrollCode, FormatCode);
        }
    });
}








function showPopUpSave(EmpCode, groupCompanyGI, FormatCode, HistoryCode) {

    debugger;

    var TypeCode = $('#DocumentType_Code').val();
    var title = '';
    swal.fire({
        title: title,
        //text: "Reason to unlock",
        inputLabel: 'Enter The promotion type',
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Promotion type is mandatory'
                )
            }
        }

    }).then((result) => {
        debugger;
        if (result.value) {
            console.log("Result: " + result.value);
            this.getPromotionLetterPDF(EmpCode, groupCompanyGI, result.value, FormatCode, TypeCode, HistoryCode);
        }

    });

}
function showReasonBoxAndSave(EmpCode, groupCompanyGI, FormatCode) {
    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';
    swal.fire({
        title: title,
        //text: "Reason to unlock",
        inputLabel: 'Enter Reason',
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Reason for Salary Certificate is mandatory'
                )
            }
        }

    }).then((result) => {
        debugger;
        if (result.value) {
            console.log("Result: " + result.value);
            this.getSalaryCertificatePDF(EmpCode, groupCompanyGI, result.value, FormatCode, TypeCode);
        }

    });


}

function showReasonAndSave(EmpCode, groupCompanyGI, FormatCode) {
    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';
    swal.fire({
        title: title,
        //text: "Reason to unlock",
        inputLabel: 'Enter Reason',
        input: 'text',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',

        preConfirm: (value) => {
            if (!value) {
                Swal.showValidationMessage(
                    'Reason for Still Working Letter is mandatory'
                )
            }
        }

    }).then((result) => {
        debugger;
        if (result.value) {
            console.log("Result: " + result.value);
            this.getStillWorkingLetterPDF(EmpCode, groupCompanyGI, result.value, FormatCode, TypeCode);
        }

    });


}


function showDateAndSave(EmpCode, groupCompanyGI, FormatCode) {
    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';

    swal.fire({
        title: title,
        html: `
            <label for="dateInput">Enter Date:</label>
            <input type="text" id="dateInput" class="swal2-input" placeholder="Enter Date">
            <label for="remarksInput">Enter Remarks:</label>
            <input type="text" id="remarksInput" class="swal2-input" placeholder="Enter Remarks">
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',
        preConfirm: () => {
            const dateValue = document.getElementById('dateInput').value;
            const remarksValue = document.getElementById('remarksInput').value;

            if (!dateValue) {
                Swal.showValidationMessage('Date for Absconding Letter is mandatory');
                return false;
            }

            if (!remarksValue) {
                Swal.showValidationMessage('Remarks is mandatory');
                return false;
            }

            return { date: dateValue, remarks: remarksValue };
        }
    }).then((result) => {
        debugger;
        if (result.isConfirmed) {
            console.log("Date: " + result.value.date);
            console.log("Remarks: " + result.value.remarks);
            this.getAbscondingLetterPDF(EmpCode, groupCompanyGI, result.value.date, FormatCode, TypeCode, result.value.remarks);
        }
    });
}

function GetTerminationLetterPDF(EmpCode, groupCompanyGI, FormatCode) {
    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';

    swal.fire({
        title: title,
        html: `
            <label for="offerletternum">Enter Offer Letter Number:</label>
            <input type="text" id="offerletternum" class="swal2-input" placeholder="Enter OfferLetter Number">
            <label for="offerletterdate">Enter Offer Letter Initiated Date:</label>
            <input type="text" id="offerletterdate" class="swal2-input" placeholder="Enter Offer Letter Initiated Date">
            <label for="dateInput">Enter Date of Absconding:</label>
            <input type="text" id="dateInput" class="swal2-input" placeholder="Enter Offer Date of Absconding">
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',
        preConfirm: () => {
            const offerletternum = document.getElementById('offerletternum').value;
            const offerletterdate = document.getElementById('offerletterdate').value;
            const dateInput = document.getElementById('dateInput').value;

            if (!dateInput) {
                Swal.showValidationMessage('Date for Absconding Letter is mandatory');
                return false;
            }

            if (!offerletterdate) {
                Swal.showValidationMessage('Date is mandatory');
                return false;
            }

            if (!offerletternum) {
                Swal.showValidationMessage('OfferLetter Number is mandatory');
                return false;
            }

            return { offerletterNum: offerletternum, offerletterdate: offerletterdate, Date: dateInput };
        }
    }).then((result) => {
        debugger;
        if (result.isConfirmed) {
            console.log("offerletterNum: " + result.value.offerletterNum);
            console.log("offerletterdate: " + result.value.offerletterdate);
            console.log("Date: " + result.value.Date);

            this.getTerminationLetter(EmpCode, groupCompanyGI, FormatCode, TypeCode, result.value.offerletterNum, result.value.offerletterdate, result.value.Date);
        }
    });
}

function GetShowCausePDF(EmpCode, GcCode, FormatCode, TypeCode) {
    debugger;
    var TypeCode = $('#DocumentType_Code').val();
    var title = '';

    swal.fire({
        title: title,
        html: `
            <label for="dateInput">Enter Date:</label>
            <input type="text" id="dateInput" class="swal2-input" placeholder="Enter Date">
             <label for="ActiondateInput">Enter Action Date:</label>
            <input type="text" id="ActiondateInput" class="swal2-input" placeholder="Enter Date">
        `,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok',
        preConfirm: () => {
            const dateValue = document.getElementById('dateInput').value;
            const ActiondateValue = document.getElementById('ActiondateInput').value;

            if (!dateValue) {
                Swal.showValidationMessage('Date for Absconding Letter is mandatory');
                return false;
            }



            return { date: dateValue, Actiondate: ActiondateValue };
        }
    }).then((result) => {
        debugger;
        if (result.isConfirmed) {
            console.log("Date: " + result.value.date);
            this.getShowCauseLetterPDF(EmpCode, GcCode, FormatCode, TypeCode, result.value.date, result.value.Actiondate);
        }
    });
}

//var url = "/DataBank/CompanyLettersDetails/GetPromotionLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
function getPromotionLetterPDF(EmployeeCode, groupCompanyGI, LetterType, FormatCode, TypeCode, HistoryCode) {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();

    var url = "/DataBank/CompanyLettersDetails/GetPromotionLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&PromotionType=" + LetterType;
    //var url = "/DataBank/CompanyLettersDetails/GetSalaryLetterReport?EmpCode=" + EmployeeCode + "&GroupCompanyGI=" + groupCompanyGI + "&Reason=" + Reason + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode;
    window.location.href = url;
}

function getWarningLetterPDF(EmployeeCode, selectedDate, FormatCode, HistoryCode, GcCode, PreEnrollCode,TypeCode) {
    debugger;

 
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();

    var url = "/DataBank/CompanyLettersDetails/GetWarningLetter?EmpCode=" + EmployeeCode + "&LetterFormatCode=" + FormatCode + "&GcCode=" + GcCode + "&TypeCode=" + TypeCode  + "&ActionDate=" + selectedDate;


    //var url = "/DataBank/CompanyLettersDetails/GetSalaryLetterReport?EmpCode=" + EmployeeCode + "&GroupCompanyGI=" + groupCompanyGI + "&Reason=" + Reason + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode;
    window.location.href = url;
}

function getAppointmentLetterPDF(EmployeeCode, Flag, FormatCode, HistoryCode, GcCode, PreEnrollCode) {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();

    var url = "/DataBank/CompanyLettersDetails/GetAppointmentLetterPDF?PreEnrollCode=" + PreEnrollCode + "&LetterFormatCode=" + FormatCode + "&EmpCode=" + EmployeeCode + "&GcCode=" + GcCode + "&Flag=" + Flag;


    //var url = "/DataBank/CompanyLettersDetails/GetSalaryLetterReport?EmpCode=" + EmployeeCode + "&GroupCompanyGI=" + groupCompanyGI + "&Reason=" + Reason + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode;
    window.location.href = url;
}


function getSalaryCertificatePDF(EmployeeCode, groupCompanyGI, Reason, FormatCode, TypeCode) {
    debugger;
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();
    var url = "/DataBank/CompanyLettersDetails/GetSalaryLetterReport?EmpCode=" + EmployeeCode + "&GroupCompanyGI=" + groupCompanyGI + "&Reason=" + Reason + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode;
    window.location.href = url;
}
function getStillWorkingLetterPDF(EmployeeCode, groupCompanyGI, Reason, FormatCode, TypeCode) {
    debugger;
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();
    var url = "/DataBank/CompanyLettersDetails/GetStillWorkingLetter?EmpCode=" + EmployeeCode + "&GroupCompanyGI=" + groupCompanyGI + "&Reason=" + Reason + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode;
    window.location.href = url;
}

function getAbscondingLetterPDF(EmployeeCode, groupCompanyGI, Date, FormatCode, TypeCode, Remarks) {
    debugger;
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();
    var url = "/DataBank/CompanyLettersDetails/GetAbscondingLetter?EmpCode=" + EmployeeCode + "&GroupCompanyGI=" + groupCompanyGI + "&Date=" + Date + "&Remarks=" + Remarks + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode;
    window.location.href = url;
}

function getTerminationLetter(EmpCode, GcCode, FormatCode, TypeCode, offerletterNum, offerletterdate, Date) {
    debugger;
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();
    var url = "/DataBank/CompanyLettersDetails/GetTerminationLetter?EmpCode=" + EmpCode + "&GcCode=" + GcCode + "&DocTypeCode=" + FormatCode + "&LetterFormatCode=" + TypeCode + "&offerletterNum=" + offerletterNum + "&offerletterdate=" + offerletterdate + "&Date=" + Date;
    window.location.href = url;
}

function getShowCauseLetterPDF(EmployeeCode, GcCode, FormatCode, TypeCode, Date, Actiondate) {
    debugger;
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();
    var url = "/DataBank/CompanyLettersDetails/GetShowCauseNotice?EmpCode=" + EmployeeCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + TypeCode + "&DocTypeCode=" + FormatCode + "&Date=" + Date + "&ActionDate=" + Actiondate;
    window.location.href = url;
}

function GetReportPDF(EmployeeCode, PreEnrollCode, GcCode, HistoryCode, FormatCode, EmployeeGI, GroupCompanyCode) {
    debugger;
    var docVal = $("#DocumentType_Code option:selected").text();
    var docType = $("#DocumentType_Code").val();
    var fromDate = $('#From').val();
    var toDate = $('#To').val();

    if (FormatCode == 2) {

        var url = "/DataBank/CompanyLettersDetails/GetAppointmentLetterPDF?PreEnrollCode=" + PreEnrollCode + "&LetterFormatCode=" + docType + "&EmpCode=" + EmployeeCode + "&GcCode=" + GcCode;
        window.location.href = url;
    }
    else if (FormatCode == 3) {

        var url = "/DataBank/CompanyLettersDetails/GetIncrementLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;

        window.location.href = url;
    }

    else if (FormatCode == 4) {

        var url = "/DataBank/CompanyLettersDetails/GetPromotionLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }

    else if (FormatCode == 5) {

        var url = "/DataBank/CompanyLettersDetails/GetExperienceLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate + "&GroupCompanyCode=" + GroupCompanyCode;
        window.location.href = url;
    }

    else if (FormatCode == 6) {

        var url = "/DataBank/CompanyLettersDetails/GetRelievingLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }
    else if (FormatCode == 8) {
        debugger;
        var url = "/DataBank/CompanyLettersDetails/GetTransferLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;

        window.location.href = url;
    }
    else if (FormatCode == 1) {

        var url = "/DataBank/CompanyLettersDetails/GetOfferLetterPDF?PreEnrollCode=" + PreEnrollCode + "&EmpGI=" + EmployeeGI + "&EmpCode=" + EmployeeCode + "&LetterFormatCode=" + docType;

        window.location.href = url;
    }



    else if (FormatCode == 9) {

        var url = "/DataBank/CompanyLettersDetails/GetRevisionLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }

    else if (FormatCode == 11) {

        var url = "/DataBank/CompanyLettersDetails/GetConfirmationLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;
        window.location.href = url;
    }
    else if (FormatCode == 12) {

        var url = "/DataBank/CompanyLettersDetails/GetTerminationLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;
        window.location.href = url;
    }

    else if (FormatCode == 13) {

        var url = "/DataBank/CompanyLettersDetails/GetEmploymentCertificate?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }
    else if (FormatCode == 14) {
        debugger;
        var url = "/DataBank/CompanyLettersDetails/GetWarningLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;

        window.location.href = url;
    }
    else if (FormatCode == 15) {
        debugger;
        var url = "/DataBank/CompanyLettersDetails/GetShowCauseNotice?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;

        window.location.href = url;
    }
    else if (FormatCode == 16) {
        debugger;
        var url = "/DataBank/CompanyLettersDetails/GetDeploymentLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&HistoryCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;

        window.location.href = url;
    }
    else if (FormatCode == 17) {
        debugger;
        var url = "/DataBank/CompanyLettersDetails/GetNoDueLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&LetterFormatCode=" + docType + "&GcCode=" + GcCode;

        window.location.href = url;
    }
    else if (FormatCode == 18) {

        var url = "/DataBank/CompanyLettersDetails/GetStillWorkingLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }
    else if (FormatCode == 19) {

        var url = "/DataBank/CompanyLettersDetails/GetAbscondingLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&GcCode=" + GcCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }
    else if (FormatCode == 20) {
        debugger
        var url = "/DataBank/CompanyLettersDetails/GetProbationExtensionLetter?EmpCode=" + EmployeeCode + "&TypeCode=" + FormatCode + "&GcCode=" + GcCode + "&HisCode=" + HistoryCode + "&LetterFormatCode=" + docType + "&From=" + fromDate + "&To=" + toDate;
        window.location.href = url;
    }
}



function ApplyFilter(mode) {
    debugger

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