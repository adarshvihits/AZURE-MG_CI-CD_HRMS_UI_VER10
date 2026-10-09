var prevTab = 'ActiveTab';
var currentTab = 0;




function OnTabClick(Tab, TabIndex) {
    debugger;
    currentTab = TabIndex;
    $("." + prevTab).removeClass("active");
    $("." + Tab).addClass("active");
    fillData();
    prevTab = Tab;
}

function fillData() {
   

        //$('.ActiveCount').html("@Model.ActiveCount");
        //$('.ResignedCount').html("@Model.ResignedCount");

        var filter1 = $('#BasicFilter').serialize();

        var temp = filter1;




        var dataTable = $("#ActiveListtable").DataTable({
            //"paging": true,
            "destroy":true,
            "pagingType": 'full_numbers',
            "ajax": {
                "url": "/Databank/GrowthHistory/GetActiveList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';

                        var current = $('#ActiveListtable').DataTable();


                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tabindex=' + currentTab + '&dummy=dummy';

                        d.filter = temp;

                        // alert(d.filter);

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {
                    $('#EmployeeProfileList_Count').val(d.recordsTotal);
                    if (d.data.length != 0) {
                        debugger;
                        $('.ActiveCount').html(d.activeCount);
                        $('.ResignedCount').html(d.resignedCount);
                    }
                    else {

                     // alert(d.data[0])

                      //  $('.ActiveCount').html(d.recordsTotal);
                    }



                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                 

                        d.data[i].employeeName = '<a href="/DataBank/GrowthHistory/Create?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=EmployeeProfile&BreadcrumbName=Employee Profile List">' + d.data[i].employeeName + '</a>'

                  
                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    }

                    return d.data;
                }

            },


            "columns": [

                { "data": "employeeid" },
                { "data": "employeeName" },
                { "data": "designation" },
                { "data": "department" },
                { "data": "location" },
                { "data": "category" },
                { "data": "doj" },
                { "data": "yos" },


            ],


            "dom": 'rt<"bottom"><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',
            "serverSide": "true",
            "scrollY": '52vh',
            "sScrollX": "100%",
            "sScrollXInner": "100%",

            "autoWidth": false,

            "scrollCollapse": false,

            "order": [0, "asc"],
            "processing": "true",
            language: {

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


//function OnTabClick(Tab, TabIndex) {
//    showSpinner();
//    currentTab = TabIndex;
//    $("." + prevTab).removeClass("active");
//    $("." + Tab).addClass("active");
   
//    //$('#tabsDropdownAllowance').val(currentTab);
//    $.ajaxSetup({
//        async: false
//    });

//    $.ajax({

//        type: 'POST',
//        url: '/DataBank/GrowthHistory/GetActiveList',
//        dataType: 'html',
//        data: { TabIndex: TabIndex },
//        success: function (result) {
//            debugger
//            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                window.location.reload();
//            }
//            else {
//              //  $('#EmployeeList').html("");
//              //  $('#EmployeeList').html(result);
//            }
//            hideSpinner();


//        },


//        error: function (xhr, textStatus, errorThrown) {
//            debugger
//            document.body.id = "kt_body";
//            $('body').html(xhr.responseText);
//            hideSpinner();

//        }
//    });
//    prevTab = Tab;


//}

function ExportToExcel() {
    debugger;
    /*if (validateDropdowns()) {*/
    if (currentTab == 0) {
        var currentTabName = "Active";
        /*  var ActiveListtableLength = document.getElementById("ActiveListtable").rows.length - 1;*/
        var ActiveListtableLength = $('#EmployeeProfileList_Count').val();
        if (ActiveListtableLength > 0) {
            url = "/DataBank/GrowthHistory/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + "&TotalRow=" + ActiveListtableLength + '&data=' + $('#BasicFilter').serialize();;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
    else if (currentTab == 1) {
        debugger
        var currentTabName = "Resigned";
        var ResignedListTableLength = $('#EmployeeProfileList_Count').val();
        if (ResignedListTableLength > 0) {
            url = "/DataBank/GrowthHistory/ExportToExcel?TabIndex=" + currentTab + "&TabName=" + currentTabName + "&TotalRow=" + ResignedListTableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
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

function Expand(index) {
    debugger
    $('.fa-plus-circle').removeClass('hide');
    $('.fa-minus-circle').addClass('hide');
   


    let elem = document.getElementsByClassName("view_" + index);
    let isMainPresent = elem[0].classList.contains("open");
    if (isMainPresent) {
        $('.icon_' + index + ' .fa-plus-circle').removeClass('hide');
        $('.icon_' + index + ' .fa-minus-circle').addClass('hide');


    }
    else {
        $('.expand-btn')
            .removeClass("open")
            .next(".fold")
            .removeClass("open");

        $.ajax({

            type: 'GET',
            url: '/DataBank/GrowthHistory/GetGrowthHistoryDetails',
            dataType: 'html',
            data: { EmployeeGI: $('#GrowthHistory_' + index + '__EmployeeGI').val(), HistoryGI: $('#GrowthHistory_' + index + '__HistoryGI').val() },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    $('#InnerList_' + index).html("");
                    $('#InnerList_' + index).html(result);
                }

            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);


            }
        });


        $('.icon_' + index + ' .fa-plus-circle').addClass('hide');
        $('.icon_' + index + ' .fa-minus-circle').removeClass('hide');
    }


    $('.view_' + index)
        .toggleClass("open")
        .next(".fold")
        .toggleClass("open");

}