var prevTab = 'ActiveTab';
var currentTab = 0;


function fillData() {

    debugger;
    var TabIndex = currentTab;

    if (TabIndex == 0) {
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;


        var dataTable = $("#SalaryStructureActiveTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/SalaryStructureReport/GetSalaryStructureReportList",
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


                    debugger
                    $('.AllTabCount').html(d.data.Active);
                    $('.ResignedTabCount').html(d.data.Resigned);


                    $('#SalaryStructureReportDetails_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);

                        //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "class":"sticky-col first-col" },
                { "data": "fullName", "class":"second-col sticky-col" },
                { "data": "salaryStructure", "class":"designation-col sticky-col " },
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "category" },
                { "data": "doj" },
                { "data": "lid" },
                { "data": "yos" },
                { "data": "basicSalary" },
                { "data": "otherAllowance" },
                { "data": "grossSalary" }
               


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
    else {

        debugger;
        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;

      

        var dataTable = $("#SalaryStructureResignedTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Databank/SalaryStructureReport/GetSalaryStructureReportList",
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



                    debugger


                    $('.AllTabCount').html(d.count.activeCount);
                    $('.ResignedTabCount').html(d.count.resignedCount);



                    $('#SalaryStructureReportDetails_Count').val(d.recordsTotal);
                    for (var i = 0, ien = d.data.length; i < ien; i++) {


                        //d.data[i].employeeid = '<div class="d-flex align-items-center"><div class="img-report"><img src="' + d.data[i].absoluteUri + '">  </div> <div class="name-post"><div>  <a href = "/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List" >' + d.data[i].employeeName + ' </a > </div> <div> (' + d.data[i].employeeid + ')' + ' </div></div> </div>'


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID","class": "sticky-col first-col"  },
                { "data": "fullName","class": "second-col sticky-col" },
                { "data": "salaryStructure", "class": "designation-col sticky-col "},
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "category" },
                { "data": "doj" },
                { "data": "lid" },
                { "data": "yos" },
                { "data": "lwd" },

                { "data": "basicSalary" },
                { "data": "grossSalary" },
                { "data": "otherAllowance" },


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

    $.ajaxSetup({ async: false });
    debugger;



    currentTab = TabIndex;
    currentTabName = Tab;
    $('#TabCode').val(TabIndex);
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    //var url = "/DataBank/EmployeeProfile/GetEmployeeProfileList";
    //$('#activeListPartial').load(url, { TabIndex: TabIndex }, function () { });
    ApplyFilter('apply');
    prevTab = Tab;


   // debugger;
   ///* showSpinner();*/
   // currentTab = TabIndex;
   // $("." + prevTab).removeClass("active");
   // $("." + Tab).addClass("active");
   // $.ajax({

   //     type: 'POST',
   //     url: '/DataBank/SalaryStructureReport/GetList',
   //     dataType: 'html',
   //     data: { TabIndex: TabIndex },

   //     success: function (result) {
   //         debugger
   //         if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
   //             window.location.reload();
   //         }
   //         else {
   //             $('#SalaryList').html("");
   //             $('#SalaryList').html(result);
   //         }
   //         hideSpinner();


   //     },


   //     error: function (xhr, textStatus, errorThrown) {
   //         debugger
   //         document.body.id = "kt_body";
   //         $('body').html(xhr.responseText);
   //         hideSpinner();

   //     }
   // });
   // prevTab = Tab;


}


function ExportToExcel() {
    debugger;
    if (currentTab == 0) {
        var searchValue = $('#dataTableSearch').val();
        var TableLength = $('#SalaryStructureReportDetails_Count').val();
        if (TableLength > 0) {
            url = "/DataBank/SalaryStructureReport/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + 'ACTIVE' + "&totRecords=" + TableLength + '&searchValue=' + searchValue + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else
    {
        var searchValue = $('#dataTableSearch').val();
        var TableLength = $('#SalaryStructureReportDetails_Count').val();
        if (TableLength > 0) {
            url = "/DataBank/SalaryStructureReport/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + 'RESIGNED' + "&totRecords=" + TableLength + '&searchValue=' + searchValue + '&data=' + $('#BasicFilter').serialize();
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }

    }
}



function ApplyFilter(mode) {

    
    if (mode == 'apply') {
       
        if (filterApplicable >= 1) {
            
            
          
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
           
         
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

       
    }

    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');       
    }

    debugger;
    $('#Report').data('kendoGrid').dataSource.read().then(function () {
        var totalRecords = $('#Report').data('kendoGrid').dataSource.total();
        $('#active').text(totalRecords);
        var firstRowData = $('#Report').data('kendoGrid').dataSource.data()[0];
        var tab = firstRowData.Active        // Assuming you want the first row
        var activeValue = firstRowData.Resigned; // Replace "active" with the appropriate column field name
        $('#active').text(tab);
        $('#resigned').text(activeValue);
        if (currentTab == 0)
            $('#SalaryStructureReportDetails_Count').val(tab);
        else
            $('#SalaryStructureReportDetails_Count').val(activeValue)

        $('#Report').data('kendoGrid').dataSource.page(1);
    });
    $('#Report').data('kendoGrid').refresh();
}




//function ApplyFilter(mode) {
//    var tabIndex = $('#Tabindex').val();
//    var search = $('#SearchId').val();
//    $("#Tabindex").val(currentTab);
//    $("#SearchId").val($('#FilterList_Code').val())
//    if (mode == 'apply') {
//        if (filterApplicable >= 1) {
//            fillData();
//            CloseSlider('basicFilter');
//            $('#filterForm').removeClass('filter-apply');
//        }
//        else {
//            //showSpinner();
//            fillData();
//            CloseSlider('basicFilter');
//            $('#filterForm').addClass('filter-apply');
//        }
//    }
//    else {
//        /*showSpinner();*/
//        filterApplicable = 1;
//        $('.reset').val('');
//        $('#Optional_Filter_EmploymentStatus_Code').val(0);
//        $('#filterForm').removeClass('filter-apply');
//        fillData();
//           }
//}