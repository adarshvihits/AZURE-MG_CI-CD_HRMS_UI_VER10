var TabIndex = 1;

var dataTable;
//function GetList() {

//    var fromdate = $('#FromDate').val();
//    var todate = $('#ToDate').val();
//     TabIndex = $('input[name="BasedOn"]:checked').val();
//    var UserCode = $('#UserDropdown').val();
//    var MenuCode = $('#MenuDropdown').val();

//    if (TabIndex == 1) {
//        $('.usrDropdown').removeClass('hide');
//        $('.mnDropdown').addClass('hide');       
//    }
//    else {
//        $('.usrDropdown').addClass('hide');
//        $('.mnDropdown').removeClass('hide');       
//    }

//    if (fromdate != '' && todate != '' && ((TabIndex == 1 && UserCode != '') ||(TabIndex==2 && MenuCode != ''))) {
//        $.ajax({
//            type: 'POST',
//            url: '/Recruitment/RecruitmentTransactionHistory/GetList?Tab=' + TabIndex + '&Fromdate=' + fromdate + '&Todate=' + todate + '&UserCode=' + UserCode + '&MenuCode=' + MenuCode,
//            dataType: 'html',           
//            success: function (result) {
//                debugger
//                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
//                    window.location.reload();
//                }
//                else {
//                    $('#ListView').html("");
//                    $('#ListView').html(result);
//                    if (TabIndex == 1) {                       
//                        $('.menuwise').addClass('hide');
//                    }
//                    else {                       
//                        $('.menuwise').removeClass('hide');
//                    }
//                }
//            },
//            error: function (xhr, textStatus, errorThrown) {
//                debugger
//                document.body.id = "kt_body";
//                $('body').html(xhr.responseText);
//            }
//        });
//    }

//}

function ExportToExcel() {

    debugger;
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    var TabIndex = $('input[name="BasedOn"]:checked').val();
    var UserCode = $('#UserDropdown').val();
    var MenuCode = $('#MenuDropdown').val();
    var tableLength = $('#RecruitmentTrnHistory_Count').val();

    if (tableLength > 0) {
        url = '/Recruitment/RecruitmentTransactionHistory/ExportToExcel?Tab=' + TabIndex + '&Fromdate=' + fromdate + '&Todate=' + todate + '&UserCode=' + UserCode + '&MenuCode=' + MenuCode + "&length=" + tableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }

}


function GetList() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;
    var fromdate = $('#FromDate').val();
    var todate = $('#ToDate').val();
    TabIndex = $('input[name="BasedOn"]:checked').val();
    var UserCode = $('#UserDropdown').val();
    var MenuCode = $('#MenuDropdown').val();



    if (TabIndex == 1) {
        $('.usrDropdown').removeClass('hide');
        $('.mnDropdown').addClass('hide');
    }
    else if (TabIndex == 2) {
        $('.usrDropdown').addClass('hide');
        $('.mnDropdown').removeClass('hide');
    }
    else {
        $('.usrDropdown').addClass('hide');
        $('.mnDropdown').addClass('hide');
    }


    //if (fromdate != '' && todate != '' && ((TabIndex == 1 && UserCode != '') || (TabIndex == 2 && MenuCode != ''))) {


        /*  $('#JoiningReportTable').html('');*/
         dataTable = $("#RecruitmentTrnHistoryTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/Recruitment/RecruitmentTransactionHistory/GetList",
                "type": "POST",

                "datatype": "json",


                'data':

                    function (d) {
                        temp = '';
                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();

                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Tab=' + TabIndex + '&Fromdate=' + fromdate + '&Todate=' + todate + '&UserCode=' + UserCode + '&MenuCode=' + MenuCode+ '&dummy=dummy';
                        d.filter = temp;
                        return JSON.stringify(d);
                    },
                "dataSrc": function (d) {


                    $('#RecruitmentTrnHistory_Count').val(d.recordsTotal);
                    if (TabIndex == 1) {
                       /* $('.menuwise').addClass('hide');*/
                        $('.Recruitment_trans_list').addClass('Recruitment_userwise_list');
                        $('.Recruitment_userwise_list').removeClass('Recruitment_trans_list');
                    }
                    else {                       
                       /* $('.menuwise').removeClass('hide');*/
                        $('.Recruitment_userwise_list').addClass('Recruitment_trans_list');
                        $('.Recruitment_trans_list').removeClass('Recruitment_userwise_list');
                    }
                   
                    return d.data;
                }

            },

            "columns": [

                { "data": "refNo"  },
                { "data": "candidateName"},
                { "data": "jobCode"},
                { "data": "jobTitle" },
                { "data": "userID" },
                { "data": "userName" },
                { "data": "transaction"},
                { "data": "actions" },
                { "data": "date" },
                { "data": "time" },


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

        $.fn.DataTable.ext.pager.numbers_length = 5;

    //    if (salVisible == 0) {
    //        let column7 = dataTable.column(7);
    //        column7.visible(false);
    //}
    debugger;
        if (TabIndex == 1) {
            let column4 = dataTable.column(4)
            column4.visible(false);
            let column5 = dataTable.column(5)
            column5.visible(false);
        }
        else {
            let column4 = dataTable.column(4)
            column4.visible(true);
            let column5 = dataTable.column(5)
            column5.visible(true);
        }
        

   
   
   
}
