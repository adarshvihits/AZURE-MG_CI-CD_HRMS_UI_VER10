


function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var month = $('#Month').val();
    var pfgroup = $('#PFGroup').val();
    var format = $('#Format').val();



    var dataTable = $("#PfEcrReportTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/PayRoll/PfEcrReport/GetPfEcrList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';

                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();


                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&SearchId=' + searchValue + '&Month=' + month + '&PFGroup=' + pfgroup + '&format=' + format + '&dummy=dummy';
                    d.filter = temp;


                    return JSON.stringify(d);
                },




            "dataSrc": function (d) {

                $('#PfEcrSummary_Count').val(d.recordsTotal);
                for (var i = 0, ien = d.data.length; i < ien; i++) {

                }

                return d.data;
            }

        },

        "columns": [

            { "data": "uan"},
            { "data": "memberName"},
            { "data": "grossWage"},
            { "data": "epfWage" },
            { "data": "epsWage"},
            { "data": "edliWage"},
            { "data": "epfContriRemitted"},
            { "data": "epsContriRemitted"},
            { "data": "epfEpsDiffRemitted"},
            { "data": "ncpdays"},
            { "data": "refundAdvance"},



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
     /*   dataTable.search('').draw();*/
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

    debugger
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();
    var format = $('#Format').val();

    if (Month !== '' && PFGroup !== '' && format != '') {
        debugger
        showSpinner();
        $.ajax({

            type: 'GET',
            url: '/PayRoll/PfEcrReport/GetList',
            dataType: 'html',
            data: { Month: Month, PFGroup: PFGroup, format: format },
            success: function (result) {
                debugger
                if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                    window.location.reload();
                }

                else {

                    $('#PFEcrReportList').html("");
                    $('#PFEcrReportList').html(result);
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


function ExportToExcel() {
    debugger;
    var format = $('#Format').val();
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();
    var MonthName = $('#Month option:selected').text();
    var PFGroupName = $('#PFGroup option:selected').text();

    var TableLength = $('#PfEcrSummary_Count').val();
    if (TableLength > 0) {
        url = "/PayRoll/PfEcrReport/ExportToExcel?Month=" + Month + "&PFGroup=" + PFGroup + "&MonthName=" + MonthName + "&PFGroupName=" + PFGroupName + "&format=" + format + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function onFormatChange(sender) {
    debugger;
    var Format = $('#Format').val();
    $('#buttonID').val("Generate " + Format);
    $('.Name').html(Format);
    fillData();
    showButton();
}

function showButton() {
    debugger;
    document.getElementById('buttonID').style.visibility = 'Visible';
    
}



function OnGenerateButtonClick() {
    debugger;
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();
    var format = $('#Format').val();


    if (validatePFECR()) {
        if ($('#PfEcrSummary_Count').val()> 0) {
            var url = "/PayRoll/PfEcrReport/Export?Month=" + Month + "&PFGroup=" + PFGroup + "&format=" + format + "&totCount=" + $('#PfEcrSummary_Count').val();
            window.location.href = url;
        }
        else {
            swal.fire('No data found for Generate ECR');
        }
    }

}

function ApplyFilter(mode) {
    debugger;
    var Month = $('#Month').val();
    var PFGroup = $('#PFGroup').val();
    var format = $('#Format').val();


    if (mode == 'apply') {
        if (filterApplicable >= 1) {
            CloseSlider('basicFilter');
            fillData();
            //$.ajax({

            //    type: 'POST',
            //    url: '/PayRoll/PfEcrReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize() + "&Month=" + Month + "&PFGroup=" + PFGroup + "&format=" + format,
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }

            //        else {
            //            $('#PFEcrReportList').html("");
            //            $('#PFEcrReportList').html(result);

            //            $('#PFEcrReportList').removeClass('hide');
            //        }

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
            //    url: '/PayRoll/PfEcrReport/ApplyFilter',
            //    dataType: 'html',
            //    data: $('#BasicFilter').serialize() + "&Month=" + Month + "&PFGroup=" + PFGroup + "&format=" + format,
            //    success: function (result) {
            //        debugger
            //        CloseSlider('basicFilter');
            //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
            //            window.location.reload();
            //        }

            //        else {
            //            $('#PFEcrReportList').html("");
            //            $('#PFEcrReportList').html(result);

            //            $('#PFEcrReportList').removeClass('hide');
            //        }

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
        //    url: '/PayRoll/PfEcrReport/ApplyFilter',
        //    dataType: 'html',
        //    data: $('#BasicFilter').serialize() + "&Month=" + Month + "&PFGroup=" + PFGroup + "&format=" + format,
        //    success: function (result) {
        //        debugger


        //        if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
        //            window.location.reload();
        //        }

        //        else {
        //            $('#PFEcrReportList').html("");
        //            $('#PFEcrReportList').html(result);

        //            $('#PFEcrReportList').removeClass('hide');
        //            //$('#rewardsListPartial').addClass('hide');
        //        }
        //    },


        //    error: function (xhr, textStatus, errorThrown) {
        //        swal.fire(xhr.responseText);
        //    }
        //});


    }
}

function validatePFECR() {
    var i = 0;
    if (!validateById('#Month', 'month')) {
        i++;
    }
    if (!validateById('#PFGroup', 'pfgroup')) {
        i++;
    }
    if (!validateById('#Format', 'format')) {
        i++;
    }
    if (i == 0)
        return true;
    else
        return false;
}