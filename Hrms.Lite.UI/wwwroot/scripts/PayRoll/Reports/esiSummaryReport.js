

function fillData() {

    debugger;

    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;


    var Month = $('#Month').val();
    var ESIGroup = $('#ESIGroup').val();


    /* if (Allowance != '') {*/
    var dataTable = $("#EsisummeryReporttable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/ESISummaryReport/GetEsiReportList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';




                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&MonthCode=' + Month + "&ESIGroupCode=" + ESIGroup+'&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#ESISummary_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {
                 
                    //d.data[i].deduction = '<span id="editbt_' + i + '"><a onclick = "onEditClick(' + i + ')"  data-bs-toggle="tooltip" title = "Edit" type = "button"class="btn btn-icon btn-edit icon-only" ><i class="icon-xl fas fa-pencil-alt"></i></a></span><span id = "updateCancel_' + i + '" class="hide"><a type = "button" data-bs-toggle="tooltip" title = "Update" class="btn btn-icon btn-update icon-only " onclick = "onUpdateClick(' + i + ',' + "'" + d.data[i].employeeGI + "'" + ')"> <i class="icon-xl fas fa-sync-alt"></i> </a><a type = "button" data-bs-toggle="tooltip" title = "Cancel" class="btn btn-icon btn-cancel icon-only " onclick = "onCancelClick(' + i + ')"> <i class="icon-xl fas fa-times"></i></a></span>'

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "slNo", "width": "7%" },
            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "11%" },
            { "data": "esiNumber", "width": "10%" },
            { "data": "actualESISalary", "width": "11%" },
            { "data": "esiSalary", "width": "9%" },
            { "data": "paidDays", "width": "8%" },
            { "data": "employeeContribution", "width": "13%" },
            { "data": "employerContribution", "width": "11%" },
            { "data": "total", "width": "10%" },
             


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





    //}
    //else {
    //    return false;
    //}

}




function getList() {

    debugger
    var Month = $('#Month').val();
    var ESIGroup = $('#ESIGroup').val();
    /* showSpinner();*/
    if (Month != '' && ESIGroup != '') {
        //var url = "/PayRoll/ESISummaryReport/GetList?MonthCode=" + Month + "&ESIGroupCode=" + ESIGroup;
        //$('#ESISummaryReportlist').load(url, function () { /*hideSpinner();*/ });
        $.ajax({

            type: 'GET',
            url: '/PayRoll/ESISummaryReport/GetList',
            dataType: 'html',
            data: { MonthCode: Month, ESIGroupCode: ESIGroup },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#ESISummaryReportlist').html("");
                    $('#ESISummaryReportlist').html(result);
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
    else {

        return false;
    }
}

function ApplyFilter(mode) {
    debugger
    var Month = $('#Month').val();
    var ESIGroup = $('#ESIGroup').val();
    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayRoll/ESISummaryReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize() + "&MonthCode=" + Month + "&ESIGroupCode=" + ESIGroup,
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }

            //        else {
            //            $('#ESISummaryReportlist').html("");
            //            $('#ESISummaryReportlist').html(result);

            //            $('#ESISummaryReportlist').removeClass('hide');
            //        }
            //        //$('#rewardsListPartial').addClass('hide');               
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        swal.fire(xhr.responseText);
            //    }
            //});
            fillData();
            CloseSlider('basicFilter');
            $('#filterForm').removeClass('filter-apply');
        }
        else {
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayRoll/ESISummaryReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize() + "&MonthCode=" + Month + "&ESIGroupCode=" + ESIGroup,
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }

            //        else {
            //            $('#ESISummaryReportlist').html("");
            //            $('#ESISummaryReportlist').html(result);

            //            $('#ESISummaryReportlist').removeClass('hide');
            //        }
            //        //$('#rewardsListPartial').addClass('hide');               
            //    },


            //    error: function (xhr, textStatus, errorThrown) {
            //        swal.fire(xhr.responseText);
            //    }
            //});
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

    }
}

function ExportToExcel() {
    debugger;
    var Month = $('#Month').val();
    var ESIGroup = $('#ESIGroup').val();
    var EsiGpName = $('#ESIGroup option:selected').text();
    var Monthname = $('#Month option:selected').text();
    if (validateDropdowns()) {

        var TableLength = $('#ESISummary_Count').val();
        if (TableLength > 0) {
            url = "/PayRoll/ESISummaryReport/ExportToExcel?MonthName=" + Monthname + '&ESiName=' + EsiGpName + "&Month=" + Month + "&ESiCode=" + ESIGroup + '&TotalRows=' + TableLength;
            window.location.href = url;
        }
        else {
            swal.fire("No data found to export");
        }
    }
}

function validateDropdowns() {
    debugger;
    var i = 0;
    if (!validateById('#Month', 'month')) {
        i++;
    }
    //if (!validateById('#ESIGroup', 'esi')) {
    //    i++;
    //}

    if (i == 0)
        return true;
    else
        return false;

}
