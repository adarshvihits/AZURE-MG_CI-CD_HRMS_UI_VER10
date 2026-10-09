


function ValidateDate() {
    debugger;
    var fromDate = $('#TransferFromDate').val();
    var toDate = $('#TransferToDate').val();
    var D1 = new Date(fromDate);
    var D2 = new Date(toDate);
    if (D1 > D2) {
        swal.fire("To date should be greater than From date");
        /*  $("#To").val('');*/



        return false;
    }
    else
        return true;
}


function GetList() {
    debugger;
    var fromDate = $('#TransferFromDate').val();
    var toDate = $('#TransferToDate').val();
    var transferType = $('#EmployeeTransferType').val();
    if (fromDate != '' && toDate != '') {
        var url = "/DataBank/EmployeeTransferReport/GetTransferList?fromDate=" + fromDate+"&toDate=" + toDate + "&transferType=" + transferType;
        $('#listView').load(url, function () { });
    }
}








function fillData() {
    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var fromDate = $('#TransferFromDate').val();
    var toDate = $('#TransferToDate').val();
    var transferType = $('#EmployeeTransferType').val();
    //if (transferType == 2) {
    //    /*   document.getElementById("fromAndtoDates").style.display = "block";*/
    //    $("#fromAndtoDates").css("display", "block");
    //}
    //else if (transferType == 1 || transferType == 0) {
    //    $("#fromAndtoDates").css("display", "none");
    //}

    

    if (transferType == 1) {

        var dataTable = $("#JoiningTransferTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EmployeeTransferReport/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromDate=' + fromDate + '&toDate=' + toDate + '&transferType=' + transferType + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    debugger;

                    $('#EmployeePrimaryInfo_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);


                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "" },
                { "data": "fullName", "width": "" },
                { "data": "designation", "width": "" },
                { "data": "department", "width": "" },
                { "data": "location", "width": "" },
                { "data": "region", "width": "" },
                { "data": "category", "width": "" },
                { "data": "state", "width": "" },
                { "data": "doj", "width": "" },
                { "data": "age", "width": "" },

                { "data": "transfer_Joining", "width": "" },



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
    }

    else {
        var dataTable = $("#employeeTransferTable").DataTable({

            "pagingType": 'full_numbers',
            "destroy": 'true',
            "ajax": {
                "url": "/DataBank/EmployeeTransferReport/GetList",
                "type": "POST",


                "datatype": "json",


                'data':

                    function (d) {
                        debugger;
                        temp = '';




                        var start = d.start;

                        var searchValue = $('#dataTableSearch').val();






                        temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&fromDate=' + fromDate + '&toDate=' + toDate + '&transferType=' + transferType + '&dummy=dummy';
                        d.filter = temp;

                        return JSON.stringify(d);
                    },




                "dataSrc": function (d) {



                    debugger;

                    $('#EmployeePrimaryInfo_Count').val(d.recordsTotal);


                    for (var i = 0, ien = d.data.length; i < ien; i++) {

                        d.data[i].doj = _GetFormattedDate(d.data[i].doj);
                        d.data[i].transferDate = _GetFormattedDate(d.data[i].transferDate);



                    }

                    return d.data;
                }

            },

            "columns": [

                { "data": "employeeID", "width": "" },
                { "data": "fullName", "width": "" },
                { "data": "designation", "width": "" },
                { "data": "department", "width": "" },
                { "data": "location", "width": "" },
                { "data": "region", "width": "" },
                { "data": "category", "width": "" },
                { "data": "state", "width": "" },
                { "data": "doj", "width": "" },
                { "data": "age", "width": "" },
                { "data": "transferDate", "width": "" },
                { "data": "transfer_Joining", "width": "" },



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
    }

        



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






























function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            fillData();
            CloseSlider('basicFilter');
            //$.ajax({

            //    type: 'POST',
            //    url: '/DataBank/EmployeeTransferReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#listView').html("");
            //            $('#listView').html(result);
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
            //    url: '/DataBank/EmployeeTransferReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize(),
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');

            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }
            //        else {
            //            $('#listView').html("");
            //            $('#listView').html(result);
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
        //    url: '/DataBank/EmployeeTransferReport/ApplyFilter',
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#listView').html("");
        //            $('#listView').html(result);
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
    debugger
    var TableLength = $('#EmployeePrimaryInfo_Count').val();
    /* var TableLength = document.getElementById("vacList").rows.length - 1;*/    
    if (TableLength > 0) {
        url = "/DataBank/EmployeeTransferReport/ExportToExcel?TotRecords=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}