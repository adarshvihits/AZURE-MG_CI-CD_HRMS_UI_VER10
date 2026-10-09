
var salVisible;



function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var fromDate = $('#From').val();
    var toDate = $('#To').val();

    if (fromDate != '' && toDate != '') {

    
    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#SeparationTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/SeparationReport/GetSeparationList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&dummy=dummy';



                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#SeparationReportList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);
                    d.data[i].resignationDate = _GetFormattedDate(d.data[i].resignationDate);

                    debugger
                    if (d.data[i].absoluteUri != "" && d.data[i].absoluteUri != null)
                        d.data[i].employeeID = '<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <div class="name-post"> <a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=SeparationReport&BreadcrumbName=Separation Report List">' + d.data[i].employeeName + '</a> <div>(' + d.data[i].employeeID + ')' + ' </div> </div></div>'
                    else
                        d.data[i].employeeID = '<div class="d-flex align-items-center" ><div class="employee-profile-img" style=" background-color: #989ca9; color: #fff; display: flex; text-align: center; justify-content: center; align-items: center;"><span class="profile-name-leter-chg" style = "font-size:23px;">' + d.data[i].employeeName.substring(0, 1) + '</span></div > <div class="name-post"> <a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=SeparationReport&BreadcrumbName=Separation Report List">' + d.data[i].employeeName + '</a> <div>(' + d.data[i].employeeID + ')' + ' </div> </div></div>'



                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID" },
            { "data": "designation" },
            { "data": "department" },
            { "data": "location" },
            { "data": "category" },
            { "data": "doj"},
            { "data": "resignationDate" },
            { "data": "lwd" },
            { "data": "yos" },
            { "data": "grossPay" },
            { "data": "separationType" },
            { "data": "remarks" },
            { "data": "acceptanceRemarks" },
            { "data": "relievingRemarks" },
            { "data": "workHandoverDetails" },
            { "data": "reason"},

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
        debugger
    $.fn.DataTable.ext.pager.numbers_length = 5;
        if (salVisible == 0) {
            let column9 = dataTable.column(9);
            column9.visible(false);
        }

}




}






function onGroupByChange() {
    var from = $('#From').val();
    var to = $('#To').val();
    if (from != '' && to != '') {
        var url = "/Databank/SeparationReport/GroupSummary";
        $('#GroupSummaryListDiv').load(url, { GrpByCode: $('#GroupBy_Code').val(), GrpByName: $('#GroupBy_Code option:selected').text() }, function () { })
    }
    else {
        return false;   
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
        $('#filterForm').removeClass('filter-apply');
        fillData();
        CloseSlider('basicFilter');
    }
}


function ExportToExcel() {

    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var totalrecords = $('#SeparationReportList_Count').val();

    
    if (totalrecords > 0) {
        url = "/DataBank/SeparationReport/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&totalRecords=" + totalrecords;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function GroupSummaryExportToExcel() {


    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var id = $('#GroupBy_Code').val();
    var type = $('#GroupBy_Code option:selected').text();
    var tableLength = document.getElementById("GroupsummaryTable").rows.length - 1;
    if (tableLength > 1) {
        url = "/DataBank/SeparationReport/GroupSummaryExportToExcel?id=" + id + "&fromDate=" + fromDate + "&toDate=" + toDate + "&type=" + type;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }



}
