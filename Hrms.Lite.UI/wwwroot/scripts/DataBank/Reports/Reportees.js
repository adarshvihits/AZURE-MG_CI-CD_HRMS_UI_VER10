


function getList() {
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var temp = filter1;
    var Authority = $('#Authority_Code').val();
    var Level = $('#RequestLevel_Code').val();
    var Type = $('#RequestType_Code').val();

    var dataTable = $("#ReporteesTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/DataBank/Reportees/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + Type + '&Level=' + Level  + '&Authority=' + Authority + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                $('#ReporteesList_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                   
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    
                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeHeader.employeeId" },
            { "data": "employeeHeader.employeeName" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "doj" },
            { "data": "type" },
            { "data": "level" },
            { "data": "authorityName" },


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
function onReqTypeChange(sender, id) {
    debugger;
 


    $.getJSON("/DataBank/Reportees/GetLevelDropDown", { TypeCode: $(sender).val() }, function (data) {
        $('#RequestLevel_Code option').remove();
        $('#RequestLevel_Code').append('<option value="0">All</option');

        for (var i = 0; i < data.length; i++) {
            $('#RequestLevel_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        getList();
    });

}
function onReqLevelChange(sender, id) {
    debugger;

    var Type = $('#RequestType_Code').val();    

    $.getJSON("/DataBank/Reportees/GetAuthorityDropDown", { LevelCode: $(sender).val(), TypeCode: Type },function (data) {
        console.log(data);
        $('#Authority_Code option').remove();

        $('#Authority_Code').append('<option value>All</option'); 
        debugger;
        for (var i = 0; i < data.length; i++) {
            $('#Authority_Code').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }
        $('.selectpicker').selectpicker('refresh');
        getList();
    });
}
function ApplyFilter(mode) {
   
    var tabIndex = $('#Tabindex').val();
    var search = $('#SearchId').val();
    //$("#Tabindex").val(currentTab);
    $("#SearchId").val($('#FilterList_Code').val())
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            getList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            /*  showSpinner();*/
            getList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }
    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#Optional_Filter_EmploymentStatus_Code').val(0);
        $('#filterForm').removeClass('filter-apply');
        getList();
        //    CloseSlider('basicFilter');
    }
}


//function ExportToExcel() {
//    debugger;

//    var Authority = $('#Authority_Code').val();
//    var AuthorityName = $('#Authority_Code option:selected').text();
//    var Level = $('#RequestLevel_Code').val();
//    var Type = $('#RequestType_Code').val();
//    var tableLength = $('#ReporteesList_Count').val();
//    if (tableLength > 0) {
//        url = "/DataBank/Reportees/ExportToExcel?Type=" + Type + "&Level=" + Level + "&Authority=" + Authority + "&AuthorityName=" + AuthorityName + "&totRecords=" + tableLength;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }
//}




function ExportToExcel() {
    debugger;
    var filterdata = $('#BasicFilter').serialize();
    //var ConfirmationValue = $('input[name="option"]:checked').val();
    var Authority = $('#Authority_Code').val();
    var AuthorityName = $('#Authority_Code option:selected').text();
    var Level = $('#RequestLevel_Code').val();
    var LevelName = $('#RequestLevel_Code option:selected').text();
    var Type = $('#RequestType_Code').val();
    var TypeName = $('#RequestType_Code option:selected').text();
    var tableLength = $('#ReporteesList_Count').val();

    /*    var Type = $('#Type').val();*/



    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&Level=' + Level + '&Authority=' + Authority + '&AuthorityName=' + AuthorityName + '&LevelName=' + LevelName + '&TypeName=' + TypeName+ '&totRecords=' + tableLength;
  
    if (tableLength > 0) {
        url = '/DataBank/Reportees/ExportToExcel?' + PARAM ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
