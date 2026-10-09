
var prevTab = 'Verification';
var currentTab = 'APPROVAL PENDING';

function OnTabClick(Tab, TabIndex) {
    
    var radiomode = $("input[type='radio']:checked").val();
    showSpinner();
    currentTab = TabIndex;

    
    $(".btn-title").removeClass("active");


    if (window.event && window.event.currentTarget) {
        $(window.event.currentTarget).addClass("active");
    } else {
      
        $(".btn-title").filter(function () {
            return $(this).text().trim() === (Tab === 'OfferSend' ? 'Offer Send' : Tab === 'Verified' ? 'Offer Verified' : 'Offer Verification');
        }).addClass("active");
    }

  
    if (TabIndex == 'APPROVED' || TabIndex == 'APPROVEDFINAL' || TabIndex == 'APPROVAL PENDING') {
        $("#radio").addClass("hide").hide(); // Hides the radio buttons
    } else if (TabIndex == 'CANDIDATELINK') {
        $("#radio").removeClass("hide").show(); 
    }

    
    var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'GET',
        url: '/DataBank/Offer/GetList',
        dataType: 'html',
        data: {
            TabIndex: currentTab,
            radiomode: radiomode,
            filter: filter
        },
        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            } else {
                $('#offerverification').html(result);
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            hideSpinner();
        }
    });

    prevTab = Tab;
}


function fillData() {


    if (currentTab == 'APPROVED') {


        debugger;
        var filter1 = $('#BasicFilter').serialize();

        var doc = 1;


        var temp = filter1;

        /*  $('#JoiningReportTable').html('');*/
        var dataTables = $("#offerLettersendList_Table").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/Offer/GetOfferLetterList",
                "type": "POST",


                "datatype": "json",

                
                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;
                        debugger;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocType=' + doc + '&Tabindex=' + currentTab+ '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#OfferLetterList_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {



                        // d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Send Mail" + '</a>'
                        /*d.data[i].absoluteUri = '<a onclick="showDocument(' + "'" + d.data[i].fileName + "'" +','+ "'" + d.data[i].absoluteUri + "'" + ')">' + "View" + '</a>'*/
                        d.data[i].absoluteUri = '<a onclick="showUploadDocument(' + "'" + d.data[i].employeeGI + "'" + ')">' + "View" + '</a>'
                        d.data[i].dob = '<a onclick="SendMail(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'"+')">' + "Send Mail" + '</a>'
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "16%" },
                { "data": "applicantID", "width": "16%" },
                { "data": "designation", "width": "10%" },
                { "data": "department", "width": "10%" },
                { "data": "location", "width": "10%" },
                { "data": "doj", "width": "8%" },
                { "data": "absoluteUri", "width": "10%" },
                { "data": "dob", "width": "10%" },



            ],

            /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/



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
    else if (currentTab == 'CANDIDATELINK') {
  
            debugger;
            var filter1 = $('#BasicFilter').serialize();

            var doc = 1;


            var temp = filter1;

            /*  $('#JoiningReportTable').html('');*/
            var dataTables = $("#offerLetterList_Table").DataTable({

                "pagingType": 'full_numbers',
                "destroy": 'true',
                "ajax": {
                    "url": "/DataBank/Offer/GetOfferLetterList",
                    "type": "POST",


                    "datatype": "json",


                    'data':

                        function (d) {
                            temp = '';
                            var start = d.start;
                            debugger;
                            var searchValue = $('#dataTableSearch').val();
                            temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocType=' + doc + '&Tabindex=' + currentTab + '&dummy=dummy';
                            d.filter = temp;

                            return JSON.stringify(d);
                        },

                    "dataSrc": function (d) {

                        $('#OfferLetterList_Count').val(d.recordsTotal);


                        for (var i = 0, ien = d.data.length; i < ien; i++) {
                            //d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Verify" + '</a>'
                            d.data[i].upload = '<a onclick="uploadOffer(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + d.data[i].letterFormatCode + ')">' + "Upload" + '</a>'



                            //d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Verify" + '</a>'
                            d.data[i].dob = '<a onclick="getData(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + d.data[i].letterFormatCode + ')">' + "Verify" + '</a>'


                            d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                            //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


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
                    { "data": "upload", "width": "10%" },
                    { "data": "dob", "width": "10%" },



                ],

                /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/



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
   

    else if (currentTab == 'APPROVEDFINAL') {

        debugger;
        var filter1 = $('#BasicFilter').serialize();

        var doc = 1;


        var temp = filter1;

        /*  $('#JoiningReportTable').html('');*/
        var dataTables = $("#offerLetterVerifiedList_Table").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/Offer/GetOfferLetterList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;
                        debugger;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocType=' + doc + '&Tabindex=' + currentTab + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },

                "dataSrc": function (d) {

                    $('#OfferLetterList_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        //d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Verify" + '</a>'
                        d.data[i].absoluteUri = '<a onclick="getDataVerified(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + d.data[i].letterFormatCode + ')">' + "View" + '</a>'


                        
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID" },
                { "data": "employeeName" },
                { "data": "applicantID"},
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "doj"},
                { "data": "approvedStatus"  },
                { "data": "absoluteUri"}



            ],

            /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/



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




    else {

    

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var doc = 1;


    var temp = filter1;

    /*  $('#JoiningReportTable').html('');*/
    var dataTables = $("#offerLetterList_Table").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/Offer/GetOfferLetterList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;
                    debugger;
                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&DocType=' + doc + '&Tabindex=' + currentTab + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {

                $('#OfferLetterList_Count').val(d.recordsTotal);


                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    //d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Verify" + '</a>'
                    d.data[i].upload = '<a onclick="uploadOffer(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + d.data[i].letterFormatCode + ')">' + "Upload" + '</a>'


                    //d.data[i].dob = '<a href="/DataBank/offer/GetOfferLetterPDF?PreEnrollCode=' + d.data[i].preEnrollCode + '">' + "Verify" + '</a>'
                    d.data[i].dob = '<a onclick="getData(' + "'" + d.data[i].preEnrollCode + "'" + ',' + "'" + d.data[i].employeeGI + "'" + ',' + "'" + d.data[i].employeeCode + "'" + ',' + d.data[i].letterFormatCode + ')">' + "Verify" + '</a>'


                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "16%" },
            { "data": "applicantID", "width": "16%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "10%" },
            { "data": "doj", "width": "8%" },
            { "data": "upload", "width": "10%" },
            { "data": "dob", "width": "10%" },



        ],

        /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/



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
}

function getData(PreEnrollcode, empGI, empCode,LetterFormatCode) {
    debugger
    var url = "/DataBank/offer/GetPDF?PreEnrollCode=" + PreEnrollcode + "&EmpGI=" + empGI + "&EmpCode=" + empCode + "&LetterFormatCode=" + LetterFormatCode;
    window.location.href = url;
    
}


function getDataVerified(PreEnrollcode, empGI, empCode, LetterFormatCode) {
    debugger
    var url = "/DataBank/offer/GetPDFVerified?PreEnrollCode=" + PreEnrollcode + "&EmpGI=" + empGI + "&EmpCode=" + empCode + "&LetterFormatCode=" + LetterFormatCode;
    window.location.href = url;

}

function uploadOffer(PreEnrollcode, empGI, empCode, LetterFormatCode) {
    debugger
    var m = $('.newApplication');

    // Store values in hidden fields
    m.find('#hdnPreEnrollcode').val(PreEnrollcode);
    m.find('#hdnEmpGI').val(empGI);
    m.find('#hdnEmpCode').val(empCode);
    m.find('#hdnLetterFormatCode').val(LetterFormatCode);

    m.find('#dynamic-certification-content').empty();
    m.modal('show');
}

function View(url) {
    debugger;
    window.open(url, '_blank');
}



function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            if (currentTab == "CANDIDATELINK") {
                OnTabClick('PreEnrollLink', currentTab);

            }
            else {
                fillData();
            }
            
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            if (currentTab == "CANDIDATELINK") {
                OnTabClick('PreEnrollLink', currentTab);

            }
            else {
                fillData();
            }
           
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {

        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        if (currentTab == "CANDIDATELINK") {
            OnTabClick('PreEnrollLink', currentTab);

        }
        else {
            fillData();
        }
        
    }
}


function SendMail(PreEnrollCode, PreEnrollGI, EmpCode) {
    debugger;

    swal.fire({
        text: "Do you want to Send Mail?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({

                type: 'POST',
                url: '/DataBank/Offer/SendMail',
                dataType: 'json',
                data: { PreEnrollCode: PreEnrollCode, PreEnrollGI: PreEnrollGI, EmpCode: EmpCode},
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            debugger;
                            var url = "/DataBank/Offer/GetList?TabIndex=" + 'APPROVED';

                            $('#offerverification').load(url, function () { });
                            // window.top.close();



                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            var url = "/DataBank/Offer/GetList?TabIndex=" + 'APPROVED';

                            $('#offerverification').load(url, function () { });
                            //var url = "/Recruitment/OfferProcessing/GertOfferLetter?PreEnrollCode=" + PreEnrollCode + "&PreEnrollGI=" + PreEnrollGI;
                            //window.open(url, "_blank");
                            // window.location.reload();


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
        else {
            return false;
        }
    })
}


//function SendCandidateLink() {//PreEnrollCode, PreEnrollGI
//    debugger;

//    swal.fire({
//        text: "Do you want to Send Mail?",
//        icon: 'warning',
//        showCancelButton: true,
//        confirmButtonColor: '#3085d6',
//        cancelButtonColor: '#d33',
//        confirmButtonText: 'Ok'
//    }).then((result) => {
//        if (result.isConfirmed) {
//            $.ajax({
//                type: 'POST',
//                url: '/DataBank/Offer/SendMail',
//                dataType: 'json',
//                data: { PreEnrollCode: PreEnrollCode, PreEnrollGI: PreEnrollGI, EmpCode: EmpCode },
//                success: function (result) {
//                    debugger;
//                    if (result.success == true) {
//                        debugger
//                        swal.fire(result.message).then(function () {
//                            debugger;
//                            var url = "/DataBank/Offer/GetList?TabIndex=" + 'APPROVED';

//                            $('#offerverification').load(url, function () { });
//                            // window.top.close();



//                        });

//                    }

//                },
//            });
//        }

//    }

// }


function SendCandidateLink() {
    debugger;

    swal.fire({
        text: "Do you want to Send Mail?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            var table = $('#offercandidatelink').DataTable();
            var data = table.$('input,select,textarea').serializeArray()
            $.ajax({

                type: 'POST',
                url: '/DataBank/Offer/SendCandidateMail',
                dataType: 'json',
                data: data,//PreEnrollCode: PreEnrollCode, PreEnrollGI: PreEnrollGI, EmpCode: EmpCode
                success: function (result) {
                    debugger;
                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            debugger;
                            //window.location.reload();
                            //var url = "/DataBank/Offer/GetList?TabIndex=" + 'CANDIDATELINK';
                            OnTabClick('PreEnrollLink', 'CANDIDATELINK')
                            //window.location.href = url;
                            //$('#offerverification').load(url, function () { });
                            // window.top.close();



                        });

                    }

                    else {
                        debugger;
                        swal.fire(result.message).then(function () {
                            //var url = "/DataBank/Offer/GetList?TabIndex=" + 'APPROVED';

                            //$('#offerverification').load(url, function () { });
                            //var url = "/Recruitment/OfferProcessing/GertOfferLetter?PreEnrollCode=" + PreEnrollCode + "&PreEnrollGI=" + PreEnrollGI;
                            //window.open(url, "_blank");
                            // window.location.reload();


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
        else {
            return false;
        }
    })
}

function CandidatelinkRadio() {
    var radiomode = $("input[type='radio']:checked").val();

    showSpinner(); // Good practice to show loader here too
    $.ajax({
        type: 'GET',
        url: '/DataBank/Offer/GetList',
        dataType: 'html',
        // Change: Use currentTab instead of hardcoded "CANDIDATELINK"
        data: { TabIndex: currentTab, radiomode: radiomode },

        success: function (result) {
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#offerverification').html("");
                $('#offerverification').html(result);
            }
            hideSpinner();
        },
        error: function (xhr, textStatus, errorThrown) {
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);
            hideSpinner();
        }
    });
}

function onExportToExcelClick() {
    debugger;
    var radiomode = $("input[type='radio']:checked").val();
    var pendingTableLength = $('#OfferLetterList_Count').val();
    if (pendingTableLength > 0) {
        url = "/DataBank/Offer/ExportToExcel?TabIndex=" + currentTab + " & radiomode=" + radiomode;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function onOfferExportToExcelClick() {
    debugger;
    var doc = 1;
    var Tabname = prevTab;
    var PARAM = 'data=' + $("#BasicFilter").serialize();

    var TableLength = $('#OfferLetterList_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/Offer/OfferExportToExcel?" + PARAM +"&DocType=" + doc + "&TabIndex=" + currentTab + "&Tabname=" + Tabname + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function Preview() {

    debugger;
    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
    if (path == "") {
        swal.fire("No file Uploaded")
    }
    else {
        window.open(path);
    }
}
function showUploadDocument(employeeGI) {

    debugger
    var url = "/DataBank/Offer/ShowDoc?PreEnrollGI=" + employeeGI;
    window.open(url, "_blank");

}
function OnApproveClick(mode) {
    debugger;

    var ReportName = '';
    var PreEnrollCode = $('#hdnPreEnrollcode').val();
    var EmpCode = $('#hdnEmpCode').val();
    var EmpGI = $('#hdnEmpGI').val();

    var fileInput = document.getElementById("FileData");

    if (!fileInput || fileInput.files.length === 0) {
        swal.fire("Please select a file before approving");
        return;
    }

    var file = fileInput.files[0];
    var fileName = file.name;   // ✅ filename

    swal.fire({
        text: "Do you want to Approve?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Ok'
    }).then((result) => {

        if (result.isConfirmed) {

            var formData = new FormData();
            formData.append("FileData", file);
            formData.append("FileName", fileName);   // ✅ pass filename
            formData.append("ReportName", ReportName);
            formData.append("PreEnrollCode", PreEnrollCode);
            formData.append("EmpGI", EmpGI);
            formData.append("EmpCode", EmpCode);
            formData.append("mode", mode);

            $.ajax({
                type: 'POST',
                url: '/DataBank/Offer/OfferApproveAndSave',
                data: formData,
                contentType: false,
                processData: false,
                dataType: 'json',
                success: function (result) {
                    swal.fire(result.message).then(function () {
                        // ✅ CLOSE MODAL
                        var m = $('.newApplication');
                        m.modal('hide');
                        $('#offerverification')
                            .load("/DataBank/Offer/GetList?TabIndex=APPROVAL PENDING");
                    });
                }
            });
        }
    });
}