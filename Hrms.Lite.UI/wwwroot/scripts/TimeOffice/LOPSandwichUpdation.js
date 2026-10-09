function GetList() {
    debugger;
    var Month = $('#Month_Code').val();
    var Payroll = $('#Payroll').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'POST',
        url: '/TimeOffice/LOPSandwichUpdation/GetList',
        dataType: 'html',
        data: filter + "&Month=" + Month + "&Payroll=" + Payroll,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#lopsandupdate').html("");
                $('#lopsandupdate').html(result);
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


function lopsandsave() {
    debugger;
    var Month = $('#Month_Code').val();
    var Payroll = $('#Payroll').val();

    debugger;
    if ($('#SelectAll').is(':checked')) {
        var table = $('#lopsandsavetable').DataTable();
        data = table.$('input,select,textarea').serializeArray(); 
    } else {
        data = $('#LOP_SAND_UPDATE').serialize();
    }

    swal.fire({
        text: "Do you want to Save?",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'POST',
                url: '/TimeOffice/LOPSandwichUpdation/LOPSandwichUpdationSave',
                dataType: 'json',
                data: data,
                success: function (result) {
                    if (result.success == true) {
                        swal.fire("Updated Successfully").then(function () {
                            GetList()
                        });
                    } else {
                        swal.fire(result.message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    document.body.id = "kt_body";
                    $('body').html(xhr.responseText);
                }
            });
        } else {
            return false;
        }
    });
}


function ExportToExcel() {
    debugger;
    var Month = $('#Month_Code').val();
    var Payroll = $('#Payroll').val();
    var filter = $('#BasicFilter').serialize();
    var MonthName = $('#Month_Code option:selected').text();
    var PayrollName = $('#Payroll option:selected').text();

    var PARAM = 'data=' + filter + '&Month=' + Month + '&Payroll=' + Payroll + '&MonthName=' + MonthName + '&PayrollName=' + PayrollName;
    var TableLength = $('#LOPSandwichUpdationList_Count').val();
    if (TableLength > 0) {
        url = '/TimeOffice/LOPSandwichUpdation/ExportToExcel?' + PARAM + "&TotalRows=" + TableLength;
        window.location.href = url;
    }
    else {
        swal.fire("No data found to export");
    }
}


function onSelectAllClick() {
    dataTable.destroy()
    if ($('#SelectAll').is(':checked')) {        
        $('#lopsandsavetable').find('input[type=checkbox]').prop('checked', true);
    }
    else {
        $('#lopsandsavetable').find('input[type=checkbox]').prop('checked', false);
        
    }
    pagesetting()
    //var totalRows = $('#lopsandsavetable tbody tr').length;
    //for (let i = 0; i < totalRows; i++) {
    //    onCheckboxChange(document.getElementById('LOPSandwichUpdationList_' + i + '__Applicable'), i)
    //}
}


function pagesetting() {
    dataTable = $('.common-table').DataTable({

        "paging": true,

        "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
        "pagingType": 'full_numbers',
        //"pagingType": 'full_numbers_no_ellipses',
        "scrollY": '60vh',
        "sScrollX": "100%",
        "sScrollXInner": "100%",

        "ordering": false,
        "autoWidth": false,

        "scrollCollapse": false,

        // "dom": '<"top" if>rt<"bottom" lp>',
        "dom": '<t><"my-wrapper"<"my-inner2"l><"my-inner1"i><"my-inner3"p>>',

        language: {

            emptyTable: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 60vh;"><img src="/images/nodata.png" /></div >',

            zeroRecords: '<div style="text-align: center;display: flex;align-items: center;justify-content: center;height: 60vh;"><img src="/images/nodata.png" /></div >',

            sLengthMenu: "_MENU_",
            paginate: {
                //next: '&#8594;', // or '→'
                //previous: '&#8592;' // or '←' 

                next: '<img src="/images/icons/Icon awesome-arrow-right-1.png">',
                previous: '<img src="/images/icons/Icon awesome-arrow-right.png">',
                first: '<img src="/images/icons/Icon metro-next-1.png">',
                last: '<img src="/images/icons/Icon metro-next.png">'
                //next: '<span class="glyphicon glyphicon-menu-right"></span>',
                //previous: '<span class="glyphicon glyphicon-menu-left"></span>'

            }
        }

    });


    //  $(".intro, .demo, .end").css('background', 'green');


    $('.ihits-table table').removeClass("dataTable no-footer");


    $('.paginate_button.next').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.next').attr("title", "Next");
    $('.paginate_button.previous').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.previous').attr("title", "Previous");
    $('.paginate_button.first').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.first').attr("title", "First");
    $('.paginate_button.last').attr("data-bs-toggle", "tooltip");
    $('.paginate_button.last').attr("title", "Last");

    $(".ihits-table table tr th").removeAttr('style');


    $("#dataTableSearch").on("keyup search input paste cut", function () {
        dataTable.search(this.value).draw();


    });

    $('.search-slider-inner').click(function () {
        //dataTable.search('').draw();
        $('#dataTableSearch').val('');
        $('#dataTableSearch').focus();
    });
    debugger;
    var info = dataTable.page.info();

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
}


function TransactionHistory() {
    window.location.href = '/TimeOffice/LOPSandwichUpdation/TransactionHistory'
}


function GetHistoryList() {
    debugger;
    var Month = $('#Month_Code').val();
    var Payroll = $('#Payroll').val();
    var filter = $("#BasicFilter").serialize();
    $.ajax({
        type: 'POST',
        url: '/TimeOffice/LOPSandwichUpdation/GetHistoryList',
        dataType: 'html',
        data: filter + "&Month=" + Month + "&Payroll=" + Payroll,
        success: function (result) {
            debugger
            if ($($.parseHTML(result)).filter(".Login_Session").length > 0) {
                window.location.reload();
            }
            else {
                $('#ListView').html("");
                $('#ListView').html(result);
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


function searchRows() {
    debugger
    var input, filter, table, tr, td, i;
    var img = "<img src='/images/nodata.png' />";

    input = document.getElementById("searchList");
    filter = input.value.toUpperCase();
    table = document.getElementById("lopsandsavetable");
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