

function fillData() {

    debugger;
    var filter1 = $('#BasicFilter').serialize();

    var temp = filter1;

    var classification = $('#Classification').val();
    var groupby = $('#GroupBy').val();
    var from = $('#From').val();
    var to = $('#To').val();
    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#ReaderLogTable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/TimeOffice/ReaderLogReport/GetReaderLogList",
            "type": "POST",


            "datatype": "json",


            'data':

                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filter1 + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&F_From=' + from + '&F_To=' + to + "&groupbyCode=" + groupby + "&ClassificationCode=" + classification +'&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },

            "dataSrc": function (d) {

                $('#ReaderLogReportList_Count').val(d.recordsTotal);
                $('.dataTables_sizing ').css("display", 'block');

                for (var i = 0, ien = d.data.length; i < ien; i++) {

                    d.data[i].date = _GetFormattedDate(d.data[i].date);
                    //d.data[i].lwd = _GetFormattedDate(d.data[i].lwd);


                }

                return d.data;
            }

        },

        "columns": [

            { "data": "employeeID"},
            { "data": "employeeName" },
            { "data": "designation"},
            { "data": "department", },
            { "data": "location", },
            { "data": "punchid", },
            { "data": "date", },
            { "data": "time", },
            { "data": "status" },
            { "data": "readerName" },
            { "data": "readerType" },
 

        ],

        /* "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',*/



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





function onGroupByChange(sender, id) {
    debugger;
    var cid = $("#GroupBy").val();
    if (cid == "") {
        $(sender).addClass('is-invalid');
        $("#" + id + "").addClass('error-message');
        $("#" + id + "").html("This field is required");
    }
    else {
        $(sender).removeClass('is-invalid');
        $("#" + id + "").removeClass('error-message');
        $("#" + id + "").html("");
    }
    $.getJSON("/TimeOffice/ReaderLogReport/GetClassification", { id: cid }, function (data) {
        debugger;
        $('#Classification option').remove();
        //console.log(data);
        if (cid == 1) {
            $('#Classification').append('<option value="0">ALL</option');
        }
        for (var i = 0; i < data.length; i++) {
            debugger;
            $('#Classification').append('<option value="' + data[i].value + '">' + data[i].text + '</option');
        }

    });
//    fillData();
}

function getList(sender) {
    debugger;

    var url = "/TimeOffice/ReaderLogReport/GetList";
    var classification = $('#Classification').val();
    var grooupby = $('#GroupBy').val();
    var from = $('#From').val();
    var to = $('#To').val();
    if (classification != "" && grooupby != "" && from != "" && to != "") {
        $('#ReaderLogListPartial').load(url, { From: from, To: to, groupbyCode: grooupby, ClassificationCode: classification }, function () { });
        //url = '/TimeOffice/ReaderLogReport/GetList?From=' + from + '&To=' + to + '&groupbyCode=' + grooupby + '&ClassificationCode=' + classification;
        //window.location.href = url;
    }
}

function ApplyFilter(mode) {
    debugger
    if (mode == 'apply') {
        var classification = $('#Classification').val();
        var grooupby = $('#GroupBy').val();
        var from = $('#From').val();
        var to = $('#To').val();
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

    }
}

function ExportToExcel() {
    debugger;
    var classification = $('#Classification').val();
    var grooupby = $('#GroupBy').val();
    var from = $('#From').val();
    var to = $('#To').val();
    var classificationName = $('#Classification option:selected').text();
    var grooupbyName = $('#GroupBy option:selected').text();
    //if (!($('#ReaderLogTable').length))
    //    swal.fire("No data found to export");
    var TableLength = $('#ReaderLogReportList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/ReaderLogReport/ExportToExcel?From=' + from + '&To=' + to + "&groupbyCode=" + grooupby + "&ClassificationCode=" + classification + "&ClassificationName=" + classificationName + "&GrooupbyName=" + grooupbyName + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function validateDate(val, id) {
    debugger;
    /*alert($(val).val());*/
    var from = $('#From').val();
    var to = $('#To').val();
    date1 = new Date(from);
    date2 = new Date(to);
    if (id == "from") {

        if (date1 > date2) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("From Date should be Less than To Date");
            return false;
        }

        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }
    }

    else {

        if (date1 > date2) {
            $(val).addClass('is-invalid');
            $("#" + id + "").addClass('error-message');
            $("#" + id + "").html("To Date should be greater than From Date");
            return false;
        }


        else {
            $(val).removeClass('is-invalid');
            $("#" + id + "").removeClass('error-message');
            $("#" + id + "").html("");
            return true;
        }

    }




}