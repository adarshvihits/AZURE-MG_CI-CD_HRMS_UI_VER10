

function fillHeading() {
    debugger 

    var ReportType_Name = $("#ReportType_Code option:selected").text();
        $.ajax({

            type: 'GET',
            url: '/DataBank/PersonalDetailsReport/GetReportHeading',
            dataType: 'html',
            data: { TypeCode: $("#ReportType_Code").val(), ReportType_Name: ReportType_Name },

            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#pdreport').html("");
                    $('#pdreport').html(result);
                }         


            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);
                hideSpinner();

            }
        });

       
    } 


function fillData()
{
    debugger;

    var filter1 = $('#BasicFilter').serialize();
    var data1 = $("#personalreport_form").serialize();
    var temp = filter1;
    var type = $('#ReportType_Code').val();
    PersonalDetailsTypes = [];
    PersonalDetailsTypes = $("#PersonalDetailsTypes").val();
    if (type == 3) // family
    {
        debugger;
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",


                "datatype": "json",
                'data':
                    function (d) {
                        debugger;
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1  + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&type=' + type +
                            '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;
                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        
                        d.data[i].dateOfBirth = _GetFormattedDate(d.data[i].dateOfBirth);

                    }


                  
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "6" },
                { "data": "employeeName", "width": "12" },
                { "data": "designation", "width": "8" },
                { "data": "department", "width": "8" },
                { "data": "location", "width": "9" },
                { "data": "category", "width": "8" },
                { "data": "region", "width": "7" },
                { "data": "relationName", "width": "7" },
                { "data": "reportType", "width": "8" },
                { "data": "dateOfBirth", "width": "9" },
                { "data": "occupation", "width": "9" },
                { "data": "statusName", "width": "4" },
                { "data": "uhid", "width": "4" },
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
    } //Family end

    else if (type == 2) //Qualification 
    {
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1  + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue
                            + '&type=' + type + '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);                    
                    //d.data[i].file.actualFileName = ''
                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        if (d.data[i].file != null)
                            d.data[i].file = '<button data-bs-toggle="tooltip" title="" id="viewgrievance" type="button" class=" btn btn-icon btn-eye icon-only pr-2" onclick="showDocument(' + "'" + d.data[i].file.fileName + "'" + ',' + "'" + d.data[i].file.absoluteUri + "'" + ')" data-bs-original-title=""> <i class="icon-xl far fa-eye"></i></button >'
                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "9" },
                { "data": "employeeName", "width": "11" },
                { "data": "designation", "width": "10" },
                { "data": "department", "width": "10" },
                { "data": "location", "width": "10" },
                { "data": "category", "width": "10" },
                { "data": "region", "width": "8" },            
                { "data": "college_University", "width": "12" },
                { "data": "aggregate", "width": "6" },   
                { "data": "reportType", "width": "8" },
                { "data": "file", "width": "6" }
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
    } //qualification ends
    else if (type == 5) //language
    {
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';
                        var start = d.start;
                                                var searchValue = $('#dataTableSearch').val();
                        temp = filter1  + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&type=' + type +
                            '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);                  

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        if (d.data[i].read != true) {
                            d.data[i].read = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                        }
                        else {
                            d.data[i].read = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                        }
                        if (d.data[i].write != true) {
                            d.data[i].write = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                        }
                        else {
                            d.data[i].write = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                        }
                        if (d.data[i].speak != true) {
                            d.data[i].speak = '<button type="button"class="btn btn-icon btn-close border-radius"> <i class="icon-xl fas fa-times"></i> </button>'
                        }
                        else {
                            d.data[i].speak = '<button type="button" class="btn btn-icon btn-tick"><i class="icon-xl fas fa-check"></i></button>'
                        }
                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "9" },
                { "data": "employeeName", "width": "13" },
                { "data": "designation", "width": "10" },
                { "data": "department", "width": "10" },
                { "data": "location", "width": "9" },
                { "data": "category", "width": "9" },
                { "data": "region", "width": "8" },
    
                { "data": "read", "width": "8" },
                { "data": "write", "width": "8" },
                { "data": "speak", "width": "8" },
                { "data": "reportType", "width": "10" },
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
    } //languages

    else if (type == 6) //Certification
    {
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue
                            + '&type=' + type + '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {                    
                        if (d.data[i].file!=null)
                            d.data[i].file = '<button data-bs-toggle="tooltip" title="" id="viewgrievance" type="button" class=" btn btn-icon btn-eye icon-only pr-2" onclick="showDocument(' + "'" + d.data[i].file.fileName + "'" + ',' + "'" + d.data[i].file.absoluteUri + "'" + ')" data-bs-original-title=""> <i class="icon-xl far fa-eye"></i></button >'
                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "9" },
                { "data": "employeeName", "width": "15" },
                { "data": "designation", "width": "15" },
                { "data": "department", "width": "12" },
                { "data": "location", "width": "12" },
                { "data": "category", "width": "12" },
                { "data": "region", "width": "11" },

                { "data": "reportType", "width": "14" },
                { "data": "file", "width": "6" }
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
    } //Certification ends
    else if (type == 7) //Annual Health
    {
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue
                            + '&type=' + type + '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10" },
                { "data": "employeeName", "width": "19" },
                { "data": "designation", "width": "18" },
                { "data": "department", "width": "18" },
                { "data": "doj", "width": "6" },
                { "data": "gender", "width": "5" },
                { "data": "mobile", "width": "5" },
                { "data": "age", "width": "5" },
                { "data": "reportType", "width": "14" }
               
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
    } //Annual Health ends
    else if (type == 8) //Previous Employer
    {
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';
                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue
                            + '&type=' + type + '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].fromDate = _GetFormattedDate(d.data[i].fromDate);
                        d.data[i].toDate = _GetFormattedDate(d.data[i].toDate);
                    }
                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "9" },
                { "data": "employeeName", "width": "15" },
                { "data": "designation", "width": "15" },
                { "data": "department", "width": "12" },
                { "data": "location", "width": "12" },
                { "data": "doj", "width": "5" },
                { "data": "reportType", "width": "11" },
                { "data": "previousDesignation", "width": "11" },
                { "data": "fromDate", "width": "5" },
                { "data": "toDate", "width": "5" }
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
    } //Previous Employer ends
    else {
        var dataTable = $("#PersonalDetailsReportTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/PersonalDetailsReport/GetReportList",
                "type": "POST",

                "datatype": "json",
                'data':

                    function (d) {
                        debugger;
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1  + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&type=' + type +
                            '&PersonalDetailsTypes=' + PersonalDetailsTypes + '&dummy=dummy';
                        d.filter = temp;
                        d.data1 = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    $('#PersonalDetailsDetailedReport_Count').val(d.recordsTotal);                

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "9" },
                { "data": "employeeName", "width": "15" },
                { "data": "designation", "width": "15" },
                { "data": "department", "width": "12" },
                { "data": "location", "width": "12" },
                { "data": "category", "width": "12" },
                { "data": "region", "width": "11" },
        
                { "data": "reportType", "width": "14" },

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
    }
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
            //.search('').draw();
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




function onReportTypeChange(sender) {
    debugger
    $.ajaxSetup({ async: false });
    if ($("#ReportType_Code option:selected").text() != "Previous Employement" && $("#ReportType_Code option:selected").text() != "Previous Employement - Format 1") {

        $.getJSON("/DataBank/PersonalDetailsReport/GetPersonalDetailsTypeDropdown", { Type: $(sender).val() }, function (data) {
            debugger;
            console.log(data);
            $('#PersonalDetailsTypes option').remove();

            for (var i = 0; i < data.length; i++) {
                $('#PersonalDetailsTypes').append('<option value="' + data[i].code + '">' + data[i].name + '</option');
            }
            $('.multiselectDropdownFields').multiselect('rebuild');
            debugger;
            $('#PersonalDetailsTypes')
                .multiselect('selectAll', false)
            

        });
        $(".PersonalDetailsTypesDropdown").removeClass('hide');
    }

    else
        $(".PersonalDetailsTypesDropdown").addClass('hide');


    $(".DetailsType").html($("#ReportType_Code option:selected").text());
    $("._DetailsType").html($("#ReportType_Code option:selected").text());
    fillHeading();
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
      
    }
}

function ExportToExcel() {
    debugger;
    var tableLength = $('#PersonalDetailsDetailedReport_Count').val();
    var ReportType = $('#ReportType_Code option:selected').text();
    if (tableLength > 0) {
        url = "/DataBank/PersonalDetailsReport/ExportToExcel?tableLength=" + tableLength + "&ReportType=" + ReportType;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}