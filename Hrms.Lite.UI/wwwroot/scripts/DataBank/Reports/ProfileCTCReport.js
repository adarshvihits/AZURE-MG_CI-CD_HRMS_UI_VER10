
function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var SalaryPeriod = $('#Month').val();


    var dataTable = $("#profilectcTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/ProfileCTCReport/GetProfileCTCList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    debugger;
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SalaryPeriodCode=' + SalaryPeriod + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



                debugger;

                $('#List_Count').val(d.recordsTotal);
            

                for (var i = 0, ien = d.data.length; i < ien; i++)
                {


             
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID", "width":"10" },
            { "data": "employeeName", "width": "17" },
            { "data": "designation", "width": "10" },
            { "data": "department", "width": "8" },
            { "data": "location", "width": "8"},
            { "data": "category", "width": "7" },
            { "data": "region", "width": "7" },
            { "data": "gross", "width": "6" },
            { "data": "perks", "width": "6" },
            { "data": "pfmc", "width": "7" },
            { "data": "esimc", "width": "7"},
            { "data": "ctc", "width": "7" },



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














function GetList() {
    debugger;
    var SalaryPeriod = $('#Month').val();


    $.ajax({

        type: 'POST',
        url: '/DataBank/ProfileCTCReport/GetList',
        dataType: 'html',
        data: { SalaryPeriodCode: SalaryPeriod},

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ProfileCTCreportListID').html("");
                $('#ProfileCTCreportListID').html(result);
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

function ApplyFilter(mode) {
    debugger
    var SalaryPeriod = $('#Month').val();
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/ProfileCTCReport/ApplyFilter?SalaryPeriodCode=' + SalaryPeriod,
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#ProfileCTCreportListID').html("");
            //            $('#ProfileCTCreportListID').html(result);
            //        }
            //        hideSpinner();
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }

            //});
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/ProfileCTCReport/ApplyFilter?SalaryPeriodCode=' + SalaryPeriod,
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#ProfileCTCreportListID').html("");
            //            $('#ProfileCTCreportListID').html(result);
            //        }
            //        hideSpinner();
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        document.body.id = "kt_body";
            //        $('body').html(xhr.responseText);
            //    }

            //});
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        fillData();
        //$.ajax({

        //    type: 'POST',
        //    url: '/DataBank/ProfileCTCReport/ApplyFilter?SalaryPeriodCode=' + SalaryPeriod,
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#ProfileCTCreportListID').html("");
        //            $('#ProfileCTCreportListID').html(result);
        //        }
        //        hideSpinner();
        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //    }
        //});

    }
}


function ExportToExcel() {

    debugger;
    var SalaryPeriod = $('#Month').val();
    var month = $('#Month option:selected').text();
    var pendingTableLength = $('#List_Count').val();
    if (pendingTableLength > 0) {
        var url = "/DataBank/ProfileCTCReport/ExportToExcel?SalaryPeriodCode=" + SalaryPeriod + "&Month=" + month + "&TotalRec=" + pendingTableLength + '&data=' + $('#BasicFilter').serialize();;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}