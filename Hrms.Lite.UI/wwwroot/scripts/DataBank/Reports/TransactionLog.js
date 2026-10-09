var TabIndex = 1;


function fillData() {
    var dataTable;
    debugger;
    var filter1 = $('#BasicFilter').serialize();
    var Menu = $('#MenuDropdown').val()
    var user = $('#UserDropdown').val()
    var from = $('#FromDate').val()
    var to = $('#ToDate').val()
    var TabIndex = $('input[name="BasedOn"]:checked').val();
  
    debugger
    if (TabIndex == 1) {
        $('.usrDropdown').removeClass('hide');
        $('.mnDropdown').addClass('hide');
      
    }
    else {
        $('.usrDropdown').addClass('hide');
        $('.mnDropdown').removeClass('hide');


    }

    var temp = filter1;

 
    var dataTable = $("#TransactionLogList").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/TransactionLog/GetList",
                "type": "POST",
                "datatype": "json",
                'data':

                    function (d) {
                        temp = '';

                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&Tab=' + TabIndex + '&Fromdate=' + from + '&Todate=' + to + '&UserCode=' + user + '&MenuCode=' + Menu + '&searchValue=' + searchValue + '&dummy=dummy';
                        d.filter = temp;


                        return JSON.stringify(d);
                    },


                "dataSrc": function (d) {

                    $('#TransactionlogList_Count').val(d.recordsTotal);
                    console.log(d.data);

                    for (var i = 0, ien = d.data.length; i < ien; i++) {
                        d.data[i].date = _GetFormattedDate(d.data[i].date);
                       

                        
                      
                    }
                    $('#RecruitmentTrnHistory_Count').val(d.recordsTotal);
                    if (TabIndex == 1) {
                        /* $('.menuwise').addClass('hide');*/
                        $('.Transaction_user_list').addClass('Transaction_usermenu_list');
                        $('.Transaction_usermenu_list').removeClass('.Transaction_user_list');
                    }
                    else {
                        /* $('.menuwise').removeClass('hide');*/
                        $('.Transaction_usermenu_list').addClass('Transaction_user_list');
                        $('.Transaction_user_list').removeClass('.Transaction_usermenu_list');
                        $('.menuwise').removeClass('hide');
                    }
                    return d.data;
                }
        },
            
            "columns": [

                { "data": "employeeID", "width": "10%" },
                { "data": "candidateName", "width": "20%" },
                { "data": "userID", "width": "15%" },
                { "data": "userName", "width": "15%" },
                { "data": "transaction", "width": "15%" },
                { "data": "actions", "width": "15%" },
                { "data": "date", "width": "15%" },
                { "data": "time", "width": "15%" },              


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
            // dataTable.search('').draw();
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
    

    debugger;
    if (TabIndex == 1) {
        let column2 = dataTable.column(2)
        column2.visible(false);
        let column3 = dataTable.column(3)
        column3.visible(false);
    }
    else {
        let column2 = dataTable.column(2)
        column2.visible(true);
        let column3 = dataTable.column(3)
        column3.visible(true);
    }



}

function ExportToExcel() {

    debugger;
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    var TabIndex = $('input[name="BasedOn"]:checked').val();
    var UserCode = $('#UserDropdown').val();
    var MenuCode = $('#MenuDropdown').val();
    var tableLength = $('#TransactionlogList_Count').val();
    var filterdata = $('#BasicFilter').serialize();
    var PARAM = 'data=' + filterdata;
    if (tableLength > 0) {
        url = '/DataBank/TransactionLog/ExportToExcel?' + PARAM+'&Tab='+TabIndex + '&Fromdate=' + fromdate + '&Todate=' + todate + '&UserCode=' + UserCode + '&MenuCode=' + MenuCode + '&TotalRecords=' + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}