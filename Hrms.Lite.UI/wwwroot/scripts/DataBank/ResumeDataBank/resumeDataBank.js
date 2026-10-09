/*--contact----*/

var date = new Date();
date.setDate(date.getDate());
var prevTab = 'rdAll';
var currentTab = 1;
var TabName = 'New';
var selectedTab = '';

function onTabClick(TabIndex, spanName, tabname) {
    debugger
    $.ajaxSetup({
        async: false
    });
    currentTab = TabIndex;
    TabName = tabname;
    $("." + prevTab).removeClass("active");
    $("." + spanName).addClass("active");
    //$('#TabCode').val(TabIndex);

    //var searchid = $('#FilterList_Code').val();

    if (TabIndex == 5) {
        $(".rd_fromTo").removeClass('hide');
    }
    else {
        $(".rd_fromTo").addClass('hide');
    }
    if (TabIndex != 1)
        $(".rd_filter").addClass('hide');
    else
        $(".rd_filter").removeClass('hide');
    ApplyFilter('apply');

    prevTab = spanName;
}



function CheckImageExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp'];
    console.log(size);
    if (size > 2048 * 1024) {
        swal.fire('File size cannot exceeds 2 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only jpg, jpeg, png and bmp formats are allowed");
        $(e).val('');
    }

}

function onEmpClick(resumeDataBankGI) {

    var url = '/DataBank/ResumeDataBank/Edit?ResumeDataBankGI=' + resumeDataBankGI;
    window.location.href = url;
}


function fillData() {

    debugger;
    var d = currentTab;

    if (currentTab == 1) {

        debugger;
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var FROM = $("#From").val();
        var TO = $("#To").val();

        var dataTable = $("#ResumeDBTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/ResumeDataBank/GetResumeRepoList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&TabIndex=' + currentTab + '&FROMDATE=' + FROM + '&TODATE=' + TO + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ResumeDataBank_List_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].candidateName = '<a onclick="onEmpClick( ' + "'" + d.data[i].resumeDataBankGI + "'" + ')">' + d.data[i].candidateName + '</a>'
                        d.data[i].resumes = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="Resume Uploaded By Candidate" onclick="previewDocument(' + "'" + d.data[i].resumes + "'" + ')"></i></td>'
                        d.data[i].remarks = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="System Generated Resume" onclick="viewResume(' + "'" + d.data[i].resumeDataBankGI + "'" + ')"></i></td>'

                    }



                    $('.allCount').html(d.all);
                    $('.tab1Count').html(d.countTab1);
                    $('.tab2Count').html(d.countTab2);
                    $('.tab3Count').html(d.countTab3);
                    $('.tab5Count').html(d.countTab5);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                    }
                    return d.data;
                }
            },
            "columns": [

                { "data": "refNo", "width": "20%" },
                { "data": "candidateName", "width": "20%" },
                { "data": "designation", "width": "15%" },
                { "data": "suitedLocation", "width": "15%" },
                { "data": "yos", "width": "15%" },
                { "data": "age", "width": "15%" },
                { "data": "resumes", "width": "15%" },
                { "data": "remarks", "width": "15%" },

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
            // dataTable.search('').draw();
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
    else if (currentTab == 3) {

        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;

        var dataTable = $("#HoldTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/ResumeDataBank/GetResumeRepoList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&TabIndex=' + currentTab + '&FROMDATE=' + FROM + '&TODATE=' + TO + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ResumeDataBank_List_Count').val(d.recordsTotal);
                    $('.allCount').html(d.all);
                    $('.tab1Count').html(d.countTab1);
                    $('.tab2Count').html(d.countTab2);
                    $('.tab3Count').html(d.countTab3);
                    $('.tab5Count').html(d.countTab5);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].resumes = '    <td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="Resume Uploaded By Candidate" onclick="previewDocument(' + "'" + d.data[i].resumes + "'" + ')"></i></td>'
                        d.data[i].remarks = '<td data-label="View"><i class="icon-xl far fa-eye"  data-bs-toggle="tooltip" title="System Generated Resume" onclick="viewResume(' + "'" + d.data[i].resumeDataBankGI + "'" + ')"></i></td>'
                        d.data[i].age = '<td data-label="Restart"><i class="icon-xl la la-arrow-circle-right" onclick="onRestartClick(' + "'" + d.data[i].resumeDataBankGI + "'" + ')"></i></td>'
                    }
                    return d.data;
                }
            },
            "columns": [


                { "data": "refNo", "width": "20%" },
                { "data": "candidateName", "width": "20%" },
                { "data": "mrfNo", "width": "20%" },
                { "data": "jobTitle", "width": "15%" },
                { "data": "heldAt", "width": "15%" },


                { "data": "resumes", "width": "15%" },
                { "data": "remarks", "width": "15%" },
                { "data": "age", "width": "15%" },

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
            // dataTable.search('').draw();
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


    if (currentTab == 2) {

        debugger;
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var FROM = $("#From").val();
        var TO = $("#To").val();

        var dataTable = $("#RecTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/ResumeDataBank/GetResumeRepoList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&TabIndex=' + currentTab + '&FROMDATE=' + FROM + '&TODATE=' + TO + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ResumeDataBank_List_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].resumes = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="Resume Uploaded By Candidate" onclick="previewDocument(' + "'" + d.data[i].resumes + "'" + ')"></i></td>'
                        d.data[i].remarks = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="System Generated Resume" onclick="viewResume(' + "'" + d.data[i].resumeDataBankGI + "'" + ')"></i></td>'

                    }



                    $('.allCount').html(d.all);
                    $('.tab1Count').html(d.countTab1);
                    $('.tab2Count').html(d.countTab2);
                    $('.tab3Count').html(d.countTab3);
                    $('.tab5Count').html(d.countTab5);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                    }
                    return d.data;
                }
            },
            "columns": [

                { "data": "refNo", "width": "20%" },
                { "data": "candidateName", "width": "20%" },
                { "data": "mrfNo", "width": "15%" },
                { "data": "jobTitle", "width": "15%" },
                { "data": "stage", "width": "15%" },

                { "data": "resumes", "width": "15%" },
                { "data": "remarks", "width": "15%" },


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
            // dataTable.search('').draw();
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

    if (currentTab == 4) {

        debugger;
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var FROM = $("#From").val();
        var TO = $("#To").val();

        var dataTable = $("#rejTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/ResumeDataBank/GetResumeRepoList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&TabIndex=' + currentTab + '&FROMDATE=' + FROM + '&TODATE=' + TO + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ResumeDataBank_List_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].resumes = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="Resume Uploaded By Candidate" onclick="previewDocument(' + "'" + d.data[i].resumes + "'" + ')"></i></td>'
                        d.data[i].age = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="System Generated Resume" onclick="viewResume(' + "'" + d.data[i].resumeDataBankGI + "'" + ')"></i></td>'

                    }



                    $('.allCount').html(d.all);
                    $('.tab1Count').html(d.countTab1);
                    $('.tab2Count').html(d.countTab2);
                    $('.tab3Count').html(d.countTab3);
                    $('.tab5Count').html(d.countTab5);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                    }
                    return d.data;
                }
            },
            "columns": [

                { "data": "refNo", "width": "20%" },
                { "data": "candidateName", "width": "20%" },
                { "data": "mrfNo", "width": "15%" },
                { "data": "jobTitle", "width": "15%" },
                { "data": "rejectedAt", "width": "15%" },
                { "data": "rejectReason", "width": "15%" },
                { "data": "remarks", "width": "15%" },
                { "data": "resumes", "width": "15%" },
                { "data": "age", "width": "15%" },



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
            // dataTable.search('').draw();
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
    if (currentTab == 6) {

        debugger;
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var FROM = $("#From").val();
        var TO = $("#To").val();

        var dataTable = $("#closedTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/ResumeDataBank/GetResumeRepoList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&TabIndex=' + currentTab + '&FROMDATE=' + FROM + '&TODATE=' + TO + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#ResumeDataBank_List_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].resumes = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="Resume Uploaded By Candidate" onclick="previewDocument(' + "'" + d.data[i].resumes + "'" + ')"></i></td>'
                        d.data[i].remarks = '<td data-label="View"><i class="icon-xl far fa-eye" data-bs-toggle="tooltip" title="System Generated Resume" onclick="viewResume(' + "'" + d.data[i].resumeDataBankGI + "'" + ')"></i></td>'

                    }



                    $('.allCount').html(d.all);
                    $('.tab1Count').html(d.CountTab1);
                    $('.tab2Count').html(d.countTab2);
                    $('.tab3Count').html(d.countTab3);
                    $('.tab5Count').html(d.countTab5);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                    }
                    return d.data;
                }
            },
            "columns": [

                { "data": "refNo", "width": "20%" },
                { "data": "candidateName", "width": "20%" },
                { "data": "mrfNo", "width": "15%" },
                { "data": "jobTitle", "width": "15%" },
                { "data": "stage", "width": "15%" },
                { "data": "resumes", "width": "15%" },
                { "data": "remarks", "width": "15%" },
                //{ "data": "resumes", "width": "15%" },
                //{ "data": "age", "width": "15%" },



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
            // dataTable.search('').draw();
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


function CheckResumeExtensionSize(e) {
    debugger;
    console.log(e);
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg', 'png', 'bmp', 'pdf'];
    console.log(size);
    if (size > 2048 * 1024) {
        swal.fire('File size cannot exceeds 2 Mb');
        $(e).val('');
    }
    else if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Only pdf, Jpeg, Jpg, Png and bmp files are allowed");
        $(e).val('');
    }

}

function onCountryChange(sender, id) {
    debugger
    var cid = $(sender).val();

    $.getJSON("/DataBank/ResumeDataBank/GetState", { id: cid }, function (data) {

        $('#Details_State_Code option').remove();
        $('#Details_State_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Details_State_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
    //Make Dtsrict Dropdwn Empty when no Country is Selected
    //if (cid == 0) {
    $.getJSON("/DataBank/ResumeDataBank/GetDistrict", { id: 0 }, function (data) {

        $('#Details_District_Code option').remove();
        $('#Details_District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Details_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
    });

}

function onStateChange(sender, id) {
    var cid = $(sender).val();
    debugger;

    $.getJSON("/DataBank/ResumeDataBank/GetDistrict", { id: cid }, function (data) {

        $('#Details_District_Code option').remove();
        $('#Details_District_Code').append('<option value="">--Select--</option');

        for (var i = 0; i < data.length; i++) {
            $('#Details_District_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}



function onSkillTypeChange(sender, index) {
    debugger;
    if (!validateById('#Skill_' + index + '__SkillType_Code', 'skilType_' + index))
        return false;
    var cid = $('#Skill_' + index + '__SkillType_Code').val();

    $.getJSON("/DataBank/ResumeDataBank/GetSkill", { id: cid }, function (data) {

        $('#Skill_' + index + '__Skill_Code option').remove();
        $('#Skill_' + index + '__Skill_Code').append('<option value="">--Select--</option');
        debugger
        for (var i = 0; i < data.length; i++) {
            $('#Skill_' + index + '__Skill_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
}

function onQualifnTypeChange(sender, index) {
    debugger;
    if (validateById('#Education_' + index + '__QualificationType_Code', 'rd_Qualtype_' + index)) {
        var cid = $('#Education_' + index + '__QualificationType_Code').val();

        $.getJSON("/DataBank/ResumeDataBank/GetQualification", { qualTypeCode: cid }, function (data) {
            console.log(data);

            $('#Education_' + index + '__Qualification_Code option').remove();
            $('#Education_' + index + '__Qualification_Code').append('<option value="">--Select--</option');
            debugger
            for (var i = 0; i < data.length; i++) {
                $('#Education_' + index + '__Qualification_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });

    }
}

function onQualifnChange(sender, index) {
    debugger;
    if (validateById('#Education_' + index + '__Qualification_Code', 'rd_Qualificn_' + index)) {
        var cid = $('#Education_' + index + '__Qualification_Code').val();

        $.getJSON("/DataBank/ResumeDataBank/GetSpecialisation", { QualCode: cid }, function (data) {
            console.log(data);
            $('#Education_' + index + '__SpecializationOfBranch_Code option').remove();
            $('#Education_' + index + '__SpecializationOfBranch_Code').append('<option value="">--Select--</option');
            debugger
            for (var i = 0; i < data.length; i++) {
                $('#Education_' + index + '__SpecializationOfBranch_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
            }

        });
    }
}
function showExpDiv(sender) {
    debugger
    var freshorExp = $(sender).val();

    if (freshorExp == 0)
        $(".RdExp").addClass('hide');
    else
        $(".RdExp").removeClass('hide');
}
function onCertificateValidityChange(index) {
    var cid = $("#Certifications_" + index + "__CertificateValidity_Code").val();
    debugger
    if (cid == 1) //lifetime
        $("#reneweldate_" + index).addClass('hide');
    else
        $("#reneweldate_" + index).removeClass('hide');
}
function CalcExperience(val, type, id, index) {
    debugger;
    /*var a = $(val).attr('id');*/
    ///*    var index = parseInt(a.substring(11, 12));*/

    if (!validateById('#Experience_' + index + '__From', 'rd_From_' + index) || !validateById('#Experience_' + index + '__To', 'rd_To_' + index))
        return false;

    else {

        var from = $('#Experience_' + index + '__From').val();
        var to = $('#Experience_' + index + '__To').val();
        date1 = new Date(from);
        date2 = new Date(to);

        const oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

        const firstDate = date2;
        const secondDate = date1;
        const diffDays = Math.round(Math.abs((firstDate - secondDate) / oneDay));

        var diff = parseFloat(diffDays);
        debugger;
        diff = diff / 365;

        var monthWiseDiff = diff / 0.0833344444;

        monthWiseDiff = parseInt(monthWiseDiff);

        abc = monthWiseDiff % 12;
        monthWiseDiff = monthWiseDiff / 12;

        var month = 0.01 * abc;
        var year = parseInt(monthWiseDiff);

        var experience = year + month;
        /*alert(experience);*/
        $('#Experience_' + index + '__Experience').val(experience);
        if (date1 > date2) {
            $(val).addClass('is-invalid');
            $("#" + id + index + "").addClass('error-message');
            if (type == 'to')
                $("#" + id + index + "").html("To date should be greater than From date");
            else
                $("#" + id + index + "").html("From date should be less than To date");
            return false;
        }
        else {
            $(val).removeClass('is-invalid');
            $("#" + id + index + "").removeClass('error-message');
            $("#" + id + index + "").html("");
            return true;
        }
    }
}

function validateContact() {
    var i = 0;
    if ($("#Mode").val() == "CREATE") {
        if ($('#Details_CandidatesPhoto_AbsoluteUri').val() == '') {
            i++;
            swal.fire("Candidate Photo is Required");
            return false
        }
    }
    if (!validateById('#Details_RefNo', 'rd_refnumber')) {
        i++;
    }
    if (!validateById('#Details_FullName', 'rd_fullname')) {
        i++;
    }
    if (!validateById('#Details_Mobile', 'rd_mobile')) {
        i++;
    } if (!validateById('#Details_Email', 'rd_Email')) {
        i++;
    }
    if (!validateById('#Details_DOB', 'rd_dob')) {
        i++;
    }

    if (!validateById('#Details_MaritalStatus_Code', 'rd_marital')) {
        i++;
    }

    if (!validateById('#Details_Gender_Code', 'rd_Gender')) {
        i++;
    }
    if (!validateAadharWithoutMandatory('Details_Adhar', 'rd_Adhar')) {
        i++;
    }

    if (!validatePanWithoutMandatory('Details_PAN', 'rd_Pan')) {
        i++;
    }

    if (i == 0)
        return true
    else
        return false
}

function validatePersonal() {
    var i = 0;
    if (!validateById('#Details_House', 'rd_House')) {
        i++;
    }
    if (!validateById('#Details_Street', 'rd_Street')) {
        i++;
    }
    if (!validateById('#Details_City', 'rd_City')) {
        i++;
    }
    if (!validateById('#Details_Pin', 'rd_Pincode')) {
        i++;
    }

    if (!validateById('#Details_Country_Code', 'rd_Country')) {
        i++;
    }

    if (!validateById('#Details_State_Code', 'rd_State')) {
        i++;
    }

    if (!validateById('#Details_District_Code', 'rd_District')) {
        i++;
    }

    if (i == 0)
        return true
    else
        return false
}
function CheckIfExpEmpty() {
    debugger;
    var i = 0;
    $(".ifExpEmpty").each(function (index, object) {
        if (!validateById("#Experience_" + index + "__CompanyName", 'rd_CompanyName_' + index))
            i++;
        if (!validateById("#Experience_" + index + "__Location", 'rd_Location_' + index))
            i++;
        if (!validateById("#Experience_" + index + "__Designation", 'rd_Designation_' + index))
            i++;
        if (!validateById("#Experience_" + index + "__From", 'rd_From_' + index))
            i++;
        if (!validateById("#Experience_" + index + "__To", 'rd_To_' + index))
            i++;
    });
    if (i == 0)
        return true
    else
        return false

}

function validateExperience() {
    debugger
    var i = 0;
    var val = $("input[type='radio']:checked").val();
    if (val == 1) {
        if (!CheckIfExpEmpty())
            i++;
    }

    if (i == 0)
        return true
    else
        return false
}

function validateEDucation() {
    debugger
    var i = 0;
    $(".ifEduEmpty").each(function (index, object) {
        if (!validateById("#Education_" + index + "__QualificationType_Code", 'rd_Qualtype_' + index))
            i++;
        if (!validateById("#Education_" + index + "__Qualification_Code", 'rd_Qualificn_' + index))
            i++;
        if (!validateById("#Education_" + index + "__SpecializationOfBranch_Code", 'rd_Spec_' + index))
            i++;
        if (!validateById("#Education_" + index + "__YearOfCompletion", 'rd_Yr_' + index))
            i++;

    });
    if (i == 0)
        return true
    else
        return false
}

function validateCertification() {
    debugger
    var i = 0;
    $(".ifCertEmpty").each(function (index, object) {
        if (!validateById("#Certifications_" + index + "__Certificate", 'Certificate_' + index))
            i++;
        if (!validateById("#Certifications_" + index + "__CertificateType_Code", 'CertificateType_' + index))
            i++;
        if (!validateById("#Certifications_" + index + "__IssuedBy", 'IssuedBy_' + index))
            i++;
        if (!validateById("#Certifications_" + index + "__IssuedDate", 'IssuedDate_' + index))
            i++;
        if (!validateById("#Certifications_" + index + "__CertificateMode_Code", 'CertificateMode_' + index))
            i++;
        if (!validateById("#Certifications_" + index + "__CertificateValidity_Code", 'CertificateValidity_' + index))
            i++;
        if ($("#Certifications_" + index + "__CertificateValidity_Code").val() == 1) {
            if (!validateById("#Certifications_" + index + "__RenewalDate", 'renwDate_' + index))
                i++;
        }

    });
    if (i == 0)
        return true
    else
        return false
}
function validateSkill() {
    debugger
    var i = 0;
    $(".ifSkilEmpty").each(function (index, object) {
        if (!validateById("#Skill_" + index + "__SkillType_Code", 'skilType_' + index))
            i++;
        if (!validateById("#Skill_" + index + "__Skill_Code", 'skill_' + index))
            i++;
        if (!validateById("#Skill_" + index + "__SkillProficiency_Code", 'skilProf_' + index))
            i++;

    });
    if (i == 0)
        return true
    else
        return false
}

function validateSummary() {
    debugger
    var i = 0;
    if (!validateById('#Details_SuitedDesignation', 'rd_SuitedDesignation')) {
        i++;
    }

    if (!validateById('#Details_SuitableLocation', 'rd_SuitableLocation')) {
        i++;
    }
    if (!validateById('#Details_ResumeSource_Code', 'rd_ResumeSource')) {
        i++;
    }
    if ($("#Details_Resume_FileData").val() == "" && $("#Details_Resume_AbsoluteUri").val() == "") {
        swal.fire('Resume is required');
        i++;
    }
    if (i == 0)
        return true
    else
        return false
}
function onNextClick(tabName, StepperName) {
    debugger
    selectedTab = StepperName;
    if (tabName == "Rd_Basic") {

        if (validateContact()) {
            $('#Rd_Address').removeClass('hide');
            $('#Rd_Experience').addClass('hide');
            $('#Rd_Education').addClass('hide');
            $('#Rd_Certification').addClass('hide');
            $('#Rd_Skill').addClass('hide');
            $('#Rd_Summary').addClass('hide');
            $('#Rd_Basic').addClass('hide');
        }
        else
            return false;
    }
    else if (tabName == "Rd_Address") {
        //if (validatePersonal())
        //{
        $('#Rd_Experience').removeClass('hide');
        $('#Rd_Address').addClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Skill').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').addClass('hide');
        // }
        // else
        //return false;
    }
    else if (tabName == "Rd_Experience") {
        if (validateExperience()) {

            $('#Rd_Education').removeClass('hide');
            $('#Rd_Experience').addClass('hide');
            $('#Rd_Education').removeClass('hide');
            $('#Rd_Certification').addClass('hide');
            $('#Rd_Skill').addClass('hide');
            $('#Rd_Summary').addClass('hide');
            $('#Rd_Basic').addClass('hide');
        }
        else
            return false;
    }
    else if (tabName == "Rd_Education") {
        if (validateEDucation()) {
            $('#Rd_Certification').removeClass('hide');
            $('#Rd_Education').addClass('hide');
            $('#Rd_Basic').addClass('hide');
            $('#Rd_Experience').addClass('hide');
            $('#Rd_Skill').addClass('hide');
            $('#Rd_Summary').addClass('hide');
            $('#Rd_Basic').addClass('hide');
        }
        else
            return false;

    }
    else if (tabName == "Rd_Certification") {
        /*  if (validateCertification()) {*/
        $('#Rd_Skill').removeClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Basic').addClass('hide');
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Address').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').addClass('hide');
        //}
        //else
        //    return false;
    }

    else if (tabName == "Rd_Skill") {
        if (validateSkill()) {
            $('#Rd_Summary').removeClass('hide');
            $('#Rd_Education').addClass('hide');
            $('#Rd_Basic').addClass('hide');
            $('#Rd_Certification').addClass('hide');
            $('#Rd_Address').addClass('hide');
            $('#Rd_Experience').addClass('hide');
            $('#Rd_Skill').addClass('hide');
        }
        else
            return false;
    }
    else if (tabName == "Rd_Summary") {
        if (validateSummary()) {
            $('#Rd_Summary').removeClass('hide');
            $('#Rd_Education').addClass('hide');
            $('#Rd_Basic').addClass('hide');
            $('#Rd_Certification').addClass('hide');
            $('#Rd_Address').addClass('hide');
            $('#Rd_Experience').addClass('hide');
            $('#Rd_Skill').addClass('hide');
        }
        else
            return false;

    }
}





//function onNextClick(tabName) {
//    debugger
//    if (tabName == "Rd_Basic") {


//        $('#Rd_Address').removeClass('hide');
//        $('#Rd_Experience').addClass('hide');
//        $('#Rd_Education').addClass('hide');
//        $('#Rd_Certification').addClass('hide');
//        $('#Rd_Skill').addClass('hide');
//        $('#Rd_Summary').addClass('hide');
//        $('#Rd_Basic').addClass('hide');

//    }
//    else if (tabName == "Rd_Address") {
//        $('#Rd_Experience').removeClass('hide');
//        $('#Rd_Address').addClass('hide');
//        $('#Rd_Education').addClass('hide');
//        $('#Rd_Certification').addClass('hide');
//        $('#Rd_Skill').addClass('hide');
//        $('#Rd_Summary').addClass('hide');
//        $('#Rd_Basic').addClass('hide');
//    }
//    else if (tabName == "Rd_Experience")
//    {


//            $('#Rd_Education').removeClass('hide');
//            $('#Rd_Experience').addClass('hide');
//            $('#Rd_Education').removeClass('hide');
//            $('#Rd_Certification').addClass('hide');
//            $('#Rd_Skill').addClass('hide');
//            $('#Rd_Summary').addClass('hide');
//            $('#Rd_Basic').addClass('hide');        
//    }
//    else if (tabName == "Rd_Education") {

//        if (validateEDucation()) {
//            $('#Rd_Certification').removeClass('hide');
//            $('#Rd_Education').addClass('hide');
//            $('#Rd_Basic').addClass('hide');
//            $('#Rd_Experience').addClass('hide');
//            $('#Rd_Skill').addClass('hide');
//            $('#Rd_Summary').addClass('hide');
//            $('#Rd_Basic').addClass('hide');
//        }
//        else
//            return false;
//    }
//    else if (tabName == "Rd_Certification") {
//        $('#Rd_Skill').removeClass('hide');
//        $('#Rd_Education').addClass('hide');
//        $('#Rd_Basic').addClass('hide');
//        $('#Rd_Certification').addClass('hide');
//        $('#Rd_Address').addClass('hide');
//        $('#Rd_Summary').addClass('hide');
//        $('#Rd_Basic').addClass('hide');
//    }

//    else if (tabName == "Rd_Skill") {
//        $('#Rd_Summary').removeClass('hide');
//        $('#Rd_Education').addClass('hide');
//        $('#Rd_Basic').addClass('hide');
//        $('#Rd_Certification').addClass('hide');
//        $('#Rd_Address').addClass('hide');
//        $('#Rd_Experience').addClass('hide');
//        $('#Rd_Skill').addClass('hide');
//    }
//    else if (tabName == "Rd_Summary") {
//        $('#Rd_Summary').removeClass('hide');
//        $('#Rd_Education').addClass('hide');
//        $('#Rd_Basic').addClass('hide');
//        $('#Rd_Certification').addClass('hide');
//        $('#Rd_Address').addClass('hide');
//        $('#Rd_Experience').addClass('hide');
//        $('#Rd_Skill').addClass('hide');

//    }
//}

function onTabNameClick(tabName) {
    debugger
    if (tabName == "Rd_Basic") {
        $('#Rd_Address').addClass('hide');
        $('#Rd_Experience').addClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Skill').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').removeClass('hide');

    }
    else if (tabName == "Rd_Address") {
        $('#Rd_Experience').addClass('hide');
        $('#Rd_Address').removeClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Skill').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').addClass('hide');
    }
    else if (tabName == "Rd_Experience") {
        $('#Rd_Education').removeClass('hide');
        $('#Rd_Experience').removeClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Skill').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').addClass('hide');
    }
    else if (tabName == "Rd_Education") {
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Education').removeClass('hide');
        $('#Rd_Basic').addClass('hide');
        $('#Rd_Experience').addClass('hide');
        $('#Rd_Skill').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').addClass('hide');
    }
    else if (tabName == "Rd_Certification") {
        $('#Rd_Skill').addClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Basic').addClass('hide');
        $('#Rd_Certification').removeClass('hide');
        $('#Rd_Address').addClass('hide');
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Basic').addClass('hide');
    }

    else if (tabName == "Rd_Skill") {
        $('#Rd_Summary').addClass('hide');
        $('#Rd_Education').addClass('hide');
        $('#Rd_Basic').addClass('hide');
        $('#Rd_Certification').addClass('hide');
        $('#Rd_Address').addClass('hide');
        $('#Rd_Experience').addClass('hide');
        $('#Rd_Skill').removeClass('hide');
    }

}

function onResumeDBSaveClick() {
    debugger

    if (validateSummary()) {
        swal.fire({
            text: "Do you want to Save?",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok'
        }).then((result) => {
            if (result.isConfirmed) {
                var resumedata = new FormData($('#ResumeDBSave')[0]);

                $.ajax({

                    type: 'POST',
                    url: '/DataBank/ResumeDataBank/Save',
                    contentType: false,
                    processData: false,
                    dataType: 'json',
                    data: resumedata,
                    success: function (result) {
                        if (result.success == true) {
                            debugger
                            swal.fire(result.message).then(function () {
                                var url = '/DataBank/ResumeDataBank/Index';
                                window.location.href = url;

                            });

                        }

                        else {
                            debugger;
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
        return false;
}

function onDateChange(sender, type) {
    debugger
    if (validateById('#From', 'rd_From')) {
        if (validateById('#To', 'rd_To')) {
            if (new Date($("#From").val()) > new Date($("#To").val())) {
                $(sender).addClass('is-invalid');
                if (type == 'from') {
                    $("#rd_From").addClass('error-message');
                    $("#rd_From").html("From Date Should be less than To date");
                }
                else {
                    $("#rd_To").addClass('error-message');
                    $("#rd_To").html("To Date Should be greater than From date");
                }
                return false;
            }
            else {
                $(sender).removeClass('is-invalid');
                if (type == 'from') {
                    $("#rd_From").removeClass('error-message');
                    $("#rd_From").html("");
                }
                else {
                    $("#rd_To").removeClass('error-message');
                    $("#rd_To").html("");
                }
                //    var url = "/DataBank/ResumeDataBank/GetResumeList";
                //$('#rdList').load(url, { TabIndex: 5, FROMDATE: $("#From").val(), TODATE: $("#To").val() }, function () { });
                $.ajax({

                    type: 'GET',
                    url: '/DataBank/ResumeDataBank/GetResumeList',
                    dataType: 'html',
                    data: { TabIndex: 5, FROMDATE: $("#From").val(), TODATE: $("#To").val() },
                    success: function (result) {
                        debugger
                        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                            window.location.reload();
                        }

                        else {
                            $('#rdList').html("");
                            $('#rdList').html(result);
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

    }
}


//function onExportToExcelClick()
//{
//    debugger
//    table = $('#ResumeDBTable').DataTable();
//    var FROMDATE = $("#From").val();
//   var TODATE = $("#To").val();
//    if (table.page.info().recordsTotal > 0) {
//        url = "/DataBank/ResumeDataBank/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + TabName, "&FROMDATE=" + FROMDATE, "&TODATE=" + TODATE;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }
//}
function onPreviewClick(url) {
    debugger
    /*    var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);*/
    window.open(url, "_blank");
}

function onRestartClick(resumeGI) {
    debugger
    swal.fire({
        text: "Do you want to Restart?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {

            $.ajax({
                type: 'POST',
                url: '/DataBank/ResumeDataBank/ReleaseHold?ResumeDataBankGI=' + resumeGI,
                dataType: 'json',
                success: function (result) {
                    if (result.success == true) {
                        debugger
                        swal.fire(result.message).then(function () {
                            //var url = '/DataBank/ResumeDataBank/GetResumeList';
                            //$('#rdList').load(url, { TabIndex: 3, FROMDATE: null, TODATE:null }, function () { });
                            ApplyFilter('apply');
                        });

                    }

                    else {
                        debugger;
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

function viewResume(GI) {
    debugger;
    var url = "/DataBank/ResumeDataBank/ResumeView";
    /* window.location.href = url + '?ResumeGI=' + GI;*/
    window.open(url + '?ResumeGI=' + GI, '_blank');
}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    $("#SearchId").val($('#DurationwiseFilterSlab_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResumeDataBank/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&TabIndex=" + currentTab + "&FROMDATE=" + $("#From").val() + "TODATE=" + $("#To").val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#rdList').html("");
                        $('#rdList').html(result);

                        $('#rdList').removeClass('hide');
                    }
                    hideSpinner();

                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            showSpinner();
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResumeDataBank/ApplyFilter',
                dataType: 'html',
                data: $('#BasicFilter').serialize() + "&TabIndex=" + currentTab + "&FROMDATE=" + $("#From").val() + "TODATE=" + $("#To").val(),
                success: function (result) {
                    debugger
                    CloseSlider('basicFilter');
                    if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                        window.location.reload();
                    }

                    else {
                        $('#rdList').html("");
                        $('#rdList').html(result);

                        $('#rdList').removeClass('hide');
                    }
                    hideSpinner();
                    //$('#rewardsListPartial').addClass('hide');               
                },


                error: function (xhr, textStatus, errorThrown) {
                    $('body').html(xhr.responseText);
                }
            });
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        debugger;
        showSpinner();
        filterApplicable = 1;
        $('.reset').val('');
        $('.resetdd').val('');
        $(".reset").multiselect('destroy');
        $(".reset").multiselect();
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        $.ajax({

            type: 'POST',
            url: '/DataBank/ResumeDataBank/ApplyFilter',
            dataType: 'html',
            data: $('#BasicFilter').serialize() + "&TabIndex=" + currentTab + "&FROMDATE=" + $("#From").val() + "TODATE=" + $("#To").val(),
            success: function (result) {
                debugger

                //CloseSlider('assetAllocationFilter');

                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {
                    $('#rdList').html("");
                    $('#rdList').html(result);

                    $('#rdList').removeClass('hide');
                }
                hideSpinner();
                //$('#rewardsListPartial').addClass('hide');

            },


            error: function (xhr, textStatus, errorThrown) {
                $('body').html(xhr.responseText);
            }
        });
    }
}

function onYOSChange(val) {
    debugger;
    $('#YOSFrom').val('');
    $('#YOSTo').val('');
    YosText = $('#YSAboveOrBelow_Code option:selected').text();
    if (YosText == 'Between') {

        $('#YOSTo').removeClass('hide');
    }
    else {
        $('#YOSTo').addClass('hide');

    }
}


function onAgeChange(val) {
    debugger;
    $('#AgeFrom').val('');
    $('#AgeTo').val('');
    AgeText = $('#AgeAboveOrBelow_Code option:selected').text();
    if (AgeText == 'Between') {
        $('#AgeTo').removeClass('hide');

    }
    else {
        $('#AgeTo').addClass('hide');

    }

}
function Preview() {
    debugger
    if ($('#Details_Resume_FileData').val() == '' && $('#Details_Resume_AbsoluteUri').val() == '') {
        swal.fire("No file uploaded");
    }

    else if ($('#Details_Resume_FileData').val() != '') {
        var path = URL.createObjectURL($('.PreviewImage')[0].files[0]);
        window.open(path, "_blank");
    }
    else {
        window.open($('#Details_Resume_AbsoluteUri').val(), '_blank');
    }
}


//function onExportToExcelClick() {
//    debugger
//    var FROMDATE = $("#From").val();
//    var TODATE = $("#To").val();
//    if (currentTab == 1) {
//        var TableLength = $('#ResumeDataBank_List_Count').val();
//        //var TableLength = document.getElementById("ResumeDBTable").rows.length - 1;
//        if (TableLength > 0) {
//            url = "/DataBank/ResumeDataBank/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + TabName, "&FROMDATE=" + FROMDATE, "&TODATE=" + TODATE;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//    if (currentTab == 2) {
//        var TableLength = $('#ResumeDataBank_List_Count').val();
//        if (TableLength > 0) {
//            url = "/DataBank/ResumeDataBank/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + TabName, "&FROMDATE=" + FROMDATE, "&TODATE=" + TODATE;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }

//    if (currentTab == 3) {
//        var TableLength = $('#ResumeDataBank_List_Count').val();
//        if (TableLength > 0) {
//            url = "/DataBank/ResumeDataBank/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + TabName, "&FROMDATE=" + FROMDATE, "&TODATE=" + TODATE;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }


//    else
//    {
//        var TableLength = $('#ResumeDataBank_List_Count').val();
//        if (TableLength > 0) {
//            url = "/DataBank/ResumeDataBank/onExportToExcelClick?TabIndex=" + currentTab + "&TabName=" + TabName, "&FROMDATE=" + FROMDATE, "&TODATE=" + TODATE;
//            window.location.href = url;
//        }
//        else {
//            swal.fire("No data found to export");
//        }
//    }
//}


function ClickOnExport() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filterdata;
    var FROMDATE = $("#From").val();
    var TODATE = $("#To").val();
    var TableLength = $('#ResumeDataBank_List_Count').val();
    if (TableLength > 0) {
        url = "/DataBank/ResumeDataBank/ExportToExcel?" + PARAM + '&TabIndex=' + currentTab + "&TabName=" + TabName + "&FROMDATE=" + FROMDATE + "&TODATE=" + TODATE + "&TotalRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




function CheckProfilePic(e) {
    debugger;

    debugger;
    var size = $(e)[0].files[0].size;
    var filename = $(e)[0].files[0].name;
    var fileExtension = ['jpeg', 'jpg'];
    //if (size > 2048 * 1024) {
    //    swal.fire('File size cannot exceeds 2 Mb').then(function () {
    //        $(e).val('');
    //        return false;
    //    });
    //}
    if ($.inArray($(e).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        swal.fire("Incorrect format. Only jpeg and jpg images are allowed").then(function () {
            $(e).val('');
            return false;
        });
    }
    else {
        return true;
    }

}
function validateProfilePhoto() {
    debugger
    if ($("#Details_CandidatesPhoto_FileData").val() == null)
        swal.fire("No file Uploaded");

    else {
        if (CheckProfilePic(document.getElementById('Details_CandidatesPhoto_FileData'))) {
            var photo = new FormData($('#ResumeDBSave')[0]);
            //console.log(photo);
            $.ajax({

                type: 'POST',
                url: '/DataBank/ResumeDataBank/SaveProfileToFolder?SelectedTab=' + selectedTab,
                dataType: 'html',
                data: photo,
                contentType: false,
                processData: false,
                success: function (Data) {
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
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
    }


}