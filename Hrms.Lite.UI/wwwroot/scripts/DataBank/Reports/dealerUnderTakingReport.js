

function fillData(status) {

    debugger;
     
    var filter1 = $('#BasicFilter').serialize();

    var TabIndex = $('input[name="TabIndex"]:checked').val();


    /* if (Allowance != '') {*/
    var dataTable = $("#DealerUnderTakingReporttable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": true,
        "ajax": {
            "url": "/DataBank/DealerUnderTakingReport/GetDealerUnderTakingList",
            "type": "POST",

            "datatype": "json",
            'data':
                function (d) {
                    var searchValue = $('#dataTableSearch').val();

                    // 🔥 IMPORTANT — PASS TAB INDEX HERE
                    d.filter = filter1 +
                        '&draw=' + d.draw +
                        '&start=' + d.start +
                        '&length=' + d.length +
                        '&searchValue=' + searchValue +
                        '&TabIndex=' + TabIndex +  // 🔥 CHANGED
                        '&dummy=dummy';

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {
                $('#DealerUnderTakingList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    d.data[i].effectiveDate = _GetFormattedDate(d.data[i].effectiveDate);
                    d.data[i].closeDate = _GetFormattedDate(d.data[i].closeDate);
                    
                    d.data[i].entryDate = _GetFormattedDate(d.data[i].entryDate);
                    d.data[i].des = '<div class="d-flex justify-content-center"><a data-bs-toggle="tooltip" title="" class="view_btn m-0" onclick = "onEditClick(' + "'" + d.data[i].employeeGI + "'" + ')"  data-bs-original-title="Approve" aria-describedby="tooltip462179"> </a></div>'



                }
                console.log(d.data);
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeId" },
            { "data": "name" },
            { "data": "designationName", },
            { "data": "departmentName"},
            { "data": "locationName" },
            { "data": "effectiveDate" },
            { "data": "closeDate" },
            { "data": "entryDate" },
            { "data": "remarks" },
            { "data": "status"},
            { "data": "createdBy"},
            { "data": "approvedBy" },
            { "data": "des" },
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

    /*setTimeout(datepicker, 500);*/

}
















//function onEditClick(employeeGI) {
//    var TabIndex = $('input[name="TabIndex"]:checked').val();

//    if (TabIndex == "1")
//        $.ajax({

//            type: 'GET',
//            url: "/DataBank/EmployeeVsNps/Edit",
//            dataType: 'html',
//            data: { EmployeeGI: employeeGI },

//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    $('body').append('<div class="form-overlay"></div>');
//                    $('#AddNewOrEditSlider').html("");
//                    $('#AddNewOrEditSlider').html(result);
//                }
//                hideSpinner();


//            },
//            error: function (xhr, textStatus, errorThrown) {
//                debugger
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//                hideSpinner();

//            }
//        });
//    else
//        $.ajax({

//            type: 'GET',
//            url: "/DataBank/EmployeeVsNps/Create",
//            dataType: 'html',
//            data: { EmployeeGI: employeeGI },

//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    $('body').append('<div class="form-overlay"></div>');
//                    $('#AddNewOrEditSlider').html("");
//                    $('#AddNewOrEditSlider').html(result);
//                }
//                hideSpinner();


//            },
//            error: function (xhr, textStatus, errorThrown) {
//                debugger
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//                hideSpinner();

//            }
//        });

//}







function ApplyFilter(mode) {
    debugger

    if (mode == 'apply') {
        /* showSpinner();*/
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
        /* showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //    CloseSlider('basicFilter');
    }
}










function ExportToExcel() {
    debugger;
    var TabIndex = $('input[name="TabIndex"]:checked').val();
    var Filter = $('#BasicFilter').serialize();
    var tlength = 0;
    if (TabIndex == 0) {
        var currentTabName = "ALL";
        tlength = $('#DealerUnderTakingList_Count').val();
    }
    else if (TabIndex == 1) {
        var currentTabName = "Active";
        tlength = $('#DealerUnderTakingList_Count').val();
    }
    else {
        var currentTabName = "Resigned";
        tlength = $('#DealerUnderTakingList_Count').val();
    }

    if (tlength != 0) {
        url = "/DataBank/DealerUnderTakingReport/ExportToExcel?TabIndex=" + TabIndex + "&currentTabName=" + currentTabName +
            "&Filter=" + Filter + "&TotalRows=" + tlength;
        window.location.href = url;
    }
    else {
        swal.fire(NoDataFoundMsg);
    }
}





function onEditClick(EmployeeGI) {

    debugger;
    $.ajax({
        type: 'GET',
        url: "/DataBank/DealerUnderTakingReport/Edit?EmployeeGI= " + EmployeeGI,

        dataType: 'html',

        success: function (Data) {
            var m = $('#UHID_Popup');
            m.find('.modal-body').html('');
            m.find('.modal-body').html(Data);
            m.modal('show');
        },
        error: function (xhr, textStatus, errorThrown) {
            swal.fire(xhr.responseText);

        }
    });

}