function GetList() {
    var Type = $('#PorEtype').val();
    var validate = 0;
    if (Type == 1) {
        var PolicyOrEmpCode = $('#PolicyOrEmp').val();
        $('.policy').removeClass('hide');
        $('.selectpickerDiv').addClass('hide');
        $('.filter-button').removeClass('hide');
    }
    else {
        var PolicyOrEmpCode = $('.selectpicker').val();
        $('.policy').addClass('hide');
        $('.selectpickerDiv').removeClass('hide');
        $('.filter-button').addClass('hide');
    }
    if (Type == 2 && PolicyOrEmpCode == '')
        validate = 1;
    var AcceptanceCode = $('#Acc').val();
    if (Type != '' && validate!=1  && AcceptanceCode != '') {
        //showSpinner();

        fillData(Type, PolicyOrEmpCode, AcceptanceCode);

        //$.ajax({
        //    type: 'POST',
        //    url: '/DataBank/HRPolicyAcceptanceReport/GetList?Type=' + Type + '&PolicyOrEmpCode=' + PolicyOrEmpCode + '&AcceptanceCode=' + AcceptanceCode,
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize(),
        //    success: function (result) {
        //        debugger
        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }
        //        else {
        //            $('#ListView').html("");
        //            $('#ListView').html(result);
        //        }
        //        hideSpinner();


        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        debugger
        //        document.body.id = "kt_body";
        //        $('body').html(xhr.responseText);
        //        hideSpinner();

        //    }
        //});
    }
    
}

function fillDataOld() {

    debugger;
    var filter = $('#BasicFilter').serialize();


    var PolicyCode = $('#PolicyCode').val();

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#HrPolicyAcceptanceTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/HRPolicyAcceptanceReport/GetHrPolicyAcceptanceDetails",
            "type": "POST",


            "datatype": "json",


            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&PolicyCode=' + PolicyCode + '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#HrpolicyAcceptanceDetails_Count').val(d.recordsTotal);


                console.log(d.data);
                return d.data;
            }

        },
        "columns": [
            { "data": "employeeID", "width": "10%" },
            { "data": "employee", "width": "10%" },
            { "data": "policyName", "width": "10%" },
            { "data": "category", "width": "8%" },
            { "data": "visibleFrom", "width": "16%" },
            { "data": "visibleTo", "width": "10%" },
            { "data": "designation", "width": "8%" },
            { "data": "department", "width": "8%" },
            { "data": "location", "width": "8%" },
            { "data": "acceptedOn", "width": "10%" },
            { "data": "time", "width": "10%" },


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
        //dataTable.search('').draw();
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
function fillData(Type, PolicyOrEmpCode, AcceptanceCode) {

    debugger;
    var filter = $('#BasicFilter').serialize();


    //var PolicyCode = $('#PolicyCode').val();

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#PolicyAcceptanceTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/HRPolicyAcceptanceReport/GetHrPolicyAcceptanceList",
            "type": "POST",


            "datatype": "json",


            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&PolicyOrEmpCode=' + PolicyOrEmpCode + '&Type=' + Type + '&AcceptanceCode=' + AcceptanceCode+ '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

               // $('#HrpolicyAcceptanceDetails_Count').val(d.recordsTotal);
                $('#PolicyacceptanceReportList_Count').val(d.recordsTotal);
                console.log(d.data)
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                    debugger
                    d.data[i].visibleFrom = _GetFormattedDate(d.data[i].visibleFrom);
                    d.data[i].visibleTo = _GetFormattedDate(d.data[i].visibleTo);
                    //if (d.data[i].accepDate != '' && d.data[i].accepDate != null)
                    //    d.data[i].accepDate = _GetFormattedDate(d.data[i].accepDate.split('T')[0]) +' & '+ d.data[i].accepDate.split('T')[1];
                }
                return d.data;
            }

        },
        "columns": [
            { "data": "employeeID" },
            { "data": "employeeName" },
            { "data": "designation"},
            { "data": "department" },
            { "data": "location" },
            { "data": "policyCategory" },
            { "data": "policy" },
            { "data": "visibleFrom" },
            { "data": "visibleTo"},
            { "data": "accepStatus" },
            { "data": "accepDate" },


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
        //dataTable.search('').draw();
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
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            GetList();
            CloseSlider('basicFilter');
            $('#filterForm').addClass('filter-apply');
        }

    }
    else {
        filterApplicable = 1;
        $('.reset').val('');
        $('#filterForm').removeClass('filter-apply');
        GetList();

    }
}
function ExportToExcel() {
    debugger;
    var filter = $('#BasicFilter').serialize();
    var Type = $('#PorEtype').val();
    if (Type == 1) {
        var PolicyOrEmpCode = $('#PolicyOrEmp').val();
        var PolicyName = $('#PolicyOrEmpCode option:selected').text();
    }
    else {
        var PolicyOrEmpCode = $('.selectpicker').val();
        var PolicyName = $('.selectpicker option:selected').text();
    }   
    var AcceptanceCode = $('#Acc').val();

    var PARAM = 'data=' + filter + '&PolicyOrEmpCode=' + PolicyOrEmpCode + '&PolicyName=' + PolicyName + '&Type=' + Type + '&AcceptanceCode=' + AcceptanceCode;
    var TableLength = $('#PolicyacceptanceReportList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/HRPolicyAcceptanceReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}
function ExportToExcelOld() {
    debugger;
    var filter = $('#BasicFilter').serialize();

    var PolicyCode = $('#PolicyCode').val();
    var PolicyName = $('#PolicyCode option:selected').text();

    var PARAM = 'data=' + filter + '&PolicyCode=' + PolicyCode + '&PolicyName=' + PolicyName;
    var TableLength = $('#HrpolicyAcceptanceDetails_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/HRPolicyAcceptanceReport/ExportToExcelOld?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}