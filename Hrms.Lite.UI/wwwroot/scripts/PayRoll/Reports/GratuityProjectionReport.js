
function fillDataResigned() {
    debugger;


    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var type = $('input[name="EmployeeStatus"]:checked').val();
    /*var type = $('#EmployeeStatus').val();*/
    var dataTable = $("#GratuityProjectionResignedTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/GratuityProjectionReport/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + type + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {
                debugger;

                if (d.tab == 0) {
                    $('.lwd').addClass("hide");
                }
                $('#GratuityProjection_Count').val(d.recordsTotal);



                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].doc = _GetFormattedDate(d.data[i].doc);
                    if (d.data[i].lwd != null)
                        d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                }



                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "12%" },
            { "data": "designation", "width": "10%" },
            { "data": "department", "width": "10%" },
            { "data": "location", "width": "9%" },
            { "data": "doj", "width": "8%" },
            { "data": "doc", "width": "8%" },
            { "data": "lwd", "width": "8%" },
            { "data": "yos", "width": "6%" },
            { "data": "lds", "width": "7%" },
            { "data": "gratuityApllicableSalary", "width": "9%" },
            { "data": "eligibleGratuityAmount", "width": "10%" },




        ],






        "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        "serverSide": "true",
        "scrollY": '52vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",
        "AutoWidth": false,

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

    setTimeout(tooltipHover, 500);

}






function fillData() {
    debugger;
 
   
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var type = $('input[name="EmployeeStatus"]:checked').val();
    /*var type = $('#EmployeeStatus').val();*/



    if (type == 0) {
        var dataTable = $("#GratuityProjectionActiveTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/GratuityProjectionReport/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + type + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;

                    if (d.tab == 0) {
                        $('.lwd').addClass("hide");
                    }
                    $('#GratuityProjection_Count').val(d.recordsTotal);



                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].doc = _GetFormattedDate(d.data[i].doc);

                    }



                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "employeeName", "width": "12%" },
                { "data": "designation", "width": "10%" },
                { "data": "department", "width": "10%" },
                { "data": "location", "width": "9%" },
                { "data": "doj", "width": "8%" },
                { "data": "doc", "width": "8%" },
                /* {*//* *//*"data": "lwd", "width": "8%" },*/
                { "data": "yos", "width": "7%" },
                { "data": "lds", "width": "7%" },
                { "data": "gratuityApllicableSalary", "width": "9%" },
                { "data": "eligibleGratuityAmount", "width": "10%" },




            ],






            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,

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

        setTimeout(tooltipHover, 500);
    }

    else if (type == 1) {
        debugger
        var dataTable = $("#GratuityProjectionResignedTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/PayRoll/GratuityProjectionReport/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();


                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + type + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    debugger;                    
                    if (d.tab == 0) {
                        $('.lwd').addClass("hide");
                    }
                    $('#GratuityProjection_Count').val(d.recordsTotal);



                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].doc = _GetFormattedDate(d.data[i].doc);
                        if (d.data[i].lwd != null)
                            d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    }



                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "9%" },
                { "data": "employeeName", "width": "10%" },
                { "data": "designation", "width": "9%" },
                { "data": "department", "width": "9%" },
                { "data": "location", "width": "9%" },
                { "data": "doj", "width": "8%" },
                { "data": "doc", "width": "8%" },
                { "data": "lwd", "width": "8%" },
                { "data": "yos", "width": "6%" },
                { "data": "lds", "width": "7%" },
                { "data": "gratuityApllicableSalary", "width": "9%" },
                { "data": "eligibleGratuityAmount", "width": "8%" },




            ],






            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",
            "AutoWidth": false,

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

        setTimeout(tooltipHover, 500);
    }






 

}



function onRadiobuttonClick(val) {
    debugger;
  
    $.ajax({

        type: 'GET',
        url: '/PayRoll/GratuityProjectionReport/GetGratuityProjectionReportList',
        dataType: 'html',
        data: { Type: val },
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }

            else {
                $('#gratuityProjectionList').html("");
                $('#gratuityProjectionList').html(result);
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




function ExportToExcel()
{
    debugger;
   
    var type = $('#EmployeeStatus').val();
    var TableLength = $('#GratuityProjection_Count').val();
    if (TableLength > 0)
    {
        url = "/PayRoll/GratuityProjectionReport/ExportToExcel?TotalRec=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}



function ApplyFilter(mode) {
    debugger;
  
    /*var Radioval = $('input[name="EmployeeStatus"]:checked').val();*/
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayaRoll/GratuityProjectionReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        $('#gratuityProjectionList').html("");
            //        $('#gratuityProjectionList').html(result);

            //        $('#gratuityProjectionList').removeClass('hide');

                         
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        swal.fire(xhr.responseText);
            //    }
            //});
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayaRoll/GratuityProjectionReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        $('#gratuityProjectionList').html("");
            //        $('#gratuityProjectionList').html(result);

            //        $('#gratuityProjectionList').removeClass('hide');

                            
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        swal.fire(xhr.responseText);
            //    }
            //});
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //$.ajax({

        //    type: 'POST',

        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger

             
        //        $('#gratuityProjectionList').html("");
        //        $('#gratuityProjectionList').html(result);

        //        $('#gratuityProjectionList').removeClass('hide');
             

        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        swal.fire(xhr.responseText);
        //    }
        //});
    }
}