var prevTab = 'all';
var currentTab = 'All';
var currTabName = 'All';
function tabClick(Tab, TabIndex, TabId, TabName) {
    debugger;
    showSpinner();
    currentTab = TabIndex;
    currTabName = TabName;
    $('#TabCode').val(TabId);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");    

    //ApplyFilter('apply');

    $.ajax({

        type: 'POST',
        url: '/DataBank/IndustryBenchmarkSalary/GetBenchmarkList',
        dataType: 'html',
        data: { TabIndex: currentTab},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#IndustryBenchmarkSalaryList').html("");
                $('#IndustryBenchmarkSalaryList').html(result);
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



function fillData() {

   
   /* $.ajaxSetup({ async: false });*/
    var TabIndex = currentTab;
    if (TabIndex == 'All') {

        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;

        /*  $('#JoiningReportTable').html('');*/
        var dataTable = $("#industryBenchmarkSalaryTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/IndustryBenchmarkSalary/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&TabIndex=" + currentTab + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {
                    debugger
                    $('.AllTabCount').html(d.allCount);
                    $('.Tab1Count').html(d.greaterCount);
                    $('.Tab2Count').html(d.lessCount);
                    $('.Tab3Count').html(d.sameCount);

                    $('#IndustryBenchmarkSalaryList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        if (d.data[i].flag == "RED") {

                            d.data[i].variation = '<td data-label="Variation"><span class="text-color-low">' + d.data[i].variation + '</span></td>'
                        }
                        if (d.data[i].flag == "GREEN") {
                            d.data[i].variation = '<td data-label="Variation"><span class="text-color-high">' + d.data[i].variation + '</span></td>'

                        }
                        if (d.data[i].flag == "EQUAL") {
                            d.data[i].variation = '<td data-label="Variation"><span>' + d.data[i].variation + '</span></td>'

                        }

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeHeader.employeeId","width":"10%" },
                { "data": "employeeHeader.employeeName", "width": "18%" },
                { "data": "designation.name", "width": "14%" },
                { "data": "department.name", "width": "13%" },
                { "data": "location.name", "width": "11%" },
                { "data": "wageGrade", "width": "10%" },
                { "data": "currentGP", "width": "8%"},
                { "data": "benchmarkGP", "width": "8%" },
                { "data": "variation", "width": "8%" },

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


        $("#dataTableSearch").on("keyup search paste cut", function () {
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
    else if (TabIndex == 'GREATER') {
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var TabIndex = 'GREATER';
        /*  $('#JoiningReportTable').html('');*/
        var dataTable1 = $("#GreaterBenchMarkListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/IndustryBenchmarkSalary/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&TabIndex=" + currentTab+'&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {
                    $('.AllTabCount').html(d.allCount);
                    $('.Tab1Count').html(d.greaterCount);
                    $('.Tab2Count').html(d.lessCount);
                    $('.Tab3Count').html(d.sameCount);

                    $('#IndustryBenchmarkSalaryList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;
                       

                        if (d.data[i].flag == "RED") {
                           
                            d.data[i].variation = '<td data-label="Variation"><span class="textcolorlow">' + d.data[i].variation + '</span></td>'
                        }
                        if (d.data[i].flag == "GREEN") {
                            d.data[i].variation = '<td data-label="Variation"><span class="text-color-high">' + d.data[i].variation + '</span></td>'

                        }
                        if (d.data[i].flag == "EQUAL") {
                            d.data[i].variation = '<td data-label="Variation"><span>' + d.data[i].variation + '</span></td>'

                        }

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeHeader.employeeId", "width": "10%" },
                { "data": "employeeHeader.employeeName", "width": "18%" },
                { "data": "designation.name", "width": "14%" },
                { "data": "department.name", "width": "13%" },
                { "data": "location.name", "width": "11%" },
                { "data": "wageGrade", "width": "10%" },
                { "data": "currentGP", "width": "8%" },
                { "data": "benchmarkGP", "width": "8%" },
                { "data": "variation", "width": "8%" },

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
           
            dataTable1.search(this.value).draw();
            $('#noDataFound').css("display", 'none');

        });

        $('.search-slider-inner').click(function () {
          
            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable1.search('').draw();
        });

        dataTable1.on('draw', function () {
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


    else if (TabIndex == 'LESS') {
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var TabIndex = 'LESS';
        /*  $('#JoiningReportTable').html('');*/
        var dataTable2 = $("#LessBenchMarkListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/IndustryBenchmarkSalary/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&TabIndex=" + currentTab + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {
                    $('.AllTabCount').html(d.allCount);
                    $('.Tab1Count').html(d.greaterCount);
                    $('.Tab2Count').html(d.lessCount);
                    $('.Tab3Count').html(d.sameCount);

                    $('#IndustryBenchmarkSalaryList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;


                        if (d.data[i].flag == "RED") {

                            d.data[i].variation = '<td data-label="Variation"><span class="textcolorlow">' + d.data[i].variation + '</span></td>'
                        }
                        if (d.data[i].flag == "GREEN") {
                            d.data[i].variation = '<td data-label="Variation"><span class="text-color-high">' + d.data[i].variation + '</span></td>'

                        }
                        if (d.data[i].flag == "EQUAL") {
                            d.data[i].variation = '<td data-label="Variation"><span>' + d.data[i].variation + '</span></td>'

                        }

                    }

                    return d.data;
                }

            },

            "columns": [


                { "data": "employeeHeader.employeeId", "width": "10%" },
                { "data": "employeeHeader.employeeName", "width": "18%" },
                { "data": "designation.name", "width": "14%" },
                { "data": "department.name", "width": "13%" },
                { "data": "location.name", "width": "11%" },
                { "data": "wageGrade", "width": "10%" },
                { "data": "currentGP", "width": "8%" },
                { "data": "benchmarkGP", "width": "8%" },
                { "data": "variation", "width": "8%" },

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
            dataTable2.search(this.value).draw();
            $('#noDataFound').css("display", 'none');

        });

        $('.search-slider-inner').click(function () {
           
            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable2.search('').draw();
        });

        dataTable2.on('draw', function () {
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



       else{
        var filter1 = $('#BasicFilter').serialize();
        var temp = filter1;
        var TabIndex = 'SAME';
        /*  $('#JoiningReportTable').html('');*/
        var dataTable3 = $("#SameBenchMarkListTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/IndustryBenchmarkSalary/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;
                        var searchValue = $('#dataTableSearch').val();
                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + "&TabIndex=" + currentTab + '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {
                    $('.AllTabCount').html(d.allCount);
                    $('.Tab1Count').html(d.greaterCount);
                    $('.Tab2Count').html(d.lessCount);
                    $('.Tab3Count').html(d.sameCount);

                    $('#IndustryBenchmarkSalaryList_Count').val(d.recordsTotal);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        debugger;


                        if (d.data[i].flag == "RED") {

                            d.data[i].variation = '<td data-label="Variation"><span class="textcolorlow">' + d.data[i].variation + '</span></td>'
                        }
                        if (d.data[i].flag == "GREEN") {
                            d.data[i].variation = '<td data-label="Variation"><span class="text-color-high">' + d.data[i].variation + '</span></td>'

                        }
                        if (d.data[i].flag == "EQUAL") {
                            d.data[i].variation = '<td data-label="Variation"><span>' + d.data[i].variation + '</span></td>'

                        }

                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeHeader.employeeId", "width": "10%" },
                { "data": "employeeHeader.employeeName", "width": "18%" },
                { "data": "designation.name", "width": "14%" },
                { "data": "department.name", "width": "13%" },
                { "data": "location.name", "width": "11%" },
                { "data": "wageGrade", "width": "10%" },
                { "data": "currentGP", "width": "8%" },
                { "data": "benchmarkGP", "width": "8%" },
                { "data": "variation", "width": "8%" },

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

                zeroRecords: '<div id="noDataFound"style="text-align: center;display: flex;align-items: center;justify-content: center;height: 45vh;"><img src="/images/nodata.png" /></div >',

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


       

        dataTable3.on('draw', function () {
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


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        if (currentTab == 'All') {
            dataTable.search(this.value).draw();
            $('#noDataFound').css("display", 'none');
        }

    });

    $('.search-slider-inner').click(function () {

        if (currentTab == 'All') {
            $('#dataTableSearch').val('');
            $('#dataTableSearch').focus();
            dataTable.search('').draw();

        }

      
    });


    //$.getJSON("/DataBank/IndustryBenchmarkSalary/GetTabCount", {}, function (data) {
    //    debugger;

        
    //    if (data.length != 0) {
    //        debugger;
    //        $('.AllTabCount').html(data.allCount);
    //        $('.Tab1Count').html(data.greaterBenchmarkSalaryCount);
    //        $('.Tab2Count').html(data.lessBenchmarkSalaryCount);
    //        $('.Tab3Count').html(data.sameBenchmarkSalaryCount);
    //    }
    //    else {
    //        debugger;
    //        $('.AllTabCount').html(0);
    //    }


    //});

}












function TabsDropdown(TabIndex) {
    debugger;
    currentTab = TabIndex;
    var Tab = GetTab($("#TabCode").prop('selectedIndex'));
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    showSpinner();   
    ApplyFilter('apply');
    prevTab = Tab;
  
}

function GetTab(TabCode) {
    if (TabCode == 0)
        return 'all'
    else if (TabCode == 1)
        return 'tab1';
    else if (TabCode == 2)
        return 'tab2';
    else
        return 'tab3';

}

function onAddBenchmarkSalaryClick() {
    debugger;
    $('#EmpCode').val("");
    $.ajax({

        type: 'GET',
        url: '/DataBank/IndustryBenchmarkSalary/AddBenchmarkSalary',
        dataType: 'html',      
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
               
                $('#addBenchmarkSalary').html("");
                $('#addBenchmarkSalary').html(result);
            }


        },
        error: function (xhr, textStatus, errorThrown) {
            debugger
            document.body.id = "kt_body";
            $('body').html(xhr.responseText);


        }
    });
    $('#addBenchmarkSalary').removeClass('hide');
}


function SaveIndustryBenchmarkSalary() {
    debugger;
    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            //    // Save it
            $.ajax({

                type: 'POST',
                url: '/DataBank/IndustryBenchmarkSalary/Create',
                dataType: 'json',
                data: $('#Save-IndustryBenchmarkSalary').serialize(),
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Saved Successfully").then(function () {
                            CloseSlider('addBenchmarkSalarySlider');                    
                            ApplyFilter('apply');
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
        else {
            return false;
        }
    })
}

function onExportToExcelClick() {
    debugger;    
    var benchmarkTableLength = $('#IndustryBenchmarkSalaryList_Count').val();
   
    if (benchmarkTableLength > 0) {
        url = "/DataBank/IndustryBenchmarkSalary/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currTabName + "&TotalRows=" + benchmarkTableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}

function ApplyFilter(mode) {
    debugger
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    $("#Tabindex").val(currentTab);
    //$("#SearchId").val($('#DurationwiseFilterSlab_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
           
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
            $('#filterForm').addClass('filter-apply');
        }
        
    }
    else {
     
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        /*CloseSlider('basicFilter');*/
      ;
    }
}

