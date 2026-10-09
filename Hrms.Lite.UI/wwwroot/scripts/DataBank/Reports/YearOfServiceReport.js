function fillData() {

    debugger;
    var filterdata = $('#BasicFilter').serialize();

    //var ConfirmationValue = $('input[name="option"]:checked').val();
    var Type = $('#Type').val();
    var listCategory = $('input[name="option"]:checked').val();

    /*  $('#JoiningReportTable').html('');*/
    var dataTable = $("#yearofservicetable").DataTable({

        "pagingType": 'full_numbers',
        "destroy": 'true',
        "ajax": {
            "url": "/Databank/YearOfServiceReport/GetYearOfServiceReport",
            "type": "POST",


            "datatype": "json",


            'data':
                function (d) {
                    temp = '';
                    var start = d.start;

                    var searchValue = $('#dataTableSearch').val();
                    temp = filterdata + '&draw=' + d.draw + '&start=' + d.start + '&length=' + d.length + '&searchValue=' + searchValue + '&Type=' + Type + '&listCategory=' + listCategory   +  '&dummy=dummy';
                    d.filter = temp;

                    return JSON.stringify(d);
                },
            "dataSrc": function (d) {

                $('#GetYearOfServiceReport_Count').val(d.recordsTotal);
               
                return d.data;
            }

        },
        "columns": [

            { "data": "employeeID", "width": "10%" },
            { "data": "employeeName", "width": "40%" },
            { "data": "groupByName", "width": "25%" },
            { "data": "yos", "width": "25%" }

          



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




function GetList() {
    debugger;
    showSpinner();
   
    var Type = $('#Type').val();
    var listCategory = $('input[name="option"]:checked').val();
/*    var ConfirmationValue = $('input[name="option"]:checked').val();*/
    debugger;
    $.ajax({

        type: 'GET',
        url: '/Databank/YearOfServiceReport/GetYearOfServiceReport?Type='+ Type +'&listCategory=' + listCategory,
        dataType: 'html',
        data: $('#BasicFilter').serialize(),

        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#YearOfServiceReportList').html("");
                $('#YearOfServiceReportList').html(result);
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
    var filterdata = $('#BasicFilter').serialize();
    //var ConfirmationValue = $('input[name="option"]:checked').val();
    //var Type = $('input[name="option"]:checked').val();

/*    var Type = $('#Type').val();*/
    var Type = $('#Type').val();
    var listCategory = $('input[name="option"]:checked').val();

    var TypeName = $('#Type option:selected').text();

    debugger;
    var PARAM = 'data=' + filterdata + '&Type=' + Type + '&listCategory=' + listCategory + '&TypeName=' + TypeName ;


/*    var PARAM = 'data=' + filterdata + '&Type=' + Type ;*/
    var TableLength = $('#YearOfServiceReportList_Count').val();
    if (TableLength > 0) {
        url = '/DataBank/YearOfServiceReport/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}




function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("yearofservicetable");
    tr = table.getElementsByTagName("tr");
    tbody = table.getElementsByTagName("tbody");
    $('#noDataImg').remove();
    var noOfRows = tr.length;
    var show = true;
    var spannedRows = 0;
    for (i = 0; i < tr.length; i++) {
        debugger;
        td = tr[i].getElementsByTagName("td")[1];
        //if (noOfRows > 1) {
        //    $('#noDataImg').remove();
        //}
        if (spannedRows > 0) {
            if (show)
                tr[i].style.display = "";
            else {
                tr[i].style.display = "none";
                noOfRows--;
            }

            spannedRows--;
        } else if (td) {
            if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                tr[i].style.display = "";
            } else {
                tr[i].style.display = "none";
                noOfRows--;
            }
            let rs = td.getAttribute("rowspan");
            //console.log("rs = " + rs);
            if (rs && rs > 1) {
                show = td.innerHTML.toUpperCase().indexOf(filter) > -1;
                spannedRows = rs - 1;
            }
        }
    }
    //alert(noOfRows);
    if (noOfRows == 1) {
        // $(table).find(tbody).append("<div  id='noDataImg' style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div >");
        $(table).find(tbody).append("<tr id='noDataImg' class='exit-page'><td><div  style='text-align:center; display:flex; align-items:center; justify-content:center;height:45vh;'><img src='/images/nodata.png'/></div ></td></tr>");
        // $(table).find(tbody).append("<tr id='noDataImg'><td>" + img + "</td></tr>");
    }
}






function onSearchClick() {
    if (searchClick == 0) {
        searchClick = 1;
        $('#dataTableSearch').addClass('field-width-big');
        CursorFocusOnSearch();
    }
    else {
        searchClick = 0;
        $('#dataTableSearch').removeClass('field-width-big');
    }
}