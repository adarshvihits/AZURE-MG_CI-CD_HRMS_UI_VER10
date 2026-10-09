



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var month = $('#Month').val();
    var EsiGroup = $('#ESIGroup').val();

    
    var dataTable = $("#EsireportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/ESIReport/GetESIReportList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&month=' + month + '&EsiGroup=' + EsiGroup + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {



               
              
                $('#EsiEcrSummary_Count').val(d.recordsTotal);
               

                return d.data;
            }

        },

        "columns": [

            { "data": "ipNumber" },
            { "data": "ipName"},
            { "data": "noDaysWagepaid" },
            { "data": "toatalMonthWage"},
            { "data": "reasonCode"},
            { "data": "lastWrkingDay"},
            
          


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
      /*  dataTable.search('').draw();*/
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












function ApplyFilter(mode) {
    debugger;
    var month = $('#Month').val();
    var EsiGroup = $('#ESIGroup').val();
   
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            fillData();
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayaRoll/ESIReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize() + "&Month=" + month + "&EsiGroup=" + EsiGroup,
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        $('#EsiReportList').html("");
            //        $('#EsiReportList').html(result);

            //        $('#EsiReportList').removeClass('hide');

            //        //$('#rewardsListPartial').addClass('hide');               
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        swal.fire(xhr.responseText);
            //    }
            //});
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            CloseSlider('basicFilter');
            fillData();
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayaRoll/ESIReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize() + "&Month=" + month + "&EsiGroup=" + EsiGroup,
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        $('#EsiReportList').html("");
            //        $('#EsiReportList').html(result);

            //        $('#EsiReportList').removeClass('hide');

                          
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
        //    data: $('#BasicFilter').serialize() + "&Month=" + month + "&EsiGroup=" + EsiGroup,
        //    success: function (result) {
        //        debugger

        //        //CloseSlider('assetAllocationFilter');


        //        $('#EsiReportList').html("");
        //        $('#EsiReportList').html(result);

        //        $('#EsiReportList').removeClass('hide');
        //        //$('#rewardsListPartial').addClass('hide');

        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        swal.fire(xhr.responseText);
        //    }
        //});
    }
}
function GetList() {

    debugger
    var month = $('#Month').val();
    var EsiGroup = $('#ESIGroup').val();
    /* showSpinner();*/
    if (month !== '' && EsiGroup !== '') {
        var url = "/PayRoll/ESIReport/GetList?Month=" + month + "&EsiGroup=" + EsiGroup;
        $('#EsiReportList').load(url, function () { /*hideSpinner();*/ });
    }
    else {

        return false;
    }
}



function ExportToExcel() {
    debugger;
    var month = $('#Month').val();
    var EsiGroup = $('#ESIGroup').val();
    var monthName = $('#Month option:selected').text();
    var EsiGroupName = $('#ESIGroup option:selected').text();
    var EsiReportListLength = $('#EsiEcrSummary_Count').val();

    if (EsiReportListLength > 0) {
        url = "/PayRoll/ESIReport/ExportToExcel?Month=" + month + "&EsiGroup=" + EsiGroup + "&monthName=" + monthName + "&EsiGroupname=" + EsiGroupName + "&totalrows=" + EsiReportListLength;
        window.location.href = url;

    }
    else {
        swal.fire("No data found to export");
    }
}
