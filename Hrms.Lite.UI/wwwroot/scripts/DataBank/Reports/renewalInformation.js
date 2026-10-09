var salVisible;

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var type = $('#RenewalInfoTab').val();

    if (fromDate != '' && toDate != '') {
    
    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#RenewalTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/RenewalInformation/GetList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();






                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + fromDate + '&F_To=' + toDate + '&type=' + type + '&dummy=dummy';
                    d.filter = temp;
                    d.type = type;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {




                $('#RenewalInformationList_Count').val(d.recordsTotal);

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    //d.data[i].employeeid = '<div class="d-flex align-items-center" ><div class="img-report"><img src="' + d.data[i].absoluteUri + '"></div> <a href="/DataBank/EmployeeProfile/GetEmployeeProfileDetails?EmployeeGI=' + d.data[i].employeeGI + '&Breadcrumb=JoiningReport&BreadcrumbName=Joining Report List">' + d.data[i].employeeName + '(' + d.data[i].employeeid + ')' + '</a> </div> </div></div></a> </div>'
                    d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                    d.data[i].dateofExpiry = _GetFormattedDate(d.data[i].dateofExpiry);




                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeid", "width": "10%" },
            { "data": "employeeName", "width": "17%" },
            { "data": "designation", "width": "9%" },
            { "data": "department", "width": "9%" },
            { "data": "location", "width": "9%" },
            { "data": "category", "width": "9%" },
            { "data": "doj", "width": "10%" },
            { "data": "yos", "width": "9%" },
            { "data": "dateofExpiry", "width": "9%" },
            { "data": "grossPay", "width": "9%" },


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

        if (salVisible == 0) {
            let column9 = dataTable.column(9);
            column9.visible(false);
        }



}

}












//function getList(sender) {
//    debugger;

//    var fromDate = $('#From').val();
//    var toDate = $('#To').val();
//    var type = $('#RenewalInfoTab').val();

//    if (fromDate !== '' && toDate !== '') {
//        showSpinner();    
//        $.ajax({

//            type: 'GET',
//            url: '/Databank/RenewalInformation/RenewalInformationList',
//            dataType: 'html',
//            data: { fromDate: fromDate, toDate: toDate, type: type },

//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    $('#renewalInfoList').html("");
//                    $('#renewalInfoList').html(result);
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

//        return true;
//    }
//    else {

//        return false;
//    }



//}

function OnSummaryClick() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();

    if (fromDate == '' && toDate == '') {
        debugger;
        swal.fire("Please select From and To date");
        var element = document.getElementById("myDiv");
        element.style.display = "none";
       
    }
    else {




        $.ajax({
            type: 'GET',
            url: "/DataBank/RenewalInformation/GetSummary?fromDate=" + fromDate + "&toDate=" + toDate,
            dataType: 'html',

            success: function (Data) {
                if ($($.parseHTML(Data)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }
                else {
                    var m = $('.bd-example-modal-lg');
                    m.find('.modal-body').html('');
                    m.find('.modal-body').html(Data);
                    m.modal('show');
                    $("#myDiv").css("display", "block");
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                document.body.id = "kt_body";
                $('body').html(xhr.responseText);

            }
        });
    }
}

//function ApplyFilter(mode) {
//    fillData();
//    CloseSlider('basicFilter');
//}



function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
          /*  showSpinner();*/
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        /*showSpinner();*/
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        fillData();
        /*CloseSlider('basicFilter');*/
    }
}

//function ExportToExcel() {
//    debugger;
//    var fromDate = $('#From').val();
//    var toDate = $('#To').val();
//    var due = $('#RenewalInfoTab').val();
//    var dues = $('#RenewalInfoTab option:selected').text();

//    var tableLength = $('#RenewalInformationList_Count').val();
//    if (tableLength > 0) {
//        url = "/DataBank/Renewalinformation/ExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate + "&dues=" + dues + "&due=" + due + "&totRecords=" + tableLength;
//        window.location.href = url;
//    }
//    else {
//        swal.fire("No data found to export");
//    }

//}


function ExportToExcel() {
    debugger;
    debugger;
    var filter = $('#BasicFilter').serialize();

    var fromDate = $('#From').val();
    var toDate = $('#To').val();
    var due = $('#RenewalInfoTab').val();
    var dues = $('#RenewalInfoTab option:selected').text();

    var tableLength = $('#RenewalInformationList_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/Renewalinformation/ExportToExcel?data=" + filter + "&FromDate=" + fromDate + "&toDate=" + toDate + "&dues=" + dues + "&due=" + due + "&totRecords=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }


}
function onSummaryExportToExcelClick() {
    debugger;
    var fromDate = $('#From').val();
    var toDate = $('#To').val();
   
    var tableLength = $('#EmployeeProfileSummary_Count').val();
    if (tableLength > 0) {
        url = "/DataBank/Renewalinformation/SummaryExportToExcel?fromDate=" + fromDate + "&toDate=" + toDate ;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}